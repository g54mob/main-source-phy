using UnityEngine;

namespace MoreMountains.Tools
{
	[RequireComponent(typeof(LineRenderer))]
	public class MMLineRendererCircle : MonoBehaviour
	{
		public enum DrawAxis
		{
			X = 0,
			Y = 1,
			Z = 2
		}

		[Header("Draw Axis")]
		[Tooltip("the axis on which to draw the circle")]
		public DrawAxis Axis;

		[Tooltip("the distance by which to push the circle on the draw axis")]
		public float NormalOffset;

		[Header("Geometry")]
		[Tooltip("the amount of segments on the line renderer. More segments, more smoothness, more performance cost")]
		[Range(0f, 2000f)]
		public int PositionsCount;

		[Header("Shape")]
		[Tooltip("the length of the circle's horizontal radius")]
		public float HorizontalRadius;

		[Tooltip("the length of the circle's vertical radius")]
		public float VerticalRadius;

		[Header("Debug")]
		[Tooltip("if this is true, the circle will be redrawn every time you change a value in the inspector, otherwise you'll have to call the DrawCircle method (or press the debug button below)")]
		public bool AutoRedrawOnValuesChange;

		[MMInspectorButton("DrawCircle")]
		public bool DrawCircleButton;

		protected LineRenderer _line;

		protected Vector3 _newPosition;

		protected float _angle;

		protected float _x;

		protected float _y;

		protected float _z;

		protected virtual void Awake()
		{
		}

		protected virtual void Initialization()
		{
		}

		public virtual void DrawCircle()
		{
		}

		protected virtual float ComputeX()
		{
			return 0f;
		}

		protected virtual float ComputeY()
		{
			return 0f;
		}

		protected virtual void DrawCircleX()
		{
		}

		protected virtual void DrawCircleY()
		{
		}

		protected virtual void DrawCircleZ()
		{
		}

		protected virtual void OnValidate()
		{
		}
	}
}
