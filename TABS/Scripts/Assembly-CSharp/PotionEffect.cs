using Landfall.TABS;
using UnityEngine;

public class PotionEffect : UnitEffectBase
{
	private const float UpdateEffectColorTimeInterval = 0.5f;

	public Gradient colorGradient;

	public AnimationCurve curve;

	public UnitColorInstance color;

	public float footForce = 30f;

	public float torsoForso = 10f;

	private UnitColorHandler colorHandler;

	private Unit unit;

	private float offset;

	private float speed = 1f;

	private float amount;

	private GooglyEye[] eyes;

	private StandingHandler stand;

	private MovementHandler move;

	private DataHandler data;

	private ParticleSystem part;

	private float removedMove = 0.9f;

	private float removedStand = 0.025f;

	private float counter;

	private bool done;

	private float timer;

	private Rigidbody leftFootRB;

	private Rigidbody rightFootRB;

	private Rigidbody torsoRB;

	private Balance balance;

	private bool hasUnit;

	private void Init()
	{
		part = GetComponentInChildren<ParticleSystem>();
		Random.InitState(base.transform.root.GetInstanceID());
		unit = base.transform.parent.GetComponentInChildren<Unit>();
		offset = Random.Range(0, 10000);
		speed = Random.Range(0.5f, 2f);
		move = base.transform.parent.GetComponentInChildren<MovementHandler>();
		if ((bool)move)
		{
			move.multiplier *= 0.9f;
		}
		data = base.transform.parent.GetComponentInChildren<DataHandler>();
		stand = base.transform.parent.GetComponentInChildren<StandingHandler>();
		if ((bool)stand)
		{
			stand.selfOffset += 0.025f;
		}
		eyes = base.transform.parent.GetComponentsInChildren<GooglyEye>();
		colorHandler = base.transform.root.GetComponentInChildren<UnitColorHandler>();
		Head componentInChildren = base.transform.parent.GetComponentInChildren<Head>();
		if (componentInChildren != null)
		{
			base.transform.SetPositionAndRotation(componentInChildren.transform.position, componentInChildren.transform.rotation);
			base.transform.parent = componentInChildren.transform;
		}
		if (unit != null)
		{
			hasUnit = true;
			GetBalanceComponents();
		}
	}

	private void Update()
	{
		if (!hasUnit)
		{
			return;
		}
		timer += Time.deltaTime;
		if (counter > 10f)
		{
			unit.unitConfusion = Vector3.Lerp(unit.unitConfusion, Vector3.zero, Time.deltaTime * 2f);
			if (timer > 0.5f && (bool)colorHandler)
			{
				colorHandler.SetColor(color, curve.Evaluate(1f - unit.unitConfusion.magnitude));
				timer = 0f;
			}
			if (!done)
			{
				done = true;
				if (move != null)
				{
					move.multiplier *= 1f / removedMove;
				}
				stand = base.transform.parent.GetComponentInChildren<StandingHandler>();
				if (stand != null)
				{
					stand.selfOffset -= removedStand;
				}
			}
			if (counter > 13f && unit.unitConfusion.magnitude < 0.1f)
			{
				Object.Destroy(base.gameObject);
			}
			return;
		}
		if (amount >= 0f && colorHandler != null)
		{
			color.color = colorGradient.Evaluate(1f - amount);
			colorHandler.SetColor(color, curve.Evaluate(1f - amount));
		}
		float x = (Mathf.PerlinNoise(Time.time * speed + offset, Time.time) - 0.45f) * 2f;
		float z = (Mathf.PerlinNoise(Time.time * speed + offset * 50f, Time.time) - 0.45f) * 2f;
		unit.unitConfusion += amount * Time.deltaTime * new Vector3(x, 0f, z);
		if (Random.value < 0.3f * Time.deltaTime * amount && balance != null && balance.Fall())
		{
			Vector3 insideUnitSphere = Random.insideUnitSphere;
			leftFootRB.AddForce(insideUnitSphere * footForce, ForceMode.VelocityChange);
			rightFootRB.AddForce(insideUnitSphere * footForce, ForceMode.VelocityChange);
			torsoRB.AddForce(insideUnitSphere * torsoForso, ForceMode.VelocityChange);
		}
		for (int i = 0; i < eyes.Length; i++)
		{
			eyes[i].velocity += 80f * Time.deltaTime * Vector3.Cross(eyes[i].pupil.position - eyes[i].open.transform.position, eyes[i].transform.forward);
		}
		if (unit.dead)
		{
			Object.Destroy(base.gameObject);
		}
		counter += Time.deltaTime;
	}

	public override void DoEffect()
	{
		Init();
		amount += 1.5f / Mathf.Clamp(data.maxHealth * 0.02f, 0.1f, float.PositiveInfinity);
		timer = 1.5f;
	}

	public override void Ping()
	{
		counter = 0f;
		amount += 1.5f / Mathf.Clamp(data.maxHealth * 0.02f, 0.1f, float.PositiveInfinity);
		part.Play();
		timer = 1.5f;
	}

	private void GetBalanceComponents()
	{
		if (unit.data != null)
		{
			balance = unit.data.GetComponent<Balance>();
			if (balance != null)
			{
				leftFootRB = unit.data.footLeft.GetComponent<Rigidbody>();
				rightFootRB = unit.data.footRight.GetComponent<Rigidbody>();
				torsoRB = unit.data.torso.GetComponent<Rigidbody>();
			}
		}
	}
}
