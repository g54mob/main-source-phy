using UnityEngine;

namespace LevelCreator
{
	public class ScaleLerp : MonoBehaviour
	{
		[SerializeField]
		private Vector3 targetScale = Vector3.one;

		[SerializeField]
		private float speed = 1f;

		private void Update()
		{
			float num = 1f - Mathf.Pow(1E-05f, Time.deltaTime);
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, targetScale, num * speed);
			if (base.transform.localScale == targetScale)
			{
				Object.Destroy(this);
			}
		}
	}
}
