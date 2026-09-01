using Landfall.TABC;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoardObject : MonoBehaviour
{
	public Image icon;

	public Image bar;

	public TextMeshProUGUI nameText;

	public TextMeshProUGUI valueText;

	public GameObject[] stars;

	public TABCUnitUI unitUI;

	private float targetValue;

	private float currentValue;

	private float targetMax;

	private float currentMax;

	internal void Init(UnitData unitData)
	{
		nameText.text = unitData.dataInstance.unit.unitBlueprint.Entity.Name;
		for (int i = 0; i < unitData.dataInstance.level; i++)
		{
			if ((bool)stars[i])
			{
				stars[i].SetActive(value: true);
			}
			stars[i].transform.GetChild(0).GetComponent<Image>().color = unitUI.m_LevelColors[unitData.dataInstance.level - 1];
		}
		GetComponentInChildren<LerpFollowParent>().Go(base.gameObject);
		unitData.dataInstance.unit.unitBlueprint.Entity.GetSpriteIconAsync(delegate(Sprite sprite)
		{
			if (sprite != null && icon != null)
			{
				icon.sprite = sprite;
			}
		});
	}

	public void UpdateInfo(float value, float maxValue, int positionOnScoreBoard)
	{
		targetMax = maxValue;
		targetValue = value;
		if (positionOnScoreBoard != -1)
		{
			base.transform.SetSiblingIndex(positionOnScoreBoard);
		}
	}

	public void Remove()
	{
	}

	private void Update()
	{
		currentMax = Mathf.Lerp(currentMax, targetMax, Time.deltaTime * 5f);
		currentValue = Mathf.Lerp(currentValue, targetValue, Time.deltaTime * 5f);
		if ((bool)valueText)
		{
			valueText.text = currentValue.ToString("F0");
		}
		bar.fillAmount = currentValue / currentMax;
	}
}
