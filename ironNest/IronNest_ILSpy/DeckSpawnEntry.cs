using System;

[Serializable]
public class DeckSpawnEntry
{
	public string Label;

	public DraggableItem PrefabOverride;

	public Action<DraggableItem> OnSpawned;
}
