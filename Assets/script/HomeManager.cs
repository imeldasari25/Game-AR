using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    #region Menu Quiz Variable
    [BoxGroup("Quiz")]
    public SoalDatabaseSO SoalBank;

    [BoxGroup("Quiz")]
    public GameObject levelBtnPrefab;

    [BoxGroup("Quiz")]
    public Transform levelBtnParent;
    #endregion

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        SpawnAllLevelButtons();

        ChangeCurrentTab(Tab.Home);
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

    public void SpawnAllLevelButtons()
    {
        if (SoalBank.SemuaSoal.Count == 0)
            return;

        int i = 0;

        foreach(var soal in SoalBank.SemuaSoal)
        {
            GameObject btnTemp = Instantiate(levelBtnPrefab, levelBtnParent);

            btnTemp.GetComponent<LevelButton>().SetIndex(i);

            i++;
        }
    }

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
    #endregion
}


public enum Tab
{
    Home, Quiz, Leaderboard, Setting
}