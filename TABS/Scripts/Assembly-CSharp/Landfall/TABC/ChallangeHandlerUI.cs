using UnityEngine;

namespace Landfall.TABC
{
	public class ChallangeHandlerUI : MonoBehaviour
	{
		public ChallangeScreen pickedChallange;

		public CodeAnimation codeAnim;

		public ChallangeScreen[] challanges;

		public Populate populate;

		public static ChallangeHandlerUI instance;

		private bool isPicking;

		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
		}

		public void NewChallange(ChallangeTeir challange)
		{
			codeAnim.PlayIn();
			ClearChallanges();
			SpawnChallanges(challange);
		}

		private void ClearChallanges()
		{
			if (challanges == null)
			{
				return;
			}
			for (int i = 0; i < challanges.Length; i++)
			{
				if ((bool)challanges[i])
				{
					Object.Destroy(challanges[i].gameObject);
				}
			}
		}

		private void SpawnChallanges(ChallangeTeir challange)
		{
			isPicking = true;
			challanges = populate.DoPopulate<ChallangeScreen>().ToArray();
			for (int i = 0; i < challanges.Length; i++)
			{
				challanges[i].SetInfo(challange.items[Random.Range(0, challange.items.Length)], challange.battles[Random.Range(0, challange.battles.Length)]);
			}
		}

		public void PickChallange(ChallangeScreen challangeToPick)
		{
			isPicking = false;
			codeAnim.PlayOut();
			pickedChallange = challangeToPick;
		}

		private void PickRandomChallange()
		{
			PickChallange(challanges[Random.Range(0, challanges.Length)]);
		}

		public void ForceStopPicking()
		{
			if (isPicking)
			{
				PickRandomChallange();
			}
		}

		public ChallangeScreen GetPickedChallange()
		{
			return pickedChallange;
		}
	}
}
