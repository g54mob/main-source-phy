using System.Collections.Generic;
using UnityEngine;

public class CreateLayout : MonoBehaviour
{
	public Transform simpleTogglePrefab;

	public Transform simpleDropDownPrefab;

	public Transform simpleSliderPrefab;

	public List<SimpleMenuToggleContainer> layout;

	public float ySpacing;

	public float xSpacing;

	public Transform[] menuParents;

	public void SetUpMenu()
	{
		float[] array = new float[menuParents.Length];
		for (int i = 0; i < layout.Count; i++)
		{
			Transform transform = Object.Instantiate(simpleTogglePrefab);
			int containingTab = layout[i].containingTab;
			transform.parent = menuParents[containingTab];
			SimpleMenuToggle component = transform.GetComponent<SimpleMenuToggle>();
			component.toggleExtraOption = layout[i].togglePositionInExtraOptionsArray;
			transform.localPosition = new Vector3(0f, array[containingTab], 0f);
			List<SimpleMenuDropDown> list = new List<SimpleMenuDropDown>();
			List<SimpleMenuSlider> list2 = new List<SimpleMenuSlider>();
			for (int j = 0; j < layout[i].dropDownPositionsInExtraOptionsArray.Length; j++)
			{
				Transform transform2 = Object.Instantiate(simpleDropDownPrefab);
				transform2.parent = menuParents[containingTab];
				transform2.localPosition = new Vector3(xSpacing, array[containingTab], 0f);
				SimpleMenuDropDown component2 = transform2.GetComponent<SimpleMenuDropDown>();
				component2.extraOption = layout[i].dropDownPositionsInExtraOptionsArray[j];
				list.Add(component2);
				array[containingTab] -= ySpacing;
			}
			for (int j = 0; j < layout[i].sliderPositionsInExtraOptionsArray.Length; j++)
			{
				Transform transform3 = Object.Instantiate(simpleSliderPrefab);
				transform3.parent = menuParents[containingTab];
				transform3.localPosition = new Vector3(xSpacing, array[containingTab], 0f);
				SimpleMenuSlider component3 = transform3.GetComponent<SimpleMenuSlider>();
				object[] array2 = layout[i].sliderPositionsInExtraOptionsArray[j] as object[];
				component3.floatExtraOption = array2[0] as FloatExtraOption;
				component3.min = float.Parse(array2[1].ToString());
				component3.max = float.Parse(array2[2].ToString());
				list2.Add(component3);
				array[containingTab] -= ySpacing;
			}
			if (array[containingTab] == transform.localPosition.y)
			{
				array[containingTab] -= ySpacing;
			}
			component.simpleMenuDropDowns = list.ToArray();
			component.simpleMenuSliders = list2.ToArray();
		}
	}
}
