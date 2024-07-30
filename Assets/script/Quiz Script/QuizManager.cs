using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json;

public class QuizManager : MonoBehaviour
{
    public static QuizManager Instance;

    public LevelState CurrentLevelState;

    [ListDrawerSettings(ShowIndexLabels = true)]
    public List<SoalDatabaseSO> SoalBank;

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
    public TextMeshProUGUI correctQuizTxt;
    public GameObject losePanel;
    public TextMeshProUGUI levelTxt;
    public TextMeshProUGUI timerTxt;
    public GameObject pausePanel;
    public GameObject correctPanel;
    public GameObject incorrectPanel;

    [BoxGroup("SFX")]
    public AudioClip correctSfx;
    [BoxGroup("SFX")]
    public AudioClip wrongSfx;
    [BoxGroup("SFX")]
    public AudioClip clickSfx;
    [BoxGroup("SFX")]
    public AudioSource sfxAudioSource;

    [BoxGroup("Player Data")]
    public int selectedDifficulty;
    [BoxGroup("Player Data")]
    [ListDrawerSettings(ShowIndexLabels = true)]
    public List<int> playerCurrentLevel;
    [BoxGroup("Player Data")]
    [ListDrawerSettings(ShowIndexLabels = true)]
    public List<int> indexSoalRandom;
    [BoxGroup("Player Data")]
    [ListDrawerSettings(ShowIndexLabels = true)]
    public List<bool> questionResult;
    [BoxGroup("Player Data")]
    public int SoalBenar
    {
        get
        {
            int soalBenar = 0;
            for (int i = 0; i < questionResult.Count; i++)
            {
                if (questionResult[i] == true)
                {
                    soalBenar++;
                }
            }

            return soalBenar;
        }
    }

    public const float DEFAULT_TIMER = 180f;

    string _timerString;

    public void Awake()
    {
        Instance = this;

        playerCurrentLevel = new List<int>();
        for (int i = 0; i < 6; i++)
        {
            playerCurrentLevel.Add(0);
        }

        indexSoalRandom = new List<int>();
    }

    private void Start()
    {
        questionResult = new List<bool>();

        // Mengambil index yang tersimpan di PlayerPrefs
        LoadPlayerData();

        UpdateSoal(indexSoalRandom[playerCurrentLevel[selectedDifficulty]]);

        correctPanel.SetActive(false);
        incorrectPanel.SetActive(false);
    }

    void LoadPlayerData()
    {
        // Load Selected Difficulty
        if (PlayerPrefs.HasKey(HomeManager.SELECTED_DIFFICULTY))
        {
            selectedDifficulty = PlayerPrefs.GetInt(HomeManager.SELECTED_DIFFICULTY);
        }
        else
        {
            PlayerPrefs.SetInt(HomeManager.SELECTED_DIFFICULTY, 0);
        }

        #region RANDOM_SOAL
        indexSoalRandom = new List<int>();

        for (int i = 0; i < SoalBank[selectedDifficulty].SemuaSoal.Count; i++)
        {
            indexSoalRandom.Add(i);
        }

        RandomizeSoal();
        #endregion
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

        //playerCurrentLevel = indexSoal;
        activeSoalSO = SoalBank[selectedDifficulty].SemuaSoal[indexSoalRandom[playerCurrentLevel[selectedDifficulty]]];

        // LABEL Menyesuaikan Konten Soal
        levelTxt.text = (playerCurrentLevel[selectedDifficulty] + 1) + " / " + SoalBank[selectedDifficulty].SemuaSoal.Count;

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
        correctPanel.SetActive(false);
        incorrectPanel.SetActive(false);
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

        if (indexJawaban == activeSoalSO.indexJawabanBenar)
        {
            questionResult.Add(true);
            correctPanel.SetActive(true);
            PlaySfx(correctSfx);
        }
        else
        {
            questionResult.Add(false);
            incorrectPanel.SetActive(true);
            PlaySfx(wrongSfx);
        }

        StartCoroutine(NextSoalDelayCo(2f));
    }

    public void CountStar()
    {
        jumlahBintang = 0;

        correctQuizTxt.text = SoalBenar + " / " + SoalBank[selectedDifficulty].SemuaSoal.Count;

        // Benar minimal satu
        if(SoalBenar > 0)
        {
            jumlahBintang++;
        }

        // Benar lebih dari sama dengan 10
        if (SoalBenar >= SoalBank[selectedDifficulty].SemuaSoal.Count / 2)
        {
            jumlahBintang++;
        }

        // Benar 20 soal
        if (SoalBenar >= SoalBank[selectedDifficulty].SemuaSoal.Count)
        {
            jumlahBintang++;
        }

        foreach (var bintang in bintangImg)
        {
            bintang.color = Color.grey;
        }

        for (int i = 0; i < jumlahBintang; i++)
        {
            bintangImg[i].color = Color.white;
        }

        Debug.Log("DAPET " + jumlahBintang + " Bintang");
    }

    private void RandomizeSoal()
    {
        // Fisher-Yates shuffle algorithm
        for (int i = SoalBank[selectedDifficulty].SemuaSoal.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            int temp = indexSoalRandom[i];
            indexSoalRandom[i] = indexSoalRandom[randomIndex];
            indexSoalRandom[randomIndex] = temp;
        }
    }

    #region CALL_BY_UI_BUTTON
    public void OnClick_BackToMenuBtn()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Home");

        PlaySfx();
    }

    public void NextSoal()
    {
        playerCurrentLevel[selectedDifficulty]++;

        // JIKA SOAL UDAH HABIS
        if (playerCurrentLevel[selectedDifficulty] > SoalBank[selectedDifficulty].SemuaSoal.Count - 1)
        {
            CountStar();
            winPanel.SetActive(true);

            Debug.Log("SOAL UDAH ABIS");

            playerCurrentLevel[selectedDifficulty] = 0;
            RandomizeSoal();
        }
        //SavePlayerData();

        UpdateSoal(indexSoalRandom[playerCurrentLevel[selectedDifficulty]]);
    }

    IEnumerator NextSoalDelayCo(float delay)
    {
        yield return new WaitForSeconds(delay);

        NextSoal();
    }

    public void OnClick_RestartBtn()
    {
        playerCurrentLevel[selectedDifficulty] = 0;

        winPanel.SetActive(false);

        RandomizeSoal();

        UpdateSoal(indexSoalRandom[playerCurrentLevel[selectedDifficulty]]);

        PlaySfx();
    }

    public void OnClick_PauseBtn()
    {
        CurrentLevelState = LevelState.Pause;
        pausePanel.SetActive(true);
        //Time.timeScale = 0;

        PlaySfx();
    }

    public void OnClick_ResumeBtn()
    {
        CurrentLevelState = LevelState.Mikir;
        pausePanel.SetActive(false);
        Time.timeScale = 1;

        PlaySfx();
    }

    public void PlaySfx()
    {
        sfxAudioSource.PlayOneShot(clickSfx);
    }

    public void PlaySfx(AudioClip clip)
    {
        sfxAudioSource.PlayOneShot(clip);
    }
    #endregion
}

public enum LevelState
{
    Mikir, UdahJawab, Pause
}