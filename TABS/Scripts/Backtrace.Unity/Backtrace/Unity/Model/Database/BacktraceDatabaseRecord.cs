using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Backtrace.Unity.Model.Database
{
	public class BacktraceDatabaseRecord
	{
		[Serializable]
		private struct BacktraceDatabaseRawRecord
		{
			public string Id;

			public string recordName;

			public string dataPath;

			public long size;

			public string hash;

			public List<string> attachments;
		}

		public Guid Id = Guid.NewGuid();

		internal bool Locked;

		public string Hash = string.Empty;

		private string _path = string.Empty;

		private string _diagnosticDataJson;

		private int _count = 1;

		internal string RecordPath { get; set; }

		internal string DiagnosticDataPath { get; set; }

		internal long Size { get; set; }

		internal BacktraceData Record { get; set; }

		public List<string> Attachments { get; private set; }

		public bool Duplicated => _count != 1;

		public int Count => _count;

		public BacktraceData BacktraceData
		{
			get
			{
				if (Record != null)
				{
					Record.Deduplication = Count;
					return Record;
				}
				return null;
			}
		}

		public string BacktraceDataJson()
		{
			if (!string.IsNullOrEmpty(_diagnosticDataJson))
			{
				return _diagnosticDataJson;
			}
			if (Record != null)
			{
				return Record.ToJson();
			}
			if (string.IsNullOrEmpty(DiagnosticDataPath) || !File.Exists(DiagnosticDataPath))
			{
				return null;
			}
			return File.ReadAllText(DiagnosticDataPath);
		}

		public string ToJson()
		{
			return JsonUtility.ToJson(new BacktraceDatabaseRawRecord
			{
				Id = Id.ToString(),
				recordName = RecordPath,
				dataPath = DiagnosticDataPath,
				size = Size,
				hash = Hash,
				attachments = Attachments
			}, prettyPrint: false);
		}

		public static BacktraceDatabaseRecord Deserialize(string json)
		{
			return new BacktraceDatabaseRecord(JsonUtility.FromJson<BacktraceDatabaseRawRecord>(json));
		}

		private BacktraceDatabaseRecord(BacktraceDatabaseRawRecord rawRecord)
		{
			Id = Guid.Parse(rawRecord.Id);
			RecordPath = rawRecord.recordName;
			DiagnosticDataPath = rawRecord.dataPath;
			Size = rawRecord.size;
			Hash = rawRecord.hash;
			Attachments = rawRecord.attachments;
		}

		public BacktraceDatabaseRecord(BacktraceData data, string path)
		{
			Id = data.Uuid;
			Record = data;
			_path = path;
			Attachments = data.Attachments;
		}

		public bool Save()
		{
			try
			{
				string uuidString = Record.UuidString;
				_diagnosticDataJson = Record.ToJson();
				DiagnosticDataPath = Path.Combine(_path, $"{uuidString}-attachment.json");
				Save(_diagnosticDataJson, DiagnosticDataPath);
				if (Attachments != null && Attachments.Count != 0)
				{
					foreach (string attachment in Attachments)
					{
						if (IsInsideDatabaseDirectory(attachment))
						{
							Size += new FileInfo(attachment).Length;
						}
					}
				}
				RecordPath = Path.Combine(_path, $"{uuidString}-record.json");
				Save(ToJson(), RecordPath);
				return true;
			}
			catch (IOException ex)
			{
				Debug.Log("Received IOException while saving data to database.");
				Debug.Log(ex.Message);
				return false;
			}
			catch (Exception ex2)
			{
				Debug.Log($"Received {ex2.GetType().Name} while saving data to database.");
				Debug.Log($"Message {ex2.Message}");
				return false;
			}
		}

		internal void DatabasePath(string path)
		{
			_path = path;
		}

		private void Save(string json, string destPath)
		{
			if (string.IsNullOrEmpty(json))
			{
				return;
			}
			byte[] bytes = Encoding.UTF8.GetBytes(json);
			Size += bytes.Length;
			using (FileStream fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
			{
				fileStream.Write(bytes, 0, bytes.Length);
			}
		}

		public virtual void Increment()
		{
			_count++;
		}

		internal bool Valid()
		{
			return File.Exists(DiagnosticDataPath);
		}

		internal void Delete()
		{
			Delete(DiagnosticDataPath);
			Delete(RecordPath);
			if (Attachments == null || Attachments.Count == 0)
			{
				return;
			}
			foreach (string attachment in Attachments)
			{
				if (IsInsideDatabaseDirectory(attachment))
				{
					Delete(attachment);
				}
			}
		}

		private void Delete(string path)
		{
			try
			{
				if (File.Exists(path))
				{
					File.Delete(path);
				}
			}
			catch (IOException ex)
			{
				Debug.Log($"File {path} is in use. Message: {ex.Message}");
			}
			catch (Exception ex2)
			{
				Debug.Log($"Cannot delete file: {path}. Message: {ex2.Message}");
			}
		}

		internal static BacktraceDatabaseRecord ReadFromFile(FileInfo file)
		{
			using (StreamReader streamReader = file.OpenText())
			{
				string json = streamReader.ReadToEnd();
				try
				{
					return Deserialize(json);
				}
				catch (Exception)
				{
					return null;
				}
			}
		}

		private bool IsInsideDatabaseDirectory(string path)
		{
			if (string.IsNullOrEmpty(path) || !File.Exists(path))
			{
				return false;
			}
			return Path.GetDirectoryName(path) == _path;
		}

		public virtual void Unlock()
		{
			Locked = false;
			Record = null;
			_diagnosticDataJson = string.Empty;
		}
	}
}
