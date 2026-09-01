using UnityEngine;

namespace Landfall.TABC
{
	[CreateAssetMenu(fileName = "Item", menuName = "TABC/Item", order = 2)]
	public class Item : ScriptableObject
	{
		public string itemName;

		[TextArea]
		public string description;

		public Sprite itemImage;
	}
}
