using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class AudioSourceVolumeConnection : Connection<float>
{
	public Vector2 InputRange;

	public List<AudioSource> AudioSources;

	public AudioSourceVolumeConnection(Vector2 inputRange, IList<AudioSource> audioSources)
	{
		//IL_007f: Expected O, but got I4
		InputRange = (Vector2)0;
		_ = 1120403456;
		List<AudioSource> audioSources2 = new List<AudioSource>();
		AudioSources = audioSources2;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18037D8E0");
		InputRange = inputRange;
		if (AudioSources == null)
		{
			List<AudioSource> audioSources3 = new List<AudioSource>();
			AudioSources = audioSources3;
		}
		if (audioSources != null)
		{
			CollectionExtensions.AddIfNotContained(AudioSources, audioSources);
			DefragAudioSources();
		}
	}

	public void AddAudioSources(IList<AudioSource> audioSources)
	{
		if (AudioSources == null)
		{
			List<AudioSource> audioSources2 = new List<AudioSource>();
			AudioSources = audioSources2;
		}
		if (audioSources != null)
		{
			CollectionExtensions.AddIfNotContained(AudioSources, audioSources);
			DefragAudioSources();
		}
	}

	public void RemoveAudioSources(IList<AudioSource> audioSources)
	{
		if (AudioSources == null)
		{
			List<AudioSource> audioSources2 = new List<AudioSource>();
			AudioSources = audioSources2;
		}
		if (audioSources != null)
		{
			CollectionExtensions.RemoveRange(AudioSources, audioSources);
			DefragAudioSources();
		}
	}

	public override float Get()
	{
		DefragAudioSources();
		float inValue;
		if (AudioSources != null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
			object obj = default(object);
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				UnityEngine.Object obj2 = default(UnityEngine.Object);
				if (obj2 != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
					Component component = default(Component);
					GameObject gameObject = component.gameObject;
					if (gameObject != null)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
						AudioSource audioSource = default(AudioSource);
						inValue = audioSource.volume;
						goto IL_00ef;
					}
				}
			}
		}
		inValue = 0.5f;
		goto IL_00ef;
		IL_00ef:
		float outMin = default(float);
		float outAnchor = default(float);
		float outMax = default(float);
		bool clamp = default(bool);
		return MathUtils.MapWithAnchor(inValue, 0f, 0f, 1f, outMin, outAnchor, outMax, clamp);
	}

	public override void Set(float value)
	{
		//IL_00fe: Expected F4, but got I
		//IL_00fe: Expected F4, but got O
		//IL_00fe: Expected F4, but got O
		if (AudioSources == null)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180002120");
		object obj = default(object);
		if (obj == null)
		{
			return;
		}
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A48E0");
		List<AudioSource>.Enumerator enumerator = default(List<AudioSource>.Enumerator);
		UnityEngine.Object obj2 = default(UnityEngine.Object);
		float outMin = default(float);
		float outAnchor = default(float);
		float outMax = default(float);
		bool clamp = default(bool);
		while (true)
		{
			if (enumerator.MoveNext())
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1803A3630");
				if (obj2 != null)
				{
					if ((object)obj2 == null)
					{
						break;
					}
					GameObject gameObject = ((Component)obj2).gameObject;
					if (!(gameObject == null))
					{
						Vector2 inputRange = InputRange;
						Vector2 inputRange2 = InputRange;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.AudioSourceVolumeConnection)+2C]");
						float volume = MathUtils.MapWithAnchor(value, (float)inputRange, (float)inputRange2, 0f, outMin, outAnchor, outMax, clamp);
						((AudioSource)obj2).volume = volume;
					}
				}
				continue;
			}
			enumerator.Dispose();
			return;
		}
		throw new NullReferenceException();
	}

	public void DefragAudioSources()
	{
		//IL_010d: Expected O, but got I4
		bool flag = (nint)AudioSources < 0;
		if (AudioSources == null)
		{
			return;
		}
		List<AudioSource> audioSources = AudioSources;
		int num = audioSources._size - 1;
		if (flag)
		{
			return;
		}
		UnityEngine.Object obj = default(UnityEngine.Object);
		Component component = default(Component);
		GameObject gameObject2 = default(GameObject);
		object obj2;
		do
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			bool flag3;
			GameObject gameObject3;
			if (obj != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				GameObject gameObject = component.gameObject;
				bool flag2 = gameObject == null;
				flag3 = (flag2 ? 1 : 0) < (false ? 1 : 0);
				bool flag4 = !flag2;
				gameObject2 = gameObject;
				gameObject3 = gameObject;
				if (flag4)
				{
					goto IL_00f4;
				}
			}
			flag3 = (nint)AudioSources < 0;
			AudioSources.RemoveAt(num);
			gameObject3 = gameObject2;
			goto IL_00f4;
			IL_00f4:
			num--;
			obj2 = !flag3;
			gameObject2 = gameObject3;
		}
		while (obj2 != null);
	}
}
