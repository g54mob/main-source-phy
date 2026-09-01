namespace Poly.Determinism
{
	public enum EventType
	{
		Invalid = 0,
		Awake = 1,
		Start = 2,
		OnEnable = 3,
		OnDisable = 4,
		OnDestroy = 5,
		AddToWorld = 6,
		NodeSplit = 7,
		EdgeBreak = 8,
		PickUpCheckpoint = 9,
		EventTimelineStartSimulation = 10,
		WaterDrag = 11,
		LevelFail = 12,
		AllEnginesStop = 13
	}
}
