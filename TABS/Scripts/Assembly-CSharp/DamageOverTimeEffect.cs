using Landfall.TABS;
using UnityEngine;
using UnityEngine.Events;

public class DamageOverTimeEffect : UnitEffectBase
{
	public UnityEvent spawnEvent;

	public UnityEvent pingEvent;

	public UnityEvent dmgEvent;

	public float damagePerStack = 50f;

	public float forcePerStack;

	public float effectTime = 2f;

	public UnitColorInstance color;

	public AnimationCurve colorCurve;

	public AnimationCurve damageCurve;

	public float effectDuration = 1f;

	private bool effectDone;

	private bool removedColor;

	private float currentColorValue;

	private float currentTime = 1f;

	private HealthHandler health;

	private DataHandler data;

	private UnitColorHandler colorHandler;

	private TeamHolder spawner;

	private Unit unit;

	private float damageToDo;

	private float forceToDo;

	private float damageDealtSinceLastStack;

	private float forceDealtSinceLastStack;

	private void Awake()
	{
		unit = base.transform.root.GetComponent<Unit>();
		if ((bool)unit)
		{
			health = unit.GetComponentInChildren<HealthHandler>();
			data = unit.GetComponentInChildren<DataHandler>();
			colorHandler = base.transform.root.GetComponentInChildren<UnitColorHandler>();
		}
	}

	public override void DoEffect()
	{
		spawnEvent.Invoke();
		Ping();
	}

	public override void Ping()
	{
		AddDamage();
		pingEvent.Invoke();
	}

	private void AddDamage()
	{
		damageToDo -= damageDealtSinceLastStack;
		damageToDo += damagePerStack;
		damageDealtSinceLastStack = 0f;
		currentTime = 0f;
		effectDone = false;
		removedColor = false;
		forceToDo -= forceDealtSinceLastStack;
		forceToDo += forcePerStack;
		forceDealtSinceLastStack = 0f;
	}

	private void Update()
	{
		if (!data)
		{
			return;
		}
		float num = Time.deltaTime / effectTime;
		float num2 = Mathf.Clamp(100f / data.maxHealth, 0.35f, 1.2f);
		currentTime += num;
		if (currentTime < effectDuration)
		{
			float num3 = damageToDo * num;
			damageDealtSinceLastStack += num3;
			health.TakeDamage(num3 * damageCurve.Evaluate(currentTime) * damageMultiplier, Vector3.zero, null);
			dmgEvent.Invoke();
			spawner = GetComponentInParent<TeamHolder>();
			if ((bool)spawner && (bool)spawner.spawner && (bool)unit)
			{
				unit.DealDamage(num3 * damageCurve.Evaluate(currentTime) * damageMultiplier);
			}
			if (forcePerStack != 0f)
			{
				float num4 = forceToDo * num;
				forceDealtSinceLastStack += num4;
				health.GetComponent<DataHandler>().mainRig.AddForce(damageCurve.Evaluate(currentTime) * num4 * base.transform.forward, ForceMode.Force);
			}
		}
		if (colorCurve.length > 1)
		{
			currentColorValue = Mathf.Lerp(currentColorValue, colorCurve.Evaluate(currentTime), Time.deltaTime * 5f);
			if ((bool)colorHandler && !effectDone)
			{
				colorHandler.SetColor(color, colorCurve.Evaluate(currentTime) * num2);
			}
		}
		if (currentTime >= effectDuration)
		{
			effectDone = true;
		}
		if (effectDone && !removedColor)
		{
			ResetUnitColours();
		}
	}

	private void OnDestroy()
	{
		ResetUnitColours();
	}

	private void ResetUnitColours()
	{
		for (int i = 0; i < colorHandler.colors.Count; i++)
		{
			if (color.colorName == colorHandler.colors[i].colorName)
			{
				colorHandler.colors.RemoveAt(i);
			}
		}
		colorHandler.UpdateColors();
		removedColor = true;
	}
}
