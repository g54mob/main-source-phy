using Cpp2ILInjected;
using UnityEngine;

public class SpawnEffect : MonoBehaviour
{
	public float spawnEffectTime = 2f;

	public float pause = 1f;

	public AnimationCurve fadeIn;

	private ParticleSystem ps;

	private float timer;

	private Renderer _renderer;

	private int shaderProperty;

	private void Start()
	{
		int num = Shader.PropertyToID("_cutoff");
		shaderProperty = num;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Renderer renderer = default(Renderer);
		_renderer = renderer;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696150");
		ParticleSystem particleSystem = default(ParticleSystem);
		ps = particleSystem;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18049AB00");
		ParticleSystem.MainModule mainModule = default(ParticleSystem.MainModule);
		mainModule.duration = spawnEffectTime;
		ps.Play();
	}

	private void Update()
	{
		//IL_008f: Invalid comparison between I4 and F4
		//IL_00a8: Expected F4, but got I4
		//IL_00d2: Invalid comparison between I4 and F4
		//IL_00e1: Expected F4, but got I4
		float num = pause + spawnEffectTime;
		if (!(num > timer))
		{
			ps.Play();
			timer = 0f;
		}
		else
		{
			float deltaTime = Time.deltaTime;
			float num2 = deltaTime + timer;
			timer = num2;
		}
		Material material = _renderer.GetMaterial();
		bool flag = 0f == spawnEffectTime;
		Cpp2ILHelpers.NoteDecompilerIssue("Not implemented instruction: \"jp short 0000000180383FACh\"");
		float time = 0f;
		if (!flag)
		{
			float num3 = timer / spawnEffectTime;
			bool flag2 = 0f > num3;
			time = 0f;
			if (!flag2)
			{
				bool flag3 = num3 > 1f;
				time = 1f;
				if (!flag3)
				{
					time = num3;
				}
			}
		}
		float value = fadeIn.Evaluate(time);
		material.SetFloat(shaderProperty, value);
	}
}
