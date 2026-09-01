using Landfall.TABS.AI.Systems;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS.WinConditions;
using Landfall.TABS.Workshop;
using TFBGames;
using Unity.Entities;
using UnityEngine;

namespace Landfall.TABS.UnitPlacement
{
	public abstract class BrushBehaviourBase
	{
		protected SettingsInstance m_flipColorsSetting;

		protected UnitPlacementBrush m_unitPlacementBrush;

		private GameObject m_collisionCheckGameObject;

		private BoxCollider m_collisionCheckCollider;

		private Collider[] canPlaceColliders = new Collider[2];

		private SoundPlayer soundPlayer;

		private readonly AudioPathData uiUnitPlaced = new AudioPathData("UI", "Unit Placed");

		private readonly AudioPathData uiUnitRemoved = new AudioPathData("UI", "Unit Removed");

		public UnitPlacementBrush UnitPlacementBrush => m_unitPlacementBrush;

		public BrushBehaviourBase()
		{
			m_flipColorsSetting = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_FLIP_COLORS");
		}

		public virtual void Initialize()
		{
			soundPlayer = ServiceLocator.GetService<SoundPlayer>();
			m_collisionCheckGameObject = GameObject.Find("CursorCollisionCheck");
			if (m_collisionCheckGameObject != null)
			{
				m_collisionCheckCollider = m_collisionCheckGameObject.GetComponent<BoxCollider>();
			}
			else
			{
				CreateCollisionCheckGameObject();
			}
		}

		public void OnDestroy()
		{
			if (m_collisionCheckGameObject != null)
			{
				Object.Destroy(m_collisionCheckGameObject);
			}
		}

		public void SetPlacementBrush(UnitPlacementBrush placementBrush)
		{
			m_unitPlacementBrush = placementBrush;
		}

		public virtual bool CanPlace(UnitBlueprint unitToSpawn, Team team, ref Vector3 position, Quaternion rotation, bool forMartianPlayer = false)
		{
			if (m_collisionCheckGameObject == null)
			{
				CreateCollisionCheckGameObject();
			}
			Debug.DrawLine(position, position + Vector3.up * 2f);
			if (unitToSpawn == null)
			{
				return false;
			}
			bool flag = ServiceLocator.GetService<GameModeService>().IsGameModeRestricted();
			if (Bugs._DLC_ACTIVATED && !flag && ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("BUG_MAMMOTH").currentValue == 1 && unitToSpawn.Entity.GUID.m_ID == 1380042766)
			{
				return true;
			}
			Unit component = unitToSpawn.UnitBase.GetComponent<Unit>();
			m_collisionCheckCollider.size = component.PlacementSquare.Size;
			m_collisionCheckCollider.transform.SetPositionAndRotation(position + new Vector3(0f, component.PlacementSquare.Size.y * 0.5f, 0f), rotation);
			if (Physics.OverlapBoxNonAlloc(position, component.PlacementSquare.Size * 0.5f, canPlaceColliders, rotation, m_unitPlacementBrush.Cursor.UnitMask) > 1)
			{
				return false;
			}
			return true;
		}

		public abstract bool CanRemove(Unit unit);

		public virtual Unit Place(UnitBlueprint unit, Team team, Vector3 pos, Quaternion rotation, bool addToLayout, bool isCampaignUnit, RuntimeReference runtimeReference, bool forMartianPlayer = false, bool spawnRiders = true, bool isMartian = false, int martianInstanceId = 0)
		{
			Unit unitToSpawn = null;
			unit.Spawn(pos, rotation, team, out unitToSpawn, 1f, isCampaignUnit, spawnRiders, martianInstanceId);
			if (runtimeReference == null)
			{
				unitToSpawn.RuntimeReference = ServiceLocator.GetService<RuntimeReferenceService>().CreateReference(unitToSpawn);
			}
			else
			{
				unitToSpawn.RuntimeReference = ServiceLocator.GetService<RuntimeReferenceService>().CreateReferenceFromCache(runtimeReference, unitToSpawn);
			}
			PlaceUnitBox(unitToSpawn);
			if (addToLayout)
			{
				UnitPlacementBrush.GameModeService.CurrentGameMode.TeamLayouts.AddUnitToLayout(team, unit, unitToSpawn, pos, isCampaignUnit);
			}
			unitToSpawn.FreezeBody();
			soundPlayer.PlaySoundEffectNonAlloc(uiUnitPlaced, 1f, Vector3.zero);
			return unitToSpawn;
		}

		private void PlaceUnitBox(Unit unit)
		{
			BoxCollider boxCollider = new GameObject("UnitBox").AddComponent<BoxCollider>();
			Vector3 offset = unit.PlacementSquare.Offset;
			boxCollider.transform.parent = unit.transform;
			boxCollider.transform.localPosition = offset;
			boxCollider.transform.localRotation = Quaternion.identity;
			boxCollider.transform.localScale = Vector3.one;
			boxCollider.size = unit.PlacementSquare.Size;
			boxCollider.gameObject.layer = 23;
			boxCollider.gameObject.AddComponent<DestroyOnBattle>();
			BoxCollider boxCollider2 = new GameObject("RaycastCube").AddComponent<BoxCollider>();
			boxCollider2.transform.parent = unit.transform;
			boxCollider2.transform.localPosition = offset;
			boxCollider2.transform.localRotation = Quaternion.identity;
			boxCollider2.transform.localScale = Vector3.one;
			boxCollider2.size = unit.PlacementSquare.Size;
			boxCollider2.gameObject.layer = 24;
			boxCollider2.gameObject.AddComponent<DestroyOnBattle>();
		}

		public virtual void Remove(Unit unit, Team team)
		{
			if (unit.RuntimeReference != null)
			{
				UnitPlacementBrush.GameModeService.CurrentGameMode.TeamLayouts.RemoveUnitFromLayout(team, unit.RuntimeReference);
			}
			World.Active.GetExistingManager<TeamSystem>().RemoveEntity(unit.GetComponent<GameObjectEntity>().Entity, team, unit);
			soundPlayer.PlaySoundEffectNonAlloc(uiUnitRemoved, 1f, Vector3.zero);
			GameObject[] spawnedObjects = unit.spawnedObjects;
			for (int num = spawnedObjects.Length - 1; num >= 0; num--)
			{
				Object.Destroy(spawnedObjects[num]);
			}
		}

		public virtual Team GetTeamAreaAtPosition(Vector3 position, out Quaternion rotation)
		{
			Quaternion rotation2;
			SceneSettings.TeamRequestReslut teamTerritory = SceneSettings.GetTeamTerritory(position, out rotation2);
			rotation = rotation2;
			return teamTerritory.Team;
		}

		public virtual void BrushPositionDelta(Vector3 oldPos, Vector3 newPos, Vector3 delta)
		{
			GetTeamAreaAtPosition(newPos, out var _);
			m_unitPlacementBrush.Cursor.transform.position = newPos;
		}

		public virtual void HoveringStaticSceneObjects(GameObject go, Vector3 worldPosition, Vector3 surfaceNormal)
		{
			float num = Vector3.Angle(surfaceNormal, Vector3.up);
			if (UnitPlacementBrush.Cursor.InvalidUnitPlacementMask.Contains(go.layer) || num > m_unitPlacementBrush.Cursor.MaxPlacementAngleEuler)
			{
				UnitPlacementBrush.Cursor.SetCursorState(PlacementCursorState.DenyPlacement);
				return;
			}
			Quaternion rotation;
			Team teamAreaAtPosition = GetTeamAreaAtPosition(worldPosition, out rotation);
			PlacementCursorState? placementCursorState = null;
			switch (teamAreaAtPosition)
			{
			case Team.Red:
				placementCursorState = PlacementCursorState.AllowRedPlacement;
				break;
			case Team.Blue:
				placementCursorState = PlacementCursorState.AllowBluePlacement;
				break;
			}
			if (!CanPlace(UnitPlacementBrush.UnitToSpawn, teamAreaAtPosition, ref worldPosition, rotation))
			{
				placementCursorState = PlacementCursorState.DenyPlacement;
			}
			if (placementCursorState.HasValue)
			{
				UnitPlacementBrush.Cursor.SetCursorState(placementCursorState.Value);
			}
		}

		public virtual void StartHoverDynamicObjects(GameObject go, Vector3 worldPosition)
		{
			m_unitPlacementBrush.Cursor.OnStartHoverSceneGameObject(go, worldPosition);
		}

		public virtual void HoveringDynamicObjects(GameObject go, Vector3 worldPosition)
		{
			m_unitPlacementBrush.Cursor.OnHoverSceneGameObject(go);
		}

		public virtual void EndHoverDynamicObject(GameObject go, Vector3 worldPosition)
		{
			m_unitPlacementBrush.Cursor.OnEndHoverSceneGameObject(go);
		}

		public virtual void PrimaryActionOnObject(GameObject go, Vector3 worldPosition)
		{
			if (!(go == null) && ServiceLocator.GetService<GameStateManager>().GameState != Landfall.TABS.GameState.GameState.BattleState)
			{
				Quaternion rotation;
				Team teamAreaAtPosition = GetTeamAreaAtPosition(worldPosition, out rotation);
				if (CanPlace(UnitPlacementBrush.UnitToSpawn, teamAreaAtPosition, ref worldPosition, rotation))
				{
					Vector3 offset = UnitPlacementBrush.UnitToSpawn.UnitBase.GetComponent<Unit>().PlacementSquare.Offset;
					offset.y = 0f;
					Vector3 vector = rotation * offset;
					PlaceUnit(UnitPlacementBrush.UnitToSpawn, teamAreaAtPosition, worldPosition - vector, rotation, isCampaignUnit: false);
				}
			}
		}

		public virtual void SecondaryActionOnObject(GameObject go, Vector3 worldPosition)
		{
			Unit component = go.transform.root.GetComponent<Unit>();
			if (component != null)
			{
				RemoveUnit(component);
			}
		}

		public abstract void PlaceUnit(UnitBlueprint blueprint, Team team, Vector3 worldPosition, Quaternion rotation, bool isCampaignUnit, bool forMartianPlayer = false, int martianInstanceId = 0);

		public abstract void RemoveUnit(Unit unit, bool forMartian = false);

		public abstract void PlaceLayoutUnit(TABSCampaignLevelAsset.TABSLayoutUnit unit, Team team, Quaternion unitRotation);

		private void CreateCollisionCheckGameObject()
		{
			m_collisionCheckGameObject = new GameObject("CursorCollisionCheck");
			m_collisionCheckCollider = m_collisionCheckGameObject.AddComponent<BoxCollider>();
			m_collisionCheckGameObject.layer = 23;
		}
	}
}
