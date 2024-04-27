using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        screenWidth = Screen.width;

        TrackedCardCount = 0;
    }

    public void AssignCard(Transform card, float xCoord)
    {        
        if(TrackedCardCount == 1)
        {
            // Kartu berada di sebelah Kiri Layar
            if (xCoord < screenWidth / 2)
            {
                Card_1 = card;
                Card_2 = null;
            }
            // Kartu berada di sebelah Kanan Layar
            else
            {
                Card_1 = null;
                Card_2 = card;
            }
        }
        else if(TrackedCardCount == 2)
        {
            // Kartu berada di sebelah Kiri Layar
            if (xCoord < screenWidth / 2)
            {
                Card_1 = card;               
            }
            // Kartu berada di sebelah Kanan Layar
            else
            {
                Card_2 = card;
            }
        }
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
    }
}
