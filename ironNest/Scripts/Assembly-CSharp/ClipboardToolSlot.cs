using UnityEngine;

[AddComponentMenu("Gameplay/Clipboard/Clipboard Tool Slot")]
public class ClipboardToolSlot : MonoBehaviour
{
	[Header("Marker Mapping")]
	[Tooltip("Marker prefab that should become active in MapMarkerPlacer when this tool is selected.\n\nRequirements:\n- Should be a UI prefab suitable for parenting under MapMarkerPlacer.mapCanvas.\n- This is the preferred mapping (do NOT rely on index ordering).\n\nBehavior:\n- If null, selecting this tool will not change the active marker prefab.")]
	public GameObject markerPrefab;

	[Header("Rest Pose (Unique Per Tool)")]
	[Tooltip("Transform that defines this tool's unselected/rest local pose.\n\nRecommended (prefab-friendly):\n- Create a child empty GameObject (e.g. 'RestPose') positioned/rotated where this tool should sit when not selected.\n- Assign it here.\n\nFallback behavior:\n- If not assigned and 'Capture Rest Pose On Awake' is true, this slot captures its own current local position/rotation/scale as rest pose at Awake.")]
	public Transform restPose;

	[Tooltip("If true and Rest Pose is not assigned, this slot will capture its current local position/rotation/scale on Awake and use that as the rest pose.\n\nSafe default: true.\nDisable if you want rest pose to be purely authored via a RestPose transform.")]
	public bool captureRestPoseOnAwake;

	[Header("Hover Visuals (Optional)")]
	[Tooltip("GameObjects that will be SetActive(true) when this tool is hovered.\n\nUse for:\n- Outline meshes\n- Glow meshes\n- Tooltip meshes\n\nNotes:\n- Leave empty if not needed.")]
	public GameObject[] hoverVisualsOn;

	[Tooltip("GameObjects that will be SetActive(false) when this tool is hovered.\n\nUse for:\n- Turning off 'idle' visuals while hovering (if desired)\n\nNotes:\n- Leave empty if not needed.")]
	public GameObject[] hoverVisualsOff;

	[Header("Selected Visuals (Optional)")]
	[Tooltip("GameObjects that will be SetActive(true) when this tool is selected.\n\nUse for:\n- Selected outline\n- Emissive mesh\n- 'Selected' decal\n\nNotes:\n- Leave empty if not needed.")]
	public GameObject[] selectedVisualsOn;

	[Tooltip("GameObjects that will be SetActive(false) when this tool is selected.\n\nUse for:\n- Disabling idle visuals while selected\n\nNotes:\n- Leave empty if not needed.")]
	public GameObject[] selectedVisualsOff;

	[Header("Cursor Override While Selected (Optional)")]
	[Tooltip("If true, selecting this tool requests a cursor override to be applied to the map Interactable via InteractableRuntimeCursorOverride.\n\nNotes:\n- Cursor hotspots are not supported. Cursor textures are always centered on the VirtualCursor position.\n- If false, selection of this tool will not change the map cursor override.")]
	public bool overrideMapCursorWhileSelected;

	[Tooltip("Cursor texture to request while this tool is selected.\n\nUsed only if 'Override Map Cursor While Selected' is true.\n\nNotes:\n- If null, the override request will effectively clear/disable the override (provider-dependent).\n- Cursor textures are always centered; no hotspot is used.")]
	public Texture2D selectedCursorTexture;

	[Header("Diagnostics")]
	[Tooltip("If true, emits debug logs for hover/selection visual changes and pose capture.\n\nSafe to leave off in production.")]
	public bool debugLogs;

	public Vector3 CapturedRestLocalPosition { get; private set; }

	public Quaternion CapturedRestLocalRotation { get; private set; }

	public Vector3 CapturedRestLocalScale { get; private set; }

	public bool HasRestPoseTransform => false;

	private void Awake()
	{
	}

	public void ApplyHover(bool hovered)
	{
	}

	public void ApplySelected(bool selected)
	{
	}

	public void GetRestPose(out Vector3 localPos, out Quaternion localRot, out Vector3 localScale)
	{
		localPos = default(Vector3);
		localRot = default(Quaternion);
		localScale = default(Vector3);
	}

	private static void SetActiveBatch(GameObject[] gos, bool active)
	{
	}
}
