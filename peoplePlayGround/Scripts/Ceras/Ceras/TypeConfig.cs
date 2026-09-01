using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using Ceras.Exceptions;
using Ceras.Formatters;
using Ceras.Helpers;

namespace Ceras
{
	public abstract class TypeConfig
	{
		private bool _isSealed;

		protected TypeConstruction _typeConstruction;

		internal Dictionary<ParameterInfo, MemberInfo> ParameterMap;

		protected FormatterResolverCallback _customResolver;

		protected Ceras.Formatters.IFormatter _overrideFormatter;

		protected ReadonlyFieldHandling? _readonlyOverride;

		protected TargetMember? _targetMembers;

		internal readonly List<MemberConfig> _allMembers;

		public Type Type { get; }

		public SerializerConfig Config { get; }

		public bool IsStatic { get; }

		public TypeConstruction TypeConstruction
		{
			get
			{
				return _typeConstruction;
			}
			set
			{
				ThrowIfSealed();
				_typeConstruction = value;
				if (value != null)
				{
					TypeConstruction.TypeConfig = this;
					TypeConstruction.VerifyReturnType();
				}
			}
		}

		public FormatterResolverCallback CustomResolver
		{
			get
			{
				return _customResolver;
			}
			set
			{
				ThrowIfSealed();
				if (_overrideFormatter != null)
				{
					ThrowCantSetResolverAndFormatter();
				}
				_customResolver = value;
			}
		}

		public Ceras.Formatters.IFormatter CustomFormatter
		{
			get
			{
				return _overrideFormatter;
			}
			set
			{
				ThrowIfSealed();
				if (_customResolver != null)
				{
					ThrowCantSetResolverAndFormatter();
				}
				if (value != null)
				{
					FormatterHelper.ThrowOnMismatch(value, Type);
					_overrideFormatter = value;
				}
				else
				{
					_overrideFormatter = null;
				}
			}
		}

		public ReadonlyFieldHandling? ReadonlyFieldOverride
		{
			get
			{
				return _readonlyOverride;
			}
			set
			{
				ThrowIfSealed();
				_readonlyOverride = value;
			}
		}

		public TargetMember? TargetMembers
		{
			get
			{
				return _targetMembers;
			}
			set
			{
				ThrowIfSealed();
				_targetMembers = value;
			}
		}

		public IEnumerable<MemberConfig> Members => _allMembers.Where((MemberConfig m) => !m.IsCompilerGenerated);

		private void ThrowCantSetResolverAndFormatter()
		{
			throw new InvalidOperationException("You can only set a custom resolver or a custom formatter instance, not both.");
		}

		protected TypeConfig(SerializerConfig config, Type type, bool isStatic)
		{
			Config = config;
			Type = type;
			IsStatic = isStatic;
			Type configType = typeof(MemberConfig<>).MakeGenericType(type);
			_allMembers = (from m in isStatic ? type.GetAllStaticDataMembers() : type.GetAllDataMembers()
				let a = new object[2] { this, m }
				select (MemberConfig)Activator.CreateInstance(configType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, a, null)).ToList();
			TypeConfigDefaults.ApplyTypeAttributes(this);
			TypeConfigDefaults.ApplySpecializedDefaults(this);
			foreach (MemberConfig allMember in _allMembers)
			{
				TypeConfigDefaults.ApplyMemberAttributes(allMember);
			}
		}

		internal MemberConfig GetMemberConfig(MemberInfo memberInfo)
		{
			for (int i = 0; i < _allMembers.Count; i++)
			{
				MemberConfig memberConfig = _allMembers[i];
				if (memberConfig.Member == memberInfo)
				{
					return memberConfig;
				}
			}
			throw new InvalidOperationException("Cannot find member '" + memberInfo.DeclaringType.FriendlyName() + "." + memberInfo.Name + "' in type '" + Type.FriendlyName(fullName: true) + "'");
		}

		public IEnumerable<MemberConfig> UnsafeGetAllMembersIncludingCompilerGenerated()
		{
			return _allMembers;
		}

		public bool TryConfigureLikeDataContractSerializer(bool force = false)
		{
			if (Type.GetCustomAttribute<DataContractAttribute>() == null && !force)
			{
				return false;
			}
			foreach (MemberConfig member in Members)
			{
				if (member.Member.GetCustomAttribute<IgnoreDataMemberAttribute>(inherit: true) != null)
				{
					member.ExcludeWithReason("User has called 'TryConfigureLikeDataContractSerializer', and the member has the [IgnoreDataMember] attribute.");
					continue;
				}
				DataMemberAttribute customAttribute = member.Member.GetCustomAttribute<DataMemberAttribute>(inherit: true);
				if (customAttribute != null)
				{
					if (!string.IsNullOrWhiteSpace(customAttribute.Name))
					{
						member.PersistentName = customAttribute.Name;
					}
					member.SetIncludeWithReason(SerializationOverride.ForceInclude, "User has called 'TryConfigureLikeDataContractSerializer', and the member has the [DataMember] attribute.");
				}
			}
			return true;
		}

		internal void Seal()
		{
			_isSealed = true;
		}

		internal void VerifyConstructionMethod()
		{
			if (TypeConstruction == null)
			{
				throw new CerasException("You have not configured a construction mode for the type '" + Type.FullName + "' and it has no parameterless constructor. There are many ways Ceras can handle this, select one of the methods in the TypeConfig ('config.ConfigType<YourType>().ConstructBy(...)')");
			}
			TypeConstruction.VerifyReturnType();
			TypeConstruction.VerifyParameterMapping();
		}

		internal void ThrowIfSealed()
		{
			if (_isSealed)
			{
				throw new ConfigurationSealedException("The configuration for this Type or Member is already sealed because the SerializationSchema has been instantiated (which means dynamically emitted code relies on it not changing anymore). All changes to the type or member configuration must be made before the the configuration is used in a 'CerasSerializer' instance, except for config callbacks like OnConfigNewType)");
			}
		}
	}
	public class TypeConfig<T> : TypeConfig
	{
		public new IFormatter<T> CustomFormatter
		{
			get
			{
				return (IFormatter<T>)base.CustomFormatter;
			}
			set
			{
				base.CustomFormatter = value;
			}
		}

		internal TypeConfig(SerializerConfig config, bool isStatic)
			: base(config, typeof(T), isStatic)
		{
		}

		public TypeConfig<T> ConstructByFormatter()
		{
			base.TypeConstruction = TypeConstruction.Null();
			return this;
		}

		public TypeConfig<T> ConstructBy(MethodInfo methodInfo)
		{
			base.TypeConstruction = new ConstructByMethod(methodInfo);
			return this;
		}

		public TypeConfig<T> ConstructBy(object instance, MethodInfo methodInfo)
		{
			base.TypeConstruction = new ConstructByMethod(instance, methodInfo);
			return this;
		}

		public TypeConfig<T> ConstructBy(ConstructorInfo constructorInfo)
		{
			if (constructorInfo.IsAbstract || constructorInfo.DeclaringType != base.Type)
			{
				throw new InvalidOperationException("This constructor does not belong to the type " + base.Type.FullName);
			}
			base.TypeConstruction = new SpecificConstructor(constructorInfo);
			return this;
		}

		public TypeConfig<T> ConstructByDelegate(Func<T> factory)
		{
			object target = factory.Target;
			MethodInfo method = factory.Method;
			base.TypeConstruction = new ConstructByMethod(target, method);
			return this;
		}

		public TypeConfig<T> ConstructBy(Expression<Func<T>> methodSelectExpression)
		{
			return ConstructBy(null, methodSelectExpression);
		}

		public TypeConfig<T> ConstructBy(object instance, Expression<Func<T>> methodSelectExpression)
		{
			Expression body = methodSelectExpression.Body;
			if (body is NewExpression newExpression)
			{
				if (instance != null)
				{
					throw new InvalidOperationException("You can't specify a constructor and an instance at the same time");
				}
				ConstructorInfo constructor = newExpression.Constructor;
				base.TypeConstruction = new SpecificConstructor(constructor);
			}
			else
			{
				if (!(body is MethodCallExpression methodCallExpression))
				{
					throw new InvalidOperationException("The given expression must be a 'method-call' or 'new-expression'");
				}
				if (instance == null)
				{
					base.TypeConstruction = new ConstructByMethod(methodCallExpression.Method);
				}
				else
				{
					base.TypeConstruction = new ConstructByMethod(instance, methodCallExpression.Method);
				}
			}
			return this;
		}

		public TypeConfig<T> ConstructBy(TypeConstruction manualConstructConfig)
		{
			base.TypeConstruction = manualConstructConfig;
			return this;
		}

		public TypeConfig<T> ConstructByUninitialized()
		{
			base.TypeConstruction = new UninitializedObject();
			return this;
		}

		public TypeConfig<T> MapParameters(Dictionary<ParameterInfo, MemberInfo> mapping)
		{
			if (ParameterMap != null)
			{
				throw new InvalidOperationException("ParameterMapping is already set");
			}
			if (_typeConstruction == null)
			{
				throw new InvalidOperationException("You must set a type construction method before mapping parameters");
			}
			ParameterMap = mapping.ToDictionary((KeyValuePair<ParameterInfo, MemberInfo> kvp) => kvp.Key, (KeyValuePair<ParameterInfo, MemberInfo> kvp) => kvp.Value);
			return this;
		}

		public TypeConfig<T> SetFormatter(IFormatter<T> formatterInstance)
		{
			CustomFormatter = formatterInstance;
			return this;
		}

		public TypeConfig<T> SetReadonlyHandling(ReadonlyFieldHandling mode)
		{
			_readonlyOverride = mode;
			return this;
		}

		public TypeConfig<T> SetTargetMembers(TargetMember targets)
		{
			_targetMembers = targets;
			return this;
		}

		public MemberConfig<T> ConfigMember<TMember>(Expression<Func<T, TMember>> selectMemberExpression)
		{
			MemberInfo memberInfo = ((MemberExpression)selectMemberExpression.Body).Member;
			return (MemberConfig<T>)base.Members.FirstOrDefault((MemberConfig m) => m.Member == memberInfo);
		}

		public MemberConfig<T> ConfigField(string fieldName)
		{
			return (MemberConfig<T>)base.Members.FirstOrDefault((MemberConfig m) => m.Member is FieldInfo && m.Member.Name == fieldName);
		}

		public MemberConfig<T> ConfigProperty(string propName)
		{
			return (MemberConfig<T>)base.Members.FirstOrDefault((MemberConfig m) => m.Member is PropertyInfo && m.Member.Name == propName);
		}
	}
}
