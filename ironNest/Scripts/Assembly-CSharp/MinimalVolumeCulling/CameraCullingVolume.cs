using UnityEngine;

namespace MinimalVolumeCulling
{
	[DisallowMultipleComponent]
	[RequireComponent(typeof(Collider))]
	public sealed class CameraCullingVolume : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("Priority used when multiple CameraCullingVolumes overlap the camera at the same time.\n\nHigher numbers win.\n\nUse this to resolve overlaps cleanly.\n\nSafe example:\n- Back volume priority 100\n- Center volume priority 50")]
		private int priority;

		[SerializeField]
		[Tooltip("Profile ID to activate when the camera is inside this volume.\n\nThis string must match a Profile ID in the CullingBrain 'Profiles' list.\n\nMatching rules:\n- Case-insensitive.\n- Leading/trailing whitespace ignored.\n- Empty = selects no profile (brain will fall back to CullZones that are ActiveByDefault).\n\nSupported tokens/codes: none.\n\nSafe examples:\n- BackOfTurret\n- CenterTurret")]
		private string profileId;

		[SerializeField]
		[Tooltip("If enabled, this volume's collider is forced to be a trigger.\n\nSafe default: enabled.\n\nWhy:\n- CameraCullingVolumes are meant to be trigger volumes.\n- This reduces accidental physics interactions.")]
		private bool forceTrigger;

		private Collider _collider;

		public int Priority => 0;

		public string ProfileId => null;

		public Collider VolumeCollider => null;

		private void Reset()
		{
		}

		private void OnValidate()
		{
		}
	}
}
