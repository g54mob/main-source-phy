using System;
using Poly.Base;
using Poly.Game;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Poly.UI
{
	public class NoodleNerfUI : SingletonBehaviour<NoodleNerfUI>
	{
		public Dropdown modeDropdown;

		[Space(10f)]
		public RectTransform perNodeLinkingPanel;

		public RectTransform clusteringLogicPanel;

		public RectTransform angleMonitorPanel;

		[Header("Per-Node Logic")]
		public Toggle roadLinkToAnchorBoostsStrength;

		public Slider maxStrengthDeduction;

		public Text maxStrengthDeductionValueLabel;

		public Slider fullStrengthLength;

		public Text fullStrengthLengthValueLabel;

		public Slider labelWidth;

		public Text labelWidthValueLabel;

		public Toggle resetToDefaultSettings;

		[Header("Clustering Logic")]
		public Toggle requireClusterOnBothRoadSegmentEnds;

		public Slider maxStrengthDeduction_Cluster;

		public Text maxStrengthDeductionValueLabel_Cluster;

		public Slider fullStrengthLength_Cluster;

		public Text fullStrengthLengthValueLabel_Cluster;

		public Slider labelWidth_Cluster;

		public Text labelWidthValueLabel_Cluster;

		[Space(10f)]
		public Toggle ropesDontLinkToggle;

		public Toggle allocateByRoadLength;

		public Toggle excludeDanglies;

		[Space(10f)]
		public Text descriptionText;

		[Header("Road Angle Monitoring")]
		public Toggle mustBeNerfedOnBothSides;

		public Toggle combineWithMethodOne;

		public Slider numDirChangesOk_AngleMonitor;

		public Text numDirChangesOkValueLabel_AngleMonitor;

		public Slider deductionPerDirChange_AngleMonitor;

		public Text deductionPerDirChangeValueLabel_AngleMonitor;

		public Slider maxStrengthDeduction_AngleMonitor;

		public Text maxStrengthDeductionValueLabel_AngleMonitor;

		public Slider anglePrecision_AngleMonitor;

		public Text anglePrecisionValueLabel_AngleMonitor;

		public Slider labelWidth_AngleMonitor;

		public Text labelWidthValueLabel_AngleMonitor;

		public UiSliderWrapper distRateOver05Sec;

		public UiSliderWrapper distRateOver_1Sec;

		public UiSliderWrapper distRateOver_2Sec;

		private EdgeLinkScoreViewer edgeLinkScoreViewer;

		private new void Awake()
		{
			base.Awake();
			edgeLinkScoreViewer = Main.m_Instance.GetComponentInChildren<EdgeLinkScoreViewer>(includeInactive: true);
			UiSliderWrapper uiSliderWrapper = distRateOver05Sec;
			uiSliderWrapper.onValueChanged = (UnityAction<float>)Delegate.Combine(uiSliderWrapper.onValueChanged, (UnityAction<float>)delegate(float v)
			{
				edgeLinkScoreViewer.roadLinkAngleMonitor.flipCountCooldownRate = v;
			});
			UiSliderWrapper uiSliderWrapper2 = distRateOver_1Sec;
			uiSliderWrapper2.onValueChanged = (UnityAction<float>)Delegate.Combine(uiSliderWrapper2.onValueChanged, (UnityAction<float>)delegate(float v)
			{
				edgeLinkScoreViewer.roadLinkAngleMonitor.flipCooldownFreezeDuration = v;
			});
		}

		private void OnEnable()
		{
			edgeLinkScoreViewer.showGui = true;
			modeDropdown.onValueChanged.AddListener(OnModeChanged);
			roadLinkToAnchorBoostsStrength.onValueChanged.AddListener(OnRoadLinkToAnchorChanged);
			maxStrengthDeduction.onValueChanged.AddListener(OnMaxStrengthDeductionChanged);
			fullStrengthLength.onValueChanged.AddListener(OnFullStrengthLengthChanged);
			labelWidth.onValueChanged.AddListener(OnLabelWidthChanged);
			resetToDefaultSettings.onValueChanged.AddListener(OnResetToDefaultSettings);
			labelWidth_Cluster.onValueChanged.AddListener(OnLabelWidthChanged_Cluster);
			mustBeNerfedOnBothSides.onValueChanged.AddListener(OnMustBeNerfedOnBothSidesChanged);
			combineWithMethodOne.onValueChanged.AddListener(OnCombineWithMethodOneChanged);
			numDirChangesOk_AngleMonitor.onValueChanged.AddListener(OnNumDirChangesOkChanged_AngleMonitor);
			deductionPerDirChange_AngleMonitor.onValueChanged.AddListener(OnDeductionPerDirChangeChanged_AngleMonitor);
			maxStrengthDeduction_AngleMonitor.onValueChanged.AddListener(OnMaxStrengthDeductionChanged_AngleMonitor);
			anglePrecision_AngleMonitor.onValueChanged.AddListener(OnAnglePrecisionChanged_AngleMonitor);
			labelWidth_AngleMonitor.onValueChanged.AddListener(OnLabelWidthChanged_AngleMonitor);
			distRateOver05Sec.OnEnable();
			distRateOver_1Sec.OnEnable();
			distRateOver_2Sec.OnEnable();
			EdgeLinkScoreViewer obj = edgeLinkScoreViewer;
			obj.descriptionUpdate = (Action<string>)Delegate.Combine(obj.descriptionUpdate, new Action<string>(OnDescriptionUpdate));
			UpdateUiFromData();
		}

		private void OnDisable()
		{
			edgeLinkScoreViewer.showGui = false;
			modeDropdown.onValueChanged.RemoveListener(OnModeChanged);
			roadLinkToAnchorBoostsStrength.onValueChanged.RemoveListener(OnRoadLinkToAnchorChanged);
			maxStrengthDeduction.onValueChanged.RemoveListener(OnMaxStrengthDeductionChanged);
			fullStrengthLength.onValueChanged.RemoveListener(OnFullStrengthLengthChanged);
			labelWidth.onValueChanged.RemoveListener(OnLabelWidthChanged);
			resetToDefaultSettings.onValueChanged.RemoveListener(OnResetToDefaultSettings);
			labelWidth_Cluster.onValueChanged.RemoveListener(OnLabelWidthChanged_Cluster);
			mustBeNerfedOnBothSides.onValueChanged.RemoveListener(OnMustBeNerfedOnBothSidesChanged);
			combineWithMethodOne.onValueChanged.RemoveListener(OnCombineWithMethodOneChanged);
			numDirChangesOk_AngleMonitor.onValueChanged.RemoveListener(OnNumDirChangesOkChanged_AngleMonitor);
			deductionPerDirChange_AngleMonitor.onValueChanged.RemoveListener(OnDeductionPerDirChangeChanged_AngleMonitor);
			maxStrengthDeduction_AngleMonitor.onValueChanged.RemoveListener(OnMaxStrengthDeductionChanged_AngleMonitor);
			anglePrecision_AngleMonitor.onValueChanged.RemoveListener(OnAnglePrecisionChanged_AngleMonitor);
			labelWidth_AngleMonitor.onValueChanged.RemoveListener(OnLabelWidthChanged_AngleMonitor);
			distRateOver05Sec.OnDisable();
			distRateOver_1Sec.OnDisable();
			distRateOver_2Sec.OnDisable();
			EdgeLinkScoreViewer obj = edgeLinkScoreViewer;
			obj.descriptionUpdate = (Action<string>)Delegate.Remove(obj.descriptionUpdate, new Action<string>(OnDescriptionUpdate));
		}

		private void OnRoadLinkToAnchorChanged(bool isOn)
		{
			edgeLinkScoreViewer.roadLinkToAnchorBoostStrength = isOn;
			edgeLinkScoreViewer.Reset();
		}

		private void OnMaxStrengthDeductionChanged(float value)
		{
			value = Mathf.RoundToInt(value);
			edgeLinkScoreViewer.maxStrengthDeductionFraction = value / 100f;
			maxStrengthDeductionValueLabel.text = $"{value:0}%";
			edgeLinkScoreViewer.Reset();
		}

		private void OnFullStrengthLengthChanged(float value)
		{
			edgeLinkScoreViewer.fullStrengthLength = value;
			fullStrengthLengthValueLabel.text = $"{value:0.0}m";
			edgeLinkScoreViewer.Reset();
		}

		private void OnLabelWidthChanged(float value)
		{
			edgeLinkScoreViewer.desiredLabelWidthInMeters = value;
			labelWidthValueLabel.text = $"{value:0.0}m";
		}

		private void OnResetToDefaultSettings(bool isOn)
		{
			if (isOn)
			{
				resetToDefaultSettings.isOn = false;
				maxStrengthDeduction.value = 50f;
				fullStrengthLength.value = 4f;
			}
		}

		private void OnLabelWidthChanged_Cluster(float value)
		{
			edgeLinkScoreViewer.segmentation.desiredLabelWidthInMeters = value;
			labelWidthValueLabel_Cluster.text = $"{value:0.0}m";
		}

		private void OnModeChanged(int idx)
		{
			edgeLinkScoreViewer.mode = (EdgeLinkScoreViewer.Mode)idx;
			perNodeLinkingPanel.gameObject.SetActive(value: false);
			clusteringLogicPanel.gameObject.SetActive(value: false);
			angleMonitorPanel.gameObject.SetActive(value: false);
			switch (idx)
			{
			case 0:
				perNodeLinkingPanel.gameObject.SetActive(value: true);
				break;
			case 1:
				clusteringLogicPanel.gameObject.SetActive(value: true);
				break;
			case 2:
				angleMonitorPanel.gameObject.SetActive(value: true);
				break;
			}
			edgeLinkScoreViewer.Reset();
			UpdateUiFromData();
		}

		private void OnMustBeNerfedOnBothSidesChanged(bool isOn)
		{
			edgeLinkScoreViewer.roadLinkAngleMonitor.mustBeNerfedOnBothSides = isOn;
		}

		private void OnCombineWithMethodOneChanged(bool isOn)
		{
			edgeLinkScoreViewer.roadLinkAngleMonitor.combineWithMethodOne = isOn;
		}

		private void OnNumDirChangesOkChanged_AngleMonitor(float value)
		{
			int num = Mathf.RoundToInt(value);
			edgeLinkScoreViewer.roadLinkAngleMonitor.numDirectionChangesOk = num;
			numDirChangesOkValueLabel_AngleMonitor.text = $"{num}";
		}

		private void OnDeductionPerDirChangeChanged_AngleMonitor(float value)
		{
			int num = Mathf.RoundToInt(value);
			edgeLinkScoreViewer.roadLinkAngleMonitor.deductionPerDirChange = (float)num / 100f;
			deductionPerDirChangeValueLabel_AngleMonitor.text = $"{num}%";
		}

		private void OnMaxStrengthDeductionChanged_AngleMonitor(float value)
		{
			int num = Mathf.RoundToInt(value);
			edgeLinkScoreViewer.roadLinkAngleMonitor.maxDeduction = (float)num / 100f;
			maxStrengthDeductionValueLabel_AngleMonitor.text = $"{num}%";
		}

		private void OnAnglePrecisionChanged_AngleMonitor(float value)
		{
			EdgeLinkAngleMonitor.MidNode.threshold = value * (MathF.PI / 180f);
			anglePrecisionValueLabel_AngleMonitor.text = $"{value:0.0}°";
		}

		private void OnLabelWidthChanged_AngleMonitor(float value)
		{
			edgeLinkScoreViewer.roadLinkAngleMonitor.desiredLabelWidthInMeters = value;
			labelWidthValueLabel_AngleMonitor.text = $"{value:0.0}m";
		}

		private void OnDescriptionUpdate(string description)
		{
			descriptionText.text = description;
		}

		private void UpdateUiFromData()
		{
			modeDropdown.value = (int)edgeLinkScoreViewer.mode;
			roadLinkToAnchorBoostsStrength.isOn = edgeLinkScoreViewer.roadLinkToAnchorBoostStrength;
			maxStrengthDeduction.value = edgeLinkScoreViewer.maxStrengthDeductionFraction * 100f;
			fullStrengthLength.value = edgeLinkScoreViewer.fullStrengthLength;
			labelWidth.value = edgeLinkScoreViewer.desiredLabelWidthInMeters;
			labelWidth_Cluster.value = edgeLinkScoreViewer.segmentation.desiredLabelWidthInMeters;
			mustBeNerfedOnBothSides.isOn = edgeLinkScoreViewer.roadLinkAngleMonitor.mustBeNerfedOnBothSides;
			combineWithMethodOne.isOn = edgeLinkScoreViewer.roadLinkAngleMonitor.combineWithMethodOne;
			numDirChangesOk_AngleMonitor.value = edgeLinkScoreViewer.roadLinkAngleMonitor.numDirectionChangesOk;
			deductionPerDirChange_AngleMonitor.value = edgeLinkScoreViewer.roadLinkAngleMonitor.deductionPerDirChange * 100f;
			maxStrengthDeduction_AngleMonitor.value = edgeLinkScoreViewer.roadLinkAngleMonitor.maxDeduction * 100f;
			anglePrecision_AngleMonitor.value = EdgeLinkAngleMonitor.MidNode.threshold * 57.29578f;
			labelWidth_AngleMonitor.value = edgeLinkScoreViewer.roadLinkAngleMonitor.desiredLabelWidthInMeters;
			distRateOver05Sec.InitValue(edgeLinkScoreViewer.roadLinkAngleMonitor.flipCountCooldownRate);
			distRateOver_1Sec.InitValue(edgeLinkScoreViewer.roadLinkAngleMonitor.flipCooldownFreezeDuration);
			descriptionText.text = edgeLinkScoreViewer.description;
		}
	}
}
