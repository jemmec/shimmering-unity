using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShimmerAPI;
using ShimmerLibrary;
using System;

namespace ShimmeringUnity
{
    /// <summary>
    /// Basic example of measuring heart rate from the shimmer device
    /// Ensure the ShimmerDevice has internalExpPower enabled and the
    /// INTERNAL_ADC_A13 sensor enabled. Also ensure the correct
    /// sampling rate is set before running the application.
    /// </summary>
    public class ShimmerHeartRateMonitor : MonoBehaviour
    {
        [SerializeField]
        private ShimmerDevice shimmerDevice;

        [SerializeField]
        private int heartRate;
        Filter LPF_PPG;
        Filter HPF_PPG;
        PPGToHRAlgorithm PPGtoHeartRateCalculation;
        int NumberOfHeartBeatsToAverage = 1;
        int TrainingPeriodPPG = 10; //10 second buffer
        double LPF_CORNER_FREQ_HZ = 5;
        double HPF_CORNER_FREQ_HZ = 0.5;

        private void Awake()
        {
            //Create the heart rate algorithms 
            PPGtoHeartRateCalculation = new PPGToHRAlgorithm(shimmerDevice.SamplingRate, NumberOfHeartBeatsToAverage, TrainingPeriodPPG);
            LPF_PPG = new Filter(Filter.LOW_PASS, shimmerDevice.SamplingRate, new double[] { LPF_CORNER_FREQ_HZ });
            HPF_PPG = new Filter(Filter.HIGH_PASS, shimmerDevice.SamplingRate, new double[] { HPF_CORNER_FREQ_HZ });
        }
        private void OnEnable()
        {
            shimmerDevice.OnDataRecieved.AddListener(OnDataRecieved);
        }

        private void OnDisable()
        {
            shimmerDevice.OnDataRecieved.RemoveListener(OnDataRecieved);
        }

        private void OnDataRecieved(ShimmerDevice device, ObjectCluster objectCluster)
        {
            //Get heart rate data
            SensorData dataPPG = objectCluster.GetData(
                ShimmerConfig.NAME_DICT[ShimmerConfig.SignalName.INTERNAL_ADC_A13],
                ShimmerConfig.FORMAT_DICT[ShimmerConfig.SignalFormat.CAL]
            );
            //Get system  timestamp data
            SensorData dataTS = objectCluster.GetData(
                ShimmerConfig.NAME_DICT[ShimmerConfig.SignalName.SYSTEM_TIMESTAMP],
                ShimmerConfig.FORMAT_DICT[ShimmerConfig.SignalFormat.CAL]
            );

            //Early out if either sensor data is null
            if (dataPPG == null || dataTS == null)
                return;

            //Calculate the heart rate
            double dataFilteredLP = LPF_PPG.filterData(dataPPG.Data);
            double dataFilteredHP = HPF_PPG.filterData(dataFilteredLP);
            heartRate = (int)Math.Round(PPGtoHeartRateCalculation.ppgToHrConversion(dataFilteredHP, dataTS.Data));
        }
    }

}