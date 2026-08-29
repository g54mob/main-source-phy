using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.CrashReportHandler;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public static class UnityUtils
{
	public static Pose lerpPose(Pose a, Pose b, float t)
	{
		return new Pose(Vector3.Lerp(a.position, b.position, t), Quaternion.Lerp(a.rotation, b.rotation, t));
	}

	public static float smootherStep(float minimum, float maximum, float value)
	{
		float num = Mathf.Clamp01((value - minimum) / (maximum - minimum));
		return num * num * num * (num * (num * 6f - 15f) + 10f);
	}

	public static void drawTitle(string title, TextAnchor alignment = TextAnchor.MiddleCenter)
	{
		GUILayout.Space(10f);
		GUILayout.Label(title, new GUIStyle(GUI.skin.label)
		{
			alignment = alignment,
			richText = true
		});
		GUILayout.Space(10f);
	}

	public static float getPercentBetween(float point, float min, float max)
	{
		return (point - min) / (max - min);
	}

	public static float map(float fromSource, float toSource, float fromTarget, float toTarget, float point)
	{
		return getPercentBetween(point, fromSource, toSource) * (toTarget - fromTarget) + fromTarget;
	}

	public static float mapOnCurve(float fromSource, float toSource, AnimationCurve curve, float point)
	{
		float time = map(fromSource, toSource, 0f, 1f, point);
		return curve.Evaluate(time);
	}

	public static float inverseLerp(Vector3 a, Vector3 b, Vector3 value)
	{
		Vector3 vector = b - a;
		return Vector3.Dot(value - a, vector) / Vector3.Dot(vector, vector);
	}

	public static Vector3 abs(Vector3 vec)
	{
		return new Vector3(Mathf.Abs(vec.x), Mathf.Abs(vec.y), Mathf.Abs(vec.z));
	}

	public static Vector3 clamp(Vector3 vec, float min, float max)
	{
		return new Vector3(Mathf.Clamp(vec.x, min, max), Mathf.Clamp(vec.y, min, max), Mathf.Clamp(vec.z, min, max));
	}

	public static Vector3 clampToScreen(Vector3 screenPoint)
	{
		Vector3 vector = screenPoint;
		if (vector.z < 0f)
		{
			vector.x = (float)Screen.width - vector.x;
			vector.y = (float)Screen.height - vector.y;
			vector.x = Mathf.Clamp(vector.x, 0f, Screen.width);
			vector.y = Mathf.Clamp(vector.y, 0f, Screen.height);
		}
		return new Vector3(vector.x, vector.y, 0f);
	}

	public static bool closeEnough(Vector3 vecA, Vector3 vecB, float maxDifference = 0.01f)
	{
		if (closeEnough(vecA.x, vecB.x, maxDifference) && closeEnough(vecA.y, vecB.y, maxDifference))
		{
			return closeEnough(vecA.z, vecB.z, maxDifference);
		}
		return false;
	}

	public static bool closeEnough(Vector2 vecA, Vector2 vecB, float maxDifference = 0.01f)
	{
		if (closeEnough(vecA.x, vecB.x, maxDifference))
		{
			return closeEnough(vecA.y, vecB.y, maxDifference);
		}
		return false;
	}

	public static bool closeEnough(Quaternion rotA, Quaternion rotB, float maxDifference = 1f)
	{
		return Quaternion.Angle(rotA, rotB) < maxDifference;
	}

	public static bool closeEnough(float numberA, float numberB, float maxDifference = 0.01f)
	{
		return Mathf.Abs(numberA - numberB) < maxDifference;
	}

	public static bool contains(Array array, object obj)
	{
		for (int i = 0; i < array.Length; i++)
		{
			if (array.GetValue(i) == obj)
			{
				return true;
			}
		}
		return false;
	}

	public static bool contains(Array array, string obj)
	{
		for (int i = 0; i < array.Length; i++)
		{
			if (array.GetValue(i).ToString() == obj)
			{
				return true;
			}
		}
		return false;
	}

	public static int getIndex(Array array, object obj)
	{
		for (int i = 0; i < array.Length; i++)
		{
			if (array.GetValue(i) == obj)
			{
				return i;
			}
		}
		return -1;
	}

	public static bool hasFlag(int value, int flag)
	{
		return (value & flag) == flag;
	}

	public static bool isObjectInMainOrTopSplitScreenScene(GameObject gameObject)
	{
		return SceneManager.GetActiveScene() == gameObject.scene;
	}

	public static bool hasIntersectingData(Array lookIn, Array objs)
	{
		for (int i = 0; i < lookIn.Length; i++)
		{
			for (int j = 0; j < objs.Length; j++)
			{
				if (lookIn.GetValue(i) == objs.GetValue(j))
				{
					return true;
				}
			}
		}
		return false;
	}

	public static string getUppercaseCharacters(string input)
	{
		return new Regex("[^A-Z0-9]").Replace(input, "");
	}

	public static string addSpacesBeforeCapitalLetters(string text)
	{
		string pattern = "(?<!^)(?=\\p{Lu})";
		return Regex.Replace(text, pattern, " ");
	}

	public static bool closestPointsBetweenRays(out Vector3 pointOnRay1, out Vector3 pointOnRay2, Ray ray1, Ray ray2)
	{
		Vector3 origin = ray1.origin;
		Vector3 direction = ray1.direction;
		Vector3 origin2 = ray2.origin;
		Vector3 direction2 = ray2.direction;
		float num = Vector3.Dot(direction, direction);
		float num2 = Vector3.Dot(direction, direction2);
		float num3 = Vector3.Dot(direction2, direction2);
		float num4 = num * num3 - num2 * num2;
		if (num4 == 0f)
		{
			pointOnRay1 = Vector3.zero;
			pointOnRay2 = Vector3.zero;
			return false;
		}
		Vector3 rhs = origin - origin2;
		float num5 = Vector3.Dot(direction, rhs);
		float num6 = Vector3.Dot(direction2, rhs);
		float num7 = (num2 * num6 - num5 * num3) / num4;
		float num8 = (num * num6 - num5 * num2) / num4;
		pointOnRay1 = origin + direction * num7;
		pointOnRay2 = origin2 + direction2 * num8;
		return true;
	}

	public static Vector3 limitVectorToCone(Vector3 vector, Vector3 coneDirection, float coneAngle)
	{
		coneDirection = coneDirection.normalized;
		Vector3 normalized = vector.normalized;
		if (Vector3.Angle(normalized, coneDirection) <= coneAngle)
		{
			return vector;
		}
		Vector3 vector2 = normalized - Vector3.Project(normalized, coneDirection);
		return (coneDirection * Mathf.Cos(coneAngle * (MathF.PI / 180f)) + vector2.normalized * Mathf.Sin(coneAngle * (MathF.PI / 180f))).normalized * vector.magnitude;
	}

	public static bool isPointInsideRotatedCube(Vector3 point, Transform cubeCenter, Vector3 cubeExtents)
	{
		Vector3 vector = cubeCenter.InverseTransformPoint(point);
		if (Mathf.Abs(vector.x) > Mathf.Abs(cubeExtents.x))
		{
			return false;
		}
		if (Mathf.Abs(vector.y) > Mathf.Abs(cubeExtents.y))
		{
			return false;
		}
		if (Mathf.Abs(vector.z) > Mathf.Abs(cubeExtents.z))
		{
			return false;
		}
		return true;
	}

	public static void drawBounds(Bounds meshBounds, Transform coordinateSystem = null, float cornerSphereRadius = 0.01f, float destroyAfter = 0.1f)
	{
		drawSphereAt(resolvePoint(new Vector3(meshBounds.min.x, meshBounds.min.y, meshBounds.min.z)), Color.magenta, cornerSphereRadius, "Debug Sphere", destroyAfter);
		drawSphereAt(resolvePoint(new Vector3(meshBounds.min.x, meshBounds.min.y, meshBounds.max.z)), Color.magenta, cornerSphereRadius, "Debug Sphere", destroyAfter);
		drawSphereAt(resolvePoint(new Vector3(meshBounds.min.x, meshBounds.max.y, meshBounds.min.z)), Color.magenta, cornerSphereRadius, "Debug Sphere", destroyAfter);
		drawSphereAt(resolvePoint(new Vector3(meshBounds.min.x, meshBounds.max.y, meshBounds.max.z)), Color.magenta, cornerSphereRadius, "Debug Sphere", destroyAfter);
		drawSphereAt(resolvePoint(new Vector3(meshBounds.max.x, meshBounds.min.y, meshBounds.min.z)), Color.magenta, cornerSphereRadius, "Debug Sphere", destroyAfter);
		drawSphereAt(resolvePoint(new Vector3(meshBounds.max.x, meshBounds.min.y, meshBounds.max.z)), Color.magenta, cornerSphereRadius, "Debug Sphere", destroyAfter);
		drawSphereAt(resolvePoint(new Vector3(meshBounds.max.x, meshBounds.max.y, meshBounds.min.z)), Color.magenta, cornerSphereRadius, "Debug Sphere", destroyAfter);
		drawSphereAt(resolvePoint(new Vector3(meshBounds.max.x, meshBounds.max.y, meshBounds.max.z)), Color.magenta, cornerSphereRadius, "Debug Sphere", destroyAfter);
		Vector3 resolvePoint(Vector3 point)
		{
			if (!(coordinateSystem != null))
			{
				return point;
			}
			return coordinateSystem.TransformPoint(point);
		}
	}

	public static GameObject drawSphereAt(Vector3 position, Color color = default(Color), float radius = 0.02f, string name = "Debug Sphere", float destroyAfter = 0.05f)
	{
		GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		gameObject.name = name;
		gameObject.transform.position = position;
		gameObject.transform.localScale = Vector3.one * (radius * 2f);
		UnityEngine.Object.DestroyImmediate(gameObject.GetComponent<Collider>());
		if (destroyAfter >= 0f)
		{
			UnityEngine.Object.Destroy(gameObject, destroyAfter);
		}
		if (color != default(Color))
		{
			gameObject.GetComponent<Renderer>().material.color = color;
		}
		return gameObject;
	}

	public static Bounds transformBoundsToLocal(Transform target, Bounds worldBounds)
	{
		Vector3 center = target.InverseTransformPoint(worldBounds.center);
		Bounds result = new Bounds(center, Vector3.zero);
		Vector3 extents = worldBounds.extents;
		for (int i = -1; i <= 1; i += 2)
		{
			for (int j = -1; j <= 1; j += 2)
			{
				for (int k = -1; k <= 1; k += 2)
				{
					Vector3 position = worldBounds.center + Vector3.Scale(extents, new Vector3(i, j, k));
					Vector3 point = target.InverseTransformPoint(position);
					result.Encapsulate(point);
				}
			}
		}
		return result;
	}

	public static float calculateProjectileAngle(Vector3 projectileDirection)
	{
		Vector3 to = Vector3.ProjectOnPlane(projectileDirection, Vector3.up);
		return Vector3.SignedAngle(projectileDirection, to, Vector3.Cross(Vector3.up, projectileDirection));
	}

	public static Mesh generatePlaneMesh(int width, int height, float quadSize = 1f)
	{
		Mesh mesh = new Mesh();
		int num = (width + 1) * (height + 1);
		Vector3[] array = new Vector3[num];
		Vector2[] array2 = new Vector2[num];
		int[] array3 = new int[width * height * 6];
		for (int i = 0; i <= height; i++)
		{
			for (int j = 0; j <= width; j++)
			{
				int num2 = j + i * width;
				array[num2] = new Vector3(((float)j - (float)width / 2f) * quadSize, 0f, ((float)i - (float)height / 2f) * quadSize);
				array2[num2] = new Vector2((float)j / (float)width, (float)i / (float)height);
			}
		}
		int num3 = 0;
		for (int k = 0; k < height; k++)
		{
			for (int l = 0; l < width; l++)
			{
				int num4 = l + k * width;
				array3[num3++] = num4;
				array3[num3++] = num4 + width;
				array3[num3++] = num4 + 1;
				array3[num3++] = num4 + 1;
				array3[num3++] = num4 + width;
				array3[num3++] = num4 + width + 1;
			}
		}
		mesh.vertices = array;
		mesh.triangles = array3;
		mesh.uv = array2;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
		return mesh;
	}

	public static void visitScene(Scene scene, Action<GameObject> visitor)
	{
		GameObject[] rootGameObjects = scene.GetRootGameObjects();
		for (int i = 0; i < rootGameObjects.Length; i++)
		{
			visitGameObject(rootGameObjects[i], visitor);
		}
	}

	public static void visitGameObject(GameObject rootObject, Action<GameObject> visitor)
	{
		visitor(rootObject);
		foreach (Transform item in rootObject.transform)
		{
			visitGameObject(item.gameObject, visitor);
		}
	}

	public static void visitGameObjectChildFirst(GameObject rootObject, Action<GameObject> visitor)
	{
		foreach (Transform item in rootObject.transform)
		{
			visitGameObjectChildFirst(item.gameObject, visitor);
		}
		visitor(rootObject);
	}

	public static void visitScene(Scene scene, int maxDepth = -1, Action<(GameObject gameObject, int index, int depth)> visitor = null)
	{
		int index = 0;
		GameObject[] rootGameObjects = scene.GetRootGameObjects();
		for (int i = 0; i < rootGameObjects.Length; i++)
		{
			visitGameObject(rootGameObjects[i], 0);
		}
		void visitGameObject(GameObject rootObject, int depth)
		{
			if (maxDepth >= 0 && depth > maxDepth)
			{
				return;
			}
			visitor?.Invoke((rootObject, index, depth));
			index++;
			foreach (Transform item in rootObject.transform)
			{
				visitGameObject(item.gameObject, depth + 1);
			}
		}
	}

	public static Transform findRecursively(this Transform transform, string name)
	{
		foreach (Transform item in transform)
		{
			if (item.name == name)
			{
				return item;
			}
			Transform transform3 = item.findRecursively(name);
			if (transform3 != null)
			{
				return transform3;
			}
		}
		return null;
	}

	public static int getPersistentHashCode(int value)
	{
		uint num = (uint)value;
		num = num + 2127912214 + (num << 12);
		num = num ^ 0xC761C23Cu ^ (num >> 19);
		num = num + 374761393 + (num << 5);
		num = (uint)((int)num + -744332180) ^ (num << 9);
		num = (uint)((int)num + -42973499) + (num << 3);
		return (int)(num ^ 0xB55A4F09u ^ (num >> 16));
	}

	public static int getPersistentHashCode(string value)
	{
		if (string.IsNullOrEmpty(value))
		{
			return 0;
		}
		int length = value.Length;
		uint num = (uint)length;
		int num2 = length & 1;
		length >>= 1;
		int num3 = 0;
		while (length > 0)
		{
			num += value[num3];
			uint num4 = ((uint)value[num3 + 1] << 11) ^ num;
			num = (num << 16) ^ num4;
			num3 += 2;
			num += num >> 11;
			length--;
		}
		if (num2 == 1)
		{
			num += value[num3];
			num ^= num << 11;
			num += num >> 17;
		}
		num ^= num << 3;
		num += num >> 5;
		num ^= num << 4;
		num += num >> 17;
		num ^= num << 25;
		return (int)(num + (num >> 6));
	}

	public static string replaceStringRebindKeys(string text, MenuOptions rebinding)
	{
		string text2 = null;
		bool flag = true;
		int num = 0;
		int num2 = 0;
		while (flag)
		{
			flag = false;
			for (int i = num; i < text.Length; i++)
			{
				if (text[i] == '[')
				{
					text2 = "";
					num2 = i;
				}
				else if (text2 != null && text[i] != ']')
				{
					text2 += text[i];
				}
				else if (text2 != null && text[i] == ']')
				{
					flag = true;
					num = num2 + 1;
					break;
				}
			}
			if (flag)
			{
				if (text2.StartsWith("KeyBindingAction"))
				{
					KeyBindingAction action = (KeyBindingAction)Enum.Parse(typeof(KeyBindingAction), text2.Split('.')[1]);
					KeyCode key = rebinding.getKey(action);
					KeyCode secondaryKey = rebinding.getSecondaryKey(action);
					string newValue = ((secondaryKey != KeyCode.None) ? (key.ToString() + "/" + secondaryKey) : key.ToString());
					text = text.Replace(text2, newValue);
				}
				if (text2 == "WASD")
				{
					string text3 = "";
					text3 += rebinding.getKey(KeyBindingAction.Up);
					text3 += rebinding.getKey(KeyBindingAction.Left);
					text3 += rebinding.getKey(KeyBindingAction.Down);
					text3 += rebinding.getKey(KeyBindingAction.Right);
					text = text.Replace(text2, text3);
				}
				if (text2 == "Arrow")
				{
					string text4 = "";
					text4 += rebinding.getSecondaryKey(KeyBindingAction.Up);
					text4 += rebinding.getSecondaryKey(KeyBindingAction.Left);
					text4 += rebinding.getSecondaryKey(KeyBindingAction.Down);
					text4 += rebinding.getSecondaryKey(KeyBindingAction.Right);
					text = text.Replace(text2, text4);
				}
				text2 = null;
			}
		}
		return text;
	}

	public static string addSuffixToFileName(string path, string suffix)
	{
		string directoryName = Path.GetDirectoryName(path);
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
		string extension = Path.GetExtension(path);
		return Path.Combine(directoryName, fileNameWithoutExtension + suffix + extension);
	}

	public static Texture2D createTexture(Color color, int width = 1, int height = 1)
	{
		Color[] array = new Color[width * height];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = color;
		}
		Texture2D texture2D = new Texture2D(width, height);
		texture2D.SetPixels(array);
		texture2D.Apply();
		return texture2D;
	}

	public static void resetCrashHandlers()
	{
		CrashReportHandler.SetUserMetadata("Last Levels", "");
	}

	public static void setCrashHandler(string level, Net session)
	{
		try
		{
			string text = CrashReportHandler.GetUserMetadata("Last Levels");
			if (text == null)
			{
				text = "";
			}
			string[] array = text.Split(";", StringSplitOptions.RemoveEmptyEntries);
			string text2 = "";
			int num = array.Length - 1;
			while (num >= 0 && array.Length - num < 3)
			{
				text2 = array[num] + ";" + text2;
				num--;
			}
			text2 += level;
			CrashReportHandler.SetUserMetadata("Last Levels", text2);
			CrashReportHandler.SetUserMetadata("Level", level);
			CrashReportHandler.SetUserMetadata("Net Mode", session.netMode.ToString());
			CrashReportHandler.SetUserMetadata("Protocol", session.connectionMode.ToString());
			CrashReportHandler.SetUserMetadata("Player Count", session.players.Count.ToString());
			CrashReportHandler.SetUserMetadata("Game Difficulty", session.gameDifficulty.ToString());
			CrashReportHandler.SetUserMetadata("Lobby Type", session.lobbyType.ToString());
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.Message);
			Debug.LogError(ex.StackTrace);
		}
	}

	public static TEnum tryParseEnum<TEnum>(string enumAsString) where TEnum : struct, Enum
	{
		if (!Enum.TryParse<TEnum>(enumAsString, out var result))
		{
			Debug.LogError("Could not get '" + typeof(TEnum).Name + "' from string '" + enumAsString + "'.");
		}
		return result;
	}

	public static void setCrashHandlerGame(Game game)
	{
		string value = "-";
		string value2 = "-";
		string value3 = "-";
		if (game != null)
		{
			value = game.gameState.current.ToString();
			if (game.topZoomContextSafe(out var context))
			{
				value2 = context.selected.name;
			}
			value3 = game.getCurrentPuzzleIndex().ToString();
		}
		try
		{
			CrashReportHandler.SetUserMetadata("Zoom Object", value2);
			CrashReportHandler.SetUserMetadata("View State", value);
			CrashReportHandler.SetUserMetadata("Current puzzle index", value3);
			CrashReportHandler.SetUserMetadata("Controller", Controller.isActive().ToString());
		}
		catch (Exception ex)
		{
			Debug.LogError(ex.Message);
			Debug.LogError(ex.StackTrace);
		}
	}

	public static void printMicrophoneDevices()
	{
		string[] devices = Microphone.devices;
		Debug.Log("Microphones: " + ((devices != null) ? string.Join(", ", devices) : "null"));
	}

	public static void loadStreamingAssetsTextFileAsync(string relativeFilePath, Action<string> onFileLoaded)
	{
		Executor.executeCoroutine(loadFileCoroutine(Path.Combine(Application.streamingAssetsPath, relativeFilePath), onFileLoaded));
		static IEnumerator loadFileCoroutine(string filePath, Action<string> action)
		{
			UnityWebRequest request = UnityWebRequest.Get(filePath);
			UnityWebRequestAsyncOperation asyncOperation = request.SendWebRequest();
			yield return asyncOperation;
			if (asyncOperation.webRequest.result != UnityWebRequest.Result.Success)
			{
				action(null);
			}
			else
			{
				action(request.downloadHandler.text);
			}
		}
	}

	public static void printUIUnderMouse()
	{
		if (EventSystem.current == null)
		{
			Debug.LogError("Event system is null!");
			return;
		}
		List<RaycastResult> list = new List<RaycastResult>();
		PointerEventData eventData = new PointerEventData(EventSystem.current)
		{
			position = Input.mousePosition
		};
		EventSystem.current.RaycastAll(eventData, list);
		Debug.Log($"UI Hits ({list.Count}):");
		foreach (RaycastResult item in list)
		{
			Debug.Log(item.gameObject.name, item.gameObject);
		}
		Debug.Log("----------");
	}

	public static bool isMethodOverriden(Type type, string methodName)
	{
		MethodInfo method = type.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		if (method == null)
		{
			return false;
		}
		return method.GetBaseDefinition().DeclaringType != method.DeclaringType;
	}
}
