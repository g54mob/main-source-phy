using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace InternalModding.Mods
{
	public class ModList
	{
		public enum MismatchType
		{
			MissingLocally = 0,
			MissingOnServer = 1,
			VersionDoesntMatch = 2
		}

		public class Mod
		{
			private const char seperator = '~';

			public Guid Id;

			public bool Workshop;

			public ulong WorkshopId;

			public string Version;

			public string Name;

			public Texture2D Icon;

			public ModContainer BackingMod;

			public MismatchType Mismatch;

			private Mod()
			{
			}

			public byte[] GetBytes(bool forHash = false)
			{
				byte[] array = Id.ToByteArray();
				byte[] array2 = ((!forHash) ? BitConverter.GetBytes(Workshop) : new byte[0]);
				byte[] array3 = ((!forHash) ? BitConverter.GetBytes(WorkshopId) : new byte[0]);
				byte[] bytes = Encoding.UTF8.GetBytes(Version);
				int num = NetworkCompression.PackedUIntLength(bytes.Length, false);
				byte[] bytes2 = Encoding.UTF8.GetBytes(Name);
				int num2 = NetworkCompression.PackedUIntLength(bytes2.Length, false);
				bool flag = !forHash && Icon != null && Icon != SingleInstanceFindOnly<ModManager>.Instance.NoThumbnailTexture;
				byte[] bytes3 = BitConverter.GetBytes(flag);
				Texture2D texture2D = null;
				if (flag)
				{
					if (BackingMod == null)
					{
						texture2D = Icon;
					}
					else if (BackingMod.SmallIcon != null)
					{
						texture2D = BackingMod.SmallIcon;
					}
					else
					{
						BackingMod.SmallIcon = new Texture2D(Icon.width, Icon.height, Icon.format, false);
						Graphics.CopyTexture(Icon, BackingMod.SmallIcon);
						TextureScale.Bilinear(BackingMod.SmallIcon, 64, 64);
						texture2D = BackingMod.SmallIcon;
					}
				}
				byte[] array4 = ((!flag) ? new byte[0] : CLZF2.Compress(texture2D.EncodeToPNG()));
				int num3 = (flag ? NetworkCompression.PackedUIntLength(array4.Length, false) : 0);
				byte[] array5 = new byte[array.Length + array2.Length + array3.Length + num + bytes.Length + num2 + bytes2.Length + bytes3.Length + num3 + array4.Length];
				int num4 = 0;
				Buffer.BlockCopy(array, 0, array5, num4, array.Length);
				num4 += array.Length;
				Buffer.BlockCopy(array2, 0, array5, num4, array2.Length);
				num4 += array2.Length;
				Buffer.BlockCopy(array3, 0, array5, num4, array3.Length);
				num4 += array3.Length;
				NetworkCompression.PackUInt(bytes.Length, array5, num4, false, num);
				num4 += num;
				Buffer.BlockCopy(bytes, 0, array5, num4, bytes.Length);
				num4 += bytes.Length;
				NetworkCompression.PackUInt(bytes2.Length, array5, num4, false, num2);
				num4 += num2;
				Buffer.BlockCopy(bytes2, 0, array5, num4, bytes2.Length);
				num4 += bytes2.Length;
				Buffer.BlockCopy(bytes3, 0, array5, num4, bytes3.Length);
				num4 += bytes3.Length;
				if (flag)
				{
					NetworkCompression.PackUInt(array4.Length, array5, num4, false, num3);
					num4 += num3;
					Buffer.BlockCopy(array4, 0, array5, num4, array4.Length);
					num4 += array4.Length;
				}
				return array5;
			}

			public string GetString()
			{
				return Id.ToString() + '~' + ((!Workshop) ? "L" : "W") + '~' + ((!Workshop) ? string.Empty : (WorkshopId.ToString() + '~')) + Version + '~' + Name;
			}

			public Mod CreateMismatch(MismatchType mismatch)
			{
				Mod mod = new Mod();
				mod.Id = Id;
				mod.Workshop = Workshop;
				mod.WorkshopId = WorkshopId;
				mod.Version = Version;
				mod.Name = Name;
				mod.Icon = Icon;
				mod.BackingMod = BackingMod;
				mod.Mismatch = mismatch;
				return mod;
			}

			public static Mod FromContainer(ModContainer mod)
			{
				Mod mod2 = new Mod();
				mod2.Id = mod.Info.Id;
				mod2.Workshop = mod.Info.FromWorkshop;
				mod2.WorkshopId = mod.Info.WorkshopId;
				mod2.Version = mod.Info.Version.ToString();
				mod2.Name = mod.Info.Name;
				mod2.Icon = mod.Info.Icon;
				mod2.BackingMod = mod;
				return mod2;
			}

			public static Mod FromBytes(byte[] buffer, ref int offset)
			{
				Guid id = new Guid(buffer.Slice(offset, offset + 16));
				offset += 16;
				bool workshop = BitConverter.ToBoolean(buffer, offset);
				offset++;
				ulong workshopId = BitConverter.ToUInt64(buffer, offset);
				offset += 8;
				int count;
				offset += NetworkCompression.UnpackUInt(buffer, offset, false, out count);
				string version = Encoding.UTF8.GetString(buffer, offset, count);
				offset += count;
				int count2;
				offset += NetworkCompression.UnpackUInt(buffer, offset, false, out count2);
				string name = Encoding.UTF8.GetString(buffer, offset, count2);
				offset += count2;
				bool flag = BitConverter.ToBoolean(buffer, offset);
				offset++;
				Texture2D texture2D = null;
				if (flag)
				{
					int count3;
					offset += NetworkCompression.UnpackUInt(buffer, offset, false, out count3);
					byte[] inputBytes = buffer.Slice(offset, offset + count3);
					offset += count3;
					texture2D = new Texture2D(0, 0);
					texture2D.LoadImage(CLZF2.Decompress(inputBytes));
				}
				else
				{
					texture2D = SingleInstanceFindOnly<ModManager>.Instance.NoThumbnailTexture;
				}
				Mod mod = new Mod();
				mod.Id = id;
				mod.Workshop = workshop;
				mod.WorkshopId = workshopId;
				mod.Name = name;
				mod.Version = version;
				mod.Icon = texture2D;
				return mod;
			}

			public static Mod FromString(string input)
			{
				string[] array = input.Split('~');
				if (array.Length < 4)
				{
					throw new Exception("Invalid mod specifier: " + input);
				}
				int num = 0;
				Guid id = new Guid(array[num]);
				num++;
				bool flag = array[num] == "W";
				num++;
				ulong workshopId = 0uL;
				if (flag)
				{
					workshopId = ulong.Parse(array[num]);
					num++;
				}
				string version = array[num];
				num++;
				string text = array[num];
				num++;
				for (int i = num; i < array.Length; i++)
				{
					text = text + '~' + array[i];
				}
				Mod mod = new Mod();
				mod.Id = id;
				mod.Workshop = flag;
				mod.WorkshopId = workshopId;
				mod.Name = text;
				mod.Version = version;
				return mod;
			}
		}

		public List<Mod> Mods;

		private ModList(IEnumerable<Mod> mods)
		{
			Mods = mods.OrderBy((Mod m) => m.Id).ToList();
		}

		public byte[] GetBytes(bool forHash = false)
		{
			int num = NetworkCompression.PackedUIntLength(Mods.Count, false);
			byte[][] array = Mods.Select((Mod m) => m.GetBytes(forHash)).ToArray();
			byte[] array2 = new byte[num + array.Sum((byte[] b) => b.Length)];
			int num2 = 0;
			NetworkCompression.PackUInt(Mods.Count, array2, num2, false, num);
			num2 += num;
			for (int num3 = 0; num3 < Mods.Count; num3++)
			{
				Buffer.BlockCopy(array[num3], 0, array2, num2, array[num3].Length);
				num2 += array[num3].Length;
			}
			return array2;
		}

		public string[] GetStringArray()
		{
			return Mods.Select((Mod m) => m.GetString()).ToArray();
		}

		public static ModList FromMods(IEnumerable<ModContainer> mods)
		{
			return new ModList(mods.Select(Mod.FromContainer).ToList());
		}

		public static ModList GetEmpty()
		{
			return new ModList(new List<Mod>());
		}

		public static ModList GetLocal()
		{
			IEnumerable<ModContainer> mods = ModManager.Mods.Where((ModContainer m) => m.IsActive || (m.IsEnabled && m.Info.MultiplayerCompatible));
			return FromMods(mods);
		}

		public static ModList GetLocalAll()
		{
			List<ModContainer> mods = ModManager.Mods;
			return FromMods(mods);
		}

		public static ModList FromBytes(byte[] buffer, ref int offset)
		{
			int count;
			offset += NetworkCompression.UnpackUInt(buffer, offset, false, out count);
			List<Mod> list = new List<Mod>(count);
			for (int i = 0; i < count; i++)
			{
				list.Add(Mod.FromBytes(buffer, ref offset));
			}
			return new ModList(list);
		}

		public static ModList FromStringArray(string[] input)
		{
			if (input.Length == 0 || (input.Length == 1 && string.IsNullOrEmpty(input[0])))
			{
				return GetEmpty();
			}
			return new ModList(input.Select(Mod.FromString).ToList());
		}

		public bool Compare(ModList remote, out List<Mod> mismatchedMods, bool allowExtras = false, bool compareVersions = true)
		{
			mismatchedMods = new List<Mod>();
			Mod remoteMod;
			foreach (Mod mod3 in remote.Mods)
			{
				remoteMod = mod3;
				Mod mod = Mods.FirstOrDefault((Mod m) => m.Id == remoteMod.Id);
				if (mod == null)
				{
					mismatchedMods.Add(remoteMod.CreateMismatch(MismatchType.MissingLocally));
				}
				else if (mod.Version != remoteMod.Version && compareVersions)
				{
					mismatchedMods.Add(remoteMod.CreateMismatch(MismatchType.VersionDoesntMatch));
				}
			}
			if (!allowExtras)
			{
				Mod myMod;
				foreach (Mod mod4 in Mods)
				{
					myMod = mod4;
					Mod mod2 = remote.Mods.FirstOrDefault((Mod m) => m.Id == myMod.Id);
					if (mod2 == null)
					{
						mismatchedMods.Add(myMod.CreateMismatch(MismatchType.MissingOnServer));
					}
				}
			}
			return mismatchedMods.Count == 0;
		}
	}
}
