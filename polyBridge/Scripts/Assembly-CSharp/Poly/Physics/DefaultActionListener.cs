using UnityEngine;

namespace Poly.Physics
{
	public class DefaultActionListener : ActionListener
	{
		[Header("Debug")]
		public bool logBrekage;

		public override void OnActionAdded(Action a)
		{
			if (logBrekage)
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
			if (logBrekage)
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
}
