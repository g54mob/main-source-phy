using UnityEngine;

namespace MinimalVolumeCulling
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Collider))]
	public sealed class CullZone : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("Unique identifier for this zone.\n\nUsed by CameraCullingProfile to enable/disable specific zones.\n\nFormat rules:\n- Case-insensitive comparisons.\n- Leading/trailing whitespace is ignored.\n- Keep names stable for designer workflow.\n\nSafe examples:\n- Barbet_All\n- Barbet_Near")]
		private string zoneId;

		[SerializeField]
		[Tooltip("If enabled, this CullZone is considered active by default.\n\nIn this system, Camera volumes typically decide which zones are active.\nSo a safe default is disabled.\n\nSafe default: disabled.")]
		private bool activeByDefault;

		[SerializeField]
		[Tooltip("If enabled, the collider on this object is forced to be a trigger.\n\nSafe default: enabled.\n\nWhy:\n- CullZones are meant to be trigger volumes.\n- This reduces accidental physics interactions.")]
		private bool forceTrigger;

		private Collider _collider;

		public string ZoneId => null;

		public bool ActiveByDefault => false;

		public Collider ZoneCollider => null;

		private void Reset()
		{
		}

		private void OnValidate()
		{
		}
	}
}
