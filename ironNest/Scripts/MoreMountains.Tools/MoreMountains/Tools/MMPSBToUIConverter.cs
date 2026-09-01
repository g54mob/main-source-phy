using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	[AddComponentMenu("More Mountains/Tools/GUI/MMPSBToUIConverter")]
	public class MMPSBToUIConverter : MonoBehaviour
	{
		[Header("Target")]
		public Canvas TargetCanvas;

		public float ScaleFactor;

		public bool ReplicateNesting;

		[Header("Size")]
		public float TargetWidth;

		public float TargetHeight;

		[Header("Conversion")]
		[MMInspectorButton("ConvertToCanvas")]
		public bool ConvertToCanvasButton;

		public Vector3 ChildImageOffset;

		protected Transform _topLevel;

		protected Dictionary<Transform, int> _sortingOrders;

		public virtual void ConvertToCanvas()
		{
		}

		protected virtual void CreateImageForChildren(Transform root, Transform parent)
		{
		}

		protected virtual void SetupForStretch(RectTransform rect)
		{
		}
	}
}
