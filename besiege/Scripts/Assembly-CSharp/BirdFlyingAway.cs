using UnityEngine;

public class BirdFlyingAway : MonoBehaviour
{
	public SineBob sineBobCode;

	public GameObject BirdVisual;

	public GameObject BirdStillVisual;

	public Collider[] Colliders;

	public float FlyAwaySpeed;

	private bool FlyingAway;

	private void Start()
	{
	}

	private void Update()
	{
		if (FlyingAway)
		{
			BirdVisual.transform.position += BirdVisual.transform.forward * Time.deltaTime * (0f - FlyAwaySpeed);
			BirdVisual.transform.position += BirdVisual.transform.up * Time.deltaTime * FlyAwaySpeed;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag != "LevelObject")
		{
			BirdStillVisual.SetActive(false);
			BirdVisual.SetActive(true);
			BirdVisual.transform.LookAt(base.transform);
			BirdVisual.GetComponent<Animator>().enabled = true;
			for (int i = 0; i < Colliders.Length; i++)
			{
				Colliders[i].enabled = false;
			}
			FlyingAway = true;
		}
	}
}
