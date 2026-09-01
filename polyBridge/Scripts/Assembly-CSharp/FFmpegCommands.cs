using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FFmpeg;
using UnityEngine;

public static class FFmpegCommands
{
	private static FFmpegWrapper w;

	public const char SEPARATOR = ' ';

	public const char QUOTE = '\'';

	public const char DOUBLE_QUOTE = '"';

	public const string VERSION_INSTRUCTION = "-version";

	public const string REWRITE_INSTRUCTION = "-y";

	public const string INPUT_INSTRUCTION = "-i";

	public const string INDEX_PREFIX_INSTRUCTION = "%";

	public const string INDEX_SUFIX_INSTRUCTION = "d";

	public const string RESIZE_INSTRUCTION = "-r";

	public const string SS_INSTRUCTION = "-ss";

	public const string CODEC_INSTRUCTION = "-codec";

	public const string C_CODEC_INSTRUCTION = "-c";

	public const string COPY_INSTRUCTION = "copy";

	public const string TIME_INSTRUCTION = "-t";

	public const string CODEC_VIDEO_INSTRUCTION = "-c:v";

	public const string CODEC_AUDIO_INSTRUCTION = "-c:a";

	public const string LIB_X264_INSTRUCTION = "libx264";

	public const string CONSTANT_RATE_FACTOR_INSTRUCTION = "-crf";

	public const string FILE_FORMAT_INPUT_INSTRUCTION = "-f";

	public const string CONCAT_INSTRUCTION = "concat";

	public const string SAFE_INSTRUCTION = "-safe";

	public const string ZERO_INSTRUCTION = "0";

	public const string FILTER_COMPLEX_INSTRUCTION = "-filter_complex";

	public const string MAP_INSTRUCTION = "-map";

	public const string PRESET_INSTRUCTION = "-preset";

	public const string VIDEO_INSTRUCTION = "[v]";

	public const string AUDIO_INSTRUCTION = "[a]";

	public const string ULTRASAFE_INSTRUCTION = "ultrafast";

	public const string VIDEO_FORMAT = "[{0}:v:0] ";

	public const string AUDIO_FORMAT = "[{0}:a:0] ";

	public const string CONCAT_FORMAT = "{0}=n={1}:v=1:a=1";

	public const string FIRST_INPUT_VIDEO_CHANNEL = "0:v";

	public const string SECOND_INPUT_AUDIO_CHANNEL = "1:a";

	public const string SHORTEST_INSTRUCTION = "-shortest";

	public const string PIXEL_FORMAT = "-pix_fmt";

	public const string YUV_420P = "yuv420p";

	public static FFmpegWrapper Wrapper
	{
		get
		{
			if (w == null)
			{
				w = UnityEngine.Object.FindObjectOfType<FFmpegWrapper>();
				if (w == null)
				{
					Debug.LogException(new Exception("Place a FFmpeg.prefab in the scene"));
				}
			}
			return w;
		}
	}

	public static void GetVersion()
	{
		Wrapper.Execute(new string[1] { "-version" });
	}

	public static void Convert(BaseData config)
	{
		string[] array = new string[4] { "-y", "-i", config.inputPath, config.outputPath };
		DebugCommand(array);
		Wrapper.Execute(array);
	}

	public static void Trim(TrimData config)
	{
		string[] array = new string[10]
		{
			"-y",
			"-i",
			config.inputPath,
			"-ss",
			config.fromTime,
			"-codec",
			"copy",
			"-t",
			config.durationSec.ToString(),
			config.outputPath
		};
		DebugCommand(array);
		Wrapper.Execute(array);
	}

	public static void Decode(DecodeEncodeData config)
	{
		string[] array = new string[7]
		{
			"-y",
			"-i",
			config.inputPath,
			"-r",
			config.fps.ToString(),
			config.outputPath,
			config.soundPath
		};
		DebugCommand(array);
		Wrapper.Execute(array);
	}

	public static void Encode(DecodeEncodeData config)
	{
		string[] array = new string[10]
		{
			"-y",
			"-i",
			config.inputPath,
			"-r",
			config.fps.ToString(),
			"-i",
			config.soundPath,
			"-pix_fmt",
			"yuv420p",
			config.outputPath
		};
		DebugCommand(array);
		Wrapper.Execute(array);
	}

	public static void Compress(CompressionData config)
	{
		string[] array = new string[8]
		{
			"-i",
			config.inputPath,
			"-c:v",
			"libx264",
			"-crf",
			config.crf.ToString(),
			config.outputPath,
			"-y"
		};
		DebugCommand(array);
		Wrapper.Execute(array);
	}

	public static void AppendFast(AppendData config)
	{
		string[] array = new string[10]
		{
			"-f",
			"concat",
			"-safe",
			"0",
			"-i",
			GetInputsFile(config.inputPaths),
			"-c",
			"copy",
			config.outputPath,
			"-y"
		};
		DebugCommand(array);
		Wrapper.Execute(array);
	}

	private static string GetInputsFile(List<string> inputPaths)
	{
		string directoryName = Path.GetDirectoryName(inputPaths[0].Replace("\"", string.Empty));
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("# File with input videos\n");
		foreach (string inputPath in inputPaths)
		{
			string text = inputPath.Remove(0, directoryName.Length);
			text = inputPath.Replace("\"", string.Empty);
			stringBuilder.Append("file '" + text + "'\n");
		}
		string text2 = Path.Combine(directoryName, "AppendInputFiles.txt");
		using (FileStream fileStream = File.Create(text2))
		{
			byte[] bytes = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true).GetBytes(stringBuilder.ToString());
			fileStream.Write(bytes, 0, bytes.Length);
		}
		text2 = "\"" + text2 + "\"";
		Debug.Log("FilePath: " + text2);
		return text2;
	}

	public static void AppendFull(AppendData config)
	{
		List<string> list = new List<string>();
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append('"');
		for (int i = 0; i < config.inputPaths.Count; i++)
		{
			list.Add("-i");
			list.Add(config.inputPaths[i]);
			stringBuilder.Append($"[{i}:v:0] ").Append($"[{i}:a:0] ");
		}
		stringBuilder.Append(string.Format("{0}=n={1}:v=1:a=1", "concat", config.inputPaths.Count)).Append(' ').Append("[v]")
			.Append(' ')
			.Append("[a]");
		stringBuilder.Append('"');
		list.Add("-filter_complex");
		list.Add(stringBuilder.ToString());
		list.Add("-map");
		list.Add("[v]");
		list.Add("-map");
		list.Add("[a]");
		list.Add("-preset");
		list.Add("ultrafast");
		list.Add(config.outputPath);
		list.Add("-y");
		string[] array = list.ToArray();
		DebugCommand(array);
		Wrapper.Execute(array);
	}

	public static void AddSoundFast(SoundData config)
	{
		string[] array = new string[13]
		{
			"-y", "-i", config.inputPath, "-i", config.soundPath, "-c", "copy", "-map", "0:v", "-map",
			"1:a", "-shortest", config.outputPath
		};
		DebugCommand(array);
		Wrapper.Execute(array);
	}

	public static void AddSoundFull(SoundData config)
	{
		string[] array = new string[6] { "-y", "-i", config.soundPath, "-i", config.inputPath, config.outputPath };
		DebugCommand(array);
		Wrapper.Execute(array);
	}

	public static void Watermark(WatermarkData config)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append('"');
		stringBuilder.Append("[0:v]scale=iw*").Append(config.imageScale).Append(":ih*")
			.Append(config.imageScale)
			.Append(" [ovrl], [1:v][ovrl]overlay=x=(main_w-overlay_w)*")
			.Append(config.xPosNormal)
			.Append(":y=(main_h-overlay_h)*")
			.Append(config.yPosNormal);
		stringBuilder.Append('"');
		string[] array = new string[8]
		{
			"-y",
			"-i",
			config.imagePath,
			"-i",
			config.inputPath,
			"-filter_complex",
			stringBuilder.ToString(),
			config.outputPath
		};
		DebugCommand(array);
		Wrapper.Execute(array);
	}

	public static void DirectInput(string input)
	{
		string[] array = input.Split(' ');
		DebugCommand(array);
		Wrapper.Execute(array);
	}

	public static void Abort()
	{
		Wrapper.Abort();
	}

	private static void DebugCommand(string[] command)
	{
	}
}
