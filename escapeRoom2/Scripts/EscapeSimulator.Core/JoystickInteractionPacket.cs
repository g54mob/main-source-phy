using System.Text;
using UnityEngine;

public sealed class JoystickInteractionPacket : Packet
{
	public Joystick joystick;

	public Vector2 newRotation;

	public override byte getTypeId()
	{
		return 69;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		writer.WriteComponent(joystick);
		writer.WriteVector2(in newRotation);
	}

	public override void readData(FastBinaryReader reader)
	{
		joystick = reader.ReadComponent<Joystick>();
		newRotation = reader.ReadVector2();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
		stringBuilder.AppendLine("joystick: " + $"{joystick}");
		stringBuilder.Append("newRotation: " + $"{newRotation}");
		return stringBuilder.ToString();
	}
}
