using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will cause a pause when met, preventing any other feedback lower in the sequence to run until it's complete.")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Pause/Pause")]
	public class MMF_Pause : MMF_Feedback
	{
		[CompilerGenerated]
		private sealed class _003CPauseWait_003Ed__14 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Pause _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CPauseWait_003Ed__14(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		[CompilerGenerated]
		private sealed class _003CPlayPause_003Ed__18 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_Pause _003C_003E4__this;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CPlayPause_003Ed__18(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Pause", true, 32, false, false)]
		[Tooltip("the duration of the pause, in seconds")]
		public float PauseDuration;

		public bool RandomizePauseDuration;

		[MMFCondition("RandomizePauseDuration", true)]
		public float MinPauseDuration;

		[MMFCondition("RandomizePauseDuration", true)]
		public float MaxPauseDuration;

		[MMFCondition("RandomizePauseDuration", true)]
		public bool RandomizeOnEachPlay;

		[Tooltip("if this is true, you'll need to call the Resume() method on the host MMFeedbacks for this pause to stop, and the rest of the sequence to play")]
		public bool ScriptDriven;

		[Tooltip("if this is true, a script driven pause will resume after its AutoResumeAfter delay, whether it has been manually resumed or not")]
		[MMFCondition("ScriptDriven", true)]
		public bool AutoResume;

		[Tooltip("the duration after which to auto resume, regardless of manual resume calls beforehand")]
		[MMFCondition("AutoResume", true)]
		public float AutoResumeAfter;

		public override IEnumerator Pause => null;

		public override float FeedbackDuration
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		[IteratorStateMachine(typeof(_003CPauseWait_003Ed__14))]
		protected virtual IEnumerator PauseWait()
		{
			return null;
		}

		protected override void CustomInitialization(MMF_Player owner)
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected virtual void ProcessNewPauseDuration()
		{
		}

		[IteratorStateMachine(typeof(_003CPlayPause_003Ed__18))]
		protected virtual IEnumerator PlayPause()
		{
			return null;
		}
	}
}
