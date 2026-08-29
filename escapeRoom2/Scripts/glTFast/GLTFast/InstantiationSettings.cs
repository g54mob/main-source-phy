using System;
using UnityEngine;

namespace GLTFast
{
	[Serializable]
	public class InstantiationSettings
	{
		[SerializeField]
		[Tooltip("Filter component instantiation based on type")]
		private ComponentType mask = ComponentType.All;

		[SerializeField]
		[Tooltip("Instantiated objects will be assigned to this layer")]
		private int layer;

		[SerializeField]
		[Tooltip("When checked, calculate the mesh bounds on every frame, even when the mesh is not visible")]
		private bool skinUpdateWhenOffscreen = true;

		[SerializeField]
		[Tooltip("Light intensity values are multiplied by this factor")]
		private float lightIntensityFactor = 1f;

		[SerializeField]
		[Tooltip("Scene object creation method. Determines whether or when a GameObject/Entity representing the scene should get created.")]
		private SceneObjectCreation sceneObjectCreation = SceneObjectCreation.WhenMultipleRootNodes;

		public ComponentType Mask
		{
			get
			{
				return mask;
			}
			set
			{
				mask = value;
			}
		}

		public int Layer
		{
			get
			{
				return layer;
			}
			set
			{
				layer = value;
			}
		}

		public bool SkinUpdateWhenOffscreen
		{
			get
			{
				return skinUpdateWhenOffscreen;
			}
			set
			{
				skinUpdateWhenOffscreen = value;
			}
		}

		public float LightIntensityFactor
		{
			get
			{
				return lightIntensityFactor;
			}
			set
			{
				lightIntensityFactor = value;
			}
		}

		public SceneObjectCreation SceneObjectCreation
		{
			get
			{
				return sceneObjectCreation;
			}
			set
			{
				sceneObjectCreation = value;
			}
		}
	}
}
