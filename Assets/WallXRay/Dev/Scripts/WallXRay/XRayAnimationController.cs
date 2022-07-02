using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine;

namespace AlonDorn.WallXRay
{
    public class XRayAnimationController : MonoBehaviour
    {
        [SerializeField] Transform _holeParent;
        [SerializeField] AnimationCurve _startAnimationCurve;
        [SerializeField] AnimationCurve _endAnimationCurve;
        
        [SerializeField] private float animSpeed = 0.05f;

        private Vector3 _maxScale;
        private Vector3 _minScale;
        private bool _isAnimating;


        private void Start()
        {
            _maxScale = _minScale = _holeParent.localScale;
            _minScale.x = 0;
            _minScale.y = 0;
            _holeParent.localScale = _minScale;
        }


        public async void XrayAnimationStart()
        {
            await UniTask.WaitForEndOfFrame(this);

            if (!_holeParent.gameObject.activeSelf)
                return;

            if (_isAnimating)
            {
                _isAnimating = false;
                await Task.Delay(25);
                await UniTask.WaitForEndOfFrame(this);
            }

            _isAnimating = true;
            float lerpStep = 0;
            Vector3 currentScale = _holeParent.localScale;
            while (_isAnimating && lerpStep < 1)
            {
                _holeParent.localScale = Vector3.Lerp(currentScale, _maxScale, _startAnimationCurve.Evaluate(lerpStep));
                lerpStep += animSpeed;
                await Task.Delay(25);
            }
            
            _isAnimating = false;
        }


        public async void XrayAnimationStop()
        {
            if (_isAnimating)
            {
                _isAnimating = false;
                await Task.Delay(25);
                await UniTask.WaitForEndOfFrame(this);
            }

            _isAnimating = true;
            float lerpStep = 0;
            Vector3 currentScale = _holeParent.localScale;
            while (_isAnimating && lerpStep < 1)
            {
                _holeParent.localScale = Vector3.Lerp(currentScale, _minScale, _endAnimationCurve.Evaluate(lerpStep));
                lerpStep += animSpeed;
                await Task.Delay(25);
            }
            _holeParent.localScale = _minScale;
            _holeParent.gameObject.SetActive(false);
            _isAnimating = false;
        }


        private async void OnApplicationQuit()
        {
            _isAnimating = false;
            await UniTask.WaitForEndOfFrame(this);
            _holeParent.localScale = _maxScale;
            _holeParent.gameObject.SetActive(true);
        }
    }
}

