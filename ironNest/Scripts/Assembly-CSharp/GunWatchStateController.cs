using UnityEngine;

public class GunWatchStateController : MonoBehaviour
{
	[SerializeField]
	private ClipboardStateController _clipboardStateController;

	[SerializeField]
	private Animator _animator;

	private static int IsHiddenParam;

	private static int IsRaisedParam;

	private ClipboardStateController.ClipboardState _lastClipboardState;

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void RefreshBasedOnClipboardState(in ClipboardStateController.ClipboardState clipboardState)
	{
	}
}
