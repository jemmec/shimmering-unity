using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShimmerAPI;
using System;
using System.Threading;
using UnityEngine.Events;

namespace ShimmeringUnity
{
    public enum ShimmerUnityState
    {
        None,
        Connecting,
        Connected,
        Disconnected,
        Streaming
    }

    public class ShimmerDevice : MonoBehaviour
    {

        private Queue<ObjectCluster> shimmerDataQueue = new Queue<ObjectCluster>();
        public ShimmerUnityState CurrentState { get; private set; }

        [Header("Connection Info")]
        [SerializeField]
        private string shimmerDeviceID = "";

        [SerializeField]
        private string comPort = "COM8";

        [SerializeField]
        private float samplingRate = 51.2f;

        [SerializeField]
        private DataRecievedEvent onDataRecieved = new DataRecievedEvent();
        public DataRecievedEvent OnDataRecieved => onDataRecieved;

        //Private members
        private Thread shimmerThread = null;

        private ShimmerBluetooth shimmer;

        private int connectionCount;

        //The connect blocker prevents multiple threads from spawning
        private bool connectBlocker = false;

        private int waitBufferMilliseconds = 250;

        private void Update()
        {
            //Dequeue loop
            if (shimmerDataQueue.Count > 0)
            {
                ObjectCluster objectCluster = shimmerDataQueue.Dequeue();
                OnDataRecieved.Invoke(this, objectCluster);
            }
        }

        private void OnApplicationQuit()
        {
            Disconnect();
        }

        public void Connect()
        {
            //Prevent from connecting again
            if (connectBlocker) return;
            if ((CurrentState == ShimmerUnityState.None ||
                CurrentState == ShimmerUnityState.Disconnected))
            {
                shimmerThread = new Thread(ConnectionThread);
                shimmerThread.Start();
            }
        }

        public void Disconnect()
        {
            if (CurrentState == ShimmerUnityState.Connected ||
            CurrentState == ShimmerUnityState.Connecting ||
            CurrentState == ShimmerUnityState.Streaming)
            {
                if (shimmer != null)
                {
                    //Must run in new thread
                    new Thread(() =>
                    {
                        Thread.Sleep(waitBufferMilliseconds);
                        shimmer.Disconnect();
                    }).Start();
                }
            }
        }

        public void StartStreaming()
        {
            if (CurrentState == ShimmerUnityState.Connected)
            {
                if (shimmer != null)
                {
                    new Thread(() =>
                    {
                        Thread.Sleep(waitBufferMilliseconds);
                        shimmer.StartStreaming();
                    }).Start();
                }
            }
        }

        public void StopStreaming()
        {
            if (CurrentState == ShimmerUnityState.Streaming)
            {
                if (shimmer != null)
                {
                    new Thread(() =>
                    {
                        Thread.Sleep(waitBufferMilliseconds);
                        shimmer.StopStreaming();
                    }).Start();
                }
            }
        }

        public void ForceAbortThread()
        {
            if (shimmerThread != null)
            {
                shimmerThread.Abort();
            }
        }

        //The following region runs on a seperate thread to unity, you can not
        //call ANYTHING UnityEngine related apart from Debug.Log()
        #region Shimmer Thread

        private void ConnectionThread()
        {
            Debug.Log("THREAD: Starting shimmer device connection thread...");
            int enabledSensors = ((int)ShimmerBluetooth.SensorBitmapShimmer3.SENSOR_A_ACCEL | (int)ShimmerBluetooth.SensorBitmapShimmer3.SENSOR_D_ACCEL);
            byte[] defaultECGReg1 = ShimmerBluetooth.SHIMMER3_DEFAULT_TEST_REG1; //also see ShimmerBluetooth.SHIMMER3_DEFAULT_ECG_REG1
            byte[] defaultECGReg2 = ShimmerBluetooth.SHIMMER3_DEFAULT_TEST_REG2; //also see ShimmerBluetooth.SHIMMER3_DEFAULT_ECG_REG2
            shimmer =
                new ShimmerLogAndStreamSystemSerialPort(
                    shimmerDeviceID,
                    comPort,
                    samplingRate,
                    0,
                    4,
                    enabledSensors,
                    false,
                    false,
                    false,
                    0,
                    0,
                    defaultECGReg1,
                    defaultECGReg2,
                    false
                );
            shimmer.UICallback += HandleEvent;
            shimmer.Connect();
        }

        private void HandleEvent(object sender, EventArgs args)
        {
            CustomEventArgs eventArgs = (CustomEventArgs)args;
            int indicator = eventArgs.getIndicator();

            switch (indicator)
            {
                //Occurs whenever the device's state has changed
                case (int)ShimmerBluetooth.ShimmerIdentifier.MSG_IDENTIFIER_STATE_CHANGE:
                    Debug.Log("THREAD: " + ((ShimmerBluetooth)sender).GetDeviceName() + " State = " + ((ShimmerBluetooth)sender).GetStateString() + System.Environment.NewLine);
                    int state = (int)eventArgs.getObject();
                    if (state == (int)ShimmerBluetooth.SHIMMER_STATE_CONNECTED)
                    {
                        Debug.Log("THREAD: Connected " + connectionCount);
                        CurrentState = ShimmerUnityState.Connected;

                    }
                    else if (state == (int)ShimmerBluetooth.SHIMMER_STATE_CONNECTING)
                    {
                        Debug.Log("THREAD: Connecting " + connectionCount);
                        CurrentState = ShimmerUnityState.Connecting;
                    }
                    else if (state == (int)ShimmerBluetooth.SHIMMER_STATE_NONE)
                    {
                        Debug.Log("THREAD: Disconnected " + connectionCount);
                        //Remove event handler
                        shimmer.UICallback -= HandleEvent;
                        CurrentState = ShimmerUnityState.Disconnected;
                    }
                    else if (state == (int)ShimmerBluetooth.SHIMMER_STATE_STREAMING)
                    {
                        Debug.Log("THREAD: Streaming " + connectionCount);
                        CurrentState = ShimmerUnityState.Streaming;
                    }
                    break;
                case (int)ShimmerBluetooth.ShimmerIdentifier.MSG_IDENTIFIER_NOTIFICATION_MESSAGE:
                    break;
                case (int)ShimmerBluetooth.ShimmerIdentifier.MSG_IDENTIFIER_DATA_PACKET:
                    ObjectCluster objectCluster = (ObjectCluster)eventArgs.getObject();
                    shimmerDataQueue.Enqueue(objectCluster);
                    break;
            }
        }

        #endregion

    }

    [System.Serializable]
    public class DataRecievedEvent : UnityEvent<ShimmerDevice, ObjectCluster> { }

}