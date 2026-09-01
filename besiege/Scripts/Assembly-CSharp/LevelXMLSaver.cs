using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using InternalModding;
using InternalModding.Blocks;
using InternalModding.LevelEntities;
using InternalModding.Loading;
using UnityEngine;
using XMLTypes;

[AddComponentMenu("XML/Level XML Saver")]
internal static class LevelXMLSaver
{
	private static string[] vqNames = new string[4] { "x", "y", "z", "w" };

	public static event Action<LevelEditor> OnSave;

	public static void SetXMLWriterSettings(XmlWriterSettings xmlWriterSettings)
	{
		xmlWriterSettings.Indent = true;
		xmlWriterSettings.IndentChars = "\t";
		xmlWriterSettings.NewLineChars = "\r\n";
		xmlWriterSettings.Encoding = Encoding.UTF8;
		xmlWriterSettings.OmitXmlDeclaration = true;
	}

	public static void Init(Transform floorTransform, WinCondition win)
	{
	}

	public static void Create(string path, string levelName, XDataHolder customData = null, List<LevelEntity> objects = null)
	{
		StatMaster.SavingXML = true;
		XmlWriter xmlWriter = CreateWriter(path);
		WriteLevel(xmlWriter, levelName, customData, objects);
		xmlWriter.Close();
		StatMaster.SavingXML = false;
	}

	public static void WriteLevel(XmlWriter xmlWriter, string levelName, XDataHolder customData = null, List<LevelEntity> objects = null)
	{
		LevelEditor instance = LevelEditor.Instance;
		instance.Settings.Name = levelName;
		if (customData == null)
		{
			customData = instance.CustomData;
		}
		if (objects == null)
		{
			objects = instance.Entities;
		}
		if (LevelXMLSaver.OnSave != null)
		{
			LevelXMLSaver.OnSave(instance);
		}
		xmlWriter.WriteStartElement("Level");
		WriteLevelSettings(xmlWriter, instance.Settings);
		WriteCustomData(xmlWriter, customData);
		WriteEntities(xmlWriter, objects);
		xmlWriter.WriteEndElement();
	}

	public static void WriteLevelSettings(XmlWriter xmlWriter, LevelSettings settings)
	{
		xmlWriter.WriteStartElement("LevelSettings");
		xmlWriter.WriteAttributeString("EditorVersion", LevelEditor.Version.ToString());
		xmlWriter.WriteAttributeString("UseVoting", GetBoolString(settings.UseVoting));
		xmlWriter.WriteAttributeString("CurtainMode", GetBoolString(settings.CurtainMode));
		xmlWriter.WriteAttributeString("AllowExcessPlayers", GetBoolString(settings.AllowExcessPlayers));
		xmlWriter.WriteAttributeString("HidePlayerLabels", GetBoolString(settings.HidePlayerLabels));
		xmlWriter.WriteAttributeString("AllowCopyMachine", GetBoolString(settings.AllowCopyMachine));
		xmlWriter.WriteAttributeString("allowModMachines", GetBoolString(settings.allowModMachines));
		xmlWriter.WriteAttributeString("Environment", settings.Environment.ToString());
		if (settings.WaterHeight != 0)
		{
			xmlWriter.WriteAttributeString("WaterHeight", settings.WaterHeight.ToString());
		}
		if (settings.EnvType != 0)
		{
			xmlWriter.WriteAttributeString("EnvType", settings.EnvType.ToString());
		}
		if (settings.MinPlayers != -1)
		{
			xmlWriter.WriteAttributeString("MinPlayers", settings.MinPlayers.ToString());
		}
		if (settings.MaxPlayers != -1)
		{
			xmlWriter.WriteAttributeString("MaxPlayers", settings.MaxPlayers.ToString());
		}
		if (settings.MusicID != 0)
		{
			xmlWriter.WriteAttributeString("Music", settings.MusicID.ToString());
		}
		if (settings.MusicVolume != 100)
		{
			xmlWriter.WriteAttributeString("MusicVolume", settings.MusicVolume.ToString());
		}
		for (int i = 0; i < ReferenceMaster.Instance.godPowers.Length; i++)
		{
			string text = ReferenceMaster.Instance.godPowers[i];
			LevelSettings.GodPowerSetting value;
			if (settings.GodPowerSettings.TryGetValue(text, out value))
			{
				xmlWriter.WriteStartElement(text);
				xmlWriter.WriteAttributeString("Enabled", GetBoolString(value.Enabled));
				xmlWriter.WriteAttributeString("Locked", GetBoolString(value.Locked));
				xmlWriter.WriteEndElement();
			}
		}
		bool flag = settings.BlockCountLimiter != -1;
		if (settings.BlockTypeLimiter.Count > 0 || flag)
		{
			xmlWriter.WriteStartElement("BlockLimiter");
			if (flag)
			{
				xmlWriter.WriteAttributeString("limit", settings.BlockCountLimiter.ToString());
			}
			foreach (int key in settings.BlockTypeLimiter.Keys)
			{
				xmlWriter.WriteStartElement("BlockLimit");
				xmlWriter.WriteAttributeString("id", key.ToString());
				if (key >= SingleInstanceFindOnly<ModManager>.Instance.BlockIdStart)
				{
					ModdedBlock blockByEffectiveId = ModIds.GetBlockByEffectiveId(key);
					xmlWriter.WriteAttributeString("modId", blockByEffectiveId.Info.Mod.Info.Id.ToString());
					xmlWriter.WriteAttributeString("localId", blockByEffectiveId.LocalId.ToString());
				}
				xmlWriter.WriteAttributeString("limit", settings.BlockTypeLimiter[key].ToString());
				xmlWriter.WriteEndElement();
			}
			xmlWriter.WriteEndElement();
		}
		if (settings.AllowedMachines.Count > 0)
		{
			xmlWriter.WriteStartElement("AllowedMachines");
			foreach (LevelSettings.LevelMachine allowedMachine in settings.AllowedMachines)
			{
				xmlWriter.WriteStartElement("LevelMachine");
				string infoString = allowedMachine.GetInfoString();
				xmlWriter.WriteAttributeString("info", infoString);
				string thumbString = allowedMachine.thumbString;
				xmlWriter.WriteAttributeString("thumb", thumbString);
				xmlWriter.WriteEndElement();
			}
			xmlWriter.WriteEndElement();
		}
		xmlWriter.WriteEndElement();
	}

	public static void WriteCustomData(XmlWriter xmlWriter, XDataHolder customData)
	{
		xmlWriter.WriteStartElement("CustomData");
		foreach (XData item in customData.ReadAll())
		{
			xmlWriter.WriteStartElement(item.Type);
			xmlWriter.WriteAttributeString("key", item.Key.ToString(StaticSettings.Culture));
			XAttribute[] array = item.Serialize();
			XAttribute[] array2 = array;
			foreach (XAttribute xAttribute in array2)
			{
				if (xAttribute.Name != null)
				{
					xmlWriter.WriteElementString(xAttribute.Name.ToString(StaticSettings.Culture), xAttribute.Value.ToString(StaticSettings.Culture));
				}
				else
				{
					xmlWriter.WriteString(xAttribute.Value.ToString(StaticSettings.Culture));
				}
			}
			xmlWriter.WriteEndElement();
		}
		xmlWriter.WriteEndElement();
	}

	public static bool WriteEntity(XmlWriter xmlWriter, LevelEntity entityTag)
	{
		xmlWriter.WriteStartElement("Object");
		xmlWriter.WriteAttributeString("ID", entityTag.identifier.ToString());
		xmlWriter.WriteAttributeString("Prefab", entityTag.behaviour.prefab.ID.ToString());
		if (SingleInstanceFindOnly<EntityLoader>.Instance.IsModEntity(entityTag.behaviour.prefab.ID))
		{
			ModdedEntity entityByEffectiveId = ModIds.GetEntityByEffectiveId(entityTag.behaviour.prefab.ID);
			xmlWriter.WriteAttributeString("ModID", entityByEffectiveId.Info.Mod.Info.Id.ToString());
			xmlWriter.WriteAttributeString("LocalID", entityByEffectiveId.LocalId.ToString(StaticSettings.Culture));
			if (entityByEffectiveId.Fallback != null)
			{
				xmlWriter.WriteAttributeString("Fallback", entityByEffectiveId.Fallback.Get().ToString());
			}
		}
		WriteVector3(xmlWriter, "Position", entityTag.Position);
		Quaternion rotation = entityTag.Rotation;
		bool flag = StatMaster.SavingXML && OptionsMaster.BesiegeConfig.ExcludeDefaultSaveData;
		if (!flag || !LevelEditor.IsEqualQuat(rotation, Quaternion.identity))
		{
			WriteQuaternion(xmlWriter, "Rotation", rotation);
		}
		Vector3 scale = entityTag.Scale;
		if (!flag || !LevelEditor.IsEqualVec(scale, Vector3.one))
		{
			WriteVector3(xmlWriter, "Scale", scale);
		}
		xmlWriter.WriteStartElement("Data");
		foreach (XData item in entityTag.GetEntityData().ReadAll())
		{
			xmlWriter.WriteStartElement(item.Type);
			xmlWriter.WriteAttributeString("key", item.Key.ToString(StaticSettings.Culture));
			XAttribute[] array = item.Serialize();
			if (item.IsArrayData || array.Length > 1)
			{
				XAttribute[] array2 = array;
				foreach (XAttribute xAttribute in array2)
				{
					xmlWriter.WriteElementString(xAttribute.Name.ToString(StaticSettings.Culture), xAttribute.Value.ToString(StaticSettings.Culture));
				}
			}
			else
			{
				xmlWriter.WriteString(array[0].Value.ToString(StaticSettings.Culture));
			}
			xmlWriter.WriteEndElement();
		}
		xmlWriter.WriteEndElement();
		xmlWriter.WriteEndElement();
		return true;
	}

	private static XmlWriter CreateWriter(string filePath)
	{
		if (File.Exists(filePath))
		{
			File.Delete(filePath);
		}
		XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
		SetXMLWriterSettings(xmlWriterSettings);
		return XmlWriter.Create(filePath, xmlWriterSettings);
	}

	private static string GetBoolString(bool toggle)
	{
		return (!toggle) ? "False" : "True";
	}

	private static void WriteEntities(XmlWriter xmlWriter, List<LevelEntity> objects)
	{
		xmlWriter.WriteStartElement("Objects");
		for (int i = 0; i < objects.Count; i++)
		{
			WriteEntity(xmlWriter, objects[i]);
		}
		xmlWriter.WriteEndElement();
	}

	private static void WriteVector3(XmlWriter xmlWriter, string name, Vector3 vector3)
	{
		xmlWriter.WriteStartElement(name);
		for (int i = 0; i < 3; i++)
		{
			xmlWriter.WriteAttributeString(vqNames[i], vector3[i].ToString());
		}
		xmlWriter.WriteEndElement();
	}

	private static void WriteQuaternion(XmlWriter xmlWriter, string name, Quaternion quaternion)
	{
		xmlWriter.WriteStartElement(name);
		for (int i = 0; i < 4; i++)
		{
			xmlWriter.WriteAttributeString(vqNames[i], quaternion[i].ToString());
		}
		xmlWriter.WriteEndElement();
	}
}
