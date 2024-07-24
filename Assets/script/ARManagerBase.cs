using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class ARManagerBase : MonoBehaviour
{
    /// <summary>
    /// Kartu yang ada di sebelah Kiri Layar
    /// </summary>
    public Transform Card_1
    {
        get
        {
            if (_trackedCard.Count > 0)
            {
                return _trackedCard[0].transform;
            }
            else
                return null;
        }
    }

    /// <summary>
    /// Kartu yang ada di sebelah Kanan Layar
    /// </summary>
    public Transform Card_2
    {
        get
        {
            if (_trackedCard.Count > 1)
            {
                return _trackedCard[1].transform;
            }
            else
                return null;
        }
    }

    public float screenWidth;
    public float screenHeight;

    [Space(10)]
    public int number_A;
    public int number_B;
    public int result;
    public GameObject digit_1_Obj;
    public GameObject digit_2_Obj;

    [Space(10)]
    public Transform mathOPIcon;
    public GameObject equalIcon;

    public Transform digit_1_Parent;
    public Transform digit_2_Parent;
    public Transform resultDigitParent;

    public float resultOffset;

    [ListDrawerSettings(ShowIndexLabels = true)]
    public List<GameObject> angkaPrefab;

    [SerializeField, ReadOnly]
    protected List<TrackableNumberObject> _trackedCard;

    /// <summary>
    /// Ini adalah PROPERTY
    /// </summary>
    public int TrackedCardCount
    {
        get => _trackedCard.Count;
    }

    protected virtual void Start()
    {
        _trackedCard = new List<TrackableNumberObject>();

        Screen.orientation = ScreenOrientation.LandscapeLeft;

#if UNITY_EDITOR
        screenWidth = Screen.width;
#endif

#if PLATFORM_ANDROID
        screenWidth = Screen.height;
#endif

        Debug.Log($"Width = {screenWidth}, Height = {screenHeight}");

        try
        {
            mathOPIcon.gameObject.SetActive(false);
        }
        catch { }

        try
        {
            equalIcon.SetActive(false);
        }
        catch { }
    }

    protected virtual void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            OnClick_BackBtn();
            return;
        }

        UpdateAll();
    }

    public void AddCard(TrackableNumberObject card)
    {
        _trackedCard.Add(card);

        if(_trackedCard.Count > 2)
        {
            _trackedCard.RemoveAt(2);
        }

        HandleCardCountChange();
    }

    public void RemoveCard(TrackableNumberObject card)
    {
        _trackedCard.Remove(card);

        HandleCardCountChange();
    }

    public virtual void HandleCardCountChange()
    {
        if (TrackedCardCount == 2)
        {
            PlaceMathOPIcon();
            PlaceResultNumber();
        }
        else
        {
            Destroy(digit_1_Obj);
            Destroy(digit_2_Obj);

            try
            {
                mathOPIcon.gameObject.SetActive(false);
            }
            catch { }

            try
            {
                equalIcon.gameObject.SetActive(false);
            }
            catch { }
        }
    }

    public void UpdateAll()
    {
        try
        {
            number_A = _trackedCard[0].number;
        }
        catch { }

        try
        {
            number_B = _trackedCard[1].number;
        }
        catch { }

        try
        {
            mathOPIcon.gameObject.SetActive(TrackedCardCount == 2);
            equalIcon.gameObject.SetActive(TrackedCardCount == 2);
        }
        catch { }

        if (TrackedCardCount < 2)
            return;

        if (_trackedCard[1].PosInScreenX < _trackedCard[0].PosInScreenX)
        {
            TrackableNumberObject cardTemp = _trackedCard[0];

            _trackedCard.RemoveAt(0);
            _trackedCard.Add(cardTemp);
        }

        PlaceMathOPIcon();
        PlaceResultNumber();
    }

    public void PlaceMathOPIcon()
    {
        try
        {
            Vector3 midpointPosition = (Card_1.position + Card_2.position) / 2f;
            mathOPIcon.transform.position = midpointPosition;

            float averageRot = (Card_1.eulerAngles.x + Card_2.eulerAngles.x) / 2f;

            mathOPIcon.transform.eulerAngles = 
                new Vector3(averageRot, mathOPIcon.transform.eulerAngles.y, mathOPIcon.transform.eulerAngles.z);
        }
        catch { }
    }

    public virtual void PlaceResultNumber() { }

    /// <summary>
    /// Ini di panggil di Tombol Back
    /// </summary>
    public void OnClick_BackBtn()
    {
        SceneManager.LoadScene("Home");
    }

    protected int[] ExtractDigitsFromNumber(int number)
    {
        // Convert the number to a string
        string numberString = number.ToString();

        // Create an array to hold the digits
        int[] digits = new int[numberString.Length];

        // Convert each character back to an integer and store it in the array
        for (int i = 0; i < numberString.Length; i++)
        {
            digits[i] = int.Parse(numberString[i].ToString());
        }

        return digits;
    }

    //void OnGUI()
    //{
    //    // Create a new GUIStyle
    //    GUIStyle style = new GUIStyle();

    //    // Set the font size
    //    style.fontSize = 48;

    //    // Optionally, set other style properties like color
    //    style.normal.textColor = Color.white;

    //    // Define the position and size of the label
    //    Rect rect = new Rect(10, 10, 300, 40);

    //    // Display the variable in a label with the custom style
    //    GUI.Label(rect, "Variable Value: " + TrackedCardCount, style);
    //}
}
