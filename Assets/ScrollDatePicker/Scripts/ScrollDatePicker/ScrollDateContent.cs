using System;
using UnityEngine;
using UnityEngine.UI;

namespace ScrollDatePicker
{
    public class ScrollDateContent : MonoBehaviour
    {
        public DateTime MyDate {
            get => _myDate;
            set
            {
                _myDate = value;
                SetDate(_myDate);
            }
        }

        public Vector2 AnchoredPosition
        {
            get
            {
                if (_rectTransform == null)
                {
                    _rectTransform = GetComponent<RectTransform>();
                }

                return _rectTransform.anchoredPosition;
            }
        }

        public RectTransform MyRectTransform
        {
            get
            {
                if (_rectTransform == null)
                {
                    _rectTransform = GetComponent<RectTransform>();
                }

                return _rectTransform;
            }
        }

        public CanvasGroup MyCanvasGroup
        {
            get
            {
                if (_canvasGroup == null)
                {
                    _canvasGroup = GetComponent<CanvasGroup>();

                    if (_canvasGroup == null)
                    {
                        _canvasGroup = gameObject.AddComponent<CanvasGroup>();
                    }
                }

                return _canvasGroup;
            }
        }

        public Text dateText;
        
        private DateTime _myDate = DateTime.Now;
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;



        private void SetDate(DateTime dateTime)
        {
            dateText.text =
                $"{dateTime.Year} Year {dateTime.Month} Month {dateTime.Day} Day ({dateTime.DayOfWeek.ToString()})";
        }


    }
}
