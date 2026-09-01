using UnityEngine;

namespace Landfall.TABC
{
	public class HealthHandlerClient : MonoBehaviour
	{
		public int health = 100;

		private void Start()
		{
			TakeDamage(0);
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				TakeDamage(Random.Range(1, 12));
			}
		}

		public void TakeDamage(int damage)
		{
			health -= damage;
			GameFlowHandlerClient.instance.ClientToServerUpdateHealth(health);
		}
	}
}
