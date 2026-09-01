using UnityEngine;

public class OpenExpansionChest : ClickBehaviour
{
	public GameObject[] setActiveOnClick;

	public GameObject[] setActiveOnOpen;

	public Animator chestAnimator;

	public Collider chestCollider;

	private bool wasTriggered;

	private void Awake()
	{
		releaseOnlyOver = true;
	}

	private void OnEnable()
	{
		if (TutorialFileManager.GetTutorialState("WaterExpansion") == 1)
		{
			base.gameObject.SetActive(false);
		}
	}

	public override void OnClickReleased()
	{
		OpenChest();
	}

	public void OpenChest()
	{
		if (!wasTriggered)
		{
			wasTriggered = true;
			chestAnimator.SetTrigger("Open");
			for (int i = 0; i < setActiveOnClick.Length; i++)
			{
				setActiveOnClick[i].transform.position = base.transform.position;
				setActiveOnClick[i].SetActive(true);
			}
			chestCollider.enabled = false;
			TutorialFileManager.SetTutorialState("WaterExpansion", 1);
		}
	}

	public void SpawnParticlesInChest()
	{
		for (int i = 0; i < setActiveOnOpen.Length; i++)
		{
			setActiveOnOpen[i].transform.position = base.transform.position;
			setActiveOnOpen[i].SetActive(true);
		}
	}
}
