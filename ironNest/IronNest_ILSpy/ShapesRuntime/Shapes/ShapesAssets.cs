using TMPro;
using UnityEngine;

namespace Shapes;

public class ShapesAssets : ScriptableObject
{
	public TMP_FontAsset defaultFont;

	public Mesh[] meshQuad;

	public Mesh[] meshTriangle;

	public Mesh[] meshCube;

	public Mesh[] meshSphere;

	public Mesh[] meshTorus;

	public Mesh[] meshCapsule;

	public Mesh[] meshCylinder;

	public Mesh[] meshCone;

	public Mesh[] meshConeUncapped;

	public TextAsset packageJson;

	private static ShapesAssets inst;

	public static ShapesAssets Instance
	{
		get
		{
			if (inst == null)
			{
				ShapesAssets shapesAssets = Resources.Load<ShapesAssets>("Shapes Assets");
				inst = shapesAssets;
			}
			return inst;
		}
	}

	public ShapesAssets()
	{
		Mesh[] array = new Mesh[5];
		meshQuad = array;
		Mesh[] array2 = new Mesh[5];
		meshTriangle = array2;
		meshCube = new Mesh[5];
		meshSphere = new Mesh[5];
		meshTorus = new Mesh[5];
		meshCapsule = new Mesh[5];
		meshCylinder = new Mesh[5];
		meshCone = new Mesh[5];
		meshConeUncapped = new Mesh[5];
		base._002Ector();
	}
}
