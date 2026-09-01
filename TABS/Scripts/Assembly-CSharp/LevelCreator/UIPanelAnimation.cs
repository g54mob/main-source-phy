using System;
using UnityEngine;

namespace LevelCreator
{
	[Serializable]
	public struct UIPanelAnimation
	{
		public enum TargetReference
		{
			Top = 0,
			Bottom = 1,
			Left = 2,
			Right = 3,
			Center = 4
		}

		public Vector2 m_target;

		[Tooltip("The target origo in relation to screen boundaries")]
		public TargetReference m_targetReference;

		public LeanTweenType m_easeType;

		public float m_duration;

		public Vector2 CalculateTarget()
		{
			float num = Screen.width;
			float num2 = Screen.height;
			switch (m_targetReference)
			{
			case TargetReference.Top:
				return new Vector2(m_target.x + num / 2f, m_target.y + num2);
			case TargetReference.Bottom:
				return new Vector2(m_target.x + num / 2f, m_target.y);
			case TargetReference.Left:
				return new Vector2(m_target.x, m_target.y + num2 / 2f);
			case TargetReference.Right:
				return new Vector2(m_target.x + num, m_target.y + num2 / 2f);
			case TargetReference.Center:
				return new Vector2(m_target.x + num / 2f, m_target.y + num2 / 2f);
			default:
				return m_target;
			}
		}
	}
}
