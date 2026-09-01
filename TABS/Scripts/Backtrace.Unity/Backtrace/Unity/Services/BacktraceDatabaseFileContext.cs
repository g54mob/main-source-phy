using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Backtrace.Unity.Interfaces;
using Backtrace.Unity.Model.Database;
using UnityEngine;

namespace Backtrace.Unity.Services
{
	internal class BacktraceDatabaseFileContext : IBacktraceDatabaseFileContext
	{
		private string[] _possibleDatabaseExtension = new string[4] { ".dmp", ".json", ".jpg", ".log" };

		private readonly long _maxDatabaseSize;

		private readonly uint _maxRecordNumber;

		private readonly DirectoryInfo _databaseDirectoryInfo;

		private const string RecordFilterRegex = "*-record.json";

		public BacktraceDatabaseFileContext(string databasePath, long maxDatabaseSize, uint maxRecordNumber)
		{
			_maxDatabaseSize = maxDatabaseSize;
			_maxRecordNumber = maxRecordNumber;
			_databaseDirectoryInfo = new DirectoryInfo(databasePath);
		}

		public IEnumerable<FileInfo> GetAll()
		{
			return _databaseDirectoryInfo.GetFiles();
		}

		public IEnumerable<FileInfo> GetRecords()
		{
			return from n in _databaseDirectoryInfo.GetFiles("*-record.json", SearchOption.TopDirectoryOnly)
				orderby n.CreationTime
				select n;
		}

		public void RemoveOrphaned(IEnumerable<BacktraceDatabaseRecord> existingRecords)
		{
			IEnumerable<string> source = existingRecords.Select((BacktraceDatabaseRecord n) => n.Id.ToString());
			IEnumerable<FileInfo> all = GetAll();
			for (int num = 0; num < all.Count(); num++)
			{
				FileInfo file = all.ElementAt(num);
				try
				{
					if (!_possibleDatabaseExtension.Any((string n) => n == file.Extension))
					{
						file.Delete();
						continue;
					}
					int num2 = file.Name.LastIndexOf('-');
					if (num2 == -1)
					{
						file.Delete();
						continue;
					}
					string value = file.Name.Substring(0, num2);
					if (!source.Contains(value))
					{
						file.Delete();
					}
				}
				catch (Exception)
				{
					Debug.LogWarning($"Cannot remove file in path: {file.FullName}");
				}
			}
		}

		public bool ValidFileConsistency()
		{
			FileInfo[] files = _databaseDirectoryInfo.GetFiles();
			long num = 0L;
			long num2 = 0L;
			FileInfo[] array = files;
			foreach (FileInfo fileInfo in array)
			{
				if (Regex.Match(fileInfo.FullName, "*-record.json").Success)
				{
					num2++;
					if (_maxRecordNumber > num2)
					{
						return false;
					}
				}
				num += fileInfo.Length;
				if (num > _maxDatabaseSize)
				{
					return false;
				}
			}
			return true;
		}

		public void Clear()
		{
			FileInfo[] files = _databaseDirectoryInfo.GetFiles();
			for (int i = 0; i < files.Length; i++)
			{
				files[i].Delete();
			}
		}
	}
}
