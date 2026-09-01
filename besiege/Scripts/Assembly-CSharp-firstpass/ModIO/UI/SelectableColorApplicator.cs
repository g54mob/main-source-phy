using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Selectable))]
	public class SelectableColorApplicator : MonoBehaviour
	{
		public Graphic[] innerElements = new Graphic[0];

		public SelectableColorScheme scheme;

		private Selectable selectable
		{
			get
			{
				return base.gameObject.GetComponent<Selectable>();
			}
		}

		private void Start()
		{
			UpdateColorScheme();
		}

		public void UpdateColorScheme()
		{
			if (selectable == null || scheme == null)
			{
				return;
			}
			if (selectable.targetGraphic != null)
			{
				selectable.targetGraphic.color = scheme.imageColor;
			}
			Graphic[] array = innerElements;
			foreach (Graphic graphic in array)
			{
				if (graphic != null)
				{
					graphic.color = scheme.innerElementColor;
				}
			}
			Toggle toggle = selectable as Toggle;
			if (toggle != null && toggle.graphic != null)
			{
				toggle.graphic.color = scheme.toggleColor;
			}
			selectable.colors = scheme.functionalColors;
		}
	}
}
