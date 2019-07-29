using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Picross_Solver;

namespace PicrossTest
{
    [TestClass]
    public class PicrossTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Picross p = new Picross(10, 10);

            var result = p.Test(false);

            Assert.IsTrue(result);


        }
    }
}
