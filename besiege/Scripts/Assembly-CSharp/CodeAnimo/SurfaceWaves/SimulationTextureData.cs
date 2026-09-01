using UnityEngine;

namespace CodeAnimo.SurfaceWaves
{
	public class SimulationTextureData : ScriptableObject
	{
		[HideInInspector]
		[SerializeField]
		public Vector4[] pixels;
	}
}
