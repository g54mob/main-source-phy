public sealed class DefaultNoIdentifierTransition : Transition
{
	public override byte getTypeId()
	{
		return byte.MaxValue;
	}

	public override void writeData(FastBinaryWriter writer)
	{
		base.writeData(writer);
	}

	public override void readData(FastBinaryReader reader)
	{
		base.readData(reader);
	}
}
