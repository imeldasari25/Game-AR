using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class ARManagerBase : MonoBehaviour
{
    [SerializeField]
    protected int _trackedCardCount;

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
    public float screenHeight;

    [Space(10)]
    public int number_A;
    public int number_B;
    public int result;
    public GameObject digit_1;
    public GameObject digit_2;

    [Space(10)]
    public Transform mathOPIcon;
    public Transform digit_1_Parent;
    public Transform digit_2_Parent;
    public Transform resultDigitParent;

    [Range(0, 2)]
    public float resultOffset;

    [ListDrawerSettings(ShowIndexLabels = true)]
    public List<GameObject> angkaPrefab;

    protected virtual void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;

#if UNITY_EDITOR
        screenWidth = Screen.width;
#endif

#if PLATFORM_ANDROID
        screenWidth = Screen.height;
#endif

        Debug.Log($"Width = {screenWidth}, Height = {screenHeight}");

        TrackedCardCount = 0;
        mathOPIcon.gameObject.SetActive(false);
    }

    protected virtual void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            OnClick_BackBtn();
            return;
        }
    }

    public void AssignCard(Transform card, float xCoord)
    {
        if (TrackedCardCount == 1)
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
        else if (TrackedCardCount == 2)
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
            PlaceMathOPIcon();
            PlaceResultNumber();
        }

        mathOPIcon.gameObject.SetActive(TrackedCardCount == 2);
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

    public abstract void PlaceResultNumber();

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
    public virtual void RemoveCardCounter()
    {
        TrackedCardCount--;

        if (TrackedCardCount != 2)
        {
            mathOPIcon.gameObject.SetActive(false);

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
