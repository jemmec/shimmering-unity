# Shimmering Unity

Shimmer3 integration directly in Unity.

> Tested with shimmer3 device on unity 2021.3.X (Use a different version with caution)

## Usage

1. Download the latest [unity package]() from releases and import it into your unity project.

2. Add the `ShimmerDevice` component to a GameObject in your scene (alternatively, open the `ExampleScene` scene).

3. Setup configuration through the `ShimmerDevice` inspector, setting the COM port and Sampling rate to match your connected shimmer device.

4. Run your project and test the connection with the [Connect] and [StartStreaming] buttons.

5. (OPTIONAL) Add a `ShimmerDataLogger` component to the same GameObject. Add the `ShimmerDevice` as an inspector reference, and add some signals to the signals list (ensuring the signals are enabled on your device). Run your project again, after starting the streaming you should now see the signal values in the `ShimmerDataLogger` change.

### Shimmer Data Reference

#### Signal Names (Shimmer3)

```
TIMESTAMP = "Timestamp"
SYSTEM_TIMESTAMP = "System Timestamp"
SYSTEM_TIMESTAMP_PLOT = "System Timestamp Plot"
LOW_NOISE_ACCELEROMETER_X = "Low Noise Accelerometer X"
LOW_NOISE_ACCELEROMETER_Y = "Low Noise Accelerometer Y"
LOW_NOISE_ACCELEROMETER_Z = "Low Noise Accelerometer Z"
V_SENSE_BATT = "VSenseBatt"
WIDE_RANGE_ACCELEROMETER_X = "Wide Range Accelerometer X"
WIDE_RANGE_ACCELEROMETER_Y = "Wide Range Accelerometer Y"
WIDE_RANGE_ACCELEROMETER_Z = "Wide Range Accelerometer Z"
MAGNETOMETER_X = "Magnetometer X"
MAGNETOMETER_Y = "Magnetometer Y"
MAGNETOMETER_Z = "Magnetometer Z"
GYROSCOPE_X = "Gyroscope X"
GYROSCOPE_Y = "Gyroscope Y"
GYROSCOPE_Z = "Gyroscope Z"
EXTERNAL_ADC_A7 = "External ADC A7"
EXTERNAL_ADC_A6 = "External ADC A6"
EXTERNAL_ADC_A15 = "External ADC A15"
INTERNAL_ADC_A1 = "Internal ADC A1"
INTERNAL_ADC_A12 = "Internal ADC A12"
INTERNAL_ADC_A13 = "Internal ADC A13"
INTERNAL_ADC_A14 = "Internal ADC A14"
PRESSURE = "Pressure"
TEMPERATURE = "Temperature"
GSR = "GSR"
GSR_CONDUCTANCE = "GSR Conductance"
EXG1_STATUS = "EXG1 Status"
EXG2_STATUS = "EXG2 Status"
ECG_LL_RA = "ECG LL-RA"
ECG_LA_RA = "ECG LA-RA"
ECG_VX_RL = "ECG Vx-RL"
EMG_CH1 = "EMG CH1"
EMG_CH2 = "EMG CH2"
EXG1_CH1 = "EXG1 CH1"
EXG1_CH2 = "EXG1 CH2"
EXG2_CH1 = "EXG2 CH1"
EXG2_CH2 = "EXG2 CH2"
EXG1_CH1_16BIT = "EXG1 CH1 16Bit"
EXG1_CH2_16BIT = "EXG1 CH2 16Bit"
EXG2_CH1_16BIT = "EXG2 CH1 16Bit"
EXG2_CH2_16BIT = "EXG2 CH2 16Bit"
BRIGE_AMPLIFIER_HIGH = "Bridge Amplifier High"
BRIGE_AMPLIFIER_LOW = "Bridge Amplifier Low"
QUATERNION_0 = "Quaternion 0"
QUATERNION_1 = "Quaternion 1"
QUATERNION_2 = "Quaternion 2"
QUATERNION_3 = "Quaternion 3"
AXIS_ANGLE_A = "Axis Angle A"
AXIS_ANGLE_X = "Axis Angle X"
AXIS_ANGLE_Y = "Axis Angle Y"
AXIS_ANGLE_Z = "Axis Angle Z"
```

#### Signal Formats

```
CAL = "CAL"
RAW = "RAW"
```

#### Signal Units

```
MilliSeconds = "mSecs"
NoUnits = "no units"
MeterPerSecondSquared = "m/(sec^2)"
MeterPerSecondSquared_DefaultCal = "m/(sec^2)*"
DegreePerSecond = "deg/sec"
DegreePerSecond_DefaultCal = "deg/sec*"
MilliVolts = "mVolts"
MilliVolts_DefaultCal = "mVolts*"
KiloPascal = "kPa"
Celcius = "Celcius*"
Local = "local"
Local_DefaultCal = "local*"
KiloOhms = "kOhms"
MicroSiemens = "uSiemens"
NanoAmpere = "nA"
```


## Knowledge

### Flashing shimmer to latest Log and Stream firmware
https://shimmersensing.com/support/wireless-sensor-networks-download/ >> Consensys V1.6.0 (64bit)
![image](https://github.com/jemmec/shimmering-unity/assets/41222625/d594ea29-d0db-4b0e-bc1c-b1c0effa3ee1)

### Firmware manual & User guide
1. Manual: https://shimmersensing.com/wp-content/docs/support/documentation/LogAndStream_for_Shimmer3_Firmware_User_Manual_rev0.11a.pdf
2. User guide: https://shimmersensing.com/wp-content/docs/support/documentation/Consensys_User_Guide_rev1.6a.pdf

### Lighting Indication
![image](https://github.com/jemmec/shimmering-unity/assets/41222625/e2255e72-385c-4a8b-8871-28b46a566a24)
![image](https://github.com/jemmec/shimmering-unity/assets/41222625/ffa5d1ba-fe58-4ce9-832b-2c2518af0e92)

### Manually installing ShimmerAPI into Unity

1. Clone the ShimmerAPI solution https://github.com/ShimmerEngineering/Shimmer-C-API

2. Build the Class Libraries for ShimmerAPI in Visual Studio.

3. Copy the correct version of the following `.dll` files into unity `/Plugins` folder, you may have to create the folder.

    - `ShimmerAPI.dll` (netstandard2.0) \~that we just built\~
    - `Google.Protobuf.dll` (netstandard2.0)
    - `Grpc.Core.Api.dll` (netstandard2.0)
    - `Grpc.Core.dll` (netstandard2.0)
    - `MathNet.Numerics.dll` (netstandard2.0)
    - `System.Runtime.CompilerServices.Unsafe.dll` (netstandard2.0)
    - `System.IO.Ports.dll` (net 461)
    
4. Set API target to .Net Framework inside Unity Project Settings.

5. Create a script that connects to your shimmer device via the ShimmerAPI.

## Credits

The [ShimmerAPI](https://github.com/ShimmerEngineering/Shimmer-C-API) was created by [Shimmer](https://shimmersensing.com/).
