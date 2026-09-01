using System;
using UnityEngine;

namespace Dreamteck.Splines
{
	[Serializable]
	public class SplineTrigger : ScriptableObject
	{
		public enum Type
		{
			Double = 0,
			Forward = 1,
			Backward = 2
		}

		[SerializeField]
		public Type type;

		[Range(0f, 1f)]
		public double position = 0.5;

		[SerializeField]
		public bool enabled = true;

		[SerializeField]
		public Color color = Color.white;

		[SerializeField]
		[HideInInspector]
		public SplineAction[] actions = new SplineAction[0];

		public GameObject[] gameObjects;
	}
}
