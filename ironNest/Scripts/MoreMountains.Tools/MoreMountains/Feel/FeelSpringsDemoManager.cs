using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.Feel
{
	[AddComponentMenu(null)]
	public class FeelSpringsDemoManager : MonoBehaviour
	{
		[Header("Bindings")]
		public List<GameObject> DemoObjects;

		[MMReadOnly]
		public int CurrentIndex;

		protected virtual void Start()
		{
		}

		public virtual void NextDemo()
		{
		}

		public virtual void PreviousDemo()
		{
		}

		protected virtual void EnableCurrentDemo()
		{
		}
	}
}
