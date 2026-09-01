using TMPro;
using UnityEngine;

namespace Shapes
{
	public class ShapesAssets : ScriptableObject
	{
		[Header("Config")]
		public TMP_FontAsset defaultFont;

		[Header("Meshes")]
		public Mesh[] meshQuad;

		public Mesh[] meshTriangle;

		public Mesh[] meshCube;

		public Mesh[] meshSphere;

		public Mesh[] meshTorus;

		public Mesh[] meshCapsule;

		public Mesh[] meshCylinder;

		public Mesh[] meshCone;

		public Mesh[] meshConeUncapped;

		[Header("Misc")]
		public TextAsset packageJson;

		private static ShapesAssets inst;

		public static ShapesAssets Instance => null;
	}
}
