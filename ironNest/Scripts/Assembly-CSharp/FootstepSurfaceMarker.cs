using UnityEngine;

[DisallowMultipleComponent]
public class FootstepSurfaceMarker : MonoBehaviour
{
	[Tooltip("If enabled, this surface marker is treated as the 'special' surface for footsteps.\nIf disabled, the bridge will ignore this marker and treat the surface as default.\nUse this if you want to temporarily disable special footsteps without removing the component.")]
	public bool isSpecialSurface;

	[Tooltip("Optional name for debugging/logging only. This is not used for routing by default.\nExample: 'Metal', 'Stone', 'ShipDeck'.")]
	public string debugName;
}
