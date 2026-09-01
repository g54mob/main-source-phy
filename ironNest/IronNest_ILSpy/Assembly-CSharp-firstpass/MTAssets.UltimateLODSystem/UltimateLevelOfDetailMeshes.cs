using System;
using Cpp2ILInjected;
using UnityEngine;

namespace MTAssets.UltimateLODSystem;

public class UltimateLevelOfDetailMeshes : MonoBehaviour
{
	public UltimateLevelOfDetail responsibleUlod;

	public int idOfOriginalMeshItemOfThisInResponsibleUlod;

	public UltimateLevelOfDetail GetResponsibleUlodComponent()
	{
		return responsibleUlod;
	}

	public int GetQuantityOfLods()
	{
		//IL_0041: Expected I4, but got O
		UltimateLevelOfDetail ultimateLevelOfDetail = responsibleUlod;
		if ((object)responsibleUlod != null)
		{
			return ultimateLevelOfDetail.levelsOfDetailToGenerate;
		}
		NullReferenceException ex = new NullReferenceException();
		return (int)ex;
	}

	public void SetMeshOfThisLodGroup(int level, Mesh newMesh)
	{
		//IL_0033: Expected O, but got I
		if (level <= 8)
		{
			UltimateLevelOfDetail ultimateLevelOfDetail = responsibleUlod;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
			Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v88 @ stack_-18_v2+30]");
			object obj = 0;
			responsibleUlod.ForceThisComponentToUpdateLodsRender();
		}
		else
		{
			Debug.LogError("It was not possible to define a new mesh in this LOD group, the level informed is invalid.");
		}
	}

	public Mesh GetMeshOfThisLodGroup(int level)
	{
		//IL_0087: Expected O, but got I
		//IL_00b9: Expected O, but got I
		if (level <= 8)
		{
			UltimateLevelOfDetail ultimateLevelOfDetail = responsibleUlod;
			if ((object)responsibleUlod != null && ultimateLevelOfDetail.currentScannedMeshesList != null)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				object obj = default(object);
				if (obj != null)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ stack_20_v2+30]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v84 @ stack_20_v2+30]");
					if ((nint)0 != 0)
					{
						Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v94 @ rcx_v7+20+level @ rdx (System.Int32)*8]");
						return (Mesh)0;
					}
				}
			}
			return (Mesh)(object)new NullReferenceException();
		}
		Debug.LogError("It was not possible to get mesh of desired level, the level informed is invalid.");
		return null;
	}

	public bool isMaterialChangesEnabledForThisMesh()
	{
		//IL_005b: Expected I4, but got O
		UltimateLevelOfDetail ultimateLevelOfDetail = responsibleUlod;
		if ((object)responsibleUlod != null)
		{
			bool flag = !ultimateLevelOfDetail.enableMaterialsChanges;
			return !flag;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public void SetMaterialArrayOfThisLodGroup(int level, Material[] newMaterialArray)
	{
		//IL_0063: Expected O, but got I
		//IL_0078: Expected O, but got I
		UltimateLevelOfDetail ultimateLevelOfDetail = responsibleUlod;
		object message;
		if (ultimateLevelOfDetail.enableMaterialsChanges)
		{
			if (level <= 8)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v63 @ stack_8_v3+48]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v74 @ rcx_v11+20+level @ rdx (System.Int32)*8]");
				object obj2 = 0;
				responsibleUlod.ForceThisComponentToUpdateLodsRender();
				return;
			}
			message = "It was not possible to define a new material array in this LOD group, the level informed is invalid.";
		}
		else
		{
			message = "It is not possible to supply or obtain a material array for an LOD of this mesh. Material change is disabled for this mesh and the Ultimate Level Of Detail component that manages it.";
		}
		Debug.LogError(message);
	}

	public Material[] GetMaterialArrayOfThisLodGroup(int level)
	{
		//IL_0063: Expected O, but got I
		//IL_009f: Expected O, but got I
		//IL_00b1: Expected O, but got I
		UltimateLevelOfDetail ultimateLevelOfDetail = responsibleUlod;
		if (ultimateLevelOfDetail.enableMaterialsChanges)
		{
			if (level <= 8)
			{
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1808A6DF0");
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v59 @ stack_8_v3+48]");
				object obj = 0;
				Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v10+18]");
				if ((nint)level < (nint)0)
				{
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v70 @ rcx_v10+20+level @ rdx (System.Int32)*8]");
					object obj2 = 0;
					Cpp2ILHelpers.NoteDecompilerIssue("Unmanaged memory load: [v66 @ rax_v16+10]");
					return (Material[])0;
				}
				return (Material[])(object)new IndexOutOfRangeException();
			}
			Debug.LogError("It was not possible to get mesh of desired level, the level informed is invalid.");
			return null;
		}
		Debug.LogError("It is not possible to supply or obtain a material array for an LOD of this mesh. Material change is disabled for this mesh and the Ultimate Level Of Detail component that manages it.");
		return null;
	}

	public UltimateLevelOfDetailMeshes()
	{
		//IL_000f: Expected I4, but got I8
		idOfOriginalMeshItemOfThisInResponsibleUlod = -1;
		base._002Ector();
	}
}
