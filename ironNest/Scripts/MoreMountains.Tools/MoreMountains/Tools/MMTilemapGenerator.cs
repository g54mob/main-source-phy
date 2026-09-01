using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MoreMountains.Tools
{
	[ExecuteAlways]
	public class MMTilemapGenerator : MonoBehaviour
	{
		public enum GenerateMethods
		{
			Full = 0,
			Perlin = 1,
			PerlinGround = 2,
			Random = 3,
			RandomWalk = 4,
			RandomWalkAvoider = 5,
			RandomWalkGround = 6,
			Path = 7,
			Copy = 8
		}

		[StructLayout((LayoutKind)3)]
		[CompilerGenerated]
		private struct _003CDelayedCopy_003Ed__15 : IAsyncStateMachine
		{
			public int _003C_003E1__state;

			public AsyncVoidMethodBuilder _003C_003Et__builder;

			public MMTilemapGeneratorLayer layer;

			private TaskAwaiter _003C_003Eu__1;

			private void MoveNext()
			{
			}

			void IAsyncStateMachine.MoveNext()
			{
				//ILSpy generated this explicit interface implementation from .override directive in MoveNext
				this.MoveNext();
			}

			[DebuggerHidden]
			private void SetStateMachine(IAsyncStateMachine stateMachine)
			{
			}

			void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
			{
				//ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
				this.SetStateMachine(stateMachine);
			}
		}

		[Header("Grid")]
		[Tooltip("The width of the grid, in cells")]
		[MMVector(new string[] { "Min", "Max" })]
		public Vector2Int GridWidth;

		[Tooltip("the height of the grid, in cells")]
		[MMVector(new string[] { "Min", "Max" })]
		public Vector2Int GridHeight;

		[Header("Data")]
		[Tooltip("the list of layers that will be used to generate the tilemap")]
		public MMTilemapGeneratorLayerList Layers;

		[Tooltip("a value between 0 and 1 that will be used by all layers as their random seed. If you generate another map using the same seed, it'll look the same")]
		public int GlobalSeed;

		[Tooltip("whether or not to randomize the global seed every time a new map is generated")]
		public bool RandomizeGlobalSeed;

		[Header("Slow Render")]
		[Tooltip("turning this to true will (at runtime only) draw the map progressively. This is really just for fun.")]
		public bool SlowRender;

		[Tooltip("the duration of the slow render, in seconds")]
		public float SlowRenderDuration;

		[Tooltip("the tween to use for the slow render")]
		public MMTweenType SlowRenderTweenType;

		protected int[,] _grid;

		protected int _width;

		protected int _height;

		public virtual void Generate()
		{
		}

		private void Reset()
		{
		}

		protected virtual void GenerateLayer(MMTilemapGeneratorLayer layer)
		{
		}

		[AsyncStateMachine(typeof(_003CDelayedCopy_003Ed__15))]
		private static void DelayedCopy(MMTilemapGeneratorLayer layer)
		{
		}

		protected virtual void RenderGrid(MMTilemapGeneratorLayer layer)
		{
		}

		protected virtual void OnValidate()
		{
		}
	}
}
