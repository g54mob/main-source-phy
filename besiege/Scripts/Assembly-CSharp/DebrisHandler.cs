using System.Collections;
using UnityEngine;

public class DebrisHandler : MonoBehaviour
{
	public enum ForceDirection
	{
		None = 0,
		Local = 1,
		Global = 2,
		Explosion = 3,
		Random = 4
	}

	public enum TorqueType
	{
		None = 0,
		Local = 1,
		Global = 2,
		Random = 3
	}

	public enum DestructionType
	{
		None = 0,
		Timer = 1
	}

	public enum MaterialState
	{
		None = 0,
		Burn = 1,
		Damage = 2,
		Freeze = 3
	}

	public enum LerpType
	{
		Liniare = 0,
		Curve = 1
	}

	[SerializeField]
	private BasicInfo[] debris;

	[SerializeField]
	private bool inheritForce;

	public Rigidbody inheritRB;

	[SerializeField]
	private ForceDirection forceDirection;

	[SerializeField]
	private ForceMode forceMode = ForceMode.Acceleration;

	[SerializeField]
	private float forceAmount;

	[SerializeField]
	private Vector3 forceVector = Vector3.zero;

	private Vector3 inheritedVelocity = Vector3.zero;

	[SerializeField]
	private float explosionRadius;

	[SerializeField]
	private float upwardsModifier;

	[SerializeField]
	private TorqueType torqueType;

	[SerializeField]
	private float torqueAmount;

	[SerializeField]
	private Vector3 torqueVector = Vector3.zero;

	[SerializeField]
	private MaterialState materialState;

	[SerializeField]
	private float lerpTime;

	[SerializeField]
	private LerpType lerpType;

	[SerializeField]
	private AnimationCurve lerpCurve;

	[SerializeField]
	private Color burntColor;

	[SerializeField]
	private Color iceColoration;

	private bool changingMaterial = true;

	private float returnPrecent;

	private float lerpTimer;

	private Color[] startRimColor;

	private Color[] startColours;

	[SerializeField]
	private bool destroyAfterMaterialChange;

	[SerializeField]
	private bool destroyOwnGameObject;

	[SerializeField]
	private bool scaleDownOverTime;

	[SerializeField]
	private DestructionType destructionType;

	[SerializeField]
	private float timeToDestruction;

	[SerializeField]
	private ParticleSystem[] turnOff;

	[SerializeField]
	private float timeToOff;

	[SerializeField]
	private AnimationCurve sizeCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);

	private Vector3[] startingScales;

	private float timer;

	private bool isNetworkSynced;

	protected void Start()
	{
		if ((float)debris.Length == 0f)
		{
			return;
		}
		if (!debris[0].isSimulating)
		{
			DestroyDebris();
			return;
		}
		if (scaleDownOverTime)
		{
			startingScales = new Vector3[debris.Length];
			for (int i = 0; i < debris.Length; i++)
			{
				startingScales[i] = debris[i].MeshRenderer.transform.localScale;
			}
		}
		if (materialState != MaterialState.None && lerpTime <= 0f)
		{
			lerpTime = 0.001f;
		}
		switch (materialState)
		{
		case MaterialState.None:
			changingMaterial = false;
			break;
		case MaterialState.Burn:
			StartCoroutine(SetBurnedLevel());
			break;
		case MaterialState.Damage:
			StartCoroutine(SetDamageLevel());
			break;
		case MaterialState.Freeze:
			StartCoroutine(SetFreezeLevel());
			break;
		}
		StartCoroutine(CleanUp());
		if (StatMaster.isClient && !StatMaster.isLocalSim && (isNetworkSynced || debris[0].Rigidbody == null))
		{
			return;
		}
		if (inheritForce)
		{
			if (inheritRB == null)
			{
				Debug.LogError("inheritRB is not assigned");
				return;
			}
			inheritedVelocity = inheritRB.velocity;
		}
		for (int j = 0; j < debris.Length; j++)
		{
			BasicInfo basicInfo = debris[j];
			if (basicInfo.noRigidbody)
			{
				continue;
			}
			switch (forceDirection)
			{
			case ForceDirection.None:
				if (inheritForce)
				{
					basicInfo.Rigidbody.AddForce(inheritedVelocity, forceMode);
				}
				break;
			case ForceDirection.Local:
				basicInfo.Rigidbody.AddRelativeForce(forceVector * forceAmount + inheritedVelocity, forceMode);
				break;
			case ForceDirection.Global:
				basicInfo.Rigidbody.AddForce(forceVector * forceAmount + inheritedVelocity, forceMode);
				break;
			case ForceDirection.Explosion:
				basicInfo.Rigidbody.AddExplosionForce(forceAmount, base.transform.position, explosionRadius, upwardsModifier, forceMode);
				break;
			case ForceDirection.Random:
				basicInfo.Rigidbody.AddForce(Random.insideUnitSphere.normalized * forceAmount + inheritedVelocity, forceMode);
				break;
			}
			switch (torqueType)
			{
			case TorqueType.Local:
				basicInfo.Rigidbody.AddRelativeTorque(torqueVector * torqueAmount, forceMode);
				break;
			case TorqueType.Global:
				basicInfo.Rigidbody.AddTorque(torqueVector * torqueAmount, forceMode);
				break;
			case TorqueType.Random:
				basicInfo.Rigidbody.AddForce(Random.insideUnitSphere.normalized * torqueAmount, forceMode);
				break;
			}
		}
	}

	private void SetupMaterialVariables()
	{
		startRimColor = new Color[debris.Length];
		startColours = new Color[debris.Length];
		for (int i = 0; i < debris.Length; i++)
		{
			switch (materialState)
			{
			case MaterialState.Burn:
				if (debris[i].MeshRenderer.material.HasProperty("_RimColor"))
				{
					startRimColor[i] = debris[i].MeshRenderer.material.GetColor("_RimColor");
				}
				if (debris[i].MeshRenderer.material.HasProperty("_Color"))
				{
					startColours[i] = debris[i].MeshRenderer.material.GetColor("_Color");
				}
				break;
			case MaterialState.Freeze:
				if (debris[i].MeshRenderer.material.HasProperty("_Color"))
				{
					startColours[i] = debris[i].MeshRenderer.material.GetColor("_Color");
				}
				break;
			}
		}
	}

	private float LerpMaterialChange()
	{
		lerpTimer += Time.deltaTime;
		switch (lerpType)
		{
		case LerpType.Liniare:
			returnPrecent = lerpTimer / lerpTime;
			break;
		case LerpType.Curve:
			returnPrecent = lerpCurve.Evaluate(lerpTimer / lerpTime);
			break;
		}
		return returnPrecent;
	}

	private IEnumerator SetBurnedLevel()
	{
		MaterialPropertyBlock props = new MaterialPropertyBlock();
		float pct = 0f;
		SetupMaterialVariables();
		while (pct < 1f)
		{
			pct = Mathf.Clamp01(LerpMaterialChange());
			props.SetColor("_EmissCol", pct * Color.white);
			for (int i = 0; i < debris.Length; i++)
			{
				if (!object.ReferenceEquals(debris[i].MeshRenderer, null))
				{
					props.SetColor("_RimColor", (1f - pct) * startRimColor[i]);
					props.SetColor("_Color", Color.Lerp(startColours[i], burntColor, pct));
					debris[i].MeshRenderer.SetPropertyBlock(props);
				}
			}
			yield return null;
		}
		changingMaterial = false;
	}

	private IEnumerator SetDamageLevel()
	{
		MaterialPropertyBlock props = new MaterialPropertyBlock();
		float pct = 0f;
		while (pct < 1f)
		{
			pct = Mathf.Clamp01(LerpMaterialChange());
			props.SetFloat("_DamageAmount", pct);
			for (int i = 0; i < debris.Length; i++)
			{
				if (!object.ReferenceEquals(debris[i].MeshRenderer, null))
				{
					debris[i].MeshRenderer.SetPropertyBlock(props);
				}
			}
			yield return null;
		}
		changingMaterial = false;
	}

	private IEnumerator SetFreezeLevel()
	{
		MaterialPropertyBlock props = new MaterialPropertyBlock();
		float pct = 0f;
		SetupMaterialVariables();
		while (pct < 1f)
		{
			pct = Mathf.Clamp01(LerpMaterialChange());
			props.SetColor("_EmissCol", pct * Color.white);
			props.SetFloat("_FreezeAmount", pct);
			for (int i = 0; i < debris.Length; i++)
			{
				if (!object.ReferenceEquals(debris[i].MeshRenderer, null))
				{
					props.SetColor("_Color", Color.Lerp(startColours[i], iceColoration, pct));
					debris[i].MeshRenderer.SetPropertyBlock(props);
				}
			}
			yield return null;
		}
		changingMaterial = false;
	}

	private IEnumerator CleanUp()
	{
		if (destroyAfterMaterialChange)
		{
			while (changingMaterial)
			{
				yield return null;
			}
		}
		DestructionType destructionType = this.destructionType;
		if (destructionType == DestructionType.None || destructionType != DestructionType.Timer)
		{
			yield break;
		}
		if (scaleDownOverTime)
		{
			while (timer < timeToDestruction)
			{
				timer += Time.deltaTime;
				if (timer > timeToDestruction)
				{
					timer = timeToDestruction;
				}
				float pct = timer / timeToDestruction;
				for (int i = 0; i < debris.Length; i++)
				{
					debris[i].MeshRenderer.transform.localScale = startingScales[i] * sizeCurve.Evaluate(pct);
				}
				if (timer > timeToOff)
				{
					for (int j = 0; j < turnOff.Length; j++)
					{
						turnOff[j].Stop();
					}
					timeToOff = float.MaxValue;
				}
				yield return null;
			}
		}
		else
		{
			yield return new WaitForSeconds(timeToDestruction);
		}
		DestroyDebris();
	}

	private void DestroyDebris()
	{
		for (int i = 0; i < debris.Length; i++)
		{
			Object.Destroy(debris[i].gameObject);
		}
		if (destroyOwnGameObject)
		{
			Object.Destroy(base.gameObject);
		}
		else
		{
			Object.Destroy(this);
		}
	}
}
