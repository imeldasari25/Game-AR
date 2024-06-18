using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Database", menuName = "Quiz/Soal Bank")]
public class SoalDatabaseSO : ScriptableObject
{
    public List<SoalPilihanGandaSO> SemuaSoal;
}
