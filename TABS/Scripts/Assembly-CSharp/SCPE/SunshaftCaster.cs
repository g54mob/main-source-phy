using UnityEngine;

namespace SCPE
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(Light))]
	internal sealed class SunshaftCaster : MonoBehaviour
	{
		[Range(0f, 10000f)]
		public float distance = 10000f;

		[Tooltip("Use this to match the casting position to a skybox sun")]
		public bool infiniteDistance;

		[Tooltip("This light will be used to sample the intensity if color")]
		public Light sunLight;

		private Vector3 sunPosition;

		public static Color color;

		public static float intensity;

		private void OnEnable()
		{
			sunPosition = base.transform.position;
			if (!sunLight)
			{
				sunLight = GetComponent<Light>();
				if ((bool)sunLight)
				{
					color = sunLight.color;
					intensity = sunLight.intensity;
				}
			}
		}

		private void OnDisable()
		{
			sunPosition = Vector3.zero;
			Sunshafts.sunPosition = Vector3.zero;
		}

		private void OnDrawGizmos()
		{
			Gizmos.DrawIcon(Sunshafts.sunPosition, "LensFlare Icon", allowScaling: true);
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = new Color(1f, 1f, 0f, 0.5f);
			Gizmos.DrawRay(base.transform.position, sunPosition);
		}

		private void Update()
		{
			sunPosition = -base.transform.forward * (infiniteDistance ? 1E+10f : distance);
			Sunshafts.sunPosition = sunPosition;
			if ((bool)sunLight)
			{
				color = sunLight.color;
				intensity = sunLight.intensity;
			}
		}
	}
}
