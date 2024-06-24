using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    [ReadOnly]
    public int index;

    public TextMeshProUGUI levelTxt;

    public void SetIndex(int index)
    {
        this.index = index;

        SetUI();
    }

    public void SetUI()
    {
        levelTxt.text = (index + 1).ToString();
    }

    public void OnClick()
    {
        PlayerPrefs.SetInt("Index Soal", index);

        HomeManager.Instance.PlaySfx();

        SceneManager.LoadScene("Quiz - Development");
    }
}
