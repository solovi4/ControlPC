using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CommandExecutor
{
    [ServiceContract]
    public interface ICommandExecutorService
    {
        [OperationContract]
        void VolumeIncrease();

        [OperationContract]
        void VolumeDecrease();

        [OperationContract]
        int GetVolumeLevel();

        [OperationContract]
        void SetVolume(int level);

        [OperationContract]
        void Mute();

        [OperationContract]
        void ShutDown();

        [OperationContract]
        void CancelShutDown();

        [OperationContract]
        void MoveCursor(int dx, int dy);

        [OperationContract]
        void MouseLeftButtonDown();

        [OperationContract]
        void MouseLeftButtonUp();

        [OperationContract]
        void MouseRightButtonDown();

        [OperationContract]
        void MouseRightButtonUp();

        [OperationContract]
        void SendText(string text);

    }
}
