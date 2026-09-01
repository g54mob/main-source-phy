using UnityEngine;

[AddComponentMenu("UI/Tools/Key Map Button")]
public class KeyMapButton : ClickBehaviour
{
	public Renderer bgRend;

	public Material clickedMaterial;

	public override void OnClicked()
	{
		bool flag = StatMaster.Mode.selectedTool == StatMaster.Tool.Modify;
		SetKeymapState(!flag);
		if (flag)
		{
			ReferenceMaster.ResetLevelEditor();
		}
	}

	private void SetKeymapState(bool state)
	{
		bgRend.enabled = state;
		StatMaster.Mode.selectedTool = ((!state) ? StatMaster.Tool.None : StatMaster.Tool.Modify);
	}
}
