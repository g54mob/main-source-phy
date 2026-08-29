using Unity.Mathematics;

namespace MagicaCloth2
{
	public static class Define
	{
		public enum Result
		{
			None = 0,
			Empty = 1,
			Success = 2,
			Cancel = 3,
			Process = 4,
			Warning = 10000,
			RenderMesh_UnknownWarning = 10100,
			RenderMesh_VertexWeightIs5BonesOrMore = 10101,
			Init_NonUniformScale = 10200,
			Error = 20000,
			SerializeData_InvalidData = 20050,
			SerializeData_Over31Renderers = 20051,
			SerializeData_DuplicateRootBone = 20052,
			SerializeData_DuplicateRenderer = 20053,
			Init_InvalidData = 20100,
			Init_InvalidPaintMap = 20101,
			Init_PaintMapNotReadable = 20102,
			Init_ScaleIsZero = 20103,
			Init_NegativeScale = 20104,
			RenderSetup_Exception = 20200,
			RenderSetup_UnknownError = 20201,
			RenderSetup_InvalidSource = 20202,
			RenderSetup_NoMeshOnRenderer = 20203,
			RenderSetup_InvalidType = 20204,
			RenderSetup_Unreadable = 20205,
			RenderSetup_Over65535vertices = 20206,
			VirtualMesh_UnknownError = 20300,
			VirtualMesh_InvalidSetup = 20301,
			VirtualMesh_InvalidRenderData = 20302,
			VirtualMesh_ImportError = 20303,
			VirtualMesh_SelectionException = 20304,
			VirtualMesh_SelectionUnknownError = 20305,
			VirtualMesh_InvalidSelection = 20306,
			CreateCloth_Exception = 20400,
			CreateCloth_UnknownError = 20401,
			CreateCloth_InvalidCloth = 20402,
			CreateCloth_InvalidSerializeData = 20403,
			CreateCloth_InvalidSetupList = 20404,
			CreateCloth_NoRenderer = 20405,
			CreateCloth_InvalidPaintMap = 20406,
			CreateCloth_PaintMapNotReadable = 20407,
			CreateCloth_PaintMapCountMismatch = 20408,
			CreateCloth_CanNotStart = 20409,
			CreateCloth_VertexAttributeListCountMismatch = 20410,
			CreateCloth_VertexAttributeListIsNull = 20411,
			CreateCloth_VertexAttributeListDataMismatch = 20412,
			CreateCloth_InvalidVertexAttributeData = 20413,
			Reduction_Exception = 20500,
			Reduction_UnknownError = 20501,
			Reduction_InitError = 20502,
			Reduction_SameDistanceException = 20503,
			Reduction_SimpleDistanceException = 20504,
			Reduction_ShapeDistanceException = 20505,
			Reduction_MaxSideLengthZero = 20506,
			Reduction_OrganizationError = 20507,
			Reduction_StoreVirtualMeshError = 20508,
			Reduction_CalcAverageException = 20509,
			Optimize_Exception = 20600,
			ProxyMesh_Exception = 20700,
			ProxyMesh_UnknownError = 20701,
			ProxyMesh_ApplySelectionError = 20702,
			ProxyMesh_ConvertError = 20703,
			ProxyMesh_Over32767Vertices = 20704,
			ProxyMesh_Over32767Edges = 20705,
			ProxyMesh_Over32767Triangles = 20706,
			MappingMesh_Exception = 20800,
			MappingMesh_UnknownError = 20801,
			MappingMesh_ProxyError = 20802,
			ClothInit_Exception = 22000,
			ClothInit_FailedAddRenderer = 22001,
			ClothProcess_Exception = 22100,
			ClothProcess_UnknownError = 22101,
			ClothProcess_Invalid = 22102,
			ClothProcess_InvalidRenderHandleList = 22103,
			ClothProcess_GenerateSelectionError = 22104,
			ClothProcess_OverflowTeamCount4096 = 22105,
			Constraint_Exception = 22200,
			Constraint_UnknownError = 22201,
			Constraint_CreateDistanceException = 22202,
			Constraint_CreateTriangleBendingException = 22203,
			Constraint_CreateInertiaException = 22204,
			Constraint_CreateSelfCollisionException = 22205,
			MagicaMesh_UnknownError = 22500,
			MagicaMesh_Invalid = 22501,
			MagicaMesh_InvalidRenderer = 22502,
			MagicaMesh_InvalidMeshFilter = 22503,
			PreBuildData_UnknownError = 22600,
			PreBuildData_MagicaClothException = 22601,
			PreBuildData_VirtualMeshDeserializationException = 22602,
			PreBuildData_VerificationResult = 22603,
			PreBuildData_VersionMismatch = 22604,
			PreBuildData_InvalidClothData = 22605,
			PreBuildData_Empty = 22606,
			PreBuildData_InvalidScale = 22607,
			PreBuild_UnknownError = 22700,
			PreBuild_Exception = 22701,
			PreBuild_InvalidPreBuildData = 22702,
			PreBuild_InvalidRenderSetupData = 22703,
			PreBuild_SetupDeserializationError = 22704,
			Deserialization_UnknownError = 22800,
			Deserialization_Exception = 22801,
			InitSerializeData_UnknownError = 22900,
			InitSerializeData_InvalidHash = 22901,
			InitSerializeData_InvalidVersion = 22902,
			InitSerializeData_InvalidSetupData = 22903,
			InitSerializeData_ClothTypeMismatch = 22904,
			InitSerializeData_SetupCountMismatch = 22905,
			InitSerializeData_CustomSkinningBoneCountMismatch = 22906,
			InitSerializeData_MeshClothSetupValidationError = 22907,
			InitSerializeData_BoneClothSetupValidationError = 22908,
			InitSerializeData_BoneSpringSetupValidationError = 22909,
			InitSerializeData_DeserializationError = 22910,
			InitSerializeData_InvalidCloneMesh = 22911
		}

		public static class System
		{
			public const string DefineSymbol = "MAGICACLOTH2";

			public const int LatestPreBuildVersion = 2;

			public const float Epsilon = 1E-08f;

			public const int MaxRendererCount = 31;

			public const float MinimumGridSize = 1E-05f;

			public const int MaximumTeamCount = 4096;

			public const int DefaultSimulationFrequency = 90;

			public const int SimulationFrequency_Low = 30;

			public const int SimulationFrequency_Hi = 150;

			public const int DefaultMaxSimulationCountPerFrame = 3;

			public const int MaxSimulationCountPerFrame_Low = 1;

			public const int MaxSimulationCountPerFrame_Hi = 5;

			public const float SameSurfaceAngle = 80f;

			public const float MaxDistanceRatioFutuerPrediction = 1.3f;

			public const int SplitProxyMeshVertexCount = 300;

			public const bool ReductionEnable = true;

			public const float ReductionSameDistance = 0.001f;

			public const bool ReductionDontMakeLine = true;

			public const float ReductionJoinPositionAdjustment = 1f;

			public const int ReductionMaxStep = 100;

			public const int MaxProxyMeshVertexCount = 32767;

			public const int MaxProxyMeshEdgeCount = 32767;

			public const int MaxProxyMeshTriangleCount = 32767;

			public const float ProxyMeshTrianglePairAngle = 20f;

			public const float ProxyMeshBoneClothTriangleAngle = 120f;

			public const float FrictionMass = 3f;

			public const float DepthMass = 5f;

			public const float FrictionDampingRate = 0.6f;

			public const float PositionAverageExponent = 0.5f;

			public const float MaxRealVelocity = 0.5f;

			public const float TetherCompressionStiffness = 1f;

			public const float TetherStretchStiffness = 1f;

			public const float TetherStretchLimit = 0.03f;

			public const float TetherStiffnessWidth = 0.3f;

			public const float TetherCompressionVelocityAttenuation = 0.7f;

			public const float TetherStretchVelocityAttenuation = 0.7f;

			public const float DistanceVelocityAttenuation = 0.3f;

			public const float DistanceVerticalStiffness = 1f;

			public const float DistanceHorizontalStiffness = 0.5f;

			public const float TriangleBendingMaxAngle = 120f;

			public const float VolumeMinAngle = 90f;

			public const float MaxAngleLimit = 179f;

			public const int AngleLimitIteration = 3;

			public const float AngleLimitAttenuation = 0.9f;

			public const float MaxMovementSpeedLimit = 10f;

			public const float MaxRotationSpeedLimit = 1440f;

			public const float MaxParticleSpeedLimit = 10f;

			public const int ExpandedColliderCount = 8;

			public const float ColliderCollisionDynamicFrictionRatio = 1f;

			public const float ColliderCollisionStaticFrictionRatio = 1f;

			public const float CustomSkinningAngularAttenuation = 1f;

			public const float CustomSkinningDistanceReduction = 0.6f;

			public const float CustomSkinningDistancePow = 2f;

			public const int SelfCollisionSolverIteration = 4;

			public const int SelfCollisionIgnoreGrid = 1000000;

			public const int SelfCollisionIntersectDiv = 2;

			public const float SelfCollisionFixedMass = 100f;

			public const float SelfCollisionFrictionMass = 10f;

			public const float SelfCollisionClothMass = 50f;

			public const float SelfCollisionSCR = 2f;

			public static readonly float SelfCollisionPointTriangleAngleCos = math.cos(math.radians(60f));

			public const float SelfCollisionThicknessMin = 0.001f;

			public const float SelfCollisionThicknessMax = 0.05f;

			public const float WindMaxTime = 10000f;

			public const float WindBaseSpeed = 7.5f;

			public const float BoneSpringDistanceStiffness = 0.5f;

			public const float BoneSpringTetherCompressionLimit = 0.8f;

			public const float BoneSpringCollisionFriction = 0.5f;

			public const float DistanceCullingMaxLength = 100f;
		}
	}
}
