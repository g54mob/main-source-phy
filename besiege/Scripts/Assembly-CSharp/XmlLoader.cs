using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using BesiegeDlc;
using InternalModding.Loading;
using InternalModding.Mods;
using Steamworks;
using UnityEngine;
using XMLTypes;

[AddComponentMenu("XML/XML Loader")]
public static class XmlLoader
{
	private const string NaN = "NaN";

	private const string Infinity = "Infinity";

	private const string nInfinity = "-Infinity";

	public static Dictionary<int, Vector3> version1scaling = new Dictionary<int, Vector3>
	{
		{
			2,
			new Vector3(0.55f, 0.55f, 0.55f)
		},
		{
			10,
			new Vector3(0.55f, 0.55f, 0.55f)
		},
		{
			11,
			new Vector3(0.22f, 0.22f, 0.22f)
		},
		{
			20,
			new Vector3(1f, 1f, 0.6f)
		},
		{
			21,
			new Vector3(0.7f, 0.7f, 0.7f)
		},
		{
			31,
			new Vector3(0.97f, 0.97f, 0.97f)
		},
		{
			40,
			new Vector3(0.55f, 0.55f, 0.55f)
		},
		{
			53,
			new Vector3(0.22f, 0.22f, 0.22f)
		},
		{
			56,
			new Vector3(0.22f, 0.22f, 0.22f)
		}
	};

	private static Regex machineNameRegex;

	private static Vector3 ONE = Vector3.one;

	private static float x;

	private static float y;

	private static float z;

	private static float w;

	public static event OnLoadHandler OnLoad;

	public static MachineInfo Load(string machineName)
	{
		string path = StaticSettings.SanatizeFileName(machineName) + ".bsg";
		return LoadFromFullPath(Path.Combine(StaticSettings.MachinePath, path), string.Empty);
	}

	public static bool ExternalDLCCheck(MachineInfo info)
	{
		List<DlcManager.DlcStatus> dlcIssues;
		return DlcManager.Instance.GetInfoDlcStatus(info, out dlcIssues);
	}

	public static MachineInfo LoadFromXmlDocument(XDocument xml, bool dummyLoad, string auth)
	{
		try
		{
			MachineInfo machineInfo = new MachineInfo();
			double num = 1.0;
			XElement xElement = xml.Element("Machine");
			XElement xElement2 = xElement.Element("Global");
			bool flag = !string.IsNullOrEmpty(auth);
			machineInfo.Author = auth;
			machineInfo.Name = (string)xElement.Attribute("name");
			machineInfo.Type = ((!flag) ? MachineInfo.MachineType.Local : MachineInfo.MachineType.Workshop);
			if (xElement.Attributes("bsgVersion").Any())
			{
				num = (double)xElement.Attribute("bsgVersion");
			}
			if (xElement.Attributes("Auth").Any())
			{
				string text = (string)xElement.Attribute("Auth");
				if (SteamManager.Initialized)
				{
					string text2 = SteamUser.GetSteamID().m_SteamID.ToString();
					if (!string.IsNullOrEmpty(text) && text != text2)
					{
						machineInfo.Author = text;
						machineInfo.Type = MachineInfo.MachineType.Workshop;
					}
				}
			}
			machineInfo.Position = ReadVector3(xElement2.Element("Position"));
			machineInfo.Rotation = ReadQuaternion(xElement2.Element("Rotation"));
			XDataHolder xDataHolder = new XDataHolder();
			if (xElement.Elements("Data").Any())
			{
				foreach (XElement item in xElement.Element("Data").Elements())
				{
					IEnumerable<XElement> enumerable = item.Elements();
					int num2 = enumerable.Count();
					XMLTypes.XAttribute[] array;
					if (num2 > 0)
					{
						int num3 = 0;
						array = new XMLTypes.XAttribute[num2];
						foreach (XElement item2 in enumerable)
						{
							array[num3] = new XMLTypes.XAttribute(item2.Name.LocalName.ToString(StaticSettings.Culture), item2.Value.ToString(StaticSettings.Culture));
							num3++;
						}
					}
					else
					{
						array = XMLTypes.XAttribute.Single(item.Value);
					}
					xDataHolder.Write(XDataUtil.CreateXData(item.Name.LocalName, (string)item.Attribute("key"), array));
				}
			}
			machineInfo.MachineData = xDataHolder;
			machineInfo.SkinPacks = new List<BlockSkinLoader.SkinPack>();
			List<BlockInfo> list = new List<BlockInfo>();
			HashSet<Guid> hashSet = new HashSet<Guid>();
			bool flag2 = false;
			int[] ids = new int[5] { 18, 26, 42, 52, 55 };
			foreach (XElement item3 in xElement.Element("Blocks").Elements())
			{
				XDataHolder xDataHolder2 = new XDataHolder();
				xDataHolder2.WasLoadedFromFile = true;
				xDataHolder2.WasCreated = true;
				int _id = (int)item3.Attribute("id");
				foreach (XElement item4 in item3.Element("Data").Elements())
				{
					IEnumerable<XElement> enumerable = item4.Elements();
					int num4 = enumerable.Count();
					XMLTypes.XAttribute[] array;
					if (num4 > 0)
					{
						int num5 = 0;
						array = new XMLTypes.XAttribute[num4];
						foreach (XElement item5 in enumerable)
						{
							array[num5] = new XMLTypes.XAttribute(item5.Name.LocalName, item5.Value);
							num5++;
						}
					}
					else
					{
						string text3 = item4.Value;
						if (num < 1.2 && !EqualsAny(_id, ids) && (string)item4.Attribute("key") == "flipped")
						{
							text3 = ((!(text3 == "True")) ? "True" : "False");
						}
						array = XMLTypes.XAttribute.Single(text3);
					}
					XData data = XDataUtil.CreateXData(item4.Name.LocalName, (string)item4.Attribute("key"), array);
					xDataHolder2.Write(data);
				}
				XElement xElement3 = item3.Element("Transform");
				Guid guid = new Guid((string)item3.Attribute("guid"));
				if (hashSet.Contains(guid))
				{
					guid = Guid.NewGuid();
				}
				hashSet.Add(guid);
				if (!HandleMod(item3, ref _id))
				{
					continue;
				}
				BlockInfo blockInfo = new BlockInfo();
				blockInfo.Guid = guid;
				blockInfo.ID = (BlockType)_id;
				blockInfo.Position = ReadVector3(xElement3.Element("Position"));
				blockInfo.Rotation = ReadQuaternion(xElement3.Element("Rotation"));
				blockInfo.Scale = ReadVector3(xElement3.Element("Scale"));
				blockInfo.BlockData = xDataHolder2;
				BlockInfo blockInfo2 = blockInfo;
				if (num == 1.0)
				{
					int iD = (int)blockInfo2.ID;
					Vector3 value = ONE;
					if (version1scaling.TryGetValue(iD, out value))
					{
						blockInfo2.Scale = new Vector3(blockInfo2.Scale.x / value.x, blockInfo2.Scale.y / value.y, blockInfo2.Scale.z / value.z);
					}
				}
				HandleNegativeScale(blockInfo2);
				flag2 = false;
				if (item3.Elements("Settings").Any())
				{
					XElement xElement4 = item3.Element("Settings");
					if (xElement4.Elements("Skin").Any())
					{
						XElement xElement5 = xElement4.Element("Skin");
						blockInfo2.Skin = BlockSkinLoader.SkinPack.Skin.Holder((string)xElement5.Attribute("name"), (string)xElement5.Attribute("id"), string.Empty);
						flag2 = true;
						if (SteamManager.Initialized && !PackIsInList(blockInfo2.Skin.pack, machineInfo.SkinPacks))
						{
							machineInfo.SkinPacks.Add(blockInfo2.Skin.pack);
						}
					}
				}
				if (!flag2 && !dummyLoad)
				{
					int iD2 = (int)blockInfo2.ID;
					BlockPrefab value2;
					if (PrefabMaster.BlockPrefabs.TryGetValue(iD2, out value2))
					{
						blockInfo2.Skin = value2.DefaultSkin;
					}
					else
					{
						blockInfo2.Skin = null;
					}
				}
				list.Add(blockInfo2);
			}
			machineInfo.Blocks = list;
			OnLoadHandler onLoad = XmlLoader.OnLoad;
			if (onLoad != null)
			{
				onLoad(machineInfo);
			}
			return machineInfo;
		}
		catch (XmlException exception)
		{
			Debug.LogException(exception);
			throw new MachineLoadException("Machine save file's layout is invalid. Likely corrupted or manipulated.");
		}
		catch (NullReferenceException exception2)
		{
			Debug.LogException(exception2);
			throw new MachineLoadException("Machine save file does not contain all required elements. Likely corrupted or manipulated.");
		}
		catch (ArgumentNullException exception3)
		{
			Debug.LogException(exception3);
			throw new MachineLoadException("Machine save file does not contain all required attributes. Likely corrupted or manipulated.");
		}
	}

	private static bool HandleMod(XElement eBlock, ref int _id)
	{
		if (eBlock.Attribute("modId") != null)
		{
			string text = (string)eBlock.Attribute("modId");
			int num = (int)eBlock.Attribute("localId");
			ModContainer modById = ModIds.GetModById(text);
			int num2 = ((modById != null) ? ModIds.GetEffectiveBlockId(modById.Info.Id, num) : 0);
			if (num2 == 0 || ModIds.GetBlockByEffectiveId(num2).HideInUI)
			{
				if (eBlock.Attribute("fallback") == null)
				{
					if (num2 == 0)
					{
						Debug.LogWarning("Block (" + text + ", " + num + ") is not loaded. Ignoring it.");
						return false;
					}
					return false;
				}
				int num3 = (int)eBlock.Attribute("fallback");
				_id = num3;
			}
			else
			{
				_id = num2;
			}
		}
		if (!PrefabMaster.BlockPrefabs.ContainsKey(_id))
		{
			int replacementBlockId = ModIds.GetReplacementBlockId(_id);
			if (replacementBlockId != 0)
			{
				_id = replacementBlockId;
			}
		}
		return true;
	}

	private static void HandleNegativeScale(BlockInfo blockInfo)
	{
		if (blockInfo.Scale.x < 0f)
		{
			blockInfo.Scale = new Vector3(blockInfo.Scale.x * -1f, blockInfo.Scale.y, blockInfo.Scale.z);
			blockInfo.Rotation = Quaternion.Euler(blockInfo.Rotation.eulerAngles.x, blockInfo.Rotation.eulerAngles.y, blockInfo.Rotation.eulerAngles.z - 180f);
		}
		if (blockInfo.Scale.y < 0f)
		{
			blockInfo.Scale = new Vector3(blockInfo.Scale.x, blockInfo.Scale.y * -1f, blockInfo.Scale.z);
			blockInfo.Rotation = Quaternion.Euler(blockInfo.Rotation.eulerAngles.x - 180f, blockInfo.Rotation.eulerAngles.y, blockInfo.Rotation.eulerAngles.z);
		}
		if (blockInfo.Scale.z < 0f)
		{
			blockInfo.Scale = new Vector3(blockInfo.Scale.x, blockInfo.Scale.y, blockInfo.Scale.z * -1f);
			blockInfo.Rotation = Quaternion.Euler(blockInfo.Rotation.eulerAngles.x, blockInfo.Rotation.eulerAngles.y - 180f, blockInfo.Rotation.eulerAngles.z);
		}
	}

	public static MachineInfo LoadFromString(string machineXMLString, bool dummyLoad)
	{
		try
		{
			StringReader reader = new StringReader(machineXMLString);
			XDocument xml = XDocument.Load(reader);
			return LoadFromXmlDocument(xml, dummyLoad, string.Empty);
		}
		catch (XmlException exception)
		{
			Debug.LogException(exception);
			throw new MachineLoadException("Machine save file's layout is invalid. Likely corrupted or manipulated.");
		}
		catch (NullReferenceException exception2)
		{
			Debug.LogException(exception2);
			throw new MachineLoadException("Machine save file does not contain all required elements. Likely corrupted or manipulated.");
		}
		catch (ArgumentNullException exception3)
		{
			Debug.LogException(exception3);
			throw new MachineLoadException("Machine save file does not contain all required attributes. Likely corrupted or manipulated.");
		}
		catch (Exception exception4)
		{
			Debug.LogException(exception4);
			throw new MachineLoadException("Uncaught exception while loading the machine file.");
		}
	}

	public static MachineInfo LoadFromFullPath(string path, string auth = "")
	{
		return LoadFromFullPath(path, false, auth);
	}

	public static MachineInfo LoadFromFullPath(string path, bool dummyLoad, string auth)
	{
		try
		{
			XDocument xml = null;
			byte[] buffer = File.ReadAllBytes(path);
			using (MemoryStream stream = new MemoryStream(buffer))
			{
				XmlReader reader = XmlReader.Create(stream);
				xml = XDocument.Load(reader, LoadOptions.None);
			}
			return LoadFromXmlDocument(xml, dummyLoad, auth);
		}
		catch (XmlException exception)
		{
			Debug.LogException(exception);
			throw new MachineLoadException("Machine save file's layout is invalid. Likely corrupted or manipulated.");
		}
		catch (NullReferenceException exception2)
		{
			Debug.LogException(exception2);
			throw new MachineLoadException("Machine save file does not contain all required elements. Likely corrupted or manipulated.");
		}
		catch (ArgumentNullException exception3)
		{
			Debug.LogException(exception3);
			throw new MachineLoadException("Machine save file does not contain all required attributes. Likely corrupted or manipulated.");
		}
		catch (Exception exception4)
		{
			Debug.LogException(exception4);
			throw new MachineLoadException("Uncaught exception while loading the machine file.");
		}
	}

	private static bool EqualsAny(int id, int[] ids)
	{
		for (int i = 0; i < ids.Length; i++)
		{
			if (id == ids[i])
			{
				return true;
			}
		}
		return false;
	}

	public static bool PackIsInList(BlockSkinLoader.SkinPack pack, List<BlockSkinLoader.SkinPack> packs)
	{
		bool result = false;
		if (pack.id != BlockSkinLoader.defaultString)
		{
			foreach (BlockSkinLoader.SkinPack item in new List<BlockSkinLoader.SkinPack>(packs))
			{
				if (!string.IsNullOrEmpty(pack.id) && !char.IsLetter(pack.id[0]))
				{
					if (pack.id == item.id)
					{
						result = true;
						break;
					}
				}
				else if (pack.name == item.name && pack.id == item.id)
				{
					result = true;
					break;
				}
			}
		}
		else
		{
			result = true;
		}
		return result;
	}

	public static string ReadMachineName(string name)
	{
		return ReadMachineName(name, StaticSettings.MachinePath);
	}

	public static string ReadMachineName(string name, string path)
	{
		string path2 = name + ((!name.EndsWith(".bsg")) ? ".bsg" : string.Empty);
		string path3 = Path.Combine(path, path2);
		if (!File.Exists(path3))
		{
			return null;
		}
		if (machineNameRegex == null)
		{
			machineNameRegex = new Regex("name[\\s]*=[\\s]*\"([^\"]+)\"", RegexOptions.Compiled);
		}
		string result = null;
		using (StreamReader streamReader = new StreamReader(path3))
		{
			bool flag = false;
			string text;
			while ((text = streamReader.ReadLine()) != null && !flag)
			{
				if (text.Contains("<Machine"))
				{
					Match match = machineNameRegex.Match(text);
					result = match.Groups[1].Value;
					flag = true;
				}
			}
		}
		return result;
	}

	private static Vector3 ReadVector3(XElement element)
	{
		x = 0f;
		y = 0f;
		z = 0f;
		x = FastParseFloatOnlyNumbers(element.Attribute("x").Value);
		y = FastParseFloatOnlyNumbers(element.Attribute("y").Value);
		z = FastParseFloatOnlyNumbers(element.Attribute("z").Value);
		return new Vector3(x, y, z);
	}

	private static Quaternion ReadQuaternion(XElement element)
	{
		x = 0f;
		y = 0f;
		z = 0f;
		w = 0f;
		x = FastParseFloatOnlyNumbers(element.Attribute("x").Value);
		y = FastParseFloatOnlyNumbers(element.Attribute("y").Value);
		z = FastParseFloatOnlyNumbers(element.Attribute("z").Value);
		w = FastParseFloatOnlyNumbers(element.Attribute("w").Value);
		return new Quaternion(x, y, z, w);
	}

	public static float FastParseFloat(string str)
	{
		if (str.Equals("NaN"))
		{
			return float.NaN;
		}
		if (str.Equals("Infinity"))
		{
			return float.PositiveInfinity;
		}
		if (str.Equals("-Infinity"))
		{
			return float.NegativeInfinity;
		}
		return FastParseFloatOnlyNumbers(str);
	}

	public static float FastParseFloatOnlyNumbers(string str)
	{
		if (str.Length == 0)
		{
			return 0f;
		}
		double num = 0.0;
		double num2 = 1.0;
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		int num3 = 0;
		int num4 = 0;
		bool flag4 = false;
		if (str[0] == '-')
		{
			flag4 = true;
			num4 = 1;
		}
		for (int i = num4; i < str.Length; i++)
		{
			char c = str[i];
			switch (c)
			{
			case '.':
				flag = true;
				continue;
			case 'E':
			case 'e':
				flag2 = true;
				if (i + 1 < str.Length && str[i + 1] == '-')
				{
					flag3 = true;
					i++;
				}
				continue;
			}
			int num5 = c - 48;
			if (num5 >= 0 && num5 <= 9)
			{
				if (flag2)
				{
					num3 = num3 * 10 + num5;
				}
				else if (flag)
				{
					num2 *= 0.1;
					num += (double)num5 * num2;
				}
				else
				{
					num = num * 10.0 + (double)num5;
				}
			}
		}
		if (flag2)
		{
			double num6 = Math.Pow(10.0, (!flag3) ? num3 : (-num3));
			num *= num6;
		}
		if (flag4)
		{
			num = 0.0 - num;
		}
		return (float)num;
	}
}
