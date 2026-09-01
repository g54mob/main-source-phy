using System;
using System.Collections.Generic;
using UdpKit.Collections;

namespace UdpKit
{
	internal class UdpChannelStreamer
	{
		public int Priority;

		public UdpStreamChannel Channel;

		public SortedDictionary<ulong, UdpStreamOp> IncommingData;

		private ulong SendKeyCounter;

		private ulong RecvKeyCounter;

		private readonly UdpConnection Connection;

		internal readonly ObjectPool<UdpStreamOpBlock> streamBlockPool;

		internal readonly Queue<UdpStreamOp> OutgoingData;

		public UdpChannelStreamer(UdpConnection connection, UdpStreamChannel channel)
		{
			Channel = channel;
			Priority = channel.Config.Priority;
			Connection = connection;
			OutgoingData = new Queue<UdpStreamOp>(10);
			IncommingData = new SortedDictionary<ulong, UdpStreamOp>();
			streamBlockPool = new ObjectPool<UdpStreamOpBlock>();
		}

		public void Queue(byte[] data)
		{
			UdpStreamOp udpStreamOp = new UdpStreamOp(SendKeyCounter++, Channel.Name, data);
			InitOp(udpStreamOp, 0u);
			OutgoingData.Enqueue(udpStreamOp);
		}

		public void Clear()
		{
			foreach (UdpStreamOp value in IncommingData.Values)
			{
				RaiseStreamAborted(value);
			}
			IncommingData.Clear();
			OutgoingData.Clear();
		}

		public void OnBlockLost(UdpStreamOp op, int block)
		{
			if (!Channel.IsUnreliable)
			{
				int num = block - op.BlockCurrent;
				op.Pending &= (ulong)(~(1L << num));
			}
		}

		public void OnBlockDelivered(UdpStreamOp op, int block)
		{
			if (!Channel.IsUnreliable)
			{
				int num = block - op.BlockCurrent;
				op.Pending &= (ulong)(~(1L << num));
				op.Delivered |= (ulong)(1L << num);
				while ((op.Delivered & 1) == 1)
				{
					op.Pending >>= 1;
					op.Delivered >>= 1;
					op.BlockCurrent++;
				}
				if (op.BlockCurrent == op.BlockCount)
				{
					op.DoneTime = Connection.Socket.GetCurrentTime();
				}
			}
		}

		public bool TrySend()
		{
			while (OutgoingData.Count > 0 && OutgoingData.Peek().IsDone)
			{
				OutgoingData.Dequeue();
			}
			if (OutgoingData.Count > 0)
			{
				UdpStreamOp udpStreamOp = OutgoingData.Peek();
				int num = Math.Min(udpStreamOp.BlocksRemaining, 64);
				for (int i = 0; i < num; i++)
				{
					ulong num2 = (ulong)(1L << i);
					if ((udpStreamOp.Delivered & num2) != 0L || (udpStreamOp.Pending & num2) != 0L)
					{
						continue;
					}
					if (SendBlock(udpStreamOp, udpStreamOp.BlockCurrent + i))
					{
						if (Channel.IsUnreliable)
						{
							OutgoingData.Dequeue();
						}
						else
						{
							udpStreamOp.Pending |= num2;
						}
						return true;
					}
					return false;
				}
			}
			return false;
		}

		public void OnBlockReceived(byte[] buffer, int bytes, int o)
		{
			ulong num = Blit.ReadU64(buffer, ref o);
			int num2 = Blit.ReadI32(buffer, ref o);
			if (!IncommingData.TryGetValue(num, out var value))
			{
				if (num < RecvKeyCounter)
				{
					return;
				}
				value = new UdpStreamOp(num, Channel.Name, new byte[num2]);
				InitOp(value, 0u);
				RaiseStreamStarted(value);
				IncommingData.Add(value.Key, value);
			}
			if (!value.IsDone)
			{
				RecvBlock(value, buffer, bytes, o);
				ProcessReceivedBlocks();
			}
		}

		internal bool ProcessReceivedBlocks()
		{
			lock (IncommingData)
			{
				if (Channel.IsReliable && IncommingData.TryGetValue(RecvKeyCounter, out var value) && value.IsDone)
				{
					RecvKeyCounter++;
					RaiseStreamReceived(value);
					IncommingData.Remove(value.Key);
					return true;
				}
				return false;
			}
		}

		private void InitOp(UdpStreamOp op, uint crc)
		{
			op.CRC = crc;
			op.BlockSize = Connection.Socket.StreamPipeConfig.DatagramSize - Connection.StreamPipe.Config.HeaderSize;
			op.BlockCount = op.Data.Length / op.BlockSize;
			if (op.Data.Length % op.BlockSize != 0)
			{
				op.BlockCount++;
			}
		}

		private bool SendBlock(UdpStreamOp op, int block)
		{
			byte[] sendBuffer = Connection.Socket.GetSendBuffer();
			UdpStreamOpBlock udpStreamOpBlock = streamBlockPool.Get();
			udpStreamOpBlock.Op = op;
			udpStreamOpBlock.Number = block;
			if (Connection.StreamPipe.WriteHeader(sendBuffer, udpStreamOpBlock))
			{
				int offset = Connection.StreamPipe.Config.HeaderSize;
				int length = Math.Min(op.BlockSize, op.Data.Length - block * op.BlockSize);
				Blit.PackI32(sendBuffer, ref offset, Channel.Name.Id);
				Blit.PackU64(sendBuffer, ref offset, op.Key);
				Blit.PackI32(sendBuffer, ref offset, op.Data.Length);
				Blit.PackI32(sendBuffer, ref offset, block);
				Blit.PackBytes(sendBuffer, ref offset, op.Data, block * op.BlockSize, length);
				Connection.Socket.Send(Connection.RemoteEndPoint, sendBuffer, offset);
				return true;
			}
			return false;
		}

		private void RecvBlock(UdpStreamOp op, byte[] buffer, int bytes, int o)
		{
			int num = Blit.ReadI32(buffer, ref o);
			if (num < op.BlockCurrent)
			{
				return;
			}
			int num2 = num - op.BlockCurrent;
			if (num2 < 0 || num2 >= 64)
			{
				Connection.ConnectionError(UdpConnectionError.InvalidBlockNumber);
				return;
			}
			ulong num3 = (ulong)(1L << num2);
			if (num3 != (op.Delivered & num3))
			{
				Array.Copy(buffer, o, op.Data, op.BlockSize * num, bytes - o);
				op.Delivered |= num3;
				while ((op.Delivered & 1) == 1)
				{
					op.Delivered >>= 1;
					op.BlockCurrent++;
				}
				if (op.BlockCurrent == op.BlockCount)
				{
					op.DoneTime = Connection.Socket.GetCurrentTime();
				}
				else if (Channel.IsUnreliable)
				{
					UdpLog.Error("Received partial block on unreliable {0}", Channel.Name);
				}
				else if (Channel.IsReliable)
				{
					RaiseStreamProgress(op);
				}
			}
		}

		private void RaiseStreamStarted(UdpStreamOp op)
		{
			UdpEventStreamDataStarted udpEventStreamDataStarted = new UdpEventStreamDataStarted
			{
				Connection = Connection,
				ChannelName = Channel.Name,
				streamID = op.Key
			};
			Connection.Socket.Raise(udpEventStreamDataStarted);
		}

		private void RaiseStreamReceived(UdpStreamOp op)
		{
			UdpEventStreamDataReceived udpEventStreamDataReceived = new UdpEventStreamDataReceived
			{
				Connection = Connection,
				StreamData = new UdpStreamData
				{
					Channel = Channel.Name,
					Data = op.Data
				}
			};
			Connection.Socket.Raise(udpEventStreamDataReceived);
		}

		private void RaiseStreamAborted(UdpStreamOp op)
		{
			UdpEventStreamAbort udpEventStreamAbort = new UdpEventStreamAbort
			{
				Connection = Connection,
				ChannelName = Channel.Name,
				streamID = op.Key
			};
			Connection.Socket.Raise(udpEventStreamAbort);
		}

		private void RaiseStreamProgress(UdpStreamOp op)
		{
			float percent = (float)op.BlockCurrent / (float)op.BlockCount;
			UdpEventStreamProgress udpEventStreamProgress = new UdpEventStreamProgress
			{
				Connection = Connection,
				ChannelName = Channel.Name,
				streamID = op.Key,
				percent = percent
			};
			Connection.Socket.Raise(udpEventStreamProgress);
		}
	}
}
