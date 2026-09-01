using System.Collections.Generic;
using UnityEngine;

namespace FMODUnity
{
	[AddComponentMenu("FMOD Studio/FMOD Studio Bank Loader")]
	public class StudioBankLoader : MonoBehaviour
	{
		public LoaderGameEvent LoadEvent;

		public LoaderGameEvent UnloadEvent;

		[BankRef]
		public List<string> Banks;

		public string CollisionTag;

		public bool PreloadSamples;

		private bool isQuitting;

		private void HandleGameEvent(LoaderGameEvent gameEvent)
		{
		}

		private void Start()
		{
		}

		private void OnApplicationQuit()
		{
		}

		private void OnDestroy()
		{
		}

		private void OnTriggerEnter(Collider other)
		{
		}

		private void OnTriggerExit(Collider other)
		{
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
		}

		private void OnTriggerExit2D(Collider2D other)
		{
		}

		private void OnEnable()
		{
		}

		private void OnDisable()
		{
		}

		public void Load()
		{
		}

		public void Unload()
		{
		}
	}
}
