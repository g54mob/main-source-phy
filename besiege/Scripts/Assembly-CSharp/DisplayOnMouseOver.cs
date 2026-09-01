using UnityEngine;

public class DisplayOnMouseOver : MonoBehaviour
{
	public Renderer[] rensToDisplay;

	public GameObject[] objsToHide;

	public int mask = -1;

	private bool active = true;

	private void Awake()
	{
	}

	private void OnMouseEnter()
	{
		if (UIMask.InsideMask(mask, base.transform.position) && active)
		{
			for (int i = 0; i < rensToDisplay.Length; i++)
			{
				rensToDisplay[i].enabled = true;
			}
			for (int j = 0; j < objsToHide.Length; j++)
			{
				objsToHide[j].SetActive(false);
			}
		}
	}

	private void OnMouseExit()
	{
		if (active)
		{
			for (int i = 0; i < rensToDisplay.Length; i++)
			{
				rensToDisplay[i].enabled = false;
			}
			for (int j = 0; j < objsToHide.Length; j++)
			{
				objsToHide[j].SetActive(true);
			}
		}
	}

	private void OnDisable()
	{
		for (int i = 0; i < rensToDisplay.Length; i++)
		{
			rensToDisplay[i].enabled = false;
		}
		for (int j = 0; j < objsToHide.Length; j++)
		{
			objsToHide[j].SetActive(true);
		}
	}

	private void SetEnabledMsg(bool enabled)
	{
		OnMouseExit();
		active = enabled;
	}
}
