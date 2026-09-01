using UnityEngine;

public class SwitchViewedObject : ClickBehaviour
{
	public static GameObject[] objectList;

	public static int currentActive;

	public int increase;

	public GameObject[] setObjectListOnStart;

	private void Awake()
	{
		if (setObjectListOnStart.Length != 0)
		{
			objectList = setObjectListOnStart;
		}
	}

	private void Start()
	{
		currentActive %= objectList.Length;
		for (int i = 0; i < setObjectListOnStart.Length; i++)
		{
			objectList[i].SetActive(i == currentActive);
		}
	}

	public override void OnClicked()
	{
		currentActive = (currentActive + objectList.Length + increase) % objectList.Length;
		for (int i = 0; i < objectList.Length; i++)
		{
			objectList[i].SetActive(i == currentActive);
		}
	}
}
