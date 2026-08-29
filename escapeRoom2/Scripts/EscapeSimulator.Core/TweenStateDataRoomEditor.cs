using System;
using System.Collections.Generic;
using System.Text;

[Serializable]
public class TweenStateDataRoomEditor : IReadWrite
{
	public List<TweenStateRecordRoomEditor> tweenStates;

	public virtual void Write(FastBinaryWriter writer)
	{
		writer.WriteList(tweenStates, delegate(FastBinaryWriter w, TweenStateRecordRoomEditor e)
		{
			w.WriteIReadWrite(e);
		});
	}

	public virtual void Read(FastBinaryReader reader)
	{
		tweenStates = reader.ReadList((FastBinaryReader r) => r.ReadIReadWrite<TweenStateRecordRoomEditor>());
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("tweenStates: " + ToStringHelper.Stringify(tweenStates, (TweenStateRecordRoomEditor e) => ToStringHelper.Stringify(e)));
		return stringBuilder.ToString();
	}
}
