using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LeDoyen.AtContract.Tests
{
	/// <summary>
	/// Summary description for BusinessTests
	/// </summary>
	[TestClass]
	public class BaseTests
	{
		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext { get; set; }

		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		public string CurrentDirectory
		{
			get { return Path.GetDirectoryName(new Uri(GetType().Assembly.GetName().CodeBase).LocalPath); }
		}
	}
}