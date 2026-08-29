using System.Collections.Generic;
using INab.Common;
using UnityEngine;
using UnityEngine.VFX;

namespace INab.Demo
{
	[ExecuteAlways]
	public class ControlVFXGraphs : MonoBehaviour
	{
		public VisualEffectAsset effectToReplace;

		public List<InteractiveEffect> effects = new List<InteractiveEffect>();

		public bool automaticPlayOnStart;

		public bool automaticPlayLoopOnStart;

		private void Start()
		{
			if (automaticPlayOnStart)
			{
				PlayEffects();
			}
			if (automaticPlayLoopOnStart)
			{
				StartCoroutines();
			}
			foreach (InteractiveEffect effect in effects)
			{
				if (effect == null)
				{
					effects.Remove(effect);
				}
			}
		}

		private void OnValidate()
		{
			if (!(effectToReplace != null))
			{
				return;
			}
			foreach (InteractiveEffect effect in effects)
			{
				effect.visualEffect.visualEffectAsset = effectToReplace;
			}
		}

		public void StartCoroutines()
		{
			foreach (InteractiveEffect effect in effects)
			{
				effect.EditorCoroutine = effect.StartCoroutine(effect.AutoEffectCoroutine());
			}
		}

		public void StopCoroutines()
		{
			foreach (InteractiveEffect effect in effects)
			{
				if (effect.EditorCoroutine != null)
				{
					effect.StopCoroutine(effect.EditorCoroutine);
					effect.StopAllCoroutines();
				}
				effect.EditorCoroutine = null;
			}
		}

		public void SendPlayEvents()
		{
			foreach (InteractiveEffect effect in effects)
			{
				effect.visualEffect.Play();
			}
		}

		public void SendStopEvents()
		{
			foreach (InteractiveEffect effect in effects)
			{
				effect.visualEffect.Stop();
			}
		}

		public void PlayEffects()
		{
			foreach (InteractiveEffect effect in effects)
			{
				effect.PlayEffect();
			}
		}

		public void ReverseEffects()
		{
			foreach (InteractiveEffect effect in effects)
			{
				effect.ReverseEffect(1f);
			}
		}

		public void GetRendererMaterials()
		{
			foreach (InteractiveEffect effect in effects)
			{
				effect.GetRendererMaterials();
			}
		}

		public void RefreshEffects()
		{
			foreach (InteractiveEffect effect in effects)
			{
				effect.UpdateAndSetupEffect();
			}
		}
	}
}
