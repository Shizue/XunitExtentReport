using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Threading;
using UnixSeleniumReport.Core;
using Xunit;
using Xunit.Abstractions;

namespace UnixSeleniumReport
{
    public class PageFiledsTest
    {
        [ThreadStatic]
        private static Report report;

        [ThreadStatic]
        private static GetScreenShot getScreenShot;

        [ThreadStatic]
        IWebDriver driver = new ChromeDriver();

        public PageFiledsTest()
        {
            report = new Report(driver);
            getScreenShot = new GetScreenShot(driver);
        }

        public static ExtentReports Extent { get => report.extent; set => report.extent = value; }

        [Fact]
        public void First_test_case()
        {
            try
            {
                report.Init();
                driver.Navigate().GoToUrl("https://www.google.com/");
                Thread.Sleep(2500);
                getScreenShot.Capture("testContext");
                Assert.True(true, "Passed");
                report.GetResult(Status.Pass, report.htmlReporter.Config.ReportName, getScreenShot.dirPath.ToString());
                //Assert.False(true, "Failed");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                report.Endreport();
            }
        }
    }
}
