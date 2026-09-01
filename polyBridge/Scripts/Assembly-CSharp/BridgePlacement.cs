public class BridgePlacement
{
	private static readonly float PLACEMENT_WARNING_DISPLAY_TIME_SECONDS = 2f;

	public static void DisplayPlacementFailureMessage(PlacementReturnValue placementReturnValue)
	{
		string empty = string.Empty;
		empty = placementReturnValue switch
		{
			PlacementReturnValue.FAIL_NO_MATERIAL_LEFT => string.Format(Localize.Get("UI_PLACEMENT_NO_MATERIAL_LEFT"), BridgeMaterials.GetLocalizedMaterialDisplayName(Bridge.m_BuildMaterialType)), 
			PlacementReturnValue.FAIL_CANNOT_AFFORD_COST => Localize.Get("UI_PLACEMENT_CANNOT_AFFORD_COST"), 
			PlacementReturnValue.FAIL_OUTSIDE_WORLD_BOUNDS => Localize.Get("UI_PLACEMENT_OUTSIDE_WORLD_BOUNDS"), 
			PlacementReturnValue.FAIL_NODE_ILLEGAL_POSITION => Localize.Get("UI_PLACEMENT_NODE_ILLEGAL_POSITION"), 
			PlacementReturnValue.FAIL_EDGE_OVERLAPS_BLOCKING_SHAPE => Localize.Get("UI_PLACEMENT_EDGE_OVERLAPS_BLOCKING"), 
			PlacementReturnValue.FAIL_OUTSIDE_BUILD_ZONE => Localize.Get("UI_PLACEMENT_OUTSIDE_BUILD_ZONE"), 
			PlacementReturnValue.FAIL_NO_BUILD_ANCHOR => Localize.Get("UI_PLACEMENT_NO_BUILD_ANCHOR"), 
			PlacementReturnValue.FAIL_EXCEEDS_MAX_EDGE_LIMIT_PER_NODE => string.Format(Localize.Get("UI_MAX_EDGE_CONNECTIONS"), BridgeJoints.MAX_EDGES_PER_JOINT), 
			PlacementReturnValue.FAIL_PILLAR_OVERLAPS_BLOCKING_SHAPE => Localize.Get("UI_PLACEMENT_PILLAR_OVERLAPS_BLOCKING"), 
			PlacementReturnValue.FAIL_PILLAR_EXCEEDS_MAX_HEIGHT => Localize.Get("UI_PLACEMENT_PILLAR_EXCEEDS_MAX_HEIGHT"), 
			PlacementReturnValue.FAIL_PILLAR_NOT_BETWEEN_ISLANDS => Localize.Get("UI_PLACEMENT_PILLAR_NOT_BETWEEN_ISLANDS"), 
			PlacementReturnValue.FAIL_PILLAR_ANCHOR_ILLEGAL_LOCATION => Localize.Get("UI_PLACEMENT_PILLAR_ANCHOR_ILLEGAL_POSITION"), 
			_ => string.Empty, 
		};
		if (!string.IsNullOrEmpty(empty))
		{
			GameUI.ShowMessage(ScreenMessageLocation.TOP_CENTER, empty, PLACEMENT_WARNING_DISPLAY_TIME_SECONDS);
		}
	}

	public static void PlayFailPlacement(PlacementReturnValue placementReturnValue)
	{
		if (placementReturnValue == PlacementReturnValue.FAIL_CANNOT_AFFORD_COST || placementReturnValue == PlacementReturnValue.FAIL_NO_MATERIAL_LEFT)
		{
			InterfaceAudio.Play("ui_overBudget");
		}
		else
		{
			InterfaceAudio.Play("ui_build_notHere");
		}
	}
}
