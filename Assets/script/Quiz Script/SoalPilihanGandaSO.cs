using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "PilGan Baru", menuName = "Quiz/Soal Pilihan Ganda")]
public class SoalPilihanGandaSO : ScriptableObject
{
    [TextArea]
    public string soal;

    public Sprite gambar;

    public List<Jawaban> ListJawaban;

    public int indexJawabanBenar;
}

[System.Serializable]
public class Jawaban
{
    public Sprite jawabanGambar;
    public string jawaban;    
}
