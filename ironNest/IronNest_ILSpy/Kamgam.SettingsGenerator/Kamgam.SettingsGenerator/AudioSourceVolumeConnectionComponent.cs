using System;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class AudioSourceVolumeConnectionComponent : MonoBehaviour
{
	public SettingsProvider SettingsProvider;

	public string ID;

	protected Vector2 _inputRange;

	protected AudioSource[] _audioSources;

	public Vector2 InputRange
	{
		get
		{
			AudioSourceVolumeConnection connection = Connection;
			Vector2 result = default(Vector2);
			if (connection != null)
			{
				return result;
			}
			return (Vector2)new NullReferenceException();
		}
		set
		{
			AudioSourceVolumeConnection connection = Connection;
			connection.InputRange = value;
		}
	}

	public List<AudioSource> AudioSources
	{
		get
		{
			AudioSourceVolumeConnection connection = Connection;
			if (connection != null)
			{
				return connection.AudioSources;
			}
			return (List<AudioSource>)(object)new NullReferenceException();
		}
		set
		{
			AudioSourceVolumeConnection connection = Connection;
			List<AudioSource> audioSources = connection.AudioSources;
			int version = audioSources._version + 1;
			audioSources._version = version;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73B0");
			object obj = default(object);
			if (obj == null)
			{
				audioSources._size = 0;
			}
			else
			{
				audioSources._size = 0;
				if (audioSources._size > 0)
				{
					Array.Clear(audioSources._items, 0, audioSources._size);
				}
			}
			AudioSourceVolumeConnection connection2 = Connection;
			if (connection2.AudioSources == null)
			{
				List<AudioSource> audioSources2 = new List<AudioSource>();
				connection2.AudioSources = audioSources2;
			}
			if (value != null)
			{
				CollectionExtensions.AddIfNotContained(connection2.AudioSources, value);
				connection2.DefragAudioSources();
			}
		}
	}

	public AudioSourceVolumeConnection Connection
	{
		get
		{
			//IL_0077: Expected I, but got O
			//IL_0299: Expected O, but got I
			//IL_00b4: Expected O, but got I
			//IL_00b4: Expected O, but got I
			//IL_015e: Expected I, but got O
			//IL_0166: Expected I, but got O
			//IL_0176: Expected O, but got I
			//IL_02cc: Expected O, but got I4
			//IL_01b2: Expected O, but got I
			//IL_01f6: Expected O, but got I
			if ((object)SettingsProvider != null)
			{
				Settings settings = SettingsProvider.Settings;
				if ((object)settings != null)
				{
					IConnection<float> connection = default(IConnection<float>);
					SettingsProvider provider = default(SettingsProvider);
					AudioSourceVolumeConnection orCreateFloat = (AudioSourceVolumeConnection)(object)settings.GetOrCreateFloat(ID, 0f, null, connection, provider);
					if (orCreateFloat != null)
					{
						nint num = (nint)orCreateFloat;
						Cpp2ILHelpers.NoteDecompilerIssue("Indirect call: [v205 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.AudioSourceVolumeConnection>)+588] (should have been resolved before IL gen)");
						object obj = default(object);
						if (obj == null)
						{
							nint num2 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v205 @ rdx_v4 (Il2CppClass<Kamgam.SettingsGenerator.AudioSourceVolumeConnection>)+588]");
							AudioSourceVolumeConnection audioSourceVolumeConnection = new AudioSourceVolumeConnection((Vector2)num2, (IList<AudioSource>)0);
							audioSourceVolumeConnection.InputRange = (Vector2)0;
							_ = 1120403456;
							List<AudioSource> audioSources = new List<AudioSource>();
							audioSourceVolumeConnection.AudioSources = audioSources;
							audioSourceVolumeConnection.InputRange = _inputRange;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [this @ rcx (Kamgam.SettingsGenerator.AudioSourceVolumeConnectionComponent)+34]");
							_ = 0;
							if (audioSourceVolumeConnection.AudioSources == null)
							{
								List<AudioSource> audioSources2 = new List<AudioSource>();
								audioSourceVolumeConnection.AudioSources = audioSources2;
							}
							if (_audioSources != null)
							{
								CollectionExtensions.AddIfNotContained(audioSourceVolumeConnection.AudioSources, _audioSources);
								audioSourceVolumeConnection.DefragAudioSources();
							}
							Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180A3DE00");
						}
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnVal2 @ rax_v5 (Kamgam.SettingsGenerator.AudioSourceVolumeConnection)+80]");
						AudioSourceVolumeConnection audioSourceVolumeConnection2 = (AudioSourceVolumeConnection)0;
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnVal2 @ rax_v5 (Kamgam.SettingsGenerator.AudioSourceVolumeConnection)+80]");
						if ((nint)0 != 0)
						{
							nint num3 = (nint)typeof(AudioSourceVolumeConnection);
							nint num4 = (nint)audioSourceVolumeConnection2;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v148 @ r8_v7 (Il2CppClass<Kamgam.SettingsGenerator.AudioSourceVolumeConnection>)+130]");
							object obj2 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v163 @ r9_v5 (Il2CppClass<Kamgam.SettingsGenerator.AudioSourceVolumeConnection>)+130]");
							nint num5 = 0;
							Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v148 @ r8_v7 (Il2CppClass<Kamgam.SettingsGenerator.AudioSourceVolumeConnection>)+130]");
							if (num5 >= 0)
							{
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v163 @ r9_v5 (Il2CppClass<Kamgam.SettingsGenerator.AudioSourceVolumeConnection>)+C8]");
								object obj3 = 0;
								Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v169 @ rcx_v18+FFFFFFF8+v287 @ rcx_v17*8]");
								if (0 == (nint)typeof(AudioSourceVolumeConnection))
								{
									Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [returnVal2 @ rax_v5 (Kamgam.SettingsGenerator.AudioSourceVolumeConnection)+80]");
									return (AudioSourceVolumeConnection)0;
								}
							}
						}
						throw new NullReferenceException();
					}
					return orCreateFloat;
				}
			}
			return (AudioSourceVolumeConnection)(object)new NullReferenceException();
		}
	}

	public void Start()
	{
		AudioSourceVolumeConnection connection = Connection;
		if (connection.AudioSources == null)
		{
			List<AudioSource> audioSources = new List<AudioSource>();
			connection.AudioSources = audioSources;
		}
		if (_audioSources != null)
		{
			CollectionExtensions.AddIfNotContained(connection.AudioSources, _audioSources);
			connection.DefragAudioSources();
		}
	}

	public void OnDestroy()
	{
		AudioSourceVolumeConnection connection = Connection;
		if (connection.AudioSources == null)
		{
			List<AudioSource> audioSources = new List<AudioSource>();
			connection.AudioSources = audioSources;
		}
		if (_audioSources != null)
		{
			CollectionExtensions.RemoveRange(connection.AudioSources, _audioSources);
			connection.DefragAudioSources();
		}
	}

	public void AddAudioSources(IList<AudioSource> audioSources)
	{
		AudioSourceVolumeConnection connection = Connection;
		if (connection.AudioSources == null)
		{
			List<AudioSource> audioSources2 = new List<AudioSource>();
			connection.AudioSources = audioSources2;
		}
		if (_audioSources != null)
		{
			CollectionExtensions.AddIfNotContained(connection.AudioSources, _audioSources);
			connection.DefragAudioSources();
		}
	}

	public void RemoveAudioSources(IList<AudioSource> audioSources)
	{
		AudioSourceVolumeConnection connection = Connection;
		if (connection.AudioSources == null)
		{
			List<AudioSource> audioSources2 = new List<AudioSource>();
			connection.AudioSources = audioSources2;
		}
		if (_audioSources != null)
		{
			CollectionExtensions.RemoveRange(connection.AudioSources, _audioSources);
			connection.DefragAudioSources();
		}
	}

	public void Reset()
	{
		AudioSource[] components = GetComponents<AudioSource>();
		_audioSources = components;
	}

	public AudioSourceVolumeConnectionComponent()
	{
		//IL_000b: Expected O, but got I4
		_inputRange = (Vector2)0;
		_ = 1120403456;
		base._002Ector();
	}
}
