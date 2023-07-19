using System.Collections.Generic;

namespace ShimmeringUnity
{
    /// <summary>
    /// Helper class for all shimmer data formats
    /// </summary>
    public static class ShimmerConfig
    {
        /// <summary>
        /// Enum of signal formats used by Shimmer devices
        /// </summary>
        public enum SignalFormat
        {
            CAL,
            RAW
        }

        /// <summary>
        /// Dictionary to convert a SignalFormat enum value to the 
        /// correct string value
        /// </summary>
        /// <typeparam name="SignalFormat"></typeparam>
        /// <typeparam name="string"></typeparam>
        /// <returns></returns>
        public static Dictionary<SignalFormat, string> FORMAT_DICT =
            new Dictionary<SignalFormat, string>()
        {
            {SignalFormat.CAL, "CAL"},
            {SignalFormat.RAW, "RAW"}
        };

        /// <summary>
        /// Enum of signal names used by the Shimmer3 devices
        /// </summary>
        public enum SignalName
        {
            TIMESTAMP,
            SYSTEM_TIMESTAMP,
            SYSTEM_TIMESTAMP_PLOT,
            LOW_NOISE_ACCELEROMETER_X,
            LOW_NOISE_ACCELEROMETER_Y,
            LOW_NOISE_ACCELEROMETER_Z,
            V_SENSE_BATT,
            WIDE_RANGE_ACCELEROMETER_X,
            WIDE_RANGE_ACCELEROMETER_Y,
            WIDE_RANGE_ACCELEROMETER_Z,
            MAGNETOMETER_X,
            MAGNETOMETER_Y,
            MAGNETOMETER_Z,
            GYROSCOPE_X,
            GYROSCOPE_Y,
            GYROSCOPE_Z,
            EXTERNAL_ADC_A7,
            EXTERNAL_ADC_A6,
            EXTERNAL_ADC_A15,
            INTERNAL_ADC_A1,
            INTERNAL_ADC_A12,
            INTERNAL_ADC_A13,
            INTERNAL_ADC_A14,
            PRESSURE,
            TEMPERATURE,
            GSR,
            GSR_CONDUCTANCE,
            EXG1_STATUS,
            EXG2_STATUS,
            ECG_LL_RA,
            ECG_LA_RA,
            ECG_VX_RL,
            EMG_CH1,
            EMG_CH2,
            EXG1_CH1,
            EXG1_CH2,
            EXG2_CH1,
            EXG2_CH2,
            EXG1_CH1_16BIT,
            EXG1_CH2_16BIT,
            EXG2_CH1_16BIT,
            EXG2_CH2_16BIT,
            BRIGE_AMPLIFIER_HIGH,
            BRIGE_AMPLIFIER_LOW,
            QUATERNION_0,
            QUATERNION_1,
            QUATERNION_2,
            QUATERNION_3,
            AXIS_ANGLE_A,
            AXIS_ANGLE_X,
            AXIS_ANGLE_Y,
            AXIS_ANGLE_Z,
        }

        /// <summary>
        /// Dictionary to convert a SignalName enum value to the 
        /// correct string value
        /// </summary>
        /// <typeparam name="SignalName"></typeparam>
        /// <typeparam name="string"></typeparam>
        /// <returns></returns>
        public static Dictionary<SignalName, string> NAME_DICT =
            new Dictionary<SignalName, string>()
        {
            {SignalName.TIMESTAMP, "Timestamp"},
            {SignalName.SYSTEM_TIMESTAMP, "System Timestamp"},
            {SignalName.SYSTEM_TIMESTAMP_PLOT, "System Timestamp Plot"},
            {SignalName.LOW_NOISE_ACCELEROMETER_X, "Low Noise Accelerometer X"},
            {SignalName.LOW_NOISE_ACCELEROMETER_Y, "Low Noise Accelerometer Y"},
            {SignalName.LOW_NOISE_ACCELEROMETER_Z, "Low Noise Accelerometer Z"},
            {SignalName.V_SENSE_BATT, "VSenseBatt"},
            {SignalName.WIDE_RANGE_ACCELEROMETER_X, "Wide Range Accelerometer X"},
            {SignalName.WIDE_RANGE_ACCELEROMETER_Y, "Wide Range Accelerometer Y"},
            {SignalName.WIDE_RANGE_ACCELEROMETER_Z, "Wide Range Accelerometer Z"},
            {SignalName.MAGNETOMETER_X, "Magnetometer X"},
            {SignalName.MAGNETOMETER_Y, "Magnetometer Y"},
            {SignalName.MAGNETOMETER_Z, "Magnetometer Z"},
            {SignalName.GYROSCOPE_X, "Gyroscope X"},
            {SignalName.GYROSCOPE_Y, "Gyroscope Y"},
            {SignalName.GYROSCOPE_Z, "Gyroscope Z"},
            {SignalName.EXTERNAL_ADC_A7, "External ADC A7"},
            {SignalName.EXTERNAL_ADC_A6, "External ADC A6"},
            {SignalName.EXTERNAL_ADC_A15, "External ADC A15"},
            {SignalName.INTERNAL_ADC_A1, "Internal ADC A1"},
            {SignalName.INTERNAL_ADC_A12, "Internal ADC A12"},
            {SignalName.INTERNAL_ADC_A13, "Internal ADC A13"},
            {SignalName.INTERNAL_ADC_A14, "Internal ADC A14"},
            {SignalName.PRESSURE, "Pressure"},
            {SignalName.TEMPERATURE, "Temperature"},
            {SignalName.GSR, "GSR"},
            {SignalName.GSR_CONDUCTANCE, "GSR Conductance"},
            {SignalName.EXG1_STATUS, "EXG1 Status"},
            {SignalName.EXG2_STATUS, "EXG2 Status"},
            {SignalName.ECG_LL_RA, "ECG LL-RA"},
            {SignalName.ECG_LA_RA, "ECG LA-RA"},
            {SignalName.ECG_VX_RL, "ECG Vx-RL"},
            {SignalName.EMG_CH1, "EMG CH1"},
            {SignalName.EMG_CH2, "EMG CH2"},
            {SignalName.EXG1_CH1, "EXG1 CH1"},
            {SignalName.EXG1_CH2, "EXG1 CH2"},
            {SignalName.EXG2_CH1, "EXG2 CH1"},
            {SignalName.EXG2_CH2, "EXG2 CH2"},
            {SignalName.EXG1_CH1_16BIT, "EXG1 CH1 16Bit"},
            {SignalName.EXG1_CH2_16BIT, "EXG1 CH2 16Bit"},
            {SignalName.EXG2_CH1_16BIT, "EXG2 CH1 16Bit"},
            {SignalName.EXG2_CH2_16BIT, "EXG2 CH2 16Bit"},
            {SignalName.BRIGE_AMPLIFIER_HIGH, "Bridge Amplifier High"},
            {SignalName.BRIGE_AMPLIFIER_LOW, "Bridge Amplifier Low"},
            {SignalName.QUATERNION_0, "Quaternion 0"},
            {SignalName.QUATERNION_1, "Quaternion 1"},
            {SignalName.QUATERNION_2, "Quaternion 2"},
            {SignalName.QUATERNION_3, "Quaternion 3"},
            {SignalName.AXIS_ANGLE_A, "Axis Angle A"},
            {SignalName.AXIS_ANGLE_X, "Axis Angle X"},
            {SignalName.AXIS_ANGLE_Y, "Axis Angle Y"},
            {SignalName.AXIS_ANGLE_Z, "Axis Angle Z"},
        };

        /// <summary>
        /// Enum of signal unity types used by all shimmer devices 
        /// </summary>
        public enum SignalUnits
        {
            Automatic,
            MilliSeconds,
            NoUnits,
            MeterPerSecondSquared,
            MeterPerSecondSquared_DefaultCal,
            DegreePerSecond,
            DegreePerSecond_DefaultCal,
            MilliVolts,
            MilliVolts_DefaultCal,
            KiloPascal,
            Celcius,
            Local,
            Local_DefaultCal,
            KiloOhms,
            MicroSiemens,
            NanoAmpere,
        }

        /// <summary>
        /// Dictionary to convert a SignalUnit enum value to the 
        /// correct string value
        /// </summary>
        /// <typeparam name="SignalUnits"></typeparam>
        /// <typeparam name="string"></typeparam>
        /// <returns></returns>
        public static Dictionary<SignalUnits, string> UNIT_DICT =
            new Dictionary<SignalUnits, string>()
        {
            {SignalUnits.Automatic , ""},
            {SignalUnits.MilliSeconds , "mSecs"},
            {SignalUnits.NoUnits , "no units"},
            {SignalUnits.MeterPerSecondSquared , "m/(sec^2)"},
            {SignalUnits.MeterPerSecondSquared_DefaultCal , "m/(sec^2)*"},
            {SignalUnits.DegreePerSecond , "deg/sec"},
            {SignalUnits.DegreePerSecond_DefaultCal , "deg/sec*"},
            {SignalUnits.MilliVolts , "mVolts"},
            {SignalUnits.MilliVolts_DefaultCal , "mVolts*"},
            {SignalUnits.KiloPascal , "kPa"},
            {SignalUnits.Celcius , "Celcius*"},
            {SignalUnits.Local , "local"},
            {SignalUnits.Local_DefaultCal , "local*"},
            {SignalUnits.KiloOhms , "kOhms"},
            {SignalUnits.MicroSiemens , "uSiemens"},
            {SignalUnits.NanoAmpere , "nA"},
        };

        [System.Flags]
        /// <summary>
        /// The sensor bitmap for shimmer3
        /// </summary>
        public enum SensorBitmap
        {
            SENSOR_A_ACCEL = 0x80,
            SENSOR_MPU9150_GYRO = 0x040,
            SENSOR_LSM303DLHC_MAG = 0x20,
            SENSOR_GSR = 0x04,
            SENSOR_EXT_A7 = 0x02,
            SENSOR_EXT_A6 = 0x01,
            SENSOR_VBATT = 0x2000,
            SENSOR_D_ACCEL = 0x1000,
            SENSOR_EXT_A15 = 0x0800,
            SENSOR_INT_A1 = 0x0400,
            SENSOR_INT_A12 = 0x0200,
            SENSOR_INT_A13 = 0x0100,
            SENSOR_INT_A14 = 0x800000,
            SENSOR_BMP180_PRESSURE = 0x40000,
            SENSOR_EXG1_24BIT = 0x10,
            SENSOR_EXG2_24BIT = 0x08,
            SENSOR_EXG1_16BIT = 0x100000,
            SENSOR_EXG2_16BIT = 0x080000,
            SENSOR_BRIDGE_AMP = 0x8000
        }

        /// <summary>
        /// Shimmer3 options - 0,1,2,3,4 = 2g,4g,8g,16g.
        /// </summary>
        public enum AccelerometerRange
        {
            [UnityEngine.InspectorName("± 2g")]
            zero = 0,
            [UnityEngine.InspectorName("± 4g")]
            one = 1,
            [UnityEngine.InspectorName("± 8g")]
            two = 2,
            [UnityEngine.InspectorName("± 16g")]
            three = 3
        }

        /// <summary>
        /// Range is between 0 and 4. 0 = 8-63kOhm, 1 = 63-220kOhm, 2 = 220-680kOhm, 3 = 680kOhm-4.7MOhm, 4 = Auto range
        /// </summary>
        public enum GSRRange
        {
            [UnityEngine.InspectorName("8-63kOhm")]
            zero = 0,
            [UnityEngine.InspectorName("63-220kOhm")]
            one = 1,
            [UnityEngine.InspectorName("220-680kOhm")]
            two = 2,
            [UnityEngine.InspectorName("680kOhm-4.7MOhm")]
            three = 3,
            [UnityEngine.InspectorName("Auto Range")]
            four = 4
        }

        /// <summary>
        /// Options are 0,1,2,3. Where 0 = 250 Degree/s, 1 = 500 Degree/s, 2 = 1000 Degree/s, 3 = 2000 Degree/s
        /// </summary>
        public enum GyroscopeRange
        {
            [UnityEngine.InspectorName("250dps")]
            zero = 0,
            [UnityEngine.InspectorName("500dps")]
            one = 1,
            [UnityEngine.InspectorName("1000dps")]
            two = 2,
            [UnityEngine.InspectorName("2000dps")]
            three = 3
        }

        /// <summary>
        /// Shimmer3: 1,2,3,4,5,6,7 = 1.3, 1.9, 2.5, 4.0, 4.7, 5.6, 8.1
        /// </summary>
        public enum MagnetometerRange
        {
            [UnityEngine.InspectorName("± 1.3Ga")]
            one = 1,
            [UnityEngine.InspectorName("± 1.3Ga")]
            two = 2,
            [UnityEngine.InspectorName("± 2.5Ga")]
            three = 3,
            [UnityEngine.InspectorName("± 4.0Ga")]
            four = 4,
            [UnityEngine.InspectorName("± 4.7Ga")]
            five = 5,
            [UnityEngine.InspectorName("± 5.6Ga")]
            six = 6,
            [UnityEngine.InspectorName("± 8.1Ga")]
            seven = 7
        }

    }
}
