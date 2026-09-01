using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace MoreMountains.Tools
{
	public class MMTilemapGridRenderer
	{
		[CompilerGenerated]
		private sealed class _003CSlowRenderGrid_003Ed__1 : IEnumerator<object>, IEnumerator, IDisposable
		{
			private int _003C_003E1__state;

			private object _003C_003E2__current;

			public int[,] grid;

			public int frameRate;

			public float slowRenderDuration;

			public MMTweenType slowRenderTweenType;

			public Tilemap tilemap;

			public TileBase tile;

			private int _003CtotalBlocks_003E5__2;

			private float _003CrefreshFrequency_003E5__3;

			private float _003CstartedAt_003E5__4;

			private float _003ClastWaitAt_003E5__5;

			private int _003CdrawnBlocks_003E5__6;

			private int _003ClastIndex_003E5__7;

			object IEnumerator<object>.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return null;
				}
			}

			[DebuggerHidden]
			public _003CSlowRenderGrid_003Ed__1(int _003C_003E1__state)
			{
			}

			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			private bool MoveNext()
			{
				return false;
			}

			bool IEnumerator.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				return this.MoveNext();
			}

			[DebuggerHidden]
			void IEnumerator.Reset()
			{
			}
		}

		public static void RenderGrid(int[,] grid, MMTilemapGeneratorLayer layer, bool slowRender = false, float slowRenderDuration = 1f, MMTweenType slowRenderTweenType = null, MonoBehaviour slowRenderSupport = null)
		{
		}

		[IteratorStateMachine(typeof(_003CSlowRenderGrid_003Ed__1))]
		public static IEnumerator SlowRenderGrid(int[,] grid, Tilemap tilemap, TileBase tile, float slowRenderDuration, MMTweenType slowRenderTweenType, int frameRate)
		{
			return null;
		}

		public static int TotalFilledBlocks(int[,] grid)
		{
			return 0;
		}

		private static int DrawGrid(int[,] grid, Tilemap tilemap, TileBase tile, int startIndex, int numberOfTilesToDraw)
		{
			return 0;
		}

		public static Vector3Int ComputeOffset(int width, int height)
		{
			return default(Vector3Int);
		}

		public static void ClearTilemap(Tilemap tilemap)
		{
		}
	}
}
