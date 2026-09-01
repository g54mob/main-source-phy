using Cpp2ILInjected;
using UnityEngine;

public class ShooCatOnCollision : MonoBehaviour
{
	private CatPickUpHandler _catPickUpHandler;

	private void OnTriggerEnter(Collider other)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A8AE]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		if (!other.CompareTag("Cat"))
		{
			return;
		}
		CatPickUpHandler catPickUpHandler = _catPickUpHandler;
		CatController catController = Object.FindAnyObjectByType<CatController>();
		bool flag = catController == null;
		if (!flag && catController.RecoveryState == flag)
		{
			catController.ShooCat(initiatedByPlayer: false);
			string text = catController.name;
			string message = "Success — shoo '" + text + "'.";
			_catPickUpHandler.Log(message);
			if (catPickUpHandler.onCatShoo != null)
			{
				GameObject arg = catController.gameObject;
				catPickUpHandler.onCatShoo.Invoke(arg);
			}
		}
	}
}
