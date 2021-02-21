using AventStack.ExtentReports;
using AventStack.ExtentReports.MarkupUtils;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UnixSeleniumReport.Core
{
    class Report
    {
        public ExtentReports extent { get; set; }
        public ExtentTest test { get; set; }
        public IWebDriver driver { get; set; }
        public ExtentHtmlReporter htmlReporter { get; set; }
        public Report(IWebDriver driver) { this.driver = driver; }
        public Report(ExtentReports extent, ExtentTest test, IWebDriver driver, ExtentHtmlReporter htmlReporter)
        {
            this.extent = extent;
            this.test = test;
            this.driver = driver;
            this.htmlReporter = htmlReporter;
        }

        public void Init()
        {
            string reportPath = new StringBuilder().Append(Directory.GetParent(@"../").FullName)
                                               .Append(Path.DirectorySeparatorChar).Append("Result")
                                               .Append(Path.DirectorySeparatorChar).Append("Result_")
                                               .Append(DateTime.Now.ToString("ddMMyyyy_HHmmss"))
                                               .Append(Path.DirectorySeparatorChar).ToString();
            htmlReporter = new ExtentHtmlReporter(reportPath);
            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Host Name", "SoftwareTestingMaterial");
            extent.AddSystemInfo("Environment", "Production");
            extent.AddSystemInfo("User Name", "Flávia Shizue");
            htmlReporter.Config.DocumentTitle = "Title of the Report Comes here ";
            htmlReporter.Config.ReportName = "Name of the Report Comes here ";
            htmlReporter.Config.Theme = Theme.Dark;
            test = extent.CreateTest("First one").Info("Test Started");
        }

        public void GetResult(Status status, string name, string dirPathScreenPath)
        {
            if (status == Status.Fail)
            {
                test.Log(Status.Fail, MarkupHelper.CreateLabel(name + " - Test Case Failed", ExtentColor.Red));
                test.Log(Status.Fail, MarkupHelper.CreateLabel(name + " - Test Case Failed", ExtentColor.Red));
                test.Fail("Test Case Failed Snapshot is below " + test.AddScreenCaptureFromPath(dirPathScreenPath));
            }
            else if (status == Status.Skip)
            {
                test.AddScreenCaptureFromPath(dirPathScreenPath);
                test.Log(Status.Skip, MarkupHelper.CreateLabel(name + " - Test Case Skipped", ExtentColor.Orange));
            }
            else if (status == Status.Pass)
            {
                test.AddScreenCaptureFromPath(dirPathScreenPath);
                test.Log(Status.Pass, MarkupHelper.CreateLabel(name + " Test Case PASSED", ExtentColor.Green));
            }

        }

        public void Endreport()
        {
            driver.Quit();
            extent.Flush();
        }
    }
}
