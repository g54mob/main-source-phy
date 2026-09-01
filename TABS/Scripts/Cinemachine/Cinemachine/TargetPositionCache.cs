using System;
using System.Collections.Generic;
using UnityEngine;

namespace Cinemachine
{
	internal class TargetPositionCache
	{
		public enum Mode
		{
			Disabled = 0,
			Record = 1,
			Playback = 2
		}

		private class CacheCurve
		{
			public struct Item
			{
				public Vector3 Pos;

				public Quaternion Rot;

				public static Item Lerp(Item a, Item b, float t)
				{
					return new Item
					{
						Pos = Vector3.LerpUnclamped(a.Pos, b.Pos, t),
						Rot = Quaternion.SlerpUnclamped(a.Rot, b.Rot, t)
					};
				}
			}

			public float StartTime;

			public float StepSize;

			private List<Item> m_Cache;

			public int Count => m_Cache.Count;

			public CacheCurve(float startTime, float endTime, float stepSize)
			{
				StepSize = stepSize;
				StartTime = startTime;
				m_Cache = new List<Item>(Mathf.CeilToInt((endTime - startTime) / StepSize));
			}

			public void Add(Item item, float time)
			{
				if (time < StartTime)
				{
					return;
				}
				int num = m_Cache.Count - 1;
				if (num < 0)
				{
					m_Cache.Add(item);
					return;
				}
				int num2 = Mathf.FloorToInt((time - StartTime) / StepSize);
				float num3 = (float)(num2 - num) + (time - StartTime - (float)num2 * StepSize) / StepSize;
				Item a = m_Cache[num];
				_ = StartTime;
				_ = StepSize;
				for (int i = num + 1; i <= num2; i++)
				{
					m_Cache.Add(Item.Lerp(a, item, (float)(i - num) / num3));
				}
			}

			public Item Evaluate(float time)
			{
				int count = m_Cache.Count;
				if (count == 0)
				{
					return new Item
					{
						Rot = Quaternion.identity
					};
				}
				float num = time - StartTime;
				int num2 = Mathf.Max(Mathf.FloorToInt(num / StepSize), 0);
				if (num2 >= count - 1)
				{
					return m_Cache[count - 1];
				}
				float t = (num - (float)num2 * StepSize) / StepSize;
				return Item.Lerp(m_Cache[num2], m_Cache[num2 + 1], t);
			}
		}

		private class CacheEntry
		{
			private struct RecordingItem : IComparable<RecordingItem>
			{
				public float Time;

				public CacheCurve.Item Item;

				public int CompareTo(RecordingItem other)
				{
					return Time.CompareTo(other.Time);
				}
			}

			public CacheCurve Curve;

			private List<RecordingItem> RawItems = new List<RecordingItem>();

			private RecordingItem LastRawItem;

			public void AddRawItem(float time, Transform target)
			{
				int count = RawItems.Count;
				LastRawItem = new RecordingItem
				{
					Time = time,
					Item = new CacheCurve.Item
					{
						Pos = target.position,
						Rot = target.rotation
					}
				};
				if (count == 0 || Mathf.Abs(RawItems[count - 1].Time - time) >= CacheStepSize)
				{
					RawItems.Add(LastRawItem);
				}
			}

			public void CreateCurves()
			{
				RawItems.Sort();
				int count = RawItems.Count;
				float startTime = ((count == 0) ? 0f : RawItems[0].Time);
				float endTime = ((count == 0) ? 0f : LastRawItem.Time);
				Curve = new CacheCurve(startTime, endTime, CacheStepSize);
				RecordingItem recordingItem = new RecordingItem
				{
					Time = float.MaxValue
				};
				for (int i = 0; i < count; i++)
				{
					RecordingItem recordingItem2 = RawItems[i];
					if (!(Mathf.Abs(recordingItem2.Time - recordingItem.Time) < CacheStepSize))
					{
						Curve.Add(recordingItem2.Item, recordingItem2.Time);
						recordingItem = recordingItem2;
					}
				}
				if (count > 0)
				{
					float num = LastRawItem.Time - recordingItem.Time;
					float num2 = CacheStepSize * 1.9f;
					if (num > 0.0001f)
					{
						Curve.Add(CacheCurve.Item.Lerp(recordingItem.Item, LastRawItem.Item, num2 / num), recordingItem.Time + num2);
					}
				}
				RawItems.Clear();
			}
		}

		public struct TimeRange
		{
			public float Start;

			public float End;

			public bool IsEmpty => End < Start;

			public static TimeRange Empty => new TimeRange
			{
				Start = float.MaxValue,
				End = float.MinValue
			};

			public bool Contains(float time)
			{
				if (time >= Start)
				{
					return time <= End;
				}
				return false;
			}

			public void Include(float time)
			{
				Start = Mathf.Min(Start, time);
				End = Mathf.Max(End, time);
			}
		}

		public static int kMaxResolution = 5;

		private static Mode m_CacheMode = Mode.Disabled;

		private static Dictionary<Transform, CacheEntry> m_Cache;

		private static TimeRange m_CacheTimeRange;

		private const float kWraparoundSlush = 0.1f;

		public static bool UseCache { get; set; }

		public static int Resolution { get; set; }

		public static float CacheStepSize => (float)kMaxResolution / (Mathf.Max(1f, Resolution) * 60f);

		public static Mode CacheMode
		{
			get
			{
				return m_CacheMode;
			}
			set
			{
				if (value != m_CacheMode)
				{
					m_CacheMode = value;
					switch (m_CacheMode)
					{
					default:
						ClearCache();
						break;
					case Mode.Record:
						InitCache();
						break;
					case Mode.Playback:
						CreatePlaybackCurves();
						break;
					}
				}
			}
		}

		public static bool IsRecording
		{
			get
			{
				if (UseCache)
				{
					return m_CacheMode == Mode.Record;
				}
				return false;
			}
		}

		public static bool CurrentPlaybackTimeValid
		{
			get
			{
				if (UseCache && m_CacheMode == Mode.Playback)
				{
					return HasHurrentTime;
				}
				return false;
			}
		}

		public static float CurrentTime { get; set; }

		public static TimeRange CacheTimeRange => m_CacheTimeRange;

		public static bool HasHurrentTime => m_CacheTimeRange.Contains(CurrentTime);

		private static void ClearCache()
		{
			m_Cache = null;
			m_CacheTimeRange = TimeRange.Empty;
		}

		private static void InitCache()
		{
			m_Cache = new Dictionary<Transform, CacheEntry>();
			m_CacheTimeRange = TimeRange.Empty;
		}

		private static void CreatePlaybackCurves()
		{
			if (m_Cache == null)
			{
				m_Cache = new Dictionary<Transform, CacheEntry>();
			}
			Dictionary<Transform, CacheEntry>.Enumerator enumerator = m_Cache.GetEnumerator();
			while (enumerator.MoveNext())
			{
				enumerator.Current.Value.CreateCurves();
			}
		}

		public static Vector3 GetTargetPosition(Transform target)
		{
			if (!UseCache || CacheMode == Mode.Disabled)
			{
				return target.position;
			}
			if (CacheMode == Mode.Record && !m_CacheTimeRange.IsEmpty && CurrentTime < m_CacheTimeRange.Start - 0.1f)
			{
				ClearCache();
				InitCache();
			}
			if (CacheMode == Mode.Playback && !HasHurrentTime)
			{
				return target.position;
			}
			if (!m_Cache.TryGetValue(target, out var value))
			{
				if (CacheMode != Mode.Record)
				{
					return target.position;
				}
				value = new CacheEntry();
				m_Cache.Add(target, value);
			}
			if (CacheMode == Mode.Record)
			{
				if (m_CacheTimeRange.End <= CurrentTime)
				{
					value.AddRawItem(CurrentTime, target);
					m_CacheTimeRange.Include(CurrentTime);
				}
				return target.position;
			}
			if (value.Curve == null)
			{
				return target.position;
			}
			return value.Curve.Evaluate(CurrentTime).Pos;
		}

		public static Quaternion GetTargetRotation(Transform target)
		{
			if (CacheMode == Mode.Disabled)
			{
				return target.rotation;
			}
			if (CacheMode == Mode.Record && !m_CacheTimeRange.IsEmpty && CurrentTime < m_CacheTimeRange.Start - 0.1f)
			{
				ClearCache();
				InitCache();
			}
			if (CacheMode == Mode.Playback && !HasHurrentTime)
			{
				return target.rotation;
			}
			if (!m_Cache.TryGetValue(target, out var value))
			{
				if (CacheMode != Mode.Record)
				{
					return target.rotation;
				}
				value = new CacheEntry();
				m_Cache.Add(target, value);
			}
			if (CacheMode == Mode.Record)
			{
				if (m_CacheTimeRange.End <= CurrentTime)
				{
					value.AddRawItem(CurrentTime, target);
					m_CacheTimeRange.Include(CurrentTime);
				}
				return target.rotation;
			}
			return value.Curve.Evaluate(CurrentTime).Rot;
		}
	}
}
