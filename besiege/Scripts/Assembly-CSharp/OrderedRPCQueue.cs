using System;
using System.Collections.Generic;
using UnityEngine;

public class OrderedRPCQueue : MonoBehaviour
{
	[Serializable]
	private class RPCEntry
	{
		public ushort playerId;

		public OrderedRPC.RPCMessage message;

		public RPCEntry(ushort id, OrderedRPC.RPCMessage rpcMessage)
		{
			playerId = id;
			message = rpcMessage;
		}
	}

	private Queue<RPCEntry> messageList;

	private Action<ushort, OrderedRPC.RPCMessage> executeFunc;

	private int lockCount;

	public bool isLocked
	{
		get
		{
			return lockCount > 0;
		}
	}

	protected void Awake()
	{
		messageList = new Queue<RPCEntry>();
		lockCount = 0;
		base.enabled = false;
	}

	public void Clear()
	{
		messageList.Clear();
		lockCount = 0;
		base.enabled = false;
	}

	public void SetExecuteMethod(Action<ushort, OrderedRPC.RPCMessage> executeFunction)
	{
		executeFunc = executeFunction;
	}

	public void ToggleLock(bool isLocked)
	{
		if (isLocked)
		{
			lockCount++;
		}
		else if (lockCount > 0)
		{
			lockCount--;
		}
		if (lockCount > 0)
		{
			base.enabled = true;
		}
	}

	public void Add(ushort playerId, OrderedRPC.RPCMessage message)
	{
		messageList.Enqueue(new RPCEntry(playerId, message));
		ProcessMessages();
	}

	private void ProcessMessages()
	{
		while (lockCount == 0 && messageList.Count > 0)
		{
			RPCEntry rPCEntry = messageList.Dequeue();
			try
			{
				executeFunc(rPCEntry.playerId, rPCEntry.message);
			}
			catch (Exception ex)
			{
				Debug.LogError("Exception occurred: " + ex.ToString());
			}
		}
		if (lockCount == 0)
		{
			base.enabled = false;
		}
	}

	protected void LateUpdate()
	{
		if (lockCount <= 0)
		{
			ProcessMessages();
		}
	}
}
