using System;
using System.Collections.Generic;
using UnityEngine;

public class OrderedRPC
{
	public enum RPCDestination
	{
		Server = 0,
		Network = 1,
		ServerRequest = 2,
		Player = 3
	}

	public class RPCMessage
	{
		public const int TYPE_OFFSET = 2;

		public const int SIZE_OFFSET = 3;

		public const int HEADER_SIZE = 5;

		public ushort ID;

		public RPCMessageType type;

		public byte[] data;

		public ushort senderPlayerID;

		public bool execute = true;

		public RPCDestination destination;

		public float timeReceived;

		public RPCMessage(ushort id, RPCMessageType messageType, byte[] messageData)
		{
			ID = id;
			type = messageType;
			data = ((messageData == null) ? new byte[0] : messageData);
		}

		public int Size()
		{
			return Size(data.Length);
		}

		public static int Size(int dataSize)
		{
			return 5 + dataSize;
		}

		public static int Size(byte[] data, int offset)
		{
			return Size(NetworkCompression.ReadUInt16(data, offset + 3));
		}

		public byte[] Encode()
		{
			byte[] array = new byte[Size(data.Length)];
			Encode(array, 0);
			return array;
		}

		public void Encode(byte[] buffer, int offset)
		{
			Encode(ID, type, data, 0, data.Length, buffer, offset);
		}

		public static void Encode(ushort id, RPCMessageType type, byte[] data, int dataOffset, int dataCount, byte[] buffer, int offset)
		{
			NetworkCompression.WriteUInt16(id, buffer, offset);
			buffer[offset + 2] = (byte)type;
			NetworkCompression.WriteUInt16((ushort)dataCount, buffer, offset + 3);
			if (dataCount > 0)
			{
				Buffer.BlockCopy(data, dataOffset, buffer, offset + 5, dataCount);
			}
		}

		public static bool Decode(byte[] data, int offset, out RPCMessage message)
		{
			ushort messageID = GetMessageID(data, offset);
			RPCMessageType messageType = (RPCMessageType)data[offset + 2];
			ushort num = NetworkCompression.ReadUInt16(data, offset + 3);
			byte[] array = new byte[num];
			int num2 = offset + 5;
			if (num > data.Length - num2)
			{
				message = null;
				return false;
			}
			Buffer.BlockCopy(data, num2, array, 0, num);
			message = new RPCMessage(messageID, messageType, array);
			message.timeReceived = Time.time;
			return true;
		}
	}

	public class RPCSender
	{
		public ushort ID;

		public List<RPCMessage> dataBuffer;

		public ushort receiveID;

		public static ushort MAX_ID = ushort.MaxValue;

		public RPCSender(ushort playerId)
		{
			ID = playerId;
			dataBuffer = new List<RPCMessage>();
			receiveID = 0;
		}

		public static ushort IncrementID(ushort id)
		{
			ushort num = (ushort)(id + 1);
			if (num == MAX_ID)
			{
				num = 0;
			}
			return num;
		}

		public void ClearBuffer()
		{
			dataBuffer.Clear();
			receiveID = 0;
		}
	}

	public const int MESSAGE_HEADER_SIZE = 5;

	private List<RPCSender> senders;

	private ushort currentSendID;

	private Action<ushort, RPCMessage> rpcFunc;

	private bool isServer;

	private ushort ownerId;

	private static int SKIP_CHECK_AMOUNT = 3;

	public OrderedRPC(Action<ushort, RPCMessage> func)
	{
		rpcFunc = func;
		senders = new List<RPCSender>();
	}

	public static ushort GetMessageID(byte[] data, int offset)
	{
		return NetworkCompression.ReadUInt16(data, offset);
	}

	public static void SetMessageID(ushort id, byte[] data, int offset)
	{
		NetworkCompression.WriteUInt16(id, data, offset);
	}

	public void SetMessageID(byte[] data, int offset)
	{
		SetMessageID(currentSendID, data, offset);
	}

	public void SetNetworkID(ushort playerId)
	{
		isServer = StatMaster.isServer;
		ownerId = playerId;
	}

	public void Clear(ushort playerId)
	{
		RPCSender sender = GetSender(playerId);
		sender.ClearBuffer();
	}

	public void Clear()
	{
		foreach (RPCSender item in new List<RPCSender>(senders))
		{
			Clear(item.ID);
		}
		currentSendID = 0;
	}

	public void SetReceiveID(ushort playerId, ushort id)
	{
		if (!isServer)
		{
			playerId = 0;
		}
		RPCSender sender = GetSender(playerId);
		sender.receiveID = id;
		ProcessBuffer(sender);
	}

	public ushort GetSendID()
	{
		return currentSendID;
	}

	private RPCSender GetSender(ushort playerId)
	{
		for (int i = 0; i < senders.Count; i++)
		{
			if (senders[i].ID == playerId)
			{
				return senders[i];
			}
		}
		RPCSender rPCSender = new RPCSender(playerId);
		senders.Add(rPCSender);
		return rPCSender;
	}

	public void Receive(ushort playerId, RPCMessage message)
	{
		message.senderPlayerID = playerId;
		if (!isServer)
		{
			playerId = 0;
		}
		RPCSender sender = GetSender(playerId);
		if ((isServer && playerId == ownerId) || message.type == RPCMessageType.Init || message.ID == sender.receiveID)
		{
			ExecuteMessage(sender, message);
		}
		else
		{
			sender.dataBuffer.Add(message);
		}
	}

	public bool Receive(ushort playerId, byte[] rpcData, int offset)
	{
		RPCMessage message;
		if (!RPCMessage.Decode(rpcData, offset, out message))
		{
			return false;
		}
		Receive(playerId, message);
		return true;
	}

	public void ProcessBuffer(RPCSender sender)
	{
		List<RPCMessage> list = new List<RPCMessage>(sender.dataBuffer);
		foreach (RPCMessage item in list)
		{
			if (item.ID == sender.receiveID)
			{
				ExecuteMessage(sender, item);
			}
			ushort id = sender.receiveID;
			for (int i = 0; i < SKIP_CHECK_AMOUNT; i++)
			{
				id = RPCSender.IncrementID(id);
			}
			if (item.ID <= sender.receiveID)
			{
				sender.dataBuffer.Remove(item);
			}
		}
	}

	private void ExecuteMessage(RPCSender sender, RPCMessage message)
	{
		ushort arg = ((!isServer) ? message.senderPlayerID : sender.ID);
		try
		{
			rpcFunc(arg, message);
		}
		catch (Exception ex)
		{
			Debug.LogWarning("NRE occurred: " + ex.ToString());
		}
		sender.receiveID = RPCSender.IncrementID(sender.receiveID);
		ProcessBuffer(sender);
	}

	public void Send(RPCMessageType type, byte[] messageData, int dataOffset, int dataCount, bool increaseID, byte[] buffer, int offset)
	{
		RPCMessage.Encode(currentSendID, type, messageData, dataOffset, dataCount, buffer, offset);
		if (increaseID)
		{
			IncrementSendID();
		}
	}

	public void IncrementSendID()
	{
		currentSendID = RPCSender.IncrementID(currentSendID);
	}
}
