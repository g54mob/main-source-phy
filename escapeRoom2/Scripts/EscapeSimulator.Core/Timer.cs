using System.Text;
using UnityEngine;

public abstract class Timer
{
	public float time;

	public float duration;

	public bool isCancelled;

	public float unitTime
	{
		get
		{
			if (!(time < 0f))
			{
				if (duration != 0f)
				{
					return Mathf.Clamp01(time / duration);
				}
				return 1f;
			}
			return 0f;
		}
	}

	public static Timer fromLevelLogic(FastBinaryReader reader, LevelLogic levelLogic)
	{
		byte b = reader.ReadByte();
		Timer timer = ((b == byte.MaxValue) ? new DefaultNoIdentifierTransition() : levelLogic.getTimer(b));
		if (timer != null)
		{
			timer.read(reader);
		}
		else
		{
			Debug.LogError(string.Format("Could not get valid {0} instance for type ID {1}.", "Timer", b));
		}
		return timer;
	}

	public void write(FastBinaryWriter writer)
	{
		writer.Write(getTypeId());
		writer.Write(in time, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in duration, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in isCancelled, default(FastBinaryWriter.ForPrimitives));
		writeData(writer);
	}

	public void read(FastBinaryReader reader)
	{
		time = reader.ReadSingle();
		duration = reader.ReadSingle();
		isCancelled = reader.ReadBoolean();
		readData(reader);
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine($"type: {GetType()}");
		stringBuilder.AppendLine(string.Format("{0}: {1}", "time", time));
		stringBuilder.AppendLine(string.Format("{0}: {1}", "duration", duration));
		stringBuilder.AppendLine(string.Format("{0}: {1}", "isCancelled", isCancelled));
		return stringBuilder.ToString();
	}

	public abstract byte getTypeId();

	public abstract void writeData(FastBinaryWriter writer);

	public abstract void readData(FastBinaryReader reader);
}
