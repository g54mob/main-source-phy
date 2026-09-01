using System.Collections.Generic;
using UnityEngine;

public class FrameBufferManager
{
	public class CacheEntry
	{
		public byte[] data;

		public int session;

		public float createTime;

		public void Update(int cacheSession, byte[] cacheData)
		{
			session = cacheSession;
			data = cacheData;
		}
	}

	private Dictionary<uint, CacheEntry> frameCacheBuffer;

	private List<uint> cacheFrames;

	private Dictionary<uint, FragmentedRPC> frameBuffers;

	private Queue<FragmentedRPC> frameBufferPool;

	private static Queue<CacheEntry> entryPool = new Queue<CacheEntry>();

	private bool saveCreateTime;

	public FrameBufferManager(bool saveCreateTime = false)
	{
		this.saveCreateTime = saveCreateTime;
		frameCacheBuffer = new Dictionary<uint, CacheEntry>();
		cacheFrames = new List<uint>();
		frameBufferPool = new Queue<FragmentedRPC>();
		frameBuffers = new Dictionary<uint, FragmentedRPC>();
		for (int i = 0; i < 100; i++)
		{
			frameBufferPool.Enqueue(new FragmentedRPC());
		}
	}

	public void AddCache(uint frame, int session, byte[] data, float createTime = 0f)
	{
		CacheEntry value;
		if (frameCacheBuffer.TryGetValue(frame, out value))
		{
			if (value.session < session)
			{
				value.Update(session, data);
			}
		}
		else
		{
			value = ((entryPool.Count != 0) ? entryPool.Dequeue() : new CacheEntry());
			value.Update(session, data);
			frameCacheBuffer.Add(frame, value);
			int i = 0;
			if (cacheFrames.Count > 0)
			{
				for (; i < cacheFrames.Count && cacheFrames[i] < frame; i++)
				{
				}
			}
			cacheFrames.Insert(i, frame);
		}
		value.createTime = createTime;
	}

	public bool PopCache(uint frame, int session, out CacheEntry cacheData)
	{
		CacheEntry value;
		if (!frameCacheBuffer.TryGetValue(frame, out value) || value.session != session)
		{
			cacheData = null;
			return false;
		}
		cacheData = value;
		RemoveCache(frame, cacheData);
		return true;
	}

	private void RemoveCache(uint frame, CacheEntry entry)
	{
		frameCacheBuffer.Remove(frame);
		cacheFrames.Remove(frame);
		entryPool.Enqueue(entry);
	}

	public void Clear()
	{
		frameCacheBuffer.Clear();
		cacheFrames.Clear();
		List<uint> list = new List<uint>(frameBuffers.Keys);
		foreach (uint item in list)
		{
			Remove(item);
		}
	}

	public void Remove(FragmentedRPC buffer, uint frame)
	{
		frameBuffers.Remove(frame);
		frameBufferPool.Enqueue(buffer);
	}

	public void Remove(uint frame)
	{
		FragmentedRPC value;
		if (frameBuffers.TryGetValue(frame, out value))
		{
			Remove(value, frame);
		}
	}

	public bool Get(uint frame, out FragmentedRPC buffer)
	{
		if (frameBuffers.TryGetValue(frame, out buffer))
		{
			return true;
		}
		if (frameBufferPool.Count == 0)
		{
			buffer = new FragmentedRPC();
		}
		else
		{
			buffer = frameBufferPool.Dequeue();
			buffer.Clear();
		}
		if (saveCreateTime)
		{
			buffer.createTime = Time.time;
		}
		frameBuffers.Add(frame, buffer);
		return true;
	}

	public bool GetOldestCache(int session, out uint cacheFrame, out CacheEntry cacheEntry)
	{
		uint num = 0u;
		int num2 = 0;
		bool flag = false;
		CacheEntry cacheEntry2 = null;
		for (int i = 0; i < cacheFrames.Count; i++)
		{
			uint num3 = cacheFrames[i];
			CacheEntry cacheEntry3 = frameCacheBuffer[num3];
			if (cacheEntry3.session == session)
			{
				flag = true;
				if (num2++ == 0 || num > num3)
				{
					cacheEntry2 = cacheEntry3;
					num = num3;
				}
			}
		}
		if (!flag)
		{
			cacheFrame = 0u;
			cacheEntry = null;
			return false;
		}
		cacheFrame = num;
		cacheEntry = cacheEntry2;
		RemoveCache(cacheFrame, cacheEntry2);
		return true;
	}
}
