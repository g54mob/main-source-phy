using Cpp2ILInjected;
using UnityEngine;

namespace MinimalVolumeCulling;

public sealed class CullZone : MonoBehaviour
{
	private string zoneId;

	private bool activeByDefault;

	private bool forceTrigger;

	private Collider _collider;

	public string ZoneId => zoneId;

	public bool ActiveByDefault => activeByDefault;

	public Collider ZoneCollider
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

	public CullZone()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A5CB]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		zoneId = "Barbet_All";
		forceTrigger = true;
		base._002Ector();
	}
}
