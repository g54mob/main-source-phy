using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Modding;
using Modding.Blocks;
using UnityEngine;

namespace AutoSave
{
	public class MachineAutosaveController : SingleInstanceFindOnly<MachineAutosaveController>
	{
		private class FileMaker : LocalMachineCollection
		{
			public override CreateFileResult CreateFile(string fileName, out VirtualFile virtualObject)
			{
				return base.CreateFile(fileName, out virtualObject);
			}

			public VirtualFolder ChangeFolder(string name)
			{
				if (name == "Thumbnails")
				{
					throw new ArgumentException("Thumbnail folder is not accessible.");
				}
				VirtualFolder virtualFolder = base.CurrentFolder.GetObjects().FirstOrDefault((IVirtualObject x) => x.Name == name && x is VirtualFolder) as VirtualFolder;
				if (virtualFolder == null)
				{
					CreateFolder(name);
					virtualFolder = base.CurrentFolder.GetObjects().First((IVirtualObject x) => x.Name == name && x is VirtualFolder) as VirtualFolder;
				}
				ChangeFolder(virtualFolder);
				return virtualFolder;
			}
		}

		public const string DATA_KEY = "AutoSave";

		public const string INDICATOR_AUTOSAVE = "aut ";

		public const string INDICATOR_VERSION = "ver ";

		private const string THUMBNAIL_FOLDER = "Thumbnails";

		private const string NAME_FORMAT = "yy.MM.dd HH-mm-ss";

		public static int AutosaveIntervalSeconds = 60;

		private static readonly DateTime REF_TIME = new DateTime(2014, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		public ThumbnailCreator thumbnailCreator;

		internal DynamicText MainText;

		internal DynamicText SubtitleText;

		private Thread currentThread;

		private string filename;

		private Exception threadException;

		private bool machineJustDestroyed = true;

		public override string Name
		{
			get
			{
				return "MachineAutosaveController";
			}
		}

		internal bool MachineUpdatedSinceLastSave { get; private set; }

		internal DateTime NextRun { get; private set; }

		private void Start()
		{
			if (OptionsMaster.BesiegeConfig.AutosaveEnabled)
			{
				int autosaveDeleteAfterDays = OptionsMaster.BesiegeConfig.AutosaveDeleteAfterDays;
				int num = PruneOldFiles(autosaveDeleteAfterDays, "aut ");
				if (num > 0)
				{
					Debug.Log("Found " + num + " autosave objects to delete older than " + autosaveDeleteAfterDays + " days", this);
				}
			}
			if (OptionsMaster.BesiegeConfig.SavePreviousVersionsEnabled)
			{
				int versionDeleteAfterDays = OptionsMaster.BesiegeConfig.VersionDeleteAfterDays;
				int num2 = PruneOldFiles(versionDeleteAfterDays, "ver ");
				if (num2 > 0)
				{
					Debug.Log("Found " + num2 + " version objects to delete older than " + versionDeleteAfterDays + " days", this);
				}
			}
			NextRun = DateTime.Now.AddSeconds(AutosaveIntervalSeconds);
			ReferenceMaster.onMachineChanged = (Action<Machine>)Delegate.Combine(ReferenceMaster.onMachineChanged, new Action<Machine>(OnMachineChanged));
			Events.OnMachineLoaded += OnMachineLoaded;
			Events.OnMachineDestroyed += OnMachineDestroyed;
			ReferenceMaster.onMachineModified = (Action<Machine>)Delegate.Combine(ReferenceMaster.onMachineModified, new Action<Machine>(OnMachineModified));
		}

		private void OnDestroy()
		{
			ReferenceMaster.onMachineChanged = (Action<Machine>)Delegate.Remove(ReferenceMaster.onMachineChanged, new Action<Machine>(OnMachineChanged));
			Events.OnMachineLoaded -= OnMachineLoaded;
			Events.OnMachineDestroyed -= OnMachineDestroyed;
			ReferenceMaster.onMachineModified = (Action<Machine>)Delegate.Remove(ReferenceMaster.onMachineModified, new Action<Machine>(OnMachineModified));
		}

		private void OnMachineChanged(Machine m)
		{
			MachineUpdatedSinceLastSave = false;
		}

		private void OnMachineLoaded(PlayerMachineInfo info)
		{
			machineJustDestroyed = true;
			MachineUpdatedSinceLastSave = false;
			if (info.InternalObject.MachineData.HasKey("AutoSave"))
			{
				string value = info.InternalObject.MachineData.ReadString("AutoSave");
				if (!string.IsNullOrEmpty(value))
				{
					info.InternalObject.Name = value;
					info.InternalObject.MachineData.Remove("AutoSave");
				}
			}
		}

		private void OnMachineModified(Machine m)
		{
			if (machineJustDestroyed)
			{
				machineJustDestroyed = false;
			}
			else
			{
				MachineUpdatedSinceLastSave = true;
			}
		}

		private void OnMachineDestroyed()
		{
			machineJustDestroyed = true;
			MachineUpdatedSinceLastSave = false;
		}

		private void Update()
		{
			if (!ReferenceMaster.activeMachineSimulating && NextRun < DateTime.Now)
			{
				NextRun = DateTime.Now.AddSeconds(AutosaveIntervalSeconds);
				if (MachineUpdatedSinceLastSave && OptionsMaster.BesiegeConfig.AutosaveEnabled)
				{
					MachineUpdatedSinceLastSave = false;
					AutoSave();
				}
			}
			if (currentThread != null && !currentThread.IsAlive)
			{
				currentThread = null;
				if (threadException != null)
				{
					Debug.LogException(threadException);
				}
			}
		}

		internal void VersionMachine(string dir, string machineName)
		{
			try
			{
				if (!OptionsMaster.BesiegeConfig.SavePreviousVersionsEnabled)
				{
					return;
				}
				if (dir.Contains("AutoSave"))
				{
					Debug.Log("Overwriting machine in autosave folder, not writing backup", this);
					return;
				}
				FileInfo fileInfo = new FileInfo(Path.Combine(dir, machineName + ".bsg"));
				if (fileInfo.Length != 0L)
				{
					string text = Path.Combine(StaticSettings.MachineAutosavePath, Path.Combine(machineName, "Thumbnails"));
					if (!Directory.Exists(text))
					{
						Directory.CreateDirectory(text);
					}
					using (FileMaker fileMaker = new FileMaker())
					{
						fileMaker.ChangeFolder(fileMaker.GetRoot());
						fileMaker.ChangeFolder("AutoSave");
						VirtualFolder folder = fileMaker.ChangeFolder(machineName);
						PruneFileCount(folder, OptionsMaster.BesiegeConfig.VersionMaxFiles, "ver ");
					}
					string text2 = "ver " + DateTime.Now.ToString("yy.MM.dd HH-mm-ss");
					string text3 = Path.Combine(StaticSettings.MachineAutosavePath, machineName);
					if (XmlSaver.IsXmlFormat(Path.Combine(dir, machineName + ".bsg")))
					{
						MachineInfo machineInfo = XmlLoader.LoadFromFullPath(Path.Combine(dir, machineName + ".bsg"), string.Empty);
						machineInfo.MachineData.Write("AutoSave", machineName);
						machineInfo.Name = text2;
						XmlSaver.Save(machineInfo, text3);
					}
					else
					{
						File.Copy(Path.Combine(dir, machineName + ".bsg"), Path.Combine(text3, text2 + ".bsg"));
					}
					string text4 = Path.Combine(dir, Path.Combine("Thumbnails", machineName + ".png"));
					if (File.Exists(text4))
					{
						File.Copy(text4, Path.Combine(text, text2 + ".png"));
					}
				}
			}
			catch (Exception exception)
			{
				Debug.LogException(exception);
			}
		}

		public void AutoSave()
		{
			StartCoroutine(IEAutoSave());
		}

		private IEnumerator IEAutoSave()
		{
			if ((bool)Machine.Active())
			{
				Texture2D thumbnail = thumbnailCreator.CaptureImageTexture(TextureFormat.RGB24, false, false);
				yield return null;
				byte[] thumbnailBytes = thumbnail.EncodeToPNG();
				UnityEngine.Object.Destroy(thumbnail);
				currentThread = AutosaveMachineAsync(machineInfo: Machine.Active().CreateMachineInfo(false), fm: new FileMaker(), machineName: Machine.Active().Name, blocks: Machine.Active().BuildingBlocks, thumbnail: thumbnailBytes);
				currentThread.IsBackground = true;
				currentThread.Start();
			}
		}

		private Thread AutosaveMachineAsync(FileMaker fm, string machineName, MachineInfo machineInfo, List<BlockBehaviour> blocks, byte[] thumbnail)
		{
			return new Thread((ThreadStart)delegate
			{
				try
				{
					filename = "aut " + DateTime.Now.ToString("yy.MM.dd HH-mm-ss");
					string path;
					using (fm)
					{
						fm.ChangeFolder(fm.GetRoot());
						fm.ChangeFolder("AutoSave");
						VirtualFolder virtualFolder = fm.ChangeFolder(machineName);
						fm.CreateFolder("Thumbnails");
						PruneFileCount(virtualFolder, OptionsMaster.BesiegeConfig.AutosaveMaxFiles, "aut ");
						path = virtualFolder.ObjectPath.Path;
					}
					machineInfo.Name = filename;
					machineInfo.MachineData.Write("AutoSave", machineName);
					List<BlockInfo> list = new List<BlockInfo>();
					for (int i = 0; i < blocks.Count; i++)
					{
						BlockInfo blockInfo = BlockInfo.FromBlockBehaviour(blocks[i]);
						switch (blockInfo.ID)
						{
						case BlockType.StartingBlock:
							machineInfo.Blocks.Insert(0, blockInfo);
							break;
						case BlockType.BuildNode:
							machineInfo.Blocks.Insert((machineInfo.Blocks.Count > 0) ? 1 : 0, blockInfo);
							break;
						case BlockType.BuildEdge:
							machineInfo.Blocks.Add(blockInfo);
							break;
						default:
							list.Add(blockInfo);
							break;
						}
					}
					machineInfo.Blocks.AddRange(list);
					XmlSaver.Save(machineInfo, path);
					File.WriteAllBytes(Path.Combine(path, Path.Combine("Thumbnails", filename + ".png")), thumbnail);
				}
				catch (Exception ex)
				{
					threadException = ex;
				}
			});
		}

		private void PruneFileCount(VirtualFolder folder, int maxCount, string indicator)
		{
			foreach (IVirtualObject item in (from x in folder.GetObjects()
				where x is VirtualFile && x.Name.StartsWith(indicator)
				orderby x.Date descending
				select x).Skip(maxCount - 1))
			{
				item.Delete();
			}
		}

		private int PruneOldFiles(int maxAge, string indicator)
		{
			using (FileMaker fileMaker = new FileMaker())
			{
				fileMaker.ChangeFolder(fileMaker.GetRoot());
				VirtualFolder virtualFolder = fileMaker.CurrentFolder.GetObjects().FirstOrDefault((IVirtualObject x) => x.Name == "AutoSave" && x is VirtualFolder) as VirtualFolder;
				if (virtualFolder == null)
				{
					return 0;
				}
				fileMaker.ChangeFolder(virtualFolder);
				List<IVirtualObject> list = new List<IVirtualObject>();
				foreach (VirtualFolder item in from x in fileMaker.CurrentFolder.GetObjects()
					where x is VirtualFolder
					select x)
				{
					fileMaker.ChangeFolder(item);
					(from file in fileMaker.CurrentFolder.GetObjects()
						where file is VirtualFile && file.Name.StartsWith(indicator)
						where REF_TIME.AddSeconds((long)file.Date).AddDays(maxAge) < DateTime.Now
						select file).ToList().ForEach(list.Add);
					fileMaker.ChangeFolder(item);
					if (fileMaker.CurrentFolder.GetObjects().Count() == 0)
					{
						list.Add(item);
					}
				}
				fileMaker.ChangeFolder(virtualFolder);
				list.ForEach(delegate(IVirtualObject x)
				{
					x.Delete();
				});
				return list.Count;
			}
		}
	}
}
