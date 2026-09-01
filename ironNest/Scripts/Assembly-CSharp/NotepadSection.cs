using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
[AddComponentMenu("Notepad/Notepad Section")]
public class NotepadSection : MonoBehaviour
{
	public enum WriteMode
	{
		Add = 0,
		Replace = 1
	}

	public enum AddPosition
	{
		Top = 0,
		Bottom = 1
	}

	public enum TextRevealMode
	{
		Instant = 0,
		Typewriter = 1
	}

	private struct PendingWrite
	{
		public string content;

		public WriteMode mode;

		public AddPosition addPos;

		public float delay;

		public TextRevealMode revealMode;

		public float typewriterSecondsPerCharacter;
	}

	[CompilerGenerated]
	private sealed class _003CApplyWriteTypewriterRoutine_003Ed__47 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NotepadSection _003C_003E4__this;

		public PendingWrite pw;

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
		public _003CApplyWriteTypewriterRoutine_003Ed__47(int _003C_003E1__state)
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
	private sealed class _003CProcessQueue_003Ed__45 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public NotepadSection _003C_003E4__this;

		private PendingWrite _003Cnext_003E5__2;

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
		public _003CProcessQueue_003Ed__45(int _003C_003E1__state)
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
	private sealed class _003CRevealTypewriterAddBottomScaled_003Ed__50 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public string newEntry;

		public float secondsPerChar;

		public NotepadSection _003C_003E4__this;

		public string existingPrefix;

		public int maxCharsPerFrame;

		private int _003Clength_003E5__2;

		private string _003CstaticPart_003E5__3;

		private float _003Celapsed_003E5__4;

		private int _003Cshown_003E5__5;

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
		public _003CRevealTypewriterAddBottomScaled_003Ed__50(int _003C_003E1__state)
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
	private sealed class _003CRevealTypewriterAddTopScaled_003Ed__49 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public string newEntry;

		public float secondsPerChar;

		public NotepadSection _003C_003E4__this;

		public string existingSuffix;

		public int maxCharsPerFrame;

		private int _003Clength_003E5__2;

		private float _003Celapsed_003E5__3;

		private int _003Cshown_003E5__4;

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
		public _003CRevealTypewriterAddTopScaled_003Ed__49(int _003C_003E1__state)
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
	private sealed class _003CRevealTypewriterReplaceScaled_003Ed__48 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public string fullContent;

		public float secondsPerChar;

		public NotepadSection _003C_003E4__this;

		public int maxCharsPerFrame;

		private int _003Clength_003E5__2;

		private float _003Celapsed_003E5__3;

		private int _003Cshown_003E5__4;

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
		public _003CRevealTypewriterReplaceScaled_003Ed__48(int _003C_003E1__state)
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

	[Header("Target")]
	[Tooltip("TMP_Text target (TextMeshPro 3D or TextMeshProUGUI) that displays this section's content.\nIf left empty, the component attempts GetComponent<TMP_Text>() on Awake.\nIf still missing, Write() calls are ignored and a warning is logged.")]
	[SerializeField]
	private TMP_Text targetText;

	[Header("Defaults (Used When Callers Do NOT Specify)")]
	[Tooltip("Default behavior for Write(string) calls lacking an explicit WriteMode.\nAdd: The new note is combined with existing content based on 'Default Add Position'.\nReplace: The new note replaces the entire existing content.")]
	[SerializeField]
	private WriteMode defaultWriteMode;

	[Tooltip("Placement for new content when using Add mode and caller does not specify.\nTop: New content appears BEFORE existing content.\nBottom: New content appears AFTER existing content.")]
	[SerializeField]
	private AddPosition defaultAddPosition;

	[Header("Timing / Delay")]
	[Tooltip("Section-wide DEFAULT delay (seconds) before a write applies (or begins typewriter reveal) when the caller does not specify.\n0 = immediate. Negative values are clamped to 0.\nMultiple delayed writes queue and process sequentially.\nImmediate events (onFirstNoteAdded / onAnyNoteAdded) IGNORE this delay.\nThis value participates in the 'Default Delay Only First Note' rule.")]
	[SerializeField]
	private float writeDelaySeconds;

	[Tooltip("If TRUE: The section default delay (writeDelaySeconds) applies ONLY to the FIRST write after the section becomes empty (initial state or post-Clear()).\nSubsequent writes that use EXACTLY the default delay will have their delay suppressed (set to 0).\nAny write specifying a custom delay different from writeDelaySeconds ALWAYS honors that custom delay.\nIf FALSE: The default delay applies to every write that uses it.")]
	[SerializeField]
	private bool defaultDelayOnlyFirstNote;

	[Tooltip("If TRUE: Clear() cancels and discards ALL queued (not-yet-started) writes and any in-progress delay or typewriter animation.\nIf FALSE: Queued writes continue after Clear(), potentially repopulating text.\nUse TRUE if Clear() represents a hard reset of this section.")]
	[SerializeField]
	private bool cancelPendingWritesOnClear;

	[Header("Reveal (Typing) Defaults")]
	[Tooltip("Default reveal mode used when caller does not specify one.\nInstant: New content appears all at once after any delay.\nTypewriter: ONLY the new note's characters are revealed over time.\n\nImportant:\n- If you write raw TMP rich text (e.g. <b>...</b>) using Typewriter, tags may visibly appear while typing.\n- For rich-text-heavy content, prefer Instant reveal.")]
	[SerializeField]
	private TextRevealMode defaultRevealMode;

	[Tooltip("Default reveal speed used when Typewriter is active and caller does not specify.\nThis is a FIXED seconds-per-character value (consistent typing speed regardless of note length).\nTiming uses SCALED time (Time.deltaTime), so it respects Time.timeScale.\nIf the frame rate is low or the game hitches, multiple characters may be revealed in a single frame to keep real scaled time consistent.\nIf <= 0 or content length < 1, reveal falls back to Instant.")]
	[SerializeField]
	private float defaultTypewriterSecondsPerCharacter;

	[Tooltip("Safety cap for Typewriter reveal: maximum number of characters that may be revealed in a single frame.\n0 = unlimited.\nUse a small value (e.g., 10-50) if you want to avoid large jumps on big frame spikes.\nIf negative, clamped to 0.")]
	[SerializeField]
	private int maxTypewriterCharactersPerFrame;

	[Header("Delete Behavior (Layout Lines)")]
	[Tooltip("When TRUE (recommended): deleting TMP *layout* lines (visual wrap lines) will preserve visual separation by inserting a single '\\n' at the deletion boundary\nIF (and only if) the deleted content contained no author-intended newline characters ('\\n' or '\\r').\n\nWhy:\n- TMP layout lines created by word-wrap do NOT exist as '\\n' in the raw string.\n- If you delete a wrap-only visual line, the remaining text can join and visually merge paragraphs.\n- This option prevents that by forcing a hard newline where the cut happened.\n\nTradeoff:\n- This may introduce a new author-style line break in rare cases, but it avoids the more disruptive merge behavior.")]
	[SerializeField]
	private bool preserveVisualSeparationWhenDeletingLayoutLines;

	[SerializeField]
	private bool stripTags;

	[Header("Events (See Timing Contract in Class Summary)")]
	public UnityEvent onFirstNoteAdded;

	public UnityEvent onAnyNoteAdded;

	public UnityEvent onWriteStarted;

	public UnityEvent onWriteCompleted;

	public UnityEvent onCleared;

	private static readonly List<NotepadSection> s_AllSections;

	private bool _hasAddedFirstNote;

	private readonly Queue<PendingWrite> _pendingWrites;

	private Coroutine _processCoroutine;

	private bool _isProcessing;

	public TMP_Text TargetText => null;

	public string UnityTag => null;

	public bool HasAddedFirstNote => false;

	public bool HasPendingWrites => false;

	private void Awake()
	{
	}

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
	}

	public void Write(string content)
	{
	}

	public void Write(string content, WriteMode mode, AddPosition addPosition)
	{
	}

	public void Write(string content, WriteMode mode, AddPosition addPosition, float delaySeconds)
	{
	}

	public void Write(string content, WriteMode mode, AddPosition addPosition, float delaySeconds, TextRevealMode revealMode)
	{
	}

	public void Write(string content, WriteMode mode, AddPosition addPosition, float delaySeconds, TextRevealMode revealMode, float typewriterSecondsPerCharacter)
	{
	}

	[ContextMenu("Clear Section")]
	public void Clear()
	{
	}

	[ContextMenu("Cancel Pending Writes")]
	public void CancelPendingWrites()
	{
	}

	private void EnsureProcessorRunning()
	{
	}

	[IteratorStateMachine(typeof(_003CProcessQueue_003Ed__45))]
	private IEnumerator ProcessQueue()
	{
		return null;
	}

	private void ApplyWriteInstant(PendingWrite pw)
	{
	}

	[IteratorStateMachine(typeof(_003CApplyWriteTypewriterRoutine_003Ed__47))]
	private IEnumerator ApplyWriteTypewriterRoutine(PendingWrite pw)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CRevealTypewriterReplaceScaled_003Ed__48))]
	private IEnumerator RevealTypewriterReplaceScaled(string fullContent, float secondsPerChar, int maxCharsPerFrame)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CRevealTypewriterAddTopScaled_003Ed__49))]
	private IEnumerator RevealTypewriterAddTopScaled(string newEntry, string existingSuffix, float secondsPerChar, int maxCharsPerFrame)
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CRevealTypewriterAddBottomScaled_003Ed__50))]
	private IEnumerator RevealTypewriterAddBottomScaled(string newEntry, string existingPrefix, float secondsPerChar, int maxCharsPerFrame)
	{
		return null;
	}

	private void CancelPendingWritesInternal()
	{
	}

	private void SafeInvoke(UnityEvent evt)
	{
	}

	public bool RemoveLayoutLineRange(int minLayoutLineIndex, int maxLayoutLineIndex, TMP_Text tmpForLayout = null)
	{
		return false;
	}

	public static NotepadSection ResolveByTag(string unityTag)
	{
		return null;
	}

	private static string StripTags(string s)
	{
		return null;
	}
}
