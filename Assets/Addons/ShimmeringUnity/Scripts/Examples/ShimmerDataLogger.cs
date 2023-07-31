using System.Collections.Generic;
using ShimmerAPI;
using UnityEngine;

namespace ShimmeringUnity
{
    /// <summary>
    /// Example of logging data from a shimmer device
    /// </summary>
    public class ShimmerDataLogger : MonoBehaviour
    {

        [SerializeField]
        [Tooltip("Reference to the shimmer device.")]
        private ShimmerDevice shimmerDevice;

        [System.Serializable]
        public class Signal
        {
            [SerializeField]
            [Tooltip("The signal's name. More info in the signals section of the readme.")]
            private ShimmerConfig.SignalName name;

            public ShimmerConfig.SignalName Name => name;

            [SerializeField]
            [Tooltip("The signal's format.")]
            private ShimmerConfig.SignalFormat format;

            public ShimmerConfig.SignalFormat Format => format;

            [SerializeField]
            [Tooltip("The units the signal's value is displayed in, set to \"Automatic\" for default.")]
            private ShimmerConfig.SignalUnits unit;

            public ShimmerConfig.SignalUnits Unit => unit;

            [SerializeField]
            [Tooltip("The value output of this signal (only for debug purposes).")]
            private string value;

            public string Value
            {
                set => this.value = value;
            }
        }

        [SerializeField]
        [Tooltip("List of signals to record from this device.")]
        private List<Signal> signals = new List<Signal>();

        private void OnEnable()
        {
            //Listen to the data recieved event when enabled
            shimmerDevice?.OnDataRecieved.AddListener(OnDataRecieved);
        }

        private void OnDisable()
        {
            //Stop listening to the data recieved event when disabled
            shimmerDevice?.OnDataRecieved.RemoveListener(OnDataRecieved);
        }

        /// <summary>
        /// Event listener for the shimmer device's data recieved event
        /// </summary>
        /// <param name="device"></param>
        /// <param name="objectCluster"></param>
        private void OnDataRecieved(ShimmerDevice device, ObjectCluster objectCluster)
        {
            foreach (var signal in signals)
            {
                //Get the data
                SensorData data = signal.Unit == ShimmerConfig.SignalUnits.Automatic ?
                    objectCluster.GetData(
                        ShimmerConfig.NAME_DICT[signal.Name],
                        ShimmerConfig.FORMAT_DICT[signal.Format]) :
                    objectCluster.GetData(
                        ShimmerConfig.NAME_DICT[signal.Name],
                        ShimmerConfig.FORMAT_DICT[signal.Format],
                        ShimmerConfig.UNIT_DICT[signal.Unit]);

                //If data is null, early out
                if (data == null)
                {
                    signal.Value = "NULL";
                    continue;
                }

                //Write data back into the signal for debugging
                signal.Value = $"{data.Data} {data.Unit}";

                //This is where you can do something with the data...
            }
        }

    }
}
