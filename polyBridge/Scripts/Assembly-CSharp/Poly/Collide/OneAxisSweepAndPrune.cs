using System;
using System.Collections.Generic;
using Poly.Base;
using Poly.Collide.Viewers;
using Poly.Extension;
using Poly.Math;
using Poly.Physics;
using UnityEngine;

namespace Poly.Collide
{
	[Serializable]
	public class OneAxisSweepAndPrune : IBroadphase
	{
		private struct Marker
		{
			public short aabbIndex;
		}

		private class ValueComparer : IComparer<short>
		{
			public int Compare(short a, short b)
			{
				return a - b;
			}
		}

		private static readonly int sizeBlock = 64;

		private AabbInfo[] aabbs = new AabbInfo[sizeBlock];

		private Marker[] markers = new Marker[2 * sizeBlock];

		private short[] markerValues = new short[2 * sizeBlock];

		private FastList<AabbInfo> overlappingAabbsCopy = new FastList<AabbInfo>(16);

		private ValueComparer valueComparer = new ValueComparer();

		public float collisionTolerance { get; set; }

		public void FindPotentialPairs(ShapeHandle[] shapes, int numShapes, CollisionFilter filter, in Bounds2 bpBounds, ref FastList<int> potentialPairIndices, ref FastList<int> potentialPairIndices_WithTriggers, float velocityToDisplacement)
		{
			float num = 32767f / bpBounds.extents.x;
			float x = bpBounds.center.x;
			float num2 = 32767f / bpBounds.extents.y;
			float y = bpBounds.center.y;
			int num3 = 2 * numShapes;
			int num4 = ((numShapes - 1) / sizeBlock + 1) * sizeBlock;
			if (aabbs.Length < num4)
			{
				aabbs = new AabbInfo[num4];
				markers = new Marker[2 * num4];
				markerValues = new short[2 * num4];
			}
			float a = 0.5f * collisionTolerance;
			a = Mathf.Max(a, 0.55f / num);
			int num5 = 0;
			int num6 = 0;
			while (num5 < numShapes)
			{
				Aabb aabb = shapes[num5].shape.GetAabb(ref shapes[num5].t2, a);
				Vec2 vec = shapes[num5].fastLinearVel * velocityToDisplacement;
				if (vec.x < 0f)
				{
					aabb.min.x += vec.x;
				}
				else
				{
					aabb.max.x += vec.x;
				}
				if (vec.y < 0f)
				{
					aabb.min.y += vec.y;
				}
				else
				{
					aabb.max.y += vec.y;
				}
				aabbs[num5].minY = (short)((aabb.min.y - y) * num2);
				aabbs[num5].maxY = (short)((aabb.max.y - y) * num2);
				aabbs[num5].collisionGroup = shapes[num5].collisionGroup;
				aabbs[num5].layer = shapes[num5].layer;
				aabbs[num5].isTrigger = shapes[num5].isTrigger;
				markers[num6].aabbIndex = (short)num5;
				markers[num6 + 1].aabbIndex = (short)(~num5);
				short num7 = (short)((aabb.min.x - x) * num);
				short num8 = (short)((aabb.max.x - x) * num);
				if (num8 <= num7)
				{
					num8 = (short)(num7 + 1);
					if (num7 == short.MaxValue)
					{
						num8 = num7;
						num7--;
					}
				}
				markerValues[num6] = num7;
				markerValues[num6 + 1] = num8;
				num5++;
				num6 += 2;
			}
			AabbViewer.Draw(numShapes, aabbs);
			Array.Sort(markerValues, markers, 0, num3, valueComparer);
			Vec2Short zero = Vec2Short.zero;
			overlappingAabbsCopy.Clear();
			overlappingAabbsCopy._ReserveMore(numShapes, 32);
			for (int i = 0; i < num3; i++)
			{
				short aabbIndex = markers[i].aabbIndex;
				if (aabbIndex >= 0)
				{
					int count = overlappingAabbsCopy.Count;
					potentialPairIndices._ReserveMore(potentialPairIndices.Count + count, potentialPairIndices.Count / 2);
					potentialPairIndices_WithTriggers._ReserveMore(potentialPairIndices_WithTriggers.Count + count, potentialPairIndices_WithTriggers.Count / 2);
					ref AabbInfo reference = ref aabbs[aabbIndex];
					for (int j = 0; j < count; j++)
					{
						ref AabbInfo reference2 = ref overlappingAabbsCopy[j];
						if (reference.minY <= reference2.maxY && reference2.minY <= reference.maxY && reference.collisionGroup != reference2.collisionGroup && filter.isColliding[(uint)reference.layer, (uint)reference2.layer])
						{
							short num9 = aabbIndex;
							AabbInfo aabbInfo = reference2;
							if (num9 < aabbInfo.aabbIdx)
							{
								zero.x = aabbIndex;
								aabbInfo = reference2;
								zero.y = aabbInfo.aabbIdx;
							}
							else
							{
								aabbInfo = reference2;
								zero.x = aabbInfo.aabbIdx;
								zero.y = aabbIndex;
							}
							if (!reference.isTrigger && !reference2.isTrigger)
							{
								potentialPairIndices.Add_Unchecked(in zero.key);
							}
							else
							{
								potentialPairIndices_WithTriggers.Add_Unchecked(in zero.key);
							}
						}
					}
					aabbs[aabbIndex].aabbIdx = aabbIndex;
					overlappingAabbsCopy.Add_Unchecked(in aabbs[aabbIndex]);
					aabbs[aabbIndex].overlapIdx = (short)count;
				}
				else
				{
					aabbIndex = (short)(~aabbIndex);
					short overlapIdx = aabbs[aabbIndex].overlapIdx;
					ref AabbInfo reference3 = ref overlappingAabbsCopy._RemoveAtAndSwap_Faster_Unchecked(overlapIdx);
					aabbs[reference3.aabbIdx].overlapIdx = overlapIdx;
				}
			}
		}

		void IBroadphase.FindPotentialPairs(ShapeHandle[] shapes, int numShapes, CollisionFilter filter, in Bounds2 bounds, ref FastList<int> potentialPairIndices, ref FastList<int> potentialPairIndices_WithTriggers, float velocityToDisplacement)
		{
			FindPotentialPairs(shapes, numShapes, filter, in bounds, ref potentialPairIndices, ref potentialPairIndices_WithTriggers, velocityToDisplacement);
		}
	}
}
