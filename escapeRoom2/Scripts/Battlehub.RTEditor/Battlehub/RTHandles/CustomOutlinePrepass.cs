using UnityEngine;

namespace Battlehub.RTHandles
{
	public class CustomOutlinePrepass : MonoBehaviour, ICustomOutlinePrepass
	{
		public Renderer Renderer;

		public Material PrepassMaterial;

		public Renderer GetRenderer()
		{
			return Renderer;
		}

		public Material GetOutlinePrepassMaterial()
		{
			return PrepassMaterial;
		}
	}
}
