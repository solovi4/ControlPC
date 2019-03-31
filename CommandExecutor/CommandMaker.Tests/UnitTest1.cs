using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommandMaker.Tests
{
    [TestClass]
    public class CommandMakerTests
    {
        [TestMethod]
        public void VolumeUpTest()
        {
            Program.Main(new string[] { "VolumeIncrease" });
        }
    }
}
