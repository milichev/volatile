using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace LeDoyen.AtContract.Business.WordAutomation.Configuration
{
	[XmlRoot("configuration", Namespace = NAMESPACE)]
	public sealed class Config
	{
		public const string NAMESPACE = "http://ledoyen/contractlink/";

		private NamedList<ProcessConfig> _processes;
		private NamedList<Template> _templates;

		public static Config Load(string path)
		{
			Uri uri = new Uri(path);
			XmlSerializer xs = new XmlSerializer(typeof (Config), NAMESPACE);
			using (XmlReader xr = new XmlTextReader(uri.ToString()))
			{
				Config config = (Config) xs.Deserialize(xr);
				config.OnDeserialized();
				return config;
			}
		}

		private void OnDeserialized()
		{
			Templates.ForEach(
				t =>
					{
						t._config = this;
						t.Entities.ForEach(
							e => { e._template = t; });
					});
			Processes.ForEach(p => { p._config = this; });
		}

		[XmlArray("templates", IsNullable = false)]
		public NamedList<Template> Templates
		{
			get { return _templates ?? (_templates = new NamedList<Template>()); }
		}

		[XmlArray("processes", IsNullable = false)]
		public NamedList<ProcessConfig> Processes
		{
			get { return _processes ?? (_processes = new NamedList<ProcessConfig>()); }
		}
	}

	public interface INamedConfigItem
	{
		string Name { get; set; }
	}

	public sealed class NamedList<TItem> : List<TItem>
		where TItem : class, INamedConfigItem
	{
		public T Find<T>(string name)
			where T : class, TItem
		{
			TItem entity = this[name];
			if (entity == null)
			{
				return null;
			}
			T t = entity as T;
			if (t == null)
			{
				throw new ArgumentException(
					string.Format("Wrong template entity type: {0}, expected {1}", entity.GetType(), typeof (T)));
			}
			return t;
		}

		public TItem this[string name]
		{
			get { return this.FirstOrDefault(p => String.Equals(p.Name, name, StringComparison.InvariantCultureIgnoreCase)); }
		}
	}
}