using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMReorderableAttributeAttribute : PropertyAttribute
	{
		public bool add;

		public bool remove;

		public bool draggable;

		public bool singleLine;

		public string elementNameProperty;

		public string elementNameOverride;

		public string elementIconPath;

		public MMReorderableAttributeAttribute()
		{
		}

		public MMReorderableAttributeAttribute(string elementNameProperty)
		{
		}

		public MMReorderableAttributeAttribute(string elementNameProperty, string elementIconPath)
		{
		}

		public MMReorderableAttributeAttribute(string elementNameProperty, string elementNameOverride, string elementIconPath)
		{
		}

		public MMReorderableAttributeAttribute(bool add, bool remove, bool draggable, string elementNameProperty = null, string elementIconPath = null)
		{
		}

		public MMReorderableAttributeAttribute(bool add, bool remove, bool draggable, string elementNameProperty = null, string elementNameOverride = null, string elementIconPath = null)
		{
		}
	}
}
