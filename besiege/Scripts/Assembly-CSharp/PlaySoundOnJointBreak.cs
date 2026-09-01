using UnityEngine;

public class PlaySoundOnJointBreak : MonoBehaviour
{
	public RandomSoundController randSoundController;

	private void OnJointBreak()
	{
		randSoundController.Play();
	}
}
