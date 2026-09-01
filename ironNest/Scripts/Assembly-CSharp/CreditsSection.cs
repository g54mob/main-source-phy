using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class CreditsSection : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CInitialize_003Ed__10 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CreditsSection _003C_003E4__this;

		public CreditsSectionConfig config;

		public int maxLinesPerChunk;

		public Action onFirstChunkInitialized;

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
		public _003CInitialize_003Ed__10(int _003C_003E1__state)
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
	private sealed class _003CLoadContent_003Ed__13 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public CreditsSection _003C_003E4__this;

		public CreditsSectionConfig config;

		public int maxLinesPerChunk;

		public Action onFirstChunkInitialized;

		private List<string> _003Cchunks_003E5__2;

		private bool _003ChasInitializedFirstChunk_003E5__3;

		private List<string>.Enumerator _003C_003E7__wrap3;

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
		public _003CLoadContent_003Ed__13(int _003C_003E1__state)
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

	[SerializeField]
	private StaticLocalisedText _titleLocalization;

	[SerializeField]
	private TMP_Text _titleText;

	[SerializeField]
	private TMP_Text _contentText;

	[SerializeField]
	private RectTransform _rectTransform;

	private readonly List<RectTransform> _chunkRects;

	private float _contentHeight;

	public RectTransform RectTransform => null;

	public List<RectTransform> ChunkRects => null;

	[IteratorStateMachine(typeof(_003CInitialize_003Ed__10))]
	public IEnumerator Initialize(CreditsSectionConfig config, int maxLinesPerChunk, Action onFirstChunkInitialized = null)
	{
		return null;
	}

	public float GetHeight()
	{
		return 0f;
	}

	private void LoadTitle(CreditsSectionConfig config)
	{
	}

	[IteratorStateMachine(typeof(_003CLoadContent_003Ed__13))]
	private IEnumerator LoadContent(CreditsSectionConfig config, int maxLinesPerChunk, Action onFirstChunkInitialized = null)
	{
		return null;
	}

	private static List<string> SplitIntoChunks(string text, int linesPerChunk)
	{
		return null;
	}
}
