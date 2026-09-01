using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback allows you to chain any number of target MMF Players and play them in sequence, with optional delays before and after")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks", null)]
	[FeedbackPath("Feedbacks/MMF Player Chain")]
	public class MMF_PlayerChain : MMF_Feedback
	{
		[Serializable]
		public class PlayerChainItem
		{
			[Tooltip("the target MMF Player")]
			public MMF_Player TargetPlayer;

			[Tooltip("a delay in seconds to wait for before playing this MMF Player (x) and after (y)")]
			[MMVector(new string[] { "Before", "After" })]
			public Vector2 Delay;

			[Tooltip("whether this player is active in the list or not. Inactive players will be skipped when playing the chain of players")]
			public bool Inactive;

			[Tooltip("if this is true, the sequence will be blocked until this player has completed playing")]
			public bool WaitUntilComplete;
		}

		[CompilerGenerated]
		private sealed class _003CPlayChain_003Ed__6 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_PlayerChain _003C_003E4__this;

			private List<PlayerChainItem>.Enumerator _003C_003E7__wrap1;

			private PlayerChainItem _003Citem_003E5__3;

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
			public _003CPlayChain_003Ed__6(int _003C_003E1__state)
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

			private void _003C_003Em__Finally1()
			{
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		public static bool FeedbackTypeAuthorized;

		[MMFInspectorGroup("Feedbacks", true, 79, false, false)]
		[Tooltip("the list of MMF Player that make up the chain. The chain's items will be played from index 0 to the last in the list")]
		public List<PlayerChainItem> Players;

		public override float FeedbackDuration => 0f;

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		[IteratorStateMachine(typeof(_003CPlayChain_003Ed__6))]
		protected virtual IEnumerator PlayChain()
		{
			return null;
		}

		protected virtual bool FeedbacksStillPlaying()
		{
			return false;
		}

		protected override void CustomSkipToTheEnd(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomRestoreInitialValues()
		{
		}
	}
}
