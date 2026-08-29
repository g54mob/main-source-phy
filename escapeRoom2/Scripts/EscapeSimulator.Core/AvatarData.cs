using UnityEngine;

public class AvatarData
{
	public byte[] avatarData;

	public int width;

	public int height;

	public byte[] serialize()
	{
		FastBinaryWriter writer;
		using (SharedWriter.borrow(out writer))
		{
			writer.WriteByteArray(avatarData);
			writer.Write(in width, default(FastBinaryWriter.ForPrimitives));
			writer.Write(in height, default(FastBinaryWriter.ForPrimitives));
			Debug.Log(string.Format("[{0}] Serialized avatar ({1} bytes) with width {2} and height {3}.", "AvatarData", avatarData.Length, width, height));
			return writer.ToArray();
		}
	}

	public static AvatarData deserialize(byte[] data)
	{
		AvatarData avatarData = new AvatarData();
		if (data == null)
		{
			return avatarData;
		}
		using FastBinaryReader reader = new FastBinaryReader(data);
		avatarData.avatarData = reader.ReadByteArray();
		avatarData.width = reader.ReadInt32();
		avatarData.height = reader.ReadInt32();
		Debug.Log(string.Format("[{0}] Deserialized avatar ({1} bytes) with width {2} and height {3}.", "AvatarData", avatarData.avatarData.Length, avatarData.width, avatarData.height));
		return avatarData;
	}
}
