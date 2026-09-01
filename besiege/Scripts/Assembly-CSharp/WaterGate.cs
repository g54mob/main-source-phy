using System.Collections;
using UnityEngine;

public class WaterGate : MonoBehaviour
{
	[SerializeField]
	protected int objectiveObjCount = 25;

	[SerializeField]
	protected float winDelay = 2f;

	[Header("Gate")]
	[SerializeField]
	protected Rigidbody gate;

	[SerializeField]
	protected Vector3 localGateOpenPosition;

	private float previousGatePositionY;

	[Header("Big Cog")]
	[SerializeField]
	protected Rigidbody bigCogRB;

	[SerializeField]
	protected HingeJoint bigCogJoint;

	[SerializeField]
	protected float bigCogRotationAmount = 0.25f;

	[SerializeField]
	protected ParticleSystem[] bigCogMoveParticles;

	[SerializeField]
	protected AudioClip bigCogCircleCompleteSound;

	private Transform bigCogTransfrom;

	private float bigCogRotationMax;

	private float bigCogRotationDirection = 1f;

	private Vector3 bigCogPrevRight;

	private float gatePrevPos;

	private float bigCogRotaionAmountToObjective;

	private float bigCogObjectiveDegrees;

	private float bigCogAngle;

	private float bigCogRotationPercentDiff;

	private Quaternion bigStartRotation;

	[SerializeField]
	[Header("Audio Settings")]
	protected float volumeLerpSpeed = 15f;

	[SerializeField]
	protected AudioSource audioSource;

	[SerializeField]
	protected float audiofalloffDelay = 0.5f;

	private float[] pitchArray = new float[10];

	private int k;

	private float targetVolume;

	private float startVolume;

	[Header("Other stuff")]
	[SerializeField]
	protected AudioSource completeSFX;

	[SerializeField]
	protected ParticleSystem[] completeParticles;

	private Vector3 gatePosition;

	private Vector3 gateStartPosition;

	private float amountToObjective;

	private bool runningLastSpurt;

	private void Awake()
	{
		Object.FindObjectOfType<WinCondition>().objectiveObjectCount = objectiveObjCount + 1;
		bigCogTransfrom = bigCogRB.transform;
		bigCogRotationMax = bigCogRotationAmount;
		amountToObjective = localGateOpenPosition.y / (float)objectiveObjCount;
		gatePosition = gate.transform.position;
		previousGatePositionY = gatePosition.y;
		gateStartPosition = gate.transform.position;
		bigStartRotation = bigCogTransfrom.rotation;
	}

	private void Start()
	{
		if (StatMaster.levelSimulating)
		{
			k = 0;
			if (audioSource == null)
			{
				audioSource = GetComponent<AudioSource>();
			}
			startVolume = 0.35f;
			audioSource.volume = 0f;
			audioSource.Play();
			bigCogPrevRight = bigCogTransfrom.right;
			JointLimits limits = default(JointLimits);
			bigCogJoint.useLimits = true;
			float num = bigCogRotationAmount * 1.1f * 360f;
			limits.max = ((!(num >= 0f)) ? 0f : num);
			limits.min = ((!(num < 0f)) ? 0f : num);
			bigCogJoint.limits = limits;
		}
	}

	private void Update()
	{
		SetAudioVolume(bigCogRB.angularVelocity.sqrMagnitude + bigCogRB.angularVelocity.sqrMagnitude);
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		float num = gate.transform.position.y - previousGatePositionY;
		if (Mathf.Abs(num) > amountToObjective)
		{
			bool flag = num > 0f;
			WinCondition.currentObjsCompleted += (flag ? 1 : (-1));
			previousGatePositionY += ((!flag) ? (0f - amountToObjective) : amountToObjective);
		}
		if (localGateOpenPosition.y < gate.transform.position.y && !bigCogRB.isKinematic)
		{
			bigCogRB.isKinematic = true;
			bigCogRB.constraints = RigidbodyConstraints.FreezeAll;
			for (int i = 0; i < bigCogMoveParticles.Length; i++)
			{
				bigCogMoveParticles[i].Stop();
			}
			gate.isKinematic = true;
		}
		if (localGateOpenPosition.y < gate.transform.position.y && bigCogRB.isKinematic && !runningLastSpurt)
		{
			StartCoroutine(LastSpurt());
		}
		BigCogUpdate();
		SyncRotations();
	}

	private void BigCogUpdate()
	{
		if (!(bigCogRotationAmount >= 0f))
		{
			return;
		}
		bigCogAngle = Vector3.Angle(bigCogTransfrom.right, bigCogPrevRight);
		bigCogRotationDirection = ((!(Vector3.Dot(bigCogTransfrom.forward, bigCogPrevRight) > 0f)) ? (-1f) : 1f);
		float num = bigCogAngle / 360f * bigCogRotationDirection;
		if (bigCogRotationAmount - num < 0f)
		{
			num = bigCogRotationAmount;
		}
		bigCogRotationAmount -= num;
		bigCogJoint.useLimits = bigCogRotationMax - bigCogRotationAmount < 0.1f;
		bigCogRotationPercentDiff = num / bigCogRotationMax;
		for (int i = 0; i < bigCogMoveParticles.Length; i++)
		{
			if (bigCogAngle > 0.01f)
			{
				bigCogMoveParticles[i].Play();
			}
			else
			{
				bigCogMoveParticles[i].Stop();
			}
		}
		if (bigCogAngle > 0.01f && audioSource.isPlaying)
		{
			if (k >= pitchArray.Length)
			{
				k = 0;
			}
			pitchArray[k] = num * 2.55f + 0.85f;
			float num2 = 0f;
			for (int j = 0; j < pitchArray.Length; j++)
			{
				num2 += pitchArray[j];
			}
			k++;
		}
	}

	private void SyncRotations()
	{
		float num = gatePrevPos - gate.transform.position.y;
		gatePosition = gate.transform.position;
		bool flag = num > 0.01f || num < -0.01f;
		if (bigCogAngle > 0.01f && !flag)
		{
			gatePosition.y += localGateOpenPosition.y * bigCogRotationPercentDiff;
			gate.MovePosition(gatePosition);
		}
		if (flag)
		{
			float num2 = (gate.position.y - gateStartPosition.y) / localGateOpenPosition.y;
			bigCogRB.transform.rotation = Quaternion.AngleAxis(360f * bigCogRotationMax * num2, bigCogRB.transform.up) * bigStartRotation;
		}
		gatePrevPos = gate.position.y;
		bigCogPrevRight = bigCogTransfrom.right;
	}

	private void FixedUpdate()
	{
		gatePrevPos = gate.position.y;
		bigCogPrevRight = bigCogTransfrom.right;
	}

	private void SetAudioVolume(float ang)
	{
		if (ang > 0.1f)
		{
			targetVolume = startVolume;
		}
		else
		{
			targetVolume = 0f;
		}
		audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, Time.deltaTime * volumeLerpSpeed);
	}

	private IEnumerator LastSpurt()
	{
		runningLastSpurt = true;
		if (completeSFX != null)
		{
			completeSFX.Play();
		}
		audioSource.Stop();
		for (int i = 0; i < completeParticles.Length; i++)
		{
			completeParticles[i].Play();
		}
		yield return new WaitForSeconds(winDelay);
		WinCondition.currentObjsCompleted = objectiveObjCount + 1;
		base.enabled = false;
	}
}
