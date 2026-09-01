using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.UIElements;

namespace MoreMountains.FeedbacksForThirdParty
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback is a base for UI Toolkit feedbacks")]
	public class MMF_UIToolkit : MMF_Feedback
	{
		public enum QueryModes
		{
			Name = 0,
			Class = 1
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Target", true, 54, true, false)]
		[Tooltip("the UI document on which to make modifications")]
		public UIDocument TargetDocument;

		[Tooltip("the way to perform the query, either via element name or via class")]
		public QueryModes QueryMode;

		[Tooltip("the query to perform (replace this with your own element name or class)")]
		public string Query;

		[Tooltip("whether to mark the UI document dirty after the operation. Set this to true when making a change that requires a repaint such as when using generateVisualContent to render a mesh and the mesh data has now changed.")]
		public bool MarkDirty;

		protected List<VisualElement> _visualElements;

		public override bool HasAutomatedTargetAcquisition => false;

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void PerformQuery()
		{
		}

		protected virtual void HandleMarkDirty(VisualElement element)
		{
		}
	}
}
