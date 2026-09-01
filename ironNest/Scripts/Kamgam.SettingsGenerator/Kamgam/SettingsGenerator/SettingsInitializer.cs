using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

namespace Kamgam.SettingsGenerator
{
	[DefaultExecutionOrder(-10)]
	public class SettingsInitializer : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003ConInstanceReloaded_003Ed__15 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public SettingsInitializer _003C_003E4__this;

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
			public _003ConInstanceReloaded_003Ed__15(int _003C_003E1__state)
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

		private static SettingsInitializer _instance;

		[Tooltip("Don't forget to hook this up with the right provider.")]
		public SettingsProvider Provider;

		[Tooltip("Enable if you are unloading the scene that contains the initializer.\nIf you can then disable this and use additive scene loading instead.")]
		public bool DoNotDestroy;

		[Tooltip("Used only if DoNotDestroy is enabled.\nIf enabled then it will re-apply the settings in Start() after reloading this scene.")]
		public bool ApplyOnReload;

		[Tooltip("Use this to register event methods that should be executed BEFORE the settings are initialized.")]
		public UnityEvent PreInitializationEvents;

		private static WaitForEndOfFrame _waitForEndOfFrame;

		public static SettingsInitializer Instance => null;

		public static bool Exists => false;

		public static Settings Settings => null;

		public static bool HasSettings()
		{
			return false;
		}

		public void Awake()
		{
		}

		public void Start()
		{
		}

		[IteratorStateMachine(typeof(_003ConInstanceReloaded_003Ed__15))]
		private IEnumerator onInstanceReloaded()
		{
			return null;
		}
	}
}
