using System;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionDropdownBox : MonoBehaviour
{
	[NonSerialized]
	public List<GameObject> panelList = new List<GameObject>();

	public GameObject panelBase;

	private Vector3 baseScale = new Vector3(0.6180995f, 0.8766027f, 0.4944795f);

	private void Start()
	{
		panelList.Clear();
		panelBase.SetActive(true);
		Resolution[] resolutions = Screen.resolutions;
		int num = resolutions.Length / 10;
		if (resolutions.Length % 10 != 0)
		{
			num++;
		}
		float num2 = (num - 1) / 2;
		Vector3 position = panelBase.transform.position;
		for (int i = 0; i < num; i++)
		{
			panelList.Add(UnityEngine.Object.Instantiate(panelBase));
		}
		for (int j = 0; j < num; j++)
		{
			panelList[j].transform.position = new Vector3(position.x + (float)j - num2, position.y, position.z);
			panelList[j].transform.localScale = baseScale;
			panelList[j].transform.parent = base.transform;
		}
		for (int k = 0; k < panelList.Count; k++)
		{
			panelList[k].GetComponent<ResolutionPanel>().Set(resolutions, k);
		}
		panelBase.SetActive(false);
		base.gameObject.SetActive(false);
	}
}
