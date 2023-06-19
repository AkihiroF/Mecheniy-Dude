using System;
using DG.Tweening;
using UnityEngine;

namespace _Source.Lighting
{
    public class TestLightInMenu : MonoBehaviour
    {
        [SerializeField] private HardLight2D light;

        private void Start()
        {
            Sequence seq = DOTween.Sequence();
            var a = light.Intensity;
            var up = DOTween.To(() => a, x => a = x, 3, 1.5f).OnUpdate(() => light.Intensity = a);
            var down = DOTween.To(() => a, x => a = x, 1.2f, 1.5f).OnUpdate(() => light.Intensity = a);
            seq.Append(up);
            seq.Append(down);
            seq.SetLoops(-1);
            seq.Play();
            light.transform.DOMoveX(9, 50);
        }
    }
}