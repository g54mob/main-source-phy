using TMPro;
using UnityEngine;

namespace Shapes
{
	public class TextMeshProShapes : TextMeshPro
	{
		[SerializeField]
		protected float curvature;

		[SerializeField]
		protected Vector2 curvaturePivot;

		public float Curvature
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		public Vector2 CurvaturePivot
		{
			get
			{
				return default(Vector2);
			}
			set
			{
			}
		}

		protected override void OnEnable()
		{
		}

		protected override void OnDisable()
		{
		}

		private void ApplyDeformation(TMP_TextInfo obj)
		{
		}

		private static Vector3 Bend(Vector3 p, float curvature)
		{
			return default(Vector3);
		}
	}
}
