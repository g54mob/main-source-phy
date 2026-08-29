using System.Text;

public sealed class CharacterCustomizationPacket : Packet
{
	public CharacterCustomization customization;

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteSaveable(customization);
	}

	public override void readData(FastBinaryReader reader)
	{
		customization = reader.ReadSaveable<CharacterCustomization>();
	}

	public override byte getTypeId()
	{
		return 7;
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("customization: " + $"{customization}");
		return stringBuilder.ToString();
	}
}
