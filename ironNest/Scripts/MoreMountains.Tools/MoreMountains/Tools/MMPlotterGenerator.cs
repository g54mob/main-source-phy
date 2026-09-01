using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMPlotterGenerator : MonoBehaviour
	{
		public MMPlotter PlotterPrefab;

		public Vector2 Spacing;

		public float VerticalOddSpacing;

		public int RowLength;

		[Header("Materials")]
		public Material LinearMaterial;

		public Material QuadraticMaterial;

		public Material CubicMaterial;

		public Material QuarticMaterial;

		public Material QuinticMaterial;

		public Material SinusoidalMaterial;

		public Material BounceMaterial;

		public Material OverheadMaterial;

		public Material ExponentialMaterial;

		public Material ElasticMaterial;

		public Material CircularMaterial;

		protected Vector2 _position;

		[MMInspectorButton("GeneratePlotters")]
		public bool GeneratePlottersButton;

		protected virtual void Awake()
		{
		}

		protected virtual void GeneratePlotters()
		{
		}
	}
}
