using UnityEngine;

public class ConnectJointToUnit : MonoBehaviour
{
	public enum Bodypart
	{
		Head = 0,
		Torso = 1,
		Hip = 2
	}

	public Bodypart bodypart;

	private bool done;

	public void Go()
	{
		if (done)
		{
			return;
		}
		done = true;
		ConfigurableJoint component = GetComponent<ConfigurableJoint>();
		Rigidbody rigidbody = null;
		if (bodypart == Bodypart.Head)
		{
			Head componentInChildren = base.transform.root.GetComponentInChildren<Head>();
			if ((bool)componentInChildren)
			{
				rigidbody = componentInChildren.GetComponent<Rigidbody>();
			}
		}
		if (bodypart == Bodypart.Torso)
		{
			Torso componentInChildren2 = base.transform.root.GetComponentInChildren<Torso>();
			if ((bool)componentInChildren2)
			{
				rigidbody = componentInChildren2.GetComponent<Rigidbody>();
			}
		}
		if (bodypart == Bodypart.Hip)
		{
			Hip componentInChildren3 = base.transform.root.GetComponentInChildren<Hip>();
			if ((bool)componentInChildren3)
			{
				rigidbody = componentInChildren3.GetComponent<Rigidbody>();
			}
		}
		if ((bool)rigidbody && (bool)component)
		{
			component.connectedBody = rigidbody;
		}
	}

	private void Start()
	{
		Go();
	}
}
