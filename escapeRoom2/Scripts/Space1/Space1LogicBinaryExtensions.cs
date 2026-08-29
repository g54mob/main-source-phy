using UnityEngine;

public static class Space1LogicBinaryExtensions
{
	public static void WriteTriangleKeypad(this FastBinaryWriter writer, Space1Logic.TriangleKeypad data)
	{
		writer.WriteList(data.solution, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteList(data.current, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray(data.buttons, delegate(FastBinaryWriter w, Switch3D e)
		{
			w.WriteComponent(e);
		});
		writer.WriteList(data.screens, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.WriteComponent(data.door);
		writer.WriteComponent(data.zoom);
		writer.WriteArray(data.items, delegate(FastBinaryWriter w, Item e)
		{
			w.WriteComponent(e);
		});
		writer.Write(in data.solved, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.count, default(FastBinaryWriter.ForPrimitives));
	}

	public static Space1Logic.TriangleKeypad ReadTriangleKeypad(this FastBinaryReader reader)
	{
		return new Space1Logic.TriangleKeypad
		{
			solution = reader.ReadList((FastBinaryReader r) => r.ReadInt32()),
			current = reader.ReadList((FastBinaryReader r) => r.ReadInt32()),
			buttons = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<Switch3D>()),
			screens = reader.ReadList((FastBinaryReader r) => r.ReadGameObject()),
			door = reader.ReadComponent<Switch3D>(),
			zoom = reader.ReadComponent<Zoomable>(),
			items = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<Item>()),
			solved = reader.ReadBoolean(),
			count = reader.ReadInt32()
		};
	}

	public static void WriteSplicedLogState(this FastBinaryWriter writer, Space1Logic.SplicedLogState data)
	{
		writer.Write(data.word);
		writer.Write(in data.logIndex, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.logWidth, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.splicedLogIndex, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.startTime, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.endTime, default(FastBinaryWriter.ForPrimitives));
		writer.WriteComponent(data.button);
	}

	public static Space1Logic.SplicedLogState ReadSplicedLogState(this FastBinaryReader reader)
	{
		return new Space1Logic.SplicedLogState
		{
			word = reader.ReadString(),
			logIndex = reader.ReadInt32(),
			logWidth = reader.ReadSingle(),
			splicedLogIndex = reader.ReadInt32(),
			startTime = reader.ReadSingle(),
			endTime = reader.ReadSingle(),
			button = reader.ReadComponent<Switch3D>()
		};
	}

	public static void WriteCutMeshParameter(this FastBinaryWriter writer, Space1Logic.CutMeshParameter data)
	{
		writer.Write(in data.splicedLogIndex, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.middleLogIndex, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.rightCutValue, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.leftCutValue, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.nextLogScale, default(FastBinaryWriter.ForPrimitives));
	}

	public static Space1Logic.CutMeshParameter ReadCutMeshParameter(this FastBinaryReader reader)
	{
		return new Space1Logic.CutMeshParameter
		{
			splicedLogIndex = reader.ReadInt32(),
			middleLogIndex = reader.ReadInt32(),
			rightCutValue = reader.ReadSingle(),
			leftCutValue = reader.ReadSingle(),
			nextLogScale = reader.ReadSingle()
		};
	}

	public static void WriteCratePuzzleSegment(this FastBinaryWriter writer, Space1Logic.CratePuzzleSegment data)
	{
		writer.WriteGameObject(data.segment);
		writer.WriteComponent(data.segmentTransform);
		writer.Write(in data.row, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.column, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.rotation, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in data.startingLocalPosition);
		writer.WriteQuaternion(in data.startingLocalRotation);
		writer.Write(in data.startingRow, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.startingColumn, default(FastBinaryWriter.ForPrimitives));
	}

	public static Space1Logic.CratePuzzleSegment ReadCratePuzzleSegment(this FastBinaryReader reader)
	{
		return new Space1Logic.CratePuzzleSegment
		{
			segment = reader.ReadGameObject(),
			segmentTransform = reader.ReadComponent<Transform>(),
			row = reader.ReadInt32(),
			column = reader.ReadInt32(),
			rotation = reader.ReadSingle(),
			startingLocalPosition = reader.ReadVector3(),
			startingLocalRotation = reader.ReadQuaternion(),
			startingRow = reader.ReadInt32(),
			startingColumn = reader.ReadInt32()
		};
	}

	public static void WriteCratePuzzleInstruction(this FastBinaryWriter writer, Space1Logic.CratePuzzleInstruction data)
	{
		writer.WriteComponent(data.piece);
		writer.WriteComponent(data.swappedEmpty);
		writer.Write(in data.row, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.column, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.move, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.rotate, default(FastBinaryWriter.ForPrimitives));
	}

	public static Space1Logic.CratePuzzleInstruction ReadCratePuzzleInstruction(this FastBinaryReader reader)
	{
		return new Space1Logic.CratePuzzleInstruction
		{
			piece = reader.ReadComponent<SwapperPiece>(),
			swappedEmpty = reader.ReadComponent<SwapperPiece>(),
			row = reader.ReadInt32(),
			column = reader.ReadInt32(),
			move = reader.ReadInt32(),
			rotate = reader.ReadInt32()
		};
	}
}
