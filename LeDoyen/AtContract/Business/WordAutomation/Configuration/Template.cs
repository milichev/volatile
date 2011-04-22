using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace LeDoyen.AtContract.Business.WordAutomation.Configuration
{
	[XmlType("template", Namespace = Config.NAMESPACE)]
	[DebuggerDisplay("Template {Name}")]
	public sealed class Template : INamedConfigItem
	{
		internal Config _config;
		internal Type _processType;
		private NamedList<BaseEntity> _entities;

		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAttribute("file")]
		public string FileName { get; set; }

		[XmlArray("entities", IsNullable = false)]
		[XmlArrayItem("text", typeof (TextVariable))]
		[XmlArrayItem("number", typeof (NumberVariable))]
		[XmlArrayItem("dateTime", typeof (DateTimeVariable))]
		[XmlArrayItem("lookup", typeof (LookupValue))]
		public NamedList<BaseEntity> Entities
		{
			get { return _entities ?? (_entities = new NamedList<BaseEntity>()); }
		}
	}
}