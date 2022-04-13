using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.DoNotChange;

namespace Tasks
{
    public class DoublyLinkedList<T> : IDoublyLinkedList<T>
    {
        private Node<T> _tail;
        private Node<T> _head;

        public int Length { get; private set; }

        public void Add(T e)
        {
            var node = new Node<T>(e);

            if (_tail is null)
            {
                _head = node;
            }
            else
            {
                node.Previous = _tail;
                _tail.Next = node;
            }

            _tail = node;
            Length++;
        }

        public void AddAt(int index, T e)
        {
            var node = new Node<T>(e);

            if (index == 0)
            {
                node.Next = _head;
                _head.Previous = node;
                _head = node;
            }
            else if (index == Length)
            {
                _tail.Next = node;
                node.Previous = _tail;
                _tail = node;
            }
            else
            {
                var current = GetNodeAt(index);
                node.Previous = current.Previous;
                node.Next = current;
                current.Previous.Next = node;
                current.Previous = node;
            }

            Length++;
        }

        public T ElementAt(int index)
        {
            if (index >= Length || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            return GetNodeAt(index).Data;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var node = _head;
            while (node != null)
            {
                yield return node.Data;
                node = node.Next;
            }
        }

        public void Remove(T item)
        {
            var node = _head;

            while (node != null)
            {
                if (node.Data.Equals(item))
                {
                    RemoveItem(node);
                    break;
                }
                
                node = node.Next;
            }
        }

        public T RemoveAt(int index)
        {
            if (index >= Length || index < 0)
            {
                throw new IndexOutOfRangeException();
            }

            var current = GetNodeAt(index);
            RemoveItem(current);
            return current.Data;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private Node<T> GetNodeAt(int index)
        {
            var node = _head;
            var i = 0;

            while (node != null)
            {
                if (i++ == index)
                {
                    break;
                }
                
                node = node.Next;
            }

            return node;
        }

        private void RemoveItem(Node<T> node)
        {
            if (node.Previous != null)
            {
                node.Previous.Next = node.Next;
            }
            else
            {
                _head = node.Next;
            }

            if (node.Next != null)
            {
                node.Next.Previous = node.Previous;
            }
            else
            {
                _tail = node.Previous;
            }

            Length--;
        }
    }
}
