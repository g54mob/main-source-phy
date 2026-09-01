using TFBGames;
using UnityEngine;

namespace Landfall.TABS
{
	[ExecuteInEditMode]
	public class MapSettings : MonoBehaviour
	{
		public static MapSettings Instance;

		public float teamBorder;

		public Vector3 m_mapCenter = Vector3.zero;

		public Vector3 cameraPositionOffset = Vector3.zero;

		public float m_mapRadius = 100f;

		public float mapRadiusYMultiplier = 2f;

		public Material mapGroundMaterial;

		public static float KillHeight = -25f;

		public float setKillHeight = -25f;

		private void OnEnable()
		{
			KillHeight = setKillHeight;
		}

		private void Awake()
		{
			Instance = this;
			if (GetComponent<SceneSettings>() == null)
			{
				base.gameObject.AddComponent<SceneSettings>().m_UseNewPlacementSystem = true;
			}
			else
			{
				base.gameObject.GetComponent<SceneSettings>().m_UseNewPlacementSystem = true;
			}
		}

		private void Start()
		{
			MainCam mainCam = ServiceLocator.GetService<PlayerCamerasManager>()?.GetMainCam(TFBGames.Player.One);
			if (mainCam != null && mainCam.isPlacementCam)
			{
				Vector3 position = mainCam.m_cameraRootTransform.position;
				position.x = teamBorder;
				mainCam.m_cameraRootTransform.position = position;
			}
		}

		private void OnDestroy()
		{
			Instance = null;
			KillHeight = -25f;
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = new Color(0f, 0f, 1f, 0.5f);
			Gizmos.DrawLine(new Vector3(teamBorder + 0.1f, 0.01f, -200f), new Vector3(teamBorder + 0.1f, 0.01f, 200f));
			Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
			Gizmos.DrawLine(new Vector3(teamBorder - 0.1f, 0.01f, -200f), new Vector3(teamBorder - 0.1f, 0.01f, 200f));
		}

		public Team GetTeamTerritory(Vector3 point)
		{
			if (point.x > teamBorder)
			{
				return Team.Blue;
			}
			return Team.Red;
		}
	}
}
