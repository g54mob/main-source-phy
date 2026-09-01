using UnityEngine;

public class FindDataHandler : MonoBehaviour
{
	public DataHandler data;

	private void Start()
	{
		data = base.transform.root.GetComponentInChildren<DataHandler>();
	}
}
