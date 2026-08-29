using System;
using System.Collections.Generic;
using System.Text;

[Serializable]
public class LockData : IReadWrite
{
	public LockType type;

	public List<int> password;

	public bool disableLockOnUnlock;

	public float delayCheckPassword;

	public RoomEditorLinksData onUnlock;

	public RoomEditorLinksData onLock;

	public LockLogicType logicType;

	public bool negateValue;

	public bool exitZoomOnUnlock;

	public virtual void Write(FastBinaryWriter writer)
	{
		int value = (int)type;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(password, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in disableLockOnUnlock, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in delayCheckPassword, default(FastBinaryWriter.ForPrimitives));
		writer.WriteIReadWrite(onUnlock);
		writer.WriteIReadWrite(onLock);
		value = (int)logicType;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in negateValue, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in exitZoomOnUnlock, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void Read(FastBinaryReader reader)
	{
		type = (LockType)reader.ReadInt32();
		password = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		disableLockOnUnlock = reader.ReadBoolean();
		delayCheckPassword = reader.ReadSingle();
		onUnlock = reader.ReadIReadWrite<RoomEditorLinksData>();
		onLock = reader.ReadIReadWrite<RoomEditorLinksData>();
		logicType = (LockLogicType)reader.ReadInt32();
		negateValue = reader.ReadBoolean();
		exitZoomOnUnlock = reader.ReadBoolean();
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("type: " + $"{type}");
		stringBuilder.AppendLine("password: " + ToStringHelper.Stringify(password, (int e) => $"{e}"));
		stringBuilder.AppendLine("disableLockOnUnlock: " + $"{disableLockOnUnlock}");
		stringBuilder.AppendLine("delayCheckPassword: " + $"{delayCheckPassword}");
		stringBuilder.AppendLine("onUnlock: " + ToStringHelper.Stringify(onUnlock));
		stringBuilder.AppendLine("onLock: " + ToStringHelper.Stringify(onLock));
		stringBuilder.AppendLine("logicType: " + $"{logicType}");
		stringBuilder.AppendLine("negateValue: " + $"{negateValue}");
		stringBuilder.Append("exitZoomOnUnlock: " + $"{exitZoomOnUnlock}");
		return stringBuilder.ToString();
	}
}
