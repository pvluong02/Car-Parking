using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    [SerializeField] LineDrawer lineDrawer;
    [SerializeField] private CanvasGroup avaliableLineGroup;
    [SerializeField] private GameObject avaliableLineHolder;
    [SerializeField] private Image avaliableLineFill;
    private bool isAvaliableLineUIActive = false;

    [SerializeField] Image fadePanel;
    [SerializeField] float fadeDuration;


    private Route activeRoute;
    
    private void Start()
    {
        lineDrawer.GetComponent("DrawLine");
        fadePanel.DOFade(0f, fadeDuration).From(1f);
        avaliableLineGroup.alpha = 0f;
        lineDrawer.onBeginDraw += onBeginDrawHandle;
        lineDrawer.onDraw += onDrawHandle;
        lineDrawer.onEndDraw += onEndDrawHandle;
    }

    private void onEndDrawHandle()
    {
        if (isAvaliableLineUIActive)
        {
            isAvaliableLineUIActive=false;
            activeRoute = null;

            avaliableLineGroup.DOFade(0, .3f).From(1f);
        }
    }

    private void onDrawHandle()
    {
        if(isAvaliableLineUIActive)
        {
            float maxLineLength = activeRoute.maxLineLength;
            float lineLength = activeRoute.line.length;

            avaliableLineFill.fillAmount = 1 - (lineLength / maxLineLength);
        }
    }

    private void onBeginDrawHandle(Route route)
    {
        activeRoute = route;
        avaliableLineFill.color = activeRoute.carColor;
        avaliableLineFill.fillAmount = 1f;
        avaliableLineGroup.DOFade(1f, .3f).From(0f);
        isAvaliableLineUIActive = true;
    }
    

}
