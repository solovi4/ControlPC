using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommandExecutor.Tests
{
    [TestClass]
    public class FunctionTests
    {
        [TestMethod]
        public void VolumeIncreaseTest()
        {
            CommandExecutorService commandExecutorService = new CommandExecutorService();
            var volumeBefore = commandExecutorService.GetVolumeLevel();
            commandExecutorService.VolumeIncrease();
            Assert.AreEqual(volumeBefore + 10, commandExecutorService.GetVolumeLevel());

        }

        [TestMethod]
        public void VolumeDecreaseTest()
        {
            CommandExecutorService commandExecutorService = new CommandExecutorService();
            var volumeBefore = commandExecutorService.GetVolumeLevel();
            commandExecutorService.VolumeDecrease();
            Assert.AreEqual(volumeBefore - 10, commandExecutorService.GetVolumeLevel());
        }

        [TestMethod]
        public void MuteTest()
        {
            var commandExecutor = new CommandExecutorService();
            var isMuted = commandExecutor.IsMuted();
            commandExecutor.Mute();
            Assert.AreNotEqual(isMuted, commandExecutor.IsMuted());
        }

        [TestMethod]
        public void MoveCursor()
        {
            var commandExecutor = new CommandExecutorService();
            commandExecutor.MoveCursor(100, 100);
        }
    }
}
