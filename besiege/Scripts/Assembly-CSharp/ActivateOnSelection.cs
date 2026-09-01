using UnityEngine;

public class ActivateOnSelection : ClickBehaviour
{
	public GameObject target;

	protected override void LateUpdate()
	{
		if (StatMaster.advancedBuilding && AdvancedBlockEditor.Instance.selectionController.Count > 0)
		{
			target.SetActive(true);
		}
		else
		{
			target.SetActive(false);
		}
	}
}
