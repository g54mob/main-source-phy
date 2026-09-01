using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Kamgam.SettingsGenerator.Examples
{
	public class SceneLoader : MonoBehaviour
	{
		[CompilerGenerated]
		private sealed class _003CLoadDelayed_003Ed__5 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public float delay;

			public SceneLoader _003C_003E4__this;

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
			public _003CLoadDelayed_003Ed__5(int _003C_003E1__state)
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

		public string SceneName;

		public bool LoadAdditively;

		public float Delay;

		protected SettingInt audioMusicVolumeSetting;

		private void Start()
		{
		}

		[IteratorStateMachine(typeof(_003CLoadDelayed_003Ed__5))]
		public IEnumerator LoadDelayed(float delay)
		{
			return null;
		}

		public void Load()
		{
		}
	}
}
