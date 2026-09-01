using System;
using System.Collections.Generic;
using Poly.Physics;

namespace Poly.Base
{
	public class Registry<TItem> : Singleton<Registry<TItem>> where TItem : WorldObject
	{
		public struct Event
		{
			public RegistryOp type;

			public TItem item;

			public int tempId;

			public static int nextTempId;

			public Event(RegistryOp type, TItem item)
			{
				this.type = type;
				this.item = item;
				tempId = nextTempId++;
			}

			public static int Comparison(Event a, Event b)
			{
				int num = 0;
				if (a.item is IComparable comparable)
				{
					num = comparable.CompareTo(b.item);
				}
				if (num == 0)
				{
					num = a.item.persistentId - b.item.persistentId;
				}
				if (num == 0)
				{
					num = a.tempId - b.tempId;
				}
				return num;
			}
		}

		private List<Event> events = new List<Event>();

		public static int EventCount => Singleton<Registry<TItem>, int>.instance.events.Count;

		public static void Add(TItem item)
		{
			Singleton<Registry<TItem>, int>.instance.events.Add(new Event(RegistryOp.Add, item));
		}

		public static void Remove(TItem item)
		{
			Singleton<Registry<TItem>, int>.instance.events.Add(new Event(RegistryOp.Remove, item));
		}

		public static List<Event> SortAndGetEvents()
		{
			Singleton<Registry<TItem>, int>.instance.events.Sort(Event.Comparison);
			return Singleton<Registry<TItem>, int>.instance.events;
		}

		internal static List<Event> GetAllEvents_DontModify()
		{
			return Singleton<Registry<TItem>, int>.instance.events;
		}

		public static void Clear()
		{
			Singleton<Registry<TItem>, int>.instance.events.Clear();
			Event.nextTempId = 0;
		}
	}
}
