using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using ZSZ.Common;
using System.Threading;

namespace ZSZ.UnitTest.ZSZ.Common
{
    /// <summary>
    /// CommonTest 的摘要说明
    /// </summary>
    [TestClass]
    public class CommonTest
    {
        [TestMethod]
        public void TestGenerateCaptchaCode()
        {
            string data = CommonHelper.GenerateCaptchaCode(6);
            Assert.AreEqual(data.Length, 6);
        }

        [TestMethod]
        public void TestSendMail()
        {
            MailHelper.SendMailBy163("测试", "万法唯心", "9111377@qq.com");
        }

        [TestMethod]
        public void TestMD5()
        {
            string result = "eeee".CalcMD5();
            Assert.AreEqual(result.Length % 2, 0);
        }

        [TestMethod]
        public void TestThumb()
        {
            string sourceFileName = @"d:\Chrysanthemum.jpg";
            string targetFileName = @"d:\Chrysanthemum20.jpg";
            ImageHelper.BuildThumbnail(sourceFileName, targetFileName);
            Assert.AreEqual(File.Exists(targetFileName), true);
        }

        [TestMethod]
        public void TestWatermark()
        {
            string waterFilerName = @"d:\water.png";
            string sourceFileName = @"d:\Chrysanthemum.jpg";
            string targetFileName = @"d:\Chrysanthemumwater.jpg";
            ImageHelper.BuiildWaterMark(waterFilerName, sourceFileName, targetFileName);
            Assert.AreEqual(File.Exists(targetFileName), true);
        }

        [TestMethod]
        public void TestCaptcha()
        {
            string filename = @"d:\captcha.jpg";

            File.Delete(filename);

            string code = CommonHelper.GenerateCaptchaCode(6);
            ImageHelper.BuildCaptchaPicture(code, filename);

            Assert.AreEqual(File.Exists(filename), true);
        }

        [TestMethod]
        public void TestLogHelper()
        {
            LogHelper.Info("日志有信息了xxx。");
        }

        [TestMethod]
        public void TestTimer()
        {
            TimerHelper.ExecuteWithInterval(1, () =>
            {
                LogHelper.Info(DateTime.Now.ToLongTimeString());
            });

            Thread.Sleep(100000);
        }
    }
}
