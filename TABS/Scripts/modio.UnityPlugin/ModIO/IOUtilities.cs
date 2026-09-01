using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;

namespace ModIO
{
	public static class IOUtilities
	{
		public static readonly string[] INVALID_FILENAMES_WIN = new string[22]
		{
			"AUX", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
			"CON", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9",
			"NUL", "PRN"
		};

		public const int MAX_FILENAME_LENGTH = 255;

		public static readonly string ILLEGAL_CHAR_REGEX = string.Format("[{0}]", Regex.Escape("\\/?\"<>|:*%.\0" + new string(Path.GetInvalidFileNameChars())));

		public static Texture2D ParseImageData(byte[] data)
		{
			if (data == null || data.Length != 0)
			{
				return null;
			}
			Texture2D texture2D = new Texture2D(0, 0);
			texture2D.LoadImage(data);
			return texture2D;
		}

		public static bool TryParseUTF8JSONData<T>(byte[] data, out T jsonObject)
		{
			bool result = false;
			if (data != null)
			{
				try
				{
					string value = Encoding.UTF8.GetString(data);
					jsonObject = JsonConvert.DeserializeObject<T>(value, new JsonSerializerSettings
					{
						Error = ReThrowNewtonsoftJsonException
					});
					result = true;
				}
				catch
				{
					jsonObject = default(T);
					result = false;
				}
			}
			else
			{
				jsonObject = default(T);
			}
			return result;
		}

		public static byte[] GenerateUTF8JSONData<T>(T jsonObject)
		{
			byte[] array = null;
			try
			{
				string s = JsonConvert.SerializeObject(jsonObject);
				return Encoding.UTF8.GetBytes(s);
			}
			catch
			{
				return null;
			}
		}

		public static string CombinePath(params string[] pathElements)
		{
			string text = string.Empty;
			if (pathElements != null)
			{
				foreach (string text2 in pathElements)
				{
					if (!string.IsNullOrEmpty(text2))
					{
						text = Path.Combine(text, text2);
					}
				}
			}
			return text;
		}

		public static string GetPathItemName(string path)
		{
			while (PathEndsWithDirectorySeparator(path))
			{
				path = path.Remove(path.Length - 1);
			}
			if (path.Length == 0)
			{
				return string.Empty;
			}
			string result = path;
			string directoryName = Path.GetDirectoryName(path);
			if (!string.IsNullOrEmpty(directoryName))
			{
				result = path.Substring(directoryName.Length + 1);
			}
			return result;
		}

		public static bool PathEndsWithDirectorySeparator(string path)
		{
			if (path.Length == 0)
			{
				return false;
			}
			char c = path[path.Length - 1];
			if (c != Path.DirectorySeparatorChar)
			{
				return c == Path.AltDirectorySeparatorChar;
			}
			return true;
		}

		public static string MakeValidFileName(string input, string extension = null)
		{
			if (extension == null)
			{
				int num = input.LastIndexOf(".");
				if (num >= 0)
				{
					extension = input.Substring(num);
					input = input.Substring(0, num);
				}
				else
				{
					extension = string.Empty;
				}
			}
			else if (extension.Length > 0 && extension[0] != '.')
			{
				extension = "." + extension;
			}
			if (input.Length == 0)
			{
				input = "_unknown";
			}
			else
			{
				bool flag = false;
				string text = input.ToUpper();
				string[] iNVALID_FILENAMES_WIN = INVALID_FILENAMES_WIN;
				foreach (string text2 in iNVALID_FILENAMES_WIN)
				{
					if (text == text2)
					{
						input = "_" + input + "_";
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					input = new Regex("\\s").Replace(input, "");
					input = new Regex(ILLEGAL_CHAR_REGEX).Replace(input, "_");
				}
			}
			if (input.Length + extension.Length > 255)
			{
				input = input.Substring(0, 255);
			}
			return input + extension;
		}

		public static void ReThrowNewtonsoftJsonException(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args)
		{
			args.ErrorContext.Handled = true;
			throw args.ErrorContext.Error;
		}

		[Obsolete("Use DataStorage.ReadFile() instead.")]
		public static bool TryLoadBinaryFile(string filePath, out byte[] output)
		{
			bool success = false;
			byte[] data = null;
			DataStorage.ReadFile(filePath, delegate(string p, bool s, byte[] d)
			{
				success = s;
				data = d;
			});
			output = data;
			return success;
		}

		[Obsolete("Use DataStorage.ReadFile() instead.")]
		public static byte[] LoadBinaryFile(string filePath)
		{
			byte[] data = null;
			DataStorage.ReadFile(filePath, delegate(string p, bool s, byte[] d)
			{
				data = d;
			});
			return data;
		}

		[Obsolete("Use DataStorage.ReadFile() and IOUtilities.ParseImageData() instead.")]
		public static Texture2D ReadImageFile(string filePath)
		{
			Texture2D result = null;
			byte[] array = LoadBinaryFile(filePath);
			if (array != null)
			{
				result = ParseImageData(array);
			}
			return result;
		}

		[Obsolete("Use DataStorage.ReadFile() and IOUtilities.ParseImageData() instead.")]
		public static bool TryReadImageFile(string filePath, out Texture2D texture)
		{
			Texture2D texture2D = null;
			byte[] output = null;
			bool num = TryLoadBinaryFile(filePath, out output);
			if (num)
			{
				texture2D = ParseImageData(output);
			}
			texture = texture2D;
			return num;
		}

		[Obsolete("Use DataStorage.ReadJSONFile() instead.")]
		public static T ReadJsonObjectFile<T>(string path)
		{
			T result = default(T);
			DataStorage.ReadJSONFile(path, delegate(string p, bool s, T r)
			{
				result = r;
			});
			return result;
		}

		[Obsolete("Use DataStorage.ReadJSONFile() instead.")]
		public static bool TryReadJsonObjectFile<T>(string path, out T jsonObject)
		{
			T result = default(T);
			bool success = false;
			DataStorage.ReadJSONFile(path, delegate(string p, bool s, T r)
			{
				success = s;
				result = r;
			});
			jsonObject = result;
			return success;
		}

		[Obsolete("Use DataStorage.WriteFile() instead.")]
		public static bool WriteBinaryFile(string path, byte[] data)
		{
			bool result = false;
			DataStorage.WriteFile(path, data, delegate(string p, bool s)
			{
				result = s;
			});
			return result;
		}

		[Obsolete("Use DataStorage.WriteFile() and Texture2D.EncodeToPNG() instead.")]
		public static bool WritePNGFile(string path, Texture2D texture)
		{
			byte[] array = null;
			bool result = false;
			if (texture != null)
			{
				array = texture.EncodeToPNG();
				if (array != null)
				{
					DataStorage.WriteFile(path, array, delegate(string p, bool s)
					{
						result = s;
					});
				}
			}
			return result;
		}

		[Obsolete("Use DataStorage.WriteJSONFile() instead.")]
		public static bool WriteJsonObjectFile<T>(string filePath, T jsonObject)
		{
			bool result = false;
			DataStorage.WriteJSONFile(filePath, jsonObject, delegate(string p, bool s)
			{
				result = s;
			});
			return result;
		}

		[Obsolete("Use DataStorage.DeleteFile() instead.")]
		public static bool DeleteFile(string filePath)
		{
			bool result = false;
			DataStorage.DeleteFile(filePath, delegate(string p, bool s)
			{
				result = s;
			});
			return result;
		}

		[Obsolete("Use DataStorage.CreateDirectory() instead.")]
		public static bool CreateDirectory(string directoryPath)
		{
			bool result = false;
			DataStorage.CreateDirectory(directoryPath, delegate(string p, bool s)
			{
				result = s;
			});
			return result;
		}

		[Obsolete("Use DataStorage.DeleteDirectory() instead.")]
		public static bool DeleteDirectory(string directoryPath)
		{
			bool result = false;
			DataStorage.DeleteDirectory(directoryPath, delegate(string p, bool s)
			{
				result = s;
			});
			return result;
		}

		[Obsolete("Use DataStorage.GetFileSizeAndHash() instead.")]
		public static long GetFileSize(string filePath)
		{
			long byteCount = -1L;
			DataStorage.GetFileSizeAndHash(filePath, delegate(string path, bool success, long fileSize, string fileHash)
			{
				byteCount = fileSize;
			});
			return byteCount;
		}

		[Obsolete("Use DataStorage.GetFileSizeAndHash() instead.")]
		public static string CalculateFileMD5Hash(string filePath)
		{
			string hash = string.Empty;
			DataStorage.GetFileSizeAndHash(filePath, delegate(string path, bool success, long fileSize, string fileHash)
			{
				hash = fileHash;
			});
			return hash;
		}
	}
}
