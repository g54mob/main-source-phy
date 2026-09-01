using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Kamgam.UGUIComponentsForSettings;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class UIImageByteCycler : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CCycleRoutine_003Ed__25 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UIImageByteCycler _003C_003E4__this;

		private WaitForSecondsRealtime _003Cwait_003E5__2;

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
		public _003CCycleRoutine_003Ed__25(int _003C_003E1__state)
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
	private sealed class _003CLoadAsyncRoutine_003Ed__26 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UIImageByteCycler _003C_003E4__this;

		public IReadOnlyList<byte[]> imageBytes;

		private int _003CloadedThisFrame_003E5__2;

		private int _003CframesPerFrame_003E5__3;

		private int _003Ci_003E5__4;

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
		public _003CLoadAsyncRoutine_003Ed__26(int _003C_003E1__state)
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

	[Header("References")]
	public Image targetImage;

	public SliderUGUI Slider_FrameCounter;

	[Header("Cycling")]
	public float cycleInterval;

	public bool playOnLoad;

	public bool loop;

	[Header("Loading")]
	public int FramesLoadedPerFrame;

	private readonly List<Texture2D> textures;

	private readonly List<Sprite> sprites;

	private Coroutine loadRoutine;

	private Coroutine cycleRoutine;

	private int currentIndex;

	private void OnEnable()
	{
	}

	private void OnDisable()
	{
	}

	private void OnDestroy()
	{
	}

	public void Load(byte[][] imageBytes)
	{
	}

	public void LoadAsync(IReadOnlyList<byte[]> imageBytes)
	{
	}

	public void Load(IEnumerable<byte[]> imageBytes)
	{
	}

	public void LoadAsync(IEnumerable<byte[]> imageBytes)
	{
	}

	public void Play()
	{
	}

	public void Stop()
	{
	}

	public void Clear()
	{
	}

	private void ClearLoadedFrames()
	{
	}

	public void SetFrame(int index)
	{
	}

	public void JumpToFrameAndPause(float frameNumber)
	{
	}

	public void JumpToFrameAndPause(int frameNumber)
	{
	}

	[IteratorStateMachine(typeof(_003CCycleRoutine_003Ed__25))]
	private IEnumerator CycleRoutine()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003CLoadAsyncRoutine_003Ed__26))]
	private IEnumerator LoadAsyncRoutine(IReadOnlyList<byte[]> imageBytes)
	{
		return null;
	}

	private bool TryLoadFrame(byte[] bytes)
	{
		return false;
	}

	private void StopLoading()
	{
	}

	private void ConfigureSlider()
	{
	}

	private void ShowCurrentFrame()
	{
	}

	private void SetSliderFrameSilently(int frameNumber)
	{
	}
}
