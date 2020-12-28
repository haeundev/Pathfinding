namespace Algorithm
{
    class Room<T>
    {
        public T Data;
        private Room<T> Next;
        private Room<T> Prev;
    }

    class RoomList<T>
    {
        public int Count = 0;
        
    }
}