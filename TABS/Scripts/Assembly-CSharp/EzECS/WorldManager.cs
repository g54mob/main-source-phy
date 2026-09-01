using Unity.Entities;

namespace EzECS
{
	public class WorldManager
	{
		public static void EnableAllSystems()
		{
			foreach (ScriptBehaviourManager behaviourManager in World.Active.BehaviourManagers)
			{
				if (behaviourManager is ComponentSystemBase componentSystemBase)
				{
					componentSystemBase.Enabled = true;
				}
			}
		}

		public static void DisableAllSystems()
		{
			foreach (ScriptBehaviourManager behaviourManager in World.Active.BehaviourManagers)
			{
				ComponentSystemBase componentSystemBase = behaviourManager as ComponentSystemBase;
				if (componentSystemBase != null)
				{
					componentSystemBase.Enabled = false;
				}
				if (componentSystemBase is BarrierSystem barrierSystem)
				{
					barrierSystem.ForceFlushBuffers();
				}
			}
		}
	}
}
