namespace Algorithm
{
    class MyList<T>
    {
        private const int DEFAULT_SIZE = 1;
        T[] _data = new T[DEFAULT_SIZE];

        public int Count = 0;
        public int Capacity => _data.Length;

        // O(1)
        // 이러한 이사 비용은 예외적으로 상수 시간복잡도를 가진다.
        public void Add(T item)
        {
            // 1. 공간이 충분히 남아 있는지 확인한다.
            if (Count > Capacity)
            {
                // 공간을 다시 늘려서 확보한다.
                T[] newArray = new T[Count * 2];
                for (int i = 0; i < Count; i++)
                {
                    newArray[i] = _data[i];
                }

                _data = newArray;
            }
            //2. 공간에 데이터를 넣어준다.
            _data[Count] = item;
            Count++;
        }

        // O(1)
        public T this[int index]
        {
            get => _data[index];
            set => _data[index] = value;
        }

        // O(N)
        public void RemoveAt(int index)
        {
            for (int i = index; i < Count - 1; i++)
            {
                _data[i] = _data[i + 1];
            }
            _data[Count - 1] = default(T);
            Count--;
        }
    }
}