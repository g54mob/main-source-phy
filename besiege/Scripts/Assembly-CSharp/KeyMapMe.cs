using UnityEngine;

public class KeyMapMe : MonoBehaviour
{
	public KeyMapTool keyMapController;

	private void Start()
	{
		keyMapController = GameObject.Find("KeyMapTool").gameObject.GetComponent<KeyMapTool>();
	}

	private void OnMouseEnter()
	{
		if (StatMaster.Mode.selectedTool == StatMaster.Tool.Modify)
		{
			OpenKeyMap();
		}
	}

	private void OnMouseExit()
	{
		CloseKeyMap();
	}

	private void OpenKeyMap()
	{
		keyMapController.OpenKeyMap();
	}

	private void CloseKeyMap()
	{
		keyMapController.CloseKeyMap();
	}
}
