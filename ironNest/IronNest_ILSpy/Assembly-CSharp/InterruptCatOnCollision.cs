using Cpp2ILInjected;
using UnityEngine;

public class InterruptCatOnCollision : MonoBehaviour
{
	private CatPickUpHandler _catPickUpHandler;

	private new string tag;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag(tag))
		{
			_catPickUpHandler.InterruptCat();
		}
	}

	public InterruptCatOnCollision()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A8AD]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		tag = "Player";
		base._002Ector();
	}
}
