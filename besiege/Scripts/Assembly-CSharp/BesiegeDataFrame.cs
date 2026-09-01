public class BesiegeDataFrame
{
	public uint frame;

	public ushort current;

	public byte[] data;

	public int session;

	public int dataSize;

	public BesiegeDataFrame()
	{
	}

	public BesiegeDataFrame(uint frame, int session, ushort current, byte[] data, int dataSize)
	{
		Update(frame, session, current, data, dataSize);
	}

	public void Update(uint frame, int session, ushort current, byte[] data, int dataSize)
	{
		this.frame = frame;
		this.session = session;
		this.current = current;
		this.data = data;
		this.dataSize = dataSize;
	}
}
