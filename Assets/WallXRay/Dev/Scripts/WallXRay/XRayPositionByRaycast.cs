using UnityEngine;


namespace AlonDorn.WallXRay
{

    public class XRayPositionByRaycast : XRayPosition
    {

        [SerializeField] private Camera gameCamera;
        [SerializeField] private GameObject holeParent;
        

        private void Awake()
        {
            if (!gameCamera)
                gameCamera = Camera.main;
        }


        // Activated by the InputManager's game object inspector:
        //TODO: should be registered here by OnEnable - AddListener code.
        public void OnXrayInputStart(Vector2 pressPosition)
        {
            // Debug.Log("OnXrayInputStart");
            OnXray(pressPosition);
            
        }

        // Activated by the InputManager's game object inspector:
        //TODO: should be registered here by OnEnable - AddListener code.
        public void OnXrayInputDrag(Vector2 pressPosition)
        {
            // Debug.Log("OnXrayInputDrag");
            OnXray(pressPosition);
        }



        protected override void OnXray(Vector2 pressPosition)
        {
            var ray = gameCamera.ScreenPointToRay(pressPosition);

            if (!Physics.Raycast(ray, out var hit)) return;
            if (!hit.transform.CompareTag("XRayObject")) return;

            transform.position = hit.point;
            transform.forward = hit.normal;
            if (!holeParent.activeSelf)
                holeParent.SetActive(true);
        }
       

    }
}
