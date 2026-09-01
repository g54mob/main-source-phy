using UnityEngine;

public class LevelDecalObjectColored : MonoBehaviour
{
	public Collider decalCollider;

	protected LevelEditor levelEditor;

	public void Start()
	{
		levelEditor = LevelEditor.Instance;
	}

	protected void Update()
	{
		bool flag = !StatMaster.levelSimulating && (levelEditor.CurrentState != StatMaster.Tool.None || InputManager.DeleteKey() || InputManager.DeleteKeyHeld());
		if (decalCollider.enabled != flag)
		{
			decalCollider.enabled = flag;
		}
	}
}
