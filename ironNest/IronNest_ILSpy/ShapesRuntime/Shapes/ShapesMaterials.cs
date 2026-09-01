using System;
using Cpp2ILInjected;
using UnityEngine;

namespace Shapes;

internal class ShapesMaterials
{
	private const bool USE_INSTANCING = true;

	public const string SHAPES_SHADER_PATH_PREFIX = "Shapes/";

	private readonly Material[] materials;

	// C# has no syntax for parameterized property 'Item'.
	public Material get_Item(ShapesBlendMode type)
	{
		Material[] array = materials;
		if ((int)type < array.Length)
		{
			return array[(int)type];
		}
		return (Material)(object)new IndexOutOfRangeException();
	}

	public unsafe ShapesMaterials(string shaderName, string[] keywords)
	{
		//IL_0078: Expected O, but got I4
		//IL_0081: Expected O, but got I4
		//IL_0195: Expected O, but got Ref
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Expected O, but got Unknown
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Expected O, but got Unknown
		//IL_00b4: Expected I, but got O
		//IL_012c: Expected I, but got O
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803797D0");
		Type typeFromHandle = Type.GetTypeFromHandle((RuntimeTypeHandle)typeof(ShapesBlendMode));
		string[] names = Enum.GetNames(typeFromHandle);
		Material[] array = new Material[names.Length];
		materials = array;
		if (names.Length <= 0)
		{
			return;
		}
		object obj = 32;
		object obj2 = 0;
		string shaderName2 = shaderName;
		nint num = default(nint);
		object obj3 = default(object);
		object obj4 = default(object);
		while (true)
		{
			Material[] array2 = materials;
			string blendModeSuffix = ((Enum)(&num)).ToString();
			Material material = InitMaterial(shaderName2, blendModeSuffix, keywords);
			if ((object)material != null)
			{
				nint num2 = (nint)array2;
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A66D0");
				if (obj3 == null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FA0");
					throw obj4;
				}
			}
			obj2++;
			obj += 8;
			if ((nint)obj2 < names.Length)
			{
				num = (nint)typeof(ShapesBlendMode);
				shaderName2 = shaderName;
				continue;
			}
			break;
		}
	}

	public static string GetMaterialName(string shaderName, string blendModeSuffix, string[] keywords)
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B4267B]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		bool flag = keywords == null;
		string text = "";
		if (!flag)
		{
			bool flag2 = keywords.Length == 0;
			text = "";
			if (!flag2)
			{
				string text2 = string.Join(")(", keywords);
				text = " (" + text2 + ")";
			}
		}
		return shaderName + " " + blendModeSuffix + text;
	}

	public static void ApplyDefaultGlobalProperties(Material mat)
	{
		mat.SetInt(ShapesMaterialUtils.propZTest, 4);
		mat.SetFloat(ShapesMaterialUtils.propZOffsetFactor, 0f);
		mat.SetInt(ShapesMaterialUtils.propZOffsetUnits, 0);
		mat.SetInt(ShapesMaterialUtils.propColorMask, 15);
	}

	private static Material CreateShapesMaterial(Shader shader, HideFlags hideFlags, string[] keywords)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Expected O, but got Unknown
		//IL_0059: Expected O, but got I4
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Expected O, but got Unknown
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Expected O, but got Unknown
		Material material = new Material(shader);
		material.hideFlags = hideFlags;
		material.enableInstancing = true;
		if (keywords != null)
		{
			object obj = keywords + 32;
			object obj2 = 0;
			while ((nint)obj2 < keywords.Length)
			{
				if ((nint)obj2 < keywords.Length)
				{
					material.EnableKeyword((string)obj);
					obj2++;
					obj += 8;
					continue;
				}
				return (Material)(object)new IndexOutOfRangeException();
			}
		}
		material.SetInt(ShapesMaterialUtils.propZTest, 4);
		material.SetFloat(ShapesMaterialUtils.propZOffsetFactor, 0f);
		material.SetInt(ShapesMaterialUtils.propZOffsetUnits, 0);
		material.SetInt(ShapesMaterialUtils.propColorMask, 15);
		return material;
	}

	private static Material InitMaterial(string shaderName, string blendModeSuffix, string[] keywords)
	{
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Expected O, but got Unknown
		//IL_007b: Expected O, but got I4
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Expected O, but got Unknown
		string text = "Shapes/" + shaderName + " " + blendModeSuffix;
		Shader shader = Shader.Find(text);
		Material material;
		if (shader != null)
		{
			material = new Material(shader);
			if ((object)material == null)
			{
				return (Material)(object)new NullReferenceException();
			}
			material.hideFlags = HideFlags.HideAndDontSave;
			material.enableInstancing = true;
			if (keywords != null)
			{
				object obj = keywords + 32;
				object obj2 = 0;
				while ((nint)obj2 < keywords.Length)
				{
					material.EnableKeyword((string)obj);
					obj2++;
					obj += 8;
				}
			}
			material.SetInt(ShapesMaterialUtils.propZTest, 4);
			material.SetFloat(ShapesMaterialUtils.propZOffsetFactor, 0f);
			material.SetInt(ShapesMaterialUtils.propZOffsetUnits, 0);
			material.SetInt(ShapesMaterialUtils.propColorMask, 15);
		}
		else
		{
			string message = "Could not find shader " + text;
			Debug.LogError(message);
			material = null;
		}
		return material;
	}
}
