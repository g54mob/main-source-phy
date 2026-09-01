using UnityEngine;

public class CustomAudioFalloff : MonoBehaviour
{
	public AnimationCurve fallofCurve;

	public float volume = 0.5f;

	public float yDistFactor = 1f;

	public float radius = 10f;

	public float minRadius = 5f;

	private Camera cam;

	private AudioSource audioSource;

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
		cam = Object.FindObjectOfType<MainCam>().m_camera;
	}

	private void Update()
	{
		Vector3 position = cam.transform.position;
		Vector3 position2 = base.transform.position;
		position.y *= yDistFactor;
		position2.y *= yDistFactor;
		float num = Vector3.Distance(position, position2);
		float num2 = 0f;
		if (num < radius)
		{
			num2 = fallofCurve.Evaluate(Mathf.InverseLerp(radius, minRadius, num));
		}
		audioSource.volume = num2 * volume;
		audioSource.spatialBlend = 1f - num2;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawWireSphere(base.transform.position, radius);
		Gizmos.DrawWireSphere(base.transform.position, minRadius);
	}
}
