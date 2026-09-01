using Cpp2ILInjected;
using UnityEngine;

public class MapReconClearChild : MonoBehaviour
{
	private MapReconClearHandle _handle;

	private void Awake()
	{
		if (_handle == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1806963E0");
			MapReconClearHandle handle = default(MapReconClearHandle);
			_handle = handle;
		}
		if (!(_handle != null))
		{
			string text = base.name;
			string message = "[MapReconClearChild] '" + text + "' could not find an MapReconClearHandle in its parent hierarchy. Assign it manually in the Inspector, or ensure Awake fires before this object is reparented.";
			Debug.LogWarning(message, this);
			return;
		}
		MapReconClearHandle handle2 = _handle;
		GameObject gameObject = base.gameObject;
		if (gameObject != null && !handle2._allChildren.Contains(gameObject))
		{
			handle2._allChildren.Add(gameObject);
		}
	}
}
