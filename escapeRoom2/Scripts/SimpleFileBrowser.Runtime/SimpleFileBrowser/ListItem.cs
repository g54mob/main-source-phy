using UnityEngine;

namespace SimpleFileBrowser
{
	[RequireComponent(typeof(RectTransform))]
	public class ListItem : MonoBehaviour
	{
		private IListViewAdapter adapter;

		public object Tag { get; set; }

		public int Position { get; set; }

		internal void SetAdapter(IListViewAdapter listView)
		{
			adapter = listView;
		}

		public void OnClick()
		{
			if (adapter.OnItemClicked != null)
			{
				adapter.OnItemClicked(this);
			}
		}
	}
}
