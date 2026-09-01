using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	[RequireComponent(typeof(Collider))]
	public class MMRandomBoundsInstantiator : MonoBehaviour
	{
		public enum StartModes
		{
			Awake = 0,
			Start = 1,
			None = 2
		}

		public enum ScaleModes
		{
			Uniform = 0,
			Vector3 = 1
		}

		[Header("Random instantiation")]
		public StartModes StartMode;

		public string InstantiatedObjectName;

		public bool ParentInstantiatedToThisObject;

		public bool DestroyPreviouslyInstantiatedObjects;

		[Header("Spawn")]
		public List<GameObject> RandomPool;

		[MMVector(new string[] { "Min", "Max" })]
		public Vector2Int Quantity;

		[Header("Scale")]
		public ScaleModes ScaleMode;

		[MMEnumCondition("ScaleMode", new int[] { 0 })]
		public float MinScale;

		[MMEnumCondition("ScaleMode", new int[] { 0 })]
		public float MaxScale;

		[MMEnumCondition("ScaleMode", new int[] { 1 })]
		public Vector3 MinVectorScale;

		[MMEnumCondition("ScaleMode", new int[] { 1 })]
		public Vector3 MaxVectorScale;

		[Header("Test")]
		[MMInspectorButton("Instantiate")]
		public bool InstantiateButton;

		protected Collider _collider;

		protected List<GameObject> _instantiatedGameObjects;

		protected Vector3 _newScale;

		protected virtual void Awake()
		{
		}

		protected virtual void Start()
		{
		}

		protected virtual void Instantiate()
		{
		}

		public virtual void InstantiateRandomObject()
		{
		}
	}
}
