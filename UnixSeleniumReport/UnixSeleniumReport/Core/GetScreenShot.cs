using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace UnixSeleniumReport.Core
{
    class GetScreenShot
    {
        public StringBuilder dirPath { get; set; }
        public IWebDriver driver { get; set; }
        // public StringBuilder dirPath = new StringBuilder(Directory.GetParent(@"../").FullName + Path.DirectorySeparatorChar);
        public GetScreenShot(IWebDriver driver)
        {
            dirPath = new StringBuilder(Directory.GetParent(@"../").FullName + Path.DirectorySeparatorChar);
            this.driver = driver;
        }

        public void Capture(string screenShotName)
        {
            try
            {
                Thread.Sleep(4000);
                ITakesScreenshot ts = (ITakesScreenshot)driver;
                Screenshot screenshot = ts.GetScreenshot();
                DirectoryInfo di = Directory.CreateDirectory(new StringBuilder().Append(Directory.GetParent(@"../").FullName).Append(Path.DirectorySeparatorChar).Append("Defect_Screenshots").Append(Path.DirectorySeparatorChar).ToString());
                dirPath.Append("Defect_Screenshots"+ Path.DirectorySeparatorChar).Append(screenShotName).Append("_").Append(DateTime.Now.ToString("ddMMyyyy_HHmmss")).Append(".png");
                screenshot.SaveAsFile(new Uri(dirPath.ToString()).LocalPath);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
