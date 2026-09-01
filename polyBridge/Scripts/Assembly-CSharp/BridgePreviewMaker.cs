using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vectrosity;

public class BridgePreviewMaker
{
	private static readonly int m_Width = 512;

	private static readonly int m_Height = 288;

	public static Image m_Image;

	public static float m_BridgeCost;

	public static Color m_ColorWood = Utils.RGBToColor(209, 212, 36);

	public static Color m_ColorSteel = Utils.RGBToColor(167, 43, 43);

	public static Color m_ColorHydraulic = Utils.RGBToColor(135, 76, 221);

	public static Color m_ColorRope = Utils.RGBToColor(94, 71, 10);

	public static Color m_ColorCable = Utils.RGBToColor(26, 26, 26);

	public static Color m_ColorSpring = Utils.RGBToColor(225, 225, 225);

	public static Color m_ColorPillar = Utils.RGBToColor(0, 0, 0);

	private static List<VectorLine> m_Lines;

	private static List<GameObject> m_PreviewJoints;

	private static BridgeSaveData m_BridgeSaveData;

	private static Texture2D m_Texture;

	private static int m_NumFramesUntilCapture;

	private static Vector2 m_TopLeft;

	private static Vector2 m_BottomRight;

	private static Dictionary<string, BridgeJointProxy> m_JointDictionary = new Dictionary<string, BridgeJointProxy>();

	public static void Init()
	{
		m_Lines = new List<VectorLine>();
		m_PreviewJoints = new List<GameObject>();
		m_NumFramesUntilCapture = int.MaxValue;
		RenderTexture targetTexture = new RenderTexture(m_Width, m_Height, 24, RenderTextureFormat.ARGB32);
		Cameras.BridgePreviewCamera().targetTexture = targetTexture;
		Cameras.BridgePreviewCamera().gameObject.SetActive(value: false);
		m_Texture = new Texture2D(m_Width, m_Height, TextureFormat.ARGB32, mipChain: false, linear: true);
	}

	public static void UpdateManual()
	{
		m_NumFramesUntilCapture--;
		if (m_NumFramesUntilCapture == 0)
		{
			CameraRender();
			m_NumFramesUntilCapture = int.MaxValue;
		}
	}

	public static void GeneratePreview(BridgeSaveData savefile, Image image)
	{
		m_Image = image;
		m_BridgeSaveData = savefile;
		CreatePreviewJoints();
		SetUpCameraForRender();
		CreateEdgeLines();
		CreateBridgePillars();
		CalculateBridgeCost();
		m_NumFramesUntilCapture = 2;
	}

	private static void CreatePreviewJoints()
	{
		m_JointDictionary.Clear();
		m_TopLeft = new Vector2(float.MaxValue, float.MinValue);
		m_BottomRight = new Vector2(float.MinValue, float.MaxValue);
		foreach (BridgeJointProxy bridgeJoint in m_BridgeSaveData.m_BridgeJoints)
		{
			CreatePreviewJoint(bridgeJoint);
		}
		foreach (BridgeJointProxy anchor in m_BridgeSaveData.m_Anchors)
		{
			CreatePreviewJoint(anchor);
		}
	}

	private static void CreatePreviewJoint(BridgeJointProxy joint)
	{
		GameObject gameObject = Object.Instantiate(Prefabs.m_Instance.m_PreviewJoint);
		gameObject.SetActive(value: true);
		gameObject.GetComponent<SpriteRenderer>().color = (joint.m_IsAnchor ? Color.red : Color.yellow);
		gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
		gameObject.transform.position = new Vector3(joint.m_Pos.x, joint.m_Pos.y, 4f);
		m_PreviewJoints.Add(gameObject);
		if (joint.m_Pos.x < m_TopLeft.x)
		{
			m_TopLeft.x = joint.m_Pos.x;
		}
		if (joint.m_Pos.x > m_BottomRight.x)
		{
			m_BottomRight.x = joint.m_Pos.x;
		}
		if (joint.m_Pos.y > m_TopLeft.y)
		{
			m_TopLeft.y = joint.m_Pos.y;
		}
		if (joint.m_Pos.y < m_BottomRight.y)
		{
			m_BottomRight.y = joint.m_Pos.y;
		}
		m_JointDictionary.Add(joint.m_Guid, joint);
	}

	private static void CreateEdgeLines()
	{
		VectorLine.SetCamera3D(Cameras.BridgePreviewCamera());
		foreach (BridgeEdgeProxy bridgeEdge in m_BridgeSaveData.m_BridgeEdges)
		{
			int num = 20;
			Color color = Color.black;
			switch (bridgeEdge.m_Material)
			{
			case BridgeMaterialType.ROAD:
			case BridgeMaterialType.REINFORCED_ROAD:
				num = 20;
				color = Color.black;
				break;
			case BridgeMaterialType.WOOD:
				num = 10;
				color = m_ColorWood;
				break;
			case BridgeMaterialType.STEEL:
				num = 15;
				color = m_ColorSteel;
				break;
			case BridgeMaterialType.HYDRAULICS:
				num = 10;
				color = m_ColorHydraulic;
				break;
			case BridgeMaterialType.ROPE:
				num = 7;
				color = m_ColorRope;
				break;
			case BridgeMaterialType.CABLE:
				num = 5;
				color = m_ColorCable;
				break;
			case BridgeMaterialType.SPRING:
				num = 10;
				color = m_ColorSpring;
				break;
			}
			float width = 1f / Cameras.BridgePreviewCamera().orthographicSize * 2.5f * (float)num;
			VectorLine vectorLine = new VectorLine("line", new List<Vector3>(), width, LineType.Continuous);
			vectorLine.points3.Add(PositionForAnchor(bridgeEdge.m_NodeA_Guid));
			vectorLine.points3.Add(PositionForAnchor(bridgeEdge.m_NodeB_Guid));
			vectorLine.active = true;
			vectorLine.color = color;
			vectorLine.layer = Utils.BRIDGE_PREVIEW_LAYER;
			m_Lines.Add(vectorLine);
			vectorLine.Draw3D();
		}
		VectorLine.SetCamera3D(Cameras.MainCamera());
	}

	private static void CreateBridgePillars()
	{
		VectorLine.SetCamera3D(Cameras.BridgePreviewCamera());
		foreach (BridgePillarProxy bridgePillar in m_BridgeSaveData.m_BridgePillars)
		{
			int num = 50;
			Color colorPillar = m_ColorPillar;
			float width = 1f / Cameras.BridgePreviewCamera().orthographicSize * 2.5f * (float)num;
			VectorLine vectorLine = new VectorLine("line", new List<Vector3>(), width, LineType.Continuous);
			vectorLine.points3.Add(PositionForAnchor(bridgePillar.m_AnchorGuid));
			vectorLine.points3.Add(bridgePillar.m_Pos);
			vectorLine.active = true;
			vectorLine.color = colorPillar;
			vectorLine.layer = Utils.BRIDGE_PREVIEW_LAYER;
			m_Lines.Add(vectorLine);
			vectorLine.Draw3D();
		}
		VectorLine.SetCamera3D(Cameras.MainCamera());
	}

	private static void CalculateBridgeCost()
	{
		float num = 0f;
		foreach (BridgeEdgeProxy bridgeEdge in m_BridgeSaveData.m_BridgeEdges)
		{
			float num2 = Vector3.Distance(PositionForAnchor(bridgeEdge.m_NodeA_Guid), PositionForAnchor(bridgeEdge.m_NodeB_Guid));
			BridgeMaterial bridgeMaterial = BridgeMaterials.GetBridgeMaterial(bridgeEdge.m_Material);
			if (bridgeMaterial != null)
			{
				num += bridgeMaterial.m_PricePerMeter * num2;
			}
		}
		m_BridgeCost = num;
	}

	private static void SetUpCameraForRender()
	{
		float num = 0.5625f;
		Vector2 zero = Vector2.zero;
		zero.x = m_TopLeft.x + (m_BottomRight.x - m_TopLeft.x) * 0.5f;
		zero.y = m_BottomRight.y + (m_TopLeft.y - m_BottomRight.y) * 0.5f;
		Cameras.BridgePreviewCamera().transform.position = new Vector3(zero.x, zero.y, 0f);
		if (m_TopLeft.y - m_BottomRight.y > (m_BottomRight.x - m_TopLeft.x) * num)
		{
			Cameras.BridgePreviewCamera().orthographicSize = (m_TopLeft.y - m_BottomRight.y) * 0.5f + 1f;
		}
		else
		{
			Cameras.BridgePreviewCamera().orthographicSize = (m_BottomRight.x - m_TopLeft.x) * num * 0.5f + 1f;
		}
	}

	private static Vector3 PositionForAnchor(string guid)
	{
		if (!m_JointDictionary.ContainsKey(guid))
		{
			return Vector2.zero;
		}
		BridgeJointProxy bridgeJointProxy = m_JointDictionary[guid];
		if (bridgeJointProxy == null)
		{
			return Vector2.zero;
		}
		return new Vector3(bridgeJointProxy.m_Pos.x, bridgeJointProxy.m_Pos.y, 5f);
	}

	private static void CameraRender()
	{
		Cameras.BridgePreviewCamera().gameObject.SetActive(value: true);
		Cameras.BridgePreviewCamera().Render();
		RenderTexture active = RenderTexture.active;
		RenderTexture renderTexture = (RenderTexture.active = Cameras.BridgePreviewCamera().targetTexture);
		m_Texture.ReadPixels(new Rect(0f, 0f, renderTexture.width, renderTexture.height), 0, 0);
		m_Texture.Apply();
		Sprite sprite = Sprite.Create(m_Texture, new Rect(0f, 0f, m_Width, m_Height), Vector2.zero);
		m_Image.sprite = sprite;
		m_Image.color = Color.white;
		RenderTexture.active = active;
		Cameras.BridgePreviewCamera().gameObject.SetActive(value: false);
		CleanUp();
	}

	private static void CleanUp()
	{
		for (int i = 0; i < m_PreviewJoints.Count; i++)
		{
			Object.Destroy(m_PreviewJoints[i]);
		}
		m_PreviewJoints.Clear();
		VectorLine.Destroy(m_Lines);
		m_Lines.Clear();
	}
}
