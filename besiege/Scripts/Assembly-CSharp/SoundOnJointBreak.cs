using UnityEngine;

public class SoundOnJointBreak : MonoBehaviour
{
	public RandomSoundController soundController;

	private void OnJointBreak()
	{
		soundController.Play();
	}
}
