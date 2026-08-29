using System;
using System.Collections;
using System.Collections.Generic;

namespace Battlehub.RTCommon
{
	public class UndoStack<T> : IEnumerable<UndoStack<T>.Node>, IEnumerable where T : class
	{
		public class Node
		{
			public Node Next;

			public Node Prev;

			public T Data;

			private UndoStack<T> m_stack;

			public UndoStack<T> Stack => m_stack;

			public Node(UndoStack<T> stack)
			{
				m_stack = stack;
			}
		}

		private Node m_first;

		private Node m_last;

		private Node m_tos;

		private int m_tosIndex;

		private int m_count;

		private T[] m_empty = new T[0];

		public int Count => m_count;

		public bool CanPop => m_tosIndex > 0;

		public bool CanRestore => m_tosIndex < m_count;

		public UndoStack(int size)
		{
			if (size < 1)
			{
				throw new ArgumentOutOfRangeException("size", "size < 1");
			}
			size++;
			m_first = new Node(this);
			Node node = m_first;
			for (int i = 1; i < size; i++)
			{
				node = (node.Next = new Node(this)
				{
					Prev = node
				});
			}
			m_last = node;
			m_last.Next = m_first;
			m_first.Prev = m_last;
			m_tos = m_first;
		}

		public void Push(T item, List<T> purgeList = null)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			if (m_tos == m_last)
			{
				m_last = m_last.Next;
				m_first = m_first.Next;
				if (purgeList != null && m_tos.Next.Data != null)
				{
					purgeList.Add(m_tos.Next.Data);
				}
			}
			else
			{
				if (purgeList != null)
				{
					Node node = m_tos;
					for (int i = 0; i < m_count - m_tosIndex; i++)
					{
						if (node.Data != null)
						{
							purgeList.Add(node.Data);
						}
						node = node.Next;
					}
				}
				m_tosIndex++;
				m_count = m_tosIndex;
			}
			m_tos.Data = item;
			m_tos = m_tos.Next;
			m_tos.Data = null;
		}

		public T Pop()
		{
			if (!CanPop)
			{
				throw new InvalidOperationException("Stack is empty");
			}
			m_tos = m_tos.Prev;
			m_tosIndex--;
			return m_tos.Data;
		}

		public T Peek()
		{
			if (!CanPop)
			{
				throw new InvalidOperationException("Stack is empty");
			}
			return m_tos.Prev.Data;
		}

		public T Restore()
		{
			if (!CanRestore)
			{
				throw new InvalidOperationException("Nothing to restore");
			}
			T data = m_tos.Data;
			m_tos = m_tos.Next;
			m_tosIndex++;
			return data;
		}

		public void Clear()
		{
			Node node = m_first;
			do
			{
				node.Data = null;
				node = node.Next;
			}
			while (node.Prev != m_last);
			m_tosIndex = 0;
			m_count = 0;
			m_tos = m_first;
		}

		public Node Find(T data)
		{
			Node node = m_first;
			do
			{
				if (node.Data == data)
				{
					return node;
				}
				node = node.Next;
			}
			while (node != m_first);
			return null;
		}

		public T Purge(Node node)
		{
			if (node.Stack != this)
			{
				throw new ArgumentException("node does not belong to this stack");
			}
			if (m_count == 0)
			{
				throw new InvalidOperationException("stack is empty");
			}
			if (node.Data == null)
			{
				return null;
			}
			if (node != m_last)
			{
				if (node == m_first)
				{
					m_first = m_first.Next;
				}
				if (m_tos == node)
				{
					m_tos = m_tos.Next;
				}
				Node prev = node.Prev;
				Node next = node.Next;
				if (prev != next)
				{
					prev.Next = next;
					next.Prev = prev;
				}
				m_last.Next = node;
				node.Prev = m_last;
				m_last = node;
				m_last.Next = m_first;
				m_first.Prev = m_last;
			}
			m_count--;
			if (m_count < m_tosIndex)
			{
				m_tosIndex = m_count;
			}
			T data = m_last.Data;
			m_last.Data = null;
			return data;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return _GetEnumerator();
		}

		IEnumerator<Node> IEnumerable<Node>.GetEnumerator()
		{
			return _GetEnumerator();
		}

		private IEnumerator<Node> _GetEnumerator()
		{
			int index = 0;
			Node node = m_first;
			while (index != m_count)
			{
				index++;
				yield return node;
				node = node.Next;
				if (node == m_first)
				{
					break;
				}
			}
		}
	}
}
