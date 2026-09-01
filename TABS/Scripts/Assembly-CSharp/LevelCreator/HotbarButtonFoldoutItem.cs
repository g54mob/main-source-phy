using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	public class HotbarButtonFoldoutItem : HotbarButton
	{
		[SerializeField]
		private float m_foldoutWidth = 85f;

		[SerializeField]
		private float m_foldoutTime = 0.2f;

		public override void Select()
		{
			base.Select();
			LeanTween.value(layoutElement.preferredWidth, m_foldoutWidth, m_foldoutTime).setOnUpdate(delegate(float value)
			{
				layoutElement.preferredWidth = value;
				LayoutRebuilder.ForceRebuildLayoutImmediate(hotbarItemsTransform);
			}).setEaseOutExpo();
			Icon.color = DMEditorColors.DarkNormalColor;
			Icon.GetComponent<Shadow>().enabled = false;
			Name.Text.color = DMEditorColors.NormalColor;
		}

		public override void Deselect()
		{
			base.Deselect();
			LeanTween.value(layoutElement.preferredWidth, itemWidth, m_foldoutTime).setOnUpdate(delegate(float value)
			{
				layoutElement.preferredWidth = value;
				LayoutRebuilder.ForceRebuildLayoutImmediate(hotbarItemsTransform);
			}).setEaseOutExpo();
			Icon.color = Color.white;
			Icon.GetComponent<Shadow>().enabled = true;
			Name.Text.color = Color.clear;
		}
	}
}
