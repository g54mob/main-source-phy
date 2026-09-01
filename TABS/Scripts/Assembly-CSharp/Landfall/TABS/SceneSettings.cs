using System;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS.TeamEdge;
using Landfall.TABS.UI.WinConditions;
using Landfall.TABS.UnitPlacement;
using Landfall.TABS.WinConditions;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using UnityEngine;

namespace Landfall.TABS
{
	[ExecuteInEditMode]
	public class SceneSettings : GameStateListener
	{
		public struct TeamRequestReslut
		{
			public bool CanPlace;

			public Team Team;

			public TeamRequestReslut(bool canPlace, Team team)
			{
				CanPlace = canPlace;
				Team = team;
			}
		}

		public struct PlacementRequestResult
		{
			public bool CanPlace;

			public Vector3 Posistion;

			public Quaternion Rotation;

			public PlacementRequestResult(bool canPlace, Vector3 posistion, Quaternion rotation)
			{
				CanPlace = canPlace;
				Posistion = posistion;
				Rotation = rotation;
			}
		}

		[Header("New User Scene Settings.")]
		public bool m_UseNewPlacementSystem;

		[Header("Scene's Team Edge Settings")]
		public EdgeType m_EdgeType;

		public float m_DeadzoneSize;

		[Space(10f)]
		public float m_Rotation;

		[Space(10f)]
		public float m_CircleRadius = 20f;

		public bool m_TeamIsSwapped;

		public UIColorOverwrite UIColorOverwrite;

		private float m_rot;

		private float m_size;

		private Vector3 m_pos;

		private Vector3[] linePoints = new Vector3[2];

		private MapSettingsComponent mapSettingsComponent;

		private static SceneSettings instance;

		private float fadeFactor = 1f;

		private float fadeFactorTarget = 1f;

		private bool isEditingLine;

		private PlacementLineCursor placementLineCursor;

		private PlayerActions m_playerActions;

		private MapSettings mapSettings;

		private const float CONTROLLER_ZOOM_DAMPING = 0.01f;

		private Vector3 offset;

		private UnitPlacementBrush unitBrush;

		public static TABSSceneSettings SerializedSettings
		{
			get
			{
				WinConditionPropagator winConditionPropagator = ServiceLocator.GetService<GameModeService>().CurrentGameMode.WinConditionPropagator;
				return new TABSSceneSettings(instance.offset, instance.m_Rotation, instance.m_CircleRadius, (int)instance.m_EdgeType, instance.m_TeamIsSwapped, winConditionPropagator.GetSerializedWinEvaluators().ToArray());
			}
			set
			{
				WinConditionPropagator winConditionPropagator = ServiceLocator.GetService<GameModeService>().CurrentGameMode.WinConditionPropagator;
				if (value == null)
				{
					instance.mapSettings = instance.GetComponent<MapSettings>();
					if ((bool)instance.mapSettings)
					{
						instance.offset = new Vector3(instance.mapSettings.teamBorder, 0f, 0f);
					}
					else
					{
						instance.offset = Vector3.zero;
					}
					instance.m_Rotation = 0f;
					instance.m_CircleRadius = 0f;
					instance.m_EdgeType = EdgeType.Line;
					instance.m_TeamIsSwapped = false;
					winConditionPropagator.ClearAllWinConditions();
					winConditionPropagator.InjectDefaultWinConditionsForAllTeams();
					InspectorPanel[] array = UnityEngine.Object.FindObjectsOfType<InspectorPanel>();
					for (int i = 0; i < array.Length; i++)
					{
						array[i].UpdateFromCurrentWinconditions();
					}
				}
				else
				{
					instance.offset = value.GetTeamLineCenter();
					instance.m_Rotation = value.TeamLineRotation;
					instance.m_CircleRadius = value.TeamLineRadius;
					instance.m_EdgeType = (EdgeType)value.TeamLineType;
					instance.m_TeamIsSwapped = value.TeamSwap;
					winConditionPropagator.ClearAllWinConditions();
					if (value.WinEvaluators == null || value.WinEvaluators.Length == 0)
					{
						winConditionPropagator.InjectDefaultWinConditionsForAllTeams();
					}
					else
					{
						winConditionPropagator.InjectWinConditionEvaluators(value.WinEvaluators);
					}
					InspectorPanel[] array = UnityEngine.Object.FindObjectsOfType<InspectorPanel>();
					for (int i = 0; i < array.Length; i++)
					{
						array[i].UpdateFromCurrentWinconditions();
					}
				}
				if (Application.isPlaying)
				{
					MapSettingsUI mapSettingsUI = UnityEngine.Object.FindObjectOfType<MapSettingsUI>();
					if (mapSettingsUI != null)
					{
						mapSettingsUI.UpdateUI();
					}
				}
			}
		}

		public static SceneSettings Instance
		{
			get
			{
				if (instance == null)
				{
					instance = UnityEngine.Object.FindObjectOfType<SceneSettings>();
				}
				return instance;
			}
			set
			{
				instance = value;
			}
		}

		public static bool IsEditingLine
		{
			get
			{
				if (instance == null)
				{
					return false;
				}
				return instance.isEditingLine;
			}
		}

		public float FadeFator => fadeFactor;

		public Vector2 Offset
		{
			get
			{
				if (mapSettings == null)
				{
					Initliaze();
				}
				if (Application.isPlaying)
				{
					return new Vector2(offset.x, offset.z);
				}
				return new Vector2(mapSettings.teamBorder, 0f);
			}
		}

		public Vector3 LineCenter
		{
			get
			{
				Vector2 vector = Offset;
				return new Vector3(vector.x, 0f, vector.y);
			}
		}

		public static bool UseNewPlacementSystem
		{
			get
			{
				if (Instance == null)
				{
					return false;
				}
				if (Instance.enabled)
				{
					return Instance.m_UseNewPlacementSystem;
				}
				return false;
			}
		}

		public static bool UseSceneColorOverwrite
		{
			get
			{
				if (Instance == null)
				{
					return false;
				}
				return Instance.UIColorOverwrite != null;
			}
		}

		public void ResetToDefaultTeamLine()
		{
			m_EdgeType = EdgeType.Line;
			m_Rotation = 0f;
			m_rot = 0f;
			m_size = 0f;
			m_TeamIsSwapped = false;
			offset = mapSettings.m_mapCenter;
		}

		public bool ShowCircleSettings()
		{
			return m_EdgeType == EdgeType.Circle;
		}

		private new void Awake()
		{
			if (Application.isPlaying)
			{
				base.Awake();
				Initliaze();
				if ((bool)mapSettings)
				{
					offset = new Vector3(mapSettings.teamBorder, 0f, 0f);
				}
				m_playerActions = PlayerActions.Instance;
			}
			Instance = this;
			if (UIColorOverwrite == null)
			{
				UIColorOverwrite = ScriptableObject.CreateInstance<UIColorOverwrite>();
			}
		}

		private void Start()
		{
			LoadDefaultWinConditions();
			mapSettingsComponent = UnityEngine.Object.FindObjectOfType<MapSettingsComponent>();
		}

		public void LoadDefaultWinConditions()
		{
			if (Application.isPlaying)
			{
				WinConditionPropagator winConditionPropagator = ServiceLocator.GetService<GameModeService>().CurrentGameMode.WinConditionPropagator;
				if (winConditionPropagator.GetWinConditionsForTeam(Team.Red).Length == 0)
				{
					winConditionPropagator.InjectDefaultWinConditionsForAllTeams();
				}
				else if (SerializedSettings.WinEvaluators == null || SerializedSettings.WinEvaluators.Length == 0)
				{
					winConditionPropagator.ClearAllWinConditions();
					winConditionPropagator.InjectDefaultWinConditionsForAllTeams();
				}
				InspectorPanel[] array = UnityEngine.Object.FindObjectsOfType<InspectorPanel>();
				for (int i = 0; i < array.Length; i++)
				{
					array[i].UpdateFromCurrentWinconditions();
				}
				if (Application.isPlaying)
				{
					fadeFactorTarget = 1f;
				}
			}
			if (mapSettingsComponent == null)
			{
				mapSettingsComponent = UnityEngine.Object.FindObjectOfType<MapSettingsComponent>();
			}
		}

		private void Initliaze()
		{
			mapSettings = MapSettings.Instance;
			if (mapSettings == null)
			{
				mapSettings = GetComponent<MapSettings>();
			}
		}

		private void OnEnable()
		{
			Instance = this;
			Initliaze();
		}

		private new void OnDestroy()
		{
			base.OnDestroy();
			Instance = null;
		}

		private void Update()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			fadeFactor = Mathf.Lerp(fadeFactor, fadeFactorTarget, 10f * Time.deltaTime);
			if (!isEditingLine)
			{
				return;
			}
			float num = ((PlayerActions.Instance.InputType == InputType.Controller) ? 0.01f : 1f);
			float num2 = m_playerActions.m_placementZoom.Value * num;
			if (Input.GetMouseButton(1))
			{
				if (unitBrush.RaycastMap(out var pos))
				{
					if (m_EdgeType == EdgeType.Line)
					{
						if (Vector3.Distance(pos, offset) > 2f)
						{
							Vector3 vector = pos - offset;
							vector.y = 0f;
							vector.Normalize();
							float num3 = Mathf.Asin(vector.z) * 57.29578f + 90f;
							if (vector.x > 0f)
							{
								num3 = 0f - num3;
							}
							m_rot = num3;
						}
					}
					else if (m_EdgeType == EdgeType.Circle)
					{
						Vector3 a = pos;
						Vector3 b = offset;
						a.y = 0f;
						b.y = 0f;
						float num4 = Vector3.Distance(a, b);
						if (num4 > 2f)
						{
							m_size = num4;
						}
					}
				}
			}
			else if ((bool)m_playerActions.m_finishEditLine)
			{
				StopLineEditing();
			}
			else
			{
				if (m_EdgeType == EdgeType.Line)
				{
					m_rot += num2 * 60f;
				}
				else if (m_EdgeType == EdgeType.Circle)
				{
					m_size += num2 * Mathf.Pow(m_size, 0.5f) * (float)Math.PI;
					m_size = Mathf.Clamp(m_size, 2f, 10000f);
				}
				if (unitBrush.RaycastMap(out var pos2))
				{
					m_pos = pos2;
				}
				offset = Vector3.Lerp(offset, pos2, Time.deltaTime * 10f);
				placementLineCursor.transform.position = offset;
			}
			if (m_EdgeType == EdgeType.Line)
			{
				float y = Quaternion.Lerp(Quaternion.Euler(0f, m_Rotation, 0f), Quaternion.Euler(0f, m_rot, 0f), Time.deltaTime * 10f).eulerAngles.y;
				m_Rotation = y;
			}
			else if (m_EdgeType == EdgeType.Circle)
			{
				m_CircleRadius = Mathf.Lerp(m_CircleRadius, m_size, Time.deltaTime * 10f);
			}
		}

		public override void OnEnterPlacementState()
		{
			fadeFactorTarget = 1f;
		}

		public override void OnEnterBattleState()
		{
			fadeFactorTarget = 0f;
		}

		public static TeamRequestReslut GetTeamTerritory(Vector3 point, out Quaternion rotation)
		{
			Quaternion rotation2;
			TeamRequestReslut teamTerritoryInternal = Instance.GetTeamTerritoryInternal(point, out rotation2);
			rotation = rotation2;
			return teamTerritoryInternal;
		}

		private TeamRequestReslut GetTeamTerritoryInternal(Vector3 point, out Quaternion rotation)
		{
			if (m_EdgeType == EdgeType.Line)
			{
				Quaternion rotation2;
				TeamRequestReslut teamTerritoryLine = GetTeamTerritoryLine(point, out rotation2);
				rotation = rotation2;
				return teamTerritoryLine;
			}
			if (m_EdgeType == EdgeType.Circle)
			{
				Quaternion rotation3;
				TeamRequestReslut teamTerritoryCircle = GetTeamTerritoryCircle(point, out rotation3);
				rotation = rotation3;
				return teamTerritoryCircle;
			}
			rotation = Quaternion.identity;
			return new TeamRequestReslut(canPlace: false, Team.Red);
		}

		public Vector3[] GetLinePoints()
		{
			Vector3 vector = Quaternion.Euler(new Vector3(0f, m_Rotation, 0f)) * Vector3.forward;
			Vector3 lineCenter = LineCenter;
			linePoints[0] = lineCenter + vector.normalized * 1000f;
			linePoints[1] = lineCenter - vector.normalized * 1000f;
			return linePoints;
		}

		private TeamRequestReslut GetTeamTerritoryLine(Vector3 point, out Quaternion rotation)
		{
			Vector3[] array = GetLinePoints();
			float num = DistanceToLine(array[0], array[1], point);
			Vector3 vector = PerpDirLine(array[0], array[1]);
			if (num < 0f)
			{
				vector = -vector;
			}
			rotation = Quaternion.LookRotation(vector);
			if (num > 0f)
			{
				Team team = Team.Red;
				if (m_TeamIsSwapped)
				{
					team = Team.Blue;
				}
				return new TeamRequestReslut(canPlace: true, team);
			}
			Team team2 = Team.Blue;
			if (m_TeamIsSwapped)
			{
				team2 = Team.Red;
			}
			return new TeamRequestReslut(canPlace: true, team2);
		}

		private Vector3 PerpDirLine(Vector3 point013D, Vector3 point023D)
		{
			Vector2 vector = new Vector2(point013D.x, point013D.z);
			Vector2 vector2 = new Vector2(point023D.x, point023D.z);
			Vector2 vector3 = vector - vector2;
			vector3.Normalize();
			Vector2 vector4 = new Vector2(vector3.y, 0f - vector3.x);
			vector4.Normalize();
			return new Vector3(vector4.x, 0f, vector4.y);
		}

		private float DistanceToLine(Vector3 point013D, Vector3 point023D, Vector3 testPoint)
		{
			Vector2 vector = new Vector2(point013D.x, point013D.z);
			Vector2 vector2 = new Vector2(point023D.x, point023D.z);
			Vector2 vector3 = new Vector2(testPoint.x, testPoint.z);
			Vector2 vector4 = vector - vector2;
			Vector2 vector5 = new Vector2(vector4.y, 0f - vector4.x);
			Vector2 rhs = vector - vector3;
			return Vector2.Dot(vector5.normalized, rhs);
		}

		private Vector3 DirFromCenter(Vector3 pos)
		{
			Vector3 vector = pos - offset;
			vector.y = 0f;
			return vector.normalized;
		}

		private TeamRequestReslut GetTeamTerritoryCircle(Vector3 point, out Quaternion rotation)
		{
			if (DistanceToCircle(point) > m_CircleRadius)
			{
				Team team = Team.Red;
				if (m_TeamIsSwapped)
				{
					team = Team.Blue;
				}
				rotation = Quaternion.LookRotation(-DirFromCenter(point));
				return new TeamRequestReslut(canPlace: true, team);
			}
			Team team2 = Team.Blue;
			if (m_TeamIsSwapped)
			{
				team2 = Team.Red;
			}
			rotation = Quaternion.LookRotation(DirFromCenter(point));
			return new TeamRequestReslut(canPlace: true, team2);
		}

		private float DistanceToCircle(Vector3 testPoint3D)
		{
			return Vector2.Distance(new Vector2(testPoint3D.x, testPoint3D.z), Offset);
		}

		public static PlacementRequestResult CanPlace(UnitBlueprint unitToSpawn, Team team, Vector3 pos)
		{
			return new PlacementRequestResult(canPlace: false, pos, Quaternion.identity);
		}

		public void StartLineEditing()
		{
			if (placementLineCursor == null)
			{
				unitBrush = ServiceLocator.GetService<GameModeService>().CurrentGameMode.Brush;
				placementLineCursor = UnityEngine.Object.FindObjectOfType<PlacementLineCursor>();
			}
			placementLineCursor.Enable();
			placementLineCursor.transform.position = LineCenter;
			m_rot = m_Rotation;
			m_pos = offset;
			m_size = m_CircleRadius;
			isEditingLine = true;
		}

		public void StopLineEditing()
		{
			placementLineCursor.Disable();
			isEditingLine = false;
			mapSettingsComponent.StopEditing();
		}

		public bool SetType(EdgeType type)
		{
			if (type != m_EdgeType)
			{
				m_EdgeType = type;
				return true;
			}
			m_EdgeType = type;
			return false;
		}

		public void SwapTeamEdge()
		{
			m_TeamIsSwapped = !m_TeamIsSwapped;
		}

		public static Color GetBackgroundColor()
		{
			return Instance.UIColorOverwrite.BlurBGColor;
		}

		public static Color GetTeamBackgroundColor(Team team)
		{
			if (team == Team.Red)
			{
				return Instance.UIColorOverwrite.RedUIBG;
			}
			return Instance.UIColorOverwrite.BlueUIBG;
		}
	}
}
