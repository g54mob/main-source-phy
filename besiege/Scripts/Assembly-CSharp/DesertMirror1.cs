using System;
using UnityEngine;

public class DesertMirror1 : MonoBehaviour
{
	public static bool BlocksBeenHit;

	public int maxReflectionCount = 5;

	public float maxStepDistance = 200f;

	public int numberofhits;

	public int numberofbounces;

	public Vector3 SecondTolastPos;

	public Vector3[] HitPositions;

	public bool setsThingsOnFire;

	public Transform[] targets;

	[Header("Victory Conditions")]
	private bool levelComplete;

	public bool hasVictoryTarget;

	public GameObject victoryTarget;

	public int endstate;

	public LaserTargetCheck targetCheck;

	private Vector3[] lastNormal = new Vector3[5];

	protected void Start()
	{
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Combine(ReferenceMaster.onLevelSimulation, new Action<bool>(ResetDetection));
		if (hasVictoryTarget)
		{
			victoryTarget.SetActive(true);
		}
	}

	protected void OnDestroy()
	{
		ReferenceMaster.onLevelSimulation = (Action<bool>)Delegate.Remove(ReferenceMaster.onLevelSimulation, new Action<bool>(ResetDetection));
		ResetDetection(false);
	}

	protected void ResetDetection(bool simulating)
	{
		if (!simulating)
		{
			BlocksBeenHit = false;
		}
	}

	protected void OnEnable()
	{
		LateUpdate();
	}

	protected void LateUpdate()
	{
		HitPositions[0] = base.transform.position;
		for (int i = 0; i <= maxReflectionCount; i++)
		{
			targets[i].position = HitPositions[i];
		}
		if (numberofbounces > 0)
		{
			targets[numberofbounces].position = SecondTolastPos;
		}
		DrawPredictedReflectionPattern(base.transform.position + base.transform.forward * 0.75f, base.transform.forward, maxReflectionCount);
		for (int j = numberofbounces; j <= maxReflectionCount; j++)
		{
			HitPositions[j] = HitPositions[numberofbounces];
		}
	}

	private void DrawPredictedReflectionPattern(Vector3 position, Vector3 direction, int reflectionsRemaining)
	{
		if (reflectionsRemaining == 0)
		{
			numberofbounces = numberofhits;
			numberofhits = 0;
			return;
		}
		Ray ray = new Ray(position, direction);
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo, maxStepDistance))
		{
			if (hitInfo.collider.gameObject.name == "Mirror")
			{
				direction = Vector3.Reflect(direction, (hitInfo.normal + lastNormal[reflectionsRemaining]).normalized);
				lastNormal[reflectionsRemaining] = hitInfo.normal;
				position = hitInfo.point;
				SecondTolastPos = position;
				numberofhits++;
			}
			else
			{
				lastNormal[reflectionsRemaining] = Vector3.zero;
				reflectionsRemaining = 1;
				position = hitInfo.point;
			}
			Rigidbody attachedRigidbody = hitInfo.collider.attachedRigidbody;
			if (attachedRigidbody != null)
			{
				FireTag component = attachedRigidbody.GetComponent<FireTag>();
				if (component != null && setsThingsOnFire)
				{
					component.Ignite(1f);
					if (component.basicInfo.infoType == BasicInfo.BasicInfoType.Block)
					{
						BlocksBeenHit = true;
					}
				}
			}
			if (targetCheck != null)
			{
				targetCheck.CheckParameters(hitInfo.collider.name);
			}
			if (victoryTarget != null && hasVictoryTarget && !levelComplete)
			{
				endstate = Animator.StringToHash("Base.DeadState");
				Animator component2 = victoryTarget.GetComponent<Animator>();
				if (hitInfo.collider.gameObject.name == victoryTarget.name && numberofbounces == maxReflectionCount - 1)
				{
					component2.SetFloat("HeatingSpeed", 1f);
					component2.SetBool("isheating", true);
					if (component2.GetCurrentAnimatorStateInfo(0).fullPathHash == endstate)
					{
						AudioSource component3 = victoryTarget.GetComponent<AudioSource>();
						if ((bool)component3)
						{
							component3.Play();
						}
						WinCondition.currentObjsCompleted += 100;
						component2.SetFloat("HeatingSpeed", 0f);
						component2.enabled = false;
						levelComplete = true;
					}
				}
				else if (!levelComplete)
				{
					component2.SetFloat("HeatingSpeed", -1f);
					component2.SetBool("isheating", false);
					component2.enabled = true;
				}
			}
		}
		else
		{
			position += direction * maxStepDistance;
		}
		HitPositions[numberofhits] = position;
		DrawPredictedReflectionPattern(position, direction, reflectionsRemaining - 1);
	}
}
