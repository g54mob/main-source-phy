using System;
using System.Diagnostics;
using UnityEngine;

namespace MagicaCloth2
{
	[Serializable]
	public struct ResultCode
	{
		[SerializeField]
		private volatile Define.Result result;

		[SerializeField]
		private volatile Define.Result warning;

		public Define.Result Result => result;

		public static ResultCode None => new ResultCode(Define.Result.None);

		public static ResultCode Empty => new ResultCode(Define.Result.Empty);

		public static ResultCode Success => new ResultCode(Define.Result.Success);

		public static ResultCode Error => new ResultCode(Define.Result.Error);

		public ResultCode(Define.Result initResult)
		{
			result = initResult;
			warning = Define.Result.None;
		}

		public void Clear()
		{
			result = Define.Result.None;
			warning = Define.Result.None;
		}

		public void SetResult(Define.Result code)
		{
			result = code;
		}

		public void SetSuccess()
		{
			SetResult(Define.Result.Success);
		}

		public void SetCancel()
		{
			SetResult(Define.Result.Cancel);
		}

		public void SetError(Define.Result code = Define.Result.Error)
		{
			result = code;
		}

		public void SetWarning(Define.Result code = Define.Result.Warning)
		{
			if (code != Define.Result.None)
			{
				warning = code;
			}
		}

		public void Merge(ResultCode src)
		{
			if (src.IsError())
			{
				result = src.result;
			}
			if (src.IsWarning())
			{
				warning = src.warning;
			}
		}

		public void SetProcess()
		{
			SetResult(Define.Result.Process);
		}

		public bool IsResult(Define.Result code)
		{
			return result == code;
		}

		public bool IsNone()
		{
			return result == Define.Result.None;
		}

		public bool IsSuccess()
		{
			return result == Define.Result.Success;
		}

		public bool IsFaild()
		{
			return !IsSuccess();
		}

		public bool IsCancel()
		{
			return result == Define.Result.Cancel;
		}

		public bool IsNormal()
		{
			return result < Define.Result.Warning;
		}

		public bool IsError()
		{
			return result >= Define.Result.Error;
		}

		public bool IsProcess()
		{
			return result == Define.Result.Process;
		}

		public bool IsWarning()
		{
			return warning != Define.Result.None;
		}

		public string GetResultString()
		{
			if (IsNormal())
			{
				return result.ToString();
			}
			return $"({(int)result}) {result}";
		}

		public string GetWarningString()
		{
			return $"({(int)warning}) {warning}";
		}

		public string GetResultInformation()
		{
			return result switch
			{
				Define.Result.RenderSetup_Unreadable => "It is necessary to turn on [Read/Write] in the model import settings.", 
				Define.Result.RenderSetup_Over65535vertices => "Original mesh must have no more than 65,535 vertices.", 
				Define.Result.SerializeData_Over31Renderers => $"There are {31} renderers that can be set.", 
				Define.Result.Init_ScaleIsZero => "Component scale values is 0.", 
				Define.Result.Init_NegativeScale => "Component has negative scale.", 
				_ => null, 
			};
		}

		public string GetWarningInformation()
		{
			return warning switch
			{
				Define.Result.RenderMesh_VertexWeightIs5BonesOrMore => "The source renderer mesh contains vertex weights that utilize more than 5 bones.\nA weight of 5 or more is invalid.", 
				Define.Result.Init_NonUniformScale => "Component scale values \u200b\u200bshould be uniform.\nIf the scale is not uniform, there is a risk that it will not work properly.", 
				_ => null, 
			};
		}

		[Conditional("MC2_DEBUG")]
		public void DebugLog(bool error = true, bool warning = true, bool normal = true)
		{
			if (!(IsError() && error))
			{
			}
			_ = IsWarning() && warning;
		}
	}
}
