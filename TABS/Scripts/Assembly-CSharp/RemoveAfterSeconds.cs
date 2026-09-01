using System;
using System.Collections;
using Landfall.TABS.GameState;
using UnityEngine;

public class RemoveAfterSeconds : GameStateListener, GameObjectPooling.IPoolable
{
	public float seconds = 10f;

	public float extraRandom;

	public bool shrink;

	public bool lerpIn;

	public bool reInitializeColorHandlerReferences;

	private bool isPooled;

	[HideInInspector]
	public GameObject objectToSpawn;

	[HideInInspector]
	public bool spawnObjectOnMainRig;

	private bool hasStartedTransition;

	private float defaultSize;

	private float counter;

	public bool IsManagedByPool { get; set; }

	public Action ReleaseSelf { get; set; }

	private void Start()
	{
		counter = seconds + UnityEngine.Random.Range(0f, extraRandom);
		defaultSize = base.transform.localScale.x;
		if (lerpIn)
		{
			base.transform.localScale = Vector3.zero;
		}
		isPooled = GetComponentInParent<PooledProjectile>();
	}

	private void Update()
	{
		if (hasStartedTransition)
		{
			return;
		}
		if (lerpIn)
		{
			base.transform.localScale = Vector3.Lerp(base.transform.localScale, Vector3.one * defaultSize, Time.deltaTime * 5f);
		}
		counter -= Time.deltaTime;
		if (counter < 0f)
		{
			if (shrink)
			{
				hasStartedTransition = true;
				StartCoroutine(Shrink());
			}
			else
			{
				TriggerRemoval();
			}
		}
	}

	private IEnumerator Shrink()
	{
		while (true)
		{
			Vector3 localScale = base.transform.localScale;
			Vector3 localScale2 = localScale - 3f * defaultSize * Time.deltaTime * Vector3.one;
			float num = Mathf.Sin(localScale.x * localScale.y * localScale.z);
			float num2 = Mathf.Sin(localScale2.x * localScale2.y * localScale2.z);
			if (num != num2)
			{
				if (num2 <= 0f)
				{
					break;
				}
				base.transform.localScale = localScale2;
			}
			yield return null;
		}
		TriggerRemoval();
	}

	private void TriggerRemoval()
	{
		if ((bool)objectToSpawn)
		{
			if (spawnObjectOnMainRig)
			{
				UnityEngine.Object.Instantiate(objectToSpawn, base.transform.GetComponentInChildren<DataHandler>().mainRig.position, Quaternion.identity);
			}
			else
			{
				UnityEngine.Object.Instantiate(objectToSpawn, base.transform.position, Quaternion.identity);
			}
		}
		if (isPooled)
		{
			ReleaseSelf?.Invoke();
			return;
		}
		if (reInitializeColorHandlerReferences)
		{
			base.transform.root.GetComponentInChildren<UnitColorHandler>().ForceReInitialize();
		}
		UnityEngine.Object.Destroy(base.gameObject);
	}

	public override void OnEnterPlacementState()
	{
		TriggerRemoval();
	}

	public override void OnEnterBattleState()
	{
	}

	public void Initialize()
	{
	}

	public void Reset()
	{
		counter = seconds + UnityEngine.Random.Range(0f, extraRandom);
		hasStartedTransition = false;
	}

	public void Release()
	{
	}
}
