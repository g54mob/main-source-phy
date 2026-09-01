using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/Achievements/MMAchievementDisplayer")]
	public class MMAchievementDisplayer : MonoBehaviour, MMEventListener<MMAchievementUnlockedEvent>, MMEventListenerBase
	{
		[CompilerGenerated]
		private sealed class _003CDisplayAchievement_003Ed__4 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public MMAchievementDisplayer _003C_003E4__this;

			public MMAchievement achievement;

			private CanvasGroup _003CachievementCanvasGroup_003E5__2;

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
			public _003CDisplayAchievement_003Ed__4(int _003C_003E1__state)
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

		[Header("Achievements")]
		public MMAchievementDisplayItem AchievementDisplayPrefab;

		public float AchievementDisplayDuration;

		public float AchievementFadeDuration;

		protected WaitForSeconds _achievementFadeOutWFS;

		[IteratorStateMachine(typeof(_003CDisplayAchievement_003Ed__4))]
		public virtual IEnumerator DisplayAchievement(MMAchievement achievement)
		{
			return null;
		}

		public virtual void OnMMEvent(MMAchievementUnlockedEvent achievementUnlockedEvent)
		{
		}

		protected virtual void OnEnable()
		{
		}

		protected virtual void OnDisable()
		{
		}
	}
}
