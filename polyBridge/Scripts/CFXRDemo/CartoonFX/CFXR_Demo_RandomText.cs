using UnityEngine;

namespace CartoonFX
{
	public class CFXR_Demo_RandomText : MonoBehaviour
	{
		public ParticleSystem partSystem;

		public CFXR_ParticleText_Runtime runtimeParticleText;

		private void OnEnable()
		{
			InvokeRepeating("SetRandomText", 0f, 1.5f);
		}

		private void OnDisable()
		{
			CancelInvoke("SetRandomText");
			partSystem.Clear(withChildren: true);
		}

		private void SetRandomText()
		{
			int num = Random.Range(10, 1000);
			runtimeParticleText.size = Mathf.Lerp(0.8f, 1.3f, (float)num / 1000f);
			string text = num.ToString();
			runtimeParticleText.GenerateText(text);
			partSystem.Play(withChildren: true);
		}
	}
}
