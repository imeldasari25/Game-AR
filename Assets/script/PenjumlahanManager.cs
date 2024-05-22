using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PenjumlahanManager : MonoBehaviour
{
    public static PenjumlahanManager Instance;

    [SerializeField]
    private int _trackedCardCount;

    /// <summary>
    /// Ini adalah PROPERTY
    /// </summary>
    public int TrackedCardCount
    {
        //Di gunakan saat property digunakan Value-nya
        get
        {
            return _trackedCardCount;
        }
        set
        {
            _trackedCardCount = value;           

            if (_trackedCardCount < 0)
            {
                _trackedCardCount = 0;
            }

            if (_trackedCardCount == 0)
            {
                Card_1 = null;
                Card_2 = null;
            }
        }
    }

    /// <summary>
    /// Kartu yang ada di sebelah Kiri Layar
    /// </summary>
    public Transform Card_1;

    /// <summary>
    /// Kartu yang ada di sebelah Kanan Layar
    /// </summary>
    public Transform Card_2;

    public float screenWidth;

    [Space(10)]
    public int number_A;
    public int number_B;
    public int result;

    [Space(10)]
    public GameObject calcButton;
    public TextMeshProUGUI resultText;


    private void Awake()
    {
        Instance = this;
    }

    #region Subscription
    private void OnEnable()
    {
        NumberAnimator.OnNumberAnimationDone += OnNumberAnimDoneHandler;
    }

    private void OnDestroy()
    {
        NumberAnimator.OnNumberAnimationDone -= OnNumberAnimDoneHandler;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        screenWidth = Screen.width;

        TrackedCardCount = 0;
        calcButton.SetActive(false);
        resultText.gameObject.SetActive(false);
    }

    public void AssignCard(Transform card, float xCoord)
    {        
        if(TrackedCardCount == 1)
        {
            // Kartu berada di sebelah Kiri Layar
            if (xCoord < screenWidth / 2)
            {
                Card_1 = card;
                number_A = Card_1.GetComponent<TrackableNumberObject>().number;

                Card_2 = null;
                number_B = 0;
            }
            // Kartu berada di sebelah Kanan Layar
            else
            {
                Card_1 = null;
                number_A = 0;

                Card_2 = card;
                number_B = Card_2.GetComponent<TrackableNumberObject>().number;
            }

            calcButton.SetActive(false);
        }
        else if(TrackedCardCount == 2)
        {
            // Kartu berada di sebelah Kiri Layar
            if (xCoord < screenWidth / 2)
            {
                Card_1 = card;
                number_A = Card_1.GetComponent<TrackableNumberObject>().number;
            }
            // Kartu berada di sebelah Kanan Layar
            else
            {
                Card_2 = card;
                number_B = Card_2.GetComponent<TrackableNumberObject>().number;
            }

            calcButton.SetActive(true);
        }
    }

    public void OnClick_Result()
    {
        Calculate_Sum();

        Card_1?.GetComponent<NumberAnimator>().StartGoTo();
        Card_2?.GetComponent<NumberAnimator>().StartGoTo();
    }

    public void Calculate_Sum()
    {
        result = number_A + number_B;
    }

    /// <summary>
    /// Menambahkan 1 Card Count 
    /// 
    /// Dipanggil didalam Event Vuforia didalam Inspector
    /// </summary>
    public void AddCardCounter()
    {
        TrackedCardCount++;
    }

    /// <summary>
    /// Mengurangi 1 Card Count 
    /// 
    /// Dipanggil didalam Event Vuforia didalam Inspector
    /// </summary>
    public void RemoveCardCounter()
    {
        TrackedCardCount--;

        if( TrackedCardCount == 0 )
        {
            calcButton.SetActive(false);
        }
    }

    private void OnNumberAnimDoneHandler(GameObject numberObj)
    {
        resultText.gameObject.SetActive(true);

        resultText.text = result.ToString();
    }
}
