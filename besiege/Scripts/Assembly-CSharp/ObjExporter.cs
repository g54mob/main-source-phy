using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BesiegeDlc;
using UnityEngine;

public class ObjExporter : MonoBehaviour
{
	public static bool applyPosition = true;

	public static bool applyRotation = true;

	public static bool applyScale = true;

	public static bool splitObjects = true;

	public static bool duplicateTextures;

	public static string currentExportFolder = string.Empty;

	private static Vector3 RotateAroundPoint(Vector3 point, Vector3 pivot, Quaternion angle)
	{
		return angle * (point - pivot) + pivot;
	}

	public static string GetTimestamp(DateTime value)
	{
		return value.ToString("yyyyMMddHHmmss");
	}

	public static void Export(IEnumerable<BlockBehaviour> selection, string name, bool generateMaterials = false)
	{
		Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
		string text = name.Replace(" ", string.Empty);
		string text2 = StaticSettings.DataPath + "/ExportedMachines/";
		if (!Directory.Exists(text2))
		{
			Directory.CreateDirectory(text2);
		}
		currentExportFolder = text2 + GetTimestamp(DateTime.Now) + "/";
		Directory.CreateDirectory(currentExportFolder);
		string text3 = currentExportFolder + text + ".obj";
		FileInfo fileInfo = new FileInfo(text3);
		List<Transform> list = new List<Transform>();
		List<Mesh> list2 = new List<Mesh>();
		List<string> list3 = new List<string>();
		List<BlockSkinLoader.SkinPack.Skin> list4 = new List<BlockSkinLoader.SkinPack.Skin>();
		int num = 0;
		foreach (BlockBehaviour item in selection)
		{
			Vector3 lossyScale = item.transform.lossyScale;
			int num2 = 3;
			if (lossyScale.x < 0.0001f)
			{
				num2--;
				if (lossyScale.x < 0f)
				{
					num2--;
				}
			}
			if (lossyScale.y < 0.0001f)
			{
				num2--;
				if (lossyScale.y < 0f)
				{
					num2--;
				}
			}
			if (lossyScale.z < 0.0001f)
			{
				num2--;
				if (lossyScale.z < 0f)
				{
					num2--;
				}
			}
			if (num2 < 2)
			{
				continue;
			}
			BlockType type = item.Prefab.Type;
			switch (type)
			{
			case BlockType.DoubleWoodenBlock:
			case BlockType.WoodenPole:
			case BlockType.Log:
			{
				ShorteningBlock shorteningBlock = item as ShorteningBlock;
				Renderer renderer = item.MeshRenderer;
				if (shorteningBlock.halfVis.enabled)
				{
					renderer = shorteningBlock.halfVis;
				}
				MeshFilter component = renderer.GetComponent<MeshFilter>();
				if (component != null)
				{
					list.Add(component.transform);
					list2.Add(component.sharedMesh);
					list3.Add(type.ToString() + "_" + num + "_" + component.name);
					list4.Add(item.VisualController.selectedSkin);
				}
				num++;
				continue;
			}
			case BlockType.Brace:
			case BlockType.Spring:
			case BlockType.RopeWinch:
			case BlockType.RopeMeasure:
			{
				BlockSkinLoader.SkinPack.Skin selectedSkin = item.VisualController.selectedSkin;
				if (!(selectedSkin.pack.id == "3dprint"))
				{
					break;
				}
				Transform[] array = new Transform[3];
				Mesh[] array2 = new Mesh[3];
				GenericDraggedBlock genericDraggedBlock = item as GenericDraggedBlock;
				array[0] = genericDraggedBlock.cylinder.GetComponentInChildren<MeshRenderer>().transform;
				array[1] = genericDraggedBlock.startVis.transform;
				array[2] = genericDraggedBlock.endVis.transform;
				array2[0] = selectedSkin.mesh;
				array2[1] = selectedSkin.shortSkin.mesh;
				array2[2] = selectedSkin.shortSkin.mesh;
				for (int i = 0; i < array.Length; i++)
				{
					MeshFilter componentInChildren = array[i].GetComponentInChildren<MeshFilter>();
					if (componentInChildren != null)
					{
						if (array2[i] == null)
						{
							array2[i] = componentInChildren.sharedMesh;
						}
						list.Add(componentInChildren.transform);
						list2.Add(array2[i]);
						list3.Add(type.ToString() + "_" + num + "_" + componentInChildren.name);
						list4.Add(item.VisualController.selectedSkin);
					}
				}
				continue;
			}
			case BlockType.Crossbow:
			case BlockType.Timer:
			case BlockType.Altimeter:
			case BlockType.LogicGate:
			case BlockType.Speedometer:
			{
				MeshFilter component2 = item.MeshRenderer.GetComponent<MeshFilter>();
				if (component2 != null)
				{
					list.Add(component2.transform);
					list2.Add(component2.sharedMesh);
					list3.Add(type.ToString() + "_" + num + "_" + component2.name);
					list4.Add(item.VisualController.selectedSkin);
				}
				num++;
				continue;
			}
			default:
			{
				DlcManager.DlcType dlcType;
				if (DlcManager.Instance.GetBlockDlcType(item.Prefab.Type, out dlcType) && dlcType != 0 && !DlcManager.Instance.HasPurchasedDlc(dlcType))
				{
					continue;
				}
				break;
			}
			case BlockType.Balloon:
			case BlockType.Torch:
			case BlockType.Anglometer:
				break;
			case BlockType.Pin:
			case BlockType.CameraBlock:
			case BlockType.BuildNode:
			case BlockType.BuildEdge:
				continue;
			}
			MeshRenderer[] renderers = item.VisualController.renderers;
			for (int j = 0; j < renderers.Length; j++)
			{
				if (!(renderers[j].name == "DirectionArrow"))
				{
					MeshFilter component3 = renderers[j].GetComponent<MeshFilter>();
					if (component3 != null)
					{
						list.Add(component3.transform);
						list2.Add(component3.sharedMesh);
						list3.Add(type.ToString() + "_" + num + "_" + component3.name);
						list4.Add(item.VisualController.selectedSkin);
					}
				}
			}
			num++;
		}
		StringBuilder stringBuilder = new StringBuilder();
		StringBuilder stringBuilder2 = new StringBuilder();
		stringBuilder.AppendLine("# Export of " + name);
		if (generateMaterials)
		{
			stringBuilder.AppendLine("mtllib " + text + ".mtl");
		}
		int num3 = 0;
		for (int k = 0; k < list2.Count; k++)
		{
			string text4 = list3[k];
			Mesh mesh = list2[k];
			Transform transform = list[k];
			MeshRenderer component4 = transform.GetComponent<MeshRenderer>();
			if (splitObjects)
			{
				string text5 = text4;
				stringBuilder.AppendLine("g " + text5);
			}
			if (generateMaterials && component4 != null)
			{
				Material[] sharedMaterials = component4.sharedMaterials;
				foreach (Material material in sharedMaterials)
				{
					if (!dictionary.ContainsKey(material.name))
					{
						dictionary[material.name] = true;
						stringBuilder2.Append(MaterialToString(material, list4[k], true));
						stringBuilder2.AppendLine();
					}
				}
			}
			int num4 = (int)Mathf.Clamp(transform.lossyScale.x * transform.lossyScale.z, -1f, 1f);
			if (mesh == null)
			{
				Debug.LogError("[ObjExporter] couldn't export " + text4);
				continue;
			}
			if (!mesh.isReadable)
			{
				Debug.LogError("[ObjExporter] couldn't export " + text4);
				continue;
			}
			Vector3[] vertices = mesh.vertices;
			foreach (Vector3 vector in vertices)
			{
				Vector3 vector2 = vector;
				if (applyScale)
				{
					vector2 = Vector3.Scale(vector2, transform.lossyScale);
				}
				if (applyRotation)
				{
					vector2 = RotateAroundPoint(vector2, Vector3.zero, transform.rotation);
				}
				if (applyPosition)
				{
					vector2 += transform.position;
				}
				vector2.x *= -1f;
				stringBuilder.AppendLine("v " + vector2.x + " " + vector2.y + " " + vector2.z);
			}
			Vector3[] normals = mesh.normals;
			foreach (Vector3 vector3 in normals)
			{
				Vector3 vector4 = vector3;
				if (applyScale)
				{
					vector4 = Vector3.Scale(vector4, transform.lossyScale.normalized);
				}
				if (applyRotation)
				{
					vector4 = RotateAroundPoint(vector4, Vector3.zero, transform.rotation);
				}
				vector4.x *= -1f;
				stringBuilder.AppendLine("vn " + vector4.x + " " + vector4.y + " " + vector4.z);
			}
			Vector2[] uv = mesh.uv;
			for (int num5 = 0; num5 < uv.Length; num5++)
			{
				Vector2 vector5 = uv[num5];
				stringBuilder.AppendLine("vt " + vector5.x + " " + vector5.y);
			}
			for (int num6 = 0; num6 < mesh.subMeshCount; num6++)
			{
				if (component4 != null && num6 < component4.sharedMaterials.Length)
				{
					string text6 = component4.sharedMaterials[num6].name;
					stringBuilder.AppendLine("usemtl " + text6);
				}
				else
				{
					stringBuilder.AppendLine("usemtl " + text4 + "_sm" + num6);
				}
				int[] triangles = mesh.GetTriangles(num6);
				for (int num7 = 0; num7 < triangles.Length; num7 += 3)
				{
					int index = triangles[num7] + 1 + num3;
					int index2 = triangles[num7 + 1] + 1 + num3;
					int index3 = triangles[num7 + 2] + 1 + num3;
					if (num4 < 0)
					{
						stringBuilder.AppendLine("f " + ConstructOBJString(index) + " " + ConstructOBJString(index2) + " " + ConstructOBJString(index3));
					}
					else
					{
						stringBuilder.AppendLine("f " + ConstructOBJString(index3) + " " + ConstructOBJString(index2) + " " + ConstructOBJString(index));
					}
				}
			}
			num3 += mesh.vertices.Length;
		}
		File.WriteAllText(text3, stringBuilder.ToString());
		if (generateMaterials)
		{
			File.WriteAllText(fileInfo.Directory.FullName + "\\" + text + ".mtl", stringBuilder2.ToString());
		}
	}

	private static string ConstructOBJString(int index)
	{
		string text = index.ToString();
		return text + "/" + text + "/" + text;
	}

	private static string CopyTextureToExportLocation(BlockSkinLoader.SkinPack.Skin s, string path)
	{
		string text = currentExportFolder + s.prefab.Type.ToString() + ".png";
		if (!File.Exists(text))
		{
			File.Copy(path, text);
		}
		return text;
	}

	private static string MaterialToString(Material m, BlockSkinLoader.SkinPack.Skin skin, bool exportTextures)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("newmtl " + m.name);
		if (m.HasProperty("_Color"))
		{
			stringBuilder.AppendLine("Kd " + m.color.r + " " + m.color.g + " " + m.color.b);
			if (m.color.a < 1f)
			{
				stringBuilder.AppendLine("Tr " + (1f - m.color.a));
				stringBuilder.AppendLine("d " + m.color.a);
			}
		}
		if (m.HasProperty("_SpecColor"))
		{
			Color color = m.GetColor("_SpecColor");
			color *= 0.5f;
			stringBuilder.AppendLine("Ks " + color.r + " " + color.g + " " + color.b);
		}
		else
		{
			stringBuilder.AppendLine("Ks 0.02 0.02 0.02");
		}
		if (exportTextures)
		{
			string texPath = skin.texPath;
			if (string.IsNullOrEmpty(texPath))
			{
				texPath = skin.GetDefaultOrShortDefault().texPath;
			}
			if (string.IsNullOrEmpty(texPath))
			{
				Debug.LogError("tried to export empty texture " + skin.pack.name);
			}
			if (duplicateTextures)
			{
				stringBuilder.AppendLine("map_Kd " + CopyTextureToExportLocation(skin, texPath));
			}
			else
			{
				stringBuilder.AppendLine("map_Kd " + texPath);
			}
		}
		stringBuilder.AppendLine("illum 2");
		return stringBuilder.ToString();
	}

	public static void Export(IEnumerable<MeshFilter> selection, string name)
	{
		Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
		string text = name.Replace(" ", string.Empty) + "_" + GetTimestamp(DateTime.Now);
		string path = StaticSettings.DataPath + "/MODELS/Export/";
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
		currentExportFolder = path;
		string text2 = currentExportFolder + text + ".obj";
		FileInfo fileInfo = new FileInfo(text2);
		List<Transform> list = new List<Transform>();
		List<Mesh> list2 = new List<Mesh>();
		List<string> list3 = new List<string>();
		int num = 0;
		foreach (MeshFilter item in selection)
		{
			if (item != null)
			{
				list.Add(item.transform);
				list2.Add(item.sharedMesh);
				list3.Add(num + "_" + item.name);
			}
			num++;
		}
		StringBuilder stringBuilder = new StringBuilder();
		StringBuilder stringBuilder2 = new StringBuilder();
		stringBuilder.AppendLine("# Export of " + name);
		int num2 = 0;
		for (int i = 0; i < list2.Count; i++)
		{
			string text3 = list3[i];
			Mesh mesh = list2[i];
			Transform transform = list[i];
			MeshRenderer component = transform.GetComponent<MeshRenderer>();
			if (splitObjects)
			{
				string text4 = text3;
				stringBuilder.AppendLine("g " + text4);
			}
			if (component != null)
			{
				Material[] sharedMaterials = component.sharedMaterials;
				foreach (Material material in sharedMaterials)
				{
					if (!dictionary.ContainsKey(material.name))
					{
						dictionary[material.name] = true;
						stringBuilder2.Append(MaterialToString(material));
						stringBuilder2.AppendLine();
					}
				}
			}
			int num3 = (int)Mathf.Clamp(transform.lossyScale.x * transform.lossyScale.z, -1f, 1f);
			if (!mesh.isReadable)
			{
				Debug.LogError("[ObjExporter] couldn't export " + text3);
				continue;
			}
			Vector3[] vertices = mesh.vertices;
			foreach (Vector3 vector in vertices)
			{
				Vector3 a = vector;
				Transform transform2 = transform;
				do
				{
					a = Vector3.Scale(a, transform2.localScale);
					a = RotateAroundPoint(a, Vector3.zero, transform2.localRotation);
					a += transform2.localPosition;
					transform2 = transform2.parent;
				}
				while (transform2 != null);
				a.x *= -1f;
				stringBuilder.AppendLine("v " + a.x + " " + a.y + " " + a.z);
			}
			Vector3[] normals = mesh.normals;
			foreach (Vector3 vector2 in normals)
			{
				Vector3 a2 = vector2;
				Transform transform3 = transform;
				do
				{
					a2 = Vector3.Scale(a2, transform3.localScale.normalized);
					a2 = RotateAroundPoint(a2, Vector3.zero, transform3.localRotation);
					transform3 = transform3.parent;
				}
				while (transform3 != null);
				a2.x *= -1f;
				stringBuilder.AppendLine("vn " + a2.x + " " + a2.y + " " + a2.z);
			}
			Vector2[] uv = mesh.uv;
			for (int m = 0; m < uv.Length; m++)
			{
				Vector2 vector3 = uv[m];
				stringBuilder.AppendLine("vt " + vector3.x + " " + vector3.y);
			}
			for (int n = 0; n < mesh.subMeshCount; n++)
			{
				if (component != null && n < component.sharedMaterials.Length)
				{
					string text5 = component.sharedMaterials[n].name;
					stringBuilder.AppendLine("usemtl " + text5);
				}
				else
				{
					stringBuilder.AppendLine("usemtl " + text3 + "_sm" + n);
				}
				int[] triangles = mesh.GetTriangles(n);
				for (int num4 = 0; num4 < triangles.Length; num4 += 3)
				{
					int index = triangles[num4] + 1 + num2;
					int index2 = triangles[num4 + 1] + 1 + num2;
					int index3 = triangles[num4 + 2] + 1 + num2;
					if (num3 < 0)
					{
						stringBuilder.AppendLine("f " + ConstructOBJString(index) + " " + ConstructOBJString(index2) + " " + ConstructOBJString(index3));
					}
					else
					{
						stringBuilder.AppendLine("f " + ConstructOBJString(index3) + " " + ConstructOBJString(index2) + " " + ConstructOBJString(index));
					}
				}
			}
			num2 += mesh.vertices.Length;
		}
		File.WriteAllText(text2, stringBuilder.ToString());
		File.WriteAllText(fileInfo.Directory.FullName + "\\" + text + ".mtl", stringBuilder2.ToString());
	}

	private static string MaterialToString(Material m)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine("newmtl " + m.name);
		if (m.HasProperty("_Color"))
		{
			stringBuilder.AppendLine("Kd " + m.color.r + " " + m.color.g + " " + m.color.b);
			if (m.color.a < 1f)
			{
				stringBuilder.AppendLine("Tr " + (1f - m.color.a));
				stringBuilder.AppendLine("d " + m.color.a);
			}
		}
		if (m.HasProperty("_SpecColor"))
		{
			Color color = m.GetColor("_SpecColor");
			color *= 0.5f;
			stringBuilder.AppendLine("Ks " + color.r + " " + color.g + " " + color.b);
		}
		else
		{
			stringBuilder.AppendLine("Ks 0.02 0.02 0.02");
		}
		stringBuilder.AppendLine("illum 2");
		return stringBuilder.ToString();
	}
}
