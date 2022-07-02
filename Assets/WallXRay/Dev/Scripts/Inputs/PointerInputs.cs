using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


namespace AlonDorn.WallXRay.Inputs
{
    public class PointerInputs : MonoBehaviour
    {
        public UnityEvent<Vector2> onPointerDown;
        public UnityEvent onPointerUp;
        public UnityEvent<Vector2> onPointerDrag;
      
        [SerializeField] private Camera gameCamera;

        private PointerControls _pointerControls;
        private bool _isHolding;


        private void Awake()
        {
            _pointerControls = new PointerControls();
            
            if (!gameCamera)
                gameCamera = Camera.main;
        }
        private void OnEnable()
        {
            _pointerControls.Pointer.PointerDown.performed += OnMouseDown;
            _pointerControls.Pointer.PointerDown.canceled += OnMouseUp;
            _pointerControls.Pointer.PointerPosition.performed += OnMouseDrag;
            _pointerControls.Pointer.Enable();
            
        }


        private void OnDisable()
        {
            _pointerControls.Pointer.PointerDown.performed -= OnMouseDown;
            _pointerControls.Pointer.PointerDown.canceled -= OnMouseUp;
            _pointerControls.Pointer.PointerPosition.performed -= OnMouseDrag;
            _pointerControls.Pointer.Disable();
           
        }


        private void OnMouseDown(InputAction.CallbackContext obj)
        {
            onPointerDown?.Invoke(_pointerControls.Pointer.PointerPosition.ReadValue<Vector2>());
            _isHolding = true;
        }

        private void OnMouseUp(InputAction.CallbackContext obj)
        {
            onPointerUp?.Invoke();
            _isHolding = false;
        }



        private void OnMouseDrag(InputAction.CallbackContext obj)
        {
            if (_isHolding)
            {
                onPointerDrag?.Invoke(obj.ReadValue<Vector2>());
            }
        }
    }
}

