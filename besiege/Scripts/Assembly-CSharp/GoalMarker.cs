using System;
using Localisation;
using UnityEngine;

public class GoalMarker : GenericEntity, ILocalisationAware
{
	private class GoalLine
	{
		public MText text;

		public MSlider size;
	}

	public Transform leftBracket;

	public Transform rightBracket;

	public GameObject oneLineGO;

	public GameObject twoLineGO;

	public GameObject threeLineGO;

	public TextMesh[] OneLine;

	public TextMesh[] TwoLines;

	public TextMesh[] ThreeLines;

	public Collider GoalCollider;

	public FadeInOnSimulate fadeOnSimulate;

	private MSlider bracketSlider;

	private MToggle hideInSim;

	private GoalLine[] goalLines;

	public override void Init()
	{
		if (!isInitialized)
		{
			bracketSlider = AddSlider(2505, "marker-size", 5f, 2f, 20f, string.Empty);
			hideInSim = AddToggle(3020, "sim-hide", true);
			hideInSim.Toggled += OnToggleHide;
			int num = 3;
			goalLines = new GoalLine[num];
			for (int i = 0; i < num; i++)
			{
				GoalLine goalLine = new GoalLine();
				AddGoalLineTexts(goalLine, i);
				goalLine.text.TextChanged += SetText;
				goalLine.size.ValueChanged += SetTextSize;
				goalLines[i] = goalLine;
			}
			base.Init();
			UpdateText();
			SetBracketSize(bracketSlider.Value);
			ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggle));
			bracketSlider.ValueChanged += SetBracketSize;
		}
	}

	private void AddGoalLineTexts(GoalLine goalLine, int index)
	{
		goalLine.text = AddText(string.Format(LocalisationManager.GetTranslation(2425), index), "textline" + index, (index != 0) ? string.Empty : LocalisationManager.GetTranslation(3293));
		goalLine.size = AddSlider(string.Format(LocalisationManager.GetTranslation(2426), index), "textsize" + index, 80f, 20f, 180f, string.Empty);
	}

	private void SetGoalLineTexts(GoalLine goalLine, int index)
	{
		goalLine.text.DisplayName = string.Format(LocalisationManager.GetTranslation(2425), index);
		if (index == 0)
		{
			goalLine.text.SetDefaultText(LocalisationManager.GetTranslation(3293));
		}
		goalLine.size.DisplayName = string.Format(LocalisationManager.GetTranslation(2426), index);
	}

	private void OnToggleHide(bool toggle)
	{
		fadeOnSimulate.Toggle(toggle);
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLevelSimulation, new Action<bool>(OnSimulationToggle));
	}

	private void OnSimulationToggle(bool toggle)
	{
		GoalCollider.enabled = !toggle;
	}

	public override void OnAdd()
	{
		base.OnAdd();
		fadeOnSimulate.SetAllRenderersOff();
	}

	private void SetText(string line)
	{
		UpdateText();
	}

	private void SetTextSize(float size)
	{
		UpdateText();
	}

	private void UpdateText()
	{
		string value = goalLines[1].text.Value;
		string value2 = goalLines[2].text.Value;
		int num = ((string.IsNullOrEmpty(value) && string.IsNullOrEmpty(value2)) ? 1 : ((!string.IsNullOrEmpty(value2)) ? 3 : 2));
		oneLineGO.SetActive(num == 1);
		twoLineGO.SetActive(num == 2);
		threeLineGO.SetActive(num == 3);
		float a = 0.65f;
		TextMesh[] textMeshes = fadeOnSimulate.textMeshes;
		fadeOnSimulate.textMeshes = new TextMesh[2 + num];
		TextMesh textMesh = textMeshes[0];
		TextMesh textMesh2 = textMeshes[1];
		textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, a);
		textMesh2.color = new Color(textMesh2.color.r, textMesh2.color.g, textMesh2.color.b, a);
		fadeOnSimulate.textMeshes[0] = textMesh;
		fadeOnSimulate.textMeshes[1] = textMesh2;
		TextMesh[] array;
		switch (num)
		{
		case 1:
			array = OneLine;
			break;
		case 2:
			array = TwoLines;
			break;
		default:
			array = ThreeLines;
			break;
		}
		TextMesh[] array2 = array;
		for (int i = 0; i < num; i++)
		{
			TextMesh currentMesh = array2[i];
			GoalLine goalLine = goalLines[i];
			string value3 = goalLine.text.Value;
			if (string.IsNullOrEmpty(value3))
			{
				currentMesh.text = value3;
			}
			else
			{
				WorkshopManager.VerifyString(value3, delegate(WorkshopManager.VerifyStringResult res, string str)
				{
					if (currentMesh != null)
					{
						currentMesh.text = str;
					}
				});
			}
			currentMesh.fontSize = (int)goalLine.size.Value;
			currentMesh.color = new Color(currentMesh.color.r, currentMesh.color.g, currentMesh.color.b, a);
			MeshRenderer component = currentMesh.GetComponent<MeshRenderer>();
			if (component != null)
			{
				component.enabled = true;
			}
			fadeOnSimulate.textMeshes[i + 2] = currentMesh;
		}
		fadeOnSimulate.UpdateTextMeshes();
	}

	private void SetBracketSize(float newSize)
	{
		leftBracket.localPosition = new Vector3(0f - newSize, leftBracket.localPosition.y, leftBracket.localPosition.z);
		rightBracket.localPosition = new Vector3(newSize, rightBracket.localPosition.y, rightBracket.localPosition.z);
	}

	public override void OnLocalisationChange()
	{
		base.OnLocalisationChange();
		for (int i = 0; i < goalLines.Length; i++)
		{
			GoalLine goalLine = goalLines[i];
			if (goalLine != null)
			{
				SetGoalLineTexts(goalLine, i);
			}
		}
	}
}
