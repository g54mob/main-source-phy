using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("UI/Tools/Duplicate Button")]
public class DuplicateButton : ClickBehaviour
{
	public Renderer bgRend;

	private void Awake()
	{
		bgRend.gameObject.SetActive(false);
		releaseOnlyOver = true;
	}

	public override void OnClicked()
	{
		if (base.enabled)
		{
			Machine machine = Machine.Active();
			if ((bool)machine && !machine.isSimulating && machine.CanModify)
			{
				bgRend.gameObject.SetActive(true);
			}
		}
	}

	public override void OnClickReleased()
	{
		if (base.enabled)
		{
			BlockSelectionTool selectionController = AdvancedBlockEditor.Instance.selectionController;
			BlockBehaviour firstBlock = Machine.Active().FirstBlock;
			int num = ((firstBlock != null && selectionController.MachineSelection.Contains(firstBlock)) ? 1 : 0);
			if (selectionController.Count > num)
			{
				List<UndoAction> actions = selectionController.DuplicateSelection();
				Machine.Active().UndoSystem.AddActions(actions);
			}
			bgRend.gameObject.SetActive(false);
		}
	}

	protected void OnMouseExit()
	{
		if (releaseOnlyOver)
		{
			bgRend.gameObject.SetActive(false);
		}
	}
}
