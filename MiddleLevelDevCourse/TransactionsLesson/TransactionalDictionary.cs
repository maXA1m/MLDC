using System;
using System.Collections.Generic;
using System.Linq;

namespace MiddleLevelDevCourse.TransactionsLesson
{
    public class TransactionalDictionary<TKey, TValue>
    {
        private readonly object _itemsLock = new();

        private readonly Dictionary<TKey, TValue> _items;
        private readonly Dictionary<DateTime, Transaction<TKey, TValue>> _transactions = new();

        public TransactionalDictionary() : this(new Dictionary<TKey, TValue>())
        {
        }

        public TransactionalDictionary(Dictionary<TKey, TValue> items)
        {
            _items = items;
        }

        public bool HasOpenTransactions => _transactions.Count > 0;

        public int Count => _items.Count;

        public void Add(TKey key, TValue value)
        {
            if (!HasOpenTransactions)
            {
                lock (_itemsLock)
                {
                    _items.Add(key, value);
                }

                return;
            }

            GetActiveTransaction().Add(key, value);
        }

        public void Remove(TKey key)
        {
            if (!HasOpenTransactions)
            {
                lock (_itemsLock)
                {
                    _items.Remove(key);
                }

                return;
            }

            GetActiveTransaction().Remove(key);
        }

        public TValue this[TKey key] => Get(key);

        public TValue Get(TKey key)
        {
            lock (_itemsLock)
            {
                return _items[key];
            }
        }

        /// <summary>
        /// Begins new transaction.
        /// </summary>
        /// <returns>Returns datetime as transaction key.</returns>
        public DateTime BeginTransaction()
        {
            var transactionKey = DateTime.Now;
            _transactions.Add(transactionKey, new Transaction<TKey, TValue>());
            
            return transactionKey;
        }

        /// <summary>
        /// Commits changes for the transaction.
        /// </summary>
        /// <param name="key">Transaction key.</param>
        public void Commit(DateTime key)
        {
            //TODO: transactions sequence, TIME, what to apply first?
            if(_transactions.TryGetValue(key, out var transaction))
            {
                ApplyChanges(transaction);

                _transactions.Remove(key);
            }
        }

        /// <summary>
        /// Removes transaction and all changes.
        /// </summary>
        /// <param name="key">Transaction key.</param>
        public void Rollback(DateTime key)
        {
            if (_transactions.ContainsKey(key))
            {
                _transactions.Remove(key);
            }
        }

        private Transaction<TKey, TValue> GetActiveTransaction()
        {
            // TODO: Which one to select?
            return _transactions.Last().Value;
        }

        private void ApplyChanges(Transaction<TKey, TValue> transaction)
        {
            lock (_itemsLock)
            {
                foreach(var i in transaction.AddItems)
                {
                    _items.Add(i.Key, i.Value);
                }

                foreach (var key in transaction.RemoveItems)
                {
                    _items.Remove(key);
                }
            }
        }
    }
}
