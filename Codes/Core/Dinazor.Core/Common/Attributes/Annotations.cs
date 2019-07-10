using System;

namespace Dinazor.Core.Common.Attributes
{
    public class Annotations
    {
        [System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property | AttributeTargets.Delegate | AttributeTargets.Field)]
        internal sealed class NotNullAttribute : System.Attribute
        {
        }

        [System.AttributeUsage(
    AttributeTargets.Method | AttributeTargets.Parameter |
    AttributeTargets.Property | AttributeTargets.Delegate |
    AttributeTargets.Field)]
        internal sealed class CanBeNullAttribute : System.Attribute
        {
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        internal sealed class InvokerParameterNameAttribute : System.Attribute
        {
        }

        [AttributeUsage(AttributeTargets.Parameter)]
        internal sealed class NoEnumerationAttribute : System.Attribute
        {
        }

        [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
        internal sealed class ContractAnnotationAttribute : System.Attribute
        {
            public string Contract { get; private set; }

            public bool ForceFullStates { get; private set; }

            public ContractAnnotationAttribute([NotNull] string contract)
                : this(contract, false)
            {
            }

            public ContractAnnotationAttribute([NotNull] string contract, bool forceFullStates)
            {
                Contract = contract;
                ForceFullStates = forceFullStates;
            }
        }

        [AttributeUsage(AttributeTargets.All)]
        internal sealed class UsedImplicitlyAttribute : System.Attribute
        {
            public UsedImplicitlyAttribute()
                : this(ImplicitUseKindFlags.Default, ImplicitUseTargetFlags.Default)
            {
            }

            public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags)
                : this(useKindFlags, ImplicitUseTargetFlags.Default)
            {
            }

            public UsedImplicitlyAttribute(ImplicitUseTargetFlags targetFlags)
                : this(ImplicitUseKindFlags.Default, targetFlags)
            {
            }

            public UsedImplicitlyAttribute(
                ImplicitUseKindFlags useKindFlags, ImplicitUseTargetFlags targetFlags)
            {
                UseKindFlags = useKindFlags;
                TargetFlags = targetFlags;
            }

            public ImplicitUseKindFlags UseKindFlags { get; private set; }
            public ImplicitUseTargetFlags TargetFlags { get; private set; }
        }

        [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Delegate)]
        internal sealed class StringFormatMethodAttribute : System.Attribute
        {
            public StringFormatMethodAttribute([NotNull] string formatParameterName)
            {
                FormatParameterName = formatParameterName;
            }

            [NotNull]
            public string FormatParameterName { get; private set; }
        }

        [Flags]
        internal enum ImplicitUseKindFlags
        {
            Default = Access | Assign | InstantiatedWithFixedConstructorSignature,
            Access = 1,
            Assign = 2,
            InstantiatedWithFixedConstructorSignature = 4,
            InstantiatedNoFixedConstructorSignature = 8
        }

        [Flags]
        internal enum ImplicitUseTargetFlags
        {
            Default = Itself,
            Itself = 1,
            Members = 2,
            WithMembers = Itself | Members
        }
    }
}
