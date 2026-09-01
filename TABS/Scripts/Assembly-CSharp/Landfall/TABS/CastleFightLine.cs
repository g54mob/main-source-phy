using UnityEngine;

namespace Landfall.TABS
{
	public class CastleFightLine : MonoBehaviour
	{
		public Team team;

		private static Transform redLineInstance;

		private static Transform blueLineInstance;

		public static Transform RedLine => redLineInstance;

		public static Transform BlueLine => blueLineInstance;

		private void Awake()
		{
			if (team == Team.Red)
			{
				redLineInstance = base.transform;
			}
			else
			{
				blueLineInstance = base.transform;
			}
		}
	}
}
