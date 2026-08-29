using UnityEngine;

public static class PirateDarkest3LogicBinaryExtensions
{
	public static void WriteTrailGunpowderDarkest(this FastBinaryWriter writer, PirateDarkest3Logic.TrailGunpowderDarkest data)
	{
		writer.WriteComponent(data.trackable);
		writer.WriteComponent(data.renderer);
		writer.WriteComponent(data.dissolve);
		writer.WriteGameObject(data.trail);
		writer.WriteGameObject(data.start);
		writer.WriteGameObject(data.end);
		writer.WriteGameObject(data.particles);
		writer.WriteComponent(data.tween);
		writer.WriteComponent(data.platformTween);
		writer.Write(in data.platformTweenGoal, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.state, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in data.fireFromStart, default(FastBinaryWriter.ForPrimitives));
	}

	public static PirateDarkest3Logic.TrailGunpowderDarkest ReadTrailGunpowderDarkest(this FastBinaryReader reader)
	{
		return new PirateDarkest3Logic.TrailGunpowderDarkest
		{
			trackable = reader.ReadComponent<Trackable>(),
			renderer = reader.ReadComponent<Renderer>(),
			dissolve = reader.ReadComponent<MaterialState>(),
			trail = reader.ReadGameObject(),
			start = reader.ReadGameObject(),
			end = reader.ReadGameObject(),
			particles = reader.ReadGameObject(),
			tween = reader.ReadComponent<TweenState>(),
			platformTween = reader.ReadComponent<TweenState>(),
			platformTweenGoal = reader.ReadInt32(),
			state = reader.ReadBoolean(),
			fireFromStart = reader.ReadBoolean()
		};
	}
}
