using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace LeDoyen.AtContract.Business.WordAutomation.Configuration
{
	public abstract class BaseEntity : INamedConfigItem
	{
		internal Template _template;

		protected BaseEntity()
		{
			Required = true;
		}

		public virtual object[] GetLookupValues()
		{
			return new object[0];
		}

		[XmlIgnore]
		public virtual bool SupportsLookupValues
		{
			get { return false; }
		}

		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAttribute("caption")]
		public string Caption { get; set; }

		[DefaultValue(true)]
		[XmlAttribute("required")]
		public bool Required { get; set; }
	}

	[XmlType("text", Namespace = Config.NAMESPACE)]
	[DebuggerDisplay("TextVariable {Name}")]
	public class TextVariable : BaseEntity
	{
	}

	[XmlType("number", Namespace = Config.NAMESPACE)]
	[DebuggerDisplay("NumberVariable {Name}")]
	public class NumberVariable : BaseEntity
	{
	}

	[XmlType("dateTime", Namespace = Config.NAMESPACE)]
	[DebuggerDisplay("DateTimeVariable {Name}")]
	public class DateTimeVariable : BaseEntity
	{
		public DateTimeVariable()
		{
			Format = "D";
		}

		[XmlAttribute("format")]
		public string Format { get; set; }
	}
}