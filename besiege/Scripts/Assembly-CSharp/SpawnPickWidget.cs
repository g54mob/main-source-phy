using UnityEngine;

public class SpawnPickWidget : PickWidget
{
	public override void Pick(GameObject obj)
	{
		if (obj == null)
		{
			OnCancelPick();
			UpdateVisual();
			return;
		}
		LevelPrefab component = obj.GetComponent<LevelPrefab>();
		if (component == null || component.ID != 9004)
		{
			OnCancelPick();
			UpdateVisual();
		}
		else
		{
			base.Pick(obj);
		}
	}
}
