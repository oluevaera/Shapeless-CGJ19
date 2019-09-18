using System;
using UnityEngine;
using UnityEngine.UI;

public class UIOrderPanel : MonoBehaviour
{
    [SerializeField] private Image orderImage;
    [SerializeField] private Image stage1Image;
    [SerializeField] private Image stage2Image;
    [SerializeField] private Image stage3Image;
    [SerializeField] private ProgressBar progressBar;

    private Order order;

    public void Init(Sprite orderIm, Sprite stage1, Sprite stage2, Sprite stage3, Order order)
    {
        orderImage.sprite = orderIm;
        stage1Image.sprite = stage1;
        stage2Image.sprite = stage2;
        stage3Image.sprite = stage3;
        this.order = order;
    }

    private void Update()
    {
        progressBar.Progress = order.timeRemaining / order.time;
    }
}