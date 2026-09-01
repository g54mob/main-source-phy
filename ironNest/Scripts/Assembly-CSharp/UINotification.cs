using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class UINotification : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CLifetimeRoutine_003Ed__14 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UINotification _003C_003E4__this;

		public float lifetime;

		private float _003CfadeTime_003E5__2;

		private float _003Ctime_003E5__3;

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
		public _003CLifetimeRoutine_003Ed__14(int _003C_003E1__state)
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
	public TMP_Text titleText;

	public TMP_Text descriptionText;

	public Image borderImage;

	public CanvasGroup canvasGroup;

	[Header("Timing")]
	public float defaultLifetime;

	public float defaultFadeTime;

	public bool useUnscaledTime;

	public bool disableLayoutAfterBuild;

	private LayoutGroup[] layoutGroups;

	private ContentSizeFitter[] contentSizeFitters;

	private Coroutine routine;

	private void Awake()
	{
	}

	private void OnDisable()
	{
	}

	public void Show(string title, string description, float lifetime, Color? borderColor)
	{
	}

	[IteratorStateMachine(typeof(_003CLifetimeRoutine_003Ed__14))]
	private IEnumerator LifetimeRoutine(float lifetime)
	{
		return null;
	}

	private void CacheReferences()
	{
	}

	private void EnableLayout()
	{
	}

	private void DisableLayout()
	{
	}

	private void RebuildLayout()
	{
	}

	private float DeltaTime()
	{
		return 0f;
	}

	private object Wait(float seconds)
	{
		return null;
	}
}
