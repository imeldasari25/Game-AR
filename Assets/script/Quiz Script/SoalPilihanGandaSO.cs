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
}

[System.Serializable]
public class Jawaban
{
    public string text;
    [PreviewField(72)]
    public Sprite gambar;       
}
