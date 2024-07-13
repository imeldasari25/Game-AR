using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    public static HomeManager Instance;

    public Tab CurrentTab;

    [BoxGroup("Tabs")]
    public GameObject homeTab;
    [BoxGroup("Tabs")]
    public GameObject quizTab;
    [BoxGroup("Tabs")]
    public GameObject leaderboardTab;
    [BoxGroup("Tabs")]
    public GameObject settingTab;

    [BoxGroup("Toggle")]
    public Toggle homeToggle;
    [BoxGroup("Toggle")]
    public Toggle quizToggle;
    [BoxGroup("Toggle")]
    public Toggle leaderboardToggle;
    [BoxGroup("Toggle")]
    public Toggle settingToggle;

    #region Download Marker Variable
    [BoxGroup("Download Marker")]
    public string downloadURL;
    #endregion

    #region Menu Quiz Variable
    //[BoxGroup("Quiz")]
    //public SoalDatabaseSO SoalBank;

    [BoxGroup("Quiz")]
    public GameObject levelBtnPrefab;

    [BoxGroup("Quiz")]
    public Transform levelBtnParent;
    #endregion

    #region Setting
    [BoxGroup("Setting")]
    public Toggle musicToggle;

    [BoxGroup("Setting")]
    public Toggle sfxToggle;

    [BoxGroup("Setting")]
    public Sprite toggleOn;

    [BoxGroup("Setting")]
    public Sprite toggleOff;

    [BoxGroup("Setting")]
    public AudioMixer audioMixer;
    #endregion

    [BoxGroup("SFX")]
    public AudioClip clickSfx;
    [BoxGroup("SFX")]
    public AudioSource sfxAudioSource;

    [BoxGroup("Player Data")]
    [InfoBox("0,1,2 untuk Penjumalahan \n 3,4,5 untuk Pengurangan")]
    public List<int> playerCurrentLevel;

    public const string PENJUMLAHAN_MUDAH = "Penjumlahan_Mudah";
    public const string PENJUMLAHAN_SEDANG = "Penjumlahan_Sedang";
    public const string PENJUMLAHAN_SULIT = "Penjumlahan_Sulit";

    public const string PENGURANGAN_MUDAH = "Pengurangan_Mudah";
    public const string PENGURANGAN_SEDANG = "Pengurangan_Sedang";
    public const string PENGURANGAN_SULIT = "Pengurangan_Sulit";

    public const string SELECTED_DIFFICULTY = "SelectedDifficulty";

    private void Awake()
    {
        Instance = this;

        playerCurrentLevel = new List<int>();
        for (int i = 0; i < 6; i++)
        {
            playerCurrentLevel.Add(0);
        }

        LoadPlayerData();
    }

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        //SpawnAllLevelButtons();

        ChangeCurrentTab(Tab.Home);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
            return;
        }
    }

    void LoadPlayerData()
    {
        #region Penjumlahan
        if (PlayerPrefs.HasKey(PENJUMLAHAN_MUDAH))
        {
            playerCurrentLevel[0] = PlayerPrefs.GetInt(PENJUMLAHAN_MUDAH);
        }
        else
        {
            PlayerPrefs.SetInt(PENJUMLAHAN_MUDAH, 0);
        }

        if (PlayerPrefs.HasKey(PENJUMLAHAN_SEDANG))
        {
            playerCurrentLevel[1] = PlayerPrefs.GetInt(PENJUMLAHAN_SEDANG);
        }
        else
        {
            PlayerPrefs.SetInt(PENJUMLAHAN_SEDANG, 0);
        }

        if (PlayerPrefs.HasKey(PENJUMLAHAN_SULIT))
        {
            playerCurrentLevel[2] = PlayerPrefs.GetInt(PENJUMLAHAN_SULIT);
        }
        else
        {
            PlayerPrefs.SetInt(PENJUMLAHAN_SULIT, 0);
        }
        #endregion

        #region Pengurangan
        if (PlayerPrefs.HasKey(PENGURANGAN_MUDAH))
        {
            playerCurrentLevel[3] = PlayerPrefs.GetInt(PENGURANGAN_MUDAH);
        }
        else
        {
            PlayerPrefs.SetInt(PENGURANGAN_MUDAH, 0);
        }

        if (PlayerPrefs.HasKey(PENGURANGAN_SEDANG))
        {
            playerCurrentLevel[4] = PlayerPrefs.GetInt(PENGURANGAN_SEDANG);
        }
        else
        {
            PlayerPrefs.SetInt(PENGURANGAN_SEDANG, 0);
        }

        if (PlayerPrefs.HasKey(PENGURANGAN_SULIT))
        {
            playerCurrentLevel[5] = PlayerPrefs.GetInt(PENGURANGAN_SULIT);
        }
        else
        {
            PlayerPrefs.SetInt(PENGURANGAN_SULIT, 0);
        }
        #endregion
    }

    public void ChangeCurrentTab(Tab newTab)
    {
        CurrentTab = newTab;

        switch (CurrentTab)
        {
            case Tab.Home:                
                break;
            case Tab.Quiz:
                break;
            case Tab.Leaderboard:
                break;
            case Tab.Setting:
                break;
        }

        homeTab.SetActive(CurrentTab == Tab.Home);
        quizTab.SetActive(CurrentTab == Tab.Quiz);
        leaderboardTab.SetActive(CurrentTab == Tab.Leaderboard);
        settingTab.SetActive(CurrentTab == Tab.Setting);

        homeToggle.isOn = CurrentTab == Tab.Home;
        quizToggle.isOn = CurrentTab == Tab.Quiz;
        leaderboardToggle.isOn = CurrentTab == Tab.Leaderboard;
        settingToggle.isOn = CurrentTab == Tab.Setting;
    }

    //public void SpawnAllLevelButtons()
    //{
    //    if (SoalBank.SemuaSoal.Count == 0)
    //        return;

    //    int i = 0;

    //    foreach(var soal in SoalBank.SemuaSoal)
    //    {
    //        GameObject btnTemp = Instantiate(levelBtnPrefab, levelBtnParent);

    //        btnTemp.GetComponent<LevelButton>().SetIndex(i);

    //        i++;
    //    }
    //}

    #region AR
    public void SwitchPengenalanAngka()
    {
        SceneManager.LoadScene("AR - PengenalanAngka");
    }

    public void SwitchPengurangan()
    {
        SceneManager.LoadScene("AR - Pengurangan");
    }

    public void SwitchPenjumlahan()
    {
        SceneManager.LoadScene("AR - Penjumlahan");
    }
    #endregion

    #region CALL_BY_BUTTON_UI
    public void PlaySfx()
    {
        sfxAudioSource.PlayOneShot(clickSfx);
    }

    public void OnClick_HomeBtn()
    {
        ChangeCurrentTab(Tab.Home);
    }

    public void OnClick_QuizBtn()
    {
        ChangeCurrentTab(Tab.Quiz);
    }

    public void OnClick_LeaderboardBtn()
    {
        ChangeCurrentTab(Tab.Leaderboard);
    }

    public void OnClick_SettingBtn()
    {
        ChangeCurrentTab(Tab.Setting);
    }

    public void OnClick_ExitBtn()
    {
        Application.Quit();
    }

    public void OnClick_MusicToggle()
    {
        musicToggle.GetComponentInChildren<Image>().sprite = 
            musicToggle.isOn ? toggleOn : toggleOff;

        audioMixer.SetFloat("MusicVol", musicToggle.isOn ? 0 : -80);
    }

    public void OnClick_SfxToggle()
    {
        if(sfxToggle.isOn)
            sfxToggle.GetComponentInChildren<Image>().sprite = toggleOn;
        else
            sfxToggle.GetComponentInChildren<Image>().sprite = toggleOff;

        audioMixer.SetFloat("SfxVol", sfxToggle.isOn ? 0 : -80);
    }

    public void OnClick_ToGDrive()
    {
        Application.OpenURL(downloadURL);

        PlaySfx();
    }

    //public void OnClick_PlayQuizBtn()
    //{
    //    PlaySfx();

    //    SceneManager.LoadScene("Quiz - Development");
    //}

    public void OnClick_QuizDifficulty(int difficulty)
    {
        PlayerPrefs.SetInt(SELECTED_DIFFICULTY, difficulty);

        PlaySfx();
        SceneManager.LoadScene("Quiz - Development");
    }
    #endregion
}


public enum Tab
{
    Home, Quiz, Leaderboard, Setting
}