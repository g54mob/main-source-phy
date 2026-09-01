using System;
using InternalModding.Loading;
using InternalModding.Mods;
using Modding.Blocks;
using Modding.Common;
using Modding.Levels;
using UnityEngine;

namespace Modding
{
	public class MessageType
	{
		internal DataType[] types;

		internal ModContainer mod;

		public byte ID { get; private set; }

		internal MessageType(byte id, DataType[] types, ModContainer mod)
		{
			ID = id;
			this.types = types;
			this.mod = mod;
		}

		public Message CreateMessage(params object[] objs)
		{
			if (objs.Length != types.Length)
			{
				throw new Exception("Tried to create message with wrong number of arguments!");
			}
			Message message = new Message();
			message.Type = this;
			message.objects = objs;
			return message;
		}

		internal Message DecodeMessage(byte[] buffer)
		{
			Message message = new Message();
			message.Type = this;
			message.objects = new object[types.Length];
			Message message2 = message;
			int num = 7;
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == DataType.ByteArray)
				{
					uint num2 = NetworkCompression.ReadUInt(false, buffer, num);
					byte[] array = new byte[num2];
					num += 4;
					Buffer.BlockCopy(buffer, num, array, 0, (int)num2);
					num += (int)num2;
					message2.objects[i] = array;
				}
				else if (ModNetworking.IsXData(types[i]))
				{
					uint num3 = NetworkCompression.ReadUInt(false, buffer, num);
					num += 4;
					XData xData;
					switch (types[i])
					{
					case DataType.Boolean:
						xData = new XBoolean(string.Empty);
						break;
					case DataType.Color:
						xData = new XColor(string.Empty);
						break;
					case DataType.Integer:
						xData = new XInteger(string.Empty);
						break;
					case DataType.IntegerArray:
						xData = new XIntegerArray(string.Empty);
						break;
					case DataType.Single:
						xData = new XSingle(string.Empty);
						break;
					case DataType.SingleArray:
						xData = new XSingleArray(string.Empty);
						break;
					case DataType.String:
						xData = new XString(string.Empty);
						break;
					case DataType.StringArray:
						xData = new XStringArray(string.Empty);
						break;
					case DataType.Vector3:
						xData = new XVector3(string.Empty);
						break;
					default:
						xData = null;
						break;
					}
					if (xData != null)
					{
						xData.Decode(buffer, num);
						num += (int)num3;
						message2.objects[i] = xData.RawValue;
					}
					else
					{
						message2.objects[i] = null;
						Debug.LogError(string.Concat("ModNetworking::Decode: Invalid type (", types[i], ")!"));
					}
				}
				else if (types[i] == DataType.Entity)
				{
					bool flag = buffer[num] == 1;
					num++;
					long id = BitConverter.ToInt64(buffer, num);
					num += 8;
					Entity entity = Entity.From(id);
					if (entity == null)
					{
						message2.objects[i] = null;
					}
					else
					{
						message2.objects[i] = ((!flag) ? entity.SimEntity : entity.BuildEntity);
					}
				}
				else if (types[i] == DataType.Machine)
				{
					ushort networkId = NetworkCompression.ReadUInt16(buffer, num);
					num += 2;
					Player player = Player.From(networkId);
					message2.objects[i] = ((!(player != null) || player.IsSpectator) ? null : player.Machine);
				}
				else
				{
					if (types[i] != DataType.Block)
					{
						continue;
					}
					bool flag2 = buffer[num] == 1;
					num++;
					ushort networkId2 = NetworkCompression.ReadUInt16(buffer, num);
					num += 2;
					int num4 = (int)NetworkCompression.ReadUInt(false, buffer, num);
					num += 4;
					if (num4 == -1)
					{
						message2.objects[i] = null;
						continue;
					}
					Player player2 = Player.From(networkId2);
					if (player2 == null || player2.IsSpectator)
					{
						message2.objects[i] = null;
						continue;
					}
					PlayerMachine machine = player2.Machine;
					BlockBehaviour block;
					if (!machine.InternalObject.GetBlockFromIndex(num4, out block) || block == null)
					{
						message2.objects[i] = null;
						continue;
					}
					Block block2 = Block.From(block);
					message2.objects[i] = ((!flag2) ? block2.SimBlock : block2.BuildingBlock);
				}
			}
			return message2;
		}

		internal byte[] EncodeMessage(Message msg)
		{
			long num = 0L;
			num += 2;
			num++;
			num += 2;
			num += 2;
			byte[][] array = new byte[msg.objects.Length][];
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == DataType.ByteArray)
				{
					num += 4;
					num += ((byte[])msg.objects[i]).Length;
				}
				else if (ModNetworking.IsXData(types[i]))
				{
					num += 4;
					array[i] = ModNetworking.CreateXData(types[i], msg.objects[i]).Encode();
					num += array[i].Length;
				}
				else
				{
					num += ModNetworking.DataLengths[types[i]];
				}
			}
			byte[] array2 = new byte[num];
			NetworkCompression.WriteUInt16(ModIds.GetEffectiveModId(mod), array2, 0);
			array2[2] = ID;
			NetworkCompression.WriteUInt16(msg.Sender.NetworkId, array2, 3);
			NetworkCompression.WriteUInt16(msg.Destination, array2, 5);
			int num2 = 7;
			for (int j = 0; j < types.Length; j++)
			{
				if (types[j] == DataType.ByteArray || ModNetworking.IsXData(types[j]))
				{
					byte[] array3 = ((types[j] != DataType.ByteArray) ? array[j] : ((byte[])msg.objects[j]));
					NetworkCompression.WriteUInt((uint)array3.Length, false, array2, num2);
					num2 += 4;
					Buffer.BlockCopy(array3, 0, array2, num2, array3.Length);
					num2 += array3.Length;
				}
				else if (types[j] == DataType.Entity)
				{
					LevelEntity levelEntity = ((!(msg.objects[j] is Entity)) ? ((LevelEntity)msg.objects[j]) : ((Entity)msg.objects[j]).InternalObject);
					byte b = ((!(levelEntity == null) && !levelEntity.isSimulating) ? ((byte)1) : ((byte)0));
					long value = ((!(levelEntity == null)) ? levelEntity.identifier : LevelPrefab.INVALID_ID);
					byte[] bytes = BitConverter.GetBytes(value);
					array2[num2] = b;
					num2++;
					Buffer.BlockCopy(bytes, 0, array2, num2, bytes.Length);
					num2 += bytes.Length;
				}
				else if (types[j] == DataType.Machine)
				{
					Machine machine = ((!(msg.objects[j] is PlayerMachine)) ? ((Machine)msg.objects[j]) : ((PlayerMachine)msg.objects[j]).InternalObject);
					ushort val = ((!(machine == null)) ? machine.PlayerID : ushort.MaxValue);
					NetworkCompression.WriteUInt16(val, array2, num2);
					num2 += 2;
				}
				else if (types[j] == DataType.Block)
				{
					BlockBehaviour blockBehaviour = ((!(msg.objects[j] is Block)) ? ((BlockBehaviour)msg.objects[j]) : ((Block)msg.objects[j]).InternalObject);
					byte b2 = (byte)((!(blockBehaviour == null)) ? (blockBehaviour.isBuildBlock ? 1u : 0u) : 0u);
					ushort val2 = ((!(blockBehaviour == null)) ? blockBehaviour.ParentMachine.PlayerID : ushort.MaxValue);
					int val3 = ((!(blockBehaviour == null)) ? blockBehaviour.BuildIndex : (-1));
					array2[num2] = b2;
					num2++;
					NetworkCompression.WriteUInt16(val2, array2, num2);
					num2 += 2;
					NetworkCompression.WriteUInt((uint)val3, false, array2, num2);
					num2 += 4;
				}
			}
			return array2;
		}
	}
}
