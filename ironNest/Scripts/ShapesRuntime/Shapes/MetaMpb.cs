using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Shapes
{
	internal abstract class MetaMpb : IDisposable
	{
		private bool initialized;

		private int instanceCount;

		private ShapeDrawState drawState;

		public MaterialPropertyBlock mpbOverride;

		private Matrix4x4[] matrices;

		private bool directMaterialApply;

		internal List<Vector4> color;

		private ShapeDrawCall sdc;

		public bool HasContent => false;

		private bool HasMultipleInstances => false;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void ApplyColorOrFill<T>(T fillable, Color baseColor)
		{
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void ApplyDashSettings<T>(T dashable, float thickness)
		{
		}

		internal static List<T> InitList<T>()
		{
			return null;
		}

		protected abstract void TransferShapeProperties();

		protected void Transfer(int propertyID, List<Vector4> listVec)
		{
		}

		protected void Transfer(int propertyID, List<float> listFloat)
		{
		}

		protected void Transfer(int propertyID, ref Texture tex)
		{
		}

		public bool PreAppendCheck(ShapeDrawState additionDrawState, Matrix4x4 mtx)
		{
			return false;
		}

		public ShapeDrawCall ExtractDrawCall()
		{
			return default(ShapeDrawCall);
		}

		public void ApplyDirectlyToMaterial()
		{
		}

		internal void TransferAllProperties()
		{
		}

		public void Dispose()
		{
		}
	}
}
