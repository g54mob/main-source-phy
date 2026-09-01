using UnityEngine;

[AddComponentMenu("Physics/Trigger Set Joint (Base)")]
public class TriggerSetJointBase : MonoBehaviour
{
	public const int layerToCheck = 12;

	public const int layerToCheck2 = 14;

	public int Index;

	public bool createLinks = true;

	public bool isDynamicLink;

	public bool canJoinMultiple = true;
}
