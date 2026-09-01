using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Poly.Base;
using Poly.Math;
using UnityEngine;
using UnityEngine.UI;

namespace Poly.Timers
{
	public static class PerformanceTimerDisplay
	{
		private class DisplayRow
		{
			public TimerId id;

			public uint totalTicks;

			public int numCalls;

			private int nextHistorySlot;

			private uint[] history = new uint[100];

			private uint numSamples;

			public uint avgTicks;

			public uint maxTicks;

			public DisplayRow parent;

			public int indexInParent = -1;

			public List<DisplayRow> children = new List<DisplayRow>();

			public DisplayRow FindOrAddRow(TimerId id, bool forceAdd = false)
			{
				DisplayRow displayRow = null;
				foreach (DisplayRow child in children)
				{
					if (child.id == id)
					{
						displayRow = child;
						break;
					}
				}
				if (forceAdd || displayRow == null)
				{
					DisplayRow displayRow2 = new DisplayRow
					{
						id = id,
						parent = this,
						indexInParent = children.Count
					};
					children.Add(displayRow2);
					displayRow = displayRow2;
				}
				return displayRow;
			}

			public void ClearFrameDataUnderLocalRoot(TimerId localRoot, bool doClearNow = false)
			{
				if (!doClearNow && id == localRoot)
				{
					doClearNow = true;
				}
				if (doClearNow)
				{
					totalTicks = 0u;
					numCalls = 0;
				}
				foreach (DisplayRow child in children)
				{
					child.ClearFrameDataUnderLocalRoot(localRoot, doClearNow);
				}
			}

			public void StoreFrameDataUnderLocalRoot(TimerId localRoot, bool doStoreNow = false)
			{
				if (!doStoreNow && id == localRoot)
				{
					doStoreNow = true;
				}
				if (doStoreNow)
				{
					maxTicks = System.Math.Max(maxTicks, totalTicks);
					history[nextHistorySlot++] = totalTicks;
					nextHistorySlot %= history.Length;
					if (numSamples < history.Length)
					{
						numSamples++;
					}
					avgTicks = 0u;
					uint[] array = history;
					foreach (uint num in array)
					{
						avgTicks += num;
					}
					avgTicks /= numSamples;
				}
				foreach (DisplayRow child in children)
				{
					child.StoreFrameDataUnderLocalRoot(localRoot, doStoreNow);
				}
			}

			public void ResetMax()
			{
				maxTicks = 0u;
				foreach (DisplayRow child in children)
				{
					child.ResetMax();
				}
			}

			public static implicit operator bool(DisplayRow row)
			{
				return row != null;
			}
		}

		private static DisplayRow persistentRoot;

		private static bool bufferOverflownMessageLogged;

		private static int numGathersToIgnore = 0;

		private static Stack<TimerInfo> beginInfos = new Stack<TimerInfo>();

		private static FastListClass<StringBuilder> lines = new FastListClass<StringBuilder>();

		private static Dictionary<TimerId, string> timerIdNames = new Dictionary<TimerId, string>();

		private static List<(DisplayRow, int)> rowsAndIndents = new List<(DisplayRow, int)>();

		private static StringBuilder fullText = new StringBuilder(7029);

		private static float avgNumFixedStepsPerFrame = 1f;

		private static float graphicsFps = 1f;

		private static float physicsFps = 1f;

		private static float realizedPhysicsFpsPercentage;

		private static float realtimeSinceStart = 0f;

		private static float simTimeUnscaledElapsed;

		private static float gameTimeElapsed;

		private static float simStartInRealTime;

		private static GUIStyle style;

		private static char[] buffor = new char[20];

		public static void Clear()
		{
			persistentRoot = null;
			simTimeUnscaledElapsed = 0f;
			gameTimeElapsed = 0f;
			simStartInRealTime = Time.realtimeSinceStartup;
			realizedPhysicsFpsPercentage = 100f;
		}

		public static void ResetMax()
		{
			if ((bool)persistentRoot)
			{
				persistentRoot.ResetMax();
			}
		}

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void RuntimeInitialize()
		{
			bufferOverflownMessageLogged = false;
			numGathersToIgnore = 0;
			beginInfos = new Stack<TimerInfo>();
			lines = new FastListClass<StringBuilder>();
			timerIdNames = new Dictionary<TimerId, string>();
			rowsAndIndents = new List<(DisplayRow, int)>();
			fullText = new StringBuilder(7029);
			avgNumFixedStepsPerFrame = 1f;
			graphicsFps = 1f;
			physicsFps = 1f;
			realizedPhysicsFpsPercentage = 0f;
			realtimeSinceStart = 0f;
			simTimeUnscaledElapsed = 0f;
			gameTimeElapsed = 0f;
			simStartInRealTime = 0f;
			style = null;
			buffor = new char[20];
		}

		public static void Gather(TimerInfo[] infos, int nextFreeIndex, bool concatenate = true, TimerId localRoot = TimerId.TimerRoot)
		{
			if (nextFreeIndex > 6)
			{
				if (!bufferOverflownMessageLogged)
				{
					bufferOverflownMessageLogged = true;
					UnityEngine.Debug.Log("--- Timers buffer overflown ---");
				}
				return;
			}
			Stack<TimerInfo> stack = beginInfos;
			if (!persistentRoot)
			{
				persistentRoot = new DisplayRow();
				persistentRoot.id = TimerId.TimerRoot;
			}
			persistentRoot.ClearFrameDataUnderLocalRoot(localRoot);
			DisplayRow displayRow = persistentRoot;
			for (int i = 0; i < nextFreeIndex; i++)
			{
				ref TimerInfo reference = ref infos[i];
				if (reference.isBeginning)
				{
					stack.Push(reference);
					displayRow = displayRow.FindOrAddRow(reference.id, !concatenate);
					continue;
				}
				TimerInfo timerInfo = stack.Pop();
				if (reference.id == TimerId.MatchLast)
				{
					reference.id = timerInfo.id;
				}
				_ = timerInfo.id;
				_ = reference.id;
				uint num = reference.timestamp - timerInfo.timestamp;
				displayRow.totalTicks += num;
				displayRow.numCalls++;
				displayRow = displayRow.parent;
				if (reference.id == localRoot)
				{
					stack.Clear();
					break;
				}
			}
			stack.Clear();
			if (numGathersToIgnore <= 1)
			{
				persistentRoot.StoreFrameDataUnderLocalRoot(localRoot);
			}
			if (localRoot == TimerId.Invalid)
			{
				numGathersToIgnore--;
			}
		}

		public static string Display(int nextFreeIndex, int numFixedFramesPlayed, bool showMax = true, bool showRealtime = false)
		{
			if (nextFreeIndex > 6)
			{
				return "--- Timers buffer overflown ---";
			}
			long frequency = Stopwatch.Frequency;
			long num = 1000000000 / frequency;
			DisplayRow displayRow = persistentRoot;
			DisplayRow displayRow2 = displayRow;
			if (displayRow.children.Count == 2 && displayRow.children[0].id == TimerId.GraphicsFrameEndToEnd && displayRow.children[1].id == TimerId.FixedFrameEndToEnd)
			{
				DisplayRow value = displayRow.children[0];
				displayRow.children[0] = displayRow.children[1];
				displayRow.children[1] = value;
				displayRow.children[0].indexInParent = 0;
				displayRow.children[1].indexInParent = 1;
			}
			int num2 = 0;
			int num3 = 0;
			displayRow2 = ((displayRow.children.Count > 0) ? displayRow.children[0] : null);
			while (displayRow2 != null)
			{
				if (!timerIdNames.TryGetValue(displayRow2.id, out var value2))
				{
					value2 = displayRow2.id.ToString();
					timerIdNames.Add(displayRow2.id, value2);
				}
				int val = 2 * num3 + value2.Length;
				num2 = System.Math.Max(num2, val);
				rowsAndIndents.Add((displayRow2, num3));
				if (displayRow2.children.Count > 0)
				{
					num3++;
					displayRow2 = displayRow2.children[0];
					continue;
				}
				while (displayRow2.parent != null)
				{
					if (displayRow2.indexInParent + 1 < displayRow2.parent.children.Count)
					{
						displayRow2 = displayRow2.parent.children[displayRow2.indexInParent + 1];
						break;
					}
					num3--;
					displayRow2 = displayRow2.parent;
					if (displayRow2.parent == null)
					{
						displayRow2 = null;
						break;
					}
				}
			}
			float num4 = Time.timeScale / Time.fixedDeltaTime;
			float num5 = Time.realtimeSinceStartup - realtimeSinceStart + 5.877472E-39f;
			realtimeSinceStart = Time.realtimeSinceStartup;
			if (Time.timeScale != 0f)
			{
				simTimeUnscaledElapsed += (float)numFixedFramesPlayed * Time.fixedDeltaTime / Time.timeScale;
				gameTimeElapsed += Time.deltaTime / Time.timeScale;
			}
			else
			{
				simStartInRealTime += num5;
			}
			avgNumFixedStepsPerFrame = Smoothing.Smooth(avgNumFixedStepsPerFrame, numFixedFramesPlayed, 0.8f, num5);
			graphicsFps = Smoothing.Smooth(graphicsFps, 1f / num5, 0.8f, num5);
			physicsFps = Smoothing.Smooth(physicsFps, (float)numFixedFramesPlayed / num5, 0.8f, num5);
			float target = ((Time.timeScale != 0f) ? (physicsFps / num4 * 100f) : 100f);
			realizedPhysicsFpsPercentage = Smoothing.Smooth(realizedPhysicsFpsPercentage, target, 0.8f, num5);
			fullText.Clear();
			if (Time.timeScale != 0f)
			{
				fullText.Append("-- Fixed step budget ");
				fullText.AppendFloatNoAlloc(Time.fixedDeltaTime / Time.timeScale * 1000f, 1, 5).Append(" ms, running ");
				fullText.AppendFloatNoAlloc(avgNumFixedStepsPerFrame, 2, 5).Append(" steps/frame --\r\n");
			}
			else
			{
				fullText.Append("-- Timers stopped for fixed update                       --\r\n");
			}
			fullText.Append("-- Gfx Fps: ").AppendFloatNoAlloc(graphicsFps, 0, 3).Append("    Phys steps: ")
				.AppendFloatNoAlloc(physicsFps, 0, 3)
				.Append(" / ")
				.AppendFloatNoAlloc(num4, 0, 3)
				.Append(" (")
				.AppendFloatNoAlloc(realizedPhysicsFpsPercentage, 0, 3)
				.Append("%)          --\r\n");
			float num6 = Time.realtimeSinceStartup - simStartInRealTime;
			fullText.Append("-- Real time:").AppendFloatNoAlloc(num6, 2, 7).Append("   ∆Game:")
				.AppendFloatNoAlloc(num6 - gameTimeElapsed, 3, 6)
				.Append("   ∆Sim:")
				.AppendFloatNoAlloc(num6 - gameTimeElapsed, 3, 6)
				.Append("        --\r\n");
			if (0 < rowsAndIndents.Count)
			{
				fullText.Append(' ', num2).AppendFormat("    AVG    #calls{0}{1}\r\n", showMax ? "      MAX" : "", showRealtime ? "     RLT" : "");
			}
			int num7 = 0;
			foreach (var (displayRow3, num8) in rowsAndIndents)
			{
				if (num8 == 0 && 0 < num7++ && 2 < rowsAndIndents.Count)
				{
					fullText.Append("\r\n");
				}
				if (num8 == 0)
				{
					fullText.Append("<i>");
				}
				displayRow2 = displayRow3;
				string text = timerIdNames[displayRow2.id];
				fullText.Append(' ', 2 * num8).Append(text).Append((num8 == 0) ? '-' : ' ', Mathf.Max(0, num2 - 2 * num8 - text.Length));
				int num9 = (int)(num * displayRow2.totalTicks / 1000);
				int value3 = (int)(num * displayRow2.avgTicks / 1000);
				int num10 = (int)(num * displayRow2.maxTicks / 1000);
				fullText.AppendIntNoAlloc(value3, groupSeparator: true, 7);
				fullText.Append(" μs (");
				fullText.AppendIntNoAlloc(displayRow2.numCalls);
				if (showMax || showRealtime)
				{
					fullText.Append(")");
					fullText.Append(' ', Mathf.Max(0, 3 - System.Math.Max(0, (int)System.Math.Log10(displayRow2.numCalls))));
					if (showMax)
					{
						fullText.AppendFormat("{0,9:#,##0}", num10);
					}
					if (showRealtime)
					{
						fullText.AppendFormat("{0,8:#,##0}", num9);
					}
					fullText.Append("\r\n");
				}
				else
				{
					fullText.Append(")\r\n");
				}
				if (num8 == 0)
				{
					fullText.Append("</i>");
				}
			}
			rowsAndIndents.Clear();
			string result = fullText.ToString();
			for (int i = 0; i < lines.Count; i++)
			{
				lines[i].Clear();
			}
			lines.Clear();
			return result;
		}

		private static GUIStyle GetStyle(Text text)
		{
			return new GUIStyle
			{
				alignment = TextAnchor.UpperLeft,
				fontSize = text.fontSize,
				fontStyle = text.fontStyle,
				font = text.font
			};
		}

		public static void ResizeTimersPanel(Text text)
		{
			Rect rect = default(Rect);
			Vec2 textDimensions = GetTextDimensions(text.text);
			Vec2 vec = new Vec2(9f, 18.923077f);
			Vec2 vec2 = new Vec2(textDimensions.x * vec.x, textDimensions.y * vec.y);
			vec2 += 20f * Vec2.one;
			rect.size = vec2;
			RectTransform obj = text.rectTransform.parent as RectTransform;
			obj.sizeDelta = rect.size;
			obj.anchoredPosition = new Vector2(5f, -3f / 44f * (float)Screen.height);
		}

		private static Vec2 GetTextDimensions(string text)
		{
			int b = 0;
			int num = 0;
			int num2 = 1;
			bool flag = false;
			for (int i = 0; i < text.Length; i++)
			{
				if (flag)
				{
					if (text[i] == '>')
					{
						flag = false;
					}
					continue;
				}
				switch (text[i])
				{
				case '\r':
					b = Mathf.Max(num, b);
					num2++;
					num = 0;
					break;
				case '<':
					flag = true;
					break;
				case '>':
					flag = false;
					break;
				default:
					num++;
					break;
				case '\n':
					break;
				}
			}
			b = Mathf.Max(num, b);
			if (num == 0)
			{
				num2--;
			}
			return new Vec2(b, num2);
		}

		private static (char[], int, int) IntToStr(int value, bool groupSeparator = false, int padLeft = 0, char paddingCharacter = ' ')
		{
			int num = buffor.Length;
			int i = 0;
			bool flag = false;
			if (value < 0)
			{
				flag = true;
				value = -value;
			}
			if (value == 0)
			{
				buffor[--num] = '0';
				i++;
			}
			while (0 < value)
			{
				if (groupSeparator && (i + 1) % 4 == 0)
				{
					buffor[--num] = ',';
					i++;
				}
				buffor[--num] = (char)(value % 10 + 48);
				i++;
				value /= 10;
			}
			if (flag)
			{
				buffor[--num] = '-';
				i++;
			}
			for (; i < padLeft; i++)
			{
				buffor[--num] = paddingCharacter;
			}
			return (buffor, num, i);
		}

		private static StringBuilder AppendIntNoAlloc(this StringBuilder builder, int value, bool groupSeparator = false, int padLeft = 0, char paddingCharacter = ' ')
		{
			(char[], int, int) tuple = IntToStr(value, groupSeparator, padLeft, paddingCharacter);
			return builder.Append(tuple.Item1, tuple.Item2, tuple.Item3);
		}

		private static StringBuilder AppendFloatNoAlloc(this StringBuilder builder, float value, int numDecimalPlaces = 0, int padLeft = 0)
		{
			int padLeft2 = ((numDecimalPlaces <= 0) ? padLeft : (padLeft - 1 - numDecimalPlaces));
			int num = 1;
			if (value < 0f)
			{
				num = -1;
				value = 0f - value;
			}
			builder.AppendIntNoAlloc(num * Mathf.FloorToInt(value), groupSeparator: false, padLeft2);
			if (0 < numDecimalPlaces)
			{
				int num2 = 1;
				for (int i = 0; i < numDecimalPlaces && i < 10; i++)
				{
					num2 *= 10;
				}
				builder.Append(".");
				builder.AppendIntNoAlloc(Mathf.FloorToInt(value * (float)num2) % num2, groupSeparator: false, numDecimalPlaces, '0');
			}
			return builder;
		}
	}
}
