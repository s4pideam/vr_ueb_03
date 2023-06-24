using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class WebcamPositioning : MonoBehaviour
{
    private int positionIndex;
    private RectTransform  _rectTransform;
    void Start()
    {
        positionIndex = 0;
        _rectTransform = transform.GetChild(0).transform.GetComponent<RectTransform>();
        setLocalCornerByIndex(positionIndex);
    }

    void setLocalCornerByIndex(int i)
    {
        var vertical = (RectTransform.Edge)(positionIndex % 2 +2);
        var horizontal = (RectTransform.Edge)(positionIndex/2);
        _rectTransform.SetInsetAndSizeFromParentEdge(horizontal, 0, _rectTransform.rect.width);
        _rectTransform.SetInsetAndSizeFromParentEdge(vertical, 0, _rectTransform.rect.height);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("v"))
        {
            positionIndex = ++positionIndex % 4;
            setLocalCornerByIndex(positionIndex);
        }
        
        if (Input.GetKeyDown("h"))
        {
            _rectTransform.gameObject.SetActive(!_rectTransform.gameObject.activeInHierarchy);
        }
    }
}
