using UnityEngine;
using UnityEngine.Rendering;

namespace Shapes
{
	internal struct ShapeDrawCall
	{
		public ShapeDrawState drawState;

		public MaterialPropertyBlock mpb;

		private bool usingOverrideMpb;

		public int count;

		public Matrix4x4 matrix;

		public Matrix4x4[] matrices;

		private bool instanced;

		public ShapeDrawCall(ShapeDrawState drawState, Matrix4x4 matrix, MaterialPropertyBlock mpbOverride = null)
		{
			this.drawState = default(ShapeDrawState);
			mpb = null;
			usingOverrideMpb = false;
			count = 0;
			this.matrix = default(Matrix4x4);
			matrices = null;
			instanced = false;
		}

		public ShapeDrawCall(ShapeDrawState drawState, int count, Matrix4x4[] matrices, MaterialPropertyBlock mpbOverride = null)
		{
			this.drawState = default(ShapeDrawState);
			mpb = null;
			usingOverrideMpb = false;
			this.count = 0;
			matrix = default(Matrix4x4);
			this.matrices = null;
			instanced = false;
		}

		public void AddToCommandBuffer(RasterCommandBuffer cmd)
		{
		}

		public void AddToCommandBuffer(CommandBuffer cmd)
		{
		}

		public void Cleanup()
		{
		}
	}
}
