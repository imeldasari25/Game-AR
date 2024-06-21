using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoalManager : MonoBehaviour
{
    public static SoalManager Instance;

    public LevelState CurrentLevelState;

    [InlineEditor]
    public SoalDatabaseSO Soal;

    [ReadOnly]
    public int activeSoalIndex;
    [ReadOnly]
    public SoalPilihanGandaSO activeSoalSO;
    [ReadOnly, ShowInInspector]
    private float timerInSecond;
    [ReadOnly, ShowInInspector]
    private int minutes;
    [ReadOnly, ShowInInspector]
    private int seconds;
    [ReadOnly, ShowInInspector]
    private int jumlahBintang;

    [Title("UI Reference")]
    public TextMeshProUGUI soalTxt;
    public Image soalImg;
    public List<TextMeshProUGUI> jawabanTxt;
    public List<Image> jawabanImg;
    public GameObject winPanel;
    public List<Image> bintangImg;
    public TextMeshProUGUI timerLeftTxt;
    public GameObject losePanel;
    public TextMeshProUGUI levelTxt;
    public TextMeshProUGUI timerTxt;
    public GameObject pausePanel;

    public const float DEFAULT_TIMER = 180f;

    string _timerString;

    public void Awake()
    {
        Instance = this; 
    }

    private void Start()
    {
        // Mengambil index yang tersimpan di PlayerPrefs
        int index = PlayerPrefs.GetInt("Index Soal");

        UpdateSoal(index);
    }

    private void Update()
    {
        if (CurrentLevelState != LevelState.Mikir)
            return;

        if (timerInSecond > 0)
        {
            timerInSecond -= Time.deltaTime;
            minutes = Mathf.FloorToInt(timerInSecond / 60f);
            seconds = Mathf.FloorToInt(timerInSecond % 60f);
            _timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

            timerTxt.text = _timerString;
        }
        else
        {
            CurrentLevelState = LevelState.UdahJawab;

            losePanel.SetActive(true);
        }
    }

    [Button]
    [PropertySpace(10)]
    public void UpdateSoal(int indexSoal)
    {
        ResetLevelState();

        activeSoalIndex = indexSoal;
        activeSoalSO = Soal.SemuaSoal[activeSoalIndex];

        levelTxt.text = "Level " + (activeSoalIndex + 1);

        #region Pengecekan_Soal_Text
        // Jika ada soal text
        if (activeSoalSO.soalText != "")
        {
            soalTxt.gameObject.SetActive(true);
            // Tulis di UI TextMeshPro
            soalTxt.text = activeSoalSO.soalText;
        }
        else // Kalo gk ada
        {
            soalTxt.gameObject.SetActive(false);
            // Kosongin
            soalTxt.text = "";
        }
        #endregion

        #region Pengecekan_Soal_Gambar
        // Jika ada gambar
        if (activeSoalSO.soalGambar != null)
        {
            soalImg.gameObject.SetActive(true);
            soalImg.sprite = activeSoalSO.soalGambar;
        }
        else // Kalo gk ada gambar
        {
            soalImg.gameObject.SetActive(false);
            soalImg.sprite = null;
        }
        #endregion

        #region Pengecekan_Jawaban
        for (int i = 0; i < activeSoalSO.ListJawaban.Count; i++)
        {
            // Kalo ada text
            if (activeSoalSO.ListJawaban[i].text != "")
            {
                jawabanTxt[i].text = activeSoalSO.ListJawaban[i].text;
            }
            // Kalo ada gambar
            else if (activeSoalSO.ListJawaban[i].gambar != null) 
            {
                jawabanImg[i].sprite = activeSoalSO.ListJawaban[i].gambar;
            }         
        }
        #endregion
    }

    public void ResetLevelState()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        timerInSecond = DEFAULT_TIMER;
        CurrentLevelState = LevelState.Mikir;
        jumlahBintang = 0;

        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    /// <summary>
    /// Ini di panggil oleh JawabanButton.cs
    /// </summary>
    /// <param name="indexJawaban"></param>
    public void AnswerSoal(int indexJawaban)
    {
        CurrentLevelState = LevelState.UdahJawab;

        if(indexJawaban == activeSoalSO.indexJawabanBenar)
        {
            CountStar();
            winPanel.SetActive(true);
        }
        else
        {
            losePanel.SetActive(true);
        }
    }

    public void CountStar()
    {
        timerLeftTxt.text = _timerString;

        int maxBintang = 3;
        float timePerStar = DEFAULT_TIMER / maxBintang;

        jumlahBintang = 1;

        for (int i = 0; i < maxBintang; i++)
        {
            timerInSecond -= timePerStar;
            
            if(timerInSecond >= 0)
            {
                jumlahBintang++;
            }
            else
            {
                break;
            }
        }

        foreach(var bintang in bintangImg)
        {
            bintang.color = Color.grey;
        }

        for(int i = 0; i < jumlahBintang; i++)
        {
            bintangImg[i].color = Color.white;
        }

        Debug.Log("DAPET " + jumlahBintang + " Bintang");
    }

    #region CALL_BY_UI_BUTTON
    public void OnClick_BackToMenuBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Home");
    }

    public void OnClick_NextBtn()
    {        
        activeSoalIndex++;

        if(activeSoalIndex > Soal.SemuaSoal.Count - 1)
        {
            Debug.Log("SOAL UDAH ABIS");

            activeSoalIndex = Soal.SemuaSoal.Count - 1;
        }

        UpdateSoal(activeSoalIndex);
    }

    public void OnClick_RestartBtn()
    {
        UpdateSoal(activeSoalIndex);
    }

    public void OnClick_PauseBtn()
    {
        CurrentLevelState = LevelState.Pause;
        pausePanel.SetActive(true);
        //Time.timeScale = 0;
    }

    public void OnClick_ResumeBtn()
    {
        CurrentLevelState = LevelState.Mikir;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    #endregion
}

public enum LevelState
{
    Mikir, UdahJawab, Pause
}