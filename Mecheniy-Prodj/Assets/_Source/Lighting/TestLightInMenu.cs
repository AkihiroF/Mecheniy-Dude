using System;
using DG.Tweening;
using UnityEngine;

namespace _Source.Lighting
{
    public class TestLightInMenu : MonoBehaviour
    {
        [SerializeField] private HardLight2D light;
        [SerializeField] private Transform start;
        [SerializeField] private Transform finish;
        [SerializeField] private float fromIntensity;
        [SerializeField] private float toIntensity;
        [SerializeField] private float timeMoving;
        [SerializeField] private float timeIntensityTo;
        [SerializeField] private float timeIntensityFrom;

        private bool _isFinish;

        private void Start()
        {
            _isFinish = true;
            GoMove();
        }

        private void GoMove()
        {
            _isFinish = !_isFinish;
            var point = _isFinish ? finish.position : start.position;
            Sequence seq = DOTween.Sequence();
            var a = light.Intensity;
            var up = 
                DOTween.To(() => a, x => a = x, toIntensity, timeIntensityTo).OnUpdate(() => light.Intensity = a);
            var down =
                DOTween.To(() => a, x => a = x, fromIntensity, timeIntensityFrom).OnUpdate(() => light.Intensity = a);
            seq.Append(up);
            seq.Append(down);
            seq.SetLoops(-1);
            seq.Play();
            light.transform.DOMove(point, timeMoving).OnComplete(() => GoMove());
        }

        private void OnDestroy()
        {
            DOTween.KillAll();
        }
    }
}