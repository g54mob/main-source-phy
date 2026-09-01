using UnityEngine;

public class BugGravity : MonoBehaviour
{
	private Vector3 startGrav;

	private void OnEnable()
	{
		startGrav = Physics.gravity;
		SetGrav(on: true);
	}

	private void OnDisable()
	{
		SetGrav(on: false);
	}

	public void SetGrav(bool on)
	{
		if (Bugs.EnabledOnPlatform())
		{
			if (on)
			{
				Bugs.gravityBug = true;
				Physics.gravity = startGrav * -0.3f;
			}
			else
			{
				Bugs.gravityBug = false;
				Physics.gravity = startGrav;
			}
		}
	}
}
