using System;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using TFBGames;
using UnityEngine;

namespace Landfall.TABS.UnitPlacement
{
	public class PlacementCursor : GameStateListener
	{
		[SerializeField]
		private LayerMask m_primaryActionMask;

		[SerializeField]
		private float m_maxPlacementAngleEuler = 85f;

		[SerializeField]
		private LayerMask m_invalidUnitPlacementMask;

		[SerializeField]
		private LayerMask m_secondaryActionMask;

		[SerializeField]
		private LayerMask m_unitMask;

		private PlacementCursorState m_state;

		private PlacementCursorState m_lastState;

		[SerializeField]
		private Material m_allowPlaceRedMaterial;

		[SerializeField]
		private Material m_allowPlaceBlueMaterial;

		private Material m_redMaterial;

		private Material m_blueMaterial;

		[SerializeField]
		private Material m_denyPlaceMaterial;

		[SerializeField]
		private Material m_invalidPlaceMaterial;

		[SerializeField]
		private Transform m_positionRoot;

		[SerializeField]
		private MeshRenderer m_fillCircleRenderer;

		[SerializeField]
		private MeshRenderer m_circleRenderer;

		[SerializeField]
		private Transform m_cursorObject;

		private Renderer m_renderer;

		private MeshFilter m_meshFilter;

		private GameObject m_testBoxGO;

		private BoxCollider m_testBoxCollider;

		private UnitPlacementBrush m_unitPlacementBrush;

		private SettingsInstance m_borderColorSetting;

		private InputService inputService;

		private GameObject m_hoveredGameObject;

		private Unit m_hoveredUnit;

		private Vector3 m_targetWorldPosition;

		private MeshFilter m_cursorMeshFilter;

		private Mesh m_defaultCursorMesh;

		private Vector3 m_defaultCursorPosition;

		public LayerMask PrimaryActionMask
		{
			get
			{
				return m_primaryActionMask;
			}
			set
			{
				m_primaryActionMask = value;
			}
		}

		public float MaxPlacementAngleEuler => m_maxPlacementAngleEuler;

		public LayerMask InvalidUnitPlacementMask
		{
			get
			{
				return m_invalidUnitPlacementMask;
			}
			set
			{
				m_invalidUnitPlacementMask = value;
			}
		}

		public LayerMask SecondaryActionMask
		{
			get
			{
				return m_secondaryActionMask;
			}
			set
			{
				m_secondaryActionMask = value;
			}
		}

		public LayerMask UnitMask => m_unitMask;

		public Vector3 CursorObjectScale => m_cursorObject.localScale;

		public Vector3 FillCircleRendererScale => m_fillCircleRenderer.transform.localScale;

		public bool IsFillCircleVisible
		{
			get
			{
				if (m_fillCircleRenderer != null)
				{
					return m_fillCircleRenderer.enabled;
				}
				return false;
			}
		}

		public PlacementCursorState State => m_state;

		protected override void Awake()
		{
			base.Awake();
			m_unitPlacementBrush = ServiceLocator.GetService<GameModeService>().CurrentGameMode.Brush;
			m_renderer = m_cursorObject.GetComponent<Renderer>();
			m_meshFilter = m_cursorObject.GetComponent<MeshFilter>();
			SetCursorState(PlacementCursorState.AllowRedPlacement);
			GlobalSettingsHandler service = ServiceLocator.GetService<GlobalSettingsHandler>();
			m_borderColorSetting = service.GetSettingsInstance("GAMEPLAY_FLIP_COLORS");
			m_borderColorSetting.OnValueChanged += TeamBorderColorChanged;
			TeamBorderColorChanged(m_borderColorSetting.currentValue);
			inputService = ServiceLocator.GetService<InputService>();
			SetCircleFillAmount(0f);
			ShowFillCircle(show: false);
			m_cursorMeshFilter = m_cursorObject.GetComponent<MeshFilter>();
			m_defaultCursorMesh = m_cursorMeshFilter.sharedMesh;
			m_defaultCursorPosition = m_cursorObject.localPosition;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			m_borderColorSetting.OnValueChanged -= TeamBorderColorChanged;
		}

		public override void OnEnterPlacementState()
		{
			SetCursorState(PlacementCursorState.InvalidPlacement);
		}

		public override void OnEnterBattleState()
		{
			SetCursorState(PlacementCursorState.Hide);
		}

		private void TeamBorderColorChanged(int value)
		{
			switch (value)
			{
			case 0:
				m_redMaterial = m_allowPlaceRedMaterial;
				m_blueMaterial = m_allowPlaceBlueMaterial;
				break;
			case 1:
				m_redMaterial = m_allowPlaceBlueMaterial;
				m_blueMaterial = m_allowPlaceRedMaterial;
				break;
			default:
				throw new ArgumentOutOfRangeException("Value must be either 0 or 1. Registered value: " + value);
			}
		}

		private void Start()
		{
			GameModeService service = ServiceLocator.GetService<GameModeService>();
			if (service == null)
			{
				Debug.LogError("Game Mode Service is null");
			}
			if (service.CurrentGameMode == null)
			{
				Debug.LogError("Current game mode is null!");
			}
			InitializeBrushCursor();
		}

		private void InitializeBrushCursor()
		{
			(ServiceLocator.GetService<GameModeService>().CurrentGameMode?.Brush)?.SetCursor(this);
		}

		public void ChangedUnitBrush(UnitBlueprint blueprint)
		{
			Unit component = blueprint.UnitBase.GetComponent<Unit>();
			if ((bool)blueprint.PlacementMesh)
			{
				m_cursorObject.localScale = Vector3.one;
				m_cursorObject.localPosition = Vector3.zero;
				m_cursorMeshFilter.sharedMesh = blueprint.PlacementMesh;
			}
			else
			{
				m_cursorObject.localScale = new Vector3(component.PlacementSquare.Size.x * blueprint.sizeMultiplier, m_cursorObject.localScale.y, component.PlacementSquare.Size.z * blueprint.sizeMultiplier);
				m_cursorObject.localPosition = m_defaultCursorPosition;
				m_cursorMeshFilter.sharedMesh = m_defaultCursorMesh;
			}
		}

		public void ResetCursorScale()
		{
			m_cursorObject.localScale = new Vector3(1f, m_cursorObject.localScale.y, 1f);
		}

		public void ReApplyCursorScale(Vector3 cursorObject, Vector3 fillCircle)
		{
			m_cursorObject.localScale = cursorObject;
		}

		private void Update()
		{
			Quaternion rotation = Quaternion.identity;
			SceneSettings.GetTeamTerritory(base.transform.position, out rotation);
			m_cursorObject.rotation = rotation;
		}

		public void SetCursorState(PlacementCursorState state)
		{
			m_lastState = m_state;
			m_state = state;
			if (m_state != m_lastState)
			{
				m_unitPlacementBrush.CursorStateChanged();
			}
			switch (m_state)
			{
			case PlacementCursorState.Hide:
				if (m_renderer != null)
				{
					if (m_hoveredUnit != null)
					{
						m_hoveredUnit.RemoveHighlight();
						m_hoveredUnit = null;
						m_hoveredGameObject = null;
					}
					m_positionRoot.gameObject.SetActive(value: false);
				}
				break;
			case PlacementCursorState.AllowRedPlacement:
				if (m_renderer != null)
				{
					m_positionRoot.gameObject.SetActive(value: true);
					m_renderer.sharedMaterial = m_redMaterial;
				}
				if (m_redMaterial != null)
				{
					m_fillCircleRenderer.sharedMaterial.color = m_redMaterial.color;
					m_circleRenderer.sharedMaterial.color = m_redMaterial.color;
				}
				break;
			case PlacementCursorState.AllowBluePlacement:
				if (m_renderer != null)
				{
					m_positionRoot.gameObject.SetActive(value: true);
					m_renderer.sharedMaterial = m_blueMaterial;
				}
				if (m_blueMaterial != null)
				{
					m_fillCircleRenderer.sharedMaterial.color = m_blueMaterial.color;
					m_circleRenderer.sharedMaterial.color = m_blueMaterial.color;
				}
				break;
			case PlacementCursorState.DenyPlacement:
				if (m_renderer != null)
				{
					m_positionRoot.gameObject.SetActive(value: true);
					m_renderer.sharedMaterial = m_denyPlaceMaterial;
				}
				break;
			case PlacementCursorState.InvalidPlacement:
				if (m_renderer != null)
				{
					m_positionRoot.gameObject.SetActive(value: true);
					m_renderer.sharedMaterial = m_invalidPlaceMaterial;
				}
				break;
			}
			if (inputService != null)
			{
				inputService.SetCursorState(state);
			}
		}

		public void OnStartHoverSceneGameObject(GameObject go, Vector3 position)
		{
			if (m_hoveredUnit != null)
			{
				m_hoveredUnit.RemoveHighlight();
			}
			m_hoveredGameObject = go;
			m_hoveredUnit = go.GetComponent<Unit>();
			if (m_hoveredUnit != null)
			{
				m_hoveredUnit.SetHighlight(Color.white);
			}
			m_targetWorldPosition = position;
		}

		public void OnEndHoverSceneGameObject(GameObject go)
		{
			if (m_hoveredGameObject == go && m_hoveredUnit != null)
			{
				m_hoveredUnit.RemoveHighlight();
				m_hoveredGameObject = null;
				m_hoveredUnit = null;
			}
		}

		public void OnHoverSceneGameObject(GameObject go)
		{
		}

		public void SetCircleFillAmount(float fill)
		{
			if (m_fillCircleRenderer != null)
			{
				m_fillCircleRenderer.sharedMaterial.SetFloat("_Fill", fill);
			}
		}

		public void ShowFillCircle(bool show)
		{
			if (m_fillCircleRenderer != null)
			{
				m_fillCircleRenderer.enabled = show;
			}
		}
	}
}
