using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Landfall.TABC
{
	public class ChallangeScreen : MonoBehaviour
	{
		public BattleLayout battle;

		public Image challangeImg;

		public Image itemImg;

		public TextMeshProUGUI challangeTitle;

		public TextMeshProUGUI itemText;

		private void Awake()
		{
		}

		private void Start()
		{
		}

		public void Select()
		{
			ChallangeHandlerUI.instance.PickChallange(this);
		}

		public void Remove()
		{
		}

		public void SetInfo(Item item, NeutralBattle challange)
		{
			battle = challange.battle;
			challangeImg.sprite = challange.challangeImage;
			challangeTitle.text = challange.challangeName;
			itemImg.sprite = item.itemImage;
			itemText.text = item.itemName;
		}
	}
}
