using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Cpp2ILInjected;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public class DistanceEnabler : MonoBehaviour
{
	private sealed class _003CStateCoroutine_003Ed__13 : IEnumerator<object>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private object _003C_003E2__current;

		public DistanceEnabler _003C_003E4__this;

		public bool enable;

		private float _003Cstep_003E5__2;

		private int _003Ci_003E5__3;

		object IEnumerator<object>.Current => _003C_003E2__current;

		object IEnumerator.Current => _003C_003E2__current;

		public _003CStateCoroutine_003Ed__13(int _003C_003E1__state)
		{
			((IDisposable)this).Dispose();
			this._003C_003E1__state = _003C_003E1__state;
		}

		void IDisposable.Dispose()
		{
		}

		private unsafe bool MoveNext()
		{
			//IL_00f3: Expected I4, but got I8
			//IL_0113: Expected O, but got I4
			//IL_0015: Expected O, but got I4
			//IL_00b8: Expected I4, but got I8
			//IL_04cd: Expected I4, but got O
			//IL_006d: Expected I4, but got I8
			//IL_0483: Expected O, but got I4
			DistanceEnabler distanceEnabler = _003C_003E4__this;
			bool flag = _003C_003E1__state == 0;
			int num;
			int num2;
			if (!flag)
			{
				object obj = _003C_003E1__state - 1;
				if (!flag)
				{
					if ((nint)obj != 1)
					{
						return false;
					}
					num = _003Ci_003E5__3 + 1;
					_003C_003E1__state = -1;
					_003Ci_003E5__3 = num;
					if ((object)distanceEnabler != null)
					{
						goto IL_04cd;
					}
				}
				else
				{
					num2 = _003Ci_003E5__3 + 1;
					_003C_003E1__state = -1;
					_003Ci_003E5__3 = num2;
					if ((object)distanceEnabler != null)
					{
						goto IL_04ed;
					}
				}
			}
			else
			{
				_003C_003E1__state = -1;
				object obj2 = default(object);
				bool? flag2 = (byte)(&obj2) != 0;
				distanceEnabler.stateCoroutineTargetState = (bool?)(object)0;
				if (!enable)
				{
					if ((object)distanceEnabler.light != null)
					{
						float intensity = distanceEnabler.light.intensity;
						float num3 = intensity / 20f;
						_003Ci_003E5__3 = 0;
						_003Cstep_003E5__2 = num3;
						num = 0;
						goto IL_04cd;
					}
				}
				else if ((object)distanceEnabler.light != null)
				{
					if (distanceEnabler.light.enabled)
					{
						goto IL_0337;
					}
					if ((object)distanceEnabler.light != null)
					{
						distanceEnabler.light.enabled = true;
						if ((object)distanceEnabler.light != null)
						{
							distanceEnabler.light.intensity = 0f;
							goto IL_0337;
						}
					}
				}
			}
			goto IL_04bf;
			IL_04cd:
			if (num < 20)
			{
				if ((object)distanceEnabler.light != null)
				{
					float intensity2 = distanceEnabler.light.intensity;
					float intensity3 = intensity2 - _003Cstep_003E5__2;
					distanceEnabler.light.intensity = intensity3;
					_003C_003E2__current = distanceEnabler.waitForEndOfFrame;
					_003C_003E1__state = 2;
					goto IL_050d;
				}
			}
			else if ((object)distanceEnabler.light != null)
			{
				distanceEnabler.light.intensity = 0f;
				if ((object)distanceEnabler.light != null)
				{
					distanceEnabler.light.enabled = false;
					goto IL_046b;
				}
			}
			goto IL_04bf;
			IL_046b:
			distanceEnabler.stateCoroutine = null;
			distanceEnabler.stateCoroutineTargetState = (bool?)(object)0;
			return false;
			IL_04bf:
			NullReferenceException ex = new NullReferenceException();
			return (byte)(int)ex != 0;
			IL_050d:
			return true;
			IL_04ed:
			if (num2 < 20)
			{
				if ((object)distanceEnabler.light != null)
				{
					float intensity4 = distanceEnabler.light.intensity;
					float intensity5 = intensity4 + _003Cstep_003E5__2;
					distanceEnabler.light.intensity = intensity5;
					_003C_003E2__current = distanceEnabler.waitForEndOfFrame;
					_003C_003E1__state = 1;
					goto IL_050d;
				}
			}
			else if ((object)distanceEnabler.light != null)
			{
				distanceEnabler.light.intensity = distanceEnabler.defaultIntensity;
				goto IL_046b;
			}
			goto IL_04bf;
			IL_0337:
			if ((object)distanceEnabler.light == null)
			{
				goto IL_04bf;
			}
			float intensity6 = distanceEnabler.light.intensity;
			float num4 = distanceEnabler.defaultIntensity - intensity6;
			_003Ci_003E5__3 = 0;
			float num5 = num4 / 20f;
			_003Cstep_003E5__2 = num5;
			num2 = 0;
			goto IL_04ed;
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

	private bool lowPriority;

	private readonly WaitForEndOfFrame waitForEndOfFrame;

	private Light light;

	private Transform player;

	private Transform lightTransform;

	private Coroutine stateCoroutine;

	private float defaultIntensity;

	private bool initialised;

	private bool? stateCoroutineTargetState;

	private void OnEnable()
	{
		if (player == null)
		{
			Camera main = Camera.main;
			Transform transform = main.transform;
			player = transform;
		}
		if (this.light == null)
		{
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
			Light light = default(Light);
			this.light = light;
		}
		if (lightTransform == null)
		{
			Transform transform2 = base.transform;
			lightTransform = transform2;
		}
		if (!initialised)
		{
			float intensity = this.light.intensity;
			defaultIntensity = intensity;
			initialised = true;
		}
	}

	private void Update()
	{
		//IL_0117: Expected F8, but got I4
		//IL_00fa: Expected F8, but got I4
		//IL_012e: Expected F8, but got I4
		//IL_009e: Expected F8, but got I4
		//IL_00cc: Expected F8, but got I4
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Expected O, but got Unknown
		//IL_03e7: Expected O, but got I4
		//IL_015e: Invalid comparison between F8 and I4
		//IL_0295: Expected O, but got I4
		//IL_02bc: Expected O, but got I4
		//IL_042e: Invalid comparison between F4 and I4
		//IL_0457: Expected O, but got I4
		//IL_0186: Expected F8, but got I4
		//IL_030a: Invalid comparison between F4 and I4
		//IL_0319: Invalid comparison between F4 and I4
		//IL_0342: Expected O, but got I4
		if (!(player != null) || !(light != null))
		{
			return;
		}
		int mainLightShadowResolution = UniversalRenderPipelineUtils.GetMainLightShadowResolution();
		double num;
		if (mainLightShadowResolution > 1024)
		{
			if (mainLightShadowResolution == 2048)
			{
				num = 40.0;
			}
			else
			{
				if (mainLightShadowResolution != 4096)
				{
					goto IL_0125;
				}
				num = 60.0;
			}
		}
		else if (mainLightShadowResolution == 512)
		{
			num = 15.0;
		}
		else
		{
			bool flag = mainLightShadowResolution == 1024;
			num = 25.0;
			if (!flag)
			{
				goto IL_0125;
			}
		}
		goto IL_04be;
		IL_0125:
		num = 10.0;
		goto IL_04be;
		IL_04be:
		if (lowPriority)
		{
			float num2 = (float)num * 0.2f;
			double num3 = Math.Round(num2);
			bool flag2 = !(num3 < 1.0);
			num = num3;
			if (!flag2)
			{
				num = 1.0;
			}
		}
		Vector3 position = player.position;
		Vector3 position2 = lightTransform.position;
		double num4 = num * num;
		float num5 = position.x - position2.x;
		object obj2 = default(object);
		object obj3 = default(object);
		object obj = obj2 - obj3;
		float num6 = position.z - position2.z;
		float num7 = num5 * num5;
		object obj4 = obj * obj;
		float num8 = num6 * num6;
		float num9 = (float)obj4 + num7;
		float num10 = num9 + num8;
		bool? flag3 = default(bool?);
		bool enable;
		if (num4 < (double)num10)
		{
			bool valueOrDefault = flag3 == true;
			nint num11 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj5 = (valueOrDefault ? 1 : 0) ^ 1;
			object obj7 = default(object);
			object obj6 = obj7 & obj5;
			bool flag4 = obj6 == null;
			object obj8 = !flag4;
			if (obj8 != null || !light.enabled)
			{
				return;
			}
			float intensity = light.intensity;
			bool flag5 = intensity < 0f;
			bool flag6 = intensity == 0f;
			bool flag7 = !flag5;
			bool flag8 = !flag6;
			object obj9 = flag8 & flag7;
			if (obj9 == null)
			{
				return;
			}
			if (stateCoroutine != null)
			{
				StopCoroutine(stateCoroutine);
			}
			enable = false;
		}
		else
		{
			bool valueOrDefault2 = flag3 == true;
			nint num12 = 0;
			Cpp2ILHelpers.NoteDecompilerIssue("Method not found @1802A6FD0");
			object obj11 = default(object);
			object obj10 = obj11 & valueOrDefault2;
			bool flag9 = obj10 == null;
			object obj12 = !flag9;
			if (obj12 != null)
			{
				return;
			}
			float intensity2 = light.intensity;
			bool flag10 = defaultIntensity < intensity2;
			float num13 = defaultIntensity - intensity2;
			bool flag11 = num13 == 0f;
			bool flag12 = !flag10;
			bool flag13 = !flag11;
			object obj13 = flag13 & flag12;
			if (obj13 == null)
			{
				return;
			}
			if (stateCoroutine != null)
			{
				StopCoroutine(stateCoroutine);
			}
			enable = true;
		}
		IEnumerator routine = StateCoroutine(enable);
		Coroutine coroutine = StartCoroutine(routine);
		stateCoroutine = coroutine;
	}

	private int CalculateCullDistance()
	{
		//IL_0118: Invalid comparison between F8 and I4
		//IL_0129: Expected I4, but got F8
		int mainLightShadowResolution = UniversalRenderPipelineUtils.GetMainLightShadowResolution();
		int num;
		if (mainLightShadowResolution > 1024)
		{
			if (mainLightShadowResolution == 2048)
			{
				num = 40;
			}
			else
			{
				if (mainLightShadowResolution != 4096)
				{
					goto IL_00df;
				}
				num = 60;
			}
		}
		else if (mainLightShadowResolution == 512)
		{
			num = 15;
		}
		else
		{
			bool flag = mainLightShadowResolution == 1024;
			num = 25;
			if (!flag)
			{
				goto IL_00df;
			}
		}
		goto IL_014a;
		IL_00df:
		num = 10;
		goto IL_014a;
		IL_014a:
		if (lowPriority)
		{
			float num2 = (float)num * 0.2f;
			double num3 = Math.Round(num2);
			bool flag2 = !(num3 < 1.0);
			num = (int)num3;
			if (!flag2)
			{
				num = 1;
			}
		}
		return num;
	}

	private bool ShouldBeOn(int cullDistance)
	{
		//IL_0140: Expected I4, but got O
		//IL_008c: Expected O, but got I4
		//IL_011c: Invalid comparison between O and F4
		if (cullDistance <= 0)
		{
			return false;
		}
		if ((object)player != null)
		{
			Vector3 position = player.position;
			if ((object)lightTransform != null)
			{
				Vector3 position2 = lightTransform.position;
				object obj = cullDistance * cullDistance;
				float num = position.x - position2.x;
				object obj3 = default(object);
				object obj4 = default(object);
				object obj2 = obj3 - obj4;
				float num2 = position.z - position2.z;
				float num3 = num * num;
				object obj5 = obj2 * obj2;
				float num4 = num2 * num2;
				float num5 = (float)obj5 + num3;
				float num6 = num5 + num4;
				bool flag = System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref obj) < System.Runtime.CompilerServices.Unsafe.As<object, UIntPtr>(ref (object)num6);
				return !flag;
			}
		}
		NullReferenceException ex = new NullReferenceException();
		return (byte)(int)ex != 0;
	}

	private IEnumerator StateCoroutine(bool enable)
	{
		_003CStateCoroutine_003Ed__13 obj = new _003CStateCoroutine_003Ed__13(0);
		obj._003C_003E1__state = 0;
		obj._003C_003E4__this = this;
		obj.enable = enable;
		return obj;
	}

	public DistanceEnabler()
	{
		WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
		this.waitForEndOfFrame = waitForEndOfFrame;
		base._002Ector();
	}
}
