using System.Collections;
using UnityEngine;

public class TempleEye : MonoBehaviour
{
	[SerializeField]
	protected int objectiveObjCount = 25;

	[SerializeField]
	protected float winDelay = 2f;

	[SerializeField]
	[Header("Inner Circle")]
	protected Rigidbody innerRB;

	[SerializeField]
	protected HingeJoint innerJoint;

	[SerializeField]
	protected float innerRotationAmount = 0.25f;

	[SerializeField]
	protected ParticleSystem[] innerCircleMoveParticles;

	[SerializeField]
	protected AudioClip innerCircleCompleteSound;

	private Transform innerTransfrom;

	[SerializeField]
	[Header("Outer Circle")]
	protected Rigidbody outerRB;

	[SerializeField]
	protected HingeJoint outerJoint;

	[SerializeField]
	protected float outerRotationAmount = 0.25f;

	[SerializeField]
	protected ParticleSystem[] outerCircleMoveParticles;

	[SerializeField]
	protected AudioClip outerCircleCompleteSound;

	private Transform outerTransfrom;

	[SerializeField]
	[Header("Eye")]
	protected Transform eyePivot;

	[SerializeField]
	protected float innerEyeRotationAmount = 55f;

	[SerializeField]
	protected Transform innerButtomLid;

	[SerializeField]
	protected Transform innerTopLid;

	private float innerRotationMax;

	private float innerRotationDirection = 1f;

	private Vector3 innerPrevRight;

	private float innerRotaionAmountToObjective;

	private float innerObjectiveDegrees;

	[SerializeField]
	protected float outerEyeRotationAmount = 55f;

	[SerializeField]
	protected Transform outerButtomLid;

	[SerializeField]
	protected Transform outerTopLid;

	private float outerRotationMax;

	private float outerRotationDirection = 1f;

	private Vector3 outerPrevRight;

	private float outerRotaionAmountToObjective;

	private float outerObjectiveDegrees;

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

	[SerializeField]
	[Header("Other stuff")]
	protected AudioSource completeSFX;

	[SerializeField]
	protected ParticleSystem[] completeParticles;

	private float angle;

	private void Awake()
	{
		Object.FindObjectOfType<WinCondition>().objectiveObjectCount = objectiveObjCount * 2 + 1;
		innerTransfrom = innerRB.transform;
		outerTransfrom = outerRB.transform;
		innerRotationMax = innerRotationAmount;
		outerRotationMax = outerRotationAmount;
		outerRotaionAmountToObjective = Mathf.Abs(outerRotationAmount) / (float)objectiveObjCount;
		outerObjectiveDegrees = Mathf.Abs(outerRotationAmount);
		innerRotaionAmountToObjective = Mathf.Abs(innerRotationAmount) / (float)objectiveObjCount;
		innerObjectiveDegrees = Mathf.Abs(innerRotationAmount);
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
			outerPrevRight = outerTransfrom.right;
			innerPrevRight = innerTransfrom.right;
			startVolume = 0.35f;
			audioSource.volume = 0f;
			audioSource.Play();
			JointLimits limits = default(JointLimits);
			innerJoint.useLimits = true;
			limits.max = 0f;
			limits.min = 0f;
			innerJoint.limits = limits;
			outerJoint.useLimits = true;
			float num = outerRotationAmount * 1.1f * 360f;
			limits.max = ((!(num >= 0f)) ? 0f : num);
			limits.min = ((!(num < 0f)) ? 0f : num);
			outerJoint.limits = limits;
		}
	}

	private void Update()
	{
		SetAudioVolume(innerRB.angularVelocity.sqrMagnitude + outerRB.angularVelocity.sqrMagnitude);
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		float num = outerObjectiveDegrees - outerRotationAmount;
		if (Mathf.Abs(num) > outerRotaionAmountToObjective)
		{
			bool flag = num > 0f;
			outerObjectiveDegrees -= outerRotaionAmountToObjective * (float)(flag ? 1 : (-1));
			WinCondition.currentObjsCompleted += (flag ? 1 : (-1));
		}
		float num2 = innerObjectiveDegrees - Mathf.Abs(innerRotationAmount);
		if (Mathf.Abs(num2) > innerRotaionAmountToObjective)
		{
			bool flag2 = num2 > 0f;
			innerObjectiveDegrees -= innerRotaionAmountToObjective * (float)(flag2 ? 1 : (-1));
			WinCondition.currentObjsCompleted += (flag2 ? 1 : (-1));
		}
		if (innerRotationAmount >= 0f && !innerRB.isKinematic)
		{
			innerRB.isKinematic = true;
			innerRB.constraints = RigidbodyConstraints.FreezeAll;
			for (int i = 0; i < innerCircleMoveParticles.Length; i++)
			{
				innerCircleMoveParticles[i].Stop();
			}
			AudioSource.PlayClipAtPoint(innerCircleCompleteSound, base.transform.position);
		}
		if (outerRotationAmount <= 0f && !outerRB.isKinematic)
		{
			outerRB.isKinematic = true;
			outerRB.constraints = RigidbodyConstraints.FreezeAll;
			JointLimits limits = default(JointLimits);
			innerJoint.useLimits = true;
			float num3 = innerRotationAmount * 1.1f * 360f;
			limits.max = ((!(num3 >= 0f)) ? 0f : num3);
			limits.min = ((!(num3 < 0f)) ? 0f : num3);
			innerJoint.limits = limits;
			for (int j = 0; j < outerCircleMoveParticles.Length; j++)
			{
				outerCircleMoveParticles[j].Stop();
			}
			AudioSource.PlayClipAtPoint(outerCircleCompleteSound, base.transform.position);
		}
		if (innerRotationAmount >= 0f && innerRB.isKinematic && outerRotationAmount <= 0f && outerRB.isKinematic)
		{
			StartCoroutine(LastSpurt());
		}
		if (outerRotationAmount >= 0f)
		{
			outerRotationDirection = ((!(Vector3.Dot(outerTransfrom.forward, outerPrevRight) < 0f)) ? 1f : (-1f));
			angle = Vector3.Angle(outerTransfrom.right, outerPrevRight);
			float num4 = angle / 360f;
			outerRotationAmount -= num4 * outerRotationDirection;
			outerButtomLid.RotateAround(eyePivot.position, eyePivot.right, num4 / outerRotationMax * outerEyeRotationAmount * outerRotationDirection);
			outerTopLid.RotateAround(eyePivot.position, eyePivot.right, num4 / outerRotationMax * (0f - outerEyeRotationAmount) * outerRotationDirection);
			for (int k = 0; k < outerCircleMoveParticles.Length; k++)
			{
				if (angle > 0.01f)
				{
					outerCircleMoveParticles[k].Play();
				}
				else
				{
					outerCircleMoveParticles[k].Stop();
				}
			}
			if (angle > 0.01f && audioSource.isPlaying)
			{
				if (this.k >= pitchArray.Length)
				{
					this.k = 0;
				}
				pitchArray[this.k] = num4 * 2.55f + 0.85f;
				float num5 = 0f;
				for (int l = 0; l < pitchArray.Length; l++)
				{
					num5 += pitchArray[l];
				}
				this.k++;
			}
		}
		if (innerRotationAmount <= 0f)
		{
			innerRotationDirection = ((!(Vector3.Dot(innerTransfrom.forward, innerPrevRight) > 0f)) ? 1f : (-1f));
			angle = Vector3.Angle(innerTransfrom.right, innerPrevRight);
			float num6 = angle / 360f;
			innerRotationAmount += num6 * innerRotationDirection;
			innerTopLid.RotateAround(eyePivot.position, eyePivot.forward, num6 / innerRotationMax * innerEyeRotationAmount * innerRotationDirection);
			innerButtomLid.RotateAround(eyePivot.position, eyePivot.forward, num6 / innerRotationMax * (0f - innerEyeRotationAmount) * innerRotationDirection);
			for (int m = 0; m < innerCircleMoveParticles.Length; m++)
			{
				if (angle > 0.01f)
				{
					innerCircleMoveParticles[m].Play();
				}
				else
				{
					innerCircleMoveParticles[m].Stop();
				}
			}
			if (angle > 0.01f && audioSource.isPlaying)
			{
				if (this.k >= pitchArray.Length)
				{
					this.k = 0;
				}
				pitchArray[this.k] = num6 * 2.55f + 0.85f;
				float num7 = 0f;
				for (int n = 0; n < pitchArray.Length; n++)
				{
					num7 += pitchArray[n];
				}
				this.k++;
			}
		}
		outerPrevRight = outerTransfrom.right;
		innerPrevRight = innerTransfrom.right;
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
		WinCondition.currentObjsCompleted++;
		base.enabled = false;
	}
}
