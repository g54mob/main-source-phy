using System;
using UnityEngine;

[Serializable]
public class DeckSpawnEntry
{
	[Tooltip("Human-readable label used in debug logs.")]
	public string Label;

	[Tooltip("Specific prefab to instantiate for this entry.\nIf null, the deck area's fallback prefab is used.")]
	public DraggableItem PrefabOverride;

	[Tooltip("Callback invoked with the spawned DraggableItem immediately after instantiation.\nUse this to initialise domain-specific components (e.g. PunchcardRuntime.Initialize).")]
	public Action<DraggableItem> OnSpawned;
}
