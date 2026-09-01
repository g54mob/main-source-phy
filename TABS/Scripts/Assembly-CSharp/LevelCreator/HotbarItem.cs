using UnityEngine;
using UnityEngine.Events;

namespace LevelCreator
{
	public class HotbarItem
	{
		public Sprite icon;

		public UnityAction callback;

		public string group;

		public string name;

		public float normalizedSize;

		public string temp_id;
	}
}
