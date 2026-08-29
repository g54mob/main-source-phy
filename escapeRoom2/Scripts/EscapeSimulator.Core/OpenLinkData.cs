using System;
using System.Text;

[Serializable]
public class OpenLinkData : IReadWrite
{
	public string link;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.Write(link);
	}

	public virtual void Read(FastBinaryReader reader)
	{
		link = reader.ReadString();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("link: " + ToStringHelper.Stringify(link));
		return stringBuilder.ToString();
	}
}
