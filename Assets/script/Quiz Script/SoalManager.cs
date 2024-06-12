using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoalManager : MonoBehaviour
{
    public static SoalManager Instance;

    [ListDrawerSettings(ShowIndexLabels = true)]
    public List<SoalPilihanGandaSO> Soal;

    [ReadOnly]
    public int activeSoalIndex;
    [ReadOnly]
    public SoalPilihanGandaSO activeSoalSO;

    [Title("UI Reference")]
    public TextMeshProUGUI soalTxt;
    public Image soalImg;
    public List<TextMeshProUGUI> jawabanTxt;
    public List<Image> jawabanImg;

    public void Awake()
    {
        Instance = this; 
    }

    private void Start()
    {
        UpdateSoal(0);
    }

    [Button]
    [PropertySpace(10)]
    public void UpdateSoal(int indexSoal)
    {
        activeSoalIndex = indexSoal;
        activeSoalSO = Soal[activeSoalIndex];

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

    /// <summary>
    /// Ini di panggil oleh JawabanButton.cs
    /// </summary>
    /// <param name="indexJawaban"></param>
    public void AnswerSoal(int indexJawaban)
    {
        if(indexJawaban == activeSoalSO.indexJawabanBenar)
        {
            Debug.Log("YEY JAWABAN BENAR");
        }
        else
        {
            Debug.Log("HUUUU, CUPU !!!");
        }
    }
}
