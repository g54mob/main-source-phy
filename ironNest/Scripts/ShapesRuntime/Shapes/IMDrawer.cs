using System;
using System.Collections.Generic;
using UnityEngine;

namespace Shapes
{
	internal struct IMDrawer : IDisposable
	{
		public enum DrawType
		{
			Shape = 0,
			Custom = 1,
			TextAssetClone = 2,
			TextPooledAuto = 3,
			TextPooledPersistent = 4
		}

		internal static MetaMpb metaMpbPrevious;

		private static Dictionary<Material, string[]> matKeywords;

		private MetaMpb metaMpb;

		private ShapeDrawState drawState;

		private Matrix4x4 mtx;

		private bool allowInstancing;

		private static string[] GetMaterialKeywords(Material m)
		{
			return null;
		}

		public IMDrawer(MetaMpb metaMpb, Material sourceMat, Mesh sourceMesh, int submesh = 0, DrawType drawType = DrawType.Shape, bool allowInstancing = true, int textAutoDisposeId = -1)
		{
			this.metaMpb = null;
			drawState = default(ShapeDrawState);
			mtx = default(Matrix4x4);
			this.allowInstancing = false;
		}

		private static void ApplyGlobalProperties(Material m)
		{
		}

		private static void ApplyGlobalPropertiesTMP(Material m)
		{
		}

		public void Dispose()
		{
		}
	}
}
