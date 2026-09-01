using Poly.Base;
using Poly.Draw;
using UnityEngine;

namespace Poly.Collide.Viewers
{
	public class AabbViewer : SingletonBehaviour<AabbViewer>
	{
		private static bool haveInstance;

		private void Start()
		{
			haveInstance = (bool)SingletonBehaviour<AabbViewer>.instance && (bool)Singleton<GlDrawer, int>.instance;
		}

		private void Update()
		{
		}

		public static void Draw(int numAabbs, AabbInfo[] aabbs)
		{
			if (haveInstance && SingletonBehaviour<AabbViewer>.instance.enabled)
			{
				_ = 0.01f * Vector3.back;
				GlDrawer.color = Color.blue;
				for (int i = 0; i < numAabbs; i++)
				{
				}
			}
		}
	}
}
