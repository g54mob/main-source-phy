using UnityEngine;

public class AnimateAlongCurve : MonoBehaviour
{
	public Camera HudCam;

	public Camera MainCam;

	public Transform start;

	public Transform end;

	public Vector3 endOffset = Vector3.zero;

	public float time = 3f;

	private float elapsedTime;

	public AnimationCurve X;

	public AnimationCurve Y;

	public AnimationCurve Z;

	private bool done;

	private Vector3 dir;

	private Vector3 startPos;

	private Vector3 endPos;

	public bool endHUDToMAIN;

	[Header("Begin at end")]
	public GameObject[] enableAtEndofAnimation;

	[Header("Stop at end")]
	public MeshRenderer waterSphere;

	public ParticleSystem[] trailParticles;

	public GameObject[] otherObjectsToDisable;

	private Vector3 prevPos;

	private void Start()
	{
		if (endHUDToMAIN)
		{
			endPos = HudCam.WorldToViewportPoint(end.position);
			endPos = MainCam.ViewportToWorldPoint(endPos);
		}
		else
		{
			endPos = end.position;
		}
		endPos += endOffset;
		startPos = start.position;
		prevPos = startPos;
		dir = endPos - startPos;
	}

	private void Update()
	{
		if (elapsedTime > time)
		{
			elapsedTime = time;
			done = true;
		}
		if (endHUDToMAIN)
		{
			endPos = HudCam.WorldToViewportPoint(end.position);
			endPos = MainCam.ViewportToWorldPoint(endPos);
			endPos += endOffset;
			dir = endPos - startPos;
		}
		Vector3 vector = new Vector3(dir.x * X.Evaluate(elapsedTime / time), dir.y * Y.Evaluate(elapsedTime / time), dir.z * Z.Evaluate(elapsedTime / time));
		elapsedTime += Time.deltaTime;
		base.transform.position = startPos + vector;
		Quaternion rotation = base.transform.rotation;
		rotation.SetLookRotation((base.transform.position - prevPos).normalized);
		base.transform.rotation = rotation;
		if (done)
		{
			for (int i = 0; i < enableAtEndofAnimation.Length; i++)
			{
				enableAtEndofAnimation[i].SetActive(true);
			}
			for (int j = 0; j < trailParticles.Length; j++)
			{
				trailParticles[j].Stop();
			}
			for (int k = 0; k < otherObjectsToDisable.Length; k++)
			{
				otherObjectsToDisable[k].SetActive(false);
			}
			waterSphere.enabled = false;
			base.enabled = false;
		}
		prevPos = base.transform.position;
	}
}
