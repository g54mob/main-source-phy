using System;
using System.IO;
using Sirenix.Serialization;
using UnityEngine;

public class SandboxCustomCamera
{
	private static string FILE_EXTENSION = ".camera";

	public static string Serialize(string path, string name)
	{
		if (!Directory.Exists(path))
		{
			return null;
		}
		string path2 = AddFileExtension(name);
		string text = Path.Combine(path, path2);
		byte[] bytes = SerializationUtility.SerializeValue(new SandboxCustomCameraProxy(Cameras.MainCamera()), DataFormat.JSON);
		try
		{
			File.WriteAllBytes(text, bytes);
			return text;
		}
		catch (Exception ex)
		{
			Debug.LogWarningFormat("Caught exception in SandboxCustomCamera::Serialize {0}", ex.Message);
			return null;
		}
	}

	public static void TryLoad(string name)
	{
		string path = AddFileExtension(name);
		string text = Path.Combine(Application.streamingAssetsPath, "MainMenuCameras", path);
		if (File.Exists(text))
		{
			SandboxCustomCameraProxy sandboxCustomCameraProxy = Deserialize(text);
			if (sandboxCustomCameraProxy != null)
			{
				Cameras.MainCamera().transform.position = sandboxCustomCameraProxy.m_Pos;
				Cameras.MainCamera().transform.rotation = sandboxCustomCameraProxy.m_Rot;
				Cameras.SetOrthographicSize(sandboxCustomCameraProxy.m_OrthographicSize);
			}
		}
	}

	private static SandboxCustomCameraProxy Deserialize(string pathAndFilename)
	{
		try
		{
			return SerializationUtility.DeserializeValue<SandboxCustomCameraProxy>(File.ReadAllBytes(AddFileExtension(pathAndFilename)), DataFormat.JSON);
		}
		catch (Exception ex)
		{
			Debug.LogWarningFormat("Caught Exception in SandboxCustomCamera::Deserialize {0}", ex.Message);
			return null;
		}
	}

	public static string AddFileExtension(string filename)
	{
		if (Path.GetExtension(filename) == FILE_EXTENSION)
		{
			return filename;
		}
		return filename + FILE_EXTENSION;
	}
}
