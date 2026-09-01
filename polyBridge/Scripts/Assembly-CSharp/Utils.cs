using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using I2.Loc;
using Poly.Collide;
using Poly.Math;
using UnityEngine;
using UnityEngine.UI;

public class Utils
{
	public static int DEFAULT_LAYER = 0;

	public static int TRANSPARENT_FX_LAYER = 1;

	public static int WATER_LAYER = 4;

	public static int UI_LAYER = 5;

	public static int SPRING_LAYER = 8;

	public static int JOINT_LAYER = 9;

	public static int EDGE_LAYER = 10;

	public static int VEHICLE_LAYER = 11;

	public static int SCENEGEO_LAYER = 12;

	public static int SCENEGEOSTATIC_LAYER = 13;

	public static int JOINT_HOTSPOT_LAYER = 14;

	public static int NO_RENDER_LAYER = 15;

	public static int SKY_LAYER = 16;

	public static int SANDBOX_SELECT_LAYER = 17;

	public static int SPLINE_CONTROL_POINT_LAYER = 18;

	public static int PISTON_LAYER = 19;

	public static int JOINT_SELECTOR_LAYER = 20;

	public static int FOREGROUND_LAYER = 21;

	public static int TERRAIN_LAYER = 22;

	public static int RENDER_LAST_LAYER = 23;

	public static int SPLIT_JOINT_NUMBER_LAYER = 24;

	public static int CUSTOM_SHAPE_LAYER = 25;

	public static int BRIDGE_PILLAR_LAYER = 26;

	public static int BUILD_ZONE_LAYER = 27;

	public static int BRIDGE_PREVIEW_LAYER = 28;

	public static int OUTLINE_LAYER = 29;

	public static int PICKUP_BY_VEHICLE_LAYER = 30;

	public static int DECOR_LAYER = 31;

	public static int DEFAULT_LAYER_MASK = 1 << DEFAULT_LAYER;

	public static int TRANSPARENT_FX_LAYER_MASK = 1 << TRANSPARENT_FX_LAYER;

	public static int WATER_LAYER_MASK = 1 << WATER_LAYER;

	public static int UI_LAYER_MASK = 1 << UI_LAYER;

	public static int SPRING_LAYER_MASK = 1 << SPRING_LAYER;

	public static int JOINT_LAYER_MASK = 1 << JOINT_LAYER;

	public static int EDGE_LAYER_MASK = 1 << EDGE_LAYER;

	public static int VEHICLE_LAYER_MASK = 1 << VEHICLE_LAYER;

	public static int SCENEGEO_LAYER_MASK = 1 << SCENEGEO_LAYER;

	public static int SCENEGEOSTATIC_LAYER_MASK = 1 << SCENEGEOSTATIC_LAYER;

	public static int JOINT_HOTSPOT_LAYER_MASK = 1 << JOINT_HOTSPOT_LAYER;

	public static int NO_RENDER_LAYER_MASK = 1 << NO_RENDER_LAYER;

	public static int SKY_LAYER_MASK = 1 << SKY_LAYER;

	public static int SANDBOX_SELECT_MASK = 1 << SANDBOX_SELECT_LAYER;

	public static int SPLINE_CONTROL_POINT_MASK = 1 << SPLINE_CONTROL_POINT_LAYER;

	public static int PISTON_LAYER_MASK = 1 << PISTON_LAYER;

	public static int JOINT_SELECTOR_LAYER_MASK = 1 << JOINT_SELECTOR_LAYER;

	public static int FOREGROUND_LAYER_MASK = 1 << FOREGROUND_LAYER;

	public static int TERRAIN_LAYER_MASK = 1 << TERRAIN_LAYER;

	public static int RENDER_LAST_LAYER_MASK = 1 << RENDER_LAST_LAYER;

	public static int SPLIT_JOINT_NUMBER_LAYER_MASK = 1 << SPLIT_JOINT_NUMBER_LAYER;

	public static int CUSTOM_SHAPE_LAYER_MASK = 1 << CUSTOM_SHAPE_LAYER;

	public static int BUILD_ZONE_LAYER_MASK = 1 << BUILD_ZONE_LAYER;

	public static int BRIDGE_PILLAR_LAYER_MASK = 1 << BRIDGE_PILLAR_LAYER;

	public static int BRIDGE_PREVIEW_LAYER_MASK = 1 << BRIDGE_PREVIEW_LAYER;

	public static int OUTLINE_LAYER_MASK = 1 << OUTLINE_LAYER;

	public static int PICKUP_BY_VEHICLE_LAYER_MASK = 1 << PICKUP_BY_VEHICLE_LAYER;

	public static int DECOR_LAYER_MASK = 1 << DECOR_LAYER;

	public static readonly int MAX_RAYCAST_HITS = 32;

	public static RaycastHit[] m_RaycastHits = new RaycastHit[MAX_RAYCAST_HITS];

	private static Dictionary<string, string> m_LastWrittenChecksums = new Dictionary<string, string>();

	public static void SetParent(GameObject go, GameObject parent)
	{
		Vector3 localScale = go.transform.localScale;
		go.transform.SetParent(parent.transform);
		go.transform.localScale = localScale;
	}

	public static int GetActiveImmediateChildren(GameObject go)
	{
		int num = 0;
		for (int i = 0; i < go.transform.childCount; i++)
		{
			if (go.transform.GetChild(i).gameObject.activeInHierarchy)
			{
				num++;
			}
		}
		return num;
	}

	public static bool PolygonShapeOverlapsShapes(PolygonShape testShape, List<PolygonShape> shapes)
	{
		foreach (PolygonShape shape in shapes)
		{
			if (PolygonShapeOverlapsShape(testShape, shape))
			{
				return true;
			}
		}
		return false;
	}

	public static bool PolygonShapeOverlapsShape(PolygonShape testShape, PolygonShape shape)
	{
		PolygonCollisionProcess.Init(ref testShape, ref Transform2.identity, ref shape, ref Transform2.identity, out var process);
		PolygonIntersection.CalcClosestPoint(ref process, out var closestPoint, doAveragePointPositions: false);
		return closestPoint.distance < 0f;
	}

	public static bool RectOverlaps(RectTransform rectTrans1, RectTransform rectTrans2)
	{
		Rect rect = RectFromRectTransform(rectTrans1);
		Rect other = RectFromRectTransform(rectTrans2);
		return rect.Overlaps(other);
	}

	public static Rect RectFromRectTransform(RectTransform rectTransform)
	{
		Vector2 sizeDelta = rectTransform.sizeDelta;
		float num = sizeDelta.x * rectTransform.lossyScale.x;
		float num2 = sizeDelta.y * rectTransform.lossyScale.y;
		Vector3 position = rectTransform.position;
		return new Rect(position.x - num / 2f, position.y - num2 / 2f, num, num2);
	}

	public static void SetLayerRecursively(GameObject go, int layerNumber)
	{
		if (!(go == null))
		{
			Transform[] componentsInChildren = go.GetComponentsInChildren<Transform>(includeInactive: true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].gameObject.layer = layerNumber;
			}
		}
	}

	public static void ReplaceLayerRecursively(GameObject go, int oldLayerNumber, int newLayerNumber)
	{
		if (go == null)
		{
			return;
		}
		Transform[] componentsInChildren = go.GetComponentsInChildren<Transform>(includeInactive: true);
		foreach (Transform transform in componentsInChildren)
		{
			if (transform.gameObject.layer == oldLayerNumber)
			{
				transform.gameObject.layer = newLayerNumber;
			}
		}
	}

	public static Vector3 GetWorldPointFromScreenPos(Vector2 screenPos)
	{
		Vector3 result = Cameras.MainCamera().ScreenToWorldPoint(screenPos);
		if (Game.InDecorModeTopView())
		{
			result.y = TerrainIslands.GetMaxHeight();
		}
		else
		{
			result.z = 0f;
		}
		return result;
	}

	public static float ApproximateFloat(float val, int precision = 100)
	{
		return (float)Mathf.RoundToInt(val * (float)precision) * (1f / (float)precision);
	}

	public static Vector2 ApproximateV2(Vector2 val, int precision = 100)
	{
		return new Vector2(ApproximateFloat(val.x, precision), ApproximateFloat(val.y, precision));
	}

	public static Vector2 V3toV2(Vector3 v3)
	{
		return new Vector2(v3.x, v3.y);
	}

	public static bool FloatLessOrEqualThan(float a, float b)
	{
		return Mathf.RoundToInt(a * 100f) <= Mathf.RoundToInt(b * 100f);
	}

	public static bool FloatLessThan(float a, float b)
	{
		return Mathf.RoundToInt(a * 100f) < Mathf.RoundToInt(b * 100f);
	}

	public static bool FloatGreaterOrEqualThan(float a, float b)
	{
		return Mathf.RoundToInt(a * 100f) >= Mathf.RoundToInt(b * 100f);
	}

	public static bool FloatGreaterThan(float a, float b)
	{
		return Mathf.RoundToInt(a * 100f) > Mathf.RoundToInt(b * 100f);
	}

	public static bool ApproximatelyEquals(float a, float b, int precision = 100)
	{
		return Mathf.RoundToInt(a * (float)precision) == Mathf.RoundToInt(b * (float)precision);
	}

	public static bool ApproximatelyEquals(Vector2 a, Vector2 b, int precision = 100)
	{
		if (ApproximatelyEquals(a.x, b.x, precision))
		{
			return ApproximatelyEquals(a.y, b.y, precision);
		}
		return false;
	}

	public static bool ApproximatelyEquals(Vector3 a, Vector3 b, int precision = 100)
	{
		if (ApproximatelyEquals(a.x, b.x, precision) && ApproximatelyEquals(a.y, b.y, precision))
		{
			return ApproximatelyEquals(a.z, b.z, precision);
		}
		return false;
	}

	public static Vector3 V2toV3(Vector2 v2)
	{
		return new Vector3(v2.x, v2.y, 0f);
	}

	public static float RoundToNearestMultipleOf(float value, float n)
	{
		return (float)Mathf.RoundToInt(value / n) * n;
	}

	public static string IntToLetters(int value)
	{
		string text = string.Empty;
		while (--value >= 0)
		{
			text = (char)(65 + value % 26) + text;
			value /= 26;
		}
		return text;
	}

	public static float LowResSin(float x)
	{
		if (x < -MathF.PI)
		{
			x += MathF.PI * 2f;
		}
		else if (x > MathF.PI)
		{
			x -= MathF.PI * 2f;
		}
		if (x < 0f)
		{
			return x * (4f / MathF.PI + 0.40528473f * x);
		}
		return x * (4f / MathF.PI - 0.40528473f * x);
	}

	public static void SetLayerOnAllRecursive(GameObject obj, int layer)
	{
		obj.layer = layer;
		foreach (Transform item in obj.transform)
		{
			SetLayerOnAllRecursive(item.gameObject, layer);
		}
	}

	public static Color RGBToColor(int r, int g, int b)
	{
		return new Color((float)r / 255f, (float)g / 255f, (float)b / 255f);
	}

	public static string ColorToHex(Color color)
	{
		return $"<#{ColorUtility.ToHtmlStringRGB(color)}>";
	}

	public static GameObject FindChildWithName(GameObject go, string tag, bool recursive = true)
	{
		List<GameObject> list = FindChildrenWithName(go, tag, recursive);
		if (list.Count > 0)
		{
			return list[0];
		}
		return null;
	}

	public static List<GameObject> FindChildrenWithName(GameObject go, string tag, bool recursive = true)
	{
		List<GameObject> list = new List<GameObject>();
		Transform[] componentsInChildren = go.GetComponentsInChildren<Transform>(includeInactive: true);
		foreach (Transform transform in componentsInChildren)
		{
			if (transform.gameObject.name.Contains(tag))
			{
				list.Add(transform.gameObject);
			}
		}
		return list;
	}

	public static GameObject FindChildWithTag(GameObject go, string tag, bool recursive = true)
	{
		List<GameObject> list = FindChildrenWithTag(go, tag, recursive);
		if (list.Count > 0)
		{
			return list[0];
		}
		return null;
	}

	public static List<GameObject> FindChildrenWithTag(GameObject go, string tag, bool recursive = true)
	{
		List<GameObject> list = new List<GameObject>();
		Transform[] componentsInChildren = go.GetComponentsInChildren<Transform>(includeInactive: true);
		foreach (Transform transform in componentsInChildren)
		{
			if (transform.gameObject.tag == tag)
			{
				list.Add(transform.gameObject);
			}
		}
		return list;
	}

	public static string CleanInput(string strIn)
	{
		return Regex.Replace(strIn, "[^\\w\\-]", "", RegexOptions.None);
	}

	public static string GenerateRandomString(int length = 8)
	{
		string text = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
		char[] array = new char[length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = text[UnityEngine.Random.Range(0, text.Length)];
		}
		return new string(array);
	}

	public static Vector3 CastRayToPlane(Ray ray)
	{
		float num = ray.origin.z / ray.direction.z;
		return ray.origin - ray.direction * num;
	}

	public static string FormatInteger(float p)
	{
		return Mathf.RoundToInt(p).ToString();
	}

	public static string FormatOneDecimalPlace(float p)
	{
		return p.ToString("0.0");
	}

	public static string FormatOneDecimalPlace_Floor(float p)
	{
		p = 0.1f * Mathf.Floor(10f * p);
		return p.ToString("0.0");
	}

	public static string FormatTwoDecimalPlaces_Floor(float p)
	{
		p = 0.01f * Mathf.Floor(100f * p);
		return p.ToString("0.00");
	}

	public static string FormatTwoDecimalPlaces(float p)
	{
		return p.ToString("0.00");
	}

	public static string FormatThreeDecimalPlaces(float p)
	{
		return p.ToString("0.000");
	}

	public static string FormatDistance(float d)
	{
		return d.ToString("0.00 m");
	}

	public static string FormatDistanceOneDecimalPlace(float p)
	{
		return p.ToString("0.0 m");
	}

	public static string FormatSpeed(float s)
	{
		return s.ToString("0.0 m/s");
	}

	public static string FormatAcceleration(float s)
	{
		return s.ToString("0.0 m/s²");
	}

	public static string FormatWeight(float w)
	{
		return w.ToString("0.0 Pg");
	}

	public static string FormatMass(float w)
	{
		return w.ToString("0.00 Pg");
	}

	public static string FormatIntegerMass(float w)
	{
		return FormatInteger(w) + " Pg";
	}

	public static string FormatAngle(float a)
	{
		return FormatOneDecimalPlace(a) + "º";
	}

	public static string FormatSeconds(float t)
	{
		return t.ToString("0.0 s");
	}

	public static string FormatCash(int cash)
	{
		if (cash < 0)
		{
			return string.Format(CultureInfo.InvariantCulture, "-${0:n0}", Mathf.Abs(cash));
		}
		if (cash > Budget.MAX_CASH_BUDGET)
		{
			return Localize.Get("UI_UNLIMITED");
		}
		return string.Format(CultureInfo.InvariantCulture, "${0:n0}", cash);
	}

	public static string FormatCashNoDollarSign(int cash)
	{
		if (cash < 0)
		{
			return string.Format(CultureInfo.InvariantCulture, "-{0:n0}", Mathf.Abs(cash));
		}
		if (cash > Budget.MAX_CASH_BUDGET)
		{
			return Localize.Get("UI_UNLIMITED");
		}
		return string.Format(CultureInfo.InvariantCulture, "{0:n0}", cash);
	}

	public static string FormatMaterialBudget(int limit)
	{
		if (limit < 0 || limit > Budget.MAX_MATERIAL_BUDGET)
		{
			return Localize.Get("UI_UNLIMITED");
		}
		return limit.ToString();
	}

	public static string FormatPercentage(float normalized)
	{
		return Mathf.RoundToInt(normalized * 100f) + "%";
	}

	public static string FormatPercentageToOneDecimalPlace(float normalized)
	{
		return (normalized * 100f).ToString("0.0") + "%";
	}

	public static string FormatPercentageToTwoDecimalPlaces(float normalized)
	{
		return (normalized * 100f).ToString("0.00") + "%";
	}

	public static string FormatVariant(int variantIndex, int numVariants)
	{
		return string.Format(Localize.Get("UI_SANDBOX_STYLE"), variantIndex + 1, numVariants);
	}

	public static string FormatIntegerWithCommas(int value)
	{
		return $"{value:n0}";
	}

	public static string FormatStress(float stress)
	{
		return FormatTwoDecimalPlaces(stress) + "%";
	}

	public static string FormatShortDate(DateTime dateTime)
	{
		if (LocalizationManager.CurrentLanguageCode == "en")
		{
			return dateTime.ToLocalTime().ToString("MMM d, yyyy") ?? "";
		}
		if (LocalizationManager.CurrentLanguageCode == "ja" || LocalizationManager.CurrentLanguageCode == "ko" || LocalizationManager.CurrentLanguageCode == "zh-CN" || LocalizationManager.CurrentLanguageCode == "zh-TW")
		{
			return dateTime.ToLocalTime().ToString("yyyy-MM-dd") ?? "";
		}
		return dateTime.ToLocalTime().ToString("dd/MM/yyyy") ?? "";
	}

	public static string FormatPercentileAsTopBottom(int percentile)
	{
		if (percentile >= 50)
		{
			int num = Mathf.Clamp(100 - percentile, 1, 50);
			return string.Format(Localize.Get("UI_TOP_PERCENT"), num);
		}
		int num2 = Mathf.Clamp(percentile, 1, 50);
		return string.Format(Localize.Get("UI_BOTTOM_PERCENT"), num2);
	}

	public static bool HasInvalidFileNameChars(string filename)
	{
		if (string.IsNullOrEmpty(filename))
		{
			return true;
		}
		return filename.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0;
	}

	public static bool HasInvalidPathChars(string directoryName)
	{
		if (string.IsNullOrEmpty(directoryName))
		{
			return true;
		}
		return directoryName.IndexOfAny(Path.GetInvalidPathChars()) >= 0;
	}

	public static string RemoveInvalidCharsFromFilename(string filename)
	{
		filename = filename.Replace("<", string.Empty);
		filename = filename.Replace(">", string.Empty);
		filename = filename.Replace(":", string.Empty);
		filename = filename.Replace("\"", string.Empty);
		filename = filename.Replace("/", string.Empty);
		filename = filename.Replace("\\", string.Empty);
		filename = filename.Replace("|", string.Empty);
		filename = filename.Replace("?", string.Empty);
		filename = filename.Replace("*", string.Empty);
		return filename;
	}

	public static string RemoveInvalidCharsFromPath(string directory)
	{
		directory = directory.Replace("<", string.Empty);
		directory = directory.Replace(">", string.Empty);
		directory = directory.Replace(":", string.Empty);
		directory = directory.Replace(";", string.Empty);
		directory = directory.Replace("=", string.Empty);
		directory = directory.Replace(",", string.Empty);
		directory = directory.Replace(".", string.Empty);
		directory = directory.Replace("|", string.Empty);
		directory = directory.Replace("?", string.Empty);
		directory = directory.Replace("*", string.Empty);
		directory = directory.Replace("[", string.Empty);
		directory = directory.Replace("]", string.Empty);
		return directory;
	}

	public static string GetFileSafePreviewUrl(string url)
	{
		string result = "";
		if (!string.IsNullOrEmpty(url))
		{
			result = RemoveInvalidCharsFromPath(url);
			result = RemoveInvalidCharsFromFilename(result);
		}
		return result;
	}

	public static StreamReader OpenStream(string fullpath)
	{
		try
		{
			if (File.Exists(fullpath))
			{
				return File.OpenText(fullpath);
			}
		}
		catch (Exception ex)
		{
			Debug.LogFormat("Exception trying to open stream {0}: {1}", fullpath, ex.Message);
		}
		return null;
	}

	public static void DeleteFile(string fullpath)
	{
		try
		{
			if (File.Exists(fullpath))
			{
				File.Delete(fullpath);
			}
		}
		catch (Exception ex)
		{
			Debug.LogFormat("Exception trying to delete {0}: {1}", fullpath, ex.Message);
		}
	}

	public static void RenameFile(string oldFullPath, string newFullPath)
	{
		try
		{
			if (File.Exists(oldFullPath))
			{
				File.Copy(oldFullPath, newFullPath);
				File.Delete(oldFullPath);
			}
		}
		catch (Exception ex)
		{
			Debug.LogFormat("Exception trying to rename {0} to {1}: {2}", oldFullPath, newFullPath, ex.Message);
		}
	}

	public static bool DeleteDirectoryAndContents(string fullpath)
	{
		try
		{
			if (Directory.Exists(fullpath))
			{
				Directory.Delete(fullpath, recursive: true);
			}
			return true;
		}
		catch (Exception ex)
		{
			Debug.LogFormat("Exception trying to delete direcotry {0}: {1}", fullpath, ex.Message);
			return false;
		}
	}

	public static void DeleteAllFilesInDirectory(string fullpath)
	{
		if (string.IsNullOrEmpty(fullpath) || !DirectoryExists(fullpath))
		{
			return;
		}
		try
		{
			foreach (FileInfo item in new DirectoryInfo(fullpath).EnumerateFiles())
			{
				item.Delete();
			}
		}
		catch (Exception ex)
		{
			Debug.LogFormat("Exception trying to delete files in {0}: {1}", fullpath, ex.Message);
		}
	}

	public static bool CopyFlatDirectory(string sourcePath, string destPath)
	{
		CreateDirectory(destPath);
		try
		{
			FileInfo[] files = new DirectoryInfo(sourcePath).GetFiles("*");
			foreach (FileInfo fileInfo in files)
			{
				File.Copy(fileInfo.FullName, Path.Combine(destPath, fileInfo.Name));
			}
			return true;
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Failed to copy files from " + sourcePath + " to " + destPath + " due to " + ex.Message);
			return false;
		}
	}

	public static void CopyDirectoryRecursive(string sourceDirectory, string targetDirectory)
	{
		DirectoryInfo source = new DirectoryInfo(sourceDirectory);
		DirectoryInfo target = new DirectoryInfo(targetDirectory);
		CopyAll(source, target);
	}

	private static void CopyAll(DirectoryInfo source, DirectoryInfo target)
	{
		Directory.CreateDirectory(target.FullName);
		FileInfo[] files = source.GetFiles();
		foreach (FileInfo fileInfo in files)
		{
			Debug.LogFormat("Copying {0}\\{1}", target.FullName, fileInfo.Name);
			fileInfo.CopyTo(Path.Combine(target.FullName, fileInfo.Name), overwrite: true);
		}
		DirectoryInfo[] directories = source.GetDirectories();
		foreach (DirectoryInfo directoryInfo in directories)
		{
			DirectoryInfo target2 = target.CreateSubdirectory(directoryInfo.Name);
			CopyAll(directoryInfo, target2);
		}
	}

	public static void RenameDirectory(string oldFullPath, string newFullPath)
	{
		try
		{
			if (Directory.Exists(oldFullPath))
			{
				Directory.Move(oldFullPath, newFullPath);
			}
		}
		catch (Exception ex)
		{
			Debug.LogFormat("Exception trying to rename directory {0} to {1}: {2}", oldFullPath, newFullPath, ex.Message);
		}
	}

	public static void RecursiveDelete(DirectoryInfo baseDir)
	{
		if (!baseDir.Exists)
		{
			return;
		}
		foreach (DirectoryInfo item in baseDir.EnumerateDirectories())
		{
			RecursiveDelete(item);
		}
		FileInfo[] files = baseDir.GetFiles();
		foreach (FileInfo obj in files)
		{
			obj.IsReadOnly = false;
			obj.Delete();
		}
		baseDir.Delete();
	}

	public static Color GetStressColor(float normalizedStress)
	{
		return HSVToRGB((1f - normalizedStress) * 0.35f, 0.7f, 0.7f);
	}

	public static Color HSVToRGB(float H, float S, float V)
	{
		if (S == 0f)
		{
			return new Color(V, V, V);
		}
		if (V == 0f)
		{
			return Color.black;
		}
		Color black = Color.black;
		float num = H * 6f;
		int num2 = Mathf.FloorToInt(num);
		float num3 = num - (float)num2;
		float num4 = V * (1f - S);
		float num5 = V * (1f - S * num3);
		float num6 = V * (1f - S * (1f - num3));
		switch (num2)
		{
		case -1:
			black.r = V;
			black.g = num4;
			black.b = num5;
			break;
		case 0:
			black.r = V;
			black.g = num6;
			black.b = num4;
			break;
		case 1:
			black.r = num5;
			black.g = V;
			black.b = num4;
			break;
		case 2:
			black.r = num4;
			black.g = V;
			black.b = num6;
			break;
		case 3:
			black.r = num4;
			black.g = num5;
			black.b = V;
			break;
		case 4:
			black.r = num6;
			black.g = num4;
			black.b = V;
			break;
		case 5:
			black.r = V;
			black.g = num4;
			black.b = num5;
			break;
		case 6:
			black.r = V;
			black.g = num6;
			black.b = num4;
			break;
		}
		black.r = Mathf.Clamp(black.r, 0f, 1f);
		black.g = Mathf.Clamp(black.g, 0f, 1f);
		black.b = Mathf.Clamp(black.b, 0f, 1f);
		return black;
	}

	public static Color GetColorFromHexCode(string hexCode, Color fallbackColor)
	{
		try
		{
			if (ColorUtility.TryParseHtmlString(hexCode, out var color))
			{
				return color;
			}
			return fallbackColor;
		}
		catch
		{
			return fallbackColor;
		}
	}

	public static Color ColorFromHSV(float h, float s, float v, float a = 1f)
	{
		if (s == 0f)
		{
			return new Color(v, v, v, a);
		}
		float num = h / 60f;
		int num2 = (int)num;
		float num3 = num - (float)num2;
		float num4 = v * (1f - s);
		float num5 = v * (1f - s * num3);
		float num6 = v * (1f - s * (1f - num3));
		Color result = new Color(0f, 0f, 0f, a);
		switch (num2)
		{
		case 0:
			result.r = v;
			result.g = num6;
			result.b = num4;
			break;
		case 1:
			result.r = num5;
			result.g = v;
			result.b = num4;
			break;
		case 2:
			result.r = num4;
			result.g = v;
			result.b = num6;
			break;
		case 3:
			result.r = num4;
			result.g = num5;
			result.b = v;
			break;
		case 4:
			result.r = num6;
			result.g = num4;
			result.b = v;
			break;
		default:
			result.r = v;
			result.g = num4;
			result.b = num5;
			break;
		}
		return result;
	}

	public static float GetColorDistance(Color c1, Color c2)
	{
		float num = c1.r - c2.r;
		float num2 = c1.g - c2.g;
		float num3 = c2.b - c2.b;
		return num * num + num2 * num2 + num3 * num3;
	}

	public static Vector3 NearestPointOnLineSegment(Vector3 start, Vector3 end, Vector3 pnt)
	{
		Vector3 vector = end - start;
		float magnitude = vector.magnitude;
		vector.Normalize();
		float value = Vector3.Dot(pnt - start, vector);
		value = Mathf.Clamp(value, 0f, magnitude);
		return start + vector * value;
	}

	public static bool LineSegmentIntersectsSphere(Vector3 start, Vector3 end, Vector3 center, float radius)
	{
		Vector3 b = NearestPointOnLineSegment(start, end, center);
		return Vector3.Distance(center, b) < radius;
	}

	public static void CreateDirectory(string path)
	{
		try
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarningFormat("Exception {0} trying to create directory: '{1}'", ex.Message, path);
		}
	}

	public static bool DirectoryExists(string path)
	{
		try
		{
			return Directory.Exists(path);
		}
		catch (Exception ex)
		{
			Debug.LogWarningFormat("Exception {0} trying to check if directory exists: '{1}'", ex.Message, path);
			return false;
		}
	}

	public static bool FileExists(string fullPath)
	{
		try
		{
			return File.Exists(fullPath);
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Exception " + ex.Message + " trying to check if file exists: '" + fullPath + "'");
			return false;
		}
	}

	public static long GetFileLengthInBytes(string fullPath)
	{
		try
		{
			return new FileInfo(fullPath).Length;
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Exception " + ex.Message + " calling Utils.GetFileLengthInBytes for '" + fullPath + "'");
			return 0L;
		}
	}

	public static byte[] ReadAllBytes(string fullPath)
	{
		try
		{
			return File.ReadAllBytes(fullPath);
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Exception " + ex.Message + " trying to read bytes from file: '" + fullPath + "'");
			return null;
		}
	}

	public static string ReadAllText(string fullPath)
	{
		try
		{
			return File.ReadAllText(fullPath);
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Exception " + ex.Message + " trying to read text from file: '" + fullPath + "'");
			return null;
		}
	}

	public static bool WriteAllText(string pathAndFilename, string text)
	{
		try
		{
			File.WriteAllText(pathAndFilename, text);
			return true;
		}
		catch (Exception ex)
		{
			Debug.LogWarningFormat("Exception {0} trying to write text to file: '{1}'", ex.Message, pathAndFilename);
			return false;
		}
	}

	public static void WriteBytesWithBackup(string path, string filename, byte[] bytes)
	{
		string text = Path.Combine(path, filename);
		string text2 = Path.ChangeExtension(text, ".restore");
		string text3 = Path.Combine(Application.persistentDataPath, Guid.NewGuid().ToString());
		string text4 = MD5HashFor(bytes);
		if (m_LastWrittenChecksums.ContainsKey(text) && FileExists(text) && m_LastWrittenChecksums[text] == text4)
		{
			return;
		}
		try
		{
			if (File.Exists(text2))
			{
				File.Delete(text2);
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarning("Caught Exception in WriteBytesWithBackup() doing File.Delete: " + ex.Message);
		}
		try
		{
			using FileStream fileStream = File.Create(text3, 1024, FileOptions.WriteThrough);
			fileStream.Write(bytes, 0, bytes.Length);
			fileStream.Close();
		}
		catch (Exception ex2)
		{
			Debug.LogWarning("Caught Exception in WriteBytesWithBackup() doing tempFile.Write: " + ex2.Message);
			WriteBytes(path, filename, bytes);
			return;
		}
		bool flag = false;
		bool flag2 = false;
		if (File.Exists(text))
		{
			try
			{
				File.Replace(text3, text, text2);
			}
			catch (Exception ex3)
			{
				flag = true;
				Debug.LogWarning("Caught Exception in WriteBytesWithBackup() doing File.Replace: " + ex3.Message);
			}
		}
		else
		{
			try
			{
				File.Move(text3, text);
			}
			catch (Exception ex4)
			{
				flag2 = true;
				Debug.LogWarning("Caught Exception in WriteBytesWithBackup() doing File.Move: " + ex4.Message);
			}
		}
		if (flag || flag2)
		{
			try
			{
				File.Copy(text, text2, overwrite: true);
			}
			catch (Exception ex5)
			{
				Debug.LogWarning("Caught Exception in WriteBytesWithBackup() doing File.Copy: " + ex5.Message);
			}
			try
			{
				File.Copy(text3, text, overwrite: true);
			}
			catch (Exception ex6)
			{
				Debug.LogWarning("Caught Exception in WriteBytesWithBackup() doing File.Copy: " + ex6.Message);
			}
		}
		DeleteFile(text3);
		if (m_LastWrittenChecksums.ContainsKey(text))
		{
			m_LastWrittenChecksums[text] = text4;
		}
		else
		{
			m_LastWrittenChecksums.Add(text, text4);
		}
	}

	public static void WriteBytes(string path, string filename, byte[] bytes)
	{
		WriteBytes(Path.Combine(path, filename), bytes);
	}

	public static void WriteBytes(string filepath, byte[] bytes)
	{
		try
		{
			string text = MD5HashFor(bytes);
			if (!m_LastWrittenChecksums.ContainsKey(filepath) || !(m_LastWrittenChecksums[filepath] == text))
			{
				File.WriteAllBytes(filepath, bytes);
				if (m_LastWrittenChecksums.ContainsKey(filepath))
				{
					m_LastWrittenChecksums[filepath] = text;
				}
				else
				{
					m_LastWrittenChecksums.Add(filepath, text);
				}
			}
		}
		catch (Exception ex)
		{
			Debug.LogWarningFormat("File.WriteBytes() failed due to '{0}'", ex.Message);
		}
	}

	public static Collider GetClosestRaycastHit(Vector2 screenPos, int layerMask)
	{
		int num = Physics.RaycastNonAlloc(Cameras.MainCamera().ScreenPointToRay(screenPos), m_RaycastHits, float.MaxValue, layerMask);
		Collider result = null;
		float num2 = float.MaxValue;
		for (int i = 0; i < num; i++)
		{
			Vector2 b = Cameras.MainCamera().WorldToScreenPoint(m_RaycastHits[i].transform.position);
			float num3 = Vector2.Distance(screenPos, b);
			if (num3 < num2)
			{
				num2 = num3;
				result = m_RaycastHits[i].collider;
			}
		}
		return result;
	}

	public static Color ScaleBrightness(Color color, float scale)
	{
		Color.RGBToHSV(color, out var H, out var S, out var V);
		return Color.HSVToRGB(H, S, Mathf.Clamp01(V * scale));
	}

	public static byte[] ZipPayload(byte[] payloadBytes)
	{
		using MemoryStream memoryStream = new MemoryStream();
		using GZipStream gZipStream = new GZipStream(memoryStream, CompressionMode.Compress);
		gZipStream.Write(payloadBytes, 0, payloadBytes.Length);
		gZipStream.Close();
		return memoryStream.ToArray();
	}

	public static byte[] UnZipPayload(byte[] payloadBytesCompressed)
	{
		using MemoryStream stream = new MemoryStream(payloadBytesCompressed);
		using GZipStream gZipStream = new GZipStream(stream, CompressionMode.Decompress);
		using MemoryStream memoryStream = new MemoryStream();
		gZipStream.CopyTo(memoryStream);
		return memoryStream.ToArray();
	}

	public static string GetFirstWord(string text)
	{
		if (!string.IsNullOrEmpty(text))
		{
			return text.Substring(0, text.IndexOf(" "));
		}
		return text;
	}

	public static string StripFirstWord(string text)
	{
		if (!string.IsNullOrEmpty(text))
		{
			return text.Substring(text.IndexOf(" ") + 1);
		}
		return text;
	}

	public static char StripTab(string input, int charIndex, char charToValidate)
	{
		if (charToValidate == '\t')
		{
			return '\0';
		}
		return charToValidate;
	}

	public static string DecodeFromUtf8(string utf8String)
	{
		if (string.IsNullOrEmpty(utf8String))
		{
			return utf8String;
		}
		return Regex.Unescape(utf8String);
	}

	public static Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
	{
		Texture2D texture2D = new Texture2D(targetWidth, targetHeight, source.format, mipChain: true);
		Color[] pixels = texture2D.GetPixels(0);
		float num = 1f / (float)targetWidth;
		float num2 = 1f / (float)targetHeight;
		for (int i = 0; i < pixels.Length; i++)
		{
			pixels[i] = source.GetPixelBilinear(num * ((float)i % (float)targetWidth), num2 * Mathf.Floor(i / targetWidth));
		}
		texture2D.SetPixels(pixels, 0);
		texture2D.Apply();
		return texture2D;
	}

	public static bool LineIntersectsRect(Vector2 p1, Vector2 p2, Rect r)
	{
		if (!LineIntersectsLine(p1, p2, new Vector2(r.x, r.y), new Vector2(r.x + r.width, r.y)) && !LineIntersectsLine(p1, p2, new Vector2(r.x + r.width, r.y), new Vector2(r.x + r.width, r.y + r.height)) && !LineIntersectsLine(p1, p2, new Vector2(r.x + r.width, r.y + r.height), new Vector2(r.x, r.y + r.height)) && !LineIntersectsLine(p1, p2, new Vector2(r.x, r.y + r.height), new Vector2(r.x, r.y)))
		{
			if (r.Contains(p1))
			{
				return r.Contains(p2);
			}
			return false;
		}
		return true;
	}

	public static bool LineIntersectsLine(Vector2 l1p1, Vector2 l1p2, Vector2 l2p1, Vector2 l2p2)
	{
		float num = (l1p1.y - l2p1.y) * (l2p2.x - l2p1.x) - (l1p1.x - l2p1.x) * (l2p2.y - l2p1.y);
		float num2 = (l1p2.x - l1p1.x) * (l2p2.y - l2p1.y) - (l1p2.y - l1p1.y) * (l2p2.x - l2p1.x);
		if (num2 == 0f)
		{
			return false;
		}
		float num3 = num / num2;
		num = (l1p1.y - l2p1.y) * (l1p2.x - l1p1.x) - (l1p1.x - l2p1.x) * (l1p2.y - l1p1.y);
		float num4 = num / num2;
		if (num3 < 0f || num3 > 1f || num4 < 0f || num4 > 1f)
		{
			return false;
		}
		return true;
	}

	public static bool RectOverlapsCircle2D(Vector2 rectPos, float width, float height, Vector2 circlePos, float radius)
	{
		float num = circlePos.x - Mathf.Max(rectPos.x, Mathf.Min(circlePos.x, rectPos.x + width));
		float num2 = circlePos.y - Mathf.Max(rectPos.y, Mathf.Min(circlePos.y, rectPos.y + height));
		return num * num + num2 * num2 < radius * radius;
	}

	public static void SizeRawImageToParent(RawImage rawImage)
	{
		RectTransform component = rawImage.transform.parent.GetComponent<RectTransform>();
		RectTransform component2 = rawImage.GetComponent<RectTransform>();
		float num = (float)rawImage.texture.width / (float)rawImage.texture.height;
		Rect rect = new Rect(0f, 0f, component.rect.width, component.rect.height);
		float num2 = rect.height;
		float num3 = num2 * num;
		if (num3 > rect.width)
		{
			num3 = rect.width;
			num2 = num3 / num;
		}
		component2.sizeDelta = new Vector2(num3, num2);
	}

	public static string GenerateCaseInsenstiveString(string input)
	{
		string text = string.Empty;
		for (int i = 0; i < input.Length; i++)
		{
			char c = input[i];
			if (char.IsUpper(c))
			{
				text += "_";
				text += c;
			}
			else
			{
				text += c;
			}
		}
		return text;
	}

	public static string GenerateUniqueId()
	{
		return Regex.Replace(Convert.ToBase64String(Guid.NewGuid().ToByteArray()), "[/+=]", "");
	}

	public static bool BoundsIntersect2D(Bounds A, Bounds B)
	{
		Vector2 vector = A.center - A.extents;
		Vector2 vector2 = A.center + A.extents;
		Vector2 vector3 = B.center - B.extents;
		Vector2 vector4 = B.center + B.extents;
		if (vector.x < vector4.x && vector2.x > vector3.x && vector.y < vector4.y)
		{
			return vector2.y > vector3.y;
		}
		return false;
	}

	public static bool LineSegmentsIntersect(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
	{
		float num = Direction(p3, p4, p1);
		float num2 = Direction(p3, p4, p2);
		float num3 = Direction(p1, p2, p3);
		float num4 = Direction(p1, p2, p4);
		if (((num > 0f && num2 < 0f) || (num < 0f && num2 > 0f)) && ((num3 > 0f && num4 < 0f) || (num3 < 0f && num4 > 0f)))
		{
			return true;
		}
		if (num == 0f && OnSegment(p3, p4, p1))
		{
			return true;
		}
		if (num2 == 0f && OnSegment(p3, p4, p2))
		{
			return true;
		}
		if (num3 == 0f && OnSegment(p1, p2, p3))
		{
			return true;
		}
		if (num4 == 0f && OnSegment(p1, p2, p4))
		{
			return true;
		}
		return false;
	}

	public static Vector3 GetWorldPosAtCenterOfScreen()
	{
		return GetWorldPointFromScreenPos(new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f));
	}

	public static float ConvertAngleToMinus180ToPositive180Range(float angle)
	{
		angle %= 360f;
		if (Mathf.Approximately(Math.Abs(angle), 180f))
		{
			return angle;
		}
		if (angle > 180f)
		{
			return angle - 360f;
		}
		if (angle < -180f)
		{
			return angle + 360f;
		}
		return angle;
	}

	public static bool IsInteger(float value)
	{
		return Math.Abs(value - (float)(int)value) < Mathf.Epsilon;
	}

	private static float Direction(Vector2 p1, Vector2 p2, Vector2 p3)
	{
		Vector2 vector = p3 - p1;
		Vector2 vector2 = p2 - p1;
		return vector.x * vector2.y - vector2.x * vector.y;
	}

	private static bool OnSegment(Vector2 p1, Vector2 p2, Vector2 p)
	{
		if (Mathf.Min(p1.x, p2.x) <= p.x && p.x <= Mathf.Max(p1.x, p2.x) && Mathf.Min(p1.y, p2.y) <= p.y)
		{
			return p.y <= Mathf.Max(p1.y, p2.y);
		}
		return false;
	}

	public static string MD5HashFor(byte[] data)
	{
		byte[] array = MD5.Create().ComputeHash(data);
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < array.Length; i++)
		{
			stringBuilder.Append(array[i].ToString("x2"));
		}
		return stringBuilder.ToString();
	}

	public static string MD5HashFor(string s)
	{
		MD5 mD = MD5.Create();
		byte[] bytes = Encoding.ASCII.GetBytes(s);
		byte[] array = mD.ComputeHash(bytes);
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < array.Length; i++)
		{
			stringBuilder.Append(array[i].ToString("x2"));
		}
		return stringBuilder.ToString();
	}

	public static bool MD5HashesMatch(string a, string b)
	{
		StringComparer ordinalIgnoreCase = StringComparer.OrdinalIgnoreCase;
		return ordinalIgnoreCase.Compare(a, b) == 0;
	}

	public static Sprite CreateSpriteFromTexture(string fullpath)
	{
		if (FileExists(fullpath))
		{
			Texture2D texture2D = new Texture2D(2, 2);
			byte[] array = ReadAllBytes(fullpath);
			if (array != null && array.Length != 0)
			{
				if (!texture2D.LoadImage(ReadAllBytes(fullpath)))
				{
					return null;
				}
				return Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f));
			}
		}
		return null;
	}

	public static string AddQuotation(string path)
	{
		if (string.IsNullOrEmpty(path))
		{
			throw new Exception("Empty path.");
		}
		if (path[0] != '"')
		{
			return "\"" + path + "\"";
		}
		return path;
	}

	public static bool CompareStringLists(List<string> list1, List<string> list2)
	{
		if (list1.Count != list2.Count)
		{
			return false;
		}
		for (int i = 0; i < list1.Count; i++)
		{
			if (list1[i] != list2[i])
			{
				return false;
			}
		}
		return true;
	}

	public static void OpenLocalPath(string localPath)
	{
		Application.OpenURL(localPath);
	}

	public static string GetPathFromPathAndFilename(string pathAndFilename)
	{
		if (string.IsNullOrEmpty(pathAndFilename))
		{
			return string.Empty;
		}
		string[] array = pathAndFilename.Split(Path.DirectorySeparatorChar);
		if (array.Length < 2)
		{
			return string.Empty;
		}
		string text = string.Empty;
		for (int i = 0; i < array.Length - 1; i++)
		{
			text = Path.Combine(text, array[i]);
		}
		return text;
	}

	public static void EnableDropdown(GameObject dropdown, bool active)
	{
		dropdown.transform.parent.parent.gameObject.SetActive(active);
	}

	public static void EnableInputField(GameObject inputfield, bool active)
	{
		inputfield.transform.parent.gameObject.SetActive(active);
	}

	public static void EnableToggle(GameObject toggle, bool active)
	{
		toggle.transform.parent.parent.gameObject.SetActive(active);
	}

	public static void EnableSlider(GameObject slider, bool active)
	{
		slider.transform.parent.gameObject.SetActive(active);
	}

	public static bool PointIsOffscreen(Vector2 pos)
	{
		if (pos.x < 0f || pos.x > (float)Screen.width)
		{
			return true;
		}
		if (pos.y < 0f || pos.y > (float)Screen.height)
		{
			return true;
		}
		return false;
	}
}
