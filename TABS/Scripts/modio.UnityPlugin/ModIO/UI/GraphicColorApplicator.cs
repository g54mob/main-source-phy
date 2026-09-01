using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
	[ExecuteAlways]
	[RequireComponent(typeof(Graphic))]
	public class GraphicColorApplicator : MonoBehaviour
	{
		public GraphicColorScheme scheme;

		public Graphic[] innerElements = new Graphic[0];

		private Graphic graphic => base.gameObject.GetComponent<Graphic>();

		private void Start()
		{
			UpdateColorScheme();
		}

		public void UpdateColorScheme()
		{
			if (this.graphic == null || scheme == null)
			{
				return;
			}
			this.graphic.color = scheme.baseColor;
			Graphic[] array = innerElements;
			foreach (Graphic graphic in array)
			{
				if (graphic != null)
				{
					graphic.color = scheme.innerElementColor;
				}
			}
		}

		public void SetColorScheme(GraphicColorScheme scheme)
		{
			this.scheme = scheme;
			UpdateColorScheme();
		}
	}
}
