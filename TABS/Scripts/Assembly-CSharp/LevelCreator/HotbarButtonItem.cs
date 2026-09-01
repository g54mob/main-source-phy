using UnityEngine;

namespace LevelCreator
{
	public class HotbarButtonItem : HotbarButton
	{
		[SerializeField]
		private float m_normalScale = 1f;

		[SerializeField]
		private float m_highlightScale = 1.5f;

		[SerializeField]
		private float m_scaleTime = 0.1f;

		public override void Select()
		{
			base.Select();
			LeanTween.scale(base.gameObject, Vector3.one * m_highlightScale, m_scaleTime);
		}

		public override void Deselect()
		{
			base.Deselect();
			LeanTween.scale(base.gameObject, Vector3.one * m_normalScale, m_scaleTime);
		}
	}
}
