public static class SurfaceSlotBinaryExtensions
{
	public static void WritePlacement(this FastBinaryWriter writer, SurfaceSlot.Placement data)
	{
	}

	public static SurfaceSlot.Placement ReadPlacement(this FastBinaryReader reader)
	{
		return new SurfaceSlot.Placement();
	}
}
