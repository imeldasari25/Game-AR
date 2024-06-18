using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JawabanButton : MonoBehaviour
{
    /// <summary>
    /// Di panggil di Inspector Button Unity
    /// </summary>
    /// <param name="indexJawaban"></param>
    public void OnClick_JawbanBtn(int indexJawaban)
    {
        SoalManager.Instance.AnswerSoal(indexJawaban);
    }
}
