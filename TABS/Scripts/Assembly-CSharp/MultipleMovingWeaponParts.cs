using UnityEngine;

public class MultipleMovingWeaponParts : MonoBehaviour
{
	private MovingWeaponPart[] movingWeaponParts;

	private void Start()
	{
		movingWeaponParts = GetComponents<MovingWeaponPart>();
	}

	private void Update()
	{
	}

	public void PlayNr1()
	{
		if (movingWeaponParts != null)
		{
			movingWeaponParts[0]?.PlayRecoilAnimation();
		}
	}

	public void PlayNr2()
	{
		if (movingWeaponParts != null)
		{
			movingWeaponParts[1]?.PlayRecoilAnimation();
		}
	}

	public void PlayNr3()
	{
		if (movingWeaponParts != null)
		{
			movingWeaponParts[2]?.PlayRecoilAnimation();
		}
	}

	public void PlayNr4()
	{
		if (movingWeaponParts != null)
		{
			movingWeaponParts[3]?.PlayRecoilAnimation();
		}
	}

	public void PlayNr5()
	{
		if (movingWeaponParts != null)
		{
			movingWeaponParts[4]?.PlayRecoilAnimation();
		}
	}
}
