using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

public class ServerConfig
{
	public bool LevelEditorEnabled;

	public string Password;

	public int MaxPlayers;

	public int Port;

	public bool UseUPNPForwarding;

	public List<string> MapRotation;

	public ServerConfig()
	{
		LevelEditorEnabled = true;
		Password = string.Empty;
		MaxPlayers = 8;
		Port = StatMaster.DefaultPort;
		MapRotation = new List<string>();
	}

	public ServerConfig(bool levelEditorEnabled, string password, int maxPlayers, int port, bool useUPNPForwarding, List<string> mapRotation)
	{
		LevelEditorEnabled = levelEditorEnabled;
		Password = password;
		MaxPlayers = maxPlayers;
		Port = port;
		UseUPNPForwarding = useUPNPForwarding;
		MapRotation = mapRotation;
	}

	public void Save(string configPath)
	{
		using (StreamWriter textWriter = new StreamWriter(configPath, false, Encoding.UTF8))
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(ServerConfig));
			xmlSerializer.Serialize(textWriter, this);
		}
	}

	public void Load(string configPath)
	{
		XmlSerializer xmlSerializer = new XmlSerializer(typeof(ServerConfig));
		FileStream fileStream = new FileStream(configPath, FileMode.Open);
		ServerConfig config = xmlSerializer.Deserialize(fileStream) as ServerConfig;
		Clone(config);
		fileStream.Close();
	}

	public void Clone(ServerConfig config)
	{
		LevelEditorEnabled = config.LevelEditorEnabled;
		Password = config.Password;
		MaxPlayers = config.MaxPlayers;
		Port = config.Port;
		UseUPNPForwarding = config.UseUPNPForwarding;
		MapRotation = config.MapRotation;
	}
}
