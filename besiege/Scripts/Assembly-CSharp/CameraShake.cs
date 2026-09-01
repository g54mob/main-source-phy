using UnityEngine;

public class CameraShake : MonoBehaviour
{
	public static bool shakeBig;

	public static bool shakeMed;

	public static bool shakeSmall;

	public float amount = 1f;

	public float decay = 1f;

	private Vector3 originPosition;

	private Quaternion originRotation;

	private float shake_decay;

	private float shake_intensity;

	private void Update()
	{
		if (shakeSmall)
		{
			shakeSmall = false;
			Shake(0.5f);
		}
		if (shakeMed)
		{
			shakeMed = false;
			Shake(1f);
		}
		if (shakeBig)
		{
			shakeBig = false;
			Shake(1.6f);
		}
		if (shake_intensity > 0f)
		{
			base.transform.localPosition = originPosition + Random.insideUnitSphere * shake_intensity;
			base.transform.localRotation = new Quaternion(originRotation.x + Random.Range(0f - shake_intensity, shake_intensity) * 0.2f, originRotation.y + Random.Range(0f - shake_intensity, shake_intensity) * 0.2f, originRotation.z + Random.Range(0f - shake_intensity, shake_intensity) * 0.2f, originRotation.w + Random.Range(0f - shake_intensity, shake_intensity) * 0.2f);
			shake_intensity -= shake_decay * Time.deltaTime;
		}
	}

	private void Shake(float intensity)
	{
		originPosition = base.transform.localPosition;
		originRotation = base.transform.localRotation;
		shake_intensity = 0.3f * amount * intensity;
		shake_decay = decay;
	}
}
