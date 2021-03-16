using System.Collections.Generic;

namespace MiddleLevelDevCourse.TransactionsLesson
{
    public class Transaction<TKey, TValue>
    {
        public readonly Dictionary<TKey, TValue> _addItems = new(5);
        public readonly List<TKey> _removeItems = new(5);

        public IReadOnlyDictionary<TKey, TValue> AddItems => _addItems;

        public IReadOnlyCollection<TKey> RemoveItems => _removeItems;

        public void Add(TKey key, TValue value)
        {
            _addItems.Add(key, value);
        }

        public void Remove(TKey key)
        {
            if (_addItems.ContainsKey(key))
            {
                _addItems.Remove(key);
                return;
            }

            _removeItems.Add(key);
        }
    }
}
