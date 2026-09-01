using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Kamgam.SettingsGenerator;
using UnityEngine;

public class UpscalerOptionsHelper : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayedSet_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public UpscalerOptionsHelper _003C_003E4__this;

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
		public _003CDelayedSet_003Ed__7(int _003C_003E1__state)
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
	private sealed class _003COnRenderScaleChanged_003Ed__8 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public float scale;

		public UpscalerOptionsHelper _003C_003E4__this;

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
		public _003COnRenderScaleChanged_003Ed__8(int _003C_003E1__state)
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

	[SerializeField]
	private SettingsProvider _settingsProvider;

	[SerializeField]
	private string upscalingId;

	[SerializeField]
	private string aaID;

	[SerializeField]
	private string renderScaleId;

	[SerializeField]
	private OptionsButtonUGUIResolver upscalingResolver;

	public void UpdateUpscaling()
	{
	}

	public void RenderScaleChanged(float newScale)
	{
	}

	[IteratorStateMachine(typeof(_003CDelayedSet_003Ed__7))]
	private IEnumerator DelayedSet()
	{
		return null;
	}

	[IteratorStateMachine(typeof(_003COnRenderScaleChanged_003Ed__8))]
	private IEnumerator OnRenderScaleChanged(float scale)
	{
		return null;
	}
}
