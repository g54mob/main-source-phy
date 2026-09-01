using Landfall.TABS;
using UnityEngine;

namespace TFBGames
{
	public class MoveRigidbodiesRoot : MonoBehaviour
	{
		[SerializeField]
		private Vector3 worldDelta;

		private void Awake()
		{
			Go();
		}

		private void Go()
		{
			base.transform.root.GetComponent<Unit>().Hip.parent.position += worldDelta;
		}
	}
}
