using System.Collections.Generic;
using UnityEngine;

namespace Battlehub.Utils
{
	[ExecuteInEditMode]
	public class Run : MonoBehaviour
	{
		private static Run m_instance;

		private List<IAnimationInfo> m_animations;

		public static Run Instance => m_instance;

		public void Animation(IAnimationInfo animation)
		{
			if (!m_animations.Contains(animation))
			{
				m_animations.Add(animation);
			}
		}

		public void Remove(IAnimationInfo animation)
		{
			m_animations.Remove(animation);
		}

		private void Awake()
		{
			m_instance = this;
			m_animations = new List<IAnimationInfo>();
		}

		private void Update()
		{
			for (int i = 0; i < m_animations.Count; i++)
			{
				IAnimationInfo animationInfo = m_animations[i];
				if (animationInfo.Delay > 0f)
				{
					animationInfo.Delay -= Time.unscaledDeltaTime;
				}
				else
				{
					animationInfo.T += Time.unscaledDeltaTime;
				}
				if (animationInfo.T >= animationInfo.Duration)
				{
					m_animations.Remove(animationInfo);
				}
			}
		}
	}
}
