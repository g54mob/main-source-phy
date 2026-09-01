using UnityEngine;

public class GodMeteorController : MonoBehaviour
{
	public Transform meteorPrefab;

	public Transform camObj;

	public float spawnBehindAmount = 6f;

	public float spawnHeight = 6f;

	public float meteorForce = 1000f;

	public RaycastHit hit;

	private Ray ray;

	private void Start()
	{
		camObj = Camera.main.transform;
	}

	private void Update()
	{
		if (Machine.Active().isSimulating && InputManager.LeftMouseButton())
		{
			ray = Camera.main.ScreenPointToRay(InputManager.CursorPosition());
			if (Physics.Raycast(ray, out hit, 1000f))
			{
				ShootMeteor(hit.point);
			}
		}
	}

	private void ShootMeteor(Vector3 targetPos)
	{
		Transform transform = (Object.Instantiate(meteorPrefab, camObj.position - camObj.forward * spawnBehindAmount + new Vector3(0f, spawnHeight, 0f), Quaternion.identity) as GameObject).transform;
		transform.GetComponent<Rigidbody>().AddForce((targetPos - transform.position).normalized * meteorForce);
	}
}
