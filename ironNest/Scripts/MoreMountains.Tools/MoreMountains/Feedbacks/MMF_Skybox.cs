using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you change the scene's skybox on play, replacing it with another one, either a specific one, or one picked at random among multiple skyboxes.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Renderer/Skybox")]
	public class MMF_Skybox : MMF_Feedback
	{
		public enum Modes
		{
			Single = 0,
			Random = 1
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Skybox", true, 65, false, false)]
		public Modes Mode;

		public Material SingleSkybox;

		public Material[] RandomSkyboxes;

		protected Material _initialSkybox;

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}
	}
}
