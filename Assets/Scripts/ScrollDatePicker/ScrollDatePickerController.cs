using System;
using Boo.Lang;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace ScrollDatePicker
{
    [RequireComponent(typeof(UI_InfiniteScrollModified))]
    public class ScrollDatePickerController : MonoBehaviour, IScrollEvent
    {
        public enum DatePickerScrollMovement
        {
            None = 0,
            Up = -1,
            Down = 1
        }


        public string SelectedDateString
        {
            get
            {
                if (_dateContents.Count > 0)
                {
                    return _dateContents[_centerIndex].MyDate.ToShortDateString();
                }

                return DateTime.Now.ToShortDateString();
            }
        }

        public DateTime SelectedDateTime
        {
            get
            {
                if (_dateContents.Count > 0)
                {
                    return _dateContents[_centerIndex].MyDate;
                }

                return DateTime.Now;
            }
        }


        public UI_InfiniteScrollModified infiniteScrollModified;
        public RectTransform scrollViewContent;
        public RectTransform center;

        public float lerpSpeed = 5f;
        public float centerTolerance = .1f; 
        public int yearLimit;
        public bool shouldInit;

        private readonly List<ScrollDateContent> _dateContents = new List<ScrollDateContent>();
        private DateTime _currentDate;
        private int _centerIndex;
        private float _offsetContentPositionY = -365;
        private float _currentContentPositionY = 0f;
        private bool _update = false;

        void Awake()
        {
            if (shouldInit)
            {
                Initialize();
            }
        }

        public void Update()
        {
            if (_dateContents.Count <= 0 || !_update)
            {
                return;
            }

            var distance = new float[_dateContents.Count];
            var minDistance = 0f;
        
            for (var i = 0; i < _dateContents.Count; i++)
            {
                distance[i] = Mathf.Abs(center.position.y - _dateContents[i].MyRectTransform.transform.position.y);

                var scale = Mathf.Min(1f, 1 / (1 + distance[i] / 300));
                _dateContents[i].MyRectTransform.transform.localScale = new Vector3(scale, scale, 1f);
            }

            minDistance = Mathf.Min(distance);
        
            for (var i = 0; i < _dateContents.Count; i++)
            {

                if (Mathf.Abs(minDistance - distance[i]) < centerTolerance)
                {
                    _centerIndex = i;

                    var isDateGreaterThanMinAllowed = DateTime.Compare(_dateContents[i].MyDate, _currentDate) != -1;
                    var isDateLesserThanMaxAllowed =
                        yearLimit <= 0 || DateTime.Compare(_currentDate.AddYears(yearLimit), _dateContents[i].MyDate) != -1;
                    
                    if (isDateGreaterThanMinAllowed && isDateLesserThanMaxAllowed)
                    {
                        _currentContentPositionY = _dateContents[i].AnchoredPosition.y;
                    }
                }
            
                _dateContents[i].MyCanvasGroup.interactable = i == _centerIndex;
                _dateContents[i].MyCanvasGroup.alpha = .5f + .5f * Convert.ToInt32(i == _centerIndex);
            }

            ScrollToSelected();
        }

        public void Initialize()
        {
            foreach (RectTransform child in scrollViewContent.transform)
            {
                var tempDateContent = child.GetComponent<ScrollDateContent>();

                if (tempDateContent != null)
                {
                    _dateContents.Add(tempDateContent);
                }
                else
                {
                    Destroy(child.gameObject);
                }
            }

            if (_dateContents.Count < 5)
            {
                Debug.LogError("No DateContent or only small amount of DateContents found in the list of prefabs");
                return;
            }
        
            if (_dateContents.Count % 2 == 0)
            {
                var tempDateContent = Instantiate(_dateContents[0], scrollViewContent, false).GetComponent<ScrollDateContent>();

                _dateContents.Add(tempDateContent);
            }
        
            Invoke(nameof(InitializeDelayed), .1f);
        
        }

        private void InitializeDelayed()
        {
            _centerIndex = (_dateContents.Count - 1) / 2;
            _offsetContentPositionY = _dateContents[_centerIndex].AnchoredPosition.y;
        
            InitializeDate();
            infiniteScrollModified.Init();

            _update = true;
        }


        private void InitializeDate()
        {
            _currentDate = DateTime.Now;
            var dateIncrement = _dateContents.Count / 2;

            for (var i = 0; i < _dateContents.Count; i++)
            {
                if (i < _centerIndex)
                {
                    _dateContents[i].MyDate = _currentDate.AddDays(-dateIncrement);
                    dateIncrement -= 1;
                }

                if (i == _centerIndex)
                {
                    _dateContents[i].MyDate = _currentDate;
                    dateIncrement = 0;
                }

                if (i > _centerIndex)
                {
                    dateIncrement += 1;
                    _dateContents[i].MyDate = _currentDate.AddDays(dateIncrement);
                }
            }
        }

        public void ScrollUp()
        {
            _dateContents.SetAsFirstAndRemove();
            _dateContents[0].MyDate = _dateContents[1].MyDate.AddDays(-1);
        }

        public void ScrollDown()
        {
            _dateContents.SetAsLastAndRemove();
            _dateContents[_dateContents.Count - 1].MyDate = _dateContents[_dateContents.Count - 2].MyDate.AddDays(1);
        }

        private float GetContentYPosition()
        {
            return _offsetContentPositionY - _currentContentPositionY;
        }

        private void ScrollToSelected()
        {
            var newY = Mathf.Lerp(scrollViewContent.anchoredPosition.y, GetContentYPosition(),
                Time.deltaTime * lerpSpeed);
            var newPosition = new Vector2(scrollViewContent.anchoredPosition.x, newY);
        
            scrollViewContent.anchoredPosition = newPosition;
        }
    }
}
