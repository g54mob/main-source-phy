using System.Collections.Generic;
using Poly.Collide;
using Poly.Collide.Unity;
using UnityEngine;

public class CustomShapes
{
	public static List<CustomShape> m_Shapes = new List<CustomShape>();

	public static float BUILD_MODE_SATURATION = 0.5f;

	public static float DEFAULT_MASS = 40f;

	public static float MIN_MASS = 0.4f;

	public static float MAX_MASS = 2000f;

	public static float MIN_NORMALIZED_SCALE = 0.1f;

	public static float MIN_NORMALIZED_SCALE_SLIDER = 0.25f;

	public static float MAX_NORMALIZED_SCALE_SLIDER = 3f;

	public static float MAX_NORMALIZED_SCALE = 10f;

	public static float DEFAULT_BOUNCINESS = 0.5f;

	public static float DEFAULT_PIN_MOTOR_STRENGTH = 0f;

	public static float DEFAULT_PIN_TARGET_VELOCITY = 0f;

	public static float DEFAULT_PIN_TARGET_ACCELERATION_SECONDS = 0f;

	public static float MAX_PIN_MOTOR_STRENGTH = 1000f;

	public static float MAX_PIN_TARGET_VELOCITY = 1000f;

	public static float MAX_PIN_TARGET_ACCELERATION = 1000f;

	public static int NGON_DEFAULT_NUM_EDGES = 5;

	public static int NGON_DEFAULT_RADIUS = 1;

	public static int NGON_MIN_RADIUS = 1;

	public static int NGON_MAX_RADIUS = 50;

	public static int NGON_MIN_NUM_EDGES = 1;

	public static int NGON_MAX_NUM_EDGES = 128;

	public static float DEFAULT_TILING = 1f;

	public static float DEFAULT_THICKNESS = 4f;

	public static float MIN_THICKNESS = 0.1f;

	public static float MAX_THICKNESS = 100f;

	public static float MIN_Z = -50f;

	public static float MAX_Z = 50f;

	public static string CUSTOM_SHAPE_EXT = ".shape";

	public static string CUSTOM_SHAPE_DYNAMIC_PROP_EXT = ".prop";

	public static string CUSTOM_SHAPE_NAME_SQUARE = "square";

	public static string CUSTOM_SHAPE_NAME_CIRCLE = "circle";

	public static string CUSTOM_SHAPE_NAME_NGON = "ngon";

	public static string AUTO_GENERATED_MESH_ID = "AUTOGEN";

	public static CustomShape CreateCustomShape(GameObject prefab, Vector3 pos, Quaternion rot)
	{
		GameObject gameObject = Object.Instantiate(prefab, pos, rot);
		if (!gameObject)
		{
			return null;
		}
		CustomShape component = gameObject.GetComponent<CustomShape>();
		if (!component)
		{
			return null;
		}
		component.name = prefab.name;
		component.m_MeshId = AUTO_GENERATED_MESH_ID;
		component.m_TextureTiling = DEFAULT_TILING;
		component.m_Thickness = DEFAULT_THICKNESS;
		component.m_Texture = CustomShapeTextures.m_Instance.GetDefaultCustomShapeTexture();
		component.m_CollidesWithVehicles = true;
		component.UpdateShaderProperties(Color.grey, buildMode: false);
		component.RecalculateGridOffset();
		m_Shapes.Add(component);
		return component;
	}

	public static void DestroyAll()
	{
		foreach (CustomShape shape in m_Shapes)
		{
			DestroyCustomShape(shape);
		}
		m_Shapes.Clear();
	}

	public static void DestroyCustomShape(CustomShape shape)
	{
		shape.gameObject.SetActive(value: false);
		for (int num = shape.m_Anchors.Count - 1; num >= 0; num--)
		{
			shape.DestroyAnchor(shape.m_Anchors[num]);
		}
		Object.Destroy(shape.gameObject);
	}

	public static void AddToSimulation()
	{
		foreach (CustomShape shape in m_Shapes)
		{
			if (shape.gameObject.activeInHierarchy)
			{
				CustomShapeCollisionInfo componentInChildren = shape.GetComponentInChildren<CustomShapeCollisionInfo>();
				if ((bool)componentInChildren)
				{
					componentInChildren.OnAddedToWorld();
					shape.OnAddToSimulation();
				}
			}
		}
	}

	public static void UpdateManual()
	{
		foreach (CustomShape shape in m_Shapes)
		{
			if (shape.gameObject.activeInHierarchy)
			{
				shape.UpdateManual();
			}
		}
	}

	public static void LateUpdateManual()
	{
		foreach (CustomShape shape in m_Shapes)
		{
			if (shape.gameObject.activeInHierarchy)
			{
				shape.LateUpdateManual();
			}
		}
	}

	public static void FixedUpdateManual()
	{
		foreach (CustomShape shape in m_Shapes)
		{
			if (shape.gameObject.activeInHierarchy)
			{
				shape.FixedUpdateManual();
			}
		}
	}

	public static void Restore()
	{
		foreach (CustomShape shape in m_Shapes)
		{
			if (shape.gameObject.activeInHierarchy)
			{
				shape.EndSimulation();
				shape.Restore();
			}
		}
		ReattachAnchorsToCustomShapes();
	}

	public static void UpdateSpawnTransform()
	{
		foreach (CustomShape shape in m_Shapes)
		{
			if (shape.gameObject.activeInHierarchy)
			{
				shape.UpdateSpawnTransform();
			}
		}
	}

	public static void DisableOutlines()
	{
		foreach (CustomShape shape in m_Shapes)
		{
			if (shape.gameObject.activeInHierarchy)
			{
				shape.DisableOutline();
			}
		}
	}

	public static void EnableMeshRendering()
	{
		foreach (CustomShape shape in m_Shapes)
		{
			if (shape.gameObject.activeInHierarchy)
			{
				shape.EnableMeshRendering(on: true);
			}
		}
	}

	public static void UpdateOutlines()
	{
		foreach (CustomShape shape in m_Shapes)
		{
			if (shape.gameObject.activeInHierarchy)
			{
				shape.UpdateOutline();
			}
		}
	}

	public static bool OverlapsPolygonShape(PolygonShape polygonShape)
	{
		foreach (CustomShape shape in m_Shapes)
		{
			if (shape.gameObject.activeInHierarchy && shape.OverlapsPolygonShape(polygonShape))
			{
				return true;
			}
		}
		return false;
	}

	public static bool OverlapsPolygonShapeBlockingRoad(PolygonShape polygonShape)
	{
		foreach (CustomShape shape in m_Shapes)
		{
			if (shape.gameObject.activeInHierarchy && shape.m_CollidesWithRoad && shape.OverlapsPolygonShape(polygonShape))
			{
				return true;
			}
		}
		return false;
	}

	public static bool OverlapsPolygonShapeBlockingNodes(PolygonShape polygonShape)
	{
		foreach (CustomShape shape in m_Shapes)
		{
			if (shape.gameObject.activeInHierarchy && shape.m_CollidesWithNodes && shape.OverlapsPolygonShape(polygonShape))
			{
				return true;
			}
		}
		return false;
	}

	public static void UpdatePolygonShapes()
	{
		foreach (CustomShape shape in m_Shapes)
		{
			if (shape.gameObject.activeInHierarchy)
			{
				shape.UpdatePolygonShapes();
			}
		}
	}

	public static void EnableAnchorsAttachedToCustomShapes()
	{
		foreach (CustomShape shape in m_Shapes)
		{
			if (!shape.gameObject.activeInHierarchy)
			{
				continue;
			}
			foreach (CustomShapeAnchor anchor in shape.m_Anchors)
			{
				BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(anchor.m_BridgeJointGuid);
				if ((bool)bridgeJoint)
				{
					bridgeJoint.gameObject.SetActive(value: true);
				}
			}
		}
	}

	public static bool AnchorUsedByShape(string bridgeGuid)
	{
		foreach (CustomShape shape in m_Shapes)
		{
			if (!shape.gameObject.activeInHierarchy)
			{
				continue;
			}
			foreach (CustomShapeAnchor anchor in shape.m_Anchors)
			{
				if (anchor.m_BridgeJointGuid == bridgeGuid)
				{
					return true;
				}
			}
		}
		return false;
	}

	public static void ReattachAnchorsToCustomShapes()
	{
		foreach (CustomShape shape in m_Shapes)
		{
			if (!shape.gameObject.activeInHierarchy)
			{
				continue;
			}
			foreach (CustomShapeAnchor anchor in shape.m_Anchors)
			{
				BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(anchor.m_BridgeJointGuid);
				if ((bool)bridgeJoint)
				{
					bridgeJoint.transform.parent = anchor.transform;
					bridgeJoint.transform.localPosition = Vector3.zero;
					bridgeJoint.transform.rotation = Quaternion.identity;
				}
			}
		}
	}

	public static CustomShapePin CreatePin(Vector3 pos, Transform parent, Vector3 localScale)
	{
		GameObject gameObject = Object.Instantiate(Prefabs.m_Instance.m_CustomShapePin, pos, Quaternion.identity, parent);
		if (gameObject == null)
		{
			return null;
		}
		CustomShapePin component = gameObject.GetComponent<CustomShapePin>();
		if ((bool)component)
		{
			component.InverseScale(localScale);
		}
		return component;
	}

	public static CustomShapeAnchor CreateAnchor(Vector3 pos, Transform parent, Vector3 shapeScale)
	{
		GameObject gameObject = Object.Instantiate(Prefabs.m_Instance.m_CustomShapeAnchor, pos, Quaternion.identity, parent);
		if (gameObject == null)
		{
			return null;
		}
		CustomShapeAnchor component = gameObject.GetComponent<CustomShapeAnchor>();
		if ((bool)component)
		{
			component.InverseScale(shapeScale);
		}
		return component;
	}

	public static List<CustomShapeProxy> Serialize()
	{
		List<CustomShapeProxy> list = new List<CustomShapeProxy>();
		foreach (CustomShape shape in m_Shapes)
		{
			if (shape.gameObject.activeInHierarchy)
			{
				shape.RemoveAnchorsOutsideShape();
				list.Add(new CustomShapeProxy(shape));
			}
		}
		return list;
	}

	public static void Deserialize(List<CustomShapeProxy> proxies)
	{
		if (proxies == null)
		{
			return;
		}
		foreach (CustomShapeProxy proxy in proxies)
		{
			CreateCustomShapeFromProxy(proxy);
		}
	}

	public static CustomShape CreateCustomShapeFromProxy(CustomShapeProxy proxy)
	{
		CustomShape customShape = CreateCustomShape(Prefabs.m_Instance.m_CustomShape, proxy.m_Pos, proxy.m_Rot);
		if ((bool)customShape)
		{
			ApplyProxyToCustomShape(customShape, proxy);
			customShape.EnableMeshRendering(GameStateManager.GetState() != GameState.SANDBOX);
		}
		return customShape;
	}

	public static void ApplyProxyToCustomShape(CustomShape shape, CustomShapeProxy proxy)
	{
		shape.transform.position = proxy.m_Pos;
		shape.m_RotationDegrees = proxy.m_RotationDegrees;
		shape.m_CollidesWithRoad = proxy.m_CollidesWithRoad;
		shape.m_CollidesWithNodes = proxy.m_CollidesWithNodes;
		shape.m_CollidesWithRamps = proxy.m_CollidesWithRamps;
		shape.m_CollidesWithVehicles = proxy.m_CollidesWithVehicles;
		shape.m_CollidesWithSplitNodes = proxy.m_CollidesWithSplitNodes;
		shape.m_Color = proxy.m_Color;
		shape.m_Thickness = proxy.m_Thickness;
		shape.m_Mass = proxy.m_Mass;
		shape.m_Bounciness = proxy.m_Bounciness;
		shape.m_PinMotorStrength = proxy.m_PinMotorStrength;
		shape.m_PinTargetVelocity = proxy.m_PinTargetVelocity;
		shape.m_PinTargetAccelerationSeconds = proxy.m_PinTargetAcceleration;
		shape.m_Texture = CustomShapeTextures.m_Instance.GetTextureFromId(proxy.m_TextureId);
		shape.m_TextureTiling = proxy.m_TextureTiling;
		shape.m_SpawnPos = proxy.m_Pos;
		shape.m_SpawnRot = proxy.m_Rot;
		shape.m_Behavior = proxy.m_Behavior;
		shape.m_LowFriction = proxy.m_LowFriction;
		shape.m_MeshId = proxy.m_MeshId;
		if (shape.m_MeshId != AUTO_GENERATED_MESH_ID)
		{
			shape.UseCustomMesh(shape.m_MeshId, proxy.m_MeshLocalPos, 0f);
			shape.m_AutoGeneratedMesh.SetActive(value: false);
		}
		shape.DestroyVertsAndEdges();
		shape.InitializeFromPointsList(proxy.m_PointsLocalSpace);
		shape.m_CustomMeshScale = proxy.m_MeshScale;
		if (shape.m_CustomMesh != null)
		{
			shape.m_CustomMesh.transform.localScale = proxy.m_MeshScale;
		}
		if (proxy.m_Scale.magnitude > Mathf.Epsilon)
		{
			shape.SetLocalScale(proxy.m_Scale);
		}
		shape.Flip(proxy.m_Flipped);
		shape.DestroyPins();
		foreach (Vector3 staticPin in proxy.m_StaticPins)
		{
			CustomShapePin customShapePin = CreatePin(shape.transform.TransformPoint(staticPin), shape.m_PinsParent.transform, shape.transform.localScale);
			if ((bool)customShapePin)
			{
				shape.m_Pins.Add(customShapePin);
			}
		}
		if (shape.m_Pins.Count > 1)
		{
			shape.m_Behavior = CustomShapeBehavior.STATIC;
		}
		shape.DestroyAnchors();
		for (int i = 0; i < proxy.m_DynamicAnchorGuids.Count; i++)
		{
			BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(proxy.m_DynamicAnchorGuids[i]);
			if ((bool)bridgeJoint)
			{
				CustomShapeAnchor customShapeAnchor = CreateAnchor(bridgeJoint.transform.position, shape.m_AnchorsParent.transform, shape.transform.localScale);
				if ((bool)customShapeAnchor)
				{
					customShapeAnchor.SetBridgeJointGuid(bridgeJoint.m_Guid);
					customShapeAnchor.m_SpriteRenderer.gameObject.SetActive(value: false);
					bridgeJoint.transform.parent = customShapeAnchor.transform;
					shape.m_Anchors.Add(customShapeAnchor);
				}
			}
			else if (i < proxy.m_DynamicAnchors.Count)
			{
				shape.AddAnchor(shape.transform.TransformPoint(proxy.m_DynamicAnchors[i]));
			}
		}
		if (shape.m_Anchors.Count > 0)
		{
			shape.transform.position = new Vector3(shape.transform.position.x, shape.transform.position.y, 0f);
		}
		shape.m_SandboxItem.SetOutlineDirty(dirty: true);
		shape.UpdatePolygonShapes();
	}

	public static void UnParentDynamicAnchors(List<string> dynamicAnchorGuids)
	{
		foreach (string dynamicAnchorGuid in dynamicAnchorGuids)
		{
			BridgeJoint bridgeJoint = BridgeJoints.FindByGuid(dynamicAnchorGuid);
			if ((bool)bridgeJoint)
			{
				bridgeJoint.transform.SetParent(null);
			}
		}
	}

	public static void ShowAllPins()
	{
		foreach (CustomShape shape in m_Shapes)
		{
			if (shape.gameObject.activeInHierarchy)
			{
				shape.ShowStaticPins();
			}
		}
	}

	public static void ShowPinMeshes(bool on)
	{
		foreach (CustomShape shape in m_Shapes)
		{
			if (shape.gameObject.activeInHierarchy)
			{
				shape.ShowPinMeshes(on);
			}
		}
	}

	public static void HidePinsForStaticShapes()
	{
		foreach (CustomShape shape in m_Shapes)
		{
			if (shape.gameObject.activeInHierarchy && shape.m_Behavior == CustomShapeBehavior.STATIC)
			{
				shape.HideStaticPins();
			}
		}
	}

	public static void HideExternalPins()
	{
		foreach (CustomShape shape in m_Shapes)
		{
			if (!shape.gameObject.activeInHierarchy)
			{
				continue;
			}
			foreach (CustomShapePin pin in shape.m_Pins)
			{
				if (!shape.OverlapsPoint(pin.transform.position))
				{
					pin.gameObject.SetActive(value: false);
				}
			}
		}
	}

	public static void UpdateShaderProperties(bool buildMode)
	{
		foreach (CustomShape shape in m_Shapes)
		{
			if (shape.gameObject.activeInHierarchy)
			{
				shape.UpdateShaderProperties(shape.m_Color, buildMode);
			}
		}
	}

	public static CustomShape GetClosestThatOverlapsPolygonShape(Vector2 pos)
	{
		CustomShape result = null;
		float num = float.MaxValue;
		foreach (CustomShape shape in m_Shapes)
		{
			if (shape.m_PolygonCollider2D.OverlapPoint(pos))
			{
				float num2 = Vector2.Distance(pos, shape.transform.position);
				if (num2 < num)
				{
					num = num2;
					result = shape;
				}
			}
		}
		return result;
	}

	public static void EnterSandboxMode()
	{
		foreach (CustomShape shape in m_Shapes)
		{
			shape.EnterSandboxMode();
		}
	}

	public static void EnterBuildMode()
	{
		foreach (CustomShape shape in m_Shapes)
		{
			shape.EnterBuildMode();
		}
	}

	public static void EnterSimMode()
	{
		foreach (CustomShape shape in m_Shapes)
		{
			shape.EnterSimMode();
		}
	}

	public static void ShowAnchorMeshes(bool on)
	{
		foreach (CustomShape shape in m_Shapes)
		{
			shape.ShowAnchorMeshes(on);
		}
	}

	public static void MaybeDisableMeshRendering()
	{
		foreach (CustomShape shape in m_Shapes)
		{
			shape.MaybeDisableMeshRendering();
		}
	}

	public static void UpdateCustomShapeMinimumStrengthHint(CustomShape shape)
	{
		if (shape.m_Behavior == CustomShapeBehavior.MOTORIZED)
		{
			shape.m_CollisionInfo.CreatePolygonShapes_ForBuildMode(calculateMinimumStrengthHint: true);
		}
	}
}
