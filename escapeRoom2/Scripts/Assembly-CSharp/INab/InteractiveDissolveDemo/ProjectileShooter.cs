using UnityEngine;

namespace INab.InteractiveDissolveDemo
{
	public class ProjectileShooter : MonoBehaviour
	{
		public GameObject projectilePrefab;

		public Transform shootPoint;

		public float shootForce = 1000f;

		public LayerMask layerMask;

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Mouse0))
			{
				ShootProjectile();
			}
		}

		private void ShootProjectile()
		{
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo, 300f, layerMask.value))
			{
				Rigidbody component = Object.Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation).GetComponent<Rigidbody>();
				if (component != null)
				{
					Vector3 normalized = (hitInfo.point - shootPoint.position).normalized;
					component.AddForce(normalized * shootForce);
				}
			}
		}
	}
}
