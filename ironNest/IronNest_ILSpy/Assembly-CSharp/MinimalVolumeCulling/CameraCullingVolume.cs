using Cpp2ILInjected;
using UnityEngine;

namespace MinimalVolumeCulling;

public sealed class CameraCullingVolume : MonoBehaviour
{
	private int priority;

	private string profileId;

	private bool forceTrigger;

	private Collider _collider;

	public int Priority => priority;

	public string ProfileId => profileId;

	public Collider VolumeCollider
	{
		get
		{
			if (_collider == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				Collider collider = default(Collider);
				_collider = collider;
			}
			return _collider;
		}
	}

	private void Reset()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Collider collider = default(Collider);
		_collider = collider;
		if (_collider != null)
		{
			_collider.isTrigger = true;
		}
	}

	private void OnValidate()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Collider collider = default(Collider);
		_collider = collider;
		if (forceTrigger && _collider != null && !_collider.isTrigger)
		{
			_collider.isTrigger = true;
		}
	}

	public CameraCullingVolume()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A5AF]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		profileId = "BackOfTurret";
		forceTrigger = true;
		base._002Ector();
	}
}
