public class CustomRoomDataFile
{
	public enum CustomType
	{
		Texture = 0,
		Sound = 1,
		Model = 2,
		Lua = 3,
		Other = 4
	}

	public string path;

	public CustomType customType;

	public byte[] data;

	public CustomRoomDataFile(string path, CustomType customType, byte[] data)
	{
		this.path = path;
		this.customType = customType;
		this.data = data;
	}

	public static CustomType getCustomType(string fileName)
	{
		if (fileName.ToLower().EndsWith(".jpg") || fileName.ToLower().EndsWith(".png"))
		{
			return CustomType.Texture;
		}
		if (fileName.ToLower().EndsWith(".mp3"))
		{
			return CustomType.Sound;
		}
		if (fileName.ToLower().EndsWith(".lua"))
		{
			return CustomType.Lua;
		}
		if (fileName.EndsWith(".gltf") || fileName.EndsWith(".glb"))
		{
			return CustomType.Model;
		}
		return CustomType.Other;
	}
}
