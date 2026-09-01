using UnityEngine;

namespace Poly.Base
{
	public class SingletonBehaviour<TClass> : PolyBehaviour where TClass : SingletonBehaviour<TClass>
	{
		protected static TClass _instance;

		private static bool allowSearchingWithFindObject;

		public static TClass instance
		{
			get
			{
				if (allowSearchingWithFindObject)
				{
					if ((object)_instance == null)
					{
						_instance = Object.FindObjectOfType<TClass>() ?? Object.FindObjectOfType<TClass>(includeInactive: true);
					}
					allowSearchingWithFindObject = false;
				}
				return _instance;
			}
		}

		public static bool instanceExists => _instance;

		protected void Awake()
		{
			if ((object)_instance == null)
			{
				_instance = this as TClass;
				allowSearchingWithFindObject = false;
			}
			else
			{
				_ = _instance;
			}
		}

		protected void OnDestroy()
		{
			if ((object)_instance == this)
			{
				_instance = null;
				allowSearchingWithFindObject = true;
			}
		}

		static SingletonBehaviour()
		{
			allowSearchingWithFindObject = true;
			RuntimeInitializer.AddReinitAction(Init);
		}

		private static void Init()
		{
			_instance = null;
			allowSearchingWithFindObject = true;
		}
	}
}
