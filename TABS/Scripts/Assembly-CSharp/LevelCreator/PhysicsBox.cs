using UnityEngine;

namespace LevelCreator
{
	public class PhysicsBox : TriggerBox, ITriggerable
	{
		public void Trigger()
		{
			EnablePhysicsOnConnectedObjects();
		}

		public override void Trigger(Collider other)
		{
			if (CanTrigger(other))
			{
				EnablePhysicsOnConnectedObjects();
			}
		}

		private void EnablePhysicsOnConnectedObjects()
		{
			ForEachConnection(delegate(DMEditorComponent c)
			{
				if (c != null)
				{
					c.SimulatePhysics(null, 100f, scaleForceByMass: false, 100f);
				}
			});
		}

		public override DMEditorComponent ValidateHighlightedObject(DMEditorComponent obj)
		{
			if (obj == null || !obj.CanSimulatePhysics)
			{
				return null;
			}
			return obj;
		}
	}
}
