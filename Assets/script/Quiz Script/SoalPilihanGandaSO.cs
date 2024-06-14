using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "PilGan Baru", menuName = "Quiz/Soal Pilihan Ganda")]
public class SoalPilihanGandaSO : ScriptableObject
{
    [Title("Soal")]
    [TextArea]
    public string soalText;

    [PreviewField(256)]
    public Sprite soalGambar;

    [Title("Jawaban")]
    [TableList(ShowIndexLabels = true)]
    public List<Jawaban> ListJawaban;

    [Range(0, 3)]
    public int indexJawabanBenar;

    [Title("Tips")]
    [TextArea]
    public string tipsText;

    [Title("Runtime")]
    public int JumlahBintang;

    /// <summary>
    /// Di panggil oleh manager saat jawaban benar
    /// </summary>
    /// <param name="jumlahBintang"></param>
    public void SetBintang(int jumlahBintang)
    {
        JumlahBintang = jumlahBintang;
    }
}

[System.Serializable]
public class Jawaban
{
    public string text;
    [PreviewField(72)]
    public Sprite gambar;       
}
