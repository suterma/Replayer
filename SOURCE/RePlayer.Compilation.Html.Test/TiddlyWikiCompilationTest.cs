using RePlayer.Compilation.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Diagnostics;
using RePlayer.Core;
using RePlayer.Core.v03.Annotation;

namespace RePlayer.Compilation.Html.Test
{
    
    
    /// <summary>
    ///This is a test class for TiddlyWikiCompilationTest and is intended
    ///to contain all TiddlyWikiCompilationTest Unit Tests
    ///</summary>
    [TestClass()]
    public class TiddlyWikiCompilationTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

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
        ///Integration test for a TiddlyWiki compilation
        ///</summary>
        [TestMethod()]
        public void IntegrationTestThat_LoadsAZipCompilationAndConvertsItIntoATiddlyWikiCompilation()
        {
            //load the original compilation
            ICompilation compilation = CompilationFactory.Retrieve(@"Spezial-PB Compilation 2010.zip");

            //convert to target type
            var targetCompilation = CompilationFactory.Convert<TiddlyWikiCompilation>(compilation);

            //save target compilation
            targetCompilation.Store();

            Assert.IsTrue(File.Exists(@"Spezial-PB Compilation 2010.html"));

            //open for visual inspection
            String firefoxPath = @"C:\Documents and Settings\All Users\Desktop\PortableApps\FirefoxPortable\FirefoxPortable.exe";
            Process.Start(firefoxPath, @"file:///" + @"""" + (Path.GetFullPath(@"Spezial-PB Compilation 2010.html")) + @"""");
        }
    }
}
