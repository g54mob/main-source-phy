using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Synty.Interface.FantasyMenus.Samples
{
	public class SampleOscillateSliders : MonoBehaviour
	{
		[Header("References")]
		public List<Slider> sliders;

		[Header("Parameters")]
		public bool autoGetSliders = true;

		public float speed = 1f;

		public float offset = 0.5f;

		private void GetSliders()
		{
			sliders = Object.FindObjectsByType<Slider>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList();
		}

		private void Reset()
		{
			GetSliders();
		}

		private void Start()
		{
			if (autoGetSliders)
			{
				GetSliders();
			}
		}

		private void Update()
		{
			for (int i = 0; i < sliders.Count; i++)
			{
				sliders[i].value = Mathf.Sin(Time.time * speed + (float)i * offset) * 0.5f + 0.5f;
			}
		}
	}
}
