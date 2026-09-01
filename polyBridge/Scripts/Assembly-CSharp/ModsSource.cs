using System;
using System.Collections.Generic;
using System.IO;
using Sirenix.Serialization;
using UnityEngine;

public class ModsSource
{
	public static Dictionary<string, string> m_Source = new Dictionary<string, string>();

	public static Dictionary<string, string> m_Uploads = new Dictionary<string, string>();

	private static readonly string MODS_SOURCE_FILENAME = ".modssource";

	private static readonly string MOD_UPLOADS_FILENAME = ".moduploads";

	public static void Init()
	{
		LoadSource();
		LoadUploads();
	}

	public static string GetSourceFolder(string localItemID)
	{
		if (m_Source.ContainsKey(localItemID))
		{
			return m_Source[localItemID];
		}
		return string.Empty;
	}

	public static void SaveSourceFolder(string localItemID, string fullpath)
	{
		if (m_Source.ContainsKey(localItemID))
		{
			m_Source[localItemID] = fullpath;
		}
		else
		{
			m_Source.Add(localItemID, fullpath);
		}
		SaveSource();
	}

	public static string GetUploadByLocalItemID(string localItemID)
	{
		foreach (KeyValuePair<string, string> upload in m_Uploads)
		{
			if (upload.Value == localItemID)
			{
				return upload.Key;
			}
		}
		return string.Empty;
	}

	public static string GetLocalItemFromUpload(string itemID)
	{
		if (m_Uploads.ContainsKey(itemID))
		{
			return m_Uploads[itemID];
		}
		return string.Empty;
	}

	public static void SaveUpload(string itemID, string localItemID)
	{
		if (m_Uploads.ContainsKey(itemID))
		{
			m_Uploads[itemID] = localItemID;
		}
		else
		{
			m_Uploads.Add(itemID, localItemID);
		}
		SaveUploads();
	}

	private static void LoadSource()
	{
		string fullPath = Path.Combine(Profiles.GetProfileRootDirectory(), MODS_SOURCE_FILENAME);
		if (!Utils.FileExists(fullPath))
		{
			return;
		}
		byte[] array = Utils.ReadAllBytes(fullPath);
		if (array != null && array.Length != 0)
		{
			try
			{
				m_Source = SerializationUtility.DeserializeValue<Dictionary<string, string>>(array, DataFormat.Binary);
			}
			catch (Exception ex)
			{
				Debug.LogWarning("Exception parsing " + MODS_SOURCE_FILENAME + ": " + ex.Message);
			}
		}
	}

	private static void SaveSource()
	{
		byte[] bytes = SerializationUtility.SerializeValue(m_Source, DataFormat.Binary);
		Utils.WriteBytes(Path.Combine(Profiles.GetProfileRootDirectory(), MODS_SOURCE_FILENAME), bytes);
	}

	private static void LoadUploads()
	{
		string fullPath = Path.Combine(Profiles.GetProfileRootDirectory(), MOD_UPLOADS_FILENAME);
		if (!Utils.FileExists(fullPath))
		{
			return;
		}
		byte[] array = Utils.ReadAllBytes(fullPath);
		if (array != null && array.Length != 0)
		{
			try
			{
				m_Uploads = SerializationUtility.DeserializeValue<Dictionary<string, string>>(array, DataFormat.Binary);
			}
			catch (Exception ex)
			{
				Debug.LogWarning("Exception parsing " + MOD_UPLOADS_FILENAME + ": " + ex.Message);
			}
		}
	}

	private static void SaveUploads()
	{
		byte[] bytes = SerializationUtility.SerializeValue(m_Uploads, DataFormat.Binary);
		Utils.WriteBytes(Path.Combine(Profiles.GetProfileRootDirectory(), MOD_UPLOADS_FILENAME), bytes);
	}
}
