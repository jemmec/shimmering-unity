using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShimmerAPI;
using ShimmerLibrary;

namespace ShimmeringUnity
{


    /// <summary>
    /// Basic example of measuring heart rate from the shimmer device
    /// Ensure the ShimmerDevice has internalExpPower enabled and the
    /// INTERNAL_ADC_A13 sensor enabled.
    /// </summary>
    public class ShimmerHeartrateMonitor : MonoBehaviour
    {

        PPGToHRAlgorithm PPGtoHeartRateCalculation;

        private void Start()
        {


        }

    }

}