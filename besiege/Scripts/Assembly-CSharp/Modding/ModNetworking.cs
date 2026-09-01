using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using InternalModding;
using InternalModding.Assemblies;
using InternalModding.Loading;
using InternalModding.Misc;
using InternalModding.Mods;
using Modding.Common;
using UnityEngine;

namespace Modding
{
	public static class ModNetworking
	{
		public class CallbacksWrapper
		{
			public class CallbackWrapper
			{
				private readonly List<Action<Message>> evt;

				internal CallbackWrapper(List<Action<Message>> evt)
				{
					this.evt = evt;
				}

				public static CallbackWrapper operator +(CallbackWrapper wrapper, Action<Message> handler)
				{
					wrapper.evt.Add(handler);
					return wrapper;
				}

				public static CallbackWrapper operator -(CallbackWrapper wrapper, Action<Message> handler)
				{
					wrapper.evt.Remove(handler);
					return wrapper;
				}
			}

			public CallbackWrapper this[MessageType type]
			{
				get
				{
					ModContainer mod;
					Check("Callbacks[messageType]", Assembly.GetCallingAssembly(), out mod);
					return new CallbackWrapper(TypeSpecificCallbacks[mod][type]);
				}
				set
				{
				}
			}
		}

		internal static Dictionary<DataType, uint> DataLengths = new Dictionary<DataType, uint>
		{
			{
				DataType.Entity,
				9u
			},
			{
				DataType.Machine,
				2u
			},
			{
				DataType.Block,
				7u
			}
		};

		private static Dictionary<ModContainer, List<MessageType>> MessageTypes = new Dictionary<ModContainer, List<MessageType>>();

		private static Dictionary<ModContainer, List<Action<Message>>> GenericCallbacks = new Dictionary<ModContainer, List<Action<Message>>>();

		private static Dictionary<ModContainer, Dictionary<MessageType, List<Action<Message>>>> TypeSpecificCallbacks = new Dictionary<ModContainer, Dictionary<MessageType, List<Action<Message>>>>();

		private static DataType[] XDataTypes = new DataType[9]
		{
			DataType.Boolean,
			DataType.Color,
			DataType.Integer,
			DataType.IntegerArray,
			DataType.Single,
			DataType.SingleArray,
			DataType.String,
			DataType.StringArray,
			DataType.Vector3
		};

		public static CallbacksWrapper Callbacks = new CallbacksWrapper();

		private static TransportNetworkManager manager;

		private static FragmentedRPC RPCBuffer = new FragmentedRPC();

		public static bool IsNetworkingReady
		{
			get
			{
				return NetworkAuxAddPiece.Instance != null && NetworkAuxAddPiece.Instance.receivedGameState;
			}
		}

		public static event Action<Message> MessageReceived
		{
			add
			{
				ModContainer mod;
				Check("MessageReceived.add", Assembly.GetCallingAssembly(), out mod);
				GenericCallbacks[mod].Add(value);
			}
			remove
			{
				ModContainer mod;
				Check("MessageReceived.add", Assembly.GetCallingAssembly(), out mod);
				GenericCallbacks[mod].Remove(value);
			}
		}

		internal static bool IsXData(DataType type)
		{
			return XDataTypes.Contains(type);
		}

		internal static XData CreateXData(DataType type, object value)
		{
			switch (type)
			{
			case DataType.Boolean:
				return new XBoolean(string.Empty, (bool)value);
			case DataType.Color:
				return new XColor(string.Empty, (Color)value);
			case DataType.Integer:
				return new XInteger(string.Empty, (int)value);
			case DataType.IntegerArray:
				return new XIntegerArray(string.Empty, (int[])value);
			case DataType.Single:
				return new XSingle(string.Empty, (float)value);
			case DataType.SingleArray:
				return new XSingleArray(string.Empty, (float[])value);
			case DataType.String:
				return new XString(string.Empty, (string)value);
			case DataType.StringArray:
				return new XStringArray(string.Empty, (string[])value);
			case DataType.Vector3:
				return new XVector3(string.Empty, (Vector3)value);
			default:
				return null;
			}
		}

		public static MessageType CreateMessageType(params DataType[] types)
		{
			ModContainer mod;
			Check("CreateMessageType", Assembly.GetCallingAssembly(), out mod);
			if (!mod.Info.MultiplayerCompatible)
			{
				MLog.Warn(mod.Info.Name + " is calling the ModNetworking API but has set MultiplayerCompatible to false!");
			}
			if (!MessageTypes.ContainsKey(mod))
			{
				MessageTypes[mod] = new List<MessageType>();
				TypeSpecificCallbacks[mod] = new Dictionary<MessageType, List<Action<Message>>>();
				GenericCallbacks[mod] = new List<Action<Message>>();
			}
			List<MessageType> list = MessageTypes[mod];
			if (list.Count > 255)
			{
				throw new Exception("More than 255 message types per mod are not supported!");
			}
			byte id = (byte)list.Count;
			MessageType messageType = new MessageType(id, types, mod);
			list.Add(messageType);
			TypeSpecificCallbacks[mod][messageType] = new List<Action<Message>>();
			return messageType;
		}

		public static void SendToAll(Message message)
		{
			ModContainer mod;
			if (Check("SendMessage", Assembly.GetCallingAssembly(), out mod))
			{
				message.Sender = Player.GetLocalPlayer();
				message.Destination = ushort.MaxValue;
				SendMessage(message);
			}
		}

		public static void SendTo(Player player, Message message)
		{
			ModContainer mod;
			if (Check("SendMessage", Assembly.GetCallingAssembly(), out mod))
			{
				message.Sender = Player.GetLocalPlayer();
				message.Destination = player.NetworkId;
				SendMessage(message);
			}
		}

		public static void SendToHost(Message message)
		{
			ModContainer mod;
			if (Check("SendMessage", Assembly.GetCallingAssembly(), out mod))
			{
				message.Sender = Player.GetLocalPlayer();
				message.Destination = 0;
				SendMessage(message);
			}
		}

		public static void SendInSimulation(Message message)
		{
			ModContainer mod;
			if (!Check("SendInSimulation", Assembly.GetCallingAssembly(), out mod) || StatMaster.InLocalPlayMode)
			{
				return;
			}
			message.Sender = Player.GetLocalPlayer();
			foreach (PlayerData player in Playerlist.Players)
			{
				if (!player.isLocalPlayer && (player.PlayMode == BesiegePlayMode.GlobalSimulation || player.PlayMode == BesiegePlayMode.BuildMode))
				{
					message.Destination = player.networkId;
					SendMessage(message);
				}
			}
		}

		private static List<byte[]> FragmentMessage(byte[] rawBytes, OrderedRPC.RPCDestination destination)
		{
			List<byte[]> byteList = new List<byte[]>();
			FragmentedRPC.Send(delegate(ushort current, byte[] bytes)
			{
				NetworkCompression.WriteUInt16(current, bytes, 0);
				byteList.Add(bytes);
			}, rawBytes, ((destination != OrderedRPC.RPCDestination.Server) ? manager.PlayerMessageHeaderSize() : manager.ServerMessageHeaderSize()) + 5, 2);
			return byteList;
		}

		private static OrderedRPC.RPCDestination GetRPCDestination(ushort destination)
		{
			int result;
			switch (destination)
			{
			case 0:
				result = 0;
				break;
			case ushort.MaxValue:
				result = 1;
				break;
			default:
				result = 3;
				break;
			}
			return (OrderedRPC.RPCDestination)result;
		}

		private static void SendMessage(Message message)
		{
			if (message.Destination == PlayerData.localPlayer.networkId)
			{
				return;
			}
			List<byte[]> list = FragmentMessage(message.Type.EncodeMessage(message), GetRPCDestination(message.Destination));
			if (StatMaster.isHosting)
			{
				if (message.Destination != ushort.MaxValue)
				{
					foreach (byte[] item in list)
					{
						manager.SendModData(message.Destination, item);
					}
					return;
				}
				{
					foreach (PlayerData player in Playerlist.Players)
					{
						if (player.isLocalPlayer)
						{
							continue;
						}
						foreach (byte[] item2 in list)
						{
							manager.SendModData(player.networkId, item2);
						}
					}
					return;
				}
			}
			foreach (byte[] item3 in list)
			{
				manager.SendModData(0, item3);
			}
		}

		internal static void OnRawMessage(ushort playerId, byte[] buffer, int bufferSize)
		{
			if (!IsNetworkingReady)
			{
				return;
			}
			ushort index = NetworkCompression.ReadUInt16(buffer, 0);
			byte[] data = buffer.Slice(2, bufferSize);
			byte[] outData;
			if (!RPCBuffer.Add(playerId, index, data, out outData))
			{
				return;
			}
			ushort effectiveId = NetworkCompression.ReadUInt16(outData, 0);
			byte b = outData[2];
			ushort networkId = NetworkCompression.ReadUInt16(outData, 3);
			ushort num = NetworkCompression.ReadUInt16(outData, 5);
			if (StatMaster.isHosting && (num == ushort.MaxValue || num != 0))
			{
				List<byte[]> list = FragmentMessage(outData, GetRPCDestination(num));
				switch (num)
				{
				case ushort.MaxValue:
					foreach (PlayerData player in Playerlist.Players)
					{
						if (player.isLocalPlayer || player.networkId == playerId)
						{
							continue;
						}
						foreach (byte[] item in list)
						{
							manager.SendModData(player.networkId, item);
						}
					}
					break;
				default:
					foreach (byte[] item2 in list)
					{
						manager.SendModData(num, item2);
					}
					break;
				case 0:
					break;
				}
			}
			if (num == ushort.MaxValue || num == PlayerData.localPlayer.networkId)
			{
				ModContainer modByEffectiveId = ModIds.GetModByEffectiveId(effectiveId);
				if (modByEffectiveId != null && MessageTypes.ContainsKey(modByEffectiveId) && MessageTypes[modByEffectiveId].Count > b)
				{
					MessageType messageType = MessageTypes[modByEffectiveId][b];
					Message message = messageType.DecodeMessage(outData);
					message.Sender = Player.From(networkId);
					Invoke(GenericCallbacks[modByEffectiveId], message);
					Invoke(TypeSpecificCallbacks[modByEffectiveId][messageType], message);
				}
			}
		}

		internal static void Initialize()
		{
			ReferenceMaster.OnConnect += delegate
			{
				manager = BesiegeNetworkManager.Instance.gameObject.GetComponent<TransportNetworkManager>();
			};
		}

		private static void Invoke<T>(IEnumerable<Action<T>> actions, T arg)
		{
			foreach (Action<T> action in actions)
			{
				ModdingUtil.PerformCallback(delegate
				{
					action(arg);
				});
			}
		}

		private static bool Check(string name, Assembly caller, out ModContainer mod)
		{
			mod = AssemblyLoader.GetModByAssembly(caller);
			if (mod == null)
			{
				throw new InvalidOperationException(name + " called from an assembly not listed in the mod manifest.");
			}
			return (StatMaster.isClient || StatMaster.isHosting) && IsNetworkingReady;
		}
	}
}
