using System.Collections.Generic;
using UnityEngine;

namespace Shapes
{
	public abstract class ShapesObjPool<T, P> : MonoBehaviour
	{
		private const int ALLOCATION_COUNT_WARNING = 500;

		private const int ALLOCATION_COUNT_CAP = 1000;

		private Stack<T> elementsPassive;

		private Dictionary<int, T> elementsActive;

		private static P instance;

		private int ElementCount => 0;

		public T ImmediateModeElement => default(T);

		public static int InstanceElementCount => 0;

		public static int InstanceElementCountActive => 0;

		public static bool InstanceExists => false;

		public abstract string PoolTypeName { get; }

		public static P Instance => default(P);

		private static P CreatePool()
		{
			return default(P);
		}

		private void ClearData()
		{
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		public T GetElement(int id)
		{
			return default(T);
		}

		public T AllocateElement(int id)
		{
			return default(T);
		}

		public void ReleaseElement(int id)
		{
		}

		private T CreateElement(int id)
		{
			return default(T);
		}

		public abstract void OnCreatedNewComponent(T comp);
	}
}
