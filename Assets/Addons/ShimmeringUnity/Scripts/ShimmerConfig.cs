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

    }
}
