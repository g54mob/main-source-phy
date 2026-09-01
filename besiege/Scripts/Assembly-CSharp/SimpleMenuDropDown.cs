using System.Collections.Generic;
using UnityEngine;

public class SimpleMenuDropDown : MonoBehaviour, ICanBeReset
{
	public ExtraOption extraOption;

	public Transform starPrefab;

	public Shader particlesAdditive;

	public Shader particlesMultiplyColored;

	public TextMesh textMesh;

	public float arrowStartX;

	public float arrowSpacerX = 3f;

	public float arrowStartY = -0.6f;

	public float arrowStartZ = -0.1f;

	public List<Renderer> starList;

	public int currentlySelected;

	public int options = -1;

	public DropDownStarBehaviour[] arrows;

	private void Start()
	{
		options = extraOption.arguments.Length;
		for (int i = 0; i < options; i++)
		{
			Transform transform = Object.Instantiate(starPrefab);
			transform.parent = base.transform;
			transform.localPosition = new Vector3(arrowStartX + arrowSpacerX * (float)(i + 1) / (float)(options + 1), arrowStartY, arrowStartZ);
			starList.Add(transform.GetComponent<Renderer>());
			DropDownStarBehaviour component = transform.GetComponent<DropDownStarBehaviour>();
			component.extraOption = extraOption;
			component.myValue = extraOption.arguments[i].ToString();
		}
		arrows[0].extraOption = extraOption;
		arrows[1].extraOption = extraOption;
		SetGraphics();
	}

	private void SetGraphics()
	{
		currentlySelected = (int)extraOption.parsedValue;
		textMesh.text = extraOption.currentName;
		for (int i = 0; i < options; i++)
		{
			if (extraOption.parsedValue == extraOption.arguments[i])
			{
				starList[i].material.shader = particlesAdditive;
				arrows[1].myValue = extraOption.arguments[(i + 1) % options].ToString();
				arrows[0].myValue = extraOption.arguments[(i + options - 1) % options].ToString();
			}
			else
			{
				starList[i].material.shader = particlesMultiplyColored;
			}
		}
	}

	public object GetValue()
	{
		return extraOption.parsedValue;
	}

	public void SetValue(string newValue)
	{
		extraOption.SetValue(newValue);
		SetGraphics();
	}

	private void Update()
	{
		int num = (int)extraOption.parsedValue;
		if (currentlySelected != num)
		{
			SetGraphics();
		}
	}

	public void Reset()
	{
		extraOption.Reset();
	}
}
