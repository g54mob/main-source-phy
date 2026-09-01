using Poly.Physics;
using UnityEngine;
using UnityEngine.UI;

namespace Poly.UI
{
	public class SimulationOptionsUI : MonoBehaviour
	{
		public Toggle visualizeContactEventsToggle;

		public Toggle useEarlyMergeToggle;

		public Slider minProgressForEarlyMerge;

		public Text minProgressValueLabel;

		public Slider maxDistanceForEarlyMerge;

		public Text maxDistanceValueLabel;

		public Toggle superRigidSolverToggle;

		public Toggle alwaysRunEngineToggle;

		public Image[] statusImages;

		public Color statusEnabledColor = Color.green;

		public Color statusDisabledColor = Color.red;

		public Image[] statusEnabledImages;

		private World world;

		private bool tempSettingsInstantiated;

		private bool prevStatus;
	}
}
