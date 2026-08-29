using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace GLTFast
{
	public class GameObjectSceneInstance
	{
		private List<Camera> m_Cameras;

		private List<Light> m_Lights;

		public IReadOnlyList<Camera> Cameras => m_Cameras;

		public IReadOnlyList<Light> Lights => m_Lights;

		public MaterialsVariantsControl MaterialsVariantsControl { get; private set; }

		public Animation LegacyAnimation { get; private set; }

		public Playable? Playable { get; internal set; }

		internal void AddCamera(Camera camera)
		{
			if (m_Cameras == null)
			{
				m_Cameras = new List<Camera>();
			}
			m_Cameras.Add(camera);
		}

		internal void AddLight(Light light)
		{
			if (m_Lights == null)
			{
				m_Lights = new List<Light>();
			}
			m_Lights.Add(light);
		}

		internal void SetMaterialsVariantsControl(MaterialsVariantsControl control)
		{
			MaterialsVariantsControl = control;
		}

		internal void SetLegacyAnimation(Animation animation)
		{
			LegacyAnimation = animation;
		}
	}
}
