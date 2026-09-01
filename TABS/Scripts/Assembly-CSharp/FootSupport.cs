using Landfall.MonoBatch;
using UnityEngine;

public class FootSupport : BatchedMonobehaviour
{
	private AnimationHandler anim;

	private GroundChecker groundChecker;

	private DataHandler data;

	public bool isRight;

	private Collider col;

	public PhysicMaterial slippery;

	public PhysicMaterial foot;

	public float extraSupportTime;

	public float extraNoSupportTime;

	private float supportCounter;

	private float noSupportCounter;

	protected override void Start()
	{
		base.Start();
		anim = GetComponentInParent<AnimationHandler>();
		data = GetComponentInParent<DataHandler>();
		groundChecker = GetComponent<GroundChecker>();
		col = GetComponentInChildren<Collider>();
	}

	public override void BatchedUpdate()
	{
		if (!groundChecker)
		{
			return;
		}
		if (anim.currentState == 0 || anim.currentState == 3 || data.isRight != isRight)
		{
			supportCounter = 0f;
			noSupportCounter += Time.deltaTime;
			if (noSupportCounter > extraNoSupportTime)
			{
				groundChecker.isActive = true;
				col.sharedMaterial = foot;
			}
		}
		else
		{
			noSupportCounter = 0f;
			supportCounter += Time.deltaTime;
			if (supportCounter > extraSupportTime)
			{
				groundChecker.isActive = false;
				col.sharedMaterial = slippery;
			}
		}
	}
}
