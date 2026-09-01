using UnityEngine;

namespace ModIO.UI
{
	public class TagContainerItem : MonoBehaviour
	{
		public GenericTextComponent tagName;

		private string tagNameValue;

		public GenericTextComponent categoryName;

		public string TagName
		{
			get
			{
				return tagNameValue;
			}
			set
			{
				tagNameValue = value;
				tagName.text = tagNameValue;
			}
		}
	}
}
