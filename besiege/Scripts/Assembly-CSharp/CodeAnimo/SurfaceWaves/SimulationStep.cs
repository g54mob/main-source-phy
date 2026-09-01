using UnityEngine;

namespace CodeAnimo.SurfaceWaves
{
	public abstract class SimulationStep : MonoBehaviour
	{
		public abstract void LoadData();

		public abstract void RunStep();
	}
}
