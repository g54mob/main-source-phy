using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using FluffyUnderware.DevTools;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Build/Volume Spots", ModuleName = "Volume Spots", Description = "Generate spots along a path/volume", UsesRandom = true)]
	[HelpURL("https://curvyeditor.com/doclink/cgvolumespots")]
	public class BuildVolumeSpots : CGModule
	{
		private class GroupSet
		{
			public CGBoundsGroup Group;

			public float Length;

			public List<int> Items = new List<int>();

			public List<float> Distances = new List<float>();
		}

		[HideInInspector]
		[InputSlotInfo(new Type[] { typeof(CGPath) }, Name = "Path/Volume", DisplayName = "Volume/Rasterized Path")]
		public CGModuleInputSlot InPath = new CGModuleInputSlot();

		[HideInInspector]
		[InputSlotInfo(new Type[] { typeof(CGBounds) }, Array = true)]
		public CGModuleInputSlot InBounds = new CGModuleInputSlot();

		[HideInInspector]
		[OutputSlotInfo(typeof(CGSpots))]
		public CGModuleOutputSlot OutSpots = new CGModuleOutputSlot();

		[Tab("General")]
		[FloatRegion(RegionOptionsPropertyName = "RangeOptions", Precision = 4)]
		[SerializeField]
		private FloatRegion m_Range = FloatRegion.ZeroOne;

		[Tooltip("When the source is a Volume, you can choose if you want to use it's path or the volume")]
		[FieldCondition("Volume", null, true, ActionAttribute.ActionEnum.Show, null, ActionAttribute.ActionPositionEnum.Below)]
		[SerializeField]
		private bool m_UseVolume;

		[Tooltip("Dry run without actually creating spots?")]
		[SerializeField]
		private bool m_Simulate;

		[Section("Default/General/Cross", true, false, 100)]
		[SerializeField]
		[RangeEx(-1f, 1f, "", "")]
		private float m_CrossBase;

		[SerializeField]
		private AnimationCurve m_CrossCurve = AnimationCurve.Linear(0f, 0f, 1f, 0f);

		[Tab("Groups")]
		[ArrayEx(Space = 10)]
		[SerializeField]
		private List<CGBoundsGroup> m_Groups = new List<CGBoundsGroup>();

		[IntRegion(UseSlider = false, RegionOptionsPropertyName = "RepeatingGroupsOptions", Options = AttributeOptionsFlags.Compact)]
		[SerializeField]
		private IntRegion m_RepeatingGroups;

		[SerializeField]
		private CurvyRepeatingOrderEnum m_RepeatingOrder = CurvyRepeatingOrderEnum.Row;

		[SerializeField]
		private bool m_FitEnd;

		public CGSpots SimulatedSpots;

		private WeightedRandom<int> mGroupBag;

		private List<CGBounds> mBounds;

		public FloatRegion Range
		{
			get
			{
				return m_Range;
			}
			set
			{
				if (m_Range != value)
				{
					m_Range = value;
				}
				base.Dirty = true;
			}
		}

		public bool UseVolume
		{
			get
			{
				return m_UseVolume;
			}
			set
			{
				if (m_UseVolume != value)
				{
					m_UseVolume = value;
				}
				base.Dirty = true;
			}
		}

		public bool Simulate
		{
			get
			{
				return m_Simulate;
			}
			set
			{
				if (m_Simulate != value)
				{
					m_Simulate = value;
				}
				base.Dirty = true;
			}
		}

		public float CrossBase
		{
			get
			{
				return m_CrossBase;
			}
			set
			{
				float num = Mathf.Repeat(value, 1f);
				if (m_CrossBase != num)
				{
					m_CrossBase = num;
				}
				base.Dirty = true;
			}
		}

		public AnimationCurve CrossCurve
		{
			get
			{
				return m_CrossCurve;
			}
			set
			{
				if (m_CrossCurve != value)
				{
					m_CrossCurve = value;
				}
				base.Dirty = true;
			}
		}

		public List<CGBoundsGroup> Groups
		{
			get
			{
				return m_Groups;
			}
			set
			{
				if (m_Groups != value)
				{
					m_Groups = value;
				}
			}
		}

		public CurvyRepeatingOrderEnum RepeatingOrder
		{
			get
			{
				return m_RepeatingOrder;
			}
			set
			{
				if (m_RepeatingOrder != value)
				{
					m_RepeatingOrder = value;
				}
				base.Dirty = true;
			}
		}

		public int FirstRepeating
		{
			get
			{
				return m_RepeatingGroups.From;
			}
			set
			{
				int num = Mathf.Clamp(value, 0, Mathf.Max(0, GroupCount - 1));
				if (m_RepeatingGroups.From != num)
				{
					m_RepeatingGroups.From = num;
				}
				base.Dirty = true;
			}
		}

		public int LastRepeating
		{
			get
			{
				return m_RepeatingGroups.To;
			}
			set
			{
				int num = Mathf.Clamp(value, FirstRepeating, Mathf.Max(0, GroupCount - 1));
				if (m_RepeatingGroups.To != num)
				{
					m_RepeatingGroups.To = num;
				}
				base.Dirty = true;
			}
		}

		public bool FitEnd
		{
			get
			{
				return m_FitEnd;
			}
			set
			{
				if (m_FitEnd != value)
				{
					m_FitEnd = value;
				}
				base.Dirty = true;
			}
		}

		public int GroupCount => Groups.Count;

		public GUIContent[] BoundsNames
		{
			get
			{
				if (mBounds == null)
				{
					return new GUIContent[0];
				}
				GUIContent[] array = new GUIContent[mBounds.Count];
				for (int i = 0; i < mBounds.Count; i++)
				{
					array[i] = new GUIContent(string.Format(CultureInfo.InvariantCulture, "{0}:{1}", i.ToString(CultureInfo.InvariantCulture), mBounds[i].Name));
				}
				return array;
			}
		}

		public int[] BoundsIndices
		{
			get
			{
				if (mBounds == null)
				{
					return new int[0];
				}
				int[] array = new int[mBounds.Count];
				for (int i = 0; i < mBounds.Count; i++)
				{
					array[i] = i;
				}
				return array;
			}
		}

		public int Count { get; private set; }

		private int lastGroupIndex => Mathf.Max(0, GroupCount - 1);

		private RegionOptions<float> RangeOptions => RegionOptions<float>.MinMax(0f, 1f);

		private RegionOptions<int> RepeatingGroupsOptions => RegionOptions<int>.MinMax(0, Mathf.Max(0, GroupCount - 1));

		private CGPath Path { get; set; }

		private CGVolume Volume => Path as CGVolume;

		private float Length
		{
			get
			{
				if (Path == null)
				{
					return 0f;
				}
				return Path.Length * m_Range.Length;
			}
		}

		private float StartDistance { get; set; }

		protected override void OnEnable()
		{
			base.OnEnable();
			Properties.MinWidth = 350f;
		}

		public override void Reset()
		{
			base.Reset();
			m_Range = FloatRegion.ZeroOne;
			UseVolume = false;
			Simulate = false;
			CrossBase = 0f;
			CrossCurve = AnimationCurve.Linear(0f, 0f, 1f, 0f);
			RepeatingOrder = CurvyRepeatingOrderEnum.Row;
			FirstRepeating = 0;
			LastRepeating = 0;
			FitEnd = false;
			Groups.Clear();
			AddGroup("Group");
		}

		public override void OnStateChange()
		{
			base.OnStateChange();
			if (!IsConfigured)
			{
				Clear();
			}
		}

		public void Clear()
		{
			Count = 0;
			SimulatedSpots = new CGSpots();
			OutSpots.SetData(SimulatedSpots);
		}

		public override void Refresh()
		{
			base.Refresh();
			mBounds = InBounds.GetAllData<CGBounds>(Array.Empty<CGDataRequestParameter>());
			bool flag = false;
			for (int i = 0; i < mBounds.Count; i++)
			{
				CGBounds cGBounds = mBounds[i];
				if (cGBounds is CGGameObject && ((CGGameObject)cGBounds).Object == null)
				{
					flag = true;
					UIMessages.Add($"Input object of index {i} has no Game Object attached to it. Correct this to enable spots generation.");
				}
				else if (cGBounds.Depth <= 0.01f)
				{
					CGBounds cGBounds2 = new CGBounds(cGBounds);
					UIMessages.Add($"Input object \"{cGBounds2.Name}\" has bounds with a depth of {cGBounds.Depth}. The minimal accepted depth is {0.01f}. The depth value was overriden.");
					cGBounds2.Bounds = new Bounds(cGBounds.Bounds.center, new Vector3(cGBounds.Bounds.size.x, cGBounds.Bounds.size.y, 0.01f));
					mBounds[i] = cGBounds2;
				}
			}
			if (mBounds.Count == 0)
			{
				flag = true;
				UIMessages.Add("The input bounds list is empty. Add some to enable spots generation.");
			}
			foreach (CGBoundsGroup group in Groups)
			{
				if (group.ItemCount == 0)
				{
					flag = true;
					UIMessages.Add($"Group \"{group.Name}\" has 0 item in it. Add some to enable spots generation.");
					continue;
				}
				foreach (CGBoundsGroupItem item in group.Items)
				{
					int index = item.Index;
					if (index < 0 || index >= mBounds.Count)
					{
						flag = true;
						UIMessages.Add($"Group \"{group.Name}\" has a reference to an inexistent item of index {index}. Correct the reference to enable spots generation.");
						break;
					}
				}
			}
			Path = InPath.GetData<CGPath>(Array.Empty<CGDataRequestParameter>());
			if (Path != null && Volume == null && UseVolume)
			{
				m_UseVolume = false;
			}
			List<CGSpot> spots = new List<CGSpot>();
			List<GroupSet> sets = null;
			prepare();
			if ((bool)Path && !flag)
			{
				float remainingLength = Length;
				StartDistance = Path.FToDistance(m_Range.Low);
				float currentDistance = StartDistance;
				for (int j = 0; j < FirstRepeating; j++)
				{
					addGroupItems(Groups[j], ref spots, ref remainingLength, ref currentDistance);
					if (remainingLength <= 0f)
					{
						break;
					}
				}
				if (GroupCount - LastRepeating - 1 > 0)
				{
					sets = new List<GroupSet>();
					float currentDistance2 = 0f;
					for (int k = LastRepeating + 1; k < GroupCount; k++)
					{
						sets.Add(addGroupItems(Groups[k], ref spots, ref remainingLength, ref currentDistance2, calcLengthOnly: true));
					}
				}
				bool flag2 = false;
				if (RepeatingOrder == CurvyRepeatingOrderEnum.Row)
				{
					int firstRepeating = FirstRepeating;
					while (remainingLength > 0f)
					{
						addGroupItems(Groups[firstRepeating++], ref spots, ref remainingLength, ref currentDistance);
						if (firstRepeating > LastRepeating)
						{
							firstRepeating = FirstRepeating;
						}
						if (spots.Count >= 10000)
						{
							flag2 = true;
							break;
						}
					}
				}
				else
				{
					while (remainingLength > 0f)
					{
						addGroupItems(Groups[mGroupBag.Next()], ref spots, ref remainingLength, ref currentDistance);
						if (spots.Count >= 10000)
						{
							flag2 = true;
							break;
						}
					}
				}
				if (flag2)
				{
					string text = $"Number of generated spots reached the maximal allowed number, which is {10000}. Spots generation was stopped. Try to reduce the number of spots needed by using bigger Bounds as inputs and/or setting bigger space between two spots.";
					UIMessages.Add(text);
					DTLog.LogError("[Curvy] Volume spots: " + text);
				}
				if (sets != null)
				{
					rebase(ref spots, ref sets, currentDistance);
				}
			}
			Count = spots.Count;
			SimulatedSpots = new CGSpots(spots);
			if (Simulate)
			{
				OutSpots.SetData(new CGSpots());
			}
			else
			{
				OutSpots.SetData(SimulatedSpots);
			}
		}

		public CGBoundsGroup AddGroup(string name)
		{
			CGBoundsGroup cGBoundsGroup = new CGBoundsGroup(name);
			cGBoundsGroup.Items.Add(new CGBoundsGroupItem());
			Groups.Add(cGBoundsGroup);
			base.Dirty = true;
			return cGBoundsGroup;
		}

		public void RemoveGroup(CGBoundsGroup group)
		{
			Groups.Remove(group);
			base.Dirty = true;
		}

		private GroupSet addGroupItems(CGBoundsGroup group, ref List<CGSpot> spots, ref float remainingLength, ref float currentDistance, bool calcLengthOnly = false)
		{
			for (int i = 0; i < group.ItemCount; i++)
			{
				getItemBounds(group.Items[i].Index);
			}
			int num = 0;
			float next = group.SpaceBefore.Next;
			float next2 = group.SpaceAfter.Next;
			float num2 = remainingLength - next;
			GroupSet groupSet = null;
			GroupSet groupSet2 = new GroupSet();
			float num3 = currentDistance + next;
			if (calcLengthOnly)
			{
				groupSet = new GroupSet();
				groupSet.Group = group;
				groupSet.Length = next + next2;
			}
			for (int j = 0; j < group.FirstRepeating; j++)
			{
				int index = group.Items[j].Index;
				CGBounds bounds = getItemBounds(index);
				num2 -= bounds.Depth;
				if (num2 > 0f)
				{
					if (calcLengthOnly)
					{
						groupSet.Length += bounds.Depth;
						groupSet.Items.Add(index);
						groupSet.Distances.Add(num3);
					}
					else
					{
						spots.Add(getSpot(index, ref group, ref bounds, num3));
					}
					num3 += bounds.Depth;
					num++;
					continue;
				}
				if (group.KeepTogether && num > 0)
				{
					spots.RemoveRange(spots.Count - num, num);
				}
				break;
			}
			if (num2 > 0f)
			{
				float num4 = 0f;
				for (int k = group.LastRepeating + 1; k < group.ItemCount; k++)
				{
					int index = group.Items[k].Index;
					CGBounds bounds = getItemBounds(index);
					num2 -= bounds.Depth;
					if (!(num2 > 0f))
					{
						break;
					}
					groupSet2.Length += bounds.Depth;
					groupSet2.Items.Add(index);
					groupSet2.Distances.Add(num4);
					num4 += bounds.Depth;
				}
				if (num2 > 0f)
				{
					for (int l = group.FirstRepeating; l <= group.LastRepeating; l++)
					{
						int index2 = ((group.RepeatingOrder == CurvyRepeatingOrderEnum.Row) ? l : group.getRandomItemINTERNAL());
						int index = group.Items[index2].Index;
						CGBounds bounds = getItemBounds(index);
						num2 -= bounds.Depth;
						if (num2 > 0f)
						{
							if (calcLengthOnly)
							{
								groupSet.Length += bounds.Depth;
								groupSet.Items.Add(index);
								groupSet.Distances.Add(num3);
							}
							else
							{
								spots.Add(getSpot(index, ref group, ref bounds, num3));
							}
							num3 += bounds.Depth;
							num++;
							continue;
						}
						if (group.KeepTogether && num > 0)
						{
							spots.RemoveRange(spots.Count - num, num);
						}
						break;
					}
				}
				if (num2 > 0f || !group.KeepTogether)
				{
					for (int m = 0; m < groupSet2.Items.Count; m++)
					{
						CGBounds bounds2 = getItemBounds(groupSet2.Items[m]);
						spots.Add(getSpot(groupSet2.Items[m], ref group, ref bounds2, num3 + groupSet2.Distances[m]));
						num3 += bounds2.Depth;
					}
				}
			}
			remainingLength = num2 - next2;
			currentDistance = num3 + next2;
			return groupSet;
		}

		private void rebase(ref List<CGSpot> spots, ref List<GroupSet> sets, float currentDistance)
		{
			if (FitEnd)
			{
				currentDistance = Path.FToDistance(m_Range.To);
				for (int i = 0; i < sets.Count; i++)
				{
					currentDistance -= sets[i].Length;
				}
			}
			for (int j = 0; j < sets.Count; j++)
			{
				GroupSet groupSet = sets[j];
				for (int k = 0; k < groupSet.Items.Count; k++)
				{
					CGBounds bounds = getItemBounds(groupSet.Items[k]);
					spots.Add(getSpot(groupSet.Items[k], ref groupSet.Group, ref bounds, currentDistance + groupSet.Distances[k]));
				}
			}
		}

		private CGSpot getSpot(int itemID, ref CGBoundsGroup group, ref CGBounds bounds, float startDist)
		{
			CGSpot result = new CGSpot(itemID);
			float f = Path.DistanceToF(startDist + bounds.Depth / 2f);
			Vector3 pos = Vector3.zero;
			Vector3 dir = Vector3.forward;
			Vector3 up = Vector3.up;
			float crossValue = getCrossValue((startDist - StartDistance) / Length, group);
			if (group.RotationMode != CGBoundsGroup.RotationModeEnum.Independent)
			{
				if (UseVolume)
				{
					Volume.InterpolateVolume(f, crossValue, out pos, out dir, out up);
				}
				else
				{
					Path.Interpolate(f, crossValue, out pos, out dir, out up);
				}
				switch (group.RotationMode)
				{
				case CGBoundsGroup.RotationModeEnum.Direction:
					up = Vector3.up;
					break;
				case CGBoundsGroup.RotationModeEnum.Horizontal:
					up = Vector3.up;
					dir.y = 0f;
					break;
				}
			}
			else
			{
				pos = (UseVolume ? Volume.InterpolateVolumePosition(f, crossValue) : Path.InterpolatePosition(f));
			}
			if (Path.SourceIsManaged)
			{
				result.Rotation = Quaternion.LookRotation(dir, up) * Quaternion.Euler(group.RotationOffset.x + group.RotationScatter.x * (float)UnityEngine.Random.Range(-1, 1), group.RotationOffset.y + group.RotationScatter.y * (float)UnityEngine.Random.Range(-1, 1), group.RotationOffset.z + group.RotationScatter.z * (float)UnityEngine.Random.Range(-1, 1));
				result.Position = pos + result.Rotation * new Vector3(0f, group.Height.Next, 0f);
			}
			else
			{
				result.Rotation = Quaternion.LookRotation(dir, up) * Quaternion.Euler(group.RotationOffset.x + group.RotationScatter.x * (float)UnityEngine.Random.Range(-1, 1), group.RotationOffset.y + group.RotationScatter.y * (float)UnityEngine.Random.Range(-1, 1), group.RotationOffset.z + group.RotationScatter.z * (float)UnityEngine.Random.Range(-1, 1));
				result.Position = pos + result.Rotation * new Vector3(0f, group.Height.Next, 0f);
			}
			return result;
		}

		private void prepare()
		{
			m_RepeatingGroups.MakePositive();
			m_RepeatingGroups.Clamp(0, GroupCount - 1);
			if (mGroupBag == null)
			{
				mGroupBag = new WeightedRandom<int>();
			}
			else
			{
				mGroupBag.Clear();
			}
			if (RepeatingOrder == CurvyRepeatingOrderEnum.Random)
			{
				List<CGWeightedItem> itemsWeights = Groups.Cast<CGWeightedItem>().ToList();
				CGBoundsGroup.FillItemBag(mGroupBag, itemsWeights, FirstRepeating, LastRepeating);
			}
			for (int i = 0; i < Groups.Count; i++)
			{
				Groups[i].PrepareINTERNAL();
			}
		}

		private CGBounds getItemBounds(int itemIndex)
		{
			if (itemIndex < 0 || itemIndex >= mBounds.Count)
			{
				return null;
			}
			return mBounds[itemIndex];
		}

		private float getCrossValue(float globalF, CGBoundsGroup group)
		{
			return group.DistributionMode switch
			{
				CGBoundsGroup.DistributionModeEnum.Parent => DTMath.MapValue(-0.5f, 0.5f, CrossBase + m_CrossCurve.Evaluate(globalF) + group.PositionOffset.Next), 
				CGBoundsGroup.DistributionModeEnum.Self => DTMath.MapValue(-0.5f, 0.5f, group.PositionOffset.Next), 
				_ => 0f, 
			};
		}
	}
}
