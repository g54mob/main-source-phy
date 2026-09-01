using UnityEngine;

public class Autorotate : MonoBehaviour
{
	public Vector3 rotation;

	public bool unscaledTime = true;

	private unsafe void Update()
	{
		//IL_0058: Expected O, but got Ref
		Transform transform = base.transform;
		if (unscaledTime)
		{
			float unscaledDeltaTime = Time.unscaledDeltaTime;
		}
		else
		{
			float deltaTime = Time.deltaTime;
		}
		Vector3 vector = default(Vector3);
		transform.Rotate((Vector3)(&vector), Space.Self);
	}
}
