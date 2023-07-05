using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShimmerAPI;
using System;
using System.Threading;

public enum ShimmerUnityState
{
    None,
    Connecting,
    Connected,
    Disconnected,
    Streaming
}

public class ShimmerConnectionTest : MonoBehaviour
{

    private Queue<int> shimmerEventQueue = new Queue<int>();

    [SerializeField]
    [Tooltip("The current state indication of the shimmer device.")]
    private ShimmerUnityState currentState;

    [Header("Connection Info")]
    [SerializeField]
    private string shimmerID = "";

    [SerializeField]
    private string comPort = "COM8";

    [SerializeField]
    private float samplingRate = 51.2f;

    [Header("Test")]

    [SerializeField]
    private bool tryConnect = false;

    private Thread shimmerThread = null;

    private ShimmerBluetooth shimmer;

    private int connectionCount;


    private void Update()
    {
        if (tryConnect || Input.GetKeyDown(KeyCode.Space))
        {
            tryConnect = false;
            if (shimmerThread == null)
            {
                Debug.Log("Creating new thread");
                shimmerThread = new Thread(Connect);
                shimmerThread.Start();
            }
        }

        if (shimmerEventQueue.Count > 0)
        {
            int indicator = shimmerEventQueue.Dequeue();
            Debug.Log("MAIN: Shimmer Event " + indicator);
        }
    }

    private void Connect()
    {
        Debug.Log("THREAD: Starting Shimmer Connection...");
        int enabledSensors = ((int)ShimmerBluetooth.SensorBitmapShimmer3.SENSOR_A_ACCEL | (int)ShimmerBluetooth.SensorBitmapShimmer3.SENSOR_D_ACCEL);
        byte[] defaultECGReg1 = ShimmerBluetooth.SHIMMER3_DEFAULT_TEST_REG1; //also see ShimmerBluetooth.SHIMMER3_DEFAULT_ECG_REG1
        byte[] defaultECGReg2 = ShimmerBluetooth.SHIMMER3_DEFAULT_TEST_REG2; //also see ShimmerBluetooth.SHIMMER3_DEFAULT_ECG_REG2
        shimmer =
            new ShimmerLogAndStreamSystemSerialPort(
                shimmerID,
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
        Debug.Log("THREAD: End of shimmer connection...");
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
                    currentState = ShimmerUnityState.Connected;
                    //Needs to be executed on a seperate thread, and a sleep to ensure everything on the current thread is completed
                    new Thread(() =>
                    {
                        Thread.Sleep(500);
                        shimmer.StartStreaming();
                    }).Start();

                }
                else if (state == (int)ShimmerBluetooth.SHIMMER_STATE_CONNECTING)
                {
                    Debug.Log("THREAD: Connecting " + connectionCount);
                    currentState = ShimmerUnityState.Connecting;
                }
                else if (state == (int)ShimmerBluetooth.SHIMMER_STATE_NONE)
                {
                    Debug.Log("THREAD: Disconnected " + connectionCount);
                    currentState = ShimmerUnityState.Disconnected;
                    new Thread(() =>
                    {
                        //Retry after half a second
                        Thread.Sleep(500);
                        connectionCount += 1;
                        shimmer.Connect();
                    }).Start();
                }
                else if (state == (int)ShimmerBluetooth.SHIMMER_STATE_STREAMING)
                {
                    Debug.Log("THREAD: Streaming " + connectionCount);
                    currentState = ShimmerUnityState.Streaming;
                }
                break;
            case (int)ShimmerBluetooth.ShimmerIdentifier.MSG_IDENTIFIER_NOTIFICATION_MESSAGE:
                break;
            case (int)ShimmerBluetooth.ShimmerIdentifier.MSG_IDENTIFIER_DATA_PACKET:
                ObjectCluster objectCluster = (ObjectCluster)eventArgs.getObject();
                SensorData data = objectCluster.GetData(Shimmer3Configuration.SignalNames.LOW_NOISE_ACCELEROMETER_X, "CAL");
                Debug.Log("THREAD: Data recieved LOW_NOISE_ACCELEROMETER_X" + data.Data);
                //Enque the the data
                break;
        }
    }

    private void OnApplicationQuit()
    {
        if (shimmerThread != null)
        {

            shimmerThread.Abort();
            shimmerThread = null;
        }
    }

}
