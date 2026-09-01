using System;
using System.Collections;
using System.Collections.Generic;
using Cpp2ILInjected;
using UnityEngine;

namespace TMPro.Examples;

public class ShaderPropAnimator : MonoBehaviour
{
	private sealed class _003CAnimateProperties_003Ed__6 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public ShaderPropAnimator _003C_003E4__this;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CAnimateProperties_003Ed__6(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			//IL_0014: Expected I4, but got I8
			//IL_0089: Expected I4, but got I8
			//IL_01a5: Expected I4, but got O
			ShaderPropAnimator shaderPropAnimator = _003C_003E4__this;
			if (_003C_003E1__state == 0)
			{
				_003C_003E1__state = -1;
				float frame = UnityEngine.Random.Range(0f, 1f);
				if ((object)_003C_003E4__this != null)
				{
					shaderPropAnimator.m_frame = frame;
					goto IL_00a8;
				}
			}
			else
			{
				if (_003C_003E1__state != 1)
				{
					return false;
				}
				_003C_003E1__state = -1;
				if ((object)_003C_003E4__this != null)
				{
					goto IL_00a8;
				}
			}
			goto IL_0197;
			IL_00a8:
			if (shaderPropAnimator.GlowCurve != null)
			{
				float value = shaderPropAnimator.GlowCurve.Evaluate(shaderPropAnimator.m_frame);
				if ((object)shaderPropAnimator.m_Material != null)
				{
					shaderPropAnimator.m_Material.SetFloat(ShaderUtilities.ID_GlowPower, value);
					float deltaTime = Time.deltaTime;
					float num = UnityEngine.Random.Range(0.2f, 0.3f);
					float num2 = num * deltaTime;
					float frame2 = num2 + shaderPropAnimator.m_frame;
					shaderPropAnimator.m_frame = frame2;
					WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
					_003C_003E2__current = waitForEndOfFrame;
					_003C_003E1__state = 1;
					return true;
				}
			}
			goto IL_0197;
			IL_0197:
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

	private Renderer m_Renderer;

	private Material m_Material;

	public AnimationCurve GlowCurve;

	public float m_frame;

	private void Awake()
	{
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		Renderer renderer = default(Renderer);
		m_Renderer = renderer;
		Material material = m_Renderer.GetMaterial();
		m_Material = material;
	}

	private void Start()
	{
		_003CAnimateProperties_003Ed__6 obj = new _003CAnimateProperties_003Ed__6(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		Coroutine coroutine = StartCoroutine(obj);
	}

	private IEnumerator AnimateProperties()
	{
		_003CAnimateProperties_003Ed__6 obj = new _003CAnimateProperties_003Ed__6(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		return obj;
	}
}
