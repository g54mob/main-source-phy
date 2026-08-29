using System;
using Battlehub.Utils;
using UnityEngine;

namespace Battlehub.RTCommon
{
	public class Splash : MonoBehaviour
	{
		[SerializeField]
		private CanvasGroup m_progress;

		[SerializeField]
		private CanvasGroup m_loadingText;

		public void Show(Action action)
		{
			if (m_progress != null)
			{
				if (Run.Instance == null)
				{
					base.gameObject.AddComponent<Run>();
				}
				FadeProgress(0f, 1f, 0f, 0.5f, m_progress, AnimationInfo<object, float>.EaseOutCubic, delegate
				{
					action();
					if (m_loadingText != null)
					{
						FadeProgress(1f, 0f, 0f, 0.7f, m_loadingText, AnimationInfo<object, float>.EaseInCubic, delegate
						{
						});
					}
					FadeProgress(1f, 0f, 0.7f, 0.5f, m_progress, AnimationInfo<object, float>.EaseInCubic, delegate
					{
						UnityEngine.Object.Destroy(base.gameObject);
					});
				});
			}
			else
			{
				action();
			}
		}

		private void FadeProgress(float from, float to, float delay, float duration, CanvasGroup group, Func<float, float> easing, Action done)
		{
			Run.Instance.Animation(new FloatAnimationInfo(from, to, duration, easing, delegate(object target, float value, float t, bool completed)
			{
				group.alpha = value;
				if (completed)
				{
					done();
				}
			})
			{
				Delay = delay
			});
		}
	}
}
