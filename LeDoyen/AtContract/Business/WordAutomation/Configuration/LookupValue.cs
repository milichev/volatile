using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Xml.Serialization;

namespace LeDoyen.AtContract.Business.WordAutomation.Configuration
{
	public interface ILookupValueProvider
	{
		object[] GetValues(LookupValue entity);
	}

	[XmlType("lookup", Namespace = Config.NAMESPACE)]
	[DebuggerDisplay("LookupValue {Name}")]
	public class LookupValue : BaseEntity
	{
		private Type _providerType;

		public override object[] GetLookupValues()
		{
			var provider = Activator.CreateInstance(ProviderType) as ILookupValueProvider;
			if (provider == null)
			{
				throw new ApplicationException(string.Format("Wrong configuration type: {0}", ProviderType));
			}
			return provider.GetValues(this);
		}

		[XmlAttribute("provider")]
		public string ProviderTypeName { get; set; }

		[XmlIgnore]
		public Type ProviderType
		{
			get
			{
				return _providerType ?? (_providerType = (Type) new TypeNameConverter().ConvertFrom(
					null, CultureInfo.InvariantCulture, ProviderTypeName));
			}
		}

		public override bool SupportsLookupValues
		{
			get { return true; }
		}
	}
}