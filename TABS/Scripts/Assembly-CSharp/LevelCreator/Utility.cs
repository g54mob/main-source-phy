using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using HighlightingSystem;
using UnityEngine;
using UnityEngine.Events;

namespace LevelCreator
{
	public class Utility
	{
		public enum GameMode
		{
			PlayMode = 0,
			EditorMode = 1
		}

		public enum SnapDistance
		{
			Short = 0,
			Unlimited = 1
		}

		public struct SnapTransform
		{
			public Vector3 position;

			public Quaternion slope;
		}

		public static GameMode GetCurrentGameMode()
		{
			if (DMEditor.Instance == null)
			{
				return GameMode.PlayMode;
			}
			return GameMode.EditorMode;
		}

		public static DMEditorComponent GetRootEditorObject(DMEditorComponent editorObject)
		{
			DMEditorComponent component = editorObject.transform.parent.GetComponent<DMEditorComponent>();
			if (!component)
			{
				return editorObject;
			}
			return GetRootEditorObject(component);
		}

		public static int PositiveModulo(int x, int m)
		{
			return (x % m + m) % m;
		}

		public static Vector3 Divide(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
		}

		public static TargetInfo GetTargetInfo(Vector3 origin, Vector3 forward, float distance, int layerMask)
		{
			if (Physics.Raycast(origin + forward, forward, out var hitInfo, distance, layerMask))
			{
				return new TargetInfo
				{
					position = hitInfo.point,
					normal = hitInfo.normal,
					gameObject = hitInfo.transform.gameObject,
					hit = true
				};
			}
			return new TargetInfo
			{
				position = origin + forward * distance,
				normal = Vector3.up,
				gameObject = null,
				hit = false
			};
		}

		public static TargetInfo GetTargetInfo(Vector3 origin, Vector3 forward, float distance)
		{
			return GetTargetInfo(origin, forward, distance, ~((1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("Water"))));
		}

		public static Vector3 GetTargetPosition(Vector3 position, Vector3 forward, float distance)
		{
			if (Physics.Raycast(position + forward, forward, out var hitInfo, distance, ~((1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("Water")))))
			{
				return hitInfo.point;
			}
			return position + forward * distance;
		}

		public static Vector3 GetTargetPositionOnVolume(Vector3 position, Vector3 forward, float distance)
		{
			if (Physics.Raycast(position + forward, forward, out var hitInfo, distance, 1 << LayerMask.NameToLayer("Map")))
			{
				return hitInfo.point;
			}
			return position + forward * distance;
		}

		public static Vector3 GetTargetPositionIncludingWater(Vector3 position, Vector3 forward, float distance)
		{
			if (Physics.Raycast(position + forward, forward, out var hitInfo, distance, ~((1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("Player")))))
			{
				return hitInfo.point;
			}
			return position + forward * distance;
		}

		public static Vector3 GetTargetPositionOnVolumeIncludingWater(Vector3 position, Vector3 forward, float distance)
		{
			if (Physics.Raycast(position + forward, forward, out var hitInfo, distance, (1 << LayerMask.NameToLayer("Map")) | (1 << LayerMask.NameToLayer("Water"))))
			{
				return hitInfo.point;
			}
			return position + forward * distance;
		}

		public static DMEditorComponent GetObjectInLine(Vector3 position, Vector3 forward, float distance)
		{
			if (Physics.Raycast(position + forward, forward, out var hitInfo, distance, ~((1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("Water")))))
			{
				return hitInfo.transform.GetComponentInParent<DMEditorComponent>();
			}
			return null;
		}

		public static List<DMEditorComponent> GetObjectsInLine(Vector3 position, Vector3 forward, float distance)
		{
			List<DMEditorComponent> list = new List<DMEditorComponent>();
			RaycastHit[] array = Physics.RaycastAll(position + forward, forward, distance, ~((1 << LayerMask.NameToLayer("Ignore Raycast")) | (1 << LayerMask.NameToLayer("Player")) | (1 << LayerMask.NameToLayer("Water"))));
			for (int i = 0; i < array.Length; i++)
			{
				RaycastHit raycastHit = array[i];
				if (!(raycastHit.transform == null))
				{
					DMEditorComponent componentInParent = raycastHit.transform.GetComponentInParent<DMEditorComponent>();
					if ((bool)componentInParent)
					{
						list.Add(componentInParent);
					}
				}
			}
			return list;
		}

		public static List<DMEditorComponent> SortByDistance(List<DMEditorComponent> objects, Vector3 point)
		{
			return (from x in objects
				where x != null
				orderby Vector3.Distance(x.Position, point)
				select x).ToList();
		}

		public static void ConstructPath(Vector3 position, Vector3 velocity, List<Vector3> path)
		{
			path.Clear();
			float num = 1f / 64f;
			for (float num2 = num; (double)num2 < 0.25; num2 += num)
			{
				path.Add(position);
				Vector3 vector = position + velocity * num2;
				UnityEngine.Debug.DrawLine(position, vector, Color.red);
				if (Physics.Linecast(position, vector, out var hitInfo, 1 << LayerMask.NameToLayer("Map")))
				{
					position = hitInfo.point;
					break;
				}
				position = vector;
				velocity += new Vector3(0f, -9.98f, 0f) * num2;
			}
			path.Add(position);
		}

		public static void ConstructPath_ExplicitCheckVolumeOnly(Volume volume, Vector3 position, Vector3 velocity, List<Vector3> path)
		{
			path.Clear();
			float num = 1f / 64f;
			for (float num2 = num; (double)num2 < 0.25; num2 += num)
			{
				path.Add(position);
				Vector3 vector = position + velocity * num2;
				Vector3? vector2 = volume.LineCast(position, vector);
				if (vector2.HasValue)
				{
					position = vector2.Value;
					break;
				}
				position = vector;
				velocity += new Vector3(0f, -9.98f, 0f) * num2;
			}
			path.Add(position);
		}

		public static void RenderLocalPath(Transform transform, float lineWidth, List<Vector3> path, Color color)
		{
			if (path.Count >= 2)
			{
				GL.Begin(5);
				Vector3 vector = transform.TransformDirection(Vector3.Cross((path[0] - path[1]).normalized, Vector3.up));
				for (int i = 0; i < path.Count; i++)
				{
					GL.Color(color);
					Vector3 vector2 = transform.TransformPoint(path[i]);
					GL.Vertex(vector2 - vector * lineWidth);
					GL.Vertex(vector2 + vector * lineWidth);
				}
				GL.End();
			}
		}

		public static void RenderPath(Vector3 up, float lineWidth, List<Vector3> path)
		{
			if (path.Count >= 2)
			{
				GL.Begin(5);
				Vector3 vector = Vector3.Cross((path[1] - path[2]).normalized, up);
				for (int i = 0; i < path.Count; i++)
				{
					GL.Color(Color.yellow);
					Vector3 vector2 = path[i];
					GL.Vertex(vector2 - vector * lineWidth);
					GL.Vertex(vector2 + vector * lineWidth);
				}
				GL.End();
			}
		}

		public static bool FindBestGroundPosition(Vector3 currentPosition, Vector3 up, out Vector3 bestPosition)
		{
			Vector3 bestNormal;
			return FindBestGroundPosition(currentPosition, up, out bestPosition, out bestNormal);
		}

		public static bool FindBestGroundPosition(Vector3 currentPosition, Vector3 up, out Vector3 bestPosition, out Vector3 bestNormal)
		{
			bool flag = false;
			bestPosition = Vector3.zero;
			bestNormal = Vector3.zero;
			Vector3 vector = currentPosition + up;
			Vector3 end = currentPosition - up;
			Vector3 start = vector;
			for (int i = 0; i < 10; i++)
			{
				if (!Physics.Linecast(start, end, out var hitInfo, 1 << LayerMask.NameToLayer("Map")))
				{
					break;
				}
				if (!flag || Vector3.Distance(hitInfo.point, currentPosition) < Vector3.Distance(bestPosition, currentPosition))
				{
					bestPosition = hitInfo.point;
					bestNormal = hitInfo.normal;
					flag = true;
				}
				start = hitInfo.point + Vector3.down * 0.01f;
			}
			return flag;
		}

		public static bool SnapObjectAt(DMEditorComponent dmEditorComponent, Vector3 newPosition, DMEditorComponent.TeleportMode teleportMode, SnapDistance snapDistance)
		{
			if (dmEditorComponent == null)
			{
				return false;
			}
			SnapTransform? snapTransform = GetSnapTransform(newPosition, snapDistance);
			if (snapTransform.HasValue)
			{
				dmEditorComponent.Position = snapTransform.Value.position;
				dmEditorComponent.Slope = snapTransform.Value.slope;
			}
			else
			{
				dmEditorComponent.Position = newPosition;
				dmEditorComponent.Slope = Quaternion.identity;
			}
			dmEditorComponent.Teleport(teleportMode);
			return snapTransform.HasValue;
		}

		public static void RotateObject(DMEditorComponent dmEditorComponent, Quaternion newAdditionalRotation)
		{
			if (!(dmEditorComponent == null))
			{
				dmEditorComponent.AdditionalRotation = newAdditionalRotation;
				dmEditorComponent.gameObject.transform.localRotation = dmEditorComponent.CalculateLocalRotation();
			}
		}

		public static void ScaleObject(DMEditorComponent dmEditorComponent, Vector3 newScale)
		{
			if (!(dmEditorComponent == null))
			{
				dmEditorComponent.Scale = newScale;
				dmEditorComponent.gameObject.transform.localScale = dmEditorComponent.Scale;
			}
		}

		public static float GetSnapDistance(SnapDistance snapDistance)
		{
			if (snapDistance != SnapDistance.Short)
			{
				return 1000f;
			}
			return 10f;
		}

		public static SnapTransform? GetSnapTransform(Vector3 position, SnapDistance snapDistance)
		{
			float snapDistance2 = GetSnapDistance(snapDistance);
			if (FindBestGroundPosition(position, Vector3.up * snapDistance2, out var bestPosition, out var bestNormal))
			{
				Vector3 vector = Vector3.Cross(Vector3.up, bestNormal);
				Quaternion slope = Quaternion.AngleAxis(vector.magnitude * 90f, vector.normalized);
				return new SnapTransform
				{
					position = bestPosition,
					slope = slope
				};
			}
			return null;
		}

		public static Bounds GetBounds(Transform obj)
		{
			List<Renderer> list = new List<Renderer>();
			obj.GetComponentsInChildren(list);
			Bounds result = default(Bounds);
			bool flag = false;
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].enabled)
				{
					if (!flag)
					{
						result = list[i].bounds;
						flag = true;
					}
					else
					{
						result.Encapsulate(list[i].bounds);
					}
				}
			}
			return result;
		}

		public static void SetHighlightObject(GameObject gameObject, bool highlight, Color? hoverColor = null, Color? constantColor = null)
		{
			if (gameObject == null)
			{
				return;
			}
			Highlighter highlighter = gameObject.GetComponent<Highlighter>();
			if (highlight)
			{
				if (!highlighter)
				{
					highlighter = gameObject.AddComponent<Highlighter>();
					highlighter.ConstantOn(constantColor ?? Color.white);
				}
				highlighter.enabled = true;
				highlighter.Hover(hoverColor ?? Color.white);
			}
			else if ((bool)highlighter)
			{
				highlighter.enabled = false;
			}
			if ((bool)highlighter)
			{
				highlighter.ConstantOn(constantColor ?? Color.white);
			}
			foreach (Transform item in gameObject.transform)
			{
				if ((bool)item.GetComponent<DMEditorComponent>())
				{
					SetHighlightObject(item.gameObject, highlight, hoverColor, constantColor);
				}
			}
		}

		public static void SetLayerRecursively(GameObject obj, int newLayer)
		{
			if (obj == null)
			{
				return;
			}
			obj.layer = newLayer;
			foreach (Transform item in obj.transform)
			{
				if (!(item == null))
				{
					SetLayerRecursively(item.gameObject, newLayer);
				}
			}
		}

		public static void DestroyChildren(Transform parent)
		{
			if (parent == null)
			{
				return;
			}
			foreach (Transform item in parent)
			{
				if (item != null)
				{
					UnityEngine.Object.Destroy(item.gameObject);
				}
			}
		}

		public static void LogStopwatch(string function, Stopwatch stopWatch)
		{
			_ = stopWatch.Elapsed;
			UnityEngine.Debug.Log(function + " took " + stopWatch.Elapsed.TotalMilliseconds + "ms");
		}

		public static void CopyTo(Stream src, Stream dest)
		{
			byte[] array = new byte[4096];
			int count;
			while ((count = src.Read(array, 0, array.Length)) != 0)
			{
				dest.Write(array, 0, count);
			}
		}

		public static byte[] Zip(byte[] bytes)
		{
			using (MemoryStream src = new MemoryStream(bytes))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (GZipStream dest = new GZipStream(memoryStream, CompressionMode.Compress))
					{
						CopyTo(src, dest);
					}
					return memoryStream.ToArray();
				}
			}
		}

		public static byte[] Unzip(byte[] bytes)
		{
			using (MemoryStream stream = new MemoryStream(bytes))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					using (GZipStream src = new GZipStream(stream, CompressionMode.Decompress))
					{
						CopyTo(src, memoryStream);
					}
					return memoryStream.ToArray();
				}
			}
		}

		public static AudioPlayer PlaySound(string soundRef, float volumeMultiplier, Transform followTarget)
		{
			return ServiceLocator.GetService<SoundPlayer>().PlaySoundEffect(soundRef, volumeMultiplier, followTarget.position, SoundEffectVariations.MaterialType.Default, followTarget);
		}

		public static AudioPlayer PlaySound(string soundRef, float volumeMultiplier, Vector3 position)
		{
			return ServiceLocator.GetService<SoundPlayer>().PlaySoundEffect(soundRef, volumeMultiplier, position);
		}

		public static void PlayUIClickSound()
		{
			PlaySound("UI/Click", 1f, DMEditor.Instance.playerCamera.transform.position);
		}

		public static void PlayUIHoverSound()
		{
			PlaySound("UI/Hover", 1f, DMEditor.Instance.playerCamera.transform.position);
		}

		private static PlayContinousSound PlayContinousSound(ContinousSoundData soundData, Vector3 position)
		{
			if (string.IsNullOrEmpty(soundData.soundRef) || soundData.loopStart == soundData.loopEnd)
			{
				return null;
			}
			GameObject gameObject = new GameObject();
			gameObject.name = soundData.soundRef;
			gameObject.transform.position = position;
			PlayContinousSound playContinousSound = gameObject.AddComponent<PlayContinousSound>();
			playContinousSound.Play(soundData);
			return playContinousSound;
		}

		public static PlayContinousSound PlayContinousSound(ContinousSoundData soundData, Transform followTarget)
		{
			PlayContinousSound playContinousSound = PlayContinousSound(soundData, followTarget.position);
			if (playContinousSound == null)
			{
				return null;
			}
			playContinousSound.m_followTransform = followTarget;
			return playContinousSound;
		}

		public static float LerpCyclic(float from, float to, float t)
		{
			if (t == 0f)
			{
				return from;
			}
			float num = from;
			if (Mathf.Abs(num - to) >= 0.5f)
			{
				num += ((to > num) ? 1f : (-1f));
			}
			float num2 = Mathf.Lerp(num, to, t);
			float num3 = to - num;
			if (Mathf.Abs(num2 - num) < 0.004901961f && num3 != 0f)
			{
				num2 = ((num3 > 0.003921569f) ? (num2 + 0.004901961f) : ((!(num3 < -0.003921569f)) ? to : (num2 - 0.004901961f)));
			}
			if (num2 < 0f)
			{
				num2 += 1f;
			}
			else if (num2 > 1f)
			{
				num2 -= 1f;
			}
			return num2;
		}

		public static string SplitByUppercaseLetters(string input)
		{
			return new Regex("\r\n                (?<=[A-Z])(?=[A-Z][a-z]) |\r\n                 (?<=[^A-Z])(?=[A-Z]) |\r\n                 (?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace).Replace(input, " ");
		}

		public static IEnumerator FrameDelayCoroutine(UnityAction a, int frameDelays = 1)
		{
			yield return frameDelays;
			a?.Invoke();
		}

		public static StrengthSetting getNextStrength(StrengthSetting strength)
		{
			if (strength != StrengthSetting.highest)
			{
				return strength + 1;
			}
			return StrengthSetting.lowest;
		}

		public static StrengthSetting getPreviousStrength(StrengthSetting strength)
		{
			if (strength != StrengthSetting.lowest)
			{
				return strength + 1;
			}
			return StrengthSetting.highest;
		}

		public static StrengthSetting ToStrengthValue(float strength)
		{
			return (StrengthSetting)Mathf.Clamp(strength * 4f + 0.5f, 0f, 4f);
		}

		public static float FromStrengthValue(StrengthSetting strength)
		{
			return (float)strength / 4f;
		}

		public static void DelayAction(MonoBehaviour owner, System.Action action, int frameDelay = 1)
		{
			owner.StartCoroutine(Delay());
			IEnumerator Delay()
			{
				yield return frameDelay;
				action();
			}
		}

		public static void DelayUntil(MonoBehaviour owner, Func<bool> condition, System.Action action)
		{
			owner.StartCoroutine(Delay());
			IEnumerator Delay()
			{
				yield return new WaitUntil(condition);
				action();
			}
		}
	}
}
