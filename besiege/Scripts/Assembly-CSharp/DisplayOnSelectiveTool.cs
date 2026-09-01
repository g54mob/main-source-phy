using UnityEngine;

public class DisplayOnSelectiveTool : MonoBehaviour
{
	public MeshRenderer[] renderers = new MeshRenderer[0];

	public BasicInfo basicInfo;

	public bool hasBasicInfo;

	private void Awake()
	{
		if (hasBasicInfo && basicInfo.isSimulating)
		{
			UpdateVisibility(false);
			return;
		}
		StatMaster.Mode.ToolChanged += UpdateVisibility;
		StatMaster.hudHiddenChanged += TopggleHUD;
		UpdateVisibility(StatMaster.Mode.selectedTool);
	}

	private void TopggleHUD()
	{
		UpdateVisibility(StatMaster.Mode.selectedTool);
	}

	private void UpdateVisibility(bool show)
	{
		for (int i = 0; i < renderers.Length; i++)
		{
			renderers[i].enabled = show;
		}
	}

	private void UpdateVisibility(StatMaster.Tool t)
	{
		switch (t)
		{
		case StatMaster.Tool.Translate:
		case StatMaster.Tool.Rotate:
		case StatMaster.Tool.Scale:
		case StatMaster.Tool.Mirror:
		case StatMaster.Tool.Modify:
			UpdateVisibility(!StatMaster.hudHidden);
			break;
		default:
			UpdateVisibility(false);
			break;
		}
	}

	private void OnDestroy()
	{
		StatMaster.Mode.ToolChanged -= UpdateVisibility;
		StatMaster.hudHiddenChanged -= TopggleHUD;
	}
}
