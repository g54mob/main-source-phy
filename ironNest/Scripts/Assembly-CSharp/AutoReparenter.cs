using UnityEngine;

public class AutoReparenter : MonoBehaviour
{
	[Header("Reparenting Settings")]
	[Tooltip("Tag of the parent object in the master scene (e.g., your Canvas)")]
	[TagDropdown]
	public string masterParentTag;

	[Tooltip("Tag of mission objects to be reparented (e.g., Targets)")]
	[TagDropdown]
	public string missionObjectTag;

	private void Awake()
	{
	}

	private void ReparentMissionObjects()
	{
	}
}
