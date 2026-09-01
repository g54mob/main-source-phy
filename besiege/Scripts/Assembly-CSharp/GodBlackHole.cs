using UnityEngine;

public class GodBlackHole : MonoBehaviour
{
	public Transform blackHolePrefab;

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
				blackHolePrefab.gameObject.SetActive(true);
				blackHolePrefab.position = hitInfo.point + hitInfo.normal * 4f;
			}
		}
	}

	private void Explodey(Vector3 pos)
	{
		colliders = Physics.OverlapSphere(pos, radius);
		Collider[] array = colliders;
		foreach (Collider collider in array)
		{
			if (collider == null)
			{
				continue;
			}
			if (collider.attachedRigidbody != null)
			{
				myAttachedRigidbody = collider.attachedRigidbody;
			}
			if (myAttachedRigidbody != null && myAttachedRigidbody != prevRigidbody && myAttachedRigidbody != GetComponent<Rigidbody>() && myAttachedRigidbody.gameObject.layer != 22 && myAttachedRigidbody.tag != "KeepConstraintsAlways")
			{
				myAttachedRigidbody.WakeUp();
				myAttachedRigidbody.constraints = RigidbodyConstraints.None;
				myAttachedRigidbody.AddForce((pos - myAttachedRigidbody.position).normalized * (0f - power));
				myAttachedRigidbody.AddRelativeTorque(Random.insideUnitSphere.normalized * torquePower);
				if ((bool)myAttachedRigidbody.gameObject.GetComponent<SimpleBirdAI>())
				{
					myAttachedRigidbody.gameObject.GetComponent<SimpleBirdAI>().Explode();
				}
				if ((bool)myAttachedRigidbody.gameObject.GetComponent<BreakOnForce>())
				{
					myAttachedRigidbody.gameObject.GetComponent<BreakOnForce>().BreakExplosion(power, pos, radius, upPower);
				}
				if ((bool)myAttachedRigidbody.gameObject.GetComponent<BreakOnForceNoSpawn>())
				{
					myAttachedRigidbody.gameObject.GetComponent<BreakOnForceNoSpawn>().BreakExplosion(power, pos, radius, upPower);
				}
				prevRigidbody = myAttachedRigidbody;
			}
			else if ((bool)collider.transform.parent && (bool)collider.transform.parent.GetComponent<Rigidbody>())
			{
				collider.transform.parent.GetComponent<Rigidbody>().WakeUp();
				collider.transform.parent.GetComponent<Rigidbody>().AddExplosionForce(power, pos, radius, upPower);
			}
		}
	}
}
