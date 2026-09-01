using UnityEngine;

public class SnapOnSharpChopBalloon : SimBehaviour
{
	public BalloonController balloonCode;

	protected override void Start()
	{
		base.Start();
		if (base.isSimulating)
		{
			base.gameObject.layer = 25;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (base.SimPhysics && base.isSimulating && !other.isTrigger && (bool)other.attachedRigidbody)
		{
			BlockBehaviour component = other.attachedRigidbody.GetComponent<BlockBehaviour>();
			if (component != null && component.Prefab.hasDamageType && component.Prefab.myDamageType == DamageType.Sharp)
			{
				balloonCode.Snap();
			}
		}
	}
}
