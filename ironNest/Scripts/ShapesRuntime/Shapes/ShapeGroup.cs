using System;
using UnityEngine;

namespace Shapes
{
	[ExecuteAlways]
	public class ShapeGroup : MonoBehaviour
	{
		public static int shapeGroupsInScene;

		[ShapesColorField(true)]
		[SerializeField]
		private Color color;

		[field: NonSerialized]
		internal bool IsEnabled { get; private set; }

		public Color Color
		{
			get
			{
				return default(Color);
			}
			set
			{
			}
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		private void OnValidate()
		{
		}

		private void UpdateChildShapes()
		{
		}
	}
}
