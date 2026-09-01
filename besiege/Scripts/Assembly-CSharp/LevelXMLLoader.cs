using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using BesiegeDlc;
using InternalModding;
using InternalModding.Loading;
using UnityEngine;

[AddComponentMenu("XML/Level XML Loader")]
public static class LevelXMLLoader
{
	private static LevelEditor levelEditor;

	private static readonly List<DlcManager.DlcStatus> dlcIssues = new List<DlcManager.DlcStatus>();

	private static float version = 0f;

	public static event Action<LevelEditor> OnLoaded;

	private static XmlReaderSettings GetSettings()
	{
		XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
		xmlReaderSettings.ConformanceLevel = ConformanceLevel.Auto;
		xmlReaderSettings.IgnoreWhitespace = true;
		xmlReaderSettings.IgnoreComments = true;
		return xmlReaderSettings;
	}

	internal static bool GetDlcTypesFromFile(string filePath, out List<DlcManager.DlcType> dlcTypes)
	{
		dlcTypes = new List<DlcManager.DlcType>();
		int num = Enum.GetNames(typeof(DlcManager.DlcType)).Length;
		using (XmlReader xmlReader = XmlReader.Create(filePath, GetSettings()))
		{
			while (xmlReader.Read() && dlcTypes.Count < num)
			{
				if (xmlReader.NodeType != XmlNodeType.Element)
				{
					continue;
				}
				switch (xmlReader.Name)
				{
				case "LevelSettings":
				{
					bool flag = true;
					xmlReader.MoveToFirstAttribute();
					while (flag)
					{
						switch (xmlReader.Name)
						{
						case "Environment":
						{
							LevelSettings.LevelEnvironment env;
							DlcManager.DlcType dlcType;
							if (!LevelSettings.ParseEnvironment(xmlReader.Value, out env) || !DlcManager.Instance.GetDlcType(env, out dlcType))
							{
								break;
							}
							List<DlcManager.DlcType> list = DlcManager.Convert(DlcManager.Instance.GetDlcTypesFromMask((uint)dlcType));
							for (int j = 0; j < list.Count; j++)
							{
								if (!dlcTypes.Contains(list[j]))
								{
									dlcTypes.Add(list[j]);
								}
							}
							break;
						}
						}
						flag = xmlReader.MoveToNextAttribute();
					}
					xmlReader.MoveToElement();
					while (xmlReader.Read())
					{
						string name = xmlReader.Name;
						if (IsEndOfElement(xmlReader, "LevelSettings"))
						{
							break;
						}
						if (!name.Equals("AllowedMachines") || !xmlReader.ReadToDescendant("LevelMachine"))
						{
							continue;
						}
						do
						{
							flag = true;
							xmlReader.MoveToFirstAttribute();
							while (flag)
							{
								switch (xmlReader.Name)
								{
								case "info":
								{
									MachineInfo info = new LevelSettings.LevelMachine(xmlReader.Value).GetInfo();
									List<DlcManager.DlcType> dlcTypes2 = new List<DlcManager.DlcType>();
									DlcManager.Instance.GetMachineInfoDlc(info, out dlcTypes2);
									for (int k = 0; k < dlcTypes2.Count; k++)
									{
										if (!dlcTypes.Contains(dlcTypes2[k]))
										{
											dlcTypes.Add(dlcTypes2[k]);
										}
									}
									break;
								}
								}
								flag = xmlReader.MoveToNextAttribute();
							}
							xmlReader.MoveToElement();
						}
						while (xmlReader.ReadToNextSibling("LevelMachine"));
					}
					break;
				}
				case "Objects":
					if (!xmlReader.ReadToDescendant("Object"))
					{
						break;
					}
					do
					{
						xmlReader.MoveToFirstAttribute();
						int result = -1;
						bool flag = true;
						while (flag)
						{
							switch (xmlReader.Name)
							{
							case "Prefab":
								if (!int.TryParse(xmlReader.Value, out result))
								{
									Debug.Log("Couldn't parse prefab ID: " + xmlReader.Value + "!");
								}
								break;
							}
							flag = xmlReader.MoveToNextAttribute();
						}
						List<DlcManager.DlcType> prefabDlcTypes = DlcManager.Instance.GetPrefabDlcTypes(result);
						for (int i = 0; i < prefabDlcTypes.Count; i++)
						{
							if (!dlcTypes.Contains(prefabDlcTypes[i]))
							{
								dlcTypes.Add(prefabDlcTypes[i]);
							}
						}
						while (!IsEndOfObject(xmlReader) && xmlReader.Read())
						{
						}
					}
					while (xmlReader.ReadToNextSibling("Object"));
					break;
				}
			}
		}
		return dlcTypes.Count > 0;
	}

	public static bool ExternalDLCCheck(string filePath)
	{
		List<DlcManager.DlcType> dlcTypes = new List<DlcManager.DlcType>();
		if (GetDlcTypesFromFile(filePath, out dlcTypes))
		{
			foreach (DlcManager.DlcType item in dlcTypes)
			{
				if (DlcManager.Instance.GetDlcStatus(item) != DlcManager.DlcStatusType.Allowed)
				{
					return false;
				}
			}
		}
		return true;
	}

	public static void ReadLevelFromFile(string filePath, bool additive = false)
	{
		if (additive)
		{
			if (dlcIssues.Count > 0)
			{
				return;
			}
		}
		else
		{
			levelEditor.ClearLevel();
			dlcIssues.Clear();
		}
		using (XmlReader reader = XmlReader.Create(filePath, GetSettings()))
		{
			ReadData(reader, !additive);
		}
		if (dlcIssues.Count > 0)
		{
			DlcMismatchUI.Show(dlcIssues, 4446);
		}
	}

	public static bool ReadLevelInfoFromFile(string filePath, out XDataHolder customData, out List<EntityController.PlaceEntry> entries)
	{
		entries = null;
		customData = null;
		using (XmlReader xmlReader = XmlReader.Create(filePath, GetSettings()))
		{
			while (xmlReader.Read())
			{
				if (xmlReader.NodeType != XmlNodeType.Element)
				{
					continue;
				}
				switch (xmlReader.Name)
				{
				case "CustomData":
					customData = ReadCustomData(xmlReader);
					break;
				case "Objects":
					if (!ReadObjects(xmlReader, out entries))
					{
						return false;
					}
					if (entries.Count == 0)
					{
						Debug.LogWarning("No objects found in level file: " + filePath);
						return false;
					}
					break;
				}
			}
		}
		return true;
	}

	public static bool ReadObjectFromString(string objData, out LevelEntity entity)
	{
		using (XmlReader xmlReader = XmlReader.Create(new StringReader(objData), GetSettings()))
		{
			xmlReader.MoveToContent();
			if (CreateEntity(xmlReader, out entity))
			{
				return true;
			}
		}
		entity = null;
		return false;
	}

	public static void ReadLevelFromString(string fileData, bool settingsOnly)
	{
		dlcIssues.Clear();
		using (XmlReader reader = XmlReader.Create(new StringReader(fileData), GetSettings()))
		{
			ReadData(reader);
		}
		if (dlcIssues.Count > 0)
		{
			DlcMismatchUI.Show(dlcIssues, (!DlcManager.Instance.HasPurchasedDlc(dlcIssues[0].type)) ? 4460 : 4614);
			if (!settingsOnly)
			{
				levelEditor.ClearLevel();
			}
		}
	}

	public static string GetLevelName(string fileName)
	{
		string path = Path.Combine(StaticSettings.LevelPath, fileName);
		string result = fileName;
		if (File.Exists(path))
		{
			StreamReader streamReader = new StreamReader(path);
			string text = streamReader.ReadLine();
			Regex regex = new Regex("=\"([^\"]+)\">");
			while (text != null)
			{
				Match match = regex.Match(text);
				if (match.Success)
				{
					result = match.Groups[1].Value;
					break;
				}
				text = streamReader.ReadLine();
			}
			streamReader.Close();
		}
		return result;
	}

	private static void ReadData(XmlReader reader, bool readSettings = true)
	{
		levelEditor = LevelEditor.Instance;
		bool flag = false;
		while (reader.Read())
		{
			if (reader.NodeType != XmlNodeType.Element)
			{
				continue;
			}
			switch (reader.Name)
			{
			case "LevelSettings":
				if (readSettings)
				{
					LevelSettings settings;
					if (!ReadLevelSettings(reader, out settings))
					{
						return;
					}
					levelEditor.UpdateLevelSettings(settings);
				}
				break;
			case "CustomData":
				levelEditor.CustomData = ReadCustomData(reader);
				flag = true;
				break;
			case "Objects":
				ReadObjects(reader);
				flag = true;
				break;
			}
		}
		if (flag && LevelXMLLoader.OnLoaded != null)
		{
			LevelXMLLoader.OnLoaded(levelEditor);
		}
	}

	public static bool ReadLevelSettings(XmlReader reader, out LevelSettings settings)
	{
		settings = new LevelSettings();
		reader.MoveToFirstAttribute();
		bool flag = true;
		while (flag)
		{
			switch (reader.Name)
			{
			case "EditorVersion":
				version = GetFloat(reader.Value);
				break;
			case "UseVoting":
				settings.UseVoting = GetBool(reader.Value);
				break;
			case "CurtainMode":
				settings.CurtainMode = GetBool(reader.Value);
				break;
			case "AllowExcessPlayers":
				settings.AllowExcessPlayers = GetBool(reader.Value);
				break;
			case "HidePlayerLabels":
				settings.HidePlayerLabels = GetBool(reader.Value);
				break;
			case "AllowCopyMachine":
				settings.AllowCopyMachine = GetBool(reader.Value);
				break;
			case "Environment":
			{
				LevelSettings.LevelEnvironment env;
				if (LevelSettings.ParseEnvironment(reader.Value, out env) && DlcManager.Instance.CheckEnv(env, dlcIssues))
				{
					settings.Environment = env;
					break;
				}
				return false;
			}
			case "WaterHeight":
				settings.WaterHeight = GetInt(reader.Value);
				break;
			case "EnvType":
				settings.EnvType = GetInt(reader.Value);
				break;
			case "MinPlayers":
				settings.MinPlayers = GetInt(reader.Value);
				break;
			case "MaxPlayers":
				settings.MaxPlayers = GetInt(reader.Value);
				break;
			case "Music":
				settings.MusicID = GetInt(reader.Value);
				break;
			case "MusicVolume":
				settings.MusicVolume = Mathf.Clamp(GetInt(reader.Value), 0, 100);
				break;
			case "allowModMachines":
				settings.AllowModMachines = GetBool(reader.Value);
				break;
			}
			flag = reader.MoveToNextAttribute();
		}
		reader.MoveToElement();
		string[] godPowers = ReferenceMaster.Instance.godPowers;
		while (reader.Read())
		{
			string name = reader.Name;
			if (IsEndOfElement(reader, "LevelSettings"))
			{
				break;
			}
			if (name.Equals("AllowedMachines"))
			{
				ReadAllowedMachines(reader, settings);
				continue;
			}
			if (name.Equals("BlockLimiter"))
			{
				ReadBlockLimits(reader, settings);
				continue;
			}
			foreach (string text in godPowers)
			{
				LevelSettings.GodPowerSetting value;
				if (!reader.Name.Equals(text) || !settings.GodPowerSettings.TryGetValue(text, out value))
				{
					continue;
				}
				reader.MoveToFirstAttribute();
				flag = true;
				while (flag)
				{
					switch (reader.Name)
					{
					case "Enabled":
						value.Enabled = GetBool(reader.Value);
						break;
					case "Locked":
						value.Locked = GetBool(reader.Value);
						break;
					}
					flag = reader.MoveToNextAttribute();
				}
			}
		}
		return true;
	}

	private static void ReadBlockLimits(XmlReader reader, LevelSettings settings)
	{
		if (reader.MoveToFirstAttribute() && reader.Name.Equals("limit"))
		{
			int result;
			if (!int.TryParse(reader.Value, out result))
			{
				result = -1;
			}
			settings.BlockCountLimiter = result;
		}
		reader.MoveToElement();
		if (!reader.ReadToDescendant("BlockLimit"))
		{
			return;
		}
		do
		{
			bool flag = false;
			bool flag2 = false;
			int result2 = 0;
			int result3 = 0;
			bool flag3 = true;
			bool flag4 = false;
			bool flag5 = false;
			Guid g = Guid.Empty;
			int result4 = 0;
			reader.MoveToFirstAttribute();
			while (flag3)
			{
				switch (reader.Name)
				{
				case "id":
					flag = int.TryParse(reader.Value, out result2);
					break;
				case "limit":
					flag2 = int.TryParse(reader.Value, out result3);
					break;
				case "modId":
					flag4 = ModdingUtil.TryParseGuid(reader.Value, out g);
					break;
				case "localId":
					flag5 = int.TryParse(reader.Value, out result4);
					break;
				}
				flag3 = reader.MoveToNextAttribute();
			}
			if (flag4 && flag5)
			{
				result2 = ModIds.GetEffectiveBlockId(g, result4);
				if (result2 == 0)
				{
					flag = false;
				}
			}
			if (flag && flag2)
			{
				settings.SetBlockLimit((BlockType)result2, result3);
			}
			reader.MoveToElement();
		}
		while (reader.ReadToNextSibling("BlockLimit"));
	}

	private static void ReadAllowedMachines(XmlReader reader, LevelSettings settings)
	{
		if (!reader.ReadToDescendant("LevelMachine"))
		{
			return;
		}
		do
		{
			bool flag = false;
			bool flag2 = false;
			string thumb = null;
			string info = null;
			bool flag3 = true;
			reader.MoveToFirstAttribute();
			while (flag3)
			{
				switch (reader.Name)
				{
				case "info":
					flag2 = true;
					info = reader.Value;
					break;
				case "thumb":
					flag = true;
					thumb = reader.Value;
					break;
				}
				flag3 = reader.MoveToNextAttribute();
			}
			if (flag2 && flag)
			{
				MachineInfo info2 = new LevelSettings.LevelMachine(info).GetInfo();
				List<DlcManager.DlcType> dlcTypes = new List<DlcManager.DlcType>();
				DlcManager.Instance.GetMachineInfoDlc(info2, out dlcTypes);
				if (!DlcManager.Instance.TestDlcTypes(dlcTypes, dlcIssues))
				{
					break;
				}
				settings.AllowedMachines.Add(new LevelSettings.LevelMachine(info, thumb));
			}
			reader.MoveToElement();
		}
		while (reader.ReadToNextSibling("LevelMachine"));
	}

	private static XDataHolder ReadCustomData(XmlReader reader)
	{
		XDataHolder xDataHolder = new XDataHolder();
		if (reader.IsEmptyElement)
		{
			return xDataHolder;
		}
		reader.Read();
		while (!IsEndOfElement(reader, "CustomData"))
		{
			reader.MoveToFirstAttribute();
			if (!reader.Name.Equals("key"))
			{
				Debug.Log("Error: Expected key, got " + reader.Name + "!");
			}
			string value = reader.Value;
			reader.MoveToElement();
			xDataHolder.Write(XDataUtil.CreateXData(reader, value));
			reader.Read();
		}
		return xDataHolder;
	}

	private static bool ReadObjects(XmlReader reader)
	{
		if (!reader.ReadToDescendant("Object"))
		{
			return true;
		}
		do
		{
			LevelEntity entity;
			if (CreateEntity(reader, out entity))
			{
				levelEditor.OnEntityUpdate(entity, LevelEditor.EntityUpdateState.Place);
				continue;
			}
			while (!IsEndOfObject(reader))
			{
				if (!reader.Read())
				{
					return false;
				}
			}
		}
		while (reader.ReadToNextSibling("Object"));
		return true;
	}

	private static bool ReadObjects(XmlReader reader, out List<EntityController.PlaceEntry> entries)
	{
		entries = new List<EntityController.PlaceEntry>();
		if (!reader.ReadToDescendant("Object"))
		{
			return true;
		}
		do
		{
			EntityController.PlaceEntry placeEntry = ReadEntity(reader);
			if (placeEntry != null)
			{
				entries.Add(placeEntry);
				continue;
			}
			while (!IsEndOfObject(reader))
			{
				if (!reader.Read())
				{
					return false;
				}
			}
		}
		while (reader.ReadToNextSibling("Object"));
		return true;
	}

	private static bool IsEndOfObject(XmlReader reader)
	{
		return IsEndOfElement(reader, "Object");
	}

	private static bool IsEndOfData(XmlReader reader)
	{
		return IsEndOfElement(reader, "Data");
	}

	private static bool IsEndOfElement(XmlReader reader, string elementName)
	{
		return reader.NodeType == XmlNodeType.EndElement && reader.Name.Equals(elementName);
	}

	private static EntityController.PlaceEntry ReadEntity(XmlReader reader)
	{
		if (reader.NodeType != XmlNodeType.Element || !reader.Name.Equals("Object"))
		{
			Debug.LogError("Reader in wrong position, couldn't read entity! Current pos: " + reader.Name + " > " + reader.NodeType);
			return null;
		}
		reader.MoveToFirstAttribute();
		bool flag = true;
		long result = 0L;
		int result2 = -1;
		Guid g = Guid.Empty;
		int result3 = -1;
		bool flag2 = false;
		int result4 = -1;
		while (flag)
		{
			switch (reader.Name)
			{
			case "ID":
				if (!long.TryParse(reader.Value, out result))
				{
					Debug.Log("Couldn't parse ID: " + reader.Value + "!");
					return null;
				}
				break;
			case "Prefab":
				if (!int.TryParse(reader.Value, out result2))
				{
					Debug.Log("Couldn't parse prefab ID: " + reader.Value + "!");
				}
				break;
			case "ModID":
				if (!ModdingUtil.TryParseGuid(reader.Value, out g))
				{
					Debug.Log("Couldn't parse mod ID: " + reader.Value + "!");
				}
				flag2 = true;
				break;
			case "LocalID":
				if (!int.TryParse(reader.Value, out result3))
				{
					Debug.Log("Couldn't parse local ID: " + reader.Value + "!");
				}
				break;
			case "Fallback":
				if (!int.TryParse(reader.Value, out result4))
				{
					Debug.Log("Couldn't parse fallback: " + reader.Value + "!");
				}
				break;
			}
			flag = reader.MoveToNextAttribute();
		}
		if (flag2)
		{
			int effectiveEntityId = ModIds.GetEffectiveEntityId(g, result3);
			result2 = effectiveEntityId;
			if (result2 == 0 || ModIds.GetEntityByEffectiveId(effectiveEntityId).HideInUI)
			{
				if (result4 == -1)
				{
					if (result2 != 0)
					{
						Debug.LogWarning(string.Concat("Entity (", g, ", ", result3, ") is not loaded. Ignoring it."));
						return null;
					}
					return null;
				}
				result2 = result4;
			}
		}
		if (!DlcManager.Instance.GetPrefabDlcStatus(result2, dlcIssues))
		{
			return null;
		}
		reader.MoveToElement();
		Vector3 vec = Vector3.zero;
		Quaternion quat = Quaternion.identity;
		Vector3 vec2 = Vector3.one;
		XDataHolder xDataHolder = new XDataHolder();
		bool flag3 = false;
		while (!flag3 && reader.Read())
		{
			switch (reader.Name)
			{
			case "Position":
				ReadVector3(reader, ref vec);
				break;
			case "Rotation":
				ReadQuaternion(reader, ref quat);
				break;
			case "Scale":
				ReadVector3(reader, ref vec2);
				break;
			case "Data":
				ReadEntityData(reader, xDataHolder);
				flag3 = true;
				break;
			}
		}
		if (!IsEndOfObject(reader))
		{
			Debug.LogError("Error reading object " + result + "!");
			return null;
		}
		return new EntityController.PlaceEntry(result2, vec, quat, vec2, xDataHolder, result);
	}

	private static bool CreateEntity(XmlReader reader, out LevelEntity entity)
	{
		EntityController.PlaceEntry placeEntry = ReadEntity(reader);
		if (placeEntry == null)
		{
			entity = null;
			return false;
		}
		LevelPrefab prefab;
		if (!levelEditor.GetPrefab(placeEntry.prefabID, out prefab))
		{
			Debug.Log("Couldn't find prefab: " + reader.Value + "!");
			entity = null;
			return false;
		}
		entity = levelEditor.InstantiatePrefab(prefab, placeEntry.pos, placeEntry.rot, placeEntry.scale);
		entity.identifier = placeEntry.previousID;
		entity.Init();
		entity.LoadEntityData(placeEntry.data);
		entity.OnXMLLoad();
		return true;
	}

	private static void ReadEntityData(XmlReader reader, XDataHolder data)
	{
		reader.Read();
		bool flag = true;
		while (flag && !IsEndOfObject(reader))
		{
			if (IsEndOfData(reader))
			{
				flag = false;
			}
			else
			{
				reader.MoveToFirstAttribute();
				if (!reader.Name.Equals("key"))
				{
					Debug.Log("Error: Expected key, got " + reader.Name + "!");
				}
				string value = reader.Value;
				reader.MoveToElement();
				bool flag2 = false;
				if (version <= 0.8f)
				{
					switch (value)
					{
					case "bmt-lel-mass":
						flag2 = true;
						if (reader.Read() && reader.NodeType == XmlNodeType.Text)
						{
							string value2 = reader.Value;
							reader.Read();
							float result;
							if (float.TryParse(value2, out result))
							{
								data.Write(new XSingle(value, result * result));
							}
						}
						break;
					}
				}
				if (!flag2)
				{
					data.Write(XDataUtil.CreateXData(reader, value));
				}
			}
			reader.Read();
		}
	}

	private static void ReadVector3(XmlReader reader, ref Vector3 vec)
	{
		if (reader.AttributeCount != 3)
		{
			Debug.LogError("Vector3 doesn't have 3 attributes!");
			return;
		}
		reader.MoveToFirstAttribute();
		int num = 0;
		while (num < 3)
		{
			float result;
			if (!float.TryParse(reader.Value, out result))
			{
				result = 0f;
			}
			vec[num++] = result;
			reader.MoveToNextAttribute();
		}
		reader.MoveToElement();
	}

	private static void ReadQuaternion(XmlReader reader, ref Quaternion quat)
	{
		if (reader.AttributeCount != 4)
		{
			Debug.LogError("Quaternion doesn't have 4 attributes!");
			return;
		}
		reader.MoveToFirstAttribute();
		int num = 0;
		while (num < 4)
		{
			float result;
			if (!float.TryParse(reader.Value, out result))
			{
				result = 0f;
			}
			quat[num++] = result;
			reader.MoveToNextAttribute();
		}
		reader.MoveToElement();
	}

	private static int GetInt(string str)
	{
		int result;
		if (!int.TryParse(str, out result))
		{
			return 0;
		}
		return result;
	}

	private static float GetFloat(string str)
	{
		float result;
		if (!float.TryParse(str, out result))
		{
			return 0f;
		}
		return result;
	}

	private static bool GetBool(string str)
	{
		return str.Equals("True");
	}
}
