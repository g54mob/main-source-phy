using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMParallaxUI : MonoBehaviour
	{
		[Serializable]
		public class ParallaxLayer
		{
			public RectTransform Rect;

			public float Speed;

			public float Amplitude;

			[HideInInspector]
			public Vector2 StartPosition;

			public bool Active;
		}

		public enum Modes
		{
			Mouse = 0,
			Gyroscope = 1,
			Script = 2
		}

		public Modes Mode;

		public float AmplitudeMultiplier;

		public float SpeedMultiplier;

		public List<ParallaxLayer> ParallaxLayers;

		protected Vector2 _referencePosition;

		protected Vector3 _newPosition;

		protected Vector2 _mousePosition;

		protected virtual void Start()
		{
		}

		public virtual void Initialization()
		{
		}

		protected virtual void Update()
		{
		}

		protected virtual void MoveLayers()
		{
		}

		public virtual void SetReferencePosition(Vector3 newReferencePosition)
		{
		}
	}
}
