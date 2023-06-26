using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerController: MonoBehaviour
    {
        protected OpenCVForUnity.CoreModule.Rect faceRect ;
        
        public void updateFaceRect(OpenCVForUnity.CoreModule.Rect faceRect)
        {
            this.faceRect = faceRect;
        }

        private void Start()
        {
            faceRect = new OpenCVForUnity.CoreModule.Rect();
        }
    }
}