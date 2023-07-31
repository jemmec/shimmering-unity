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
    /// Handles inital connection and streaming of data from a Shimmer3 device
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

        //Inspector & Public members

        [Header("Configuration")]

        [SerializeField]
        [Tooltip("The dev name for this device.")]
        private string devName = "";

        public string DevName
        {
            get => devName;
            set => devName = value;
        }

        [SerializeField]
        [Tooltip("The communication port this device is connected to.")]
        private string comPort = "COM8";

        public string COMPort
        {
            get => comPort;
            set => comPort = value;
        }

        [SerializeField]
        [Tooltip("The sampling rate for the device (default 51.2Hz).")]
        private float samplingRate = 51.2f;
        public float SamplingRate
        {
            get => samplingRate;
            set => samplingRate = value;
        }

        [SerializeField]
        [Tooltip("Select the sensors you want to enable during connection.")]
        private ShimmerConfig.SensorBitmap enabledSensors;

        public ShimmerConfig.SensorBitmap EnabledSensors
        {
            get => enabledSensors;
            set => enabledSensors = value;
        }

        [SerializeField]
        [Tooltip("The range for the accelerometer.")]
        private ShimmerConfig.AccelerometerRange accelerometerRange;

        public ShimmerConfig.AccelerometerRange AccelerometerRange
        {
            get => accelerometerRange;
            set => accelerometerRange = value;
        }

        [SerializeField]
        [Tooltip("The range for the GSR.")]
        private ShimmerConfig.GSRRange gsrRange;

        public ShimmerConfig.GSRRange GSRRange
        {
            get => gsrRange;
            set => gsrRange = value;
        }

        [SerializeField]
        [Tooltip("The range for the gyroscope.")]
        private ShimmerConfig.GyroscopeRange gyroscopeRange;

        public ShimmerConfig.GyroscopeRange GyroscopeRange
        {
            get => gyroscopeRange;
            set => gyroscopeRange = value;
        }

        [SerializeField]
        [Tooltip("The range for the magnetometer.")]
        private ShimmerConfig.MagnetometerRange magnetometerRange;

        public ShimmerConfig.MagnetometerRange MagnetometerRange
        {
            get => magnetometerRange;
            set => magnetometerRange = value;
        }

        [SerializeField]
        private bool enableLowPowerAccel = false;

        public bool EnableLowPowerAccel
        {
            get => enableLowPowerAccel;
            set => enableLowPowerAccel = value;
        }

        [SerializeField]
        private bool enableLowPowerGyro = false;

        public bool EnableLowPowerGyro
        {
            get => enableLowPowerGyro;
            set => enableLowPowerGyro = value;
        }

        [SerializeField]
        private bool enableLowPowerMag = false;

        public bool EnableLowPowerMag
        {
            get => enableLowPowerMag;
            set => enableLowPowerMag = value;
        }

        [SerializeField]
        [Tooltip("Enables the internal ADC pins on the shimmer3.")]
        private bool enableInternalExpPower = true;

        public bool EnableInternalExpPower
        {
            get => enableInternalExpPower;
            set => enableInternalExpPower = value;
        }

        [SerializeField]
        [Tooltip("Data recieved event.")]
        private DataRecievedEvent onDataRecieved = new DataRecievedEvent();

        public DataRecievedEvent OnDataRecieved => onDataRecieved;

        [SerializeField]
        [Tooltip("Device state changed event.")]
        private StateChangeEvent onStateChanged = new StateChangeEvent();

        public StateChangeEvent OnStateChanged => onStateChanged;

        //Private members
        private Queue<ObjectCluster> shimmerDataQueue = new Queue<ObjectCluster>();
        private Queue<State> shimmerStateQueue = new Queue<State>();
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
            shimmer =
                new ShimmerLogAndStreamSystemSerialPort(
                    devName: devName,
                    bComPort: comPort,
                    samplingRate: samplingRate,
                    accelRange: (int)accelerometerRange,
                    gsrRange: (int)gsrRange,
                    gyroRange: (int)gyroscopeRange,
                    magRange: (int)magnetometerRange,
                    setEnabledSensors: (int)enabledSensors,
                    enableLowPowerAccel: enableLowPowerAccel,
                    enableLowPowerGyro: enableLowPowerGyro,
                    enableLowPowerMag: enableLowPowerMag,
                    exg1configuration: Shimmer3Configuration.EXG_EMG_CONFIGURATION_CHIP1,
                    exg2configuration: Shimmer3Configuration.EXG_EMG_CONFIGURATION_CHIP2,
                    internalexppower: enableInternalExpPower
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
                        shimmerStateQueue.Enqueue(CurrentState);
                    }
                    else if (state == (int)ShimmerBluetooth.SHIMMER_STATE_CONNECTING)
                    {
                        Debug.Log("THREAD: Connecting " + connectionCount);
                        CurrentState = State.Connecting;
                        shimmerStateQueue.Enqueue(CurrentState);
                    }
                    else if (state == (int)ShimmerBluetooth.SHIMMER_STATE_NONE)
                    {
                        Debug.Log("THREAD: Disconnected " + connectionCount);
                        //Remove event handler
                        shimmer.UICallback -= HandleEvent;
                        CurrentState = State.Disconnected;
                        shimmerStateQueue.Enqueue(CurrentState);
                    }
                    else if (state == (int)ShimmerBluetooth.SHIMMER_STATE_STREAMING)
                    {
                        Debug.Log("THREAD: Streaming " + connectionCount);
                        CurrentState = State.Streaming;
                        shimmerStateQueue.Enqueue(CurrentState);
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

    [System.Serializable]
    /// <summary>
    /// Custom unity event for listening to state change on the shimmer device
    /// </summary>
    public class StateChangeEvent : UnityEvent<ShimmerDevice, ShimmerDevice.State> { }

}