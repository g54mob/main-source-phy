using UnityEngine;

public class Effect_CrowColor : UnitEffectBase
{
	private bool inited;

	private UnitColorHandler colorHandler;

	public Gradient colorGradient;

	public AnimationCurve curve;

	public UnitColorInstance color;

	public float fadeSpeed = 3f;

	private float amount;

	public override void DoEffect()
	{
		Init();
		amount = 1f;
	}

	private void Update()
	{
		amount -= Time.deltaTime * fadeSpeed;
		if (amount >= 0f && (bool)colorHandler)
		{
			color.color = colorGradient.Evaluate(1f - amount);
			colorHandler.SetColor(color, curve.Evaluate(1f - amount));
		}
	}

	public override void Ping()
	{
		Init();
		amount = 1f;
	}

	private void Init()
	{
		if (!inited)
		{
			inited = true;
			colorHandler = base.transform.root.GetComponentInChildren<UnitColorHandler>();
		}
	}
}
