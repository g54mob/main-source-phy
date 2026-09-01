using UnityEngine;

namespace Landfall.TABC
{
	public class TABCBoardParticles : MonoBehaviour
	{
		public static TABCBoardParticles instance;

		public ParticleSystem[] place;

		public ParticleSystem[] remove;

		public ParticleSystem[] level2;

		public ParticleSystem[] level3;

		private void Awake()
		{
			instance = this;
		}

		public void PlayPlace(Vector3 pos)
		{
			for (int i = 0; i < place.Length; i++)
			{
				place[i].transform.position = pos;
				place[i].Emit(25);
			}
		}

		public void PlayRemove(Vector3 pos)
		{
			for (int i = 0; i < remove.Length; i++)
			{
				remove[i].transform.position = pos;
				remove[i].Emit(15);
			}
		}

		public void PlayLevelUp(Vector3 pos, int level)
		{
			ParticleSystem[] array = level2;
			if (level == 3)
			{
				array = level3;
			}
			for (int i = 0; i < array.Length; i++)
			{
				array[i].transform.position = pos;
				array[i].Play();
			}
		}
	}
}
