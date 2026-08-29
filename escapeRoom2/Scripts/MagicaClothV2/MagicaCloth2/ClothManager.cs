using System;
using System.Collections.Generic;
using System.Text;
using Unity.Jobs;
using Unity.Profiling;
using UnityEngine;

namespace MagicaCloth2
{
	public class ClothManager : IManager, IDisposable, IValid
	{
		internal HashSet<ClothProcess> clothSet = new HashSet<ClothProcess>(256);

		internal HashSet<ClothProcess> boneClothSet = new HashSet<ClothProcess>();

		internal HashSet<ClothProcess> meshClothSet = new HashSet<ClothProcess>();

		private Dictionary<int, bool> animatorVisibleDict = new Dictionary<int, bool>(30);

		private Dictionary<int, bool> rendererVisibleDict = new Dictionary<int, bool>(100);

		private JobHandle masterJob;

		private bool isValid;

		private static readonly ProfilerMarker startClothUpdateTimeProfiler = new ProfilerMarker("StartClothUpdate.Time");

		private static readonly ProfilerMarker startClothUpdateTeamProfiler = new ProfilerMarker("StartClothUpdate.Team");

		private static readonly ProfilerMarker startClothUpdatePrePareProfiler = new ProfilerMarker("StartClothUpdate.Prepare");

		private static readonly ProfilerMarker startClothUpdateScheduleProfiler = new ProfilerMarker("StartClothUpdate.Schedule");

		public void Dispose()
		{
			isValid = false;
			clothSet.Clear();
			boneClothSet.Clear();
			meshClothSet.Clear();
			animatorVisibleDict.Clear();
			rendererVisibleDict.Clear();
			MagicaManager.afterEarlyUpdateDelegate = (MagicaManager.UpdateMethod)Delegate.Remove(MagicaManager.afterEarlyUpdateDelegate, new MagicaManager.UpdateMethod(OnEarlyClothUpdate));
			MagicaManager.afterLateUpdateDelegate = (MagicaManager.UpdateMethod)Delegate.Remove(MagicaManager.afterLateUpdateDelegate, new MagicaManager.UpdateMethod(OnAfterLateUpdate));
			MagicaManager.beforeLateUpdateDelegate = (MagicaManager.UpdateMethod)Delegate.Remove(MagicaManager.beforeLateUpdateDelegate, new MagicaManager.UpdateMethod(OnBeforeLateUpdate));
		}

		public void EnterdEditMode()
		{
			Dispose();
		}

		public void Initialize()
		{
			clothSet.Clear();
			boneClothSet.Clear();
			meshClothSet.Clear();
			MagicaManager.afterEarlyUpdateDelegate = (MagicaManager.UpdateMethod)Delegate.Combine(MagicaManager.afterEarlyUpdateDelegate, new MagicaManager.UpdateMethod(OnEarlyClothUpdate));
			MagicaManager.afterLateUpdateDelegate = (MagicaManager.UpdateMethod)Delegate.Combine(MagicaManager.afterLateUpdateDelegate, new MagicaManager.UpdateMethod(OnAfterLateUpdate));
			MagicaManager.beforeLateUpdateDelegate = (MagicaManager.UpdateMethod)Delegate.Combine(MagicaManager.beforeLateUpdateDelegate, new MagicaManager.UpdateMethod(OnBeforeLateUpdate));
			isValid = true;
		}

		public bool IsValid()
		{
			return isValid;
		}

		private void ClearMasterJob()
		{
			masterJob = default(JobHandle);
		}

		private void CompleteMasterJob()
		{
			masterJob.Complete();
		}

		internal int AddCloth(ClothProcess cprocess, in ClothParameters clothParams)
		{
			if (!isValid)
			{
				return 0;
			}
			int num = MagicaManager.Team.AddTeam(cprocess, clothParams);
			if (num == 0)
			{
				return 0;
			}
			clothSet.Add(cprocess);
			switch (cprocess.clothType)
			{
			case ClothProcess.ClothType.BoneCloth:
			case ClothProcess.ClothType.BoneSpring:
				boneClothSet.Add(cprocess);
				break;
			case ClothProcess.ClothType.MeshCloth:
				meshClothSet.Add(cprocess);
				break;
			default:
				Develop.LogError((object)$"Invalid cloth type! :{cprocess.clothType}");
				break;
			}
			MagicaManager.Team.comp2TeamIdMap.Add(cprocess.cloth.GetInstanceID(), num);
			return num;
		}

		internal void RemoveCloth(ClothProcess cprocess)
		{
			if (isValid)
			{
				MagicaManager.Team.RemoveTeam(cprocess.TeamId);
				clothSet.Remove(cprocess);
				boneClothSet.Remove(cprocess);
				meshClothSet.Remove(cprocess);
			}
		}

		private void OnEarlyClothUpdate()
		{
			if (MagicaManager.Team.TrueTeamCount > 0)
			{
				if (MagicaManager.Team.ActiveTeamCount > 0)
				{
					MagicaManager.Team.CameraCullingPostProcess();
				}
				ClearMasterJob();
				masterJob = MagicaManager.Bone.RestoreTransform(masterJob);
				CompleteMasterJob();
			}
		}

		private void OnBeforeLateUpdate()
		{
			if (MagicaManager.Time.updateLocation == TimeManager.UpdateLocation.BeforeLateUpdate)
			{
				ClothUpdate();
			}
		}

		private void OnAfterLateUpdate()
		{
			if (MagicaManager.Time.updateLocation == TimeManager.UpdateLocation.AfterLateUpdate)
			{
				ClothUpdate();
			}
		}

		private void ClothUpdate()
		{
			if (MagicaManager.IsPlaying())
			{
				MagicaManager.OnPreSimulation?.Invoke();
				TeamManager team = MagicaManager.Team;
				SimulationManager simulation = MagicaManager.Simulation;
				TransformManager bone = MagicaManager.Bone;
				WindManager wind = MagicaManager.Wind;
				MagicaManager.Time.FrameUpdate();
				team.AlwaysTeamUpdate();
				if (team.ActiveTeamCount != 0)
				{
					wind.AlwaysWindUpdate();
					simulation.WorkBufferUpdate();
					ClearMasterJob();
					masterJob = bone.ReadTransformSchedule(masterJob);
					masterJob = simulation.ClothSimulationSchedule(masterJob);
					team.CameraCullingPreProcess();
					CompleteMasterJob();
					MagicaManager.OnPostSimulation?.Invoke();
				}
			}
		}

		internal void ClearVisibleDict()
		{
			animatorVisibleDict.Clear();
			rendererVisibleDict.Clear();
		}

		internal bool CheckVisible(Animator ani, List<Renderer> renderers)
		{
			if ((bool)ani)
			{
				int instanceID = ani.GetInstanceID();
				if (animatorVisibleDict.ContainsKey(instanceID))
				{
					return animatorVisibleDict[instanceID];
				}
				bool flag = CheckRendererVisible(renderers);
				animatorVisibleDict.Add(instanceID, flag);
				return flag;
			}
			return CheckRendererVisible(renderers);
		}

		private bool CheckRendererVisible(List<Renderer> renderers)
		{
			foreach (Renderer renderer in renderers)
			{
				if ((bool)renderer)
				{
					int instanceID = renderer.GetInstanceID();
					bool flag;
					if (rendererVisibleDict.ContainsKey(instanceID))
					{
						flag = rendererVisibleDict[instanceID];
					}
					else
					{
						flag = renderer.isVisible;
						rendererVisibleDict.Add(instanceID, flag);
					}
					if (flag)
					{
						return true;
					}
				}
			}
			return false;
		}

		public void InformationLog(StringBuilder allsb)
		{
		}
	}
}
