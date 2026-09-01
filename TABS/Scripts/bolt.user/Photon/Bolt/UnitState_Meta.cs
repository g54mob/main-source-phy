using System;
using Photon.Bolt.Internal;

namespace Photon.Bolt
{
	internal class UnitState_Meta : NetworkState_Meta, ISerializerFactory, IFactory
	{
		internal static UnitState_Meta Instance;

		internal ObjectPool<UnitState> _pool;

		TypeId IFactory.TypeId => TypeId;

		UniqueId IFactory.TypeKey => new UniqueId(83, 16, 36, 114, 209, 112, 58, 76, 171, 8, 237, 225, 79, 96, 111, 236);

		Type IFactory.TypeObject => typeof(IUnitState);

		static UnitState_Meta()
		{
			Instance = new UnitState_Meta();
			Instance.InitMeta();
		}

		internal override void InitObject(NetworkObj obj, Offsets offsets)
		{
		}

		internal override void InitMeta()
		{
			TypeId = new TypeId(38);
			CountStorage = 6;
			CountObjects = 1;
			CountProperties = 4;
			Properties = new NetworkPropertyInfo[4];
			PropertyIdBits = 3;
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
			NetworkProperty_Integer networkProperty_Integer = new NetworkProperty_Integer();
			networkProperty_Integer.PropertyMeta = this;
			networkProperty_Integer.Settings_Property("MovementSpeed", 1, -1073741824);
			networkProperty_Integer.Settings_Offsets(1, 3);
			networkProperty_Integer.Settings_Mecanim(MecanimMode.Disabled, MecanimDirection.UsingAnimatorMethods, 0f, 0);
			networkProperty_Integer.Settings_Integer(PropertyIntCompressionSettings.Create(2, 1));
			AddProperty(1, 0, networkProperty_Integer, -1);
			NetworkProperty_Integer networkProperty_Integer2 = new NetworkProperty_Integer();
			networkProperty_Integer2.PropertyMeta = this;
			networkProperty_Integer2.Settings_Property("TargetShortNetworkId", 1, -1073741824);
			networkProperty_Integer2.Settings_Offsets(2, 4);
			networkProperty_Integer2.Settings_Mecanim(MecanimMode.Disabled, MecanimDirection.UsingAnimatorMethods, 0f, 0);
			networkProperty_Integer2.Settings_Integer(PropertyIntCompressionSettings.Create(16, 32768));
			AddProperty(2, 0, networkProperty_Integer2, -1);
			NetworkProperty_Float networkProperty_Float = new NetworkProperty_Float();
			networkProperty_Float.PropertyMeta = this;
			networkProperty_Float.Settings_Property("LookDirectionAngle", 1, -1073741824);
			networkProperty_Float.Settings_Offsets(3, 5);
			networkProperty_Float.Settings_Mecanim(MecanimMode.Disabled, MecanimDirection.UsingAnimatorMethods, 0f, 0);
			networkProperty_Float.Settings_Float(new PropertyFloatSettings
			{
				IsAngle = false
			});
			networkProperty_Float.Settings_Float(PropertyFloatCompressionSettings.Create(19, 181f, 999.9999f, 0.001f));
			AddProperty(3, 0, networkProperty_Float, -1);
			base.InitMeta();
			_pool = new ObjectPool<UnitState>();
		}

		object IFactory.Create()
		{
			return _pool.Get();
		}

		void IFactory.Return(object objToReturn)
		{
			_pool.Return(objToReturn as UnitState);
		}
	}
}
