using System.Collections;
using InternalModding.Misc;
using UnityEngine;

namespace Modding
{
	public class ModAudioClip : ModResource
	{
		private bool isDone;

		private bool hasError;

		private string error;

		public override bool HasError
		{
			get
			{
				return isDone && hasError;
			}
		}

		public override string Error
		{
			get
			{
				return (!HasError) ? string.Empty : error;
			}
		}

		public override bool Loaded
		{
			get
			{
				return isDone;
			}
		}

		public AudioClip AudioClip { get; private set; }

		internal ModAudioClip()
		{
			hasError = false;
			error = string.Empty;
			isDone = false;
		}

		internal override IEnumerator Load()
		{
			WWW www = new WWW("file:///" + base.Info.Path);
			yield return www;
			if (!HasError)
			{
				AudioClip = www.audioClip;
			}
			error = www.error;
			hasError = !string.IsNullOrEmpty(error);
			isDone = true;
			www.Dispose();
			TriggerOnLoad();
		}

		internal override void ApplyToObject(GameObject go)
		{
			AudioSource component = go.GetComponent<AudioSource>();
			if (component == null)
			{
				MLog.Warn("ModAudioClip.SetOnObject used with an object that has no AudioSource!");
			}
			else
			{
				component.clip = AudioClip;
			}
		}

		public static implicit operator AudioClip(ModAudioClip sound)
		{
			return sound.AudioClip;
		}
	}
}
