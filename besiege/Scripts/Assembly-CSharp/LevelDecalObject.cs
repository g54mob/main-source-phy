using UnityEngine;

public class LevelDecalObject : LevelMultiLookObject
{
	public Collider decalCollider;

	protected void Update()
	{
		bool flag = !StatMaster.levelSimulating && (levelEditor.CurrentState != StatMaster.Tool.None || InputManager.DeleteKey() || InputManager.DeleteKeyHeld());
		if (decalCollider.enabled != flag)
		{
			decalCollider.enabled = flag;
		}
	}
}
