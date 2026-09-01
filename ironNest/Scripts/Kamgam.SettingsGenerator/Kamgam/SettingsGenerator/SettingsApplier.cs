using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kamgam.SettingsGenerator
{
	public class SettingsApplier : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CStart_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public SettingsApplier _003C_003E4__this;

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
			public _003CStart_003Ed__9(int _003C_003E1__state)
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

		public static List<SettingsApplier> Appliers;

		public SettingsProvider Provider;

		[Header("Start")]
		public bool ApplyOnStart;

		[Tooltip("On start delay in seconds.")]
		public float ApplyOnStartDelay;

		[Header("Update")]
		[Tooltip("Only use this as a last resort if another system keeps overriding your settings.\nYou really should find out what system that is and route the settings through that instead of using this.")]
		public bool ApplyOnLateUpdate;

		[Header("Limit applied settings")]
		[Tooltip("Leave empty to apply all settings")]
		public List<string> SettingIds;

		public void OnEnable()
		{
		}

		public static SettingsApplier GetApplier(Scene? scene = null)
		{
			return null;
		}

		public static SettingsApplier CreateApplier(SettingsProvider provider, Scene? scene = null)
		{
			return null;
		}

		[IteratorStateMachine(typeof(_003CStart_003Ed__9))]
		public IEnumerator Start()
		{
			return null;
		}

		public void LateUpdate()
		{
		}

		public void Apply()
		{
		}

		public void OnDisable()
		{
		}
	}
}
