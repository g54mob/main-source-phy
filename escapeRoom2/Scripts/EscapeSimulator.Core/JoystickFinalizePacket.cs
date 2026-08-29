using System.Text;

public sealed class JoystickFinalizePacket : Packet
{
	public Joystick joystick;

	public override byte getTypeId()
	{
		return 70;
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
