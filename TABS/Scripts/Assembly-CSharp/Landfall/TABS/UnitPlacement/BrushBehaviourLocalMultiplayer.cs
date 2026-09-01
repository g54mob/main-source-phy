using Landfall.TABS.GameMode;
using Landfall.TABS.Workshop;
using TFBGames;
using UnityEngine;

namespace Landfall.TABS.UnitPlacement
{
	public class BrushBehaviourLocalMultiplayer : BrushBehaviourBase
	{
		private InputService inputService;

		private LocalMultiplayerGameMode gameMode;

		public BrushBehaviourLocalMultiplayer()
		{
			inputService = ServiceLocator.GetService<InputService>();
			gameMode = (LocalMultiplayerGameMode)ServiceLocator.GetService<GameModeService>().CurrentGameMode;
		}

		public override void PrimaryActionOnObject(GameObject go, Vector3 worldPosition)
		{
			if (!(go == null))
			{
				Quaternion rotation;
				Team teamAreaAtPosition = GetTeamAreaAtPosition(worldPosition, out rotation);
				if (IsCorrectTeamAndPlayer(teamAreaAtPosition) && gameMode.CanPlaceUnits(teamAreaAtPosition))
				{
					base.PrimaryActionOnObject(go, worldPosition);
				}
			}
		}

		public override void SecondaryActionOnObject(GameObject go, Vector3 worldPosition)
		{
			Quaternion rotation;
			Team teamAreaAtPosition = GetTeamAreaAtPosition(worldPosition, out rotation);
			if (IsCorrectTeamAndPlayer(teamAreaAtPosition))
			{
				base.SecondaryActionOnObject(go, worldPosition);
			}
		}

		public override void HoveringStaticSceneObjects(GameObject go, Vector3 worldPosition, Vector3 surfaceNormal)
		{
			Quaternion rotation;
			Team teamAreaAtPosition = GetTeamAreaAtPosition(worldPosition, out rotation);
			bool num = inputService.CurrentPlayer == TFBGames.Player.One && teamAreaAtPosition == Team.Blue;
			bool flag = inputService.CurrentPlayer == TFBGames.Player.Two && teamAreaAtPosition == Team.Red;
			if (num || flag)
			{
				base.UnitPlacementBrush.Cursor.SetCursorState(PlacementCursorState.DenyPlacement);
			}
			else
			{
				base.HoveringStaticSceneObjects(go, worldPosition, surfaceNormal);
			}
		}

		public override bool CanRemove(Unit unit)
		{
			return IsCorrectTeamAndPlayer(unit.Team);
		}

		public override void PlaceUnit(UnitBlueprint blueprint, Team team, Vector3 worldPosition, Quaternion rotation, bool isCampaignUnit, bool forMartianPlayer = false, int martianInstanceId = 0)
		{
			m_unitPlacementBrush.PlaceUnitInternal(blueprint, team, worldPosition, rotation, addToLayout: true, null, costsBudget: true, campaignUnit: false, !isCampaignUnit);
		}

		public override void PlaceLayoutUnit(TABSCampaignLevelAsset.TABSLayoutUnit unit, Team team, Quaternion unitRotation)
		{
			m_unitPlacementBrush.PlaceLayoutUnitInternal(unit, team, addToLayout: true, unitRotation);
		}

		public override void RemoveUnit(Unit unit, bool forMartian = false)
		{
			m_unitPlacementBrush.RemoveUnitInternal(unit, unit.Team);
		}

		private bool IsCorrectTeamAndPlayer(Team team)
		{
			bool num = inputService.CurrentPlayer == TFBGames.Player.One && team == Team.Red;
			bool flag = inputService.CurrentPlayer == TFBGames.Player.Two && team == Team.Blue;
			return num || flag;
		}
	}
}
