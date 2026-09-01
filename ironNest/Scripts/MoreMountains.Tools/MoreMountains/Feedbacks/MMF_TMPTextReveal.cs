using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Scripting.APIUpdating;

namespace MoreMountains.Feedbacks
{
	[AddComponentMenu(null)]
	[FeedbackHelp("This feedback will let you reveal words, lines, or characters in a target TMP, one at a time")]
	[FeedbackPath("TextMesh Pro/TMP Text Reveal")]
	[MovedFrom(false, null, "MoreMountains.Feedbacks.TextMeshPro", null)]
	public class MMF_TMPTextReveal : MMF_Feedback
	{
		public enum RevealModes
		{
			Character = 0,
			Lines = 1,
			Words = 2
		}

		public enum DurationModes
		{
			Interval = 0,
			TotalDuration = 1
		}

		[CompilerGenerated]
		private sealed class _003CRevealCharacters_003Ed__29 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_TMPTextReveal _003C_003E4__this;

			private float _003CstartTime_003E5__2;

			private int _003CvisibleCharacters_003E5__3;

			private float _003ClastCharAt_003E5__4;

			private float _003Ctime_003E5__5;

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
			public _003CRevealCharacters_003Ed__29(int _003C_003E1__state)
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
		private sealed class _003CRevealLines_003Ed__30 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_TMPTextReveal _003C_003E4__this;

			private int _003CvisibleLines_003E5__2;

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
			public _003CRevealLines_003Ed__30(int _003C_003E1__state)
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
		private sealed class _003CRevealWords_003Ed__31 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMF_TMPTextReveal _003C_003E4__this;

			private int _003CvisibleWords_003E5__2;

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
			public _003CRevealWords_003Ed__31(int _003C_003E1__state)
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

		protected string _originalText;

		protected TMP_TextInfo _textInfo;

		[MMFInspectorGroup("Target", true, 12, true, false)]
		[Tooltip("the target TMP_Text component we want to change the text on")]
		public TMP_Text TargetTMPText;

		[MMFInspectorGroup("Change Text", true, 13, false, false)]
		[Tooltip("whether or not to replace the current TMP target's text on play")]
		public bool ReplaceText;

		[Tooltip("the new text to replace the old one with")]
		[TextArea]
		public string NewText;

		[MMFInspectorGroup("Reveal", true, 14, false, false)]
		[Tooltip("the selected way to reveal the text (character by character, word by word, or line by line)")]
		public RevealModes RevealMode;

		[Tooltip("whether to define duration by the time interval between two unit reveals, or by the total duration the reveal should take")]
		public DurationModes DurationMode;

		[Tooltip("the interval (in seconds) between two reveals")]
		[MMFEnumCondition("DurationMode", new int[] { 0 })]
		public float IntervalBetweenReveals;

		[Tooltip("the total duration of the text reveal, in seconds")]
		[MMFEnumCondition("DurationMode", new int[] { 1 })]
		public float RevealDuration;

		[Tooltip("a UnityEvent to invoke every time a reveal happens (word, line or character)")]
		public UnityEvent OnReveal;

		[Tooltip("alright so that one will be weird : for reasons, TextMeshPro won't let you read the length of a disabled text, so to do so, we need to enable it, even if it's just to disable it again right after. If you're targeting a disabled text, or a text that is part of a disabled hierarchy, you'll probably want to set this to true so that the system can proceed with accurate duration computation. If you don't, and your target transform is disabled, duration won't be computed correctly.")]
		public bool AllowHierarchyActivationForDurationComputation;

		protected float _delay;

		protected Coroutine _coroutine;

		protected int _richTextLength;

		protected int _totalCharacters;

		protected int _totalLines;

		protected int _totalWords;

		protected string _initialText;

		protected int _indexLastTime;

		public override bool HasAutomatedTargetAcquisition => false;

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

		protected override void AutomateTargetAcquisition()
		{
		}

		protected override void CustomPlayFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		[IteratorStateMachine(typeof(_003CRevealCharacters_003Ed__29))]
		protected virtual IEnumerator RevealCharacters()
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CRevealLines_003Ed__30))]
		protected virtual IEnumerator RevealLines()
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CRevealWords_003Ed__31))]
		protected virtual IEnumerator RevealWords()
		{
			return null;
		}

		protected virtual void InvokeRevealEvents()
		{
		}

		protected override void CustomStopFeedback(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected override void CustomSkipToTheEnd(Vector3 position, float feedbacksIntensity = 1f)
		{
		}

		protected int RichTextLength(string richText)
		{
			return 0;
		}

		protected virtual bool IsNewVisibleCharacter()
		{
			return false;
		}

		protected override void CustomRestoreInitialValues()
		{
		}
	}
}
