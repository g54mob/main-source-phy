using Cpp2ILInjected;
using UnityEngine;

public class SkyboxAligner : MonoBehaviour
{
	public Transform target;

	public string rotationProperty;

	public float offset;

	public bool invert;

	private Material runtimeSkybox;

	private void Awake()
	{
		Material skybox = RenderSettings.skybox;
		if (skybox != null)
		{
			Material skybox2 = RenderSettings.skybox;
			Material material = new Material(skybox2);
			runtimeSkybox = material;
			RenderSettings.skybox = runtimeSkybox;
			if (runtimeSkybox.HasProperty(rotationProperty))
			{
				return;
			}
			if (!runtimeSkybox.HasProperty("_rotation"))
			{
				if (!runtimeSkybox.HasProperty("_Rotation"))
				{
					string message = "Skybox material does not have a rotation property (checked \"" + rotationProperty + "\", \"_rotation\", \"_Rotation\").";
					Debug.LogWarning(message);
				}
				else
				{
					rotationProperty = "_Rotation";
				}
			}
			else
			{
				rotationProperty = "_rotation";
			}
		}
		else
		{
			Debug.LogWarning("No skybox assigned in RenderSettings.");
		}
	}

	private void Update()
	{
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Expected F4, but got Unknown
		if (target != null && runtimeSkybox != null)
		{
			Vector3 eulerAngles = target.eulerAngles;
			bool flag = !invert;
			float num = eulerAngles.y;
			if (!flag)
			{
				float num2 = num;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182206C80]");
				num = num2 ^ 0;
			}
			float value = num + offset;
			runtimeSkybox.SetFloat(rotationProperty, value);
		}
	}

	private void OnDestroy()
	{
		if (runtimeSkybox != null)
		{
			Object.Destroy(runtimeSkybox);
		}
	}

	public SkyboxAligner()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B3A023]");
		if ((nint)0 == 0)
		{
			_ = 1;
		}
		rotationProperty = "_Rotation";
		base._002Ector();
	}
}
