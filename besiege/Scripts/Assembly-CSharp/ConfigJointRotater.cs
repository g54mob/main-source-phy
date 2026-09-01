using UnityEngine;

[AddComponentMenu("Physics/Behaviours/ConfigJointRotater")]
public class ConfigJointRotater : MonoBehaviour
{
	[SerializeField]
	private Rigidbody rb;

	[SerializeField]
	private GameObject finishLine;

	[SerializeField]
	private ConfigurableJoint joint;

	[SerializeField]
	private float degrees;

	[SerializeField]
	private Collider[] ignoreColliderPair1;

	[SerializeField]
	private Collider[] ignoreColliderPair2;

	public bool rotate;

	public AnimationCurve speedCurve;

	public bool scaleCurveByTime = true;

	[SerializeField]
	private Vector3 rotationalAxis;

	[SerializeField]
	private float time = 1f;

	[SerializeField]
	private float angularVelocityAmount = 100f;

	[SerializeField]
	private AudioSource[] sfx = new AudioSource[0];

	[SerializeField]
	private AudioClip[] finishSfx = new AudioClip[0];

	private float[] volume = new float[0];

	private float[] pitch = new float[0];

	private float currentTime;

	private float percentTime;

	private Vector3 jointEulerRotation = Vector3.zero;

	[Header("Detect Machine")]
	public bool seeMachineToStart;

	public Vector3 detectCenter;

	public float range = 25f;

	public LayerMask mask;

	public float timeToAutoStart = 2f;

	private void Rotate()
	{
		rb.WakeUp();
		currentTime += Time.fixedDeltaTime;
		if (currentTime >= time)
		{
			StopRotate();
			if (finishLine != null)
			{
				finishLine.SetActive(false);
			}
			for (int i = 0; i < sfx.Length; i++)
			{
				sfx[i].Stop();
				sfx[i].volume = volume[i] * 2f;
				sfx[i].pitch = 0.5f;
				if (finishSfx.Length > i)
				{
					sfx[i].PlayOneShot(finishSfx[i]);
				}
			}
		}
		percentTime = currentTime / time;
		if (scaleCurveByTime)
		{
			percentTime *= speedCurve.Evaluate(percentTime);
		}
		else
		{
			percentTime = speedCurve.Evaluate(percentTime);
		}
		jointEulerRotation = rotationalAxis * degrees * percentTime;
		joint.targetRotation = Quaternion.Euler(jointEulerRotation);
		if (currentTime >= time)
		{
			joint.targetAngularVelocity = Vector3.zero;
		}
		if (rotate)
		{
			for (int j = 0; j < sfx.Length; j++)
			{
				sfx[j].volume = volume[j] * Mathf.Clamp01(rb.angularVelocity.sqrMagnitude * 1000f) * 2f;
				sfx[j].pitch = pitch[j] * Mathf.Clamp01(0.5f + rb.angularVelocity.sqrMagnitude * 100f);
			}
		}
	}

	private void Start()
	{
		joint.targetAngularVelocity = Vector3.one * angularVelocityAmount;
		if (StatMaster.levelSimulating)
		{
			for (int i = 0; i < ignoreColliderPair1.Length; i++)
			{
				Physics.IgnoreCollision(ignoreColliderPair1[i], ignoreColliderPair2[i]);
			}
			volume = new float[sfx.Length];
			pitch = new float[sfx.Length];
			for (int j = 0; j < sfx.Length; j++)
			{
				volume[j] = sfx[j].volume;
				pitch[j] = sfx[j].pitch;
				sfx[j].volume = 0f;
				sfx[j].Play();
				sfx[j].timeSamples = Random.Range(0, sfx[j].clip.samples);
			}
		}
	}

	public void StartRotate()
	{
		if (!rotate)
		{
			currentTime = 0f;
		}
		rotate = true;
		seeMachineToStart = false;
	}

	public void StopRotate()
	{
		rotate = false;
		currentTime = time;
		seeMachineToStart = false;
	}

	private void FixedUpdate()
	{
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		if (rotate)
		{
			Rotate();
		}
		else
		{
			if (!seeMachineToStart)
			{
				return;
			}
			if (timeToAutoStart < 0f)
			{
				StartRotate();
				return;
			}
			Collider[] array = Physics.OverlapSphere(detectCenter, range, mask, QueryTriggerInteraction.Ignore);
			for (int i = 0; i < array.Length; i++)
			{
				if (!(array[i].transform.root == ReferenceMaster.physicsGoalInstance) && !(array[i].transform.root == WinCondition.Instance.transform))
				{
					StartRotate();
					break;
				}
			}
			timeToAutoStart -= Time.fixedDeltaTime;
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (seeMachineToStart)
		{
			DebugExtension.DebugCircle(detectCenter, Color.magenta, range, 0f);
		}
	}
}
