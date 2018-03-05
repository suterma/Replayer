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
    ///This is a test class for HtmlCompilationTest and is intended
    ///to contain all HtmlCompilationTest Unit Tests
    ///</summary>
    [TestClass()]
    public class HtmlCompilationTest
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
        ///A test for Encode
        ///</summary>
        [TestMethod()]
        [DeploymentItem("Replayer.Core.dll")]
        public void EncodeTest()
        {
            Guid guid = new Guid(1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 10);
            string expected = @"XAQAAAAIAAwAEBQYHCAkACg";
            string actual;
            actual = HtmlCompilation_Accessor.Encode(guid);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

         /// <summary>
         /// A test for Store
         /// </summary>
        [TestMethod()]
        public void StoreTest_TestsWhetherAnXmlCompilationCanBeStoredAsHtmlCompilation()
        {
            //load the original xml compilation
            ICompilation xmlCompilation = CompilationFactory.Retrieve(@"Spezial-PB Compilation 2010_ModifiedFileNames.xml");

            //convert to html
            HtmlCompilation htmlCompilation = CompilationFactory.Convert<HtmlCompilation>(xmlCompilation);


            //save html compilation
            htmlCompilation.Store();

            Assert.IsTrue(File.Exists(@"Spezial-PB Compilation 2010_ModifiedFileNames.html"));


            //open for visual inspection
            String firefoxPath = @"C:\Documents and Settings\All Users\Desktop\PortableApps\FirefoxPortable\FirefoxPortable.exe";
            Process.Start(firefoxPath, @"file:///" + @"""" + (Path.GetFullPath(@"Spezial-PB Compilation 2010_ModifiedFileNames.html")) + @"""");
        }

        /// <summary>
        ///A test for Store
        ///</summary>
        [TestMethod()]
        public void StoreTest_TestsWhetherAZipCompilationCanBeStoredAsHtmlCompilation()
        {
            //load the original compilation
            var compilation = CompilationFactory.Retrieve(@"Spezial-PB Compilation 2010.zip");

            //convert to html
            var htmlCompilation = CompilationFactory.Convert<HtmlCompilation>(compilation);


            //save html compilation
            htmlCompilation.Store();

            Assert.IsTrue(File.Exists(@"Spezial-PB Compilation 2010.html"));


            //open for visual inspection
            String firefoxPath = @"C:\Documents and Settings\All Users\Desktop\PortableApps\FirefoxPortable\FirefoxPortable.exe";
            Process.Start(firefoxPath, @"file:///" + @"""" + (Path.GetFullPath(@"Spezial-PB Compilation 2010.html")) + @"""");
        }
    }
}
