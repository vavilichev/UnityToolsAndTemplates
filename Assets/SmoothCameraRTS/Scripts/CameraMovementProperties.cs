using System;
using UnityEngine;

namespace Vavilichev.UTAT.CameraMovement
{
    [Serializable]
    public class CameraMovementProperties
    {
        public Transform Pivot;
        public float Speed = 4;
        public float Smoothness = 0.2f;
    }
}