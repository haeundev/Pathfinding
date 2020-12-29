namespace Algorithm
{
    class MyLinkedListNode<T>
    {
        public T Data;
        public MyLinkedListNode<T> Next;
        public MyLinkedListNode<T> Prev;
    }

    class MyLinkedList<T>
    {
        public MyLinkedListNode<T> Head = null;
        public MyLinkedListNode<T> Tail = null;
        public int Count = 0;

        // O(1)
        public MyLinkedListNode<T> AddLast(T data)
        {
            MyLinkedListNode<T> newMyLinkedListNode = new MyLinkedListNode<T>();
            newMyLinkedListNode.Data = data;

            if (Head == null)
                Head = newMyLinkedListNode;
            
            if (Tail != null)
            {
                Tail.Next = newMyLinkedListNode;
                newMyLinkedListNode.Prev = Tail;
            }

            Tail = newMyLinkedListNode;
            Count++;
            return newMyLinkedListNode;
        }

        // O(1)
        public void Remove(MyLinkedListNode<T> myLinkedListNode)
        {
            if (Head == myLinkedListNode)
                Head = Head.Next;

            if (Tail == myLinkedListNode)
                Tail = Tail.Prev;

            if (myLinkedListNode.Prev != null)
                myLinkedListNode.Prev.Next = myLinkedListNode.Next;
            
            if (myLinkedListNode.Next != null)
                myLinkedListNode.Next.Prev = myLinkedListNode.Prev;

            Count--;
        }
    }
}