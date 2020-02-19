using System;
using System.Collections;
using System.Collections.Generic;
using ScrollDatePicker;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollDateButtons : MonoBehaviour
{
    public EventTrigger scrollUp;
    public EventTrigger scrollDown;

    public ScrollDatePickerController scrollDatePickerController;
    public bool shouldInit;
    
    private IDictionary<string, bool> _scrollStatus = new Dictionary<string, bool>();
    
    
    private void Awake()
    {
        if (shouldInit)
        {
            Initialize();
        }
    }

    private void Update()
    {

        if (_scrollStatus.Count <= 0)
        {
            return;
        }


        if (_scrollStatus[nameof(StimulateScrollUp)])
        {
            scrollDatePickerController.StimulateUp();
        }
        else if (_scrollStatus[nameof(StimulateScrollDown)])
        {
            scrollDatePickerController.StimulateDown();
        }
       
    }

    public void Initialize()
    {
        _scrollStatus[nameof(StimulateScrollUp)] = false;
        _scrollStatus[nameof(StimulateScrollDown)] = false;
        
        // scroll up
        CreateEventEntry(scrollUp, EventTriggerType.PointerDown, StimulateScrollUp);
        CreateEventEntry(scrollUp, EventTriggerType.PointerUp, StimulateScrollUp);
        
        
        // scroll down
        CreateEventEntry(scrollDown, EventTriggerType.PointerDown, StimulateScrollDown);
        CreateEventEntry(scrollDown, EventTriggerType.PointerUp, StimulateScrollDown);
    }

    private void StimulateScrollUp(PointerEventData eventData)
    {
        _scrollStatus[nameof(StimulateScrollUp)] = !_scrollStatus[nameof(StimulateScrollUp)];
    }
    
    private void StimulateScrollDown(PointerEventData eventData)
    {
        _scrollStatus[nameof(StimulateScrollDown)] = !_scrollStatus[nameof(StimulateScrollDown)];
    }



    private void CreateEventEntry(EventTrigger eventTrigger, EventTriggerType eventTriggerType, Action<PointerEventData> eventData)
    {
        var entry = new EventTrigger.Entry();
        entry.eventID = eventTriggerType;
        entry.callback.AddListener((data) => eventData((PointerEventData)data));
        eventTrigger.triggers.Add(entry);
    }
}
