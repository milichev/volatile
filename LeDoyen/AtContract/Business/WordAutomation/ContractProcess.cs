using System;
using System.Globalization;
using System.IO;
using LeDoyen.AtContract.Business.WordAutomation.Configuration;

namespace LeDoyen.AtContract.Business.WordAutomation
{
	public class ContractProcess : DocumentComposeProcess
	{
		internal override void Initialize()
		{
			base.Initialize();

			TemplateConfigs = new[]
			                  	{
			                  		_config.Templates.Find<Template>("Agreement")
			                  	};
		}

		protected override void DoWork()
		{
			TemplatePath = TemplateConfigs[0].FileName;

			InitializeApplication();
			CreateDocument();
			UpdateDocVars();
			Document.Fields.Update();
//			FileName = GetUniqueFileName(Path.Combine(OutputDirectory, "Agreement.docx"));
//			SaveDocument(FileName);
			Document.Activate();
		}

		private void CreateDocument()
		{
			object templatePath = TemplatePath;
			object docType = 0;
			Document = Application.Documents.Add(ref templatePath, ref _false, ref docType);
			Document.Select();
		}

		private void UpdateDocVars()
		{
			object num = 123;
			Document.Variables.Add("AgreementNumber", ref num);
			DateTime date = new DateTime(2009, 4, 13, 16, 32, 12);
			object dateObj = date.ToString("D", CultureInfo.CurrentCulture);
			Document.Variables.Add("AgreementDate", ref dateObj);
		}

		public string TemplatePath { get; private set; }
		public string OutputDirectory { get; private set; }
		public string FileName { get; private set; }
	}
}