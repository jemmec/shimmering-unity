using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ShimmeringUnity
{
    [CustomEditor(typeof(ShimmerDevice), true)]
    public class ShimmerDeviceEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            ShimmerDevice shimmerDevice = (ShimmerDevice)target;
            GUIStyle style = new GUIStyle();
            style.richText = true;
            GUILayout.Label($"<size=24><color=#00ff00>Current State: {shimmerDevice.CurrentState.ToString()}</color></size>", style);
            if (GUILayout.Button("Connect"))
            {
                shimmerDevice.Connect();
            }
            if (GUILayout.Button("Disconnect"))
            {
                shimmerDevice.Disconnect();
            }
            if (GUILayout.Button("Start Streaming"))
            {
                shimmerDevice.StartStreaming();
            }
            if (GUILayout.Button("Stop Streaming"))
            {
                shimmerDevice.StopStreaming();
            }
            DrawDefaultInspector();
        }
    }
}