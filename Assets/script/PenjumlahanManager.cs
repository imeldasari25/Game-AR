using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject digit_1;
    public GameObject digit_2;   

    [Space(10)]
    public Transform plusIcon;
    public Transform digit_1_Parent;
    public Transform digit_2_Parent;
    public Transform resultDigitParent;

    [ListDrawerSettings(ShowIndexLabels = true)]
    public List<GameObject> angkaPrefab;

    private void Awake()
    {
        Instance = this;
    }

    #region Subscription
    private void OnEnable()
    {
    }

    private void OnDestroy()
    {        
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        screenWidth = Screen.width;

        Screen.orientation = ScreenOrientation.LandscapeRight;

        TrackedCardCount = 0;
        plusIcon.gameObject.SetActive(false);
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
            PlacePlusIcon();
            PlaceResultNumber();
        }

        plusIcon.gameObject.SetActive(TrackedCardCount == 2);
    }

    public void PlacePlusIcon()
    {
        try
        {
            Vector3 midpointPosition = (Card_1.position + Card_2.position) / 2f;
            plusIcon.transform.position = midpointPosition;
        }
        catch { }
    }

    public void PlaceResultNumber()
    {
        result = number_A + number_B;

        // Step 2: Extract each digit of the sum and store them in separate variables
        int digit1 = result % 10;         // Extract the ones place digit
        int digit2 = (result / 10) % 10;  // Extract the tens place digit

        if (digit_1 != null)
            Destroy(digit_1);
        if (digit_2 != null)
            Destroy(digit_2);

        digit_1 = Instantiate(angkaPrefab[digit1], digit_1_Parent);
        digit_2 = Instantiate(angkaPrefab[digit2], digit_2_Parent);

        try
        {
            Vector3 midpointPosition = (Card_1.position + Card_2.position) / 2f;

            Vector3 midpointOffset = midpointPosition + Vector3.up * 1.75f;

            resultDigitParent.transform.position = midpointOffset;
        }
        catch { }
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

        if(TrackedCardCount != 2)
        {
            plusIcon.gameObject.SetActive(false);

            Destroy(digit_1);
            Destroy(digit_2);
        }

    }

    /// <summary>
    /// Ini di panggil di Tombol Back
    /// </summary>
    public void OnClick_BackBtn()
    {
        SceneManager.LoadScene("Home");
    }
}
