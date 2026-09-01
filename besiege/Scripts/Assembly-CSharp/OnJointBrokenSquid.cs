using UnityEngine;

public class OnJointBrokenSquid : MonoBehaviour
{
	public RandomSoundController randSoundController;

	public GameObject JoinSkin;

	public GameObject Blood;

	public GameObject BloodPos;

	private void OnJointBreak()
	{
		JoinSkin.SetActive(false);
		Object.Instantiate(Blood, BloodPos.transform);
	}
}
