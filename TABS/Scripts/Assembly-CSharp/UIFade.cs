using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIFade : MonoBehaviour
{
	public bool isVisible = true;

	public float target = 1f;

	public float speed = 5f;

	private float currentA;

	private List<FadeObject> fades = new List<FadeObject>();

	public void UpdateList()
	{
		Image[] componentsInChildren = GetComponentsInChildren<Image>();
		TextMeshProUGUI[] componentsInChildren2 = GetComponentsInChildren<TextMeshProUGUI>();
		fades.Clear();
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			FadeObject fadeObject = new FadeObject();
			fadeObject.image = componentsInChildren[i];
			FadeMultiplier component = componentsInChildren[i].GetComponent<FadeMultiplier>();
			if ((bool)component)
			{
				fadeObject.multiplier = component.multiplier * component.baseMultiplier;
			}
			fades.Add(fadeObject);
		}
		for (int j = 0; j < componentsInChildren2.Length; j++)
		{
			FadeObject fadeObject2 = new FadeObject();
			fadeObject2.textMesh = componentsInChildren2[j];
			FadeMultiplier component2 = componentsInChildren2[j].GetComponent<FadeMultiplier>();
			if ((bool)component2)
			{
				fadeObject2.multiplier = component2.multiplier * component2.baseMultiplier;
			}
			fades.Add(fadeObject2);
		}
	}

	private void Start()
	{
		UpdateList();
		currentA = target;
	}

	private void Update()
	{
		if (isVisible)
		{
			currentA = Mathf.Lerp(currentA, target, speed * Time.deltaTime);
		}
		else
		{
			currentA = Mathf.Lerp(currentA, 0f, speed * Time.deltaTime);
		}
		for (int i = 0; i < fades.Count; i++)
		{
			if ((bool)fades[i].image)
			{
				Color color = fades[i].image.color;
				color.a = currentA * fades[i].multiplier;
				fades[i].image.color = color;
			}
			if ((bool)fades[i].textMesh)
			{
				Color color2 = fades[i].textMesh.color;
				color2.a = currentA * fades[i].multiplier;
				fades[i].textMesh.color = color2;
			}
		}
	}
}
