using System;
using System.Collections;

internal class ClonedEnumerator : IEnumerator, IDisposable
{
	public class EnumeratorWrapper
	{
		public int Clones { get; set; }

		public IEnumerator Enumerator { get; set; }
	}

	public class Node
	{
		public object Value { get; set; }

		public Node Next { get; set; }
	}

	private Node _Node;

	private EnumeratorWrapper _Enumerator;

	public object Current => _Node.Value;

	object IEnumerator.Current => Current;

	public ClonedEnumerator(EnumeratorWrapper enumerator, Node firstNode)
	{
		_Enumerator = enumerator;
		_Node = firstNode;
	}

	public void Dispose()
	{
		_Enumerator.Clones--;
		if (_Enumerator.Clones == 0)
		{
			_Enumerator.Enumerator = null;
		}
	}

	public bool MoveNext()
	{
		if (_Node.Next != null)
		{
			_Node = _Node.Next;
			return true;
		}
		if (_Enumerator.Enumerator.MoveNext())
		{
			_Node.Next = new Node
			{
				Value = _Enumerator.Enumerator.Current,
				Next = null
			};
			_Node = _Node.Next;
			return true;
		}
		return false;
	}

	public void Reset()
	{
		throw new NotImplementedException();
	}
}
