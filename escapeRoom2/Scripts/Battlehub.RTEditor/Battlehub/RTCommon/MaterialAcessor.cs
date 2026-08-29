using UnityEngine;

namespace Battlehub.RTCommon
{
	public class MaterialAcessor
	{
		private GameObject m_gameObject;

		private int m_materialIndex;

		public Material material => m_gameObject.GetComponent<Renderer>().sharedMaterials[m_materialIndex];

		public Texture mainTexture
		{
			get
			{
				return material.MainTexture();
			}
			set
			{
				material.MainTexture(value);
			}
		}

		public Texture normalTexture
		{
			get
			{
				return material.NormalTexture();
			}
			set
			{
				material.NormalTexture(value);
			}
		}

		public Texture normalTextureZeroSmoothness
		{
			get
			{
				return material.NormalTexture();
			}
			set
			{
				material.NormalTexture(value, 0f);
			}
		}

		public Color color
		{
			get
			{
				return material.Color();
			}
			set
			{
				material.Color(value);
			}
		}

		public MaterialAcessor(GameObject gameObject, int materialIndex)
		{
			m_gameObject = gameObject;
			m_materialIndex = materialIndex;
		}
	}
}
