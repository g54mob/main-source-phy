using UnityEngine;

namespace Battlehub
{
	internal class BHRoot : BHRoot<BHRoot>
	{
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		public static void RegisterAssembly()
		{
			KnownAssemblies.Add("Battlehub.RTEditor");
		}
	}
	public class BHRoot<T> : ScriptableObject where T : BHRoot<T>
	{
	}
}
