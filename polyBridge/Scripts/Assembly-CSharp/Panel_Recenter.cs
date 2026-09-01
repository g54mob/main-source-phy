using UnityEngine;
using UnityEngine.UI;

public class Panel_Recenter : MonoBehaviour
{
	public Button m_RecenterButton;

	private float m_DisplayTimer;

	private readonly float DISPLAY_DELAY_SECONDS = 1f;

	private readonly float DISPLAY_DELAY_SECONDS_SANDBOX = 5f;

	private readonly Plane[] m_CameraFrustumPlanes = new Plane[6];

	private void Start()
	{
		m_RecenterButton.onClick.AddListener(OnRecenter);
	}

	public void OnLayoutLoaded()
	{
		m_DisplayTimer = 0f;
	}

	public void UpdateManual()
	{
		TerrainIsland leftTerrain = TerrainIslands.GetLeftTerrain();
		if (!leftTerrain)
		{
			return;
		}
		if (ActivePanels.m_Panels.Count > 0 || GameUI.m_Instance.m_SandboxEditRamp.IsEditingSplinePoints() || GameUI.m_Instance.m_SandboxEditCustomShapeTools.gameObject.activeInHierarchy)
		{
			m_DisplayTimer = 0f;
			base.gameObject.SetActive(value: false);
			return;
		}
		Bounds bounds = new Bounds(leftTerrain.transform.position, Vector3.zero);
		foreach (TerrainIsland terrain in TerrainIslands.m_Terrains)
		{
			if ((bool)terrain)
			{
				bounds.Encapsulate(terrain.m_MeshRenderer.bounds);
			}
		}
		foreach (WaterBlock waterBlock in WaterBlocks.m_WaterBlocks)
		{
			if ((bool)waterBlock)
			{
				bounds.Encapsulate(waterBlock.m_SurfaceMeshRenderer.bounds);
				bounds.Encapsulate(waterBlock.m_SidesMeshRenderer.bounds);
			}
		}
		foreach (Vehicle vehicle in Vehicles.m_Vehicles)
		{
			if ((bool)vehicle)
			{
				bounds.Encapsulate(vehicle.ComputeBounds());
			}
		}
		foreach (ZedAxisVehicle vehicle2 in ZedAxisVehicles.m_Vehicles)
		{
			if ((bool)vehicle2)
			{
				bounds.Encapsulate(vehicle2.m_MeshRenderer.bounds);
			}
		}
		foreach (BridgeJoint joint in BridgeJoints.m_Joints)
		{
			if ((bool)joint && joint.gameObject.activeInHierarchy)
			{
				bounds.Encapsulate(joint.transform.position);
			}
		}
		foreach (Ramp ramp in Ramps.m_Ramps)
		{
			foreach (MeshRenderer pole in ramp.m_Poles)
			{
				if ((bool)pole)
				{
					bounds.Encapsulate(pole.bounds);
				}
			}
		}
		foreach (Platform platform in Platforms.m_Platforms)
		{
			foreach (MeshRenderer pole2 in platform.m_Poles)
			{
				if ((bool)pole2)
				{
					bounds.Encapsulate(pole2.bounds);
				}
			}
		}
		foreach (CustomShape shape in CustomShapes.m_Shapes)
		{
			bounds.Encapsulate(shape.m_MeshRenderer.bounds);
		}
		foreach (ClipboardJoint joint2 in BridgeTraceShadow.m_Joints)
		{
			bounds.Encapsulate(joint2.transform.position);
		}
		GeometryUtility.CalculateFrustumPlanes(Cameras.MainCamera(), m_CameraFrustumPlanes);
		if (!GeometryUtility.TestPlanesAABB(m_CameraFrustumPlanes, bounds) && !ClipboardManager.ReadyToPaste() && (!BridgeTrace.IsTracingActive() || !BridgeTrace.TracingFollowsMouse()))
		{
			m_DisplayTimer += Time.unscaledDeltaTime;
			float num = ((GameStateManager.GetState() == GameState.SANDBOX) ? DISPLAY_DELAY_SECONDS_SANDBOX : DISPLAY_DELAY_SECONDS);
			base.gameObject.SetActive(m_DisplayTimer > num);
		}
		else
		{
			m_DisplayTimer = 0f;
			base.gameObject.SetActive(value: false);
		}
	}

	private void OnRecenter()
	{
		if (GameStateManager.GetState() == GameState.SIM)
		{
			PointsOfView.m_PointsOfView[PointOfViewType.SIM_LEFT].FrameObjects(Game.GetLevelId());
			PointsOfView.SnapTo(PointOfViewType.SIM_LEFT);
			Profiles.m_ActiveProfile.m_PointOfViewType = PointOfViewType.SIM_LEFT;
		}
		else
		{
			PointsOfView.m_PointsOfView[PointOfViewType.BUILD].FrameObjects(Game.GetLevelId());
			if (Game.IsCurrentLevelTutorial())
			{
				PointsOfView.m_PointsOfView[PointOfViewType.BUILD].m_OrthographicsSize = GameSettings.TutorialOrthographicSize();
			}
			PointsOfView.SnapTo(PointOfViewType.BUILD);
		}
		m_DisplayTimer = 0f;
	}
}
