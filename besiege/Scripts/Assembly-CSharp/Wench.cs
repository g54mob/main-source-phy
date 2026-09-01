using System.Collections;
using UnityEngine;

public class Wench : MonoBehaviour
{
	public float volumeLerpSpeed = 15f;

	public AudioSource raisedSFX;

	public AudioSource audioSource;

	public float audiofalloffDelay = 0.5f;

	public Rigidbody jointRB;

	public Collider wenchCenterCollider;

	public Collider[] ignoreCollider;

	public Animator[] wenchAnimator;

	public GameObject ghost;

	public int objectiveObjCount = 25;

	public ConfigurableJoint joint;

	public Transform counterPushObject;

	public float rotationAmount = 3.685f;

	public float wenchPositionAmount = 1.7263973f;

	public float altarPositionAmount = 7.1075068f;

	public float ghostDelay = 0.25f;

	public float winDelay = 2f;

	private float rotaionAmountToObjective;

	private float objectiveDegrees;

	private float counterPushObjectY;

	private Vector3 prevRight;

	private Vector3 upVector;

	private float falloffTime;

	private Rigidbody rb;

	private float wenchPositionRatio;

	private float altarPositionRatio;

	private float altarEndPositionY;

	private float[] pitchArray = new float[10];

	private int k;

	private float angle;

	private float targetVolume;

	private float startVolume;

	private void Awake()
	{
		Object.FindObjectOfType<WinCondition>().objectiveObjectCount = objectiveObjCount + 1;
		rotaionAmountToObjective = rotationAmount / (float)objectiveObjCount;
		objectiveDegrees = rotationAmount - rotaionAmountToObjective;
	}

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		if (StatMaster.levelSimulating)
		{
			k = 0;
			for (int i = 0; i < ignoreCollider.Length; i++)
			{
				Physics.IgnoreCollision(wenchCenterCollider, ignoreCollider[i]);
			}
			if (audioSource == null)
			{
				audioSource = GetComponent<AudioSource>();
			}
			prevRight = base.transform.right;
			wenchPositionRatio = wenchPositionAmount / rotationAmount;
			altarPositionRatio = altarPositionAmount / rotationAmount;
			altarEndPositionY = altarPositionAmount + counterPushObject.position.y;
			counterPushObjectY = counterPushObject.position.y;
			startVolume = 0.35f;
			audioSource.volume = 0f;
			audioSource.Play();
		}
	}

	private void Update()
	{
		SetAudioVolume(rb.angularVelocity.sqrMagnitude);
		if (!StatMaster.levelSimulating)
		{
			return;
		}
		if (objectiveDegrees > rotationAmount)
		{
			objectiveDegrees -= rotaionAmountToObjective;
			WinCondition.currentObjsCompleted++;
		}
		if (rotationAmount <= 0f)
		{
			if (!rb.isKinematic)
			{
				rb.isKinematic = true;
				rb.constraints = RigidbodyConstraints.FreezeAll;
				StartCoroutine(LastSpurt(0.2f));
			}
			return;
		}
		if (!Mathf.Approximately(counterPushObject.position.y, counterPushObjectY))
		{
			counterPushObject.position = new Vector3(counterPushObject.position.x, Mathf.Lerp(counterPushObject.position.y, counterPushObjectY, Time.deltaTime * 10f), counterPushObject.position.z);
		}
		angle = Vector3.Angle(base.transform.right, prevRight);
		if (!(angle > 0.01f) || Vector3.Dot(base.transform.forward, prevRight) < -0.01f)
		{
			return;
		}
		jointRB.transform.rotation = rb.rotation;
		float num = angle / 360f;
		rotationAmount -= num;
		rb.MovePosition(new Vector3(rb.position.x, rb.position.y - wenchPositionRatio * num, rb.position.z));
		joint.anchor = new Vector3(joint.anchor.x, joint.anchor.y - wenchPositionRatio * num, joint.anchor.z);
		counterPushObjectY += altarPositionRatio * num;
		if (audioSource.isPlaying)
		{
			if (k >= pitchArray.Length)
			{
				k = 0;
			}
			pitchArray[k] = num * 2.55f + 0.85f;
			float num2 = 0f;
			for (int i = 0; i < pitchArray.Length; i++)
			{
				num2 += pitchArray[i];
			}
			k++;
		}
		prevRight = base.transform.right;
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

	private IEnumerator LastSpurt(float time)
	{
		raisedSFX.Play();
		while (!Mathf.Approximately(counterPushObject.position.y, altarEndPositionY))
		{
			if (!audioSource.isPlaying)
			{
				audioSource.Play();
			}
			counterPushObject.position = new Vector3(counterPushObject.position.x, Mathf.Lerp(counterPushObject.position.y, altarEndPositionY, Time.deltaTime * 10f), counterPushObject.position.z);
			yield return null;
		}
		audioSource.Stop();
		yield return StartCoroutine(Animate());
		yield return new WaitForSeconds(winDelay);
		WinCondition.currentObjsCompleted++;
		base.enabled = false;
	}

	private IEnumerator Animate()
	{
		for (int i = 0; i < wenchAnimator.Length; i++)
		{
			wenchAnimator[i].enabled = true;
		}
		yield return new WaitForSeconds(ghostDelay);
		ghost.SetActive(true);
	}
}
