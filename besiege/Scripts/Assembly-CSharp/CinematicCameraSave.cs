using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

[Serializable]
public class CinematicCameraSave
{
	public float TimeScale { get; set; }

	public CinematicCam.CamSetting PosA { get; set; }

	public CinematicCam.CamSetting PosB { get; set; }

	public float Duration { get; set; }

	public KeyCode Key { get; set; }

	public float KeyDelay { get; set; }

	public bool DelayShader { get; set; }

	public float ShaderDelay { get; set; }

	public bool RetimeShader { get; set; }

	public float ShaderSpeed { get; set; }

	public bool FollowMachine { get; set; }

	public bool Ease { get; set; }

	public bool ShowCursor { get; set; }

	public static void Save(string path, CinematicCameraSave info)
	{
		using (StreamWriter textWriter = new StreamWriter(path, false, Encoding.UTF8))
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(CinematicCameraSave));
			xmlSerializer.Serialize(textWriter, info);
		}
	}

	public static void Load(string path, CinematicCameraSave info)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(CinematicCameraSave));
		FileStream fileStream = new FileStream(path, FileMode.Open);
		CinematicCameraSave source = xmlSerializer.Deserialize(fileStream) as CinematicCameraSave;
		fileStream.Close();
		source.CopyProperties(info);
	}
}
