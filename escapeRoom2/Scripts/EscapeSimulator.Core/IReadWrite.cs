public interface IReadWrite
{
	void Write(FastBinaryWriter writer);

	void Read(FastBinaryReader reader);
}
