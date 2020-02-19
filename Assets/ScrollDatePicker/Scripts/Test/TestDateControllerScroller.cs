using System;
using System.Collections;
using System.Collections.Generic;
using ScrollDatePicker;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestDateControllerScroller : MonoBehaviour
{

    public ScrollDatePickerController datePickerController;

    public Button upButton;
    public Button downButton;


    private void Awake()
    {
    }


    private PointerEventData UpPointerDown(PointerEventData eventData)
    {
        return null;
    }
}
