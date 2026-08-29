using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RingToggle : MonoBehaviour
{
	public string[] values = new string[0];

	public int selected;

	private List<Text> textFields = new List<Text>();

	public Color active;

	public Color inactive;

	public Action<int> onChange;

	private void Awake()
	{
		syncValues();
		GetComponent<Button>().onClick.AddListener(onClick);
	}

	private void onClick()
	{
		if (values.Length != 0)
		{
			selected = (selected + 1) % values.Length;
			for (int i = 0; i < values.Length; i++)
			{
				textFields[i].color = ((selected == i) ? active : inactive);
			}
			if (onChange != null)
			{
				onChange(selected);
			}
		}
	}

	public void syncValues()
	{
		while (base.transform.childCount > 1)
		{
			UnityEngine.Object.DestroyImmediate(base.transform.GetChild(1).gameObject);
		}
		textFields.Clear();
		GameObject gameObject = base.transform.GetChild(0).gameObject;
		for (int i = 0; i < values.Length; i++)
		{
			if (i > 0)
			{
				GameObject obj = UnityEngine.Object.Instantiate(gameObject, gameObject.transform.parent);
				obj.SetActive(value: true);
				Text component = obj.GetComponent<Text>();
				component.text = "/";
				component.color = active;
			}
			GameObject obj2 = UnityEngine.Object.Instantiate(gameObject, gameObject.transform.parent);
			obj2.SetActive(value: true);
			Text component2 = obj2.GetComponent<Text>();
			component2.text = Localization.translate(values[i]);
			component2.color = ((i == selected) ? active : inactive);
			textFields.Add(component2);
		}
	}
}
