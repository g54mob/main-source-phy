using UnityEngine;

public static class Pirate1LogicBinaryExtensions
{
	public static void WriteGemPointList(this FastBinaryWriter writer, Pirate1Logic.GemPointList data)
	{
		writer.WriteList(data.points, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
	}

	public static Pirate1Logic.GemPointList ReadGemPointList(this FastBinaryReader reader)
	{
		return new Pirate1Logic.GemPointList
		{
			points = reader.ReadList((FastBinaryReader r) => r.ReadGameObject())
		};
	}

	public static void WriteDrinkCombination(this FastBinaryWriter writer, Pirate1Logic.DrinkCombination data)
	{
		int value = (int)data.Item1;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
		value = (int)data.Item2;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
	}

	public static Pirate1Logic.DrinkCombination ReadDrinkCombination(this FastBinaryReader reader)
	{
		return new Pirate1Logic.DrinkCombination
		{
			Item1 = (Pirate1Logic.Drink)reader.ReadInt32(),
			Item2 = (Pirate1Logic.Drink)reader.ReadInt32()
		};
	}
}
