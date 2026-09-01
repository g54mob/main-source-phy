using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.Serialization;
using UnityEngine;

public class CustomShapesLibrary
{
	public static Dictionary<string, CustomShapesLibrarySlot> m_LocalSlots = new Dictionary<string, CustomShapesLibrarySlot>();

	public static Dictionary<string, CustomShapesLibrarySlot> m_GameSlots = new Dictionary<string, CustomShapesLibrarySlot>();

	public static Dictionary<string, CustomShapesLibrarySlot> m_UGCSlots = new Dictionary<string, CustomShapesLibrarySlot>();

	public static Dictionary<string, CustomShapesLibrarySlot> m_AllSlots = new Dictionary<string, CustomShapesLibrarySlot>();

	public static Dictionary<string, CustomShapesLibrarySlot> m_DynamicPropSlots = new Dictionary<string, CustomShapesLibrarySlot>();

	public static string CUSTOM_SHAPE_LIBRARY_FOLDER = "CustomShapeLibrary";

	public static string CUSTOM_SHAPE_LIBRARY_TRASH_FOLDER = "CustomShapeLibraryTrash";

	private static string SLOT_INFO_FILENAME = "propstub";

	public static void Init()
	{
		RegisterFromPath(Application.persistentDataPath, CustomShapesLibrarySlotType.LOCAL);
		RegisterFromPath(Application.streamingAssetsPath, CustomShapesLibrarySlotType.GAME);
		RegisterFromPath(Application.streamingAssetsPath, CustomShapesLibrarySlotType.DYNAMIC_PROP);
	}

	public static void ClearUGCSlots()
	{
		m_UGCSlots.Clear();
	}

	public static void RegisterSlot(CustomShapesLibrarySlotType slotType, string path, string displayNameLocID, FileInfo[] fileInfos)
	{
		if (fileInfos.Length == 0)
		{
			return;
		}
		Sprite sprite = null;
		CustomShapesLibrarySlotProxy customShapesLibrarySlotProxy = LoadSlotInfo(path);
		if (customShapesLibrarySlotProxy != null)
		{
			displayNameLocID = customShapesLibrarySlotProxy.m_DisplayNamLocID;
			if (!string.IsNullOrEmpty(customShapesLibrarySlotProxy.m_IconFilename))
			{
				sprite = Utils.CreateSpriteFromTexture(Path.Combine(path, customShapesLibrarySlotProxy.m_IconFilename));
			}
		}
		CustomShapesLibrarySlot customShapesLibrarySlot = new CustomShapesLibrarySlot(slotType, displayNameLocID, sprite, path, fileInfos);
		if (customShapesLibrarySlot != null)
		{
			AddSlot(customShapesLibrarySlot);
		}
	}

	public static CustomShapesLibrarySlot GetSlotByFullPath(string fullpath)
	{
		string fileName = Path.GetFileName(fullpath);
		if (m_AllSlots.ContainsKey(fileName))
		{
			return m_AllSlots[fileName];
		}
		return null;
	}

	public static void DumpLibraryFilenamesToDebugOutput()
	{
		Debug.Log("\nLocal Shape Library:");
		DebugOutputShapeLibraryForType(CustomShapesLibrarySlotType.LOCAL);
		Debug.Log("\nGame Shape Library:");
		DebugOutputShapeLibraryForType(CustomShapesLibrarySlotType.GAME);
		Debug.Log("\nUGC Shape Library:");
		DebugOutputShapeLibraryForType(CustomShapesLibrarySlotType.UGC);
		Debug.Log("\nDynamic Props Library:");
		DebugOutputShapeLibraryForType(CustomShapesLibrarySlotType.DYNAMIC_PROP);
	}

	private static void DebugOutputShapeLibraryForType(CustomShapesLibrarySlotType slotType)
	{
		switch (slotType)
		{
		case CustomShapesLibrarySlotType.GAME:
			DebugOutputShapeLibraryForDict(m_GameSlots);
			break;
		case CustomShapesLibrarySlotType.LOCAL:
			DebugOutputShapeLibraryForDict(m_LocalSlots);
			break;
		case CustomShapesLibrarySlotType.UGC:
			DebugOutputShapeLibraryForDict(m_UGCSlots);
			break;
		case CustomShapesLibrarySlotType.DYNAMIC_PROP:
			DebugOutputShapeLibraryForDict(m_DynamicPropSlots);
			break;
		default:
			Debug.LogWarning($"Unexpected slot type {slotType} in DebugOutputShapeLibraryForType()");
			break;
		}
	}

	public static void Add(string displayNameLocID, List<CustomShape> shapes)
	{
		string text = Path.Combine(Application.persistentDataPath, CUSTOM_SHAPE_LIBRARY_FOLDER);
		Utils.CreateDirectory(text);
		string path = Utils.GenerateUniqueId();
		text = Path.Combine(text, path);
		if (Utils.DirectoryExists(text))
		{
			Utils.DeleteDirectoryAndContents(text);
		}
		Utils.CreateDirectory(text);
		for (int i = 0; i < shapes.Count; i++)
		{
			CustomShape customShape = shapes[i];
			byte[] bytes = SerializationUtility.SerializeValue(new CustomShapeProxy(customShape), DataFormat.JSON);
			string filename = Path.ChangeExtension($"cs-{i + 1}", customShape.IsDynamicProp() ? CustomShapes.CUSTOM_SHAPE_DYNAMIC_PROP_EXT : CustomShapes.CUSTOM_SHAPE_EXT);
			Utils.WriteBytesWithBackup(text, filename, bytes);
		}
		FileInfo[] fileInfos = GetFileInfos(text, CustomShapes.CUSTOM_SHAPE_EXT, CustomShapes.CUSTOM_SHAPE_DYNAMIC_PROP_EXT);
		if (fileInfos.Length != 0)
		{
			RegisterSlot(CustomShapesLibrarySlotType.LOCAL, text, displayNameLocID, fileInfos);
		}
		string prefabAddress = string.Empty;
		foreach (CustomShape shape in shapes)
		{
			if (shape.m_MeshId != CustomShapes.AUTO_GENERATED_MESH_ID)
			{
				prefabAddress = shape.m_MeshId;
				break;
			}
		}
		SaveSlotInfo(text, displayNameLocID, string.Empty, prefabAddress);
	}

	public static List<CustomShape> SpawnByFullPath(string fullpath, Vector3 pos)
	{
		CustomShapesLibrarySlot slotByFullPath = GetSlotByFullPath(fullpath);
		if (slotByFullPath == null)
		{
			Debug.LogWarning("Failed to spawn '" + fullpath + "' from CustomShapesLibrary");
			return null;
		}
		return Spawn(slotByFullPath, pos);
	}

	public static bool DeleteLocalLibrarySlot(string fullpath)
	{
		if (!fullpath.StartsWith(Path.Combine(Application.persistentDataPath, CUSTOM_SHAPE_LIBRARY_FOLDER)))
		{
			return false;
		}
		string fileName = Path.GetFileName(fullpath);
		string text = Path.Combine(Application.persistentDataPath, CUSTOM_SHAPE_LIBRARY_TRASH_FOLDER, fileName);
		if (Utils.DirectoryExists(text))
		{
			text = text + "_" + Utils.GenerateUniqueId();
		}
		Utils.CopyFlatDirectory(fullpath, text);
		if (Utils.DeleteDirectoryAndContents(fullpath))
		{
			if (m_LocalSlots.ContainsKey(fileName))
			{
				m_LocalSlots.Remove(fileName);
			}
			return true;
		}
		return false;
	}

	private static List<CustomShape> Spawn(CustomShapesLibrarySlot slot, Vector3 pos)
	{
		List<CustomShape> list = new List<CustomShape>();
		try
		{
			Vector3 vector = Vector3.zero;
			for (int i = 0; i < slot.m_Filenames.Count; i++)
			{
				byte[] array = Utils.ReadAllBytes(Path.Combine(slot.m_FullyQualifiedPath, slot.m_Filenames[i]));
				if (array != null && array.Length != 0)
				{
					CustomShapeProxy customShapeProxy = SerializationUtility.DeserializeValue<CustomShapeProxy>(array, DataFormat.JSON);
					ApplyVersionFixups(customShapeProxy);
					if (i == 0)
					{
						vector = customShapeProxy.m_Pos;
					}
					CustomShape customShape = CustomShapes.CreateCustomShapeFromProxy(customShapeProxy);
					if (customShape != null)
					{
						list.Add(customShape);
						customShape.transform.position = pos + (customShapeProxy.m_Pos - vector);
					}
				}
			}
			if (list.Count > 0)
			{
				CenterSpawnedShapes(list, pos);
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarningFormat("Caught Exception in CustomShapes::Import {0}", ex.Message);
		}
		return list;
	}

	private static void ApplyVersionFixups(CustomShapeProxy proxy)
	{
		if (proxy.m_Version < 1)
		{
			proxy.m_CollidesWithVehicles = true;
		}
	}

	private static void CenterSpawnedShapes(List<CustomShape> shapes, Vector3 center)
	{
		if (shapes.Count < 2)
		{
			return;
		}
		Bounds bounds = new Bounds(center, Vector3.one);
		foreach (CustomShape shape in shapes)
		{
			bounds.Encapsulate(shape.m_MeshRenderer.bounds);
		}
		Vector3 vector = bounds.center - center;
		foreach (CustomShape shape2 in shapes)
		{
			shape2.transform.Translate(0f - vector.x, 0f - vector.y, 0f);
		}
	}

	private static void RegisterFromPath(string path, CustomShapesLibrarySlotType slotType)
	{
		path = Path.Combine(path, CUSTOM_SHAPE_LIBRARY_FOLDER);
		if (!Utils.DirectoryExists(path))
		{
			return;
		}
		try
		{
			string[] directories = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
			foreach (string path2 in directories)
			{
				if (slotType == CustomShapesLibrarySlotType.DYNAMIC_PROP)
				{
					FileInfo[] files = new DirectoryInfo(path2).GetFiles("*" + CustomShapes.CUSTOM_SHAPE_DYNAMIC_PROP_EXT);
					if (files.Length != 0)
					{
						RegisterSlot(slotType, path2, Path.GetFileNameWithoutExtension(path2), files);
					}
				}
				else
				{
					FileInfo[] fileInfos = GetFileInfos(path2, CustomShapes.CUSTOM_SHAPE_EXT, CustomShapes.CUSTOM_SHAPE_DYNAMIC_PROP_EXT);
					if (fileInfos.Length != 0)
					{
						RegisterSlot(slotType, path2, Path.GetFileNameWithoutExtension(path2), fileInfos);
					}
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Failed to enumerate shape files at path " + path + " due to " + ex.Message);
		}
	}

	private static void AddSlot(CustomShapesLibrarySlot slot)
	{
		switch (slot.m_SlotType)
		{
		case CustomShapesLibrarySlotType.GAME:
			UpdateDict(m_GameSlots, slot);
			break;
		case CustomShapesLibrarySlotType.LOCAL:
			UpdateDict(m_LocalSlots, slot);
			break;
		case CustomShapesLibrarySlotType.UGC:
			UpdateDict(m_UGCSlots, slot);
			break;
		case CustomShapesLibrarySlotType.DYNAMIC_PROP:
			UpdateDict(m_DynamicPropSlots, slot);
			break;
		default:
			Debug.LogWarning($"Unexpected slot type {slot.m_SlotType} in AddSlot()");
			break;
		}
		string fileName = Path.GetFileName(slot.m_FullyQualifiedPath);
		if (m_AllSlots.ContainsKey(fileName))
		{
			m_AllSlots[fileName] = slot;
		}
		else
		{
			m_AllSlots.Add(fileName, slot);
		}
	}

	private static void UpdateDict(Dictionary<string, CustomShapesLibrarySlot> dict, CustomShapesLibrarySlot slot)
	{
		string fileName = Path.GetFileName(slot.m_FullyQualifiedPath);
		if (dict.ContainsKey(fileName))
		{
			dict[fileName] = slot;
		}
		else
		{
			dict.Add(fileName, slot);
		}
	}

	private static CustomShapesLibrarySlot GetSlotByTypeAndDisplayname(CustomShapesLibrarySlotType slotType, string displayName)
	{
		switch (slotType)
		{
		case CustomShapesLibrarySlotType.GAME:
			if (!m_GameSlots.ContainsKey(displayName))
			{
				return null;
			}
			return m_GameSlots[displayName];
		case CustomShapesLibrarySlotType.LOCAL:
			if (!m_LocalSlots.ContainsKey(displayName))
			{
				return null;
			}
			return m_LocalSlots[displayName];
		case CustomShapesLibrarySlotType.UGC:
			if (!m_UGCSlots.ContainsKey(displayName))
			{
				return null;
			}
			return m_UGCSlots[displayName];
		case CustomShapesLibrarySlotType.DYNAMIC_PROP:
			if (!m_DynamicPropSlots.ContainsKey(displayName))
			{
				return null;
			}
			return m_DynamicPropSlots[displayName];
		default:
			Debug.LogWarning($"Unexpected slot type {slotType} in GetSlot()");
			return null;
		}
	}

	private static void DebugOutputShapeLibraryForDict(Dictionary<string, CustomShapesLibrarySlot> dict)
	{
		foreach (KeyValuePair<string, CustomShapesLibrarySlot> item in dict)
		{
			Debug.Log("\t" + item.Key);
			foreach (string filename in item.Value.m_Filenames)
			{
				Debug.Log("\t\t" + filename);
			}
		}
	}

	private static FileInfo[] GetFileInfos(string path, string extA, string extB)
	{
		DirectoryInfo directoryInfo = new DirectoryInfo(path);
		FileInfo[] files = directoryInfo.GetFiles("*" + extA);
		FileInfo[] files2 = directoryInfo.GetFiles("*" + extB);
		FileInfo[] array = new FileInfo[files.Length + files2.Length];
		Array.Copy(files, array, files.Length);
		Array.Copy(files2, 0, array, files.Length, files2.Length);
		return array;
	}

	public static void SaveSlotInfo(string path, string displayNameLocID, string iconFilename, string prefabAddress)
	{
		if (!Directory.Exists(path))
		{
			return;
		}
		try
		{
			byte[] array = SerializationUtility.SerializeValue(new CustomShapesLibrarySlotProxy(displayNameLocID, iconFilename, prefabAddress), DataFormat.JSON);
			if (array.Length != 0)
			{
				Utils.WriteBytesWithBackup(path, SLOT_INFO_FILENAME, array);
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarningFormat("Exception {0} trying to write progress to: '{1}'", ex.Message, Path.Combine(path, SLOT_INFO_FILENAME));
		}
	}

	public static CustomShapesLibrarySlotProxy LoadSlotInfo(string path)
	{
		if (!Directory.Exists(path))
		{
			return null;
		}
		try
		{
			string path2 = Path.Combine(path, SLOT_INFO_FILENAME);
			if (File.Exists(path2))
			{
				byte[] array = File.ReadAllBytes(path2);
				if (array != null && array.Length != 0)
				{
					return SerializationUtility.DeserializeValue<CustomShapesLibrarySlotProxy>(array, DataFormat.JSON);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogFormat("Caught exception loading local achivements: {0}", ex.Message.ToString());
		}
		return null;
	}
}
