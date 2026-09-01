using UnityEngine;

namespace Landfall.TABC
{
	public class ScaleShake : MonoBehaviour
	{
		[Header("TABC")]
		public bool isMain;

		public static ScaleShake instance;

		public float multiplier = 1f;

		private float velocity;

		public float drag = 1f;

		public float spring = 1f;

		private float startLocal;

		public bool removeOnSleep;

		private void Awake()
		{
			if (isMain)
			{
				instance = this;
			}
			startLocal = base.transform.localScale.x;
		}

		private void Update()
		{
			float num = Mathf.Clamp(Time.deltaTime, 0f, 0.02f);
			velocity += (startLocal - base.transform.localScale.x) * num * 50f * spring;
			velocity -= drag * velocity * 20f * num;
			base.transform.localScale += Vector3.one * (velocity * 10f * num);
			if (removeOnSleep && Mathf.Abs(base.transform.localScale.x - startLocal) < 0.01f && velocity < 0.01f)
			{
				Object.Destroy(this);
			}
		}

		public void AddForce(float force)
		{
			velocity += force * multiplier * 5f;
		}

		public void SetTarget(float newTarget)
		{
			startLocal = newTarget;
		}
	}
}
