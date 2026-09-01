using UnityEngine;

public class UnitDontFallForSeconds : MonoBehaviour
{
	public float seconds = 2f;

	private DataHandler data;

	private void Start()
	{
		data = base.transform.root.GetComponentInChildren<DataHandler>();
	}

	public void Go()
	{
		if (!data)
		{
			data = base.transform.root.GetComponentInChildren<DataHandler>();
		}
		data.cantFallForSeconds = seconds;
	}
}
