public static class Pirate3LogicBinaryExtensions
{
	public static void WriteTrailGunpowder(this FastBinaryWriter writer, Pirate3Logic.TrailGunpowder data)
	{
	}

	public static Pirate3Logic.TrailGunpowder ReadTrailGunpowder(this FastBinaryReader reader)
	{
		return new Pirate3Logic.TrailGunpowder();
	}

	public static void WriteFishingCatchCollection(this FastBinaryWriter writer, Pirate3Logic.FishingCatchCollection data)
	{
		int value = (int)data.waterCatchType;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		value = (int)data.baitType;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		writer.WriteList(data.catches, delegate(FastBinaryWriter w, Item e)
		{
			w.WriteComponent(e);
		});
	}

	public static Pirate3Logic.FishingCatchCollection ReadFishingCatchCollection(this FastBinaryReader reader)
	{
		return new Pirate3Logic.FishingCatchCollection
		{
			waterCatchType = (Pirate3Logic.FishingWaterCatchType)reader.ReadInt32(),
			baitType = (Pirate3Logic.FishingBaitType)reader.ReadInt32(),
			catches = reader.ReadList((FastBinaryReader r) => r.ReadComponent<Item>())
		};
	}
}
