using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlonDorn.WallXRay
{
    public class AssignLayerByTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("XRayObject"))
            {
                // Debug.Log($"trigger enter {other.name}");
                // other.gameObject.layer = LayerMask.NameToLayer("XRayObject");
                other.gameObject.layer = 6;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("XRayObject"))
            {
                // Debug.Log($"trigger exit {other.name}");
                other.gameObject.layer = 0;
            }
        }
    }
}
