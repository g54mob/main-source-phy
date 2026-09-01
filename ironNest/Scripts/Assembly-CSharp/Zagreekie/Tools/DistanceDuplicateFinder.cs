using UnityEngine;

namespace Zagreekie.Tools
{
	public class DistanceDuplicateFinder : MonoBehaviour
	{
		[SerializeField]
		[Tooltip("Tick this to scan the scene for GameObjects with more than one DistanceEnabler component and select them all in the Hierarchy.")]
		private bool findDuplicates;

		private void OnValidate()
		{
		}
	}
}
