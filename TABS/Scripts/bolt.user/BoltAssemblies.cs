using System.Collections.Generic;

public static class BoltAssemblies
{
	private static readonly List<string> AssemblyList;

	public static IEnumerator<string> AllAssemblies => AssemblyList.GetEnumerator();

	static BoltAssemblies()
	{
		AssemblyList = new List<string>();
		AssemblyList.Add("Assembly-CSharp");
		AssemblyList.Add("InControl");
		AssemblyList.Add("Cinemachine");
		AssemblyList.Add("AstarPathfindingProject");
		AssemblyList.Add("NaughtyAttributes.Core");
		AssemblyList.Add("PhotonBolt");
		AssemblyList.Add("BakeryRuntimeAssembly");
		AssemblyList.Add("Sirenix.OdinInspector.CompatibilityLayer");
	}
}
