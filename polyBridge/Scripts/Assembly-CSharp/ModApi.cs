using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using I2.Loc;
using MoonSharp.Interpreter;
using Poly.Base;
using Poly.Game;
using Poly.Physics;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ModApi
{
	protected class DelayedLuaCall
	{
		public string m_LuaString;

		public float m_RealtimeToTrigger;

		public DelayedLuaCall(string luaString, float realtimeToTrigger)
		{
			m_LuaString = luaString;
			m_RealtimeToTrigger = realtimeToTrigger;
		}
	}

	protected static Script m_Api = new Script();

	protected static string m_CurrModDirPath;

	protected static string m_CurrModId;

	protected static bool m_HasLoadedCatalog;

	protected static int m_NumModsCurrentlyLoadingAddressables;

	protected static Dictionary<string, Texture2D> m_CachedTextures = new Dictionary<string, Texture2D>();

	protected static bool m_DidCameraTransformChange;

	protected static bool m_DidCameraOrthographicChange;

	protected static bool m_DidCameraOrthographicSizeChange;

	protected static bool m_DidCameraFOVChange;

	protected static string[] m_CheatFunctionNames = new string[21]
	{
		"SetMaterialStrength", "SetMaterialCost", "SetVehiclePosition", "SetVehiclePhysicsVelocity", "SetVehicleSpeed", "SetVehicleAcceleration", "SetVehicleHorsepower", "SetVehicleTargetSpeed", "SetVehicleMass", "SetVehicleFlippedDirection",
		"SetVehicleBrakingForce", "SetVehicleIdleDownhill", "SetVehicleRotation", "BreakEdge", "SetJointPosition", "SetMaterialMaxLength", "EndLevelSuccess", "IgnoreEdgePlacementRestrictions", "PlaceEdgeJointToJointCheat", "PlaceEdgeJointToPosCheat",
		"PlaceEdgePosToPosCheat"
	};

	protected static string[] m_LanguageFunctionNames = new string[1] { "AddLanguageCSV" };

	protected static string[] m_VehicleUGCFunctionNames = new string[1] { "AddAssetVehicle" };

	protected static string[] m_ZVehicleUGCFunctionNames = new string[2] { "AddAssetBoat", "AddAssetPlane" };

	protected static string[] m_CustomShapeUGCFunctionNames = new string[2] { "AddCustomShape", "AddCustomShapeTexture" };

	protected static string[] m_DecorUGCFunctionNames = new string[1] { "AddAssetDecor" };

	protected static string[] m_WorkshopCampaignFunctionNames = new string[1] { "WorkshopCampaignCreate" };

	private static bool m_IsConsoleActivationAllowed;

	private static Dictionary<string, string> m_LanguageCSVMapping = new Dictionary<string, string>();

	private static readonly string LANGUAGE_CSVS_FILENAME = ".languagecsvs";

	private static string TINT_COLOR_ID = "Color_f5c2ee45336c47cb9866222c4ffe7d87";

	private static ModFile_Materials m_Defaults;

	private static bool m_IsAnyMaterialTinted;

	protected static HashSet<string> m_PrefabAddressesInUse = new HashSet<string>();

	protected static List<Sprite> m_CreatedSprites = new List<Sprite>();

	protected static List<string> m_InGamePrefabNames = new List<string>
	{
		"Joint", "Platform", "Ramp", "Rock_Prefab", "Rock_Prefab2", "Rock_Prefab3", "Rock_Prefab4", "AIR_BalloonBox", "AIR_BalloonRound", "AIR_BalloonTri",
		"BrickPillar_Prefab", "BuildZoneRect_Prefab", "Buildzone_Tri2"
	};

	protected static List<DelayedLuaCall> m_DelayedLuaCalls = new List<DelayedLuaCall>();

	protected static Dictionary<string, int> m_IntDict = new Dictionary<string, int>();

	protected static Dictionary<string, float> m_FloatDict = new Dictionary<string, float>();

	protected static Dictionary<string, string> m_StringDict = new Dictionary<string, string>();

	protected static Dictionary<string, List<int>> m_IntListDict = new Dictionary<string, List<int>>();

	protected static Dictionary<string, List<float>> m_FloatListDict = new Dictionary<string, List<float>>();

	protected static Dictionary<string, List<string>> m_StringListDict = new Dictionary<string, List<string>>();

	private static Dictionary<string, string> m_OnUpdateDict = new Dictionary<string, string>();

	private static Dictionary<string, string> m_OnFixedUpdateDict = new Dictionary<string, string>();

	private static Dictionary<string, DynValue> m_OnUpdateFuncDict = new Dictionary<string, DynValue>();

	private static Dictionary<string, DynValue> m_OnFixedUpdateFuncDict = new Dictionary<string, DynValue>();

	private static List<string> m_ErrorMessageQueueList = new List<string>();

	private static List<string> m_ErrorMessageShownList = new List<string>();

	public static void Init()
	{
		RegisterAllFunctions();
	}

	public static void ResetAllToDefault()
	{
		ResetMaterialsToDefault();
		ResetLanguageToDefault();
		ResetSandboxToDefault();
		ResetThemeToDefault();
		ResetScreenUIToDefault();
		ResetUnityUtilsToDefault();
		ResetGameStateToDefault();
		ResetUpdateLoopsToDefault();
		ResetVehiclesToDefault();
		ResetBridgeToDefault();
		ResetDevToDefault();
		ResetUIToDefault();
		if (GameStateManager.GetState() != GameState.MAIN_MENU)
		{
			ResetCameraToDefault();
		}
		ResetWorkshopCampaignToDefault();
	}

	public static DynValue RunScript(string modDirPath, string luaScript)
	{
		m_CurrModDirPath = modDirPath;
		m_CurrModId = Path.GetFileName(modDirPath);
		m_HasLoadedCatalog = false;
		return m_Api.DoString(luaScript);
	}

	public static void RunFunction(string modDirPath, DynValue func)
	{
		m_CurrModDirPath = modDirPath;
		m_CurrModId = Path.GetFileName(modDirPath);
		m_HasLoadedCatalog = false;
		m_Api.Call(func);
	}

	public static void RunCallback(string callback)
	{
		try
		{
			m_Api.Call(m_Api.Globals[callback]);
		}
		catch
		{
			AddErrorMessageToQueue(Localize.Get("UI_MODS_ERROR_CALLBACK", callback));
		}
	}

	public static int GetNumModsLoadingAddressables()
	{
		return m_NumModsCurrentlyLoadingAddressables;
	}

	protected static List<float> Vec3ToFloatList(Vector3 vector)
	{
		List<float> list = new List<float> { 0f, 0f, 0f };
		list[0] = vector.x;
		list[1] = vector.y;
		list[2] = vector.z;
		return list;
	}

	protected static Vector3 FloatListToVec3(List<float> floatList)
	{
		Vector3 zero = Vector3.zero;
		if (floatList == null)
		{
			return zero;
		}
		zero.x = ((floatList.Count > 0) ? floatList[0] : 0f);
		zero.y = ((floatList.Count > 1) ? floatList[1] : 0f);
		zero.z = ((floatList.Count > 2) ? floatList[2] : 0f);
		return zero;
	}

	protected static Sprite GetSpriteFromPath(string path)
	{
		string fullPath = Path.GetFullPath(Path.Combine(m_CurrModDirPath, path));
		string value = Path.Combine(m_CurrModDirPath, "");
		if (!fullPath.StartsWith(value))
		{
			return null;
		}
		Texture2D textureForPath = GetTextureForPath(fullPath);
		return Sprite.Create(textureForPath, new Rect(0f, 0f, textureForPath.width, textureForPath.height), new Vector2(0.5f, 0.5f));
	}

	protected static Texture2D GetTextureForPath(string fullPath)
	{
		if (m_CachedTextures.ContainsKey(fullPath))
		{
			return m_CachedTextures[fullPath];
		}
		byte[] array = Utils.ReadAllBytes(fullPath);
		if (array == null)
		{
			array = Utils.ReadAllBytes(ForceExtensionToLowercase(fullPath));
		}
		Texture2D texture2D = new Texture2D(2, 2);
		if (array == null)
		{
			return texture2D;
		}
		if (!texture2D.LoadImage(array))
		{
			return texture2D;
		}
		if (fullPath.StartsWith(Mods.GetLocalTestModsDirectoryPath()))
		{
			m_CachedTextures.Add(fullPath, texture2D);
		}
		return texture2D;
	}

	private static string ForceExtensionToLowercase(string filename)
	{
		if (string.IsNullOrWhiteSpace(filename))
		{
			return filename;
		}
		object obj = Path.GetDirectoryName(filename);
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filename);
		string text = Path.GetExtension(filename)?.ToLowerInvariant();
		if (obj == null)
		{
			obj = "";
		}
		return Path.Combine((string)obj, fileNameWithoutExtension + text);
	}

	protected static bool InSimModeAndSimulating()
	{
		if (GameStateManager.GetState() == GameState.SIM)
		{
			return Bridge.IsSimulating();
		}
		return false;
	}

	protected static BridgeMaterialType GetBridgeMaterialType(string materialName)
	{
		materialName = FixCommonMaterialSpellingMistakes(materialName);
		BridgeMaterialType result = BridgeMaterialType.INVALID;
		if (Enum.TryParse<BridgeMaterialType>(materialName, ignoreCase: true, out result))
		{
			return result;
		}
		Debug.LogError("Mod API Error: Could not find material " + materialName);
		return BridgeMaterialType.INVALID;
	}

	protected static string FixCommonMaterialSpellingMistakes(string materialName)
	{
		if (materialName.ToUpper() == "HYDRAULIC")
		{
			return "HYDRAULICS";
		}
		if (materialName.ToUpper() == "REINFORCED ROAD")
		{
			return "REINFORCED_ROAD";
		}
		if (materialName.ToUpper() == "REINFORCEDROAD")
		{
			return "REINFORCED_ROAD";
		}
		return materialName;
	}

	private static void RegisterAllFunctions()
	{
		RegisterMaterialFunctions();
		RegisterLanguageFunctions();
		RegisterSandboxFunctions();
		RegisterThemeFunctions();
		RegisterScreenUIFunctions();
		RegisterUnityUtilsFunctions();
		RegisterGameStateFunctions();
		RegisterVehiclesFunctions();
		RegisterDevFunctions();
		RegisterUIFunctions();
		RegisterBridgeFunctions();
		RegisterCameraFunctions();
		RegisterWorkshopCampaignFunctions();
	}

	protected static void RegisterBridgeFunctions()
	{
		m_Api.Globals["GetFoundationIds"] = new Func<List<string>>(GetFoundationIds);
		m_Api.Globals["GetFoundationIdsSelected"] = new Func<List<string>>(GetFoundationIdsSelected);
		m_Api.Globals["GetBridgeEdgeIds"] = new Func<List<string>>(GetBridgeEdgeIds);
		m_Api.Globals["GetBridgeEdgeIdsSelected"] = new Func<List<string>>(GetBridgeEdgeIdsSelected);
		m_Api.Globals["GetBridgeEdgePosition"] = new Func<string, List<float>>(GetBridgeEdgePosition);
		m_Api.Globals["GetBridgeEdgeStressNormalized"] = new Func<string, float>(GetBridgeEdgeStressNormalized);
		m_Api.Globals["GetBridgeEdgeStressSigned"] = new Func<string, float>(GetBridgeEdgeStressSigned);
		m_Api.Globals["GetBridgeEdgeMaterial"] = new Func<string, string>(GetBridgeEdgeMaterial);
		m_Api.Globals["GetBridgeEdgeLength"] = new Func<string, float>(GetBridgeEdgeLength);
		m_Api.Globals["GetBridgeEdgeAngle"] = new Func<string, float>(GetBridgeEdgeAngle);
		m_Api.Globals["GetBridgeEdgeJoints"] = new Func<string, List<string>>(GetBridgeEdgeJoints);
		m_Api.Globals["GetBridgeEdgeIsBroken"] = new Func<string, bool>(GetBridgeEdgeIsBroken);
		m_Api.Globals["GetOverrideEdgeColor"] = new Func<string, string>(GetOverrideEdgeColor);
		m_Api.Globals["SetOverrideEdgeColor"] = new Action<string, string>(SetOverrideEdgeColor);
		m_Api.Globals["GetPermanentEdgeColor"] = new Func<string, string>(GetPermanentEdgeColor);
		m_Api.Globals["SetPermanentEdgeColor"] = new Action<string, string>(SetPermanentEdgeColor);
		m_Api.Globals["GetBridgeEdgeIsPrebuilt"] = new Func<string, bool>(GetBridgeEdgeIsPrebuilt);
		m_Api.Globals["BreakEdge"] = new Action<string>(BreakEdge);
		m_Api.Globals["PlaceEdgeJointToJoint"] = new Func<string, string, string, string>(PlaceEdgeJointToJoint);
		m_Api.Globals["PlaceEdgeJointToPos"] = new Func<string, string, List<float>, string>(PlaceEdgeJointToPos);
		m_Api.Globals["PlaceEdgePosToPos"] = new Func<string, List<float>, List<float>, string>(PlaceEdgePosToPos);
		m_Api.Globals["IgnoreEdgePlacementRestrictions"] = new Action<bool>(IgnoreEdgePlacementRestrictions);
		m_Api.Globals["PlaceEdgeJointToJointCheat"] = new Func<string, string, string, string>(PlaceEdgeJointToJointCheat);
		m_Api.Globals["PlaceEdgeJointToPosCheat"] = new Func<string, string, List<float>, string>(PlaceEdgeJointToPosCheat);
		m_Api.Globals["PlaceEdgePosToPosCheat"] = new Func<string, List<float>, List<float>, string>(PlaceEdgePosToPosCheat);
		m_Api.Globals["GetJointIds"] = new Func<List<string>>(GetJointIds);
		m_Api.Globals["GetJointIdsSelected"] = new Func<List<string>>(GetJointIdsSelected);
		m_Api.Globals["GetJointEdges"] = new Func<string, List<string>>(GetJointEdges);
		m_Api.Globals["GetJointPosition"] = new Func<string, List<float>>(GetJointPosition);
		m_Api.Globals["GetJointIsSplit"] = new Func<string, bool>(GetJointIsSplit);
		m_Api.Globals["SetJointIsSplit"] = new Action<string, bool>(SetJointIsSplit);
		m_Api.Globals["GetJointIsAnchor"] = new Func<string, bool>(GetJointIsAnchor);
		m_Api.Globals["SetJointPosition"] = new Action<string, List<float>>(SetJointPosition);
		m_Api.Globals["SetJointLegalPosition"] = new Func<string, List<float>, bool>(SetJointLegalPosition);
		m_Api.Globals["GetJointIsPrebuilt"] = new Func<string, bool>(GetJointIsPrebuilt);
		m_Api.Globals["GetJointIsUnbuildable"] = new Func<string, bool>(GetJointIsUnbuildable);
	}

	protected static void ResetBridgeToDefault()
	{
		foreach (KeyValuePair<string, BridgeEdge> item in BridgeEdges.GetEdgeDictionary())
		{
			if (item.Value.gameObject.activeInHierarchy)
			{
				item.Value.m_HasOverrideColor = false;
			}
		}
		BridgeJointPlacement.m_IgnoreEdgePlacementRestrictions = false;
	}

	protected static List<string> GetFoundationIds()
	{
		List<string> list = new List<string>();
		foreach (BridgePillar bridgePillar in BridgePillars.m_BridgePillars)
		{
			if (bridgePillar.gameObject.activeInHierarchy)
			{
				list.Add(bridgePillar.m_AnchorGuid);
			}
		}
		return list;
	}

	protected static List<string> GetFoundationIdsSelected()
	{
		List<string> list = new List<string>();
		foreach (BridgePillar bridgePillar in BridgeSelectionSet.m_BridgePillars)
		{
			if (bridgePillar.gameObject.activeInHierarchy)
			{
				list.Add(bridgePillar.m_AnchorGuid);
			}
		}
		return list;
	}

	protected static List<string> GetBridgeEdgeIds()
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, BridgeEdge> item in BridgeEdges.GetEdgeDictionary())
		{
			if (item.Value.gameObject.activeInHierarchy)
			{
				list.Add(item.Key);
			}
		}
		return list;
	}

	protected static List<string> GetBridgeEdgeIdsSelected()
	{
		List<string> list = new List<string>();
		foreach (BridgeEdge edge in BridgeSelectionSet.m_Edges)
		{
			if (edge.gameObject.activeInHierarchy)
			{
				list.Add(edge.m_Guid);
			}
		}
		return list;
	}

	protected static List<float> GetBridgeEdgePosition(string id)
	{
		BridgeEdge bridgeEdge = BridgeEdges.FindByGuid(id);
		if (bridgeEdge != null)
		{
			return Vec3ToFloatList(bridgeEdge.transform.position);
		}
		return new List<float> { 0f, 0f, 0f };
	}

	protected static float GetBridgeEdgeStressNormalized(string id)
	{
		BridgeEdge bridgeEdge = BridgeEdges.FindByGuid(id);
		if (bridgeEdge != null && bridgeEdge.m_PhysicsEdge != null)
		{
			return bridgeEdge.m_PhysicsEdge.smoothedStressNormalized;
		}
		return 0f;
	}

	protected static float GetBridgeEdgeStressSigned(string id)
	{
		BridgeEdge bridgeEdge = BridgeEdges.FindByGuid(id);
		if (bridgeEdge != null && bridgeEdge.m_PhysicsEdge != null)
		{
			return bridgeEdge.m_PhysicsEdge.smoothedStressSigned;
		}
		return 0f;
	}

	protected static string GetBridgeEdgeMaterial(string id)
	{
		BridgeEdge bridgeEdge = BridgeEdges.FindByGuid(id);
		if (bridgeEdge != null)
		{
			return bridgeEdge.m_Material.m_MaterialType.ToString();
		}
		return "";
	}

	protected static float GetBridgeEdgeLength(string id)
	{
		BridgeEdge bridgeEdge = BridgeEdges.FindByGuid(id);
		if (bridgeEdge != null)
		{
			return bridgeEdge.GetLength();
		}
		return 0f;
	}

	protected static float GetBridgeEdgeAngle(string id)
	{
		BridgeEdge bridgeEdge = BridgeEdges.FindByGuid(id);
		if (bridgeEdge != null)
		{
			return bridgeEdge.CalculateAngle();
		}
		return 0f;
	}

	protected static List<string> GetBridgeEdgeJoints(string id)
	{
		List<string> list = new List<string> { "", "" };
		BridgeEdge bridgeEdge = BridgeEdges.FindByGuid(id);
		if (bridgeEdge != null)
		{
			if (bridgeEdge.m_JointA != null)
			{
				list[0] = bridgeEdge.m_JointA.m_Guid;
			}
			if (bridgeEdge.m_JointB != null)
			{
				list[1] = bridgeEdge.m_JointB.m_Guid;
			}
		}
		return list;
	}

	protected static bool GetBridgeEdgeIsBroken(string id)
	{
		BridgeEdge bridgeEdge = BridgeEdges.FindByGuid(id);
		if (bridgeEdge != null)
		{
			return bridgeEdge.m_IsBroken;
		}
		return false;
	}

	protected static bool GetBridgeEdgeIsPrebuilt(string id)
	{
		BridgeEdge bridgeEdge = BridgeEdges.FindByGuid(id);
		if (bridgeEdge != null)
		{
			return bridgeEdge.IsPrebuilt();
		}
		return false;
	}

	protected static string GetOverrideEdgeColor(string id)
	{
		BridgeEdge bridgeEdge = BridgeEdges.FindByGuid(id);
		if (bridgeEdge != null && bridgeEdge.m_HasOverrideColor)
		{
			return Utils.ColorToHex(bridgeEdge.m_OverrideColor);
		}
		return "";
	}

	protected static void SetOverrideEdgeColor(string id, string colorStr)
	{
		BridgeEdge bridgeEdge = BridgeEdges.FindByGuid(id);
		if (bridgeEdge != null)
		{
			bridgeEdge.SetOverrideColor(colorStr);
		}
	}

	protected static string GetPermanentEdgeColor(string id)
	{
		BridgeEdge bridgeEdge = BridgeEdges.FindByGuid(id);
		if (bridgeEdge != null && bridgeEdge.m_HasOverrideColorPermanent)
		{
			return Utils.ColorToHex(bridgeEdge.m_OverrideColorPermanent);
		}
		return "";
	}

	protected static void SetPermanentEdgeColor(string id, string colorStr)
	{
		BridgeEdge bridgeEdge = BridgeEdges.FindByGuid(id);
		if (bridgeEdge != null)
		{
			bridgeEdge.SetOverrideColorPermanent(colorStr);
		}
	}

	protected static void BreakEdge(string id)
	{
		Mods.m_IsUsingGameplayMod = true;
		if (!InSimModeAndSimulating())
		{
			return;
		}
		BridgeEdge bridgeEdge = BridgeEdges.FindByGuid(id);
		if (bridgeEdge != null && bridgeEdge.m_PhysicsEdge != null)
		{
			List<IEdgeBreakListener> edgeBreakListeners = SingletonBehaviour<World>.instance.edgeBreakListeners;
			for (int i = 0; i < edgeBreakListeners.Count; i++)
			{
				edgeBreakListeners[i].OnEdgeBroken(bridgeEdge.m_PhysicsEdge.handle);
			}
		}
	}

	protected static string PlaceEdgeJointToJoint(string materialName, string jointIdA, string jointIdB)
	{
		if (GameStateManager.GetState() != GameState.BUILD && GameStateManager.GetState() != GameState.SANDBOX)
		{
			return "WRONG_MODE";
		}
		BridgeMaterialType bridgeMaterialType = GetBridgeMaterialType(materialName);
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(jointIdA);
		BridgeJoint bridgeJoint2 = BridgeJoints.FindByGuid(jointIdB);
		if (bridgeMaterialType == BridgeMaterialType.INVALID || bridgeJoint == null || bridgeJoint2 == null)
		{
			return "INVALID_PARAMETER";
		}
		return TryPlaceEdge(bridgeMaterialType, bridgeJoint, bridgeJoint2, bridgeJoint2.m_Transform.position);
	}

	protected static string PlaceEdgeJointToPos(string materialName, string jointIdA, List<float> posB)
	{
		if (GameStateManager.GetState() != GameState.BUILD && GameStateManager.GetState() != GameState.SANDBOX)
		{
			return "WRONG_MODE";
		}
		BridgeMaterialType bridgeMaterialType = GetBridgeMaterialType(materialName);
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(jointIdA);
		if (bridgeMaterialType == BridgeMaterialType.INVALID || bridgeJoint == null)
		{
			return "INVALID_PARAMETER";
		}
		Vector3 targetPos = FloatListToVec3(posB);
		return TryPlaceEdge(bridgeMaterialType, bridgeJoint, null, targetPos);
	}

	protected static string PlaceEdgePosToPos(string materialName, List<float> posA, List<float> posB)
	{
		if (GameStateManager.GetState() != GameState.BUILD && GameStateManager.GetState() != GameState.SANDBOX)
		{
			return "WRONG_MODE";
		}
		BridgeMaterialType bridgeMaterialType = GetBridgeMaterialType(materialName);
		if (bridgeMaterialType == BridgeMaterialType.INVALID)
		{
			return "INVALID_PARAMETER";
		}
		Vector3 vector = FloatListToVec3(posA);
		Vector3 targetPos = FloatListToVec3(posB);
		BridgeJoint bridgeJoint = BridgeJoints.GetJointAtPoint(vector);
		if (bridgeJoint == null)
		{
			if (!BridgeJointPlacement.m_IgnoreEdgePlacementRestrictions)
			{
				return "NO_START_JOINT";
			}
			bridgeJoint = BridgeJoints.CreateJoint(vector, Utils.GenerateUniqueId());
		}
		return TryPlaceEdge(bridgeMaterialType, bridgeJoint, null, targetPos);
	}

	private static string TryPlaceEdge(BridgeMaterialType materialType, BridgeJoint jointA, BridgeJoint jointB, Vector3 targetPos)
	{
		BridgeJointPlacement.ClearTriangulateJoints();
		BridgeMaterialType buildMaterialType = Bridge.m_BuildMaterialType;
		Bridge.m_BuildMaterialType = materialType;
		PlacementReturnValue placementReturnValue = BridgeJointPlacement.AllowPlacement(jointA, targetPos);
		if (placementReturnValue == PlacementReturnValue.SUCCESS)
		{
			BridgeJointPlacement.ModForcePlacementPos(targetPos);
			placementReturnValue = BridgeJointPlacement.TryFormEdgeBetweenJoints(jointA, jointB, BridgeJointPlacement.GetPlacementPos(), materialType, preview: false);
		}
		Bridge.m_BuildMaterialType = buildMaterialType;
		return placementReturnValue.ToString();
	}

	protected static void IgnoreEdgePlacementRestrictions(bool ignore)
	{
		Mods.m_IsUsingGameplayMod = true;
		BridgeJointPlacement.m_IgnoreEdgePlacementRestrictions = ignore;
	}

	protected static string PlaceEdgeJointToJointCheat(string materialName, string jointIdA, string jointIdB)
	{
		Mods.m_IsUsingGameplayMod = true;
		bool ignoreEdgePlacementRestrictions = BridgeJointPlacement.m_IgnoreEdgePlacementRestrictions;
		BridgeJointPlacement.m_IgnoreEdgePlacementRestrictions = true;
		string result = PlaceEdgeJointToJoint(materialName, jointIdA, jointIdB);
		BridgeJointPlacement.m_IgnoreEdgePlacementRestrictions = ignoreEdgePlacementRestrictions;
		return result;
	}

	protected static string PlaceEdgeJointToPosCheat(string materialName, string jointIdA, List<float> posB)
	{
		Mods.m_IsUsingGameplayMod = true;
		bool ignoreEdgePlacementRestrictions = BridgeJointPlacement.m_IgnoreEdgePlacementRestrictions;
		BridgeJointPlacement.m_IgnoreEdgePlacementRestrictions = true;
		string result = PlaceEdgeJointToPos(materialName, jointIdA, posB);
		BridgeJointPlacement.m_IgnoreEdgePlacementRestrictions = ignoreEdgePlacementRestrictions;
		return result;
	}

	protected static string PlaceEdgePosToPosCheat(string materialName, List<float> posA, List<float> posB)
	{
		Mods.m_IsUsingGameplayMod = true;
		bool ignoreEdgePlacementRestrictions = BridgeJointPlacement.m_IgnoreEdgePlacementRestrictions;
		BridgeJointPlacement.m_IgnoreEdgePlacementRestrictions = true;
		string result = PlaceEdgePosToPos(materialName, posA, posB);
		BridgeJointPlacement.m_IgnoreEdgePlacementRestrictions = ignoreEdgePlacementRestrictions;
		return result;
	}

	protected static List<string> GetJointIds()
	{
		List<string> list = new List<string>();
		foreach (KeyValuePair<string, BridgeJoint> item in BridgeJoints.GetJointDictionary())
		{
			if (item.Value.gameObject.activeInHierarchy)
			{
				list.Add(item.Key);
			}
		}
		return list;
	}

	protected static List<string> GetJointIdsSelected()
	{
		List<string> list = new List<string>();
		foreach (BridgeJoint joint in BridgeSelectionSet.m_Joints)
		{
			if (joint.gameObject.activeInHierarchy)
			{
				list.Add(joint.m_Guid);
			}
		}
		return list;
	}

	protected static List<string> GetJointEdges(string id)
	{
		List<string> list = new List<string>();
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(id);
		if (bridgeJoint != null)
		{
			for (int i = 0; i < bridgeJoint.GetNumConnectedEdges(); i++)
			{
				BridgeEdge connecteEdge = bridgeJoint.GetConnecteEdge(i);
				if (connecteEdge != null)
				{
					list.Add(connecteEdge.m_Guid);
				}
			}
		}
		return list;
	}

	protected static List<float> GetJointPosition(string id)
	{
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(id);
		if (bridgeJoint != null)
		{
			return Vec3ToFloatList(bridgeJoint.m_Transform.position);
		}
		return new List<float> { 0f, 0f, 0f };
	}

	protected static bool GetJointIsSplit(string id)
	{
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(id);
		if (bridgeJoint != null)
		{
			return bridgeJoint.m_IsSplit;
		}
		return false;
	}

	protected static void SetJointIsSplit(string id, bool shouldSplit)
	{
		if (GameStateManager.GetState() != GameState.BUILD)
		{
			return;
		}
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(id);
		if (!(bridgeJoint != null) || bridgeJoint.m_IsSplit == shouldSplit)
		{
			return;
		}
		if (shouldSplit)
		{
			if (HydraulicsPhases.m_Phases.Count > 0)
			{
				bridgeJoint.Split();
				HydraulicsController.AddSplitJointToAllPhasesAcceptingNewAdditions(bridgeJoint);
			}
		}
		else
		{
			bridgeJoint.UnSplit();
		}
	}

	protected static bool GetJointIsAnchor(string id)
	{
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(id);
		if (bridgeJoint != null)
		{
			return bridgeJoint.m_IsAnchor;
		}
		return false;
	}

	protected static bool GetJointIsPrebuilt(string id)
	{
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(id);
		if (bridgeJoint != null)
		{
			return bridgeJoint.IsConnectedToLockedPrebuilt();
		}
		return false;
	}

	protected static void SetJointPosition(string id, List<float> jointPos)
	{
		Mods.m_IsUsingGameplayMod = true;
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(id);
		if (bridgeJoint != null)
		{
			bridgeJoint.m_Transform.position = FloatListToVec3(jointPos);
		}
	}

	protected static bool SetJointLegalPosition(string id, List<float> jointPos)
	{
		if (GameStateManager.GetState() != GameState.BUILD)
		{
			return false;
		}
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(id);
		if ((bool)bridgeJoint && !bridgeJoint.m_IsAnchor && !CampaignTutorial.BlockMoveAction() && !BridgeTrace.IsFilling())
		{
			if (bridgeJoint.IsConnectedToLockedPrebuilt())
			{
				return false;
			}
			return BridgeJointMovement.ModMoveJointToPointLegally(bridgeJoint, FloatListToVec3(jointPos));
		}
		return false;
	}

	protected static bool GetJointIsUnbuildable(string id)
	{
		BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(id);
		if (bridgeJoint != null)
		{
			return bridgeJoint.m_NoBuild;
		}
		return false;
	}

	protected static void RegisterCameraFunctions()
	{
		m_Api.Globals["GetCameraPosition"] = new Func<List<float>>(GetCameraPosition);
		m_Api.Globals["SetCameraPosition"] = new Action<List<float>>(SetCameraPosition);
		m_Api.Globals["GetCameraRotation"] = new Func<List<float>>(GetCameraRotation);
		m_Api.Globals["SetCameraRotation"] = new Action<List<float>>(SetCameraRotation);
		m_Api.Globals["SetCameraLookAtObject"] = new Action<string>(SetCameraLookAtObject);
		m_Api.Globals["GetCameraOrthographic"] = new Func<bool>(GetCameraOrthographic);
		m_Api.Globals["SetCameraOrthographic"] = new Action<bool>(SetCameraOrthographic);
		m_Api.Globals["GetCameraOrthographicSize"] = new Func<float>(GetCameraOrthographicSize);
		m_Api.Globals["SetCameraOrthographicSize"] = new Action<float>(SetCameraOrthographicSize);
		m_Api.Globals["GetCameraFOV"] = new Func<float>(GetCameraFOV);
		m_Api.Globals["SetCameraFOV"] = new Action<float>(SetCameraFOV);
	}

	protected static void ResetCameraToDefault()
	{
		if (m_DidCameraTransformChange)
		{
			Cameras.MainCamera().transform.position = new Vector3(21.879f, 24.55339f, -35.94719f);
			Cameras.MainCamera().transform.rotation = Quaternion.Euler(18.716f, -18.404f, 0f);
		}
		if (m_DidCameraOrthographicChange)
		{
			Cameras.MainCamera().orthographic = true;
			Cameras.ReplayCamera().orthographic = true;
		}
		if (m_DidCameraOrthographicSizeChange)
		{
			Cameras.SetOrthographicSize(11.39678f);
		}
		if (m_DidCameraFOVChange)
		{
			Cameras.SetFOV(50.6f);
		}
		m_DidCameraTransformChange = false;
		m_DidCameraOrthographicChange = false;
		m_DidCameraOrthographicSizeChange = false;
		m_DidCameraFOVChange = false;
	}

	protected static List<float> GetCameraPosition()
	{
		return Vec3ToFloatList(Cameras.MainCamera().transform.position);
	}

	protected static void SetCameraPosition(List<float> camPosition)
	{
		m_DidCameraTransformChange = true;
		Cameras.MainCamera().transform.position = FloatListToVec3(camPosition);
	}

	protected static List<float> GetCameraRotation()
	{
		return Vec3ToFloatList(Cameras.MainCamera().transform.rotation.eulerAngles);
	}

	protected static void SetCameraRotation(List<float> camRotation)
	{
		m_DidCameraTransformChange = true;
		Cameras.MainCamera().transform.rotation = Quaternion.Euler(FloatListToVec3(camRotation));
	}

	protected static void SetCameraLookAtObject(string objectId)
	{
		m_DidCameraTransformChange = true;
		Transform transform = null;
		if (transform == null)
		{
			Vehicle vehicle = Vehicles.FindByGuid(objectId);
			if (vehicle != null)
			{
				transform = vehicle.transform;
			}
		}
		if (transform == null)
		{
			BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(objectId);
			if (bridgeJoint != null)
			{
				transform = bridgeJoint.transform;
			}
		}
		if (transform == null)
		{
			BridgeEdge bridgeEdge = BridgeEdges.FindByGuid(objectId);
			if (bridgeEdge != null)
			{
				transform = bridgeEdge.transform;
			}
		}
		if (transform != null)
		{
			Cameras.MainCamera().transform.LookAt(transform);
		}
	}

	protected static bool GetCameraOrthographic()
	{
		return Cameras.MainCamera().orthographic;
	}

	protected static void SetCameraOrthographic(bool camOrthographic)
	{
		m_DidCameraTransformChange = true;
		Cameras.MainCamera().orthographic = camOrthographic;
		Cameras.ReplayCamera().orthographic = camOrthographic;
		HeightFog.Enable(camOrthographic);
		if (camOrthographic)
		{
			Cameras.EnableSky();
		}
		else
		{
			Cameras.DisableSky();
		}
	}

	protected static float GetCameraOrthographicSize()
	{
		return Cameras.MainCamera().orthographicSize;
	}

	protected static void SetCameraOrthographicSize(float camOrthographicSize)
	{
		m_DidCameraOrthographicSizeChange = true;
		Cameras.SetOrthographicSize(camOrthographicSize);
	}

	protected static float GetCameraFOV()
	{
		return Cameras.MainCamera().fieldOfView;
	}

	protected static void SetCameraFOV(float camFOV)
	{
		m_DidCameraFOVChange = true;
		Cameras.SetFOV(camFOV);
	}

	public static bool CheckForCheatFunctions(FileInfo[] fileInfos)
	{
		return CheckForFunctions(fileInfos, m_CheatFunctionNames);
	}

	public static bool CheckForLanguageFunctions(FileInfo[] fileInfos)
	{
		return CheckForFunctions(fileInfos, m_LanguageFunctionNames);
	}

	public static bool CheckForVehicleUGCFunctions(FileInfo[] fileInfos)
	{
		return CheckForFunctions(fileInfos, m_VehicleUGCFunctionNames);
	}

	public static bool CheckForZVehicleUGCFunctions(FileInfo[] fileInfos)
	{
		return CheckForFunctions(fileInfos, m_ZVehicleUGCFunctionNames);
	}

	public static bool CheckForCustomShapeUGCFunctions(FileInfo[] fileInfos)
	{
		return CheckForFunctions(fileInfos, m_CustomShapeUGCFunctionNames);
	}

	public static bool CheckForDecorUGCFunctions(FileInfo[] fileInfos)
	{
		return CheckForFunctions(fileInfos, m_DecorUGCFunctionNames);
	}

	public static bool CheckForWorkshopCampaignFunctions(FileInfo[] fileInfos)
	{
		return CheckForFunctions(fileInfos, m_WorkshopCampaignFunctionNames);
	}

	public static bool CheckForFunctions(FileInfo[] fileInfos, string[] functions)
	{
		List<Regex> list = new List<Regex>();
		foreach (string pattern in functions)
		{
			list.Add(new Regex(pattern));
		}
		for (int i = 0; i < fileInfos.Length; i++)
		{
			string input = Utils.ReadAllText(fileInfos[i].FullName);
			foreach (Regex item in list)
			{
				if (item.Match(input).Success)
				{
					return true;
				}
			}
		}
		return false;
	}

	public static int GetFunctionCount(FileInfo[] fileInfos, string filenameFilter, string function)
	{
		int num = 0;
		try
		{
			foreach (FileInfo fileInfo in fileInfos)
			{
				if (!string.IsNullOrEmpty(filenameFilter) && !fileInfo.Name.StartsWith(filenameFilter))
				{
					continue;
				}
				using StreamReader streamReader = File.OpenText(fileInfo.FullName);
				string empty = string.Empty;
				while ((empty = streamReader.ReadLine()) != null)
				{
					if (empty.Trim().IndexOf(function) == 0)
					{
						num++;
					}
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Caught exception in ModApi.GetFunctionCount: " + ex.Message);
			num = 0;
		}
		return num;
	}

	public static List<string> GetLinesWithFunction(FileInfo[] fileInfos, string function)
	{
		List<string> list = new List<string>();
		try
		{
			for (int i = 0; i < fileInfos.Length; i++)
			{
				using StreamReader streamReader = File.OpenText(fileInfos[i].FullName);
				string empty = string.Empty;
				while ((empty = streamReader.ReadLine()) != null)
				{
					if (empty.Trim().IndexOf(function) == 0)
					{
						list.Add(empty);
					}
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Caught exception in ModApi.GetFunctionCount: " + ex.Message);
		}
		return list;
	}

	public static bool IsConsoleActivationAllowed()
	{
		return m_IsConsoleActivationAllowed;
	}

	public static void RunConsoleCommand(string command, List<string> argList)
	{
		m_Api.Call(m_Api.Globals[command], argList);
	}

	protected static void RegisterDevFunctions()
	{
		m_Api.Globals["EnableLevelNavArrows"] = new System.Action(EnableLevelNavArrows);
		m_Api.Globals["ShowLevelID"] = new System.Action(ShowLevelID);
		m_Api.Globals["EnableDebugConsole"] = new System.Action(EnableDebugConsole);
		m_Api.Globals["AddDebugConsoleCommand"] = new Action<string, string, int>(AddDebugConsoleCommand);
	}

	protected static void ResetDevToDefault()
	{
		ConsoleCommands.ClearLuaCommands();
		Panel_TopBar.m_LevelNavArrowsEnabled = false;
		Game.m_AllowShowLevelID = false;
		m_IsConsoleActivationAllowed = false;
	}

	protected static void EnableLevelNavArrows()
	{
		Panel_TopBar.m_LevelNavArrowsEnabled = true;
	}

	protected static void ShowLevelID()
	{
		Game.m_AllowShowLevelID = true;
	}

	protected static void EnableDebugConsole()
	{
		m_IsConsoleActivationAllowed = true;
		string path = Path.Combine(m_CurrModDirPath, "ConsoleCommands.lua");
		if (File.Exists(path))
		{
			m_Api.DoString(File.ReadAllText(path));
		}
	}

	protected static void AddDebugConsoleCommand(string commandName, string help, int numParameters)
	{
		ConsoleCommands.AddLuaCommand(commandName, help, numParameters);
	}

	protected static void RegisterGameStateFunctions()
	{
		m_Api.Globals["GetGameState"] = new Func<string>(GetGameState);
		m_Api.Globals["IsSimulatingWithoutPassOrFail"] = new Func<bool>(IsSimulatingWithoutPassOrFail);
		m_Api.Globals["GetSimSpeed"] = new Func<float>(GetSimSpeed);
		m_Api.Globals["SetSimSpeed"] = new Action<float>(SetSimSpeed);
		m_Api.Globals["GetDefaultSimSpeed"] = new Func<float>(GetDefaultSimSpeed);
		m_Api.Globals["GetCurrentLevelID"] = new Func<string>(GetCurrentLevelID);
		m_Api.Globals["OpenLevel"] = new Action<string>(OpenLevel);
		m_Api.Globals["OpenWorkshopLevel"] = new Action<string>(OpenWorkshopLevel);
		m_Api.Globals["SimulationStart"] = new Func<bool>(SimulationStart);
		m_Api.Globals["SimulationTogglePause"] = new Func<bool>(SimulationTogglePause);
		m_Api.Globals["SimulationPause"] = new Func<bool>(SimulationPause);
		m_Api.Globals["SimulationUnpause"] = new Func<bool>(SimulationUnpause);
		m_Api.Globals["SimulationEnd"] = new Func<bool>(SimulationEnd);
		m_Api.Globals["EndLevelSuccess"] = new Func<bool>(EndLevelSuccess);
		m_Api.Globals["EndLevelFail"] = new Func<string, bool>(EndLevelFail);
		m_Api.Globals["IsSimulating"] = new Func<bool>(IsSimulating);
	}

	protected static void ResetGameStateToDefault()
	{
	}

	protected static string GetGameState()
	{
		return GameStateManager.GetState().ToString();
	}

	protected static bool IsSimulatingWithoutPassOrFail()
	{
		return GameStateSim.IsSimulatingWithoutPassOrFail();
	}

	protected static float GetSimSpeed()
	{
		if (BridgeSimSpeed.m_SimulationSpeedIndex >= 0 && BridgeSimSpeed.m_SimulationSpeedIndex < BridgeSimSpeed.m_SimulationSpeeds.Count)
		{
			return BridgeSimSpeed.m_SimulationSpeeds[BridgeSimSpeed.m_SimulationSpeedIndex];
		}
		return 0f;
	}

	protected static void SetSimSpeed(float simSpeed)
	{
		BridgeSimSpeed.SetSimulationSpeedAbsolute(simSpeed);
		if (GameUI.m_Instance != null)
		{
			GameUI.m_Instance.m_TopBar.ApplyChangesAfterSimulationSpeedChange();
		}
	}

	protected static float GetDefaultSimSpeed()
	{
		if (BridgeSimSpeed.m_DefaultSimulationSpeedIndex >= 0 && BridgeSimSpeed.m_DefaultSimulationSpeedIndex < BridgeSimSpeed.m_SimulationSpeeds.Count)
		{
			return BridgeSimSpeed.m_SimulationSpeeds[BridgeSimSpeed.m_DefaultSimulationSpeedIndex];
		}
		return BridgeSimSpeed.m_DefaultSimulationSpeeds[3];
	}

	protected static string GetCurrentLevelID()
	{
		return Game.GetLevelId();
	}

	protected static void OpenLevel(string levelId)
	{
	}

	protected static void OpenWorkshopLevel(string workshopLevelId)
	{
	}

	protected static bool SimulationStart()
	{
		if (GameStateManager.GetState() == GameState.BUILD)
		{
			GameUI.m_Instance.m_TopBar.OnSim();
			return true;
		}
		return false;
	}

	protected static bool SimulationTogglePause()
	{
		if (InSimModeAndSimulating())
		{
			GameUI.m_Instance.m_TopBar.TogglePauseSim();
			return true;
		}
		return false;
	}

	protected static bool SimulationPause()
	{
		if (InSimModeAndSimulating() && !GameUI.m_Instance.m_TopBar.m_PausedSim)
		{
			GameUI.m_Instance.m_TopBar.OnPauseSim();
			return true;
		}
		return false;
	}

	protected static bool SimulationUnpause()
	{
		if (InSimModeAndSimulating() && GameUI.m_Instance.m_TopBar.m_PausedSim)
		{
			GameUI.m_Instance.m_TopBar.OnUnPauseSim();
			return true;
		}
		return false;
	}

	protected static bool SimulationEnd()
	{
		if (InSimModeAndSimulating())
		{
			GameUI.m_Instance.m_TopBar.OnExitSim();
			return true;
		}
		return false;
	}

	protected static bool EndLevelSuccess()
	{
		Mods.m_IsUsingGameplayMod = true;
		if (InSimModeAndSimulating())
		{
			GameStateSim.LevelSuccessImmediate();
			return true;
		}
		return false;
	}

	protected static bool EndLevelFail(string failReasonText)
	{
		if (InSimModeAndSimulating())
		{
			GameStateSim.LevelFailImmediate(failReasonText);
			return true;
		}
		return false;
	}

	protected static bool IsSimulating()
	{
		return InSimModeAndSimulating();
	}

	public static int GetNumWorkshopLanguages()
	{
		int num = Enum.GetValues(typeof(Language)).Length + 1;
		return LocalizationManager.GetAllLanguages().Count - num;
	}

	protected static void RegisterLanguageFunctions()
	{
		m_Api.Globals["AddLanguageCSV"] = new Action<string>(AddLanguageCSV);
	}

	protected static void ResetLanguageToDefault()
	{
		int num = Enum.GetValues(typeof(Language)).Length + 1;
		List<string> allLanguages = LocalizationManager.GetAllLanguages();
		for (int i = num; i < allLanguages.Count; i++)
		{
			LocalizationManager.Sources[0].RemoveLanguage(allLanguages[i]);
		}
	}

	public static void LoadLanguageCSV(string pathAndFileName, string modID)
	{
		if (!File.Exists(pathAndFileName))
		{
			return;
		}
		List<string[]> list = LocalizationReader.ReadCSV(File.ReadAllText(pathAndFileName));
		List<string[]> list2 = new List<string[]>();
		foreach (string[] item in list)
		{
			if (item.Length >= 4)
			{
				list2.Add(new string[2]
				{
					item[0],
					item[3].Trim('\r', '\n', '"')
				});
			}
		}
		if (LocalizationManager.Sources[0].Import_CSV(modID, "", list2, eSpreadsheetUpdateMode.Merge).Length > 0)
		{
			AddErrorMessageToQueue(Localize.Get("UI_LANGUAGE_MOD_ERROR", Path.GetFileName(pathAndFileName)));
		}
	}

	protected static void AddLanguageCSV(string csvFilename)
	{
		LoadLanguageCSV(Path.Combine(m_CurrModDirPath, csvFilename), m_CurrModId);
		SaveLanguageCSVMapping(m_CurrModId, csvFilename);
	}

	public static string GetLanguageCSVFileName(string modID)
	{
		if (!m_LanguageCSVMapping.ContainsKey(modID))
		{
			return string.Empty;
		}
		return m_LanguageCSVMapping[modID];
	}

	public static void LoadLanguageCSVMapping()
	{
		string fullPath = Path.Combine(Profiles.GetProfileRootDirectory(), LANGUAGE_CSVS_FILENAME);
		if (!Utils.FileExists(fullPath))
		{
			return;
		}
		byte[] array = Utils.ReadAllBytes(fullPath);
		if (array != null && array.Length != 0)
		{
			try
			{
				m_LanguageCSVMapping = SerializationUtility.DeserializeValue<Dictionary<string, string>>(array, DataFormat.JSON);
			}
			catch (Exception ex)
			{
				Debug.LogWarning("Exception parsing " + LANGUAGE_CSVS_FILENAME + ": " + ex.Message);
			}
		}
	}

	private static void SaveLanguageCSVMapping(string modID, string csvFilename)
	{
		if (m_LanguageCSVMapping.ContainsKey(modID))
		{
			m_LanguageCSVMapping[modID] = csvFilename;
		}
		else
		{
			m_LanguageCSVMapping.Add(modID, csvFilename);
		}
		byte[] bytes = SerializationUtility.SerializeValue(m_LanguageCSVMapping, DataFormat.JSON);
		Utils.WriteBytes(Path.Combine(Profiles.GetProfileRootDirectory(), LANGUAGE_CSVS_FILENAME), bytes);
	}

	protected static void RegisterMaterialFunctions()
	{
		m_Api.Globals["GetMaterialStrength"] = new Func<string, float>(GetMaterialStrength);
		m_Api.Globals["SetMaterialStrength"] = new Action<string, float>(SetMaterialStrength);
		m_Api.Globals["GetMaterialCost"] = new Func<string, float>(GetMaterialCost);
		m_Api.Globals["SetMaterialCost"] = new Action<string, float>(SetMaterialCost);
		m_Api.Globals["GetMaterialMaxLength"] = new Func<string, float>(GetMaterialMaxLength);
		m_Api.Globals["SetMaterialMaxLength"] = new Action<string, float>(SetMaterialMaxLength);
		m_Api.Globals["GetMaterialTint"] = new Func<string, string>(GetMaterialTint);
		m_Api.Globals["SetMaterialTint"] = new Action<string, string>(SetMaterialTint);
		m_Api.Globals["GetHydraulicsBaseTint"] = new Func<string>(GetHydraulicsBaseTint);
		m_Api.Globals["SetHydraulicsBaseTint"] = new Action<string>(SetHydraulicsBaseTint);
		m_Api.Globals["GetRoadEdgeTint"] = new Func<string>(GetRoadEdgeTint);
		m_Api.Globals["SetRoadEdgeTint"] = new Action<string>(SetRoadEdgeTint);
	}

	protected static void ResetMaterialsToDefault()
	{
		if (m_Defaults == null)
		{
			m_Defaults = new ModFile_Materials();
			m_Defaults.ResetToDefaults();
		}
		MaterialOverrides.m_Instance.m_RoadStrength = m_Defaults.m_RoadStrength;
		MaterialOverrides.m_Instance.m_ReinforcedRoadStrength = m_Defaults.m_ReinforcedRoadStrength;
		MaterialOverrides.m_Instance.m_WoodStrength = m_Defaults.m_WoodStrength;
		MaterialOverrides.m_Instance.m_SteelStrength = m_Defaults.m_SteelStrength;
		MaterialOverrides.m_Instance.m_HydraulicsStrength = m_Defaults.m_HydraulicsStrength;
		MaterialOverrides.m_Instance.m_RopeStrength = m_Defaults.m_RopeStrength;
		MaterialOverrides.m_Instance.m_CableStrength = m_Defaults.m_CableStrength;
		MaterialOverrides.m_Instance.m_SpringStrength = m_Defaults.m_SpringStrength;
		BridgeMaterials.GetBridgeMaterial(BridgeMaterialType.ROAD).m_MaxLength = m_Defaults.m_RoadMaxLength;
		BridgeMaterials.GetBridgeMaterial(BridgeMaterialType.REINFORCED_ROAD).m_MaxLength = m_Defaults.m_ReinforcedRoadMaxLength;
		BridgeMaterials.GetBridgeMaterial(BridgeMaterialType.WOOD).m_MaxLength = m_Defaults.m_WoodMaxLength;
		BridgeMaterials.GetBridgeMaterial(BridgeMaterialType.STEEL).m_MaxLength = m_Defaults.m_SteelMaxLength;
		BridgeMaterials.GetBridgeMaterial(BridgeMaterialType.ROPE).m_MaxLength = m_Defaults.m_RopeMaxLength;
		BridgeMaterials.GetBridgeMaterial(BridgeMaterialType.CABLE).m_MaxLength = m_Defaults.m_CableMaxLength;
		BridgeMaterials.GetBridgeMaterial(BridgeMaterialType.HYDRAULICS).m_MaxLength = m_Defaults.m_HydraulicsMaxLength;
		BridgeMaterials.GetBridgeMaterial(BridgeMaterialType.SPRING).m_MaxLength = m_Defaults.m_SpringMaxLength;
		BridgeMaterials.GetBridgeMaterial(BridgeMaterialType.ROAD).m_PricePerMeter = m_Defaults.m_RoadCost;
		BridgeMaterials.GetBridgeMaterial(BridgeMaterialType.REINFORCED_ROAD).m_PricePerMeter = m_Defaults.m_ReinforcedRoadCost;
		BridgeMaterials.GetBridgeMaterial(BridgeMaterialType.WOOD).m_PricePerMeter = m_Defaults.m_WoodCost;
		BridgeMaterials.GetBridgeMaterial(BridgeMaterialType.STEEL).m_PricePerMeter = m_Defaults.m_SteelCost;
		BridgeMaterials.GetBridgeMaterial(BridgeMaterialType.ROPE).m_PricePerMeter = m_Defaults.m_RopeCost;
		BridgeMaterials.GetBridgeMaterial(BridgeMaterialType.CABLE).m_PricePerMeter = m_Defaults.m_CableCost;
		BridgeMaterials.GetBridgeMaterial(BridgeMaterialType.HYDRAULICS).m_PricePerMeter = m_Defaults.m_HydraulicsCost;
		BridgeMaterials.GetBridgeMaterial(BridgeMaterialType.SPRING).m_PricePerMeter = m_Defaults.m_SpringCost;
		m_IsAnyMaterialTinted = true;
		if (m_IsAnyMaterialTinted)
		{
			SetMaterialTint("ROAD", "#FFFFFF");
			SetMaterialTint("WOOD", "#FFFFFF");
			SetMaterialTint("STEEL", "#E5E5E5");
			SetMaterialTint("HYDRAULICS", "#9C9C9C");
			SetMaterialTint("SPRING", "#B29C00");
			SetMaterialTint("ROPE", "#FFFFFF");
			SetMaterialTint("CABLE", "#FFFFFF");
			SetHydraulicsBaseTint("#2586FF");
			SetRoadEdgeTint("#262626");
			Bridge.GetPrefabFromBridgeMaterial(BridgeMaterialType.HYDRAULICS).GetComponent<BridgeEdge>().m_MeshRenderer.sharedMaterial.SetInt("_Desaturate", 1);
			m_IsAnyMaterialTinted = false;
		}
	}

	protected static float GetMaterialStrength(string materialName)
	{
		materialName = FixCommonMaterialSpellingMistakes(materialName);
		BridgeMaterialType result = BridgeMaterialType.INVALID;
		if (Enum.TryParse<BridgeMaterialType>(materialName, ignoreCase: true, out result))
		{
			return MaterialOverrides.m_Instance.GetMaterialStrength(result);
		}
		Debug.LogError("Mod API Error: Could not find material " + materialName);
		return 0f;
	}

	protected static void SetMaterialStrength(string materialName, float val)
	{
		materialName = FixCommonMaterialSpellingMistakes(materialName);
		Mods.m_IsUsingGameplayMod = true;
		BridgeMaterialType result = BridgeMaterialType.INVALID;
		if (Enum.TryParse<BridgeMaterialType>(materialName, ignoreCase: true, out result))
		{
			switch (result)
			{
			case BridgeMaterialType.CABLE:
				MaterialOverrides.m_Instance.m_CableStrength = val;
				break;
			case BridgeMaterialType.HYDRAULICS:
				MaterialOverrides.m_Instance.m_HydraulicsStrength = val;
				break;
			case BridgeMaterialType.REINFORCED_ROAD:
				MaterialOverrides.m_Instance.m_ReinforcedRoadStrength = val;
				break;
			case BridgeMaterialType.ROAD:
				MaterialOverrides.m_Instance.m_RoadStrength = val;
				break;
			case BridgeMaterialType.ROPE:
				MaterialOverrides.m_Instance.m_RopeStrength = val;
				break;
			case BridgeMaterialType.SPRING:
				MaterialOverrides.m_Instance.m_SpringStrength = val;
				break;
			case BridgeMaterialType.STEEL:
				MaterialOverrides.m_Instance.m_SteelStrength = val;
				break;
			case BridgeMaterialType.WOOD:
				MaterialOverrides.m_Instance.m_WoodStrength = val;
				break;
			default:
				Debug.LogError("Mod API Error: Could not find material " + materialName);
				break;
			}
		}
		else
		{
			Debug.LogError("Mod API Error: Could not find material " + materialName);
		}
	}

	protected static float GetMaterialCost(string materialName)
	{
		materialName = FixCommonMaterialSpellingMistakes(materialName);
		BridgeMaterialType result = BridgeMaterialType.INVALID;
		if (Enum.TryParse<BridgeMaterialType>(materialName, ignoreCase: true, out result))
		{
			BridgeMaterial bridgeMaterial = BridgeMaterials.GetBridgeMaterial(result);
			if (bridgeMaterial != null)
			{
				return bridgeMaterial.m_PricePerMeter;
			}
		}
		Debug.LogError("Mod API Error: Could not find material " + materialName);
		return 0f;
	}

	protected static void SetMaterialCost(string materialName, float val)
	{
		materialName = FixCommonMaterialSpellingMistakes(materialName);
		Mods.m_IsUsingGameplayMod = true;
		BridgeMaterialType result = BridgeMaterialType.INVALID;
		if (Enum.TryParse<BridgeMaterialType>(materialName, ignoreCase: true, out result))
		{
			BridgeMaterial bridgeMaterial = BridgeMaterials.GetBridgeMaterial(result);
			if (bridgeMaterial != null)
			{
				bridgeMaterial.m_PricePerMeter = val;
			}
		}
		else
		{
			Debug.LogError("Mod API Error: Could not find material " + materialName);
		}
	}

	protected static float GetMaterialMaxLength(string materialName)
	{
		materialName = FixCommonMaterialSpellingMistakes(materialName);
		BridgeMaterialType result = BridgeMaterialType.INVALID;
		if (Enum.TryParse<BridgeMaterialType>(materialName, ignoreCase: true, out result))
		{
			BridgeMaterial bridgeMaterial = BridgeMaterials.GetBridgeMaterial(result);
			if (bridgeMaterial != null)
			{
				return bridgeMaterial.m_MaxLength;
			}
		}
		Debug.LogError("Mod API Error: Could not find material " + materialName);
		return 0f;
	}

	protected static void SetMaterialMaxLength(string materialName, float val)
	{
		materialName = FixCommonMaterialSpellingMistakes(materialName);
		Mods.m_IsUsingGameplayMod = true;
		BridgeMaterialType result = BridgeMaterialType.INVALID;
		if (Enum.TryParse<BridgeMaterialType>(materialName, ignoreCase: true, out result))
		{
			BridgeMaterial bridgeMaterial = BridgeMaterials.GetBridgeMaterial(result);
			if (bridgeMaterial != null)
			{
				bridgeMaterial.m_MaxLength = val;
			}
		}
		else
		{
			Debug.LogError("Mod API Error: Could not find material " + materialName);
		}
	}

	protected static string GetMaterialTint(string materialName)
	{
		materialName = FixCommonMaterialSpellingMistakes(materialName);
		BridgeMaterialType result = BridgeMaterialType.INVALID;
		if (Enum.TryParse<BridgeMaterialType>(materialName, ignoreCase: true, out result))
		{
			Color color = Color.white;
			Material sharedMaterial = Bridge.GetPrefabFromBridgeMaterial(result).GetComponent<BridgeEdge>().m_MeshRenderer.sharedMaterial;
			if (result == BridgeMaterialType.SPRING)
			{
				sharedMaterial = Prefabs.m_Instance.m_SpringCoilLink.GetComponent<SpringCoilMeshGenerator>().renderer.sharedMaterial;
			}
			if (sharedMaterial.HasProperty(TINT_COLOR_ID))
			{
				color = sharedMaterial.GetColor(TINT_COLOR_ID);
			}
			else if (sharedMaterial.HasProperty("_BaseColor"))
			{
				color = sharedMaterial.GetColor("_BaseColor");
			}
			return Utils.ColorToHex(color);
		}
		Debug.LogError("Mod API Error: Could not find material " + materialName);
		return "";
	}

	protected static void SetMaterialTint(string materialName, string colorStr)
	{
		m_IsAnyMaterialTinted = true;
		materialName = FixCommonMaterialSpellingMistakes(materialName);
		BridgeMaterialType result = BridgeMaterialType.INVALID;
		if (Enum.TryParse<BridgeMaterialType>(materialName, ignoreCase: true, out result))
		{
			Color colorFromHexCode = Utils.GetColorFromHexCode(colorStr, Color.white);
			Material sharedMaterial = Bridge.GetPrefabFromBridgeMaterial(result).GetComponent<BridgeEdge>().m_MeshRenderer.sharedMaterial;
			if (result == BridgeMaterialType.SPRING)
			{
				sharedMaterial = Prefabs.m_Instance.m_SpringCoilLink.GetComponent<SpringCoilMeshGenerator>().renderer.sharedMaterial;
			}
			if (sharedMaterial.HasProperty(TINT_COLOR_ID))
			{
				sharedMaterial.SetColor(TINT_COLOR_ID, colorFromHexCode);
			}
			else if (sharedMaterial.HasProperty("_BaseColor"))
			{
				sharedMaterial.SetColor("_BaseColor", colorFromHexCode);
			}
			if (result == BridgeMaterialType.HYDRAULICS)
			{
				sharedMaterial.SetInt("_Desaturate", 0);
			}
		}
		else
		{
			Debug.LogError("Mod API Error: Could not find material " + materialName);
		}
	}

	protected static string GetHydraulicsBaseTint()
	{
		Color color = Color.white;
		Material material = null;
		BridgeHydraulicEdgeVisualization componentInChildren = Prefabs.m_Instance.m_HydraulicsTruss.GetComponentInChildren<BridgeHydraulicEdgeVisualization>(includeInactive: true);
		if (componentInChildren != null)
		{
			Renderer componentInChildren2 = componentInChildren.GetComponentInChildren<Renderer>(includeInactive: true);
			if (componentInChildren2 != null)
			{
				material = componentInChildren2.sharedMaterial;
			}
		}
		if (material != null && material.HasProperty(TINT_COLOR_ID))
		{
			color = material.GetColor(TINT_COLOR_ID);
		}
		return Utils.ColorToHex(color);
	}

	protected static void SetHydraulicsBaseTint(string colorStr)
	{
		m_IsAnyMaterialTinted = true;
		Color colorFromHexCode = Utils.GetColorFromHexCode(colorStr, Color.white);
		Material material = null;
		BridgeHydraulicEdgeVisualization componentInChildren = Prefabs.m_Instance.m_HydraulicsTruss.GetComponentInChildren<BridgeHydraulicEdgeVisualization>(includeInactive: true);
		if (componentInChildren != null)
		{
			Renderer componentInChildren2 = componentInChildren.GetComponentInChildren<Renderer>(includeInactive: true);
			if (componentInChildren2 != null)
			{
				material = componentInChildren2.sharedMaterial;
			}
		}
		if (material != null && material.HasProperty(TINT_COLOR_ID))
		{
			material.SetColor(TINT_COLOR_ID, colorFromHexCode);
		}
	}

	protected static string GetRoadEdgeTint()
	{
		Color color = Color.white;
		Material sharedMaterial = Prefabs.m_Instance.m_Road.GetComponent<BridgeEdge>().m_MeshRendererChild.sharedMaterial;
		if (sharedMaterial != null && sharedMaterial.HasProperty(TINT_COLOR_ID))
		{
			color = sharedMaterial.GetColor(TINT_COLOR_ID);
		}
		return Utils.ColorToHex(color);
	}

	protected static void SetRoadEdgeTint(string colorStr)
	{
		m_IsAnyMaterialTinted = true;
		Color colorFromHexCode = Utils.GetColorFromHexCode(colorStr, Color.white);
		Material sharedMaterial = Prefabs.m_Instance.m_Road.GetComponent<BridgeEdge>().m_MeshRendererChild.sharedMaterial;
		if (sharedMaterial != null && sharedMaterial.HasProperty(TINT_COLOR_ID))
		{
			sharedMaterial.SetColor(TINT_COLOR_ID, colorFromHexCode);
		}
	}

	protected static void RegisterSandboxFunctions()
	{
		m_Api.Globals["AddCustomShape"] = new Action<string>(AddCustomShape);
		m_Api.Globals["AddCustomShapeTexture"] = new Action<string, string, string>(AddCustomShapeTexture);
		m_Api.Globals["AddAssetDecor"] = new Action<string, string, string>(AddAssetDecor);
		m_Api.Globals["AddAssetBoat"] = new Action<string, string, string, float>(AddAssetBoat);
		m_Api.Globals["AddAssetPlane"] = new Action<string, string, string, float>(AddAssetPlane);
		m_Api.Globals["AddAssetVehicle"] = new Action<string, string, string, float>(AddAssetVehicle);
		m_Api.Globals["DisableRandomVehicleSpawn"] = new System.Action(DisableRandomVehicleSpawn);
	}

	protected static void ResetSandboxToDefault()
	{
		m_NumModsCurrentlyLoadingAddressables = 0;
		CustomShapesLibrary.ClearUGCSlots();
		CustomShapeTextures.m_Instance.ClearModTextures();
		GameUI.m_Instance.m_SandboxCreateDecorObjects.ClearUGC();
		DecorStubs.RemoveAllUgcStubs();
		GameUI.m_Instance.m_SandboxCreateVehicles.ClearUGC();
		VehicleStubs.RemoveAllUgcStubs();
		GameUI.m_Instance.m_SandboxCreateObjects.ClearUGC();
		ZedAxisVehicleStubs.RemoveAllUgcStubs();
		Sandbox.m_SpawnRandomVehicle = true;
		ResetPrefabAddressesInUse();
		ClearTexturesAndSprites();
	}

	protected static void AddCustomShape(string displayName)
	{
		string path = Path.Combine(m_CurrModDirPath, displayName);
		if (Utils.DirectoryExists(path))
		{
			FileInfo[] files = new DirectoryInfo(path).GetFiles("*" + CustomShapes.CUSTOM_SHAPE_EXT);
			if (files.Length != 0)
			{
				CustomShapesLibrary.RegisterSlot(CustomShapesLibrarySlotType.UGC, path, displayName, files);
				GameUI.m_Instance.m_SandboxCreateObjects.PopulateUgcCustomShapes();
			}
		}
	}

	protected static void AddCustomShapeTexture(string textureFilepath, string textureName, string guid)
	{
		string text = Path.Combine(m_CurrModDirPath, textureFilepath);
		if (File.Exists(text))
		{
			string text2 = m_CurrModId + "_" + guid;
			if (CustomShapeTextures.m_Instance.GetTextureFromId(text2) != null)
			{
				Debug.LogWarning("Trying to add CustomShapeTexture but already exists with ID " + text2);
				return;
			}
			CustomShapeTexture customShapeTexture = ScriptableObject.CreateInstance<CustomShapeTexture>();
			customShapeTexture.m_Texture = GetTextureForPath(text);
			customShapeTexture.m_DisplayNameLocID = textureName;
			customShapeTexture.m_ID = text2;
			CustomShapeTextures.m_Instance.AddModTexture(customShapeTexture);
		}
	}

	protected static async void LoadAssetCatalog()
	{
		m_HasLoadedCatalog = true;
		string path = "aa/Windows/catalog.json";
		string path2 = "aa/Windows/StandaloneWindows64";
		string destPlatformFolder = "StandaloneWindows64";
		string catalogPath = Path.Combine(m_CurrModDirPath, path);
		string sourceDirPath = Path.Combine(m_CurrModDirPath, path2);
		AsyncOperationHandle asyncOperationHandle = Addressables.LoadContentCatalogAsync(catalogPath, autoReleaseHandle: true);
		m_NumModsCurrentlyLoadingAddressables++;
		await asyncOperationHandle.Task;
		m_NumModsCurrentlyLoadingAddressables--;
		DirectoryInfo directoryInfo = new DirectoryInfo(sourceDirPath);
		if (directoryInfo == null)
		{
			return;
		}
		string runtimePath = Addressables.RuntimePath;
		if (!Utils.DirectoryExists(runtimePath))
		{
			Debug.LogError("Cannot find Addressable runtime path.\nPlease run Tools>PB3>Build Addressables in Editor and try again.");
			return;
		}
		try
		{
			FileInfo[] files = directoryInfo.GetFiles();
			foreach (FileInfo fileInfo in files)
			{
				string text = Path.Combine(runtimePath, destPlatformFolder, fileInfo.Name);
				try
				{
					File.Copy(fileInfo.FullName, text, overwrite: true);
				}
				catch (Exception ex)
				{
					Debug.LogWarning("Exception '" + ex.Message + "' trying to copy '" + fileInfo.FullName + "' to '" + text + "'");
				}
			}
		}
		catch (Exception ex2)
		{
			Debug.LogWarning("HANDLED: " + ex2.Message);
		}
	}

	protected static void AddAssetDecor(string iconFilepath, string decorName, string prefabAddress)
	{
		if (!UGCConflictsWithInGamePrefab(prefabAddress))
		{
			if (!m_HasLoadedCatalog)
			{
				LoadAssetCatalog();
			}
			string text = Path.Combine(m_CurrModDirPath, iconFilepath);
			if (File.Exists(text))
			{
				DecorStub decorStub = ScriptableObject.CreateInstance<DecorStub>();
				Texture2D textureForPath = GetTextureForPath(text);
				Sprite sprite = Sprite.Create(GetTextureForPath(text), new Rect(0f, 0f, textureForPath.width, textureForPath.height), new Vector2(0.5f, 0.5f));
				m_CreatedSprites.Add(sprite);
				decorStub.m_Sprite = sprite;
				decorStub.m_DisplayNameLocID = decorName;
				decorStub.m_PrefabAddress = prefabAddress;
				decorStub.m_ModId = m_CurrModId;
				DecorStubs.AddUgcDecorStub(decorStub);
				GameUI.m_Instance.m_SandboxCreateDecorObjects.AddDecorUGC(decorStub, m_CurrModId);
				MarkPrefabAddressInUse(prefabAddress);
			}
		}
	}

	protected static void AddAssetBoat(string iconFilepath, string zedAxisName, string prefabAddress, float mass)
	{
		AddAssetZedAxis(iconFilepath, zedAxisName, prefabAddress, ZedAxisVehicleType.BOAT, mass);
	}

	protected static void AddAssetPlane(string iconFilepath, string zedAxisName, string prefabAddress, float mass)
	{
		AddAssetZedAxis(iconFilepath, zedAxisName, prefabAddress, ZedAxisVehicleType.PLANE, mass);
	}

	protected static void AddAssetZedAxis(string iconFilepath, string zedAxisName, string prefabAddress, ZedAxisVehicleType zedAxisType, float mass)
	{
		if (!UGCConflictsWithInGamePrefab(prefabAddress))
		{
			if (!m_HasLoadedCatalog)
			{
				LoadAssetCatalog();
			}
			string text = Path.Combine(m_CurrModDirPath, iconFilepath);
			if (File.Exists(text))
			{
				Texture2D textureForPath = GetTextureForPath(text);
				Sprite sprite = Sprite.Create(textureForPath, new Rect(0f, 0f, textureForPath.width, textureForPath.height), new Vector2(0.5f, 0.5f));
				m_CreatedSprites.Add(sprite);
				ZedAxisVehicleStub zedAxisVehicleStub = ScriptableObject.CreateInstance<ZedAxisVehicleStub>();
				zedAxisVehicleStub.m_DisplayNameLocID = zedAxisName;
				zedAxisVehicleStub.m_PrefabAddress = prefabAddress;
				zedAxisVehicleStub.m_Icon = sprite;
				zedAxisVehicleStub.m_Mass = mass;
				zedAxisVehicleStub.m_UGC = true;
				zedAxisVehicleStub.m_Type = zedAxisType;
				ZedAxisVehicleStubs.Register(zedAxisVehicleStub);
				GameUI.m_Instance.m_SandboxCreateVehicles.AddZedAxisUGC(zedAxisVehicleStub, m_CurrModId);
				MarkPrefabAddressInUse(prefabAddress);
			}
		}
	}

	protected static void AddAssetVehicle(string iconFilepath, string vehicleName, string prefabAddress, float mass)
	{
		if (UGCConflictsWithInGamePrefab(prefabAddress))
		{
			return;
		}
		if (!m_HasLoadedCatalog)
		{
			try
			{
				LoadAssetCatalog();
			}
			catch (Exception ex)
			{
				Debug.LogWarning("HANDLED: " + ex.Message);
			}
		}
		string text = Path.Combine(m_CurrModDirPath, iconFilepath);
		if (File.Exists(text))
		{
			Texture2D textureForPath = GetTextureForPath(text);
			Sprite sprite = Sprite.Create(textureForPath, new Rect(0f, 0f, textureForPath.width, textureForPath.height), new Vector2(0.5f, 0.5f));
			m_CreatedSprites.Add(sprite);
			VehicleStub vehicleStub = ScriptableObject.CreateInstance<VehicleStub>();
			vehicleStub.m_DisplayNameLocID = vehicleName;
			vehicleStub.m_PrefabAddress = prefabAddress;
			vehicleStub.m_ModId = m_CurrModId;
			vehicleStub.m_Icon = sprite;
			vehicleStub.m_Mass = mass;
			vehicleStub.m_UGC = true;
			vehicleStub.m_CanBeAvatar = false;
			vehicleStub.m_Skins = new VehicleSkin[0];
			VehicleStubs.Register(vehicleStub);
			GameUI.m_Instance.m_SandboxCreateVehicles.AddVehicleUGC(vehicleStub, m_CurrModId);
			MarkPrefabAddressInUse(prefabAddress);
		}
	}

	protected static void DisableRandomVehicleSpawn()
	{
		Sandbox.m_SpawnRandomVehicle = false;
	}

	protected static void MarkPrefabAddressInUse(string prefabAddress)
	{
		if (!m_PrefabAddressesInUse.Add(prefabAddress))
		{
			string fileName = Path.GetFileName(m_CurrModDirPath);
			AddErrorMessageToQueue(Localize.Get("UI_MODS_ERROR_ASSET_NAME_IN_USE", fileName, prefabAddress) ?? "");
		}
	}

	protected static bool UGCConflictsWithInGamePrefab(string prefabAddress)
	{
		if (m_InGamePrefabNames.Contains(prefabAddress))
		{
			string fileName = Path.GetFileName(m_CurrModDirPath);
			AddErrorMessageToQueue(Localize.Get("UI_MODS_ERROR_ASSET_NAME_IN_USE", fileName, prefabAddress) ?? "");
			return true;
		}
		return false;
	}

	protected static void ResetPrefabAddressesInUse()
	{
		m_PrefabAddressesInUse.Clear();
		DecorStub[] decorStubs = DecorStubs.m_Instance.m_DecorStubs;
		foreach (DecorStub decorStub in decorStubs)
		{
			m_PrefabAddressesInUse.Add(decorStub.m_PrefabAddress);
		}
		ThemePreloadStub[] themePreloadStubs = ThemeStubs.m_Instance.m_ThemePreloadStubs;
		foreach (ThemePreloadStub themePreloadStub in themePreloadStubs)
		{
			m_PrefabAddressesInUse.Add(themePreloadStub.m_StubPrefabAddress);
		}
		VehicleStub[] stubs = VehicleStubs.m_Instance.m_Stubs;
		foreach (VehicleStub vehicleStub in stubs)
		{
			m_PrefabAddressesInUse.Add(vehicleStub.m_PrefabAddress);
		}
		ZedAxisVehicleStub[] stubs2 = ZedAxisVehicleStubs.m_Instance.m_Stubs;
		foreach (ZedAxisVehicleStub zedAxisVehicleStub in stubs2)
		{
			m_PrefabAddressesInUse.Add(zedAxisVehicleStub.m_PrefabAddress);
		}
	}

	protected static void ClearTexturesAndSprites()
	{
		foreach (Sprite createdSprite in m_CreatedSprites)
		{
			UnityEngine.Object.DestroyImmediate(createdSprite);
		}
		m_CreatedSprites.Clear();
	}

	protected static void RegisterScreenUIFunctions()
	{
		m_Api.Globals["CreateTextObject"] = new Action<string, int, int>(CreateTextObject);
		m_Api.Globals["DestroyTextObject"] = new Action<string>(DestroyTextObject);
		m_Api.Globals["UpdateTextString"] = new Action<string, string>(UpdateTextString);
		m_Api.Globals["UpdateTextScreenPos"] = new Action<string, float, float>(UpdateTextScreenPos);
		m_Api.Globals["UpdateTextAlignment"] = new Action<string, string, string>(UpdateTextAlignment);
		m_Api.Globals["UpdateTextPivot"] = new Action<string, float, float>(UpdateTextPivot);
		m_Api.Globals["UpdateTextFontSize"] = new Action<string, int>(UpdateTextFontSize);
		m_Api.Globals["UpdateTextColor"] = new Action<string, string>(UpdateTextColor);
		m_Api.Globals["UpdateTextSetBackgroundActive"] = new Action<string, bool>(UpdateTextSetBackgroundActive);
		m_Api.Globals["UpdateTextSetBackgroundColor"] = new Action<string, string, string>(UpdateTextSetBackgroundColor);
		m_Api.Globals["UpdateTextMaxWidth"] = new Action<string, int>(UpdateTextMaxWidth);
		m_Api.Globals["CreateSpriteObject"] = new Action<string, string, int, int>(CreateSpriteObject);
		m_Api.Globals["DestroySpriteObject"] = new Action<string>(DestroySpriteObject);
		m_Api.Globals["UpdateSpriteImage"] = new Action<string, string>(UpdateSpriteImage);
		m_Api.Globals["UpdateSpritePivot"] = new Action<string, float, float>(UpdateSpritePivot);
		m_Api.Globals["UpdateSpriteScreenPos"] = new Action<string, float, float>(UpdateSpriteScreenPos);
		m_Api.Globals["UpdateSpriteColor"] = new Action<string, string>(UpdateSpriteColor);
		m_Api.Globals["CreateButtonObject"] = new Action<string, string, int, int>(CreateButtonObject);
		m_Api.Globals["DestroyButtonObject"] = new Action<string>(DestroyButtonObject);
		m_Api.Globals["UpdateButtonCallback"] = new Action<string, string>(UpdateButtonCallback);
		m_Api.Globals["UpdateButtonText"] = new Action<string, string>(UpdateButtonText);
		m_Api.Globals["UpdateButtonImage"] = new Action<string, string>(UpdateButtonImage);
		m_Api.Globals["UpdateButtonPivot"] = new Action<string, float, float>(UpdateButtonPivot);
		m_Api.Globals["UpdateButtonScreenPos"] = new Action<string, float, float>(UpdateButtonScreenPos);
		m_Api.Globals["UpdateButtonColor"] = new Action<string, string, string>(UpdateButtonColor);
		m_Api.Globals["UpdateButtonTextColor"] = new Action<string, string>(UpdateButtonTextColor);
		m_Api.Globals["UpdateButtonTooltipText"] = new Action<string, string>(UpdateButtonTooltipText);
		m_Api.Globals["UpdateButtonSetOutlineActive"] = new Action<string, bool>(UpdateButtonSetOutlineActive);
		m_Api.Globals["UpdateButtonAddHoverScale"] = new Action<string>(UpdateButtonAddHoverScale);
		m_Api.Globals["UpdateButtonSetInteractable"] = new Action<string, bool>(UpdateButtonSetInteractable);
	}

	protected static void ResetScreenUIToDefault()
	{
		GameUI.m_Instance.m_ModsScreenUI.ResetToDefault();
	}

	protected static void CreateTextObject(string textId, int width, int height)
	{
		GameUI.m_Instance.m_ModsScreenUI.CreateTextObject(textId, width, height);
	}

	protected static void DestroyTextObject(string textId)
	{
		GameUI.m_Instance.m_ModsScreenUI.DestroyTextObject(textId);
	}

	protected static void UpdateTextString(string textId, string textStr)
	{
		GameUI.m_Instance.m_ModsScreenUI.UpdateTextString(textId, textStr);
	}

	protected static void UpdateTextScreenPos(string textId, float xScreenPos, float yScreenPos)
	{
		GameUI.m_Instance.m_ModsScreenUI.UpdateTextScreenPos(textId, xScreenPos, yScreenPos);
	}

	protected static void UpdateTextAlignment(string textId, string horizontalAlign, string verticalAlign)
	{
		GameUI.m_Instance.m_ModsScreenUI.UpdateTextAlignment(textId, horizontalAlign, verticalAlign);
	}

	protected static void UpdateTextPivot(string textId, float xPivot, float yPivot)
	{
		GameUI.m_Instance.m_ModsScreenUI.UpdateTextPivot(textId, xPivot, yPivot);
	}

	protected static void UpdateTextFontSize(string textId, int fontSize)
	{
		GameUI.m_Instance.m_ModsScreenUI.UpdateTextFontSize(textId, fontSize);
	}

	protected static void UpdateTextColor(string textId, string colorStr)
	{
		GameUI.m_Instance.m_ModsScreenUI.UpdateTextColor(textId, colorStr);
	}

	protected static void UpdateTextSetBackgroundActive(string textId, bool bgActive)
	{
		GameUI.m_Instance.m_ModsScreenUI.UpdateTextSetBackgroundActive(textId, bgActive);
	}

	protected static void UpdateTextSetBackgroundColor(string textId, string backgroundColorStr, string outlineColorStr)
	{
		GameUI.m_Instance.m_ModsScreenUI.UpdateTextSetBackgroundColor(textId, backgroundColorStr, outlineColorStr);
	}

	protected static void UpdateTextMaxWidth(string textId, int maxWidth)
	{
		GameUI.m_Instance.m_ModsScreenUI.UpdateTextMaxWidth(textId, maxWidth);
	}

	protected static void CreateSpriteObject(string spriteId, string imagePath, int width, int height)
	{
		GameUI.m_Instance.m_ModsScreenUI.CreateSpriteObject(spriteId, GetSpriteFromPath(imagePath), width, height);
	}

	protected static void DestroySpriteObject(string spriteId)
	{
		GameUI.m_Instance.m_ModsScreenUI.DestroySpriteObject(spriteId);
	}

	protected static void UpdateSpriteImage(string spriteId, string imagePath)
	{
		GameUI.m_Instance.m_ModsScreenUI.UpdateSpriteImage(spriteId, GetSpriteFromPath(imagePath));
	}

	protected static void UpdateSpriteScreenPos(string spriteId, float xScreenPos, float yScreenPos)
	{
		GameUI.m_Instance.m_ModsScreenUI.UpdateSpriteScreenPos(spriteId, xScreenPos, yScreenPos);
	}

	protected static void UpdateSpritePivot(string spriteId, float xPivot, float yPivot)
	{
		GameUI.m_Instance.m_ModsScreenUI.UpdateSpritePivot(spriteId, xPivot, yPivot);
	}

	protected static void UpdateSpriteColor(string spriteId, string colorStr)
	{
		GameUI.m_Instance.m_ModsScreenUI.UpdateSpriteColor(spriteId, colorStr);
	}

	protected static void CreateButtonObject(string buttonId, string callback, int width, int height)
	{
		GameUI.m_Instance.m_ModsScreenUI.CreateButtonObject(buttonId, callback, width, height);
	}

	protected static void DestroyButtonObject(string buttonId)
	{
		GameUI.m_Instance.m_ModsScreenUI.DestroyButtonObject(buttonId);
	}

	protected static void UpdateButtonCallback(string buttonId, string callback)
	{
		GameUI.m_Instance.m_ModsScreenUI.UpdateButtonCallback(buttonId, callback);
	}

	protected static void UpdateButtonText(string buttonId, string buttonText)
	{
		GameUI.m_Instance.m_ModsScreenUI.UpdateButtonText(buttonId, buttonText);
	}

	protected static void UpdateButtonImage(string buttonId, string imagePath)
	{
		GameUI.m_Instance.m_ModsScreenUI.UpdateButtonImage(buttonId, GetSpriteFromPath(imagePath));
	}

	protected static void UpdateButtonScreenPos(string buttonId, float xScreenPos, float yScreenPos)
	{
		GameUI.m_Instance.m_ModsScreenUI.UpdateButtonScreenPos(buttonId, xScreenPos, yScreenPos);
	}

	protected static void UpdateButtonPivot(string buttonId, float xPivot, float yPivot)
	{
		GameUI.m_Instance.m_ModsScreenUI.UpdateButtonPivot(buttonId, xPivot, yPivot);
	}

	protected static void UpdateButtonColor(string buttonId, string normalColorStr, string hoverColorStr)
	{
		GameUI.m_Instance.m_ModsScreenUI.UpdateButtonColor(buttonId, normalColorStr, hoverColorStr);
	}

	protected static void UpdateButtonTextColor(string buttonId, string colorStr)
	{
		GameUI.m_Instance.m_ModsScreenUI.UpdateButtonTextColor(buttonId, colorStr);
	}

	protected static void UpdateButtonTooltipText(string buttonId, string tooltipText)
	{
		GameUI.m_Instance.m_ModsScreenUI.UpdateButtonTooltipText(buttonId, tooltipText);
	}

	protected static void UpdateButtonSetOutlineActive(string buttonId, bool outlineActive)
	{
		GameUI.m_Instance.m_ModsScreenUI.UpdateButtonSetOutlineActive(buttonId, outlineActive);
	}

	protected static void UpdateButtonAddHoverScale(string buttonId)
	{
		GameUI.m_Instance.m_ModsScreenUI.UpdateButtonAddHoverScale(buttonId);
	}

	protected static void UpdateButtonSetInteractable(string buttonId, bool interactable)
	{
		GameUI.m_Instance.m_ModsScreenUI.UpdateButtonSetInteractable(buttonId, interactable);
	}

	protected static void RegisterThemeFunctions()
	{
		m_Api.Globals["SetThemeSky"] = new Action<string, string, string, string, float>(SetThemeSky);
	}

	protected static void ResetThemeToDefault()
	{
		ThemeStubs.m_Instance.ClearSkyOverrides();
	}

	protected static void SetThemeSky(string themeName, string top, string middle, string bottom, float middleOffset)
	{
		string idFromName = ThemeStubs.m_Instance.GetIdFromName(themeName);
		if (string.IsNullOrEmpty(idFromName))
		{
			Debug.LogError("Mod API Error: Could not find theme with name " + themeName);
			return;
		}
		ThemePreloadStub preloadStubFromId = ThemeStubs.m_Instance.GetPreloadStubFromId(idFromName);
		if (preloadStubFromId == null)
		{
			Debug.LogError("Mod API Error: Could not find Theme Stub with ID " + idFromName);
		}
		else
		{
			preloadStubFromId.SetSkyOverride(top, middle, bottom, middleOffset);
		}
	}

	protected static void RegisterUIFunctions()
	{
		m_Api.Globals["SetSimSpeeds"] = new Action<List<int>>(SetSimSpeeds);
		m_Api.Globals["SetDefaultSimSpeed"] = new Action<int>(SetDefaultSimSpeed);
		m_Api.Globals["ShowMessage"] = new Action<string>(ShowMessage);
		m_Api.Globals["IsMenuOpen"] = new Func<bool>(IsMenuOpen);
		m_Api.Globals["IsStressVisibilityEnabled"] = new Func<bool>(IsStressVisibilityEnabled);
		m_Api.Globals["GetUIScaleMode"] = new Func<int>(GetUIScaleMode);
		m_Api.Globals["GetUIScaleFactor"] = new Func<float>(GetUIScaleFactor);
	}

	protected static void ResetUIToDefault()
	{
		BridgeSimSpeed.SetSimulationSpeeds(BridgeSimSpeed.m_DefaultSimulationSpeeds);
		BridgeSimSpeed.SetSimulationSpeedIndex(3);
	}

	protected static void SetSimSpeeds(List<int> speeds)
	{
		List<float> list = new List<float>();
		foreach (int speed in speeds)
		{
			list.Add((float)speed / 100f);
		}
		if (list.Count > 0)
		{
			BridgeSimSpeed.SetSimulationSpeeds(list);
			BridgeSimSpeed.SetSimulationSpeedIndex(Mathf.Clamp(BridgeSimSpeed.m_SimulationSpeedIndex, 0, BridgeSimSpeed.m_SimulationSpeeds.Count - 1));
		}
	}

	protected static void SetDefaultSimSpeed(int speed)
	{
		float b = (float)speed / 100f;
		for (int i = 0; i < BridgeSimSpeed.m_SimulationSpeeds.Count; i++)
		{
			if (Mathf.Approximately(BridgeSimSpeed.m_SimulationSpeeds[i], b))
			{
				BridgeSimSpeed.m_DefaultSimulationSpeedIndex = i;
				BridgeSimSpeed.SetSimulationSpeedIndex(BridgeSimSpeed.m_DefaultSimulationSpeedIndex);
				break;
			}
		}
	}

	protected static void ShowMessage(string message)
	{
		PopUpMessage.DisplayInfoOkOnly(message);
	}

	protected static bool IsMenuOpen()
	{
		return ActivePanels.m_Panels.Count > 0;
	}

	protected static bool IsStressVisibilityEnabled()
	{
		return Profiles.m_ActiveProfile.m_StressViewEnabled;
	}

	protected static int GetUIScaleMode()
	{
		if (Profiles.m_ActiveProfile.m_UIScaleMode != UIScaleMode.SCALE_WITH_SCREEN_SIZE)
		{
			return 1;
		}
		return 0;
	}

	protected static float GetUIScaleFactor()
	{
		return Profiles.m_ActiveProfile.m_UIScaleFactor;
	}

	protected static void RegisterUnityUtilsFunctions()
	{
		m_Api.Globals["GetUnscaledDeltaTime"] = new Func<float>(GetUnscaledDeltaTime);
		m_Api.Globals["GetFrameCount"] = new Func<int>(GetFrameCount);
		m_Api.Globals["GetGameTime"] = new Func<float>(GetGameTime);
		m_Api.Globals["ConsoleLog"] = new Action<string>(ConsoleLog);
		m_Api.Globals["WorldToScreenPos"] = new Func<List<float>, List<float>>(WorldToScreenPos);
		m_Api.Globals["GetScreenResolution"] = new Func<List<float>>(GetScreenResolution);
		m_Api.Globals["GetKeyDown"] = new Func<string, bool>(GetKeyDown);
		m_Api.Globals["GetKeycodeDown"] = new Func<int, bool>(GetKeycodeDown);
		m_Api.Globals["GetKeyJustPressed"] = new Func<string, bool>(GetKeyJustPressed);
		m_Api.Globals["GetKeycodeJustPressed"] = new Func<int, bool>(GetKeycodeJustPressed);
		m_Api.Globals["GetMouseButtonDown"] = new Func<int, bool>(GetMouseButtonDown);
		m_Api.Globals["GetMouseButtonJustPressed"] = new Func<int, bool>(GetMouseButtonJustPressed);
		m_Api.Globals["GetMousePosition"] = new Func<List<float>>(GetMousePosition);
		m_Api.Globals["GetMouseScreenPosition"] = new Func<List<float>>(GetMouseScreenPosition);
		m_Api.Globals["GetMouseWorldPosition"] = new Func<List<float>>(GetMouseWorldPosition);
		m_Api.Globals["GetNameOfKeyDownThisFrame"] = new Func<string>(GetNameOfKeyDownThisFrame);
		m_Api.Globals["SetIgnoreGameInputKey"] = new Action<string>(SetIgnoreGameInputKey);
		m_Api.Globals["SetIgnoreGameInputKeycode"] = new Action<int>(SetIgnoreGameInputKeycode);
		m_Api.Globals["SetGameInputActive"] = new Action<bool>(SetGameInputActive);
		m_Api.Globals["GetVar_Int"] = new Func<string, int>(GetVar_Int);
		m_Api.Globals["SetVar_Int"] = new Action<string, int>(SetVar_Int);
		m_Api.Globals["GetVar_Float"] = new Func<string, float>(GetVar_Float);
		m_Api.Globals["SetVar_Float"] = new Action<string, float>(SetVar_Float);
		m_Api.Globals["GetVar_String"] = new Func<string, string>(GetVar_String);
		m_Api.Globals["SetVar_String"] = new Action<string, string>(SetVar_String);
		m_Api.Globals["GetVar_IntList"] = new Func<string, List<int>>(GetVar_IntList);
		m_Api.Globals["SetVar_IntList"] = new Action<string, List<int>>(SetVar_IntList);
		m_Api.Globals["GetVar_FloatList"] = new Func<string, List<float>>(GetVar_FloatList);
		m_Api.Globals["SetVar_FloatList"] = new Action<string, List<float>>(SetVar_FloatList);
		m_Api.Globals["GetVar_StringList"] = new Func<string, List<string>>(GetVar_StringList);
		m_Api.Globals["SetVar_StringList"] = new Action<string, List<string>>(SetVar_StringList);
		m_Api.Globals["RunLuaDelayed"] = new Action<string, float>(RunLuaDelayed);
	}

	protected static void ResetUnityUtilsToDefault()
	{
		m_IntDict.Clear();
		m_FloatDict.Clear();
		m_StringDict.Clear();
		m_IntListDict.Clear();
		m_FloatListDict.Clear();
		m_StringListDict.Clear();
		GameInput.m_ModKeybindIgnoreGameInput = KeyCode.None;
		GameInput.m_IgnoreAllGameInput = false;
		m_DelayedLuaCalls.Clear();
	}

	protected static bool GetKeyDown(string keycodeString)
	{
		return Input.GetKey((KeyCode)Enum.Parse(typeof(KeyCode), keycodeString));
	}

	protected static bool GetKeycodeDown(int keycodeInt)
	{
		return Input.GetKey((KeyCode)keycodeInt);
	}

	protected static bool GetKeyJustPressed(string keycodeString)
	{
		return Input.GetKeyDown((KeyCode)Enum.Parse(typeof(KeyCode), keycodeString));
	}

	protected static bool GetKeycodeJustPressed(int keycodeInt)
	{
		return Input.GetKeyDown((KeyCode)keycodeInt);
	}

	protected static bool GetMouseButtonDown(int mouseButton)
	{
		return Input.GetMouseButton(mouseButton);
	}

	protected static bool GetMouseButtonJustPressed(int mouseButton)
	{
		return Input.GetMouseButtonDown(mouseButton);
	}

	protected static List<float> GetMousePosition()
	{
		return Vec3ToFloatList(GameInput.GetMousePosition());
	}

	protected static List<float> GetMouseScreenPosition()
	{
		List<float> mousePosition = GetMousePosition();
		mousePosition[0] /= Screen.width;
		mousePosition[1] /= Screen.height;
		return mousePosition;
	}

	protected static List<float> GetMouseWorldPosition()
	{
		return Vec3ToFloatList(Utils.GetWorldPointFromScreenPos(GameInput.GetMousePosition()));
	}

	protected static string GetNameOfKeyDownThisFrame()
	{
		foreach (KeyCode value in Enum.GetValues(typeof(KeyCode)))
		{
			if (value < KeyCode.Mouse0 && Input.GetKeyDown(value))
			{
				return value.ToString();
			}
		}
		return "";
	}

	protected static void SetIgnoreGameInputKey(string keycodeString)
	{
		GameInput.m_ModKeybindIgnoreGameInput = (KeyCode)Enum.Parse(typeof(KeyCode), keycodeString);
	}

	protected static void SetIgnoreGameInputKeycode(int keycodeInt)
	{
		GameInput.m_ModKeybindIgnoreGameInput = (KeyCode)keycodeInt;
	}

	protected static void SetGameInputActive(bool inputActive)
	{
		GameInput.m_IgnoreAllGameInput = !inputActive;
	}

	protected static float GetUnscaledDeltaTime()
	{
		return Time.unscaledDeltaTime;
	}

	protected static int GetFrameCount()
	{
		return Time.frameCount;
	}

	protected static float GetGameTime()
	{
		return Time.realtimeSinceStartup;
	}

	protected static void ConsoleLog(string msg)
	{
		Debug.Log(msg);
	}

	protected static List<float> WorldToScreenPos(List<float> worldPos)
	{
		List<float> list = new List<float> { 0f, 0f, 0f };
		if (worldPos.Count >= 3)
		{
			Vector3 position = new Vector3(worldPos[0], worldPos[1], worldPos[2]);
			Vector3 vector = Camera.main.WorldToViewportPoint(position);
			list[0] = vector.x;
			list[1] = vector.y;
			list[2] = vector.z;
		}
		return list;
	}

	protected static List<float> GetScreenResolution()
	{
		List<float> list = new List<float>();
		list.Add(0f);
		list.Add(0f);
		list[0] = Screen.width;
		list[1] = Screen.height;
		return list;
	}

	protected static int GetVar_Int(string varName)
	{
		return GetVarFromDict(m_IntDict, varName);
	}

	protected static void SetVar_Int(string varName, int val)
	{
		SetVarInDict(m_IntDict, varName, val);
	}

	protected static float GetVar_Float(string varName)
	{
		return GetVarFromDict(m_FloatDict, varName);
	}

	protected static void SetVar_Float(string varName, float val)
	{
		SetVarInDict(m_FloatDict, varName, val);
	}

	protected static string GetVar_String(string varName)
	{
		return GetVarFromDict(m_StringDict, varName);
	}

	protected static void SetVar_String(string varName, string val)
	{
		SetVarInDict(m_StringDict, varName, val);
	}

	protected static List<int> GetVar_IntList(string varName)
	{
		return GetVarFromDict(m_IntListDict, varName);
	}

	protected static void SetVar_IntList(string varName, List<int> val)
	{
		SetVarInDict(m_IntListDict, varName, val);
	}

	protected static List<float> GetVar_FloatList(string varName)
	{
		return GetVarFromDict(m_FloatListDict, varName);
	}

	protected static void SetVar_FloatList(string varName, List<float> val)
	{
		SetVarInDict(m_FloatListDict, varName, val);
	}

	protected static List<string> GetVar_StringList(string varName)
	{
		return GetVarFromDict(m_StringListDict, varName);
	}

	protected static void SetVar_StringList(string varName, List<string> val)
	{
		SetVarInDict(m_StringListDict, varName, val);
	}

	private static T GetVarFromDict<T>(Dictionary<string, T> variableDict, string key)
	{
		key += m_CurrModId;
		if (variableDict.ContainsKey(key))
		{
			return variableDict[key];
		}
		return Activator.CreateInstance<T>();
	}

	private static void SetVarInDict<T>(Dictionary<string, T> variableDict, string key, T newVal)
	{
		key += m_CurrModId;
		if (variableDict.ContainsKey(key))
		{
			variableDict[key] = newVal;
		}
		else
		{
			variableDict.Add(key, newVal);
		}
	}

	protected static void RunLuaDelayed(string luaCall, float delaySeconds)
	{
		float num = Time.realtimeSinceStartup + delaySeconds;
		int num2 = -1;
		for (int i = 0; i < m_DelayedLuaCalls.Count; i++)
		{
			if (num < m_DelayedLuaCalls[i].m_RealtimeToTrigger)
			{
				num2 = i;
				break;
			}
		}
		if (num2 > 0)
		{
			m_DelayedLuaCalls.Insert(num2, new DelayedLuaCall(luaCall, num));
		}
		else
		{
			m_DelayedLuaCalls.Add(new DelayedLuaCall(luaCall, num));
		}
	}

	protected static void ResetUpdateLoopsToDefault()
	{
		m_OnUpdateDict.Clear();
		m_OnFixedUpdateDict.Clear();
		m_OnUpdateFuncDict.Clear();
		m_OnFixedUpdateFuncDict.Clear();
		m_ErrorMessageQueueList.Clear();
		m_ErrorMessageShownList.Clear();
	}

	public static void AddOnUpdate(string modDirPath, string luaScript)
	{
		if (!m_OnUpdateDict.ContainsKey(modDirPath))
		{
			m_OnUpdateDict.Add(modDirPath, luaScript);
		}
	}

	public static void AddOnFixedUpdate(string modDirPath, string luaScript)
	{
		if (!m_OnFixedUpdateDict.ContainsKey(modDirPath))
		{
			m_OnFixedUpdateDict.Add(modDirPath, luaScript);
		}
	}

	public static void RunOnUpdate()
	{
		MaybeRunDelayedLua();
		MaybeShowQueuedErrorMessage();
		if (m_OnUpdateDict.Count == 0)
		{
			return;
		}
		foreach (string key in m_OnUpdateDict.Keys)
		{
			try
			{
				if (m_OnUpdateFuncDict.ContainsKey(key))
				{
					RunFunction(key, m_OnUpdateFuncDict[key]);
					continue;
				}
				DynValue value = RunScript(key, m_OnUpdateDict[key]);
				m_OnUpdateFuncDict.Add(key, value);
			}
			catch (Exception ex)
			{
				string fileName = Path.GetFileName(key);
				AddErrorMessageToQueue(Localize.Get("UI_MODS_ERROR_UPDATE", fileName) + " " + ex.Message);
			}
		}
	}

	public static void RunOnFixedUpdate()
	{
		if (m_OnFixedUpdateDict.Count == 0)
		{
			return;
		}
		foreach (string key in m_OnFixedUpdateDict.Keys)
		{
			try
			{
				if (m_OnFixedUpdateFuncDict.ContainsKey(key))
				{
					RunFunction(key, m_OnFixedUpdateFuncDict[key]);
					continue;
				}
				DynValue value = RunScript(key, m_OnFixedUpdateDict[key]);
				m_OnFixedUpdateFuncDict.Add(key, value);
			}
			catch (Exception ex)
			{
				string fileName = Path.GetFileName(key);
				AddErrorMessageToQueue(Localize.Get("UI_MODS_ERROR_FIXEDUPDATE", fileName) + " " + ex.Message);
			}
		}
	}

	public static void AddErrorMessageToQueue(string errorMessage)
	{
		if (!m_ErrorMessageQueueList.Contains(errorMessage) && !m_ErrorMessageShownList.Contains(errorMessage))
		{
			Debug.LogError(errorMessage);
			m_ErrorMessageQueueList.Add(errorMessage);
		}
	}

	private static void MaybeShowQueuedErrorMessage()
	{
		if (!(Time.timeSinceLevelLoad < 1f) && m_ErrorMessageQueueList.Count > 0 && !PopUpMessage.IsActive())
		{
			PopUpMessage.DisplayErrorOkOnly(m_ErrorMessageQueueList[0]);
			m_ErrorMessageShownList.Add(m_ErrorMessageQueueList[0]);
			m_ErrorMessageQueueList.RemoveAt(0);
		}
	}

	private static void MaybeRunDelayedLua()
	{
		if (m_DelayedLuaCalls.Count == 0)
		{
			return;
		}
		for (int i = 0; i < 1000; i++)
		{
			if (m_DelayedLuaCalls.Count <= 0)
			{
				break;
			}
			if (!(Time.realtimeSinceStartup >= m_DelayedLuaCalls[0].m_RealtimeToTrigger))
			{
				break;
			}
			m_Api.DoString(m_DelayedLuaCalls[0].m_LuaString);
			m_DelayedLuaCalls.RemoveAt(0);
		}
	}

	protected static void RegisterVehiclesFunctions()
	{
		m_Api.Globals["AddVehicleSkin"] = new Action<string, string, string, string, string>(AddVehicleSkin);
		m_Api.Globals["AddVehicleSkinAndIcon"] = new Action<string, string, string, string, string, string>(AddVehicleSkinAndIcon);
		m_Api.Globals["GetVehicleIds"] = new Func<List<string>>(GetVehicleIds);
		m_Api.Globals["GetVehiclePosition"] = new Func<string, List<float>>(GetVehiclePosition);
		m_Api.Globals["GetVehicleSpeed"] = new Func<string, float>(GetVehicleSpeed);
		m_Api.Globals["GetVehicleGameState"] = new Func<string, string>(GetVehicleGameState);
		m_Api.Globals["GetVehicleFlippedDirection"] = new Func<string, bool>(GetVehicleFlippedDirection);
		m_Api.Globals["GetVehiclePhysicsVelocity"] = new Func<string, List<float>>(GetVehiclePhysicsVelocity);
		m_Api.Globals["GetVehicleAcceleration"] = new Func<string, float>(GetVehicleAcceleration);
		m_Api.Globals["GetVehicleHorsepower"] = new Func<string, float>(GetVehicleHorsepower);
		m_Api.Globals["GetVehicleTargetSpeed"] = new Func<string, float>(GetVehicleTargetSpeed);
		m_Api.Globals["GetVehicleMass"] = new Func<string, float>(GetVehicleMass);
		m_Api.Globals["GetVehicleRotation"] = new Func<string, List<float>>(GetVehicleRotation);
		m_Api.Globals["GetVehicleBrakingForce"] = new Func<string, float>(GetVehicleBrakingForce);
		m_Api.Globals["GetVehicleIdleDownhill"] = new Func<string, bool>(GetVehicleIdleDownhill);
		m_Api.Globals["SetVehiclePosition"] = new Action<string, List<float>>(SetVehiclePosition);
		m_Api.Globals["SetVehicleFlippedDirection"] = new Action<string, bool>(SetVehicleFlippedDirection);
		m_Api.Globals["SetVehiclePhysicsVelocity"] = new Action<string, List<float>>(SetVehiclePhysicsVelocity);
		m_Api.Globals["SetVehicleSpeed"] = new Action<string, float>(SetVehicleSpeed);
		m_Api.Globals["SetVehicleAcceleration"] = new Action<string, float>(SetVehicleAcceleration);
		m_Api.Globals["SetVehicleHorsepower"] = new Action<string, float>(SetVehicleHorsepower);
		m_Api.Globals["SetVehicleTargetSpeed"] = new Action<string, float>(SetVehicleTargetSpeed);
		m_Api.Globals["SetVehicleMass"] = new Action<string, float>(SetVehicleMass);
		m_Api.Globals["SetVehicleRotation"] = new Action<string, List<float>>(SetVehicleRotation);
		m_Api.Globals["SetVehicleBrakingForce"] = new Action<string, float>(SetVehicleBrakingForce);
		m_Api.Globals["SetVehicleIdleDownhill"] = new Action<string, bool>(SetVehicleIdleDownhill);
	}

	protected static void ResetVehiclesToDefault()
	{
		VehicleSkins.ClearUGCSkins();
	}

	protected static List<string> GetVehicleIds()
	{
		List<string> list = new List<string>();
		foreach (Vehicle vehicle in Vehicles.m_Vehicles)
		{
			list.Add(vehicle.m_Guid);
		}
		return list;
	}

	protected static bool GetVehicleFlippedDirection(string guid)
	{
		Vehicle vehicle = Vehicles.FindByGuid(guid);
		if (vehicle != null)
		{
			if (vehicle.Physics != null)
			{
				return vehicle.Physics.isFlipped;
			}
			return vehicle.m_Flipped;
		}
		return false;
	}

	protected static void SetVehicleFlippedDirection(string guid, bool flipped)
	{
		Mods.m_IsUsingGameplayMod = true;
		Vehicle vehicle = Vehicles.FindByGuid(guid);
		if (vehicle != null)
		{
			if (vehicle.Physics != null && vehicle.Physics.isFlipped != flipped)
			{
				vehicle.PhysicsVehicleFlip();
			}
			else
			{
				vehicle.m_Flipped = flipped;
			}
		}
	}

	protected static List<float> GetVehiclePosition(string guid)
	{
		List<float> list = new List<float> { 0f, 0f, 0f };
		Vehicle vehicle = Vehicles.FindByGuid(guid);
		if (vehicle != null)
		{
			list[0] = vehicle.transform.position.x;
			list[1] = vehicle.transform.position.y;
			list[2] = vehicle.transform.position.z;
		}
		return list;
	}

	protected static void SetVehiclePosition(string guid, List<float> vehiclePos)
	{
	}

	protected static List<float> GetVehicleRotation(string guid)
	{
		Vehicle vehicle = Vehicles.FindByGuid(guid);
		if (vehicle != null)
		{
			return Vec3ToFloatList(vehicle.transform.rotation.eulerAngles);
		}
		return new List<float> { 0f, 0f, 0f };
	}

	protected static void SetVehicleRotation(string guid, List<float> rotation)
	{
	}

	protected static float GetVehicleSpeed(string guid)
	{
		Vehicle vehicle = Vehicles.FindByGuid(guid);
		if (vehicle != null)
		{
			return vehicle.Speed;
		}
		return 0f;
	}

	protected static void SetVehicleSpeed(string guid, float speed)
	{
		Mods.m_IsUsingGameplayMod = true;
		_ = Vehicles.FindByGuid(guid) != null;
	}

	protected static float GetVehicleBrakingForce(string guid)
	{
		Vehicle vehicle = Vehicles.FindByGuid(guid);
		if (vehicle != null)
		{
			return vehicle.m_BrakingForceMultiplier;
		}
		return 0f;
	}

	protected static void SetVehicleBrakingForce(string guid, float brakingForce)
	{
		Mods.m_IsUsingGameplayMod = true;
		Vehicle vehicle = Vehicles.FindByGuid(guid);
		if (vehicle != null)
		{
			vehicle.m_BrakingForceMultiplier = brakingForce;
			if ((bool)vehicle.Physics)
			{
				vehicle.Physics.brakingForceMultiplier = brakingForce;
			}
		}
	}

	protected static bool GetVehicleIdleDownhill(string guid)
	{
		Vehicle vehicle = Vehicles.FindByGuid(guid);
		if (vehicle != null)
		{
			return vehicle.m_IdleOnDownhill;
		}
		return false;
	}

	protected static void SetVehicleIdleDownhill(string guid, bool idle)
	{
		Mods.m_IsUsingGameplayMod = true;
		Vehicle vehicle = Vehicles.FindByGuid(guid);
		if (vehicle != null)
		{
			vehicle.m_IdleOnDownhill = idle;
			if ((bool)vehicle.Physics)
			{
				vehicle.Physics.idleOnDownhill = idle;
			}
		}
	}

	protected static string GetVehicleGameState(string guid)
	{
		Vehicle vehicle = Vehicles.FindByGuid(guid);
		if (vehicle != null)
		{
			if (GameStateManager.GetState() == GameState.BUILD)
			{
				return "WAITING_BUILD_MODE";
			}
			if (vehicle.m_ReachedVictoryFlag)
			{
				return "REACHED_FLAG";
			}
			if ((bool)vehicle.WheelsUnderWater())
			{
				return "UNDERWATER";
			}
			if (vehicle.m_ReachedStopCheckpoint)
			{
				return "WAITING_CHECKPOINT";
			}
			if (vehicle.Physics != null && vehicle.Physics.targetVelocity <= 0.001f)
			{
				return "WAITING_START";
			}
			return "DRIVING";
		}
		return "";
	}

	protected static List<float> GetVehiclePhysicsVelocity(string guid)
	{
		List<float> result = new List<float> { 0f, 0f, 0f };
		_ = Vehicles.FindByGuid(guid) != null;
		return result;
	}

	protected static void SetVehiclePhysicsVelocity(string guid, List<float> vehiclePos)
	{
		Mods.m_IsUsingGameplayMod = true;
		_ = Vehicles.FindByGuid(guid) != null;
	}

	protected static float GetVehicleAcceleration(string guid)
	{
		Vehicle vehicle = Vehicles.FindByGuid(guid);
		if (vehicle != null)
		{
			return vehicle.m_DesiredAcceleration;
		}
		return 0f;
	}

	protected static void SetVehicleAcceleration(string guid, float accel)
	{
		Mods.m_IsUsingGameplayMod = true;
		Vehicle vehicle = Vehicles.FindByGuid(guid);
		if (vehicle != null)
		{
			vehicle.m_DesiredAcceleration = accel;
			if (vehicle.Physics != null)
			{
				vehicle.Physics.desiredAcceleration = accel;
			}
		}
	}

	protected static float GetVehicleHorsepower(string guid)
	{
		Vehicle vehicle = Vehicles.FindByGuid(guid);
		if (vehicle != null)
		{
			return vehicle.m_Acceleration;
		}
		return 0f;
	}

	protected static void SetVehicleHorsepower(string guid, float horsepower)
	{
		Mods.m_IsUsingGameplayMod = true;
		Vehicle vehicle = Vehicles.FindByGuid(guid);
		if (vehicle != null)
		{
			vehicle.m_Acceleration = horsepower;
			if (vehicle.Physics != null)
			{
				vehicle.Physics.acceleration = horsepower;
			}
		}
	}

	protected static float GetVehicleTargetSpeed(string guid)
	{
		Vehicle vehicle = Vehicles.FindByGuid(guid);
		if (vehicle != null)
		{
			return vehicle.m_TargetSpeed;
		}
		return 0f;
	}

	protected static void SetVehicleTargetSpeed(string guid, float targetSpeed)
	{
		Mods.m_IsUsingGameplayMod = true;
		Vehicle vehicle = Vehicles.FindByGuid(guid);
		if (vehicle != null)
		{
			vehicle.m_TargetSpeed = targetSpeed;
			vehicle.SetPhysicsVehicleTargetSpeed(targetSpeed);
		}
	}

	protected static float GetVehicleMass(string guid)
	{
		Vehicle vehicle = Vehicles.FindByGuid(guid);
		if (vehicle != null)
		{
			return vehicle.m_Mass;
		}
		return 0f;
	}

	protected static void SetVehicleMass(string guid, float mass)
	{
		Mods.m_IsUsingGameplayMod = true;
		Vehicle vehicle = Vehicles.FindByGuid(guid);
		if (vehicle != null)
		{
			vehicle.m_Mass = mass;
			if (vehicle.Physics != null)
			{
				vehicle.Physics.mass = mass;
			}
		}
	}

	protected static void AddVehicleSkin(string vehicleAddressableName, string textureFilepath, string displayName, string colorStr, string guid)
	{
		string text = Path.Combine(m_CurrModDirPath, textureFilepath);
		if (File.Exists(text))
		{
			string text2 = m_CurrModId + "_" + guid;
			if (VehicleSkins.FindByID(text2) != null)
			{
				Debug.LogWarning("Trying to add VehicleSkin but already exists with ID " + text2);
				return;
			}
			VehicleSkin vehicleSkin = ScriptableObject.CreateInstance<VehicleSkin>();
			vehicleSkin.m_VehicleAddressableName = vehicleAddressableName;
			vehicleSkin.m_PathToTexture = text;
			vehicleSkin.m_DisplayNameLocID = displayName;
			vehicleSkin.m_ID = text2;
			vehicleSkin.m_IsMod = true;
			vehicleSkin.m_FlagColor = Utils.GetColorFromHexCode(colorStr, Color.white);
			vehicleSkin.m_UIColor = Utils.GetColorFromHexCode(colorStr, Color.white);
			VehicleSkins.Add(vehicleSkin);
		}
	}

	protected static void AddVehicleSkinAndIcon(string vehicleAddressableName, string textureFilepath, string iconFilename, string displayName, string colorStr, string guid)
	{
		AddVehicleSkin(vehicleAddressableName, textureFilepath, displayName, colorStr, guid);
		VehicleSkin vehicleSkin = VehicleSkins.FindByID(m_CurrModId + "_" + guid);
		if (vehicleSkin == null)
		{
			return;
		}
		string text = Path.Combine(m_CurrModDirPath, iconFilename);
		if (File.Exists(text))
		{
			Texture2D textureForPath = GetTextureForPath(text);
			if (textureForPath != null)
			{
				vehicleSkin.m_Icon = Sprite.Create(textureForPath, new Rect(0f, 0f, textureForPath.width, textureForPath.height), new Vector2(0.5f, 0.5f));
			}
		}
	}

	protected static void RegisterWorkshopCampaignFunctions()
	{
		m_Api.Globals["WorkshopCampaignCreate"] = new Action<string>(WorkshopCampaignCreate);
		m_Api.Globals["WorkshopCampaignCreateWorld"] = new Action<string, string, string, int>(WorkshopCampaignCreateWorld);
		m_Api.Globals["WorkshopCampaignSetWorldIconByPrefix"] = new Action<string, string>(WorkshopCampaignSetWorldIconByPrefix);
		m_Api.Globals["WorkshopCampaignSetWorldIcon"] = new Action<string, string, string>(WorkshopCampaignSetWorldIcon);
		m_Api.Globals["WorkshopCampaignSetWorldIconPosition"] = new Action<string, float, float>(WorkshopCampaignSetWorldIconPosition);
		m_Api.Globals["WorkshopCampaignAddLevelToWorld"] = new Action<string, string>(WorkshopCampaignAddLevelToWorld);
		m_Api.Globals["WorkshopCampaignAddTutorialLevelToWorld"] = new Action<string, string>(WorkshopCampaignAddTutorialLevelToWorld);
	}

	protected static void ResetWorkshopCampaignToDefault()
	{
		WorkshopCampaigns.m_Campaigns.Clear();
	}

	protected static void WorkshopCampaignCreate(string winMessage)
	{
		if (WorkshopCampaigns.Get(m_CurrModId) == null)
		{
			WorkshopCampaign campaign = new WorkshopCampaign(m_CurrModId, winMessage);
			WorkshopCampaigns.Add(m_CurrModId, campaign);
		}
	}

	protected static void WorkshopCampaignCreateWorld(string worldName, string prefix, string subtitle, int worldDifficulty)
	{
		WorkshopCampaign workshopCampaign = WorkshopCampaigns.Get(m_CurrModId);
		if (workshopCampaign != null && workshopCampaign.m_Worlds.Count < WorkshopCampaigns.MAX_WORLD_PER_CAMPAIGN)
		{
			WorkshopCampaignWorld world = new WorkshopCampaignWorld(worldName, worldName, prefix, subtitle, Mathf.Clamp(worldDifficulty, 1, WorkshopCampaigns.MAX_DIFFICULTY));
			workshopCampaign.AddWorld(worldName, world);
		}
	}

	protected static void WorkshopCampaignSetWorldIconByPrefix(string worldName, string worldPrefix)
	{
		WorkshopCampaign workshopCampaign = WorkshopCampaigns.Get(m_CurrModId);
		if (workshopCampaign == null)
		{
			return;
		}
		WorkshopCampaignWorld world = workshopCampaign.GetWorld(worldName);
		if (world != null)
		{
			ThemePreloadStub prelodStubForWorldPrefix = CampaignWorlds.m_Instance.GetPrelodStubForWorldPrefix(worldPrefix);
			if (prelodStubForWorldPrefix != null)
			{
				world.m_IconSprite = prelodStubForWorldPrefix.m_Icon;
				world.m_IconSpriteSelected = prelodStubForWorldPrefix.m_IconSelected;
			}
		}
	}

	protected static void WorkshopCampaignSetWorldIcon(string worldName, string iconSpritePath, string selectedIconSpritePath)
	{
		WorkshopCampaign workshopCampaign = WorkshopCampaigns.Get(m_CurrModId);
		if (workshopCampaign != null)
		{
			WorkshopCampaignWorld world = workshopCampaign.GetWorld(worldName);
			if (world != null)
			{
				world.m_IconSprite = GetSpriteFromPath(iconSpritePath);
				world.m_IconSpriteSelected = GetSpriteFromPath(selectedIconSpritePath);
			}
		}
	}

	protected static void WorkshopCampaignSetWorldIconPosition(string worldName, float iconPositionX, float iconPositionY)
	{
		WorkshopCampaign workshopCampaign = WorkshopCampaigns.Get(m_CurrModId);
		if (workshopCampaign != null)
		{
			WorkshopCampaignWorld world = workshopCampaign.GetWorld(worldName);
			if (world != null)
			{
				world.m_IconPosition.x = Mathf.Clamp(iconPositionX, WorkshopCampaigns.MIN_ICON_ANCHORED_POS.x, WorkshopCampaigns.MAX_ICON_ANCHORED_POS.x);
				world.m_IconPosition.y = Mathf.Clamp(iconPositionY, WorkshopCampaigns.MIN_ICON_ANCHORED_POS.y, WorkshopCampaigns.MAX_ICON_ANCHORED_POS.y);
				world.m_UseCustomPosition = true;
			}
		}
	}

	protected static void WorkshopCampaignAddLevelToWorld(string worldName, string levelId)
	{
		WorkshopCampaign workshopCampaign = WorkshopCampaigns.Get(m_CurrModId);
		if (workshopCampaign != null)
		{
			WorkshopCampaignWorld world = workshopCampaign.GetWorld(worldName);
			if (world != null && world.m_LevelIds.Count < WorkshopCampaigns.MAX_LEVELS_PER_WORLD && !world.m_LevelIds.Contains(levelId))
			{
				world.m_LevelIds.Add(levelId);
			}
		}
	}

	protected static void WorkshopCampaignAddTutorialLevelToWorld(string worldName, string levelId)
	{
		WorkshopCampaignAddLevelToWorld(worldName, levelId);
		WorkshopCampaigns.Get(m_CurrModId)?.GetWorldWithLevelId(levelId)?.m_Tutorials.Add(levelId);
	}
}
