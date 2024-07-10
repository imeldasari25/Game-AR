using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New Database", menuName = "Quiz/Soal Bank")]
public class SoalDatabaseSO : ScriptableObject
{
    public List<SoalPilihanGandaSO> SemuaSoal;
}
