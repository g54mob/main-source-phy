using UnityEngine;

public class GodExplodeOnClick : MonoBehaviour
{
	public Transform explosionPrefab;

	public float radius = 7f;

	public float power = 1800f;

	public float torquePower = 100000f;

	public float upPower = 3f;

	public AddPiece addPieceCode;

	private Rigidbody myAttachedRigidbody;

	private Rigidbody prevRigidbody;

	private Collider[] colliders;

	private void Start()
	{
		addPieceCode = SingleInstanceFindOnly<AddPiece>.Instance;
	}

	private void Update()
	{
		if (Machine.Active().isSimulating && InputManager.LeftMouseButton() && !addPieceCode.hudOccluding)
		{
			Ray ray = Camera.main.ScreenPointToRay(InputManager.CursorPosition());
			RaycastHit hitInfo;
			if (Physics.Raycast(ray, out hitInfo, 1000f))
			{
				Explodey(hitInfo.point);
			}
		}
	}

	private void Explodey(Vector3 pos)
	{
		Object.Instantiate(explosionPrefab.gameObject, pos, Quaternion.identity);
		colliders = Physics.OverlapSphere(pos, radius);
		Collider[] array = colliders;
		foreach (Collider collider in array)
		{
			if (collider == null)
			{
				continue;
			}
			if ((bool)collider.attachedRigidbody)
			{
				myAttachedRigidbody = collider.attachedRigidbody;
			}
			if (myAttachedRigidbody != null && myAttachedRigidbody != prevRigidbody && myAttachedRigidbody != GetComponent<Rigidbody>() && myAttachedRigidbody.gameObject.layer != 22 && myAttachedRigidbody.tag != "KeepConstraintsAlways")
			{
				myAttachedRigidbody.WakeUp();
				myAttachedRigidbody.constraints = RigidbodyConstraints.None;
				myAttachedRigidbody.AddExplosionForce(power, pos, radius, upPower);
				myAttachedRigidbody.AddRelativeTorque(Random.insideUnitSphere.normalized * torquePower);
				FireTag component = myAttachedRigidbody.GetComponent<FireTag>();
				if ((bool)component)
				{
					component.Ignite(1f);
				}
				ExplodeMultiplier component2 = myAttachedRigidbody.GetComponent<ExplodeMultiplier>();
				if ((bool)component2)
				{
					component2.Explodey(power, pos, radius, upPower);
				}
				SimpleBirdAI component3 = myAttachedRigidbody.GetComponent<SimpleBirdAI>();
				if ((bool)component3)
				{
					component3.Explode();
					break;
				}
				EnemyAISimple component4 = myAttachedRigidbody.GetComponent<EnemyAISimple>();
				if ((bool)component4)
				{
					component4.Die();
					break;
				}
				CastleWallBreak component5 = myAttachedRigidbody.GetComponent<CastleWallBreak>();
				if ((bool)component5)
				{
					component5.BreakExplosion(power, pos, radius, upPower);
				}
				BreakOnForce component6 = myAttachedRigidbody.GetComponent<BreakOnForce>();
				if ((bool)component6)
				{
					component6.BreakExplosion(power, pos, radius, upPower);
				}
				InjuryController component7 = myAttachedRigidbody.GetComponent<InjuryController>();
				if ((bool)component7)
				{
					component7.activeType = InjuryType.Fire;
					component7.Kill();
				}
				prevRigidbody = myAttachedRigidbody;
			}
			else if ((bool)collider.transform.parent)
			{
				Rigidbody component8 = collider.transform.parent.GetComponent<Rigidbody>();
				if ((bool)component8)
				{
					component8.WakeUp();
					component8.AddExplosionForce(power, pos, radius, upPower);
					component8.AddRelativeTorque(Random.insideUnitSphere.normalized * torquePower);
				}
			}
		}
	}
}
