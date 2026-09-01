using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMRandomInstantiator : MonoBehaviour
	{
		public enum StartModes
		{
			Awake = 0,
			Start = 1,
			None = 2
		}

		[Header("Random instantiation")]
		public StartModes StartMode;

		public string InstantiatedObjectName;

		public bool ParentInstantiatedToThisObject;

		public bool DestroyPreviouslyInstantiatedObject;

		public List<GameObject> RandomPool;

		[Header("Test")]
		[MMInspectorButton("InstantiateRandomObject")]
		public bool InstantiateButton;

		protected GameObject _instantiatedGameObject;

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
		}

		public virtual void InstantiateRandomObject()
		{
		}
	}
}
