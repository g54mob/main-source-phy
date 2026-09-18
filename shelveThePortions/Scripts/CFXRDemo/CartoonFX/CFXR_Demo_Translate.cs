using UnityEngine;

namespace CartoonFX
{
	public class CFXR_Demo_Translate : MonoBehaviour
	{
		public Vector3 direction = new Vector3(0f, 1f, 0f);

		public bool randomRotation;

		private bool initialized;

		private Vector3 initialPosition;

		private void Awake()
		{
			if (!initialized)
			{
				initialized = true;
				initialPosition = base.transform.position;
			}
		}

		private void OnEnable()
		{
			base.transform.position = initialPosition;
			if (randomRotation)
			{
				base.transform.eulerAngles = Vector3.Lerp(Vector3.zero, Vector3.up * 360f, Random.value);
			}
		}

		private void Update()
		{
			base.transform.Translate(direction * Time.deltaTime);
		}
	}
}
