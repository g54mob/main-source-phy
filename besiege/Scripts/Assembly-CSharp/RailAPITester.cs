using UnityEngine;

public class RailAPITester : MonoBehaviour
{
	public enum RailAPITestType
	{
		None = 0,
		FindInstalledMachines = 1,
		QuerySubscribedItemsAsync = 2,
		ListAllUploads = 3,
		DeleteAllUploads = 4,
		LeaveAllRooms = 5
	}

	[SerializeField]
	private RailAPITestType performTestType;
}
