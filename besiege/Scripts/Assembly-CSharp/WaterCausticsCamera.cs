using UnityEngine;

[AddComponentMenu("Water/Image Effects/Caustics")]
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class WaterCausticsCamera : MonoBehaviour
{
	[Range(0f, 1f)]
	public float _intensity = 0.5f;

	[Range(0f, 10f)]
	public float _causticScale = 1f;

	public Vector4 UV1 = new Vector4(1f, 1f, 0f, 0f);

	public Vector4 UV2 = new Vector4(1f, 1f, 0f, 0f);

	public Vector4 tileSpeed1 = new Vector4(1f, 1f, 0f, 0f);

	public Vector4 tileSpeed2 = new Vector4(1f, 1f, 0f, 0f);

	public float SplitRGBAmount;

	public float HeightOffset;

	public float FadeDepthBegin = -30f;

	public float FadeDepthEnd = -40f;

	private Camera cam;

	[SerializeField]
	private Shader _shader;

	[HideInInspector]
	[SerializeField]
	protected Material _material;

	public Texture tex;

	public Transform waterLight;

	private bool createdMaterial;

	public Material Mat
	{
		get
		{
			if (createdMaterial)
			{
				return _material;
			}
			if (_material == null)
			{
				_material = new Material(_shader);
				_material.hideFlags = HideFlags.DontSave;
				createdMaterial = true;
			}
			return _material;
		}
	}

	private void Awake()
	{
		if (_shader != null && !_shader.isSupported)
		{
			Debug.LogError("Unsupported shader (Caustics).");
			return;
		}
		if (waterLight == null)
		{
			GameObject gameObject = GameObject.Find("Water light");
			if (gameObject != null)
			{
				waterLight = gameObject.transform;
			}
		}
		cam = Camera.main;
	}

	[ImageEffectOpaque]
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (Application.isPlaying && waterLight == null)
		{
			waterLight = GameObject.Find("Water light").transform;
			Graphics.Blit(source, destination);
			return;
		}
		if (!createdMaterial)
		{
			_material = Mat;
		}
		Matrix4x4 inverse = Matrix4x4.TRS(waterLight.position, waterLight.rotation, Vector3.one).inverse;
		_material.SetMatrix("_LightMatrix", inverse);
		inverse = cam.cameraToWorldMatrix;
		_material.SetMatrix("_InverseView", inverse);
		_material.SetFloat("_Intensity", _intensity);
		_material.SetFloat("_CausticScale", _causticScale);
		_material.SetTexture("_CausticTexture", tex);
		Matrix4x4 gPUProjectionMatrix = GL.GetGPUProjectionMatrix(cam.projectionMatrix, false);
		Matrix4x4 matrix = gPUProjectionMatrix * inverse;
		_material.SetMatrix("_ViewProjInv", matrix);
		_material.SetMatrix("_CameraInverseProjection", gPUProjectionMatrix.inverse);
		_material.SetVector("_Caustics1_ST", UV1);
		_material.SetVector("_Caustics2_ST", UV2);
		_material.SetVector("_Caustics1_Speed", tileSpeed1);
		_material.SetVector("_Caustics2_Speed", tileSpeed2);
		_material.SetFloat("_SplitRGB", SplitRGBAmount);
		_material.SetFloat("_HeightOffset", HeightOffset + WaterController.waterTransformHeight);
		_material.SetFloat("_FadeDepthBegin", FadeDepthBegin);
		_material.SetFloat("_FadeDepthEnd", FadeDepthEnd);
		Graphics.Blit(source, destination, _material);
	}
}
