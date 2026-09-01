using System;
using System.Collections.Concurrent;

public class FixedSizedQueue<T> : ConcurrentQueue<T>
{
	private readonly object syncObject = new object();

	public Action<T> OnItemDequeue;

	public int Size { get; private set; }

	public FixedSizedQueue(int size)
	{
		Size = size;
	}

	public new void Enqueue(T obj)
	{
		base.Enqueue(obj);
		lock (syncObject)
		{
			while (base.Count > Size)
			{
				T result;
				if (TryDequeue(out result) && OnItemDequeue != null)
				{
					OnItemDequeue(result);
				}
			}
		}
	}
}
