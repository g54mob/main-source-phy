using System.Collections.Generic;
using UnityEngine;

namespace MTAssets.UltimateLODSystem;

public class RuntimeInstancesDetector : MonoBehaviour
{
	public List<UltimateLevelOfDetail> instancesOfUlodInThisScene;

	public List<UltimateLevelOfDetailOptimizer> instancesOfUlodOptimizerInThisScene;

	public void RegisterNewUlodOptimizerInThisScene(UltimateLevelOfDetailOptimizer optimizer)
	{
		instancesOfUlodOptimizerInThisScene.Add(optimizer);
		List<UltimateLevelOfDetailOptimizer> list = instancesOfUlodOptimizerInThisScene;
		if (list._size > 1)
		{
			Debug.LogWarning("It has been identified that there is more than one \"Ultimate Level Of Detail Optimizer\" component in this scene. It is highly recommended that there is only one active component in the scene to avoid optimization problems and conflicts.");
		}
	}

	public RuntimeInstancesDetector()
	{
		List<UltimateLevelOfDetail> list = new List<UltimateLevelOfDetail>();
		instancesOfUlodInThisScene = list;
		instancesOfUlodOptimizerInThisScene = new List<UltimateLevelOfDetailOptimizer>();
		base._002Ector();
	}
}
