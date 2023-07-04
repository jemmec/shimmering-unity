using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShimmerAPI;
using System;

public class ShimmerConnectionTest : MonoBehaviour
{

    [SerializeField]
    private string comport = "COM8";

    [SerializeField]
    private bool tryConnect = false;

    private void Update()
    {
        if (tryConnect || Input.GetKeyDown(KeyCode.Space))
        {
            tryConnect = false;
            Connect();
        }
    }

    private void Connect()
    {
        Debug.Log("Starting Shimmer Connection...");
        int enabledSensors = ((int)ShimmerBluetooth.SensorBitmapShimmer3.SENSOR_A_ACCEL | (int)ShimmerBluetooth.SensorBitmapShimmer3.SENSOR_D_ACCEL);
        byte[] defaultECGReg1 = ShimmerBluetooth.SHIMMER3_DEFAULT_TEST_REG1; //also see ShimmerBluetooth.SHIMMER3_DEFAULT_ECG_REG1
        byte[] defaultECGReg2 = ShimmerBluetooth.SHIMMER3_DEFAULT_TEST_REG2; //also see ShimmerBluetooth.SHIMMER3_DEFAULT_ECG_REG2
        double samplingRate = 51.2;
        ShimmerBluetooth shimmer =
            new ShimmerLogAndStreamSystemSerialPort(
                "ShimmerID`",
                comport,
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
        Debug.Log("End of shimmer connection...");
    }

    private void HandleEvent(object sender, EventArgs args)
    {
        CustomEventArgs eventArgs = (CustomEventArgs)args;
        int indicator = eventArgs.getIndicator();
        Debug.Log("Shimmer Event " + indicator);
    }

}
