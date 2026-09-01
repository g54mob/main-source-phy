using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace VLB;

public class SkewingHandleSD : MonoBehaviour
{
	private sealed class _003CCoUpdate_003Ed__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public SkewingHandleSD _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CCoUpdate_003Ed__9(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_005a: Expected I4, but got I8
			//IL_0150: Expected I4, but got O
			SkewingHandleSD skewingHandleSD = _003C_003E4__this;
			if (_003C_003E1__state == 0 || _003C_003E1__state == 1)
			{
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this == null)
				{
					goto IL_0142;
				}
				if (skewingHandleSD.shouldUpdateEachFrame && _003C_003E4__this.CanSetSkewingVector())
				{
					VolumetricLightBeamSD volumetricLightBeam = skewingHandleSD.volumetricLightBeam;
					if ((object)skewingHandleSD.volumetricLightBeam == null)
					{
						goto IL_0142;
					}
					if (volumetricLightBeam._TrackChangesDuringPlaytime)
					{
						_003C_003E4__this.SetSkewingVector();
						_003C_003E2__current = null;
						_003C_003E1__state = 1;
						return true;
					}
				}
			}
			return false;
			IL_0142:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		void IEnumerator.Reset()
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			NotSupportedException ex = new NotSupportedException();
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A73A0");
			throw ex;
		}
	}

	public const string ClassName = "SkewingHandleSD";

	public VolumetricLightBeamSD volumetricLightBeam;

	public bool shouldUpdateEachFrame;

	public bool IsAttachedToSelf()
	{
		//IL_008e: Expected I4, but got O
		bool flag = volumetricLightBeam != null;
		if (!flag)
		{
			return flag;
		}
		if ((object)volumetricLightBeam != null)
		{
			GameObject gameObject = volumetricLightBeam.gameObject;
			GameObject gameObject2 = base.gameObject;
			return gameObject == gameObject2;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public bool CanSetSkewingVector()
	{
		//IL_0086: Expected I4, but got O
		//IL_0064: Expected O, but got I4
		bool flag = volumetricLightBeam != null;
		if (!flag)
		{
			return flag;
		}
		VolumetricLightBeamSD volumetricLightBeamSD = volumetricLightBeam;
		if ((object)volumetricLightBeam != null)
		{
			object obj = volumetricLightBeamSD.geomMeshType - 1;
			return obj == null;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	public bool CanUpdateEachFrame()
	{
		//IL_005e: Expected I4, but got O
		bool flag = CanSetSkewingVector();
		if (!flag)
		{
			return flag;
		}
		VolumetricLightBeamSD volumetricLightBeamSD = volumetricLightBeam;
		if ((object)volumetricLightBeam != null)
		{
			return volumetricLightBeamSD._TrackChangesDuringPlaytime;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private bool ShouldUpdateEachFrame()
	{
		//IL_008c: Expected I4, but got O
		if (!shouldUpdateEachFrame || !CanSetSkewingVector())
		{
			return false;
		}
		VolumetricLightBeamSD volumetricLightBeamSD = volumetricLightBeam;
		if ((object)volumetricLightBeam != null)
		{
			return volumetricLightBeamSD._TrackChangesDuringPlaytime;
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private void OnEnable()
	{
		if (CanSetSkewingVector())
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Invalid instruction: 22 Invalid \"Jump target not found in method: 0x180395D20\"");
		}
	}

	private void Start()
	{
		if (Application.isPlaying && shouldUpdateEachFrame && CanSetSkewingVector())
		{
			VolumetricLightBeamSD volumetricLightBeamSD = volumetricLightBeam;
			if (volumetricLightBeamSD._TrackChangesDuringPlaytime)
			{
				_003CCoUpdate_003Ed__9 obj = new _003CCoUpdate_003Ed__9(0);
				obj._003C_003E1__state = 0;
				obj._003C_003E4__this = this;
				Coroutine coroutine = StartCoroutine(obj);
			}
		}
	}

	private IEnumerator CoUpdate()
	{
		_003CCoUpdate_003Ed__9 obj = new _003CCoUpdate_003Ed__9(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}

	private unsafe void SetSkewingVector()
	{
		//IL_003d: Expected O, but got Ref
		//IL_0062: Expected O, but got F4
		Transform transform = volumetricLightBeam.transform;
		Transform transform2 = base.transform;
		Vector3 position = transform2.position;
		object obj = default(object);
		Vector3 vector = transform.InverseTransformPoint((Vector3)(&obj));
		VolumetricLightBeamSD volumetricLightBeamSD = volumetricLightBeam;
		volumetricLightBeamSD.skewingLocalForwardDirection = (Vector3)vector.x;
		_ = vector.z;
	}
}
