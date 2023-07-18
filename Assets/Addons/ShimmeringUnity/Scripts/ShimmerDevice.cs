using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShimmerAPI;
using System;
using System.Threading;
using UnityEngine.Events;

namespace ShimmeringUnity
{

    /// <summary>
    /// Handles connection and streaming of data from a Shimmer device
    /// </summary>
    public class ShimmerDevice : MonoBehaviour
    {

        /// <summary>
        /// The possible states of the shimmer device
        /// </summary>
        public enum State
        {
            /// <summary>
            /// No state, device not connected.
            /// </summary>
            None,
            /// <summary>
            /// Device is currently connecting via bluetooth.
            /// </summary>
            Connecting,
            /// <summary>
            /// Device has sucessfully connected.
            /// </summary>
            Connected,
            /// <summary>
            /// Device has been disconnected.
            /// </summary>
            Disconnected,
            /// <summary>
            /// Device is currently streaming (and connected).
            /// </summary>
            Streaming
        }

        /// <summary>
        /// The current state of this shimmer device
        /// </summary>
        /// <value></value>
        public State CurrentState { get; private set; }

        [Header("Configuration")]

        [SerializeField]
        [Tooltip("The dev name for this device.")]
        private string shimmerDeviceID = "";

        [SerializeField]
        [Tooltip("The communication port this device is connected to.")]
        private string comPort = "COM8";

        [SerializeField]
        [Tooltip("The sampling rate for the device (default 51.2Hz).")]
        private float samplingRate = 51.2f;

        [SerializeField]
        [Tooltip("Select the sensors you want to enable during connection.")]
        private ShimmerConfig.SensorBitmap enabledSensors;

        [SerializeField]
        [Tooltip("The range for the accelerometer.")]
        private ShimmerConfig.AccelerometerRange accelerometerRange;

        [SerializeField]
        [Tooltip("The range for the GSR.")]
        private ShimmerConfig.GSRRange gsrRange;

        [SerializeField]
        [Tooltip("The range for the gyroscope.")]
        private ShimmerConfig.GyroscopeRange gyroscopeRange;

        [SerializeField]
        [Tooltip("The range for the magnetometer.")]
        private ShimmerConfig.MagnetometerRange magnetometerRange;

        [SerializeField]
        [Tooltip("Enables the internal ADC pins on the shimmer3.")]
        private bool useInternalExpPower = true;

        [SerializeField]
        [Tooltip("Data recieved event.")]
        private DataRecievedEvent onDataRecieved = new DataRecievedEvent();

        public DataRecievedEvent OnDataRecieved => onDataRecieved;

        //Private members

        private Queue<ObjectCluster> shimmerDataQueue = new Queue<ObjectCluster>();
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

        /// <summary>
        /// Trys to connect to a Shimmer3 bluetooth device given the configuration
        /// </summary>
        public void Connect()
        {
            //Prevent from connecting again
            if (connectBlocker) return;
            if ((CurrentState == State.None ||
                CurrentState == State.Disconnected))
            {
                shimmerThread = new Thread(ConnectionThread);
                shimmerThread.Start();
            }
        }

        /// <summary>
        /// Trys to disconnected from the currently connected device
        /// </summary>
        public void Disconnect()
        {
            if (CurrentState == State.Connected ||
            CurrentState == State.Connecting ||
            CurrentState == State.Streaming)
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

        /// <summary>
        /// Starts the streaming on the connected device
        /// </summary>
        public void StartStreaming()
        {
            if (CurrentState == State.Connected)
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

        /// <summary>
        /// Stops the streaming on the connected device
        /// </summary>
        public void StopStreaming()
        {
            if (CurrentState == State.Streaming)
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

        /// <summary>
        /// Forces the connection thread to be aborted
        /// </summary>
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
            byte[] defaultECGReg1 = ShimmerBluetooth.SHIMMER3_DEFAULT_TEST_REG1; //also see ShimmerBluetooth.SHIMMER3_DEFAULT_ECG_REG1
            byte[] defaultECGReg2 = ShimmerBluetooth.SHIMMER3_DEFAULT_TEST_REG2; //also see ShimmerBluetooth.SHIMMER3_DEFAULT_ECG_REG2
            shimmer =
                new ShimmerLogAndStreamSystemSerialPort(
                    devName: shimmerDeviceID,
                    bComPort: comPort,
                    samplingRate: samplingRate,
                    accelRange: (int)accelerometerRange,
                    gsrRange: (int)gsrRange,
                    gyroRange: (int)gyroscopeRange,
                    magRange: (int)magnetometerRange,
                    setEnabledSensors: (int)enabledSensors,
                    enableLowPowerAccel: false,
                    enableLowPowerGyro: false,
                    enableLowPowerMag: false,
                    exg1configuration: defaultECGReg1,
                    exg2configuration: defaultECGReg2,
                    internalexppower: useInternalExpPower
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
                        CurrentState = State.Connected;
                    }
                    else if (state == (int)ShimmerBluetooth.SHIMMER_STATE_CONNECTING)
                    {
                        Debug.Log("THREAD: Connecting " + connectionCount);
                        CurrentState = State.Connecting;
                    }
                    else if (state == (int)ShimmerBluetooth.SHIMMER_STATE_NONE)
                    {
                        Debug.Log("THREAD: Disconnected " + connectionCount);
                        //Remove event handler
                        shimmer.UICallback -= HandleEvent;
                        CurrentState = State.Disconnected;
                    }
                    else if (state == (int)ShimmerBluetooth.SHIMMER_STATE_STREAMING)
                    {
                        Debug.Log("THREAD: Streaming " + connectionCount);
                        CurrentState = State.Streaming;
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
    /// <summary>
    /// Custom unity event for capturing data from the shimmer device
    /// </summary>
    public class DataRecievedEvent : UnityEvent<ShimmerDevice, ObjectCluster> { }

}