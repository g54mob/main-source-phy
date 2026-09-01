using System.Collections.Generic;
using Poly.Base;
using UnityEngine;
using UnityEngine.Rendering;

namespace Poly.Draw
{
	public class GlDrawerComponent : MonoBehaviour
	{
		public Material material;

		private Material lineMaterial;

		public bool usePostRender = true;

		public bool useGizmos = true;

		public bool _autoClear;

		private void Start()
		{
			lineMaterial = CreateLineMaterial();
			RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
		}

		private void OnDestroy()
		{
			RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
			Object.DestroyImmediate(lineMaterial);
			GlDrawer.Clear();
		}

		private void OnEndCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			if (!(camera.tag != "LastCamera") && usePostRender)
			{
				List<Vector3> lines = Singleton<GlDrawer, int>.instance.lines;
				List<Color> colors = Singleton<GlDrawer, int>.instance.colors;
				lineMaterial.SetPass(0);
				GL.PushMatrix();
				GL.MultMatrix(Matrix4x4.Translate(GlDrawer.offset));
				int num = Mathf.Min(lines.Count, 2 * colors.Count);
				GL.Begin(1);
				for (int i = 0; i < num; i += 2)
				{
					GL.Color(colors[i / 2]);
					GL.Vertex(lines[i]);
					GL.Vertex(lines[i + 1]);
				}
				GL.End();
				GL.PopMatrix();
			}
		}

		private void OnDrawGizmos()
		{
			if (useGizmos)
			{
				List<Vector3> lines = Singleton<GlDrawer, int>.instance.lines;
				List<Color> colors = Singleton<GlDrawer, int>.instance.colors;
				int num = Mathf.Min(lines.Count, 2 * colors.Count);
				for (int i = 0; i < num; i += 2)
				{
					Gizmos.color = colors[i / 2];
					Gizmos.DrawLine(GlDrawer.offset + lines[i], GlDrawer.offset + lines[i + 1]);
				}
				new GUIStyle().alignment = TextAnchor.MiddleCenter;
			}
		}

		private void FixedUpdate()
		{
			if (_autoClear)
			{
				GlDrawer.Clear();
			}
		}

		private Material CreateLineMaterial()
		{
			if ((bool)material)
			{
				material = Object.Instantiate(material);
				material.hideFlags = HideFlags.HideAndDontSave;
			}
			return material;
		}
	}
}
