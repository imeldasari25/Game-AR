using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PengenalanAngkaManager : MonoBehaviour
{
    public static PengenalanAngkaManager Instance;

    public Transform Card_1;
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

        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            OnClick_BackBtn();
            return;
        }
    }

    /// <summary>
    /// KENANG-KENANG an
    /// </summary>
    /// <param name="card"></param>
    /// <param name="xCoord"></param>
    public void AssignCard(Transform card, float xCoord)
    {
        if (xCoord < screenWidth / 2)
        {
            Card_1 = card;
            Card_2 = null;
        }           
        else
        {
            Card_1 = null;
            Card_2 = card;
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
