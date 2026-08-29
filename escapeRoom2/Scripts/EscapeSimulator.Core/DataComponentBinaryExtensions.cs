public static class DataComponentBinaryExtensions
{
	public static void WriteCustomStruct(this FastBinaryWriter writer, DataComponent.CustomStruct data)
	{
		writer.Write(in data.nestedInt, default(FastBinaryWriter.ForPrimitives));
		writer.Write(data.nestedString);
	}

	public static DataComponent.CustomStruct ReadCustomStruct(this FastBinaryReader reader)
	{
		return new DataComponent.CustomStruct
		{
			nestedInt = reader.ReadInt32(),
			nestedString = reader.ReadString()
		};
	}
}
