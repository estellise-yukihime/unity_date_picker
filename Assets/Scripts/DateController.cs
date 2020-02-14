using Boo.Lang;
using UnityEngine;

[RequireComponent(typeof(UI_InfiniteScrollModified))]
public class DateController : MonoBehaviour
{
    public UI_InfiniteScrollModified infiniteScrollModified;
    public RectTransform scrollViewContent;
    
    public float centerTolerance = .03f; 
    public int yearLimit;
    public bool shouldInit;

    private List<DateContent> _dateContents;
    private float _centerPosition;
    
    void Awake()
    {
        if (shouldInit)
        {
            Initialize();
        }
    }

    public void Initialize()
    {

        foreach (RectTransform child in scrollViewContent.transform)
        {
            var tempDateContent = child.GetComponent<DateContent>();

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
            var tempDateContent = Instantiate(_dateContents[0], scrollViewContent, false).GetComponent<DateContent>();

            _dateContents.Add(tempDateContent);
        }

        

        infiniteScrollModified.Init();
    }

    private void InitializeDate()
    {
        
    }
}
