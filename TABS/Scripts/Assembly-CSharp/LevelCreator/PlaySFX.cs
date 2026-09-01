using UnityEngine;

namespace LevelCreator
{
	public class PlaySFX : MonoBehaviour
	{
		private Transform playerTransform;

		private void Start()
		{
			playerTransform = DMEditor.Instance.playerCamera.transform;
		}

		public void Play(string soundRef)
		{
			Utility.PlaySound(soundRef, 1f, playerTransform.position);
		}
	}
}
