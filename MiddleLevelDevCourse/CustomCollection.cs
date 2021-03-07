using System;
using System.Collections.Generic;
using System.Linq;

namespace MiddleLevelDevCourse
{
    public class CustomCollection<T> where T : class
    {
        #region Fields

        private T[] _array;
        private Dictionary<int, T> _items;

        private readonly int _gap;
        private readonly int _size;
        private readonly Func<int, int> _hash;

        #endregion

        #region .ctors

        public CustomCollection() : this(HashFunctions.NoHash) { }

        public CustomCollection(Func<int, int> hash, int size = 10, int gap = 10)
        {
            if (size < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            if (gap < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(size));
            }

            _hash = hash ?? throw new ArgumentNullException(nameof(hash));
            _size = size;
            _gap = gap;
            _array = new T[size];
        }

        #endregion

        #region Logic

        public int InitialCapacity => _size;

        public int Count
        {
            get
            {
                if (_items == null)
                {
                    return _array.Where(i => i != null).Count();
                }

                return _items.Count;
            }
        }

        public void Add(int key, T value)
        {
            if(_items != null)
            {
                _items.Add(_hash(key), value);
            }
            else if (key < _array.Length)
            {
                // Key fits into array, just add value.
                _array[key] = value;
            }
            else if (key - _array.Length < _gap)
            {
                // Resize array and add value if it fits into the gap.
                var array = new T[key + _size];
                Array.Copy(_array, array, _array.Length);

                array[key] = value;

                _array = array;
            }
            else if (_items == null)
            {
                _items = new Dictionary<int, T>();
                for (int i = 0; i < _array.Length; i++)
                {
                    if (_array[i] != null)
                    {
                        _items.Add(_hash(i), _array[i]);
                    }
                }
            }
        }

        public void Remove(int key)
        {
            if (_items == null)
            {
                if (key >= _items.Count)
                {
                    throw new ArgumentOutOfRangeException(nameof(key));
                }

                _array[key] = null;
            }

            _items.Remove(key);
        }

        public T this[int key] => Get(key);

        private T Get(int key)
        {
            if (_items == null)
            {
                if (key >= _array.Length)
                {
                    return null;
                }

                return _array[key];
            }

            return _items[_hash(key)];
        }

        #endregion
    }
}