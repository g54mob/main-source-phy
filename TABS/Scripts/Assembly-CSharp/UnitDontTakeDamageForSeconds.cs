using UnityEngine;

public class UnitDontTakeDamageForSeconds : MonoBehaviour
{
	public bool playOnStart;

	public float seconds = 2f;

	private DataHandler data;

	private void Start()
	{
		data = base.transform.root.GetComponentInChildren<DataHandler>();
		if (playOnStart)
		{
			Go();
		}
	}

	public void Go()
	{
		if (!data)
		{
			data = base.transform.root.GetComponentInChildren<DataHandler>();
		}
		data.immunityForSeconds = seconds;
	}
}
