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
            GUILayout.Label($"Current State: {shimmerDevice.CurrentState.ToString()}");
            if (GUILayout.Button("Connect"))
            {
                shimmerDevice.Connect();
            }
            if (GUILayout.Button("Disconnect"))
            {

            }
            if (GUILayout.Button("Start Streaming"))
            {

            }
            if (GUILayout.Button("Stop Streaming"))
            {

            }
            DrawDefaultInspector();
        }
    }
}