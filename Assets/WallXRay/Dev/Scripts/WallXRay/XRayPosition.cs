using UnityEngine;

namespace AlonDorn.WallXRay
{
    public abstract class XRayPosition : MonoBehaviour
    {
        protected abstract void OnXray(Vector2 pressPosition);

    }
}
