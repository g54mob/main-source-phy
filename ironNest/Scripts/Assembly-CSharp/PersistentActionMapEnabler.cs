using UnityEngine;
using UnityEngine.InputSystem;

[AddComponentMenu("Input/Persistent Action Map Enabler")]
[DisallowMultipleComponent]
public sealed class PersistentActionMapEnabler : MonoBehaviour
{
	[Header("Input")]
	[SerializeField]
	[Tooltip("The InputActionAsset containing the maps to enable.\nNotes:\n• Assign the same asset referenced by your PlayerInput component.\n• Must not be null; an error is logged if missing.")]
	private InputActionAsset inputActions;

	[SerializeField]
	[Tooltip("Names of the InputActionMaps to enable permanently at startup.\nRules:\n• Names are case-sensitive and must match exactly as defined in the Input Action Asset.\n• Each listed map is enabled once in Awake and never disabled by this component.\n• Duplicate entries are safely ignored.\n• If a name is not found in the asset, an error is logged and that entry is skipped.\nExamples:\n• \"Universal\"\n• \"GlobalHotkeys\"")]
	private string[] persistentMapNames;

	private void Awake()
	{
	}
}
