using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Poly.Game
{
	public class AccelerationMeter : MonoBehaviour
	{
		public Transform t;

		public Text text;

		public Font handlesFont;

		public bool horizontalMeasurementOnly;

		[NonSerialized]
		public int numFramesToAverage = 25;

		private Vector3 prevPosition;

		private Vector3 prevVelocity;

		private List<float> velocities = new List<float>();

		private List<float> accelerations = new List<float>();

		private string lastDisplayString = "";

		private void Start()
		{
			if ((bool)t)
			{
				prevPosition = t.position;
			}
		}

		private void FixedUpdate()
		{
			if ((bool)t)
			{
				Vector3 vector = (t.position - prevPosition) / Time.fixedDeltaTime;
				float item = (vector - prevVelocity).magnitude / Time.fixedDeltaTime;
				float item2 = vector.magnitude;
				if (horizontalMeasurementOnly)
				{
					item2 = Vector3.Dot(vector, t.right);
					item = Vector3.Dot(vector - prevVelocity, t.right) / Time.fixedDeltaTime;
				}
				prevPosition = t.position;
				prevVelocity = vector;
				velocities.Add(item2);
				if (velocities.Count > numFramesToAverage)
				{
					velocities.RemoveAt(0);
				}
				accelerations.Add(item);
				if (accelerations.Count > numFramesToAverage)
				{
					accelerations.RemoveAt(0);
				}
				item2 = velocities.Average();
				item = accelerations.Average();
				lastDisplayString = $"Velocity     {item2:0.0}\r\n";
				lastDisplayString += $"Acceleration {item:0.0}";
				if ((bool)text)
				{
					text.text = lastDisplayString;
				}
			}
			else
			{
				text.text = "";
			}
		}

		private void OnDrawGizmos()
		{
			GUIStyle gUIStyle = new GUIStyle();
			gUIStyle.normal.textColor = Color.white;
			gUIStyle.alignment = TextAnchor.MiddleCenter;
			if ((bool)handlesFont)
			{
				gUIStyle.font = handlesFont;
			}
			_ = t.position + Vector3.up;
		}
	}
}
