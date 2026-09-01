using DarkTonic.MasterAudio;
using UnityEngine;

public class BridgeAudio
{
	public static void PlayMaterialSelect(BridgeMaterialType materialType)
	{
		switch (materialType)
		{
		case BridgeMaterialType.ROAD:
			MasterAudio.PlaySoundAndForget("ui_build_road_select");
			break;
		case BridgeMaterialType.REINFORCED_ROAD:
			MasterAudio.PlaySoundAndForget("ui_build_road_reinforced_select");
			break;
		case BridgeMaterialType.WOOD:
			MasterAudio.PlaySoundAndForget("ui_build_wood_select");
			break;
		case BridgeMaterialType.STEEL:
			MasterAudio.PlaySoundAndForget("ui_build_steel_select");
			break;
		case BridgeMaterialType.HYDRAULICS:
			MasterAudio.PlaySoundAndForget("ui_build_hydraulic_select");
			break;
		case BridgeMaterialType.ROPE:
			MasterAudio.PlaySoundAndForget("ui_build_rope_select");
			break;
		case BridgeMaterialType.CABLE:
			MasterAudio.PlaySoundAndForget("ui_build_cable_select");
			break;
		case BridgeMaterialType.SPRING:
			MasterAudio.PlaySoundAndForget("ui_build_spring_select");
			break;
		case BridgeMaterialType.PILLAR:
			MasterAudio.PlaySoundAndForget("ui_build_road_select");
			break;
		default:
			Debug.LogWarningFormat("No audio for when selecting material {0}", materialType.ToString());
			break;
		}
	}

	public static void PlayCreateEdge(BridgeMaterialType materialType)
	{
		switch (materialType)
		{
		case BridgeMaterialType.ROAD:
			MasterAudio.PlaySoundAndForget("ui_build_road_place");
			break;
		case BridgeMaterialType.REINFORCED_ROAD:
			MasterAudio.PlaySoundAndForget("ui_build_road_reinforced_place");
			break;
		case BridgeMaterialType.WOOD:
			MasterAudio.PlaySoundAndForget("ui_build_wood_place");
			break;
		case BridgeMaterialType.STEEL:
			MasterAudio.PlaySoundAndForget("ui_build_steel_place");
			break;
		case BridgeMaterialType.HYDRAULICS:
			MasterAudio.PlaySoundAndForget("ui_build_hydraulic_place");
			break;
		case BridgeMaterialType.ROPE:
			MasterAudio.PlaySoundAndForget("ui_build_rope_place");
			break;
		case BridgeMaterialType.CABLE:
			MasterAudio.PlaySoundAndForget("ui_build_cable_place");
			break;
		case BridgeMaterialType.SPRING:
			MasterAudio.PlaySoundAndForget("ui_build_spring_place");
			break;
		case BridgeMaterialType.PILLAR:
			MasterAudio.PlaySoundAndForget("ui_build_road_place");
			break;
		default:
			Debug.LogWarningFormat("No audio for when placing material {0}", materialType.ToString());
			break;
		}
	}

	public static void PlayBreakEdge(BridgeMaterialType materialType, Vector3 pos)
	{
		switch (materialType)
		{
		case BridgeMaterialType.ROAD:
		case BridgeMaterialType.REINFORCED_ROAD:
			SimAudio.Play("sfx_bridgeFail_road", pos);
			break;
		case BridgeMaterialType.WOOD:
			SimAudio.Play("sfx_bridgeFail_wood", pos);
			break;
		case BridgeMaterialType.STEEL:
			SimAudio.Play("sfx_bridgeFail_steel", pos);
			break;
		case BridgeMaterialType.HYDRAULICS:
			SimAudio.Play("sfx_bridgeFail_hydraulic", pos);
			break;
		case BridgeMaterialType.ROPE:
			SimAudio.Play("sfx_bridgeFail_rope", pos);
			break;
		case BridgeMaterialType.CABLE:
			SimAudio.Play("sfx_bridgeFail_cable", pos);
			break;
		case BridgeMaterialType.SPRING:
			SimAudio.Play("sfx_bridgeFail_spring", pos);
			break;
		default:
			Debug.LogWarningFormat("No audio for when {0} breaks", materialType.ToString());
			break;
		}
	}
}
