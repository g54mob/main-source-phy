using UnityEngine;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	public class DemoPackageTester : MonoBehaviour
	{
		[MMFInformation("This component is only used to display an error in the console in case dependencies for this demo haven't been installed. You can safely remove it if you want, and typically you wouldn't want to keep that in your own game.", MMFInformationAttribute.InformationType.Warning, false)]
		public bool RequiresPostProcessing;

		public bool RequiresTMP;

		public bool RequiresCinemachine;

		protected virtual void Awake()
		{
		}

		protected virtual void TestForDependencies()
		{
		}
	}
}
