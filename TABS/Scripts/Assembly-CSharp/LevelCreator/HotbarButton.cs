using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelCreator
{
	[RequireComponent(typeof(LayoutElement))]
	public abstract class HotbarButton : MonoBehaviour
	{
		public Image Icon;

		public LocalizeText Name;

		public TextMeshProUGUI SizeInfo;

		public Image Highlight;

		public float itemWidth;

		[HideInInspector]
		public RectTransform hotbarItemsTransform;

		protected LayoutElement layoutElement;

		public virtual void Select()
		{
			base.transform.localScale = Vector3.one;
			Highlight.gameObject.SetActive(value: true);
			Utility.PlaySound("UI/Hover", 1f, base.transform.position);
		}

		public virtual void Deselect()
		{
			Highlight.gameObject.SetActive(value: false);
		}

		private void Awake()
		{
			layoutElement = GetComponent<LayoutElement>();
			layoutElement.preferredWidth = itemWidth;
		}
	}
}
