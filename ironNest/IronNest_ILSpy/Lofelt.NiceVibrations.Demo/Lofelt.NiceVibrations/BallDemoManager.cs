using UnityEngine;

namespace Lofelt.NiceVibrations;

public class BallDemoManager : DemoManager
{
	public Vector2 Gravity;

	protected virtual void Start()
	{
		Vector2 gravity = default(Vector2);
		Physics2D.gravity = gravity;
	}

	public BallDemoManager()
	{
		//IL_000b: Expected O, but got I4
		Gravity = (Vector2)0;
		_ = 3253731328L;
		((MonoBehaviour)this)._002Ector();
	}
}
