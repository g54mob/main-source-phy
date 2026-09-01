using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Kamgam.SettingsGenerator;
using UnityEngine;

public class AntialiasingOptionsHelper : MonoBehaviour
{
	[CompilerGenerated]
	private sealed class _003CDelayedSet_003Ed__7 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public AntialiasingOptionsHelper _003C_003E4__this;

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

	[SerializeField]
	private SettingsProvider _settingsProvider;

	[SerializeField]
	private string aaId;

	[SerializeField]
	private string renderScaleId;

	[SerializeField]
	private string upscalingID;

	[SerializeField]
	private OptionsButtonUGUIResolver aaResolver;

	[SerializeField]
	private SliderUGUIResolver upscalingResolver;

	public void UpdateAntialiasing()
	{
	}

	[IteratorStateMachine(typeof(_003CDelayedSet_003Ed__7))]
	private IEnumerator DelayedSet()
	{
		return null;
	}
}
