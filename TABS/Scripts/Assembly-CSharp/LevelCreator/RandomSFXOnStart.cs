using UnityEngine;

namespace LevelCreator
{
	public class RandomSFXOnStart : MonoBehaviour
	{
		public string[] audioClips;

		public float volumeMultiplier = 1f;

		public bool playOnCameraPosition;

		private void Start()
		{
			Vector3 position = base.transform.position;
			if (DMEditor.Instance != null && playOnCameraPosition)
			{
				position = DMEditor.Instance.playerCamera.transform.position;
			}
			Utility.PlaySound(audioClips[Random.Range(0, audioClips.Length)], volumeMultiplier, position);
		}
	}
}
