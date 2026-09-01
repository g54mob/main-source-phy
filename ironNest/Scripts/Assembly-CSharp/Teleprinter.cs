using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
[RequireComponent(typeof(TMP_Text))]
public class Teleprinter : MonoBehaviour
{
	public enum Teleprinters
	{
		Primary = 0,
		Secondary = 1
	}

	public enum TeleprinterAlarmState
	{
		None = 0,
		High = 1,
		Low = 2,
		Sucess = 3
	}

	[Serializable]
	public class TeleprinterCharacterEvent : UnityEvent<char, int, int>
	{
	}

	[Serializable]
	public class TeleprinterLineTransitionEvent : UnityEvent<int, int>
	{
	}

	public enum CursorLockMode
	{
		None = 0,
		LockYOnly = 1,
		LockYAndZ = 2,
		LockYAndX = 3
	}

	public enum PrintingOrder
	{
		TopDown = 0,
		BottomUp = 1
	}

	private struct LineRange
	{
		public int lineNumber;

		public int firstCharIndex;

		public int lastCharIndex;
	}

	[CompilerGenerated]
	private sealed class _003CMoveCursor_003Ed__98 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Teleprinter _003C_003E4__this;

		public Vector3 fromWorld;

		public Vector3 toWorld;

		private float _003Cduration_003E5__2;

		private float _003Ct_003E5__3;

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
		public _003CMoveCursor_003Ed__98(int _003C_003E1__state)
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
	private sealed class _003CMovePaper_003Ed__97 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Teleprinter _003C_003E4__this;

		public float lineDeltaLocal;

		public int lineCount;

		public bool compensateCursorWorldPosition;

		private Vector3 _003CstartLocal_003E5__2;

		private Vector3 _003CendLocal_003E5__3;

		private float _003Cduration_003E5__4;

		private float _003Ct_003E5__5;

		private Vector3 _003CprevPaperLocal_003E5__6;

		private bool _003CdoRotate_003E5__7;

		private Quaternion _003CstartRotLocal_003E5__8;

		private Quaternion _003CendRotLocal_003E5__9;

		private Quaternion _003CstartRotWorld_003E5__10;

		private Quaternion _003CendRotWorld_003E5__11;

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
		public _003CMovePaper_003Ed__97(int _003C_003E1__state)
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
	private sealed class _003CRunQueue_003Ed__91 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Teleprinter _003C_003E4__this;

		private PrintJob _003Cjob_003E5__2;

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
		public _003CRunQueue_003Ed__91(int _003C_003E1__state)
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
	private sealed class _003CTypeChunkBottomUp_003Ed__95 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Teleprinter _003C_003E4__this;

		public string chunkRich;

		private string _003CnewFull_003E5__2;

		private TMP_TextInfo _003Cti_003E5__3;

		private int _003CnewCount_003E5__4;

		private List<LineRange> _003Clines_003E5__5;

		private int _003CfromLine_003E5__6;

		private int _003CtoLine_003E5__7;

		private LineRange _003Clr_003E5__8;

		private TMP_CharacterInfo _003Cci_003E5__9;

		private char _003Cc_003E5__10;

		private bool _003CisLineBreakChar_003E5__11;

		private Vector3 _003CtargetPos_003E5__12;

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
		public _003CTypeChunkBottomUp_003Ed__95(int _003C_003E1__state)
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
	private sealed class _003CTypeChunkTopDown_003Ed__94 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public Teleprinter _003C_003E4__this;

		public string chunkRich;

		private string _003CnewFull_003E5__2;

		private TMP_TextInfo _003Cti_003E5__3;

		private int _003CstartIndex_003E5__4;

		private int _003CendIndex_003E5__5;

		private int _003CprevLineNumLocal_003E5__6;

		private int _003Ci_003E5__7;

		private char _003Cc_003E5__8;

		private int _003CcharLineNum_003E5__9;

		private bool _003CisLineBreakChar_003E5__10;

		private Vector3 _003CtargetPos_003E5__11;

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
		public _003CTypeChunkTopDown_003Ed__94(int _003C_003E1__state)
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

	[Tooltip("What teleprinter is this")]
	public Teleprinters TeleprinterType;

	private static Dictionary<Teleprinters, Teleprinter> Lookup;

	[Header("Timing")]
	[Tooltip("Delay between each printed visible character in seconds. Set to 0 to reveal as fast as the cursor can move.")]
	public float pausePerLetter;

	[Tooltip("Maximum cursor travel speed in world units/second. Set to 0 to snap instantly to each target character.")]
	public float cursorMaxSpeed;

	[Tooltip("Paper feed speed in local units/second. The paper moves in the paper transform's LOCAL space so page tilt is respected.")]
	public float paperFeedSpeed;

	[Tooltip("Delay between jobs after one job completes and before the next starts.")]
	public float interJobDelay;

	[Header("Behavior")]
	[Tooltip("If true, empty lines will be padded with a single space so TMP still generates geometry for cursor placement. Typically not needed when using maxVisibleCharacters typing.")]
	public bool padEmptyLines;

	[Tooltip("If true, disables the typing animation and prints all queued text instantly.")]
	public bool skipAnimation;

	[Tooltip("If true, invert the paper feed direction for each line feed.")]
	public bool invertPaperDirection;

	[Tooltip("If true, when all jobs complete the paper will return to its initial local position only if accumulatePaperFeed is false.")]
	public bool resetPaperPositionOnComplete;

	[Tooltip("If true, paper feed accumulates across multiple jobs. If false, paper feed can be reset when all jobs complete.")]
	public bool accumulatePaperFeed;

	[Header("Printing Order")]
	[Tooltip("TopDown: first line first (legacy behavior using TMP's maxVisibleCharacters).\nBottomUp: newest jobs prepend at the top and are typed bottom-to-top per chunk. For BottomUp, this script assumes you configure TMP for bottom alignment so the bottom line stays fixed while new lines appear above.")]
	public PrintingOrder printingOrder;

	[Header("References")]
	[Tooltip("Transform representing the paper. Paper feed (line moves) are applied in this transform's LOCAL space. Defaults to the TMP object's transform.")]
	public Transform paperTransform;

	[Tooltip("RectTransform used as the typing head / cursor. The cursor will be moved to follow characters as they are typed.")]
	public RectTransform typerCursor;

	[Tooltip("Optional Animator to drive typing visuals. The boolean parameter named in 'Typing Bool Name' will be toggled while printing non-linebreak characters.")]
	public Animator typerAnimator;

	[Tooltip("Animator boolean parameter name set while typing. Example: 'typing'")]
	public string typingBoolName;

	[Header("Cursor & Axis Options")]
	[Tooltip("Lock scheme when moving the cursor.\nNone: free.\nLockYOnly: force cursor Y to the initial baseline.\nLockYAndZ: force Y and preserve Z.\nLockYAndX: force Y and preserve X.")]
	public CursorLockMode cursorLockMode;

	[Tooltip("If true, place the cursor using localPosition relative to its parent instead of world space.")]
	public bool useLocalCursorPosition;

	[Tooltip("When using local cursor placement, preserve the existing local Z to avoid depth jumps.")]
	public bool preserveLocalZ;

	[Header("Paper Rotation")]
	[Tooltip("Optional Transform to rotate in sync with each paper line feed. Leave null to disable rotation.")]
	public Transform rotateTransform;

	[Tooltip("Optional tag to find the rotateTransform at Awake if not assigned. The first GameObject with this tag will be used.")]
	public string rotateTransformTag;

	[Tooltip("Axis to rotate around. Interpreted in local or world space per 'Rotate In Local Space'. Example: (0,0,1)=Z axis.")]
	public Vector3 rotationAxis;

	[Tooltip("Degrees to rotate per line feed. Example: 36 = 36 degrees per line.")]
	public float degreesPerLine;

	[Tooltip("If true, rotation axis is in rotateTransform's local space; if false, world space.")]
	public bool rotateInLocalSpace;

	[Tooltip("If true and resetPaperPositionOnComplete resets the paper, also reset the rotateTransform to its initial rotation.")]
	public bool resetRotationOnComplete;

	[Header("Debug")]
	[Tooltip("Log cursor movement debug messages.")]
	public bool debugCursor;

	[Tooltip("Log character-by-character debug messages.")]
	public bool debugChars;

	[Header("Events")]
	[Tooltip("Invoked when typing starts (runner begins).")]
	public UnityEvent onTypingStarted;

	[Tooltip("Invoked when the queue has been drained and all jobs completed.")]
	public UnityEvent onAllJobsCompleted;

	[Tooltip("Invoked when a job begins processing.")]
	public UnityEvent onJobStarted;

	[Tooltip("Invoked when a job finishes processing.")]
	public UnityEvent onJobCompleted;

	[Tooltip("Invoked when a character has been printed (visible character completed). NOTE: This event has no payload; use 'onCharacterPrintedDetailed' if you need the actual character, index, or line number.")]
	public UnityEvent onCharacterPrinted;

	[Tooltip("Invoked each time a line feed (page advance) completes. This is the visual paper movement, not necessarily a TMP layout line transition due to word wrap.")]
	public UnityEvent onLineFeed;

	[Tooltip("Invoked each time a job is queued")]
	public UnityEvent onJobsEnqueued;

	[Header("Signal Events")]
	public UnityEvent OnSignal_Reset;

	public UnityEvent OnSignal_None;

	public UnityEvent OnSignal_High;

	public UnityEvent OnSignal_Low;

	public UnityEvent OnSignal_Success;

	[Header("Events (Detailed)")]
	[Tooltip("Invoked whenever a character is revealed/printed by the typing process.\nParameters:\n- character: the TMP character at charIndex (may be '\\n', '\\r', ' ', '\\t', '\\u00A0', etc.)\n- charIndex: TMP character index in the current TMP textInfo.characterInfo array\n- lineNumber: TMP line number for this character (includes word-wrapped lines)")]
	public TeleprinterCharacterEvent onCharacterPrintedDetailed;

	[Tooltip("Invoked once per TMP line transition during typing.\nThis is based on TMP's characterInfo[i].lineNumber changes, so it includes both explicit newlines and word-wrap layout transitions.\nParameters:\n- fromLine: previous TMP line number\n- toLine: new TMP line number")]
	public TeleprinterLineTransitionEvent onLineTransition;

	private TMP_Text _tmp;

	private Coroutine _runner;

	private bool _isRunning;

	private bool _baselineSet;

	private float _baselineWorldY;

	private Vector3 _initialPaperLocalPos;

	private Vector3 _lastCursorWorldPos;

	private int _prevLineNum;

	private bool _animTypingState;

	private readonly Queue<PrintJob> _pendingJobs;

	private long _nextJobId;

	public Action OnPrintingWillStart;

	public Action OnCleared;

	private static readonly Regex emptyLineRegex;

	private string _currentFullRich;

	private int _currentRevealedCharIndex;

	private Quaternion _initialRotateLocal;

	private Quaternion _initialRotateWorld;

	private bool _initialRotateStored;

	private float _cachedPausePerLetter;

	private WaitForSeconds _waitPerLetter;

	private float _cachedInterJobDelay;

	private WaitForSeconds _waitInterJob;

	private readonly List<bool> _revealMask;

	private bool _applyMaskThisFrame;

	public bool HasJobs => false;

	public bool IsPrinting => false;

	public int CurrentLineCount { get; private set; }

	public static Teleprinter GetTeleprinter(Teleprinters type)
	{
		return null;
	}

	private void Awake()
	{
	}

	private void OnValidate()
	{
	}

	private void LateUpdate()
	{
	}

	private void RefreshWaitCaches()
	{
	}

	public void SignalAlarm(TeleprinterAlarmState alarmState)
	{
	}

	public void ClearAlarm()
	{
	}

	public PrintJob SubmitLines(string sourceId, IEnumerable<string> lines, object userData = null, bool waitForTrigger = false)
	{
		return null;
	}

	public void TryStart(bool ignoreInitialDelay = false)
	{
	}

	public void ForceCompleteAll()
	{
	}

	public void ClearAll()
	{
	}

	[IteratorStateMachine(typeof(_003CRunQueue_003Ed__91))]
	private IEnumerator RunQueue()
	{
		return null;
	}

	private void AppendInstant(string chunkRich, bool prepend)
	{
	}

	private void DrainAllJobsInstant()
	{
	}

	[IteratorStateMachine(typeof(_003CTypeChunkTopDown_003Ed__94))]
	private IEnumerator TypeChunkTopDown(string chunkRich)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CTypeChunkBottomUp_003Ed__95))]
	private IEnumerator TypeChunkBottomUp(string chunkRich)
	{
		return null;
	}

	private void ApplyAlphaMaskToText()
	{
	}

	[IteratorStateMachine(typeof(_003CMovePaper_003Ed__97))]
	private IEnumerator MovePaper(float lineDeltaLocal, int lineCount, bool compensateCursorWorldPosition)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CMoveCursor_003Ed__98))]
	private IEnumerator MoveCursor(Vector3 fromWorld, Vector3 toWorld)
	{
		return null;
	}

	private void ApplyInstantRotation(float lineDeltaLocal, int lineCount)
	{
	}

	private void ApplyCursorLock(ref Vector3 targetWorld)
	{
	}

	private void SetCursorPosition(Vector3 worldPos)
	{
	}

	private Vector3 GetCharWorldPositionFromInfoOrApprox(TMP_TextInfo ti, int charIndex)
	{
		return default(Vector3);
	}

	private float GetLineVerticalDeltaCached(TMP_TextInfo ti, int fromLine, int toLine)
	{
		return 0f;
	}

	private List<LineRange> BuildChunkLineRanges(TMP_TextInfo ti, int startIndex, int endIndex)
	{
		return null;
	}

	private string PadEmptyLines(string rich)
	{
		return null;
	}

	private string JoinJobLinesAppend(List<string> lines)
	{
		return null;
	}

	private string JoinJobLinesPrepend(List<string> lines)
	{
		return null;
	}

	private void EnsureMaskCapacity(int count)
	{
	}

	private void SetTypingAnimator(bool state)
	{
	}
}
