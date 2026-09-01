using Cpp2ILInjected;
using UnityEngine;

public class AddImpulseToRigidbody : MonoBehaviour
{
	public Vector3 Impulse;

	protected Rigidbody _rigidbody;

	public Rigidbody Rigidbody
	{
		get
		{
			if (_rigidbody == null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
				Rigidbody rigidbody = default(Rigidbody);
				_rigidbody = rigidbody;
			}
			return _rigidbody;
		}
	}

	public unsafe void AddImpulse()
	{
		//IL_005b: Expected O, but got Ref
		if (_rigidbody == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Rigidbody rigidbody = default(Rigidbody);
			_rigidbody = rigidbody;
		}
		object obj = default(object);
		_rigidbody.AddForce((Vector3)(&obj), ForceMode.Impulse);
	}

	public AddImpulseToRigidbody()
	{
		Vector3 impulse = default(Vector3);
		Impulse = impulse;
		_ = 0;
		base._002Ector();
	}
}
