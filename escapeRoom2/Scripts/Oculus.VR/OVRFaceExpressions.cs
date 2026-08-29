using System;
using System.Collections;
using System.Collections.Generic;
using Meta.XR.Util;
using UnityEngine;

[HelpURL("https://developer.oculus.com/documentation/unity/move-face-tracking/")]
[Feature(Feature.FaceTracking)]
public class OVRFaceExpressions : MonoBehaviour, IReadOnlyCollection<float>, IEnumerable<float>, IEnumerable, OVRFaceExpressions.WeightProvider
{
	public interface WeightProvider
	{
		float GetWeight(FaceExpression expression);
	}

	public enum FaceRegionConfidence
	{
		Lower = 0,
		Upper = 1,
		Max = 2
	}

	public enum FaceTrackingDataSource
	{
		Visual = 0,
		Audio = 1,
		[InspectorName(null)]
		Count = 2
	}

	public enum FaceExpression
	{
		[InspectorName("None")]
		Invalid = -1,
		BrowLowererL = 0,
		BrowLowererR = 1,
		CheekPuffL = 2,
		CheekPuffR = 3,
		CheekRaiserL = 4,
		CheekRaiserR = 5,
		CheekSuckL = 6,
		CheekSuckR = 7,
		ChinRaiserB = 8,
		ChinRaiserT = 9,
		DimplerL = 10,
		DimplerR = 11,
		EyesClosedL = 12,
		EyesClosedR = 13,
		EyesLookDownL = 14,
		EyesLookDownR = 15,
		EyesLookLeftL = 16,
		EyesLookLeftR = 17,
		EyesLookRightL = 18,
		EyesLookRightR = 19,
		EyesLookUpL = 20,
		EyesLookUpR = 21,
		InnerBrowRaiserL = 22,
		InnerBrowRaiserR = 23,
		JawDrop = 24,
		JawSidewaysLeft = 25,
		JawSidewaysRight = 26,
		JawThrust = 27,
		LidTightenerL = 28,
		LidTightenerR = 29,
		LipCornerDepressorL = 30,
		LipCornerDepressorR = 31,
		LipCornerPullerL = 32,
		LipCornerPullerR = 33,
		LipFunnelerLB = 34,
		LipFunnelerLT = 35,
		LipFunnelerRB = 36,
		LipFunnelerRT = 37,
		LipPressorL = 38,
		LipPressorR = 39,
		LipPuckerL = 40,
		LipPuckerR = 41,
		LipStretcherL = 42,
		LipStretcherR = 43,
		LipSuckLB = 44,
		LipSuckLT = 45,
		LipSuckRB = 46,
		LipSuckRT = 47,
		LipTightenerL = 48,
		LipTightenerR = 49,
		LipsToward = 50,
		LowerLipDepressorL = 51,
		LowerLipDepressorR = 52,
		MouthLeft = 53,
		MouthRight = 54,
		NoseWrinklerL = 55,
		NoseWrinklerR = 56,
		OuterBrowRaiserL = 57,
		OuterBrowRaiserR = 58,
		UpperLidRaiserL = 59,
		UpperLidRaiserR = 60,
		UpperLipRaiserL = 61,
		UpperLipRaiserR = 62,
		TongueTipInterdental = 63,
		TongueTipAlveolar = 64,
		TongueFrontDorsalPalate = 65,
		TongueMidDorsalPalate = 66,
		TongueBackDorsalVelar = 67,
		TongueOut = 68,
		TongueRetreat = 69,
		[InspectorName(null)]
		Max = 70
	}

	public struct FaceExpressionsEnumerator : IEnumerator<float>, IEnumerator, IDisposable
	{
		private float[] _faceExpressions;

		private int _index;

		private int _count;

		public float Current => _faceExpressions[_index];

		object IEnumerator.Current => Current;

		internal FaceExpressionsEnumerator(float[] array)
		{
			_faceExpressions = array;
			_index = -1;
			float[] faceExpressions = _faceExpressions;
			_count = ((faceExpressions != null) ? faceExpressions.Length : 0);
		}

		public bool MoveNext()
		{
			return ++_index < _count;
		}

		public void Reset()
		{
			_index = -1;
		}

		public void Dispose()
		{
		}
	}

	public enum FaceViseme
	{
		[InspectorName("None")]
		Invalid = -1,
		SIL = 0,
		PP = 1,
		FF = 2,
		TH = 3,
		DD = 4,
		KK = 5,
		CH = 6,
		SS = 7,
		NN = 8,
		RR = 9,
		AA = 10,
		E = 11,
		IH = 12,
		OH = 13,
		OU = 14,
		[InspectorName(null)]
		Count = 15
	}

	private OVRPlugin.FaceState _currentFaceState;

	private OVRPlugin.FaceVisemesState _currentFaceVisemesState;

	private const OVRPermissionsRequester.Permission FaceTrackingPermission = OVRPermissionsRequester.Permission.FaceTracking;

	private const OVRPermissionsRequester.Permission RecordAudioPermission = OVRPermissionsRequester.Permission.RecordAudio;

	private Action<string> _onPermissionGranted;

	private static int _trackingInstanceCount;

	public bool FaceTrackingEnabled => OVRPlugin.faceTracking2Enabled;

	public bool ValidExpressions { get; private set; }

	public bool EyeFollowingBlendshapesValid { get; private set; }

	public bool AreVisemesValid { get; private set; }

	public float this[FaceExpression expression]
	{
		get
		{
			CheckValidity();
			if (expression < FaceExpression.BrowLowererL || expression >= FaceExpression.Max)
			{
				throw new ArgumentOutOfRangeException("expression", expression, $"Value must be between 0 to {70}");
			}
			return _currentFaceState.ExpressionWeights[(int)expression];
		}
	}

	public int Count
	{
		get
		{
			float[] expressionWeights = _currentFaceState.ExpressionWeights;
			if (expressionWeights == null)
			{
				return 0;
			}
			return expressionWeights.Length;
		}
	}

	private void Awake()
	{
		_onPermissionGranted = OnPermissionGranted;
	}

	private void OnEnable()
	{
		_trackingInstanceCount++;
		if (!StartFaceTracking())
		{
			base.enabled = false;
		}
	}

	private void OnPermissionGranted(string permissionId)
	{
		if (permissionId == OVRPermissionsRequester.GetPermissionId(OVRPermissionsRequester.Permission.FaceTracking) || permissionId == OVRPermissionsRequester.GetPermissionId(OVRPermissionsRequester.Permission.RecordAudio))
		{
			OVRPermissionsRequester.PermissionGranted -= _onPermissionGranted;
			base.enabled = true;
		}
	}

	private OVRPlugin.FaceTrackingDataSource[] GetRequestedFaceTrackingDataSources()
	{
		OVRRuntimeSettings runtimeSettings = OVRRuntimeSettings.GetRuntimeSettings();
		if (runtimeSettings.RequestsAudioFaceTracking && runtimeSettings.RequestsVisualFaceTracking)
		{
			return new OVRPlugin.FaceTrackingDataSource[2]
			{
				OVRPlugin.FaceTrackingDataSource.Visual,
				OVRPlugin.FaceTrackingDataSource.Audio
			};
		}
		if (runtimeSettings.RequestsVisualFaceTracking)
		{
			return new OVRPlugin.FaceTrackingDataSource[1];
		}
		if (runtimeSettings.RequestsAudioFaceTracking)
		{
			return new OVRPlugin.FaceTrackingDataSource[1] { OVRPlugin.FaceTrackingDataSource.Audio };
		}
		return new OVRPlugin.FaceTrackingDataSource[0];
	}

	private bool StartFaceTracking()
	{
		if (!OVRPermissionsRequester.IsPermissionGranted(OVRPermissionsRequester.Permission.FaceTracking) && !OVRPermissionsRequester.IsPermissionGranted(OVRPermissionsRequester.Permission.RecordAudio))
		{
			OVRPermissionsRequester.PermissionGranted -= _onPermissionGranted;
			OVRPermissionsRequester.PermissionGranted += _onPermissionGranted;
			return false;
		}
		if (!OVRPlugin.StartFaceTracking2(GetRequestedFaceTrackingDataSources()))
		{
			Debug.LogWarning("[OVRFaceExpressions] Failed to start face tracking.");
			return false;
		}
		OVRPlugin.SetFaceTrackingVisemesEnabled(OVRRuntimeSettings.GetRuntimeSettings().EnableFaceTrackingVisemesOutput);
		return true;
	}

	private void OnDisable()
	{
		if (--_trackingInstanceCount == 0)
		{
			OVRPlugin.StopFaceTracking2();
		}
	}

	private void OnDestroy()
	{
		OVRPermissionsRequester.PermissionGranted -= _onPermissionGranted;
	}

	private void Update()
	{
		ValidExpressions = OVRPlugin.GetFaceState2(OVRPlugin.Step.Render, -1, ref _currentFaceState) && _currentFaceState.Status.IsValid;
		EyeFollowingBlendshapesValid = ValidExpressions && _currentFaceState.Status.IsEyeFollowingBlendshapesValid;
		AreVisemesValid = OVRPlugin.GetFaceVisemesState(OVRPlugin.Step.Render, ref _currentFaceVisemesState) == OVRPlugin.Result.Success && _currentFaceVisemesState.IsValid;
	}

	public float GetWeight(FaceExpression expression)
	{
		return this[expression];
	}

	public bool TryGetFaceExpressionWeight(FaceExpression expression, out float weight)
	{
		if (!ValidExpressions || expression < FaceExpression.BrowLowererL || expression >= FaceExpression.Max)
		{
			weight = 0f;
			return false;
		}
		weight = _currentFaceState.ExpressionWeights[(int)expression];
		return true;
	}

	public float GetViseme(FaceViseme viseme)
	{
		CheckVisemesValidity();
		if (viseme < FaceViseme.SIL || viseme >= FaceViseme.Count)
		{
			throw new ArgumentOutOfRangeException("viseme", viseme, $"Value must be between 0 to {15}");
		}
		return _currentFaceVisemesState.Visemes[(int)viseme];
	}

	public bool TryGetFaceViseme(FaceViseme viseme, out float weight)
	{
		if (!AreVisemesValid || viseme < FaceViseme.SIL || viseme >= FaceViseme.Count)
		{
			weight = 0f;
			return false;
		}
		weight = _currentFaceVisemesState.Visemes[(int)viseme];
		return true;
	}

	public void CopyVisemesTo(float[] array, int startIndex = 0)
	{
		if (array == null)
		{
			throw new ArgumentNullException("array");
		}
		if (startIndex < 0 || startIndex >= array.Length)
		{
			throw new ArgumentOutOfRangeException("startIndex", startIndex, $"Value must be between 0 to {array.Length - 1}");
		}
		if (array.Length - startIndex < 15)
		{
			throw new ArgumentException($"Capacity is too small - required {15}, available {array.Length - startIndex}.", "array");
		}
		CheckVisemesValidity();
		for (int i = 0; i < 15; i++)
		{
			array[i + startIndex] = _currentFaceVisemesState.Visemes[i];
		}
	}

	public bool TryGetWeightConfidence(FaceRegionConfidence region, out float weightConfidence)
	{
		if (!ValidExpressions || region < FaceRegionConfidence.Lower || region >= FaceRegionConfidence.Max)
		{
			weightConfidence = 0f;
			return false;
		}
		weightConfidence = _currentFaceState.ExpressionWeightConfidences[(int)region];
		return true;
	}

	public bool TryGetFaceTrackingDataSource(out FaceTrackingDataSource dataSource)
	{
		dataSource = (FaceTrackingDataSource)_currentFaceState.DataSource;
		return ValidExpressions;
	}

	internal void CheckValidity()
	{
		if (!ValidExpressions)
		{
			throw new InvalidOperationException("Face expressions are not valid at this time. Use ValidExpressions to check for validity.");
		}
	}

	internal void CheckVisemesValidity()
	{
		if (!AreVisemesValid)
		{
			throw new InvalidOperationException("Face visemes are not valid at this time. Use AreVisemesValid to check for validity.");
		}
	}

	public void CopyTo(float[] array, int startIndex = 0)
	{
		if (array == null)
		{
			throw new ArgumentNullException("array");
		}
		if (startIndex < 0 || startIndex >= array.Length)
		{
			throw new ArgumentOutOfRangeException("startIndex", startIndex, $"Value must be between 0 to {array.Length - 1}");
		}
		if (array.Length - startIndex < 70)
		{
			throw new ArgumentException($"Capacity is too small - required {70}, available {array.Length - startIndex}.", "array");
		}
		CheckValidity();
		for (int i = 0; i < 70; i++)
		{
			array[i + startIndex] = _currentFaceState.ExpressionWeights[i];
		}
	}

	public float[] ToArray()
	{
		float[] array = new float[70];
		CopyTo(array);
		return array;
	}

	public FaceExpressionsEnumerator GetEnumerator()
	{
		return new FaceExpressionsEnumerator(_currentFaceState.ExpressionWeights);
	}

	IEnumerator<float> IEnumerable<float>.GetEnumerator()
	{
		return GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
