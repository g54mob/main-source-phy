using IKVM.Reflection.Emit;

namespace IKVM.Reflection
{
	internal sealed class MethodInfoWithReflectedType : MethodInfo
	{
		private readonly Type reflectedType;

		private readonly MethodInfo method;

		internal override MethodSignature MethodSignature
		{
			get
			{
				return method.MethodSignature;
			}
		}

		internal override int ParameterCount
		{
			get
			{
				return method.ParameterCount;
			}
		}

		public override MethodAttributes Attributes
		{
			get
			{
				return method.Attributes;
			}
		}

		public override CallingConventions CallingConvention
		{
			get
			{
				return method.CallingConvention;
			}
		}

		public override int __MethodRVA
		{
			get
			{
				return method.__MethodRVA;
			}
		}

		public override Type ReturnType
		{
			get
			{
				return method.ReturnType;
			}
		}

		public override ParameterInfo ReturnParameter
		{
			get
			{
				return new ParameterInfoWrapper(this, method.ReturnParameter);
			}
		}

		internal override bool HasThis
		{
			get
			{
				return method.HasThis;
			}
		}

		public override Module Module
		{
			get
			{
				return method.Module;
			}
		}

		public override Type DeclaringType
		{
			get
			{
				return method.DeclaringType;
			}
		}

		public override Type ReflectedType
		{
			get
			{
				return reflectedType;
			}
		}

		public override string Name
		{
			get
			{
				return method.Name;
			}
		}

		public override bool __IsMissing
		{
			get
			{
				return method.__IsMissing;
			}
		}

		public override bool ContainsGenericParameters
		{
			get
			{
				return method.ContainsGenericParameters;
			}
		}

		public override bool IsGenericMethod
		{
			get
			{
				return method.IsGenericMethod;
			}
		}

		public override bool IsGenericMethodDefinition
		{
			get
			{
				return method.IsGenericMethodDefinition;
			}
		}

		public override int MetadataToken
		{
			get
			{
				return method.MetadataToken;
			}
		}

		internal override bool IsBaked
		{
			get
			{
				return method.IsBaked;
			}
		}

		internal MethodInfoWithReflectedType(Type reflectedType, MethodInfo method)
		{
			this.reflectedType = reflectedType;
			this.method = method;
		}

		public override bool Equals(object obj)
		{
			MethodInfoWithReflectedType methodInfoWithReflectedType = obj as MethodInfoWithReflectedType;
			if (methodInfoWithReflectedType != null && methodInfoWithReflectedType.reflectedType == reflectedType)
			{
				return methodInfoWithReflectedType.method == method;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return reflectedType.GetHashCode() ^ method.GetHashCode();
		}

		public override ParameterInfo[] GetParameters()
		{
			ParameterInfo[] parameters = method.GetParameters();
			for (int i = 0; i < parameters.Length; i++)
			{
				parameters[i] = new ParameterInfoWrapper(this, parameters[i]);
			}
			return parameters;
		}

		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return method.GetMethodImplementationFlags();
		}

		public override MethodBody GetMethodBody()
		{
			return method.GetMethodBody();
		}

		public override MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			return MemberInfo.SetReflectedType(method.MakeGenericMethod(typeArguments), reflectedType);
		}

		public override MethodInfo GetGenericMethodDefinition()
		{
			return method.GetGenericMethodDefinition();
		}

		public override string ToString()
		{
			return method.ToString();
		}

		public override MethodInfo[] __GetMethodImpls()
		{
			return method.__GetMethodImpls();
		}

		internal override Type GetGenericMethodArgument(int index)
		{
			return method.GetGenericMethodArgument(index);
		}

		internal override int GetGenericMethodArgumentCount()
		{
			return method.GetGenericMethodArgumentCount();
		}

		internal override MethodInfo GetMethodOnTypeDefinition()
		{
			return method.GetMethodOnTypeDefinition();
		}

		internal override int ImportTo(ModuleBuilder module)
		{
			return method.ImportTo(module);
		}

		public override MethodBase __GetMethodOnTypeDefinition()
		{
			return method.__GetMethodOnTypeDefinition();
		}

		internal override MethodBase BindTypeParameters(Type type)
		{
			return method.BindTypeParameters(type);
		}

		public override Type[] GetGenericArguments()
		{
			return method.GetGenericArguments();
		}

		internal override int GetCurrentToken()
		{
			return method.GetCurrentToken();
		}
	}
}
