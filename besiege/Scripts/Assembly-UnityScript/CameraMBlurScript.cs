using System;
using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

[Serializable]
[AddComponentMenu("Image Effects/CameraMBlur")]
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class CameraMBlurScript : MonoBehaviour
{
	[Serializable]
	internal sealed class _0024renderlate_00241 : GenericGenerator<WaitForEndOfFrame>
	{
		[Serializable]
		internal sealed class _0024 : GenericGeneratorEnumerator<WaitForEndOfFrame>, IEnumerator
		{
			internal Matrix4x4 _0024Iviewprev_00242;

			internal CameraMBlurScript _0024self__00243;

			public _0024(CameraMBlurScript self_)
			{
				_0024self__00243 = self_;
			}

			public override bool MoveNext()
			{
				int result;
				switch (_state)
				{
				default:
					result = (Yield(2, new WaitForEndOfFrame()) ? 1 : 0);
					break;
				case 2:
					_0024Iviewprev_00242 = ((Camera)_0024self__00243.GetComponent(typeof(Camera))).worldToCameraMatrix.inverse * ((Camera)_0024self__00243.GetComponent(typeof(Camera))).projectionMatrix;
					Shader.SetGlobalMatrix("_Myviewprev", _0024Iviewprev_00242);
					YieldDefault(1);
					goto case 1;
				case 1:
					result = 0;
					break;
				}
				return (byte)result != 0;
			}
		}

		internal CameraMBlurScript _0024self__00244;

		public _0024renderlate_00241(CameraMBlurScript self_)
		{
			_0024self__00244 = self_;
		}

		public override IEnumerator<WaitForEndOfFrame> GetEnumerator()
		{
			return new _0024(_0024self__00244);
		}
	}

	public Shader compositeShader;

	public float Strength;

	private Material m_CompositeMaterial;

	public CameraMBlurScript()
	{
		Strength = 13f;
	}

	private Material GetCompositeMaterial()
	{
		if (m_CompositeMaterial == null)
		{
			m_CompositeMaterial = new Material(compositeShader);
			m_CompositeMaterial.hideFlags = HideFlags.HideAndDontSave;
		}
		return m_CompositeMaterial;
	}

	public virtual void OnDisable()
	{
		UnityEngine.Object.DestroyImmediate(m_CompositeMaterial);
	}

	public virtual void OnPreCull()
	{
		Shader.SetGlobalMatrix("_Myview", (((Camera)GetComponent(typeof(Camera))).worldToCameraMatrix.inverse * ((Camera)GetComponent(typeof(Camera))).projectionMatrix).inverse);
	}

	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Material compositeMaterial = GetCompositeMaterial();
		compositeMaterial.SetFloat("_Strength", Strength);
		Graphics.Blit(source, destination, compositeMaterial);
	}

	public virtual void OnPostRender()
	{
		StartCoroutine_Auto(renderlate());
	}

	public virtual IEnumerator renderlate()
	{
		return new _0024renderlate_00241(this).GetEnumerator();
	}

	public virtual void Main()
	{
	}
}
