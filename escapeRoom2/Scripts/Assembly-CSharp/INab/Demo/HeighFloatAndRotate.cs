using UnityEngine;

namespace INab.Demo
{
	[ExecuteAlways]
	public class HeighFloatAndRotate : MonoBehaviour
	{
		public enum Axis
		{
			X = 0,
			Y = 1,
			Z = 2
		}

		public float rotationSpeed = 100f;

		public float floatSpeed = 0.5f;

		public float floatHeightMin;

		public float floatHeightMax = 1.5f;

		public Axis axis = Axis.Y;

		public bool updateInEditor;

		private void Update()
		{
			if (updateInEditor || Application.isPlaying)
			{
				switch (axis)
				{
				case Axis.X:
					base.transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime, Space.World);
					break;
				case Axis.Y:
					base.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
					break;
				case Axis.Z:
					base.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime, Space.World);
					break;
				}
				Vector3 localPosition = Vector3.zero;
				float num = Mathf.Lerp(floatHeightMin, floatHeightMax, Mathf.Sin(Time.time * floatSpeed) * 0.5f + 0.5f);
				switch (axis)
				{
				case Axis.X:
					localPosition = new Vector3(num, base.transform.localPosition.y, base.transform.localPosition.z);
					break;
				case Axis.Y:
					localPosition = new Vector3(base.transform.localPosition.x, num, base.transform.localPosition.z);
					break;
				case Axis.Z:
					localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, num);
					break;
				}
				base.transform.localPosition = localPosition;
			}
		}
	}
}
