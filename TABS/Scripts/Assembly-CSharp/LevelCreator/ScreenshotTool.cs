using System.Collections.Generic;
using Landfall.TABS_Input;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace LevelCreator
{
	public class ScreenshotTool : Tool
	{
		[SerializeField]
		private ParticleSystem m_flash;

		private Camera m_playerCamera;

		[SerializeField]
		[BoxGroup("Sound")]
		private string m_screenshotSound;

		public static List<Texture2D> Screenshots = new List<Texture2D>();

		public static string PreservedLevelName;

		protected override void Start()
		{
			base.Start();
			m_playerCamera = DMEditor.Instance.playerCamera;
			DMEditor.Instance.toolBar.Hide();
		}

		protected override void AssignInput(PlayerActions actions)
		{
			base.AssignInput(actions);
			m_inputState.AddOnKeyDownListener(actions.m_toolPrimary, delegate
			{
				TakeScreenshot();
			}, m_contextIcons.m_primaryIcon);
			m_inputState.AddOnKeyDownListener(actions.m_toolSecondary, delegate
			{
				CleanUp(null);
			}, m_contextIcons.m_secondaryIcon);
			m_inputState.AddOnKeyDownListener(actions.m_enterExitBattle, delegate
			{
				CleanUp(null);
			});
			m_inputState.AddOnKeyDownListener(actions.m_playmode, delegate
			{
				CleanUp(null);
			});
		}

		private void TakeScreenshot()
		{
			RenderTexture temporary = RenderTexture.GetTemporary(854, 480, 24, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
			temporary.enableRandomWrite = true;
			temporary.Create();
			PostProcessLayer component = m_playerCamera.GetComponent<PostProcessLayer>();
			component.fastApproximateAntialiasing.keepAlpha = true;
			m_playerCamera.targetTexture = temporary;
			m_playerCamera.Render();
			component.fastApproximateAntialiasing.keepAlpha = false;
			RenderTexture.active = temporary;
			Texture2D texture2D = new Texture2D(temporary.width, temporary.height);
			texture2D.ReadPixels(new Rect(0f, 0f, temporary.width, temporary.height), 0, 0);
			texture2D.Apply();
			Screenshots.Insert(0, texture2D);
			m_flash.Play();
			Utility.PlaySound(m_screenshotSound, 1f, base.transform);
			CleanUp(temporary);
		}

		private void CleanUp(RenderTexture renderTexture)
		{
			m_playerCamera.targetTexture = null;
			RenderTexture.active = null;
			if (renderTexture != null)
			{
				renderTexture.Release();
			}
			DMEditor.Instance.toolBar.SwitchHotbar(0);
		}

		private void Update()
		{
			SetFocusDistance(GetFocusDistance());
		}

		private float GetFocusDistance()
		{
			return Vector3.Distance(Utility.GetTargetPosition(DMEditor.Instance.playerCamera.transform.position, DMEditor.Instance.playerCamera.transform.forward, DMEditor.Instance.rayDistance), DMEditor.Instance.playerCamera.transform.position);
		}

		public void SetDepthOfField(bool enabled)
		{
			if (!DMEditor.Instance.postProcessVolume.profile.TryGetSettings<DepthOfField>(out var outSetting))
			{
				outSetting = DMEditor.Instance.postProcessVolume.profile.AddSettings<DepthOfField>();
			}
			outSetting.active = enabled;
			outSetting.enabled.overrideState = enabled;
			outSetting.enabled.value = enabled;
			outSetting.aperture.overrideState = enabled;
			outSetting.aperture.value = 5.2f;
			outSetting.focalLength.overrideState = enabled;
			outSetting.focalLength.value = 112f;
		}

		private void SetFocusDistance(float value)
		{
			DepthOfField setting = DMEditor.Instance.postProcessVolume.profile.GetSetting<DepthOfField>();
			setting.focusDistance.value = Mathf.Lerp(setting.focusDistance.value, value, Time.deltaTime * 20f);
		}

		public void SetVignette(float value)
		{
			DMEditor.Instance.postProcessVolume.profile.GetSetting<Vignette>().intensity.value = value;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			SetDepthOfField(enabled: false);
			SetVignette(0.18f);
			SaveMenu.PreservedNameInput = PreservedLevelName;
			DMEditor.Instance.toolBar.Show();
			Utility.DelayAction(DMEditor.Instance, delegate
			{
				DMUIManager.Instance.OpenPanel(DMUIManager.UIPanels.SaveMenu);
			});
		}
	}
}
