using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace LevelCreator
{
	internal class VolumeMeshChunkUpdater
	{
		private VertexArrays vertexArrays = new VertexArrays();

		public HashSet<VolumeMeshChunk> volumeMeshChunksScheduledForUpdates = new HashSet<VolumeMeshChunk>();

		public HashSet<VolumeMeshChunk> volumeMeshChunksRescheduledForUpdates = new HashSet<VolumeMeshChunk>();

		public Queue<VolumeMeshChunk> volumeMeshChunksToUpdate = new Queue<VolumeMeshChunk>();

		public Queue<VolumeMeshChunk> volumeMeshChunksUpdated = new Queue<VolumeMeshChunk>();

		public float updateQuotaInSeconds;

		private DateTime lastUpdateChunkTimestamp;

		private bool stop;

		private List<Thread> builderThreads = new List<Thread>();

		public VolumeMeshChunkUpdater()
		{
			for (int i = 0; i < 2; i++)
			{
				builderThreads.Add(new Thread(BuildJob)
				{
					Name = "VolumeBuilderThread_" + (i + 1)
				});
			}
			foreach (Thread builderThread in builderThreads)
			{
				builderThread.Start();
			}
		}

		public void Stop()
		{
			stop = true;
		}

		public void BuildJob()
		{
			Debug.Log("starting thread:" + Thread.CurrentThread.Name);
			uint num = 0u;
			while (!stop)
			{
				VolumeMeshChunk volumeMeshChunk = null;
				lock (volumeMeshChunksToUpdate)
				{
					if (volumeMeshChunksToUpdate.Count > 0)
					{
						volumeMeshChunk = volumeMeshChunksToUpdate.Dequeue();
					}
				}
				if (volumeMeshChunk != null)
				{
					volumeMeshChunk.BuildMeshData();
					lock (volumeMeshChunksUpdated)
					{
						volumeMeshChunksUpdated.Enqueue(volumeMeshChunk);
					}
					num = 0u;
					continue;
				}
				num++;
				switch (num)
				{
				case 0u:
				case 1u:
				case 2u:
				case 3u:
				case 4u:
				case 5u:
				case 6u:
				case 7u:
				case 8u:
				case 9u:
					Thread.Sleep(0);
					break;
				case 10u:
				case 11u:
				case 12u:
				case 13u:
				case 14u:
				case 15u:
				case 16u:
				case 17u:
				case 18u:
				case 19u:
				case 20u:
				case 21u:
				case 22u:
				case 23u:
				case 24u:
				case 25u:
				case 26u:
				case 27u:
				case 28u:
				case 29u:
				case 30u:
				case 31u:
				case 32u:
				case 33u:
				case 34u:
				case 35u:
				case 36u:
				case 37u:
				case 38u:
				case 39u:
				case 40u:
				case 41u:
				case 42u:
				case 43u:
				case 44u:
				case 45u:
				case 46u:
				case 47u:
				case 48u:
				case 49u:
				case 50u:
				case 51u:
				case 52u:
				case 53u:
				case 54u:
				case 55u:
				case 56u:
				case 57u:
				case 58u:
				case 59u:
				case 60u:
				case 61u:
				case 62u:
				case 63u:
				case 64u:
				case 65u:
				case 66u:
				case 67u:
				case 68u:
				case 69u:
				case 70u:
				case 71u:
				case 72u:
				case 73u:
				case 74u:
				case 75u:
				case 76u:
				case 77u:
				case 78u:
				case 79u:
				case 80u:
				case 81u:
				case 82u:
				case 83u:
				case 84u:
				case 85u:
				case 86u:
				case 87u:
				case 88u:
				case 89u:
				case 90u:
				case 91u:
				case 92u:
				case 93u:
				case 94u:
				case 95u:
				case 96u:
				case 97u:
				case 98u:
				case 99u:
					Thread.Sleep(10);
					break;
				default:
					Thread.Sleep(100);
					break;
				}
			}
			Debug.Log("stopping thread:" + Thread.CurrentThread.Name);
		}

		public bool IsScheduledForUpdate(VolumeMeshChunk volumeMeshChunk)
		{
			return volumeMeshChunksScheduledForUpdates.Contains(volumeMeshChunk);
		}

		public DateTime GetLastUpdateChunkTimestamp()
		{
			if (volumeMeshChunksScheduledForUpdates.Count <= 0 && volumeMeshChunksRescheduledForUpdates.Count <= 0)
			{
				return lastUpdateChunkTimestamp;
			}
			return DateTime.Now;
		}

		public void ScheduleUpdate(VolumeMeshChunk volumeMeshChunk)
		{
			if (volumeMeshChunksScheduledForUpdates.Contains(volumeMeshChunk))
			{
				volumeMeshChunksRescheduledForUpdates.Add(volumeMeshChunk);
			}
			else
			{
				volumeMeshChunksScheduledForUpdates.Add(volumeMeshChunk);
				lock (volumeMeshChunksToUpdate)
				{
					volumeMeshChunksToUpdate.Enqueue(volumeMeshChunk);
				}
			}
			lastUpdateChunkTimestamp = DateTime.Now;
		}

		public void UpdateChunk(VolumeMeshChunk volumeMeshChunk, List<FoliageData> foliageItems)
		{
			updateQuotaInSeconds -= volumeMeshChunk.BuildMesh(vertexArrays, foliageItems, volumeMeshChunk.chunkPosition);
			if (volumeMeshChunksRescheduledForUpdates.Contains(volumeMeshChunk))
			{
				volumeMeshChunksRescheduledForUpdates.Remove(volumeMeshChunk);
				lock (volumeMeshChunksToUpdate)
				{
					volumeMeshChunksToUpdate.Enqueue(volumeMeshChunk);
				}
			}
			else
			{
				volumeMeshChunksScheduledForUpdates.Remove(volumeMeshChunk);
			}
			lastUpdateChunkTimestamp = DateTime.Now;
		}

		public void Update(List<FoliageData> foliageItems)
		{
			VolumeMeshChunk volumeMeshChunk = null;
			while (updateQuotaInSeconds > 0f)
			{
				lock (volumeMeshChunksUpdated)
				{
					volumeMeshChunk = ((volumeMeshChunksUpdated.Count > 0) ? volumeMeshChunksUpdated.Dequeue() : null);
				}
				if ((bool)volumeMeshChunk)
				{
					UpdateChunk(volumeMeshChunk, foliageItems);
					continue;
				}
				break;
			}
		}

		public void BuildAllChunks(List<FoliageData> foliageItems)
		{
			VolumeMeshChunk volumeMeshChunk = null;
			while (volumeMeshChunksScheduledForUpdates.Count > 0)
			{
				lock (volumeMeshChunksUpdated)
				{
					volumeMeshChunk = ((volumeMeshChunksUpdated.Count > 0) ? volumeMeshChunksUpdated.Dequeue() : null);
				}
				if ((bool)volumeMeshChunk)
				{
					UpdateChunk(volumeMeshChunk, foliageItems);
				}
			}
		}
	}
}
