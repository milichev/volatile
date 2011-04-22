using System;
using System.IO;
using LeDoyen.AtContract.Business.WordAutomation.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeDoyen.AtContract.Tests
{
	/// <summary>
	///This is a test class for ConfigTest and is intended
	///to contain all ConfigTest Unit Tests
	///</summary>
	[TestClass]
	public class ConfigTest : BaseTests
	{
		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion

		/// <summary>
		///A test for Load
		///</summary>
		[TestMethod]
		public void LoadTest()
		{
			Config config = Config.Load(ConfigPath);
			Assert.IsInstanceOfType(config, typeof (Config));
			Assert.AreEqual(1, config.Templates.Count);
			var tpl = config.Templates[0];
			var contrName = config.Templates[0].Entities.Find<LookupValue>("ContractorFullName");
			Assert.AreEqual(1, contrName.GetLookupValues()[0]);
		}

		[TestMethod]
		public void GetProcess()
		{
			Config config = Config.Load(ConfigPath);
			config.Processes["Agreement"].CreateProcess().Work();
		}

		private static string ConfigPath { get { return Path.Combine(Environment.CurrentDirectory, "wordconfig.xml"); } }
	}

	public class TestLookupProvider : ILookupValueProvider
	{
		public object[] GetValues(LookupValue entity)
		{
			return new object[] {1};
		}
	}
}