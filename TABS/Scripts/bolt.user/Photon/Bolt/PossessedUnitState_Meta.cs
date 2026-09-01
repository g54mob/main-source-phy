using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class PossessedUnitState_Meta : NetworkState_Meta, ISerializerFactory, IFactory
	{
		internal static PossessedUnitState_Meta Instance;

		internal ObjectPool<PossessedUnitState> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(160, 67, 106, 67, 5, 142, 21, 74, 175, 23, 68, 142, 190, 11, 107, 2);

		Type IFactory.TypeObject => typeof(IPossessedUnitState);

		static PossessedUnitState_Meta()
		{
			Instance = new PossessedUnitState_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(37);
			CountStorage = 3;
			CountObjects = 1;
			CountProperties = 1;
			Properties = new NetworkPropertyInfo[1];
			PropertyIdBits = 1;
			PacketMaxBits = 512;
			PacketMaxProperties = 16;
			PacketMaxPropertiesBits = 5;
			InstantiationPositionCompression = PropertyVectorCompressionSettings.Create(PropertyFloatCompressionSettings.Create(), PropertyFloatCompressionSettings.Create(), PropertyFloatCompressionSettings.Create());
			InstantiationRotationCompression = PropertyQuaternionCompression.Create(PropertyFloatCompressionSettings.Create());
			NetworkProperty_Transform networkProperty_Transform = new NetworkProperty_Transform();
			networkProperty_Transform.PropertyMeta = this;
			networkProperty_Transform.Settings_Property("MainTransform", 1, -1073741824);
			networkProperty_Transform.Settings_Offsets(0, 0);
			networkProperty_Transform.Settings_Space(TransformSpaces.World);
			networkProperty_Transform.Settings_Vector(PropertyFloatCompressionSettings.Create(20, 4000f, 100f, 0.01f), PropertyFloatCompressionSettings.Create(19, 2000f, 100f, 0.01f), PropertyFloatCompressionSettings.Create(20, 4000f, 100f, 0.01f), strict: false);
			networkProperty_Transform.Settings_Quaternion(PropertyFloatCompressionSettings.Create(), strict: false);
			networkProperty_Transform.Settings_Interpolation(10f, enabled: true);
			AddProperty(0, 0, networkProperty_Transform, -1);
			base.InitMeta();
			_pool = new ObjectPool<PossessedUnitState>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as PossessedUnitState);
		}
	}
}
