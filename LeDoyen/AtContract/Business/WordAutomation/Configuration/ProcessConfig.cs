using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Xml.Serialization;

namespace LeDoyen.AtContract.Business.WordAutomation.Configuration
{
	[XmlType("process", Namespace = Config.NAMESPACE)]
	[DebuggerDisplay("ProcessConfig {Name}")]
	public sealed class ProcessConfig : INamedConfigItem
	{
		internal Config _config;
		internal Type _processType;

		public WordProcess CreateProcess()
		{
			var process = Activator.CreateInstance(ProcessType) as WordProcess;
			if (process == null)
			{
				throw new ApplicationException(string.Format("Wrong WordProcess type: {0}", ProcessType));
			}
			process._config = _config;
			process.Initialize();
//			process.Templates = Templates.Split(',', ';').Select(tn => tn.Trim()).Where(tn => tn.Length > 0)
//				.Select(tn => _config.Templates.First(t => string.Equals(t.Name, tn, StringComparison.InvariantCultureIgnoreCase))).ToArray();

			return process;
		}

		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAttribute("templates")]
		public string Templates { get; set; }

		[XmlAttribute("type")]
		public string ProcessTypeName { get; set; }

		[XmlIgnore]
		public Type ProcessType
		{
			get
			{
				return _processType ?? (_processType = (Type) new TypeNameConverter().ConvertFrom(
					null, CultureInfo.InvariantCulture, ProcessTypeName));
			}
		}
	}
}