public static class Pirate4LogicBinaryExtensions
{
	public static void WriteSnake(this FastBinaryWriter writer, Pirate4Logic.Snake data)
	{
		writer.Write(in data.hasHead, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.hasTail, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(data.parts, delegate(FastBinaryWriter w, Pirate4Logic.SnakePart e)
		{
			w.WriteSnakePart(e);
		});
	}

	public static Pirate4Logic.Snake ReadSnake(this FastBinaryReader reader)
	{
		return new Pirate4Logic.Snake
		{
			hasHead = reader.ReadBoolean(),
			hasTail = reader.ReadBoolean(),
			parts = reader.ReadList((FastBinaryReader r) => r.ReadSnakePart())
		};
	}

	public static void WriteRopeCollisionData(this FastBinaryWriter writer, Pirate4Logic.RopeCollisionData data)
	{
		writer.WriteGameObject(data.hitObject);
		writer.Write(in data.dirSign, default(FastBinaryWriter.ForPrimitives));
		writer.WriteVector3(in data.bendingPoint);
		writer.Write(in data.prevAngle, default(FastBinaryWriter.ForPrimitives));
	}

	public static Pirate4Logic.RopeCollisionData ReadRopeCollisionData(this FastBinaryReader reader)
	{
		return new Pirate4Logic.RopeCollisionData
		{
			hitObject = reader.ReadGameObject(),
			dirSign = reader.ReadInt32(),
			bendingPoint = reader.ReadVector3(),
			prevAngle = reader.ReadSingle()
		};
	}

	public static void WriteSnakePart(this FastBinaryWriter writer, Pirate4Logic.SnakePart data)
	{
		writer.Write(in data.id, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.isFixed, default(FastBinaryWriter.ForPrimitives));
	}

	public static Pirate4Logic.SnakePart ReadSnakePart(this FastBinaryReader reader)
	{
		return new Pirate4Logic.SnakePart
		{
			id = reader.ReadInt32(),
			isFixed = reader.ReadBoolean()
		};
	}
}
