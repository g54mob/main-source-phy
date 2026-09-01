using Poly.Physics;
using UnityEngine;

public class BridgeActionListener : ActionListener
{
	[Header("Debug")]
	public bool enableLogging;

	public override void OnActionAdded(Action a)
	{
		if (a is Rope)
		{
			BridgeRopes.Add((Rope)a);
		}
		if (enableLogging)
		{
			if (a is Rope)
			{
				Vector3[] array = ((Rope)a).ComputeNodePositions();
				Debug.Log(string.Format("Rope action #{2} added; has {0} vertices; first vert at {1}", array.Length, array[0], a.persistentId));
			}
			else
			{
				Debug.Log($"{a.GetType()} action added: {a.name}");
			}
		}
	}

	public override void OnActionRemoved(Action a)
	{
		if (a is Rope)
		{
			BridgeRopes.Remove((Rope)a);
		}
		if (enableLogging)
		{
			if (a is Rope)
			{
				Debug.Log($"Rope action #{a.persistentId} removed");
			}
			else
			{
				Debug.Log($"{a.GetType()} action removed");
			}
		}
	}
}
