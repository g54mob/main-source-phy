using System.Text;

public sealed class ChangeSelectedLevelInPickerPacket : Packet
{
	public LevelPickerType selectedPicker;

	public string selectedLevel;

	public override byte getTypeId()
	{
		return 12;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		int value = (int)selectedPicker;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(selectedLevel);
	}

	public override void readData(FastBinaryReader reader)
	{
		selectedPicker = (LevelPickerType)reader.ReadInt32();
		selectedLevel = reader.ReadString();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("selectedPicker: " + $"{selectedPicker}");
		stringBuilder.Append("selectedLevel: " + ToStringHelper.Stringify(selectedLevel));
		return stringBuilder.ToString();
	}
}
