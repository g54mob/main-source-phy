using System;
using FluffyUnderware.Curvy.Utils;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator.Modules
{
	[ModuleInfo("Create/Path Line Renderer", ModuleName = "Create Path Line Renderer", Description = "Feeds a Line Renderer with a Path")]
	public class CreatePathLineRenderer : CGModule
	{
		[HideInInspector]
		[InputSlotInfo(new Type[] { typeof(CGPath) }, DisplayName = "Rasterized Path")]
		public CGModuleInputSlot InPath = new CGModuleInputSlot();

		private LineRenderer mLineRenderer;

		public LineRenderer LineRenderer
		{
			get
			{
				if (mLineRenderer == null)
				{
					mLineRenderer = GetComponent<LineRenderer>();
				}
				return mLineRenderer;
			}
		}

		protected override void Awake()
		{
			base.Awake();
			createLR();
		}

		public override void Refresh()
		{
			base.Refresh();
			CGPath data = InPath.GetData<CGPath>(Array.Empty<CGDataRequestParameter>());
			if (data != null)
			{
				LineRenderer.positionCount = data.Position.Length;
				LineRenderer.SetPositions(data.Position);
			}
			else
			{
				LineRenderer.positionCount = 0;
			}
		}

		private void createLR()
		{
			if (LineRenderer == null)
			{
				mLineRenderer = base.gameObject.AddComponent<LineRenderer>();
				mLineRenderer.useWorldSpace = false;
				mLineRenderer.textureMode = LineTextureMode.Tile;
				mLineRenderer.sharedMaterial = CurvyUtility.GetDefaultMaterial();
			}
		}
	}
}
