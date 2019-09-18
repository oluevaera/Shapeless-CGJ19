using System;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private RectTransform background;
    [SerializeField] private RectTransform fill;

    private float _progress = 0f;

    public float Progress
    {
        get { return _progress; }
        set
        {
            _progress = value;

            var newWidth = background.rect.width * _progress;
            
            Fill.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, newWidth);
        }
    }

    public RectTransform Fill => fill;

    private void Start()
    {
        Progress = 0f;
    }
}
