using System;
using System.Collections.Generic;
using UnityEngine;

public static class FastBinaryExtensions
{
	private const byte IS_EMPTY_ID = 0;

	private const byte IS_SINGLE_PLAYER_ID = 1;

	private const byte IS_STEAM_ID = 2;

	private const byte IS_CROSSPLATFORM_ID = 3;

	public static void WriteByteArray(this FastBinaryWriter writer, byte[] array)
	{
		if (array == null)
		{
			writer.Write<int>(-1, default(FastBinaryWriter.ForPrimitives));
		}
		else
		{
			writer.Write(array, default(FastBinaryWriter.ForPrimitives));
		}
	}

	public static byte[] ReadByteArray(this FastBinaryReader reader)
	{
		int num = reader.ReadInt32();
		if (num < 0)
		{
			return null;
		}
		if (num == 0)
		{
			return Array.Empty<byte>();
		}
		return reader.ReadBytes(num);
	}

	public static void WriteArray<T>(this FastBinaryWriter writer, T[] array, Action<FastBinaryWriter, T> writeStrategy)
	{
		if (array == null)
		{
			writer.Write<int>(-1, default(FastBinaryWriter.ForPrimitives));
			return;
		}
		writer.Write<int>(array.Length, default(FastBinaryWriter.ForPrimitives));
		foreach (T arg in array)
		{
			writeStrategy(writer, arg);
		}
	}

	public static T[] ReadArray<T>(this FastBinaryReader reader, Func<FastBinaryReader, T> readStrategy)
	{
		int num = reader.ReadInt32();
		if (num < 0)
		{
			return null;
		}
		if (num == 0)
		{
			return Array.Empty<T>();
		}
		T[] array = new T[num];
		for (int i = 0; i < num; i++)
		{
			array[i] = readStrategy(reader);
		}
		return array;
	}

	public static void WriteArray2D<T>(this FastBinaryWriter writer, T[,] array, Action<FastBinaryWriter, T> writeStrategy)
	{
		if (array == null)
		{
			writer.Write<int>(-1, default(FastBinaryWriter.ForPrimitives));
			return;
		}
		int value = array.GetLength(0);
		int value2 = array.GetLength(1);
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in value2, default(FastBinaryWriter.ForPrimitives));
		for (int i = 0; i < value; i++)
		{
			for (int j = 0; j < value2; j++)
			{
				writeStrategy(writer, array[i, j]);
			}
		}
	}

	public static T[,] ReadArray2D<T>(this FastBinaryReader reader, Func<FastBinaryReader, T> readStrategy)
	{
		int num = reader.ReadInt32();
		if (num < 0)
		{
			return null;
		}
		int num2 = reader.ReadInt32();
		T[,] array = new T[num, num2];
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				array[i, j] = readStrategy(reader);
			}
		}
		return array;
	}

	public static void WriteArray3D<T>(this FastBinaryWriter writer, T[,,] array, Action<FastBinaryWriter, T> writeStrategy)
	{
		if (array == null)
		{
			writer.Write<int>(-1, default(FastBinaryWriter.ForPrimitives));
			return;
		}
		int value = array.GetLength(0);
		int value2 = array.GetLength(1);
		int value3 = array.GetLength(2);
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in value2, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in value3, default(FastBinaryWriter.ForPrimitives));
		for (int i = 0; i < value; i++)
		{
			for (int j = 0; j < value2; j++)
			{
				for (int k = 0; k < value3; k++)
				{
					writeStrategy(writer, array[i, j, k]);
				}
			}
		}
	}

	public static T[,,] ReadArray3D<T>(this FastBinaryReader reader, Func<FastBinaryReader, T> readStrategy)
	{
		int num = reader.ReadInt32();
		if (num < 0)
		{
			return null;
		}
		int num2 = reader.ReadInt32();
		int num3 = reader.ReadInt32();
		T[,,] array = new T[num, num2, num3];
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num2; j++)
			{
				for (int k = 0; k < num3; k++)
				{
					array[i, j, k] = readStrategy(reader);
				}
			}
		}
		return array;
	}

	public static void WriteList<T>(this FastBinaryWriter writer, List<T> list, Action<FastBinaryWriter, T> writeStrategy)
	{
		if (list == null)
		{
			writer.Write<int>(-1, default(FastBinaryWriter.ForPrimitives));
			return;
		}
		writer.Write<int>(list.Count, default(FastBinaryWriter.ForPrimitives));
		foreach (T item in list)
		{
			writeStrategy(writer, item);
		}
	}

	public static List<T> ReadList<T>(this FastBinaryReader reader, Func<FastBinaryReader, T> readStrategy)
	{
		int num = reader.ReadInt32();
		if (num < 0)
		{
			return null;
		}
		List<T> list = new List<T>(num);
		for (int i = 0; i < num; i++)
		{
			list.Add(readStrategy(reader));
		}
		return list;
	}

	public static void WriteQueue<T>(this FastBinaryWriter writer, Queue<T> queue, Action<FastBinaryWriter, T> writeStrategy)
	{
		if (queue == null)
		{
			writer.Write<int>(-1, default(FastBinaryWriter.ForPrimitives));
			return;
		}
		writer.Write<int>(queue.Count, default(FastBinaryWriter.ForPrimitives));
		foreach (T item in queue)
		{
			writeStrategy(writer, item);
		}
	}

	public static Queue<T> ReadQueue<T>(this FastBinaryReader reader, Func<FastBinaryReader, T> readStrategy)
	{
		int num = reader.ReadInt32();
		if (num < 0)
		{
			return null;
		}
		Queue<T> queue = new Queue<T>(num);
		for (int i = 0; i < num; i++)
		{
			queue.Enqueue(readStrategy(reader));
		}
		return queue;
	}

	public static void WriteStack<T>(this FastBinaryWriter writer, Stack<T> stack, Action<FastBinaryWriter, T> writeStrategy)
	{
		if (stack == null)
		{
			writer.Write<int>(-1, default(FastBinaryWriter.ForPrimitives));
			return;
		}
		writer.Write<int>(stack.Count, default(FastBinaryWriter.ForPrimitives));
		foreach (T item in stack)
		{
			writeStrategy(writer, item);
		}
	}

	public static Stack<T> ReadStack<T>(this FastBinaryReader reader, Func<FastBinaryReader, T> readStrategy)
	{
		int num = reader.ReadInt32();
		if (num < 0)
		{
			return null;
		}
		Stack<T> stack = new Stack<T>(num);
		for (int i = 0; i < num; i++)
		{
			stack.Push(readStrategy(reader));
		}
		return stack;
	}

	public static void WriteHashSet<T>(this FastBinaryWriter writer, HashSet<T> set, Action<FastBinaryWriter, T> writeStrategy)
	{
		if (set == null)
		{
			writer.Write<int>(-1, default(FastBinaryWriter.ForPrimitives));
			return;
		}
		writer.Write<int>(set.Count, default(FastBinaryWriter.ForPrimitives));
		foreach (T item in set)
		{
			writeStrategy(writer, item);
		}
	}

	public static HashSet<T> ReadHashSet<T>(this FastBinaryReader reader, Func<FastBinaryReader, T> readStrategy)
	{
		int num = reader.ReadInt32();
		if (num < 0)
		{
			return null;
		}
		HashSet<T> hashSet = new HashSet<T>(num);
		for (int i = 0; i < num; i++)
		{
			hashSet.Add(readStrategy(reader));
		}
		return hashSet;
	}

	public static void WriteDictionary<TKey, TValue>(this FastBinaryWriter writer, Dictionary<TKey, TValue> dictionary, Action<FastBinaryWriter, TKey> keyWriteStrategy, Action<FastBinaryWriter, TValue> valueWriteStrategy)
	{
		if (dictionary == null)
		{
			writer.Write<int>(-1, default(FastBinaryWriter.ForPrimitives));
			return;
		}
		writer.Write<int>(dictionary.Count, default(FastBinaryWriter.ForPrimitives));
		foreach (var (arg, arg2) in dictionary)
		{
			keyWriteStrategy(writer, arg);
			valueWriteStrategy(writer, arg2);
		}
	}

	public static Dictionary<TKey, TValue> ReadDictionary<TKey, TValue>(this FastBinaryReader reader, Func<FastBinaryReader, TKey> keyReadStrategy, Func<FastBinaryReader, TValue> valueReadStrategy)
	{
		int num = reader.ReadInt32();
		if (num < 0)
		{
			return null;
		}
		Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(num);
		for (int i = 0; i < num; i++)
		{
			TKey key = keyReadStrategy(reader);
			TValue value = valueReadStrategy(reader);
			dictionary.Add(key, value);
		}
		return dictionary;
	}

	public static void WriteNullable<T>(this FastBinaryWriter writer, T? nullable, Action<FastBinaryWriter, T> writeStrategy) where T : struct
	{
		writer.Write<bool>(nullable.HasValue, default(FastBinaryWriter.ForPrimitives));
		if (nullable.HasValue)
		{
			writeStrategy(writer, nullable.Value);
		}
	}

	public static T? ReadNullable<T>(this FastBinaryReader reader, Func<FastBinaryReader, T> readStrategy) where T : struct
	{
		if (!reader.ReadBoolean())
		{
			return null;
		}
		return readStrategy(reader);
	}

	public static void WriteGameObject(this FastBinaryWriter writer, GameObject gameObject)
	{
		if (gameObject == null)
		{
			writer.Write(byte.MaxValue);
			return;
		}
		byte b = (byte)((!(GameStarter.splitScreenGame == null) && !(gameObject.scene.name != string.Empty)) ? 1u : 0u);
		Net session = Net.getSession(b == 1);
		if (session == null)
		{
			Debug.LogError($"Cannot write game-object '{gameObject}' as session is null.", gameObject);
			writer.Write(byte.MaxValue);
			return;
		}
		if (session.game == null)
		{
			Debug.LogError($"Cannot write game-object '{gameObject}' as session.game is null.", gameObject);
			writer.Write(byte.MaxValue);
			return;
		}
		int value = session.game.getId(gameObject);
		if (value >= 0)
		{
			writer.Write(b);
			writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		}
		else
		{
			Debug.LogError("Cannot write game-object '" + gameObject.name + "' (ID was not assigned).", gameObject);
			writer.Write(byte.MaxValue);
		}
	}

	public static GameObject ReadGameObject(this FastBinaryReader reader)
	{
		byte b = reader.ReadByte();
		if (b == byte.MaxValue)
		{
			return null;
		}
		int id = reader.ReadInt32();
		Net net = ((GameStarter.splitScreenGame == null || b == 1) ? Net.getSession() : Net.getSession(isSplitScreen: true));
		if (net == null || net.game == null)
		{
			return null;
		}
		return net.game.getLevelObject(id)?.gameObject;
	}

	public static void WriteComponent(this FastBinaryWriter writer, Component component)
	{
		writer.WriteGameObject((component != null) ? component.gameObject : null);
	}

	public static T ReadComponent<T>(this FastBinaryReader reader) where T : Component
	{
		GameObject gameObject = reader.ReadGameObject();
		if (!(gameObject != null))
		{
			return null;
		}
		return gameObject.GetComponent<T>();
	}

	public static void WritePose(this FastBinaryWriter writer, Pose pose)
	{
		writer.WriteVector3(in pose.position);
		writer.WriteQuaternion(in pose.rotation);
	}

	public static Pose ReadPose(this FastBinaryReader reader)
	{
		Vector3 position = reader.ReadVector3();
		Quaternion rotation = reader.ReadQuaternion();
		return new Pose(position, rotation);
	}

	public static void WriteNetPlayerId(this FastBinaryWriter writer, NetPlayerId playerId)
	{
		ulong result;
		if (playerId == null)
		{
			Debug.LogError("Attempted to write null player ID (this should not happen; writing empty ID instead).");
			writer.Write(0);
		}
		else if (playerId == NetPlayerId.Empty)
		{
			writer.Write(0);
		}
		else if (playerId == Net.SINGLEPLAYER_ID)
		{
			writer.Write(1);
		}
		else if (ulong.TryParse(playerId.value, out result))
		{
			writer.Write(2);
			writer.Write(in result, default(FastBinaryWriter.ForPrimitives));
		}
		else
		{
			writer.Write(3);
			writer.Write(playerId.value);
		}
	}

	public static NetPlayerId ReadNetPlayerId(this FastBinaryReader reader)
	{
		byte b = reader.ReadByte();
		return b switch
		{
			0 => NetPlayerId.Empty, 
			1 => Net.SINGLEPLAYER_ID, 
			2 => new NetPlayerId(reader.ReadUInt64().ToString()), 
			3 => new NetPlayerId(reader.ReadString()), 
			_ => throw new Exception(string.Format("Invalid ID type while reading {0}: {1}", "NetPlayerId", b)), 
		};
	}

	public static void WriteTransformSnapshot(this FastBinaryWriter writer, TransformSnapshot snapshot)
	{
		writer.WriteGameObject(snapshot.targetObject);
		writer.WriteVector3(in snapshot.position);
		writer.WriteQuaternion(in snapshot.rotation);
		writer.Write(in snapshot.teleport, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in snapshot.ownerTimestamp, default(FastBinaryWriter.ForPrimitives));
		writer.Write(snapshot.localTimeResetIndicator);
	}

	public static TransformSnapshot ReadTransformSnapshot(this FastBinaryReader reader)
	{
		return new TransformSnapshot
		{
			targetObject = reader.ReadGameObject(),
			position = reader.ReadVector3(),
			rotation = reader.ReadQuaternion(),
			teleport = reader.ReadBoolean(),
			ownerTimestamp = reader.ReadSingle(),
			localTimeResetIndicator = reader.ReadByte(),
			receivedTimestamp = Time.time
		};
	}

	public static void WritePaintableEdit(this FastBinaryWriter writer, PaintableEdit paintableEdit)
	{
		writer.WriteColor((Color)paintableEdit.brushColor);
		writer.WriteVector2(in paintableEdit.brushCenterViewport);
		writer.Write(in paintableEdit.brushSizeViewport, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in paintableEdit.hitPointInLocalSpace);
		writer.WriteVector3(in paintableEdit.cameraPosition);
		writer.Write(in paintableEdit.packedCameraRotation, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in paintableEdit.screenWidth, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in paintableEdit.screenHeight, default(FastBinaryWriter.ForPrimitives));
		writer.Write(paintableEdit.pass);
	}

	public static PaintableEdit ReadPaintableEdit(this FastBinaryReader reader)
	{
		return new PaintableEdit
		{
			brushColor = reader.ReadColor(),
			brushCenterViewport = reader.ReadVector2(),
			brushSizeViewport = reader.ReadSingle(),
			hitPointInLocalSpace = reader.ReadVector3(),
			cameraPosition = reader.ReadVector3(),
			packedCameraRotation = reader.ReadInt32(),
			screenWidth = reader.ReadUInt16(),
			screenHeight = reader.ReadUInt16(),
			pass = reader.ReadByte()
		};
	}

	public static void WriteToolContext(this FastBinaryWriter writer, ToolContext context)
	{
		writer.Write((byte)context.state);
		writer.WriteGameObject(context.target?.gameObject);
		writer.WriteGameObject(context.currentTarget?.gameObject);
		writer.WriteRay(in context.ray);
		writer.WriteRay(in context.currentRay);
		writer.WriteVector3(in context.targetHitPoint);
		writer.WriteVector3(in context.targetHitNormal);
		writer.WriteVector3(in context.currentTargetHitPoint);
		writer.WriteVector3(in context.currentTargetHitNormal);
		writer.Write(in context.deltaTime, default(FastBinaryWriter.ForPrimitives));
	}

	public static ToolContext ReadToolContext(this FastBinaryReader reader)
	{
		return new ToolContext
		{
			state = (ToolState)reader.ReadByte(),
			target = reader.ReadComponent<Interactive>(),
			currentTarget = reader.ReadComponent<Interactive>(),
			ray = reader.ReadRay(),
			currentRay = reader.ReadRay(),
			targetHitPoint = reader.ReadVector3(),
			targetHitNormal = reader.ReadVector3(),
			currentTargetHitPoint = reader.ReadVector3(),
			currentTargetHitNormal = reader.ReadVector3(),
			deltaTime = reader.ReadSingle()
		};
	}

	public static void WriteIReadWrite<T>(this FastBinaryWriter writer, T value) where T : IReadWrite
	{
		if (value == null)
		{
			writer.Write<bool>(true, default(FastBinaryWriter.ForPrimitives));
			return;
		}
		writer.Write<bool>(false, default(FastBinaryWriter.ForPrimitives));
		value.Write(writer);
	}

	public static T ReadIReadWrite<T>(this FastBinaryReader reader) where T : IReadWrite, new()
	{
		if (reader.ReadBoolean())
		{
			return default(T);
		}
		T result = new T();
		result.Read(reader);
		return result;
	}

	public static void WriteSaveable<T>(this FastBinaryWriter writer, T saveable) where T : ISaveable
	{
		saveable.save(writer);
	}

	public static T ReadSaveable<T>(this FastBinaryReader reader) where T : ISaveable, new()
	{
		T result = new T();
		result.load(reader);
		return result;
	}
}
