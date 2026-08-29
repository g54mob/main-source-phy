using System.Text;

public sealed class JoystickStartPacket : Packet
{
	public Joystick joystick;

	public override byte getTypeId()
	{
		return 68;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(joystick);
	}

	public override void readData(FastBinaryReader reader)
	{
		joystick = reader.ReadComponent<Joystick>();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.Append("joystick: " + $"{joystick}");
		return stringBuilder.ToString();
	}
}
