using System;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
	public class MMPersistent : MMPersistentBase
	{
		[Serializable]
		public struct Data
		{
			public Vector3 Position;

			public Quaternion LocalRotation;

			public Vector3 LocalScale;

			public bool ActiveState;

			public List<ComponentData> ComponentEnabledStates;
		}

		[Serializable]
		public struct ComponentData
		{
			public string Name;

			public bool EnabledState;
		}

		[Header("Properties")]
		[Tooltip("whether or not to save this object's position")]
		public bool SavePosition;

		[Tooltip("whether or not to save this object's rotation")]
		public bool SaveLocalRotation;

		[Tooltip("whether or not to save this object's scale")]
		public bool SaveLocalScale;

		[Tooltip("whether or not to save this object's active state")]
		public bool SaveActiveState;

		[Tooltip("whether or not to save this object's components' enabled states")]
		public bool SaveEnabledStates;

		public override string OnSave()
		{
			return null;
		}

		public override void OnLoad(string data)
		{
		}

		protected virtual List<ComponentData> GetCurrentComponents()
		{
			return null;
		}
	}
}
