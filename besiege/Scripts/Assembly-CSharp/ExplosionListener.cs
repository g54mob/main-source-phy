using UnityEngine;

public class ExplosionListener : MonoBehaviour
{
	public ExplodeOnCollideBlock Bomb;

	public GameObject insignaToActivate;

	private void update()
	{
		if (Bomb.hasExploded)
		{
			insignaToActivate.SetActive(true);
		}
	}
}
