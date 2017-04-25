using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ZSZ.Service;

namespace ZSZ.UnitTest.ZSZ.Service
{
    /// <summary>
    /// DatabaseTest 的摘要说明
    /// </summary>
    [TestClass]
    public class DatabaseTest
    {
        [TestMethod]
        public void TestCreateDatabase()
        {
            ZSZDbContext ctx = new ZSZDbContext();
            ctx.Database.Delete();
            ctx.Database.Create();
        }
    }
}
