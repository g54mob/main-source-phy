using System;
using System.Collections.Generic;
using System.Linq;
using InternalModding.Blocks;
using InternalModding.LevelEntities;
using InternalModding.Loading;
using Modding;
using UnityEngine;

namespace InternalModding.Mods
{
	public static class ModStatus
	{
		public static bool IsModDisabled(ModInfo mod)
		{
			XDataHolder data = Configuration.GetData();
			string[] source = ((!data.HasKey("disabled-mods")) ? new string[0] : data.ReadStringArray("disabled-mods"));
			return source.Contains(mod.Id.ToString());
		}

		public static bool IsBlockHidden(ModdedBlock block)
		{
			XDataHolder data = Configuration.GetData();
			string[] source = ((!data.HasKey("hidden-blocks")) ? new string[0] : data.ReadStringArray("hidden-blocks"));
			return source.Contains(string.Concat(block.Info.Mod.Info.Id, " ", block.LocalId));
		}

		public static bool IsEntityHidden(ModdedEntity entity)
		{
			XDataHolder data = Configuration.GetData();
			string[] source = ((!data.HasKey("hidden-entities")) ? new string[0] : data.ReadStringArray("hidden-entities"));
			return source.Contains(string.Concat(entity.Info.Mod.Info.Id, " ", entity.LocalId));
		}

		public static void EnableMod(ModContainer mod)
		{
			if (ModdingUtil.IsInGame())
			{
				throw new InvalidOperationException("Cannot enable a mod while in-game!");
			}
			XDataHolder data = Configuration.GetData();
			string[] source = ((!data.HasKey("disabled-mods")) ? new string[0] : data.ReadStringArray("disabled-mods"));
			data.Write("disabled-mods", source.Where((string id) => id != mod.Info.Id.ToString()).ToArray());
			mod.IsEnabled = true;
			SingleInstanceFindOnly<ModManager>.Instance.RecalculateState();
		}

		public static bool DisableMod(ModContainer mod)
		{
			if (ModdingUtil.IsInGame())
			{
				throw new InvalidOperationException("Cannot disable a mod while in-game!");
			}
			XDataHolder data = Configuration.GetData();
			string[] first = ((!data.HasKey("disabled-mods")) ? new string[0] : data.ReadStringArray("disabled-mods"));
			data.Write("disabled-mods", first.Union(new string[1] { mod.Info.Id.ToString() }).ToArray());
			mod.IsEnabled = false;
			return mod.CurrentState == ModContainer.State.Loaded;
		}

		public static void SetBlockHidden(ModdedBlock block, bool hidden)
		{
			if (ModdingUtil.IsInGame())
			{
				throw new InvalidOperationException("Cannot hide/unhide a block while in-game!");
			}
			XDataHolder data = Configuration.GetData();
			string[] array = ((!data.HasKey("hidden-blocks")) ? new string[0] : data.ReadStringArray("hidden-blocks"));
			if (hidden)
			{
				data.Write("hidden-blocks", array.Union(new string[1] { string.Concat(block.Info.Mod.Info.Id, " ", block.LocalId) }).ToArray());
			}
			else
			{
				data.Write("hidden-blocks", array.Where((string s) => s != string.Concat(block.Info.Mod.Info.Id, " ", block.LocalId)).ToArray());
			}
			block.HideInUI = hidden;
		}

		public static void SetEntityHidden(ModdedEntity entity, bool hidden)
		{
			if (ModdingUtil.IsInGame())
			{
				throw new InvalidOperationException("Cannot hide/unhide an entity while in-game!");
			}
			XDataHolder data = Configuration.GetData();
			string[] array = ((!data.HasKey("hidden-entities")) ? new string[0] : data.ReadStringArray("hidden-entities"));
			if (hidden)
			{
				data.Write("hidden-entities", array.Union(new string[1] { string.Concat(entity.Info.Mod.Info.Id, " ", entity.LocalId) }).ToArray());
			}
			else
			{
				data.Write("hidden-entities", array.Where((string s) => s != string.Concat(entity.Info.Mod.Info.Id, " ", entity.LocalId)).ToArray());
			}
			entity.HideInUI = hidden;
		}

		public static void ReloadBlockEntityHideStatus()
		{
			foreach (ModdedBlock loadedBlock in SingleInstanceFindOnly<BlockLoader>.Instance.LoadedBlocks)
			{
				loadedBlock.HideInUI = IsBlockHidden(loadedBlock);
			}
			foreach (ModdedEntity loadedEntity in SingleInstanceFindOnly<EntityLoader>.Instance.LoadedEntities)
			{
				loadedEntity.HideInUI = IsEntityHidden(loadedEntity);
			}
		}

		public static byte[] EncodeLocalBlockEntityHideStatus()
		{
			XDataHolder data = Configuration.GetData();
			string[] array = ((!data.HasKey("hidden-blocks")) ? new string[0] : data.ReadStringArray("hidden-blocks"));
			string[] array2 = ((!data.HasKey("hidden-entities")) ? new string[0] : data.ReadStringArray("hidden-entities"));
			List<Guid> list = new List<Guid>();
			List<uint> list2 = new List<uint>();
			List<Guid> list3 = new List<Guid>();
			List<uint> list4 = new List<uint>();
			string[] array3 = array;
			foreach (string text in array3)
			{
				string[] array4 = text.Split(' ');
				Guid guid = new Guid(array4[0]);
				uint num = (uint)int.Parse(array4[1]);
				if (ModIds.GetEffectiveBlockId(guid, (int)num) != 0)
				{
					list.Add(guid);
					list2.Add(num);
				}
			}
			string[] array5 = array2;
			foreach (string text2 in array5)
			{
				string[] array6 = text2.Split(' ');
				Guid guid2 = new Guid(array6[0]);
				uint num2 = (uint)int.Parse(array6[1]);
				if (ModIds.GetEffectiveEntityId(guid2, (int)num2) != 0)
				{
					list3.Add(guid2);
					list4.Add(num2);
				}
			}
			int num3 = 8 + 20 * (list.Count + list3.Count);
			byte[] array7 = new byte[num3];
			int num4 = 0;
			NetworkCompression.WriteUInt((uint)list.Count, false, array7, num4);
			num4 += 4;
			NetworkCompression.WriteUInt((uint)list3.Count, false, array7, num4);
			num4 += 4;
			for (int k = 0; k < list.Count + list3.Count; k++)
			{
				Guid guid3 = ((k >= list.Count) ? list3[k] : list[k]);
				uint val = ((k >= list.Count) ? list4[k] : list2[k]);
				byte[] array8 = guid3.ToByteArray();
				Buffer.BlockCopy(array8, 0, array7, num4, array8.Length);
				num4 += array8.Length;
				NetworkCompression.WriteUInt(val, false, array7, num4);
				num4 += 4;
			}
			return array7;
		}

		public static void ApplyRemoteBlockEntityHideStatus(byte[] buffer, ref int offset)
		{
			uint num = NetworkCompression.ReadUInt(false, buffer, offset);
			offset += 4;
			uint num2 = NetworkCompression.ReadUInt(false, buffer, offset);
			offset += 4;
			for (int i = 0; i < num + num2; i++)
			{
				Guid guid = new Guid(buffer.Slice(offset, offset + 16));
				offset += 16;
				uint num3 = NetworkCompression.ReadUInt(false, buffer, offset);
				offset += 4;
				if (i < num)
				{
					ModdedBlock blockByEffectiveId = ModIds.GetBlockByEffectiveId(ModIds.GetEffectiveBlockId(guid, (int)num3));
					if (blockByEffectiveId == null)
					{
						Debug.LogError(string.Concat("Got invalid block hide status for (", guid, ", ", num3, ")!"));
					}
					else
					{
						blockByEffectiveId.HideInUI = true;
					}
				}
				else
				{
					ModdedEntity entityByEffectiveId = ModIds.GetEntityByEffectiveId(ModIds.GetEffectiveEntityId(guid, (int)num3));
					if (entityByEffectiveId == null)
					{
						Debug.LogError(string.Concat("Got invalid entity hide status for (", guid, ", ", num3, ")!"));
					}
					else
					{
						entityByEffectiveId.HideInUI = true;
					}
				}
			}
		}
	}
}
