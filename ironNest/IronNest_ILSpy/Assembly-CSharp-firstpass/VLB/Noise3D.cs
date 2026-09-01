using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public static class Noise3D
{
	private static bool ms_IsSupportedChecked;

	private static bool ms_IsSupported;

	private static Texture3D ms_NoiseTexture;

	private const int kMinShaderLevel = 35;

	public static bool isSupported
	{
		get
		{
			//IL_001c: Expected O, but got I4
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Expected I4, but got Unknown
			if (!ms_IsSupportedChecked)
			{
				int graphicsShaderLevel = SystemInfo.graphicsShaderLevel;
				object obj = graphicsShaderLevel - 35;
				int num = graphicsShaderLevel ^ 0x23;
				int num2 = graphicsShaderLevel ^ obj;
				int num3 = num & num2;
				bool flag = num3 < 0;
				bool flag2 = (nint)obj < 0;
				bool flag3 = flag2 == flag;
				ms_IsSupported = flag3;
				if (!ms_IsSupported)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39CE6]");
					if ((nint)0 == 0)
					{
						_ = 1;
					}
					int graphicsShaderLevel2 = SystemInfo.graphicsShaderLevel;
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
					object arg = default(object);
					object arg2 = default(object);
					string message = $"3D Noise requires higher shader capabilities (Shader Model 3.5 / OpenGL ES 3.0), which are not available on the current platform: graphicsShaderLevel (current/required) = {arg} / {arg2}";
					Debug.LogWarning(message);
				}
				ms_IsSupportedChecked = true;
			}
			return ms_IsSupported;
		}
	}

	public static bool isProperlyLoaded => ms_NoiseTexture != null;

	public static string isNotSupportedString
	{
		get
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [182B39CE6]");
			if ((nint)0 == 0)
			{
				_ = 1;
			}
			int graphicsShaderLevel = SystemInfo.graphicsShaderLevel;
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			Cpp2ILHelpers.NoteDecompilerIssue("Unknown call target operand: \"il2cpp_value_box\"");
			object arg = default(object);
			object arg2 = default(object);
			return $"3D Noise requires higher shader capabilities (Shader Model 3.5 / OpenGL ES 3.0), which are not available on the current platform: graphicsShaderLevel (current/required) = {arg} / {arg2}";
		}
	}

	private static void OnStartUp()
	{
		LoadIfNeeded();
	}

	public static void LoadIfNeeded()
	{
		if (isSupported && ms_NoiseTexture == null)
		{
			Config instance = Config.GetInstance(true);
			ms_NoiseTexture = instance.noiseTexture3D;
			Shader.SetGlobalTextureImpl(ShaderProperties.GlobalNoiseTex3D, (Texture)ms_NoiseTexture);
			Shader.SetGlobalFloatImpl(ShaderProperties.GlobalNoiseCustomTime, -1f);
		}
	}
}
