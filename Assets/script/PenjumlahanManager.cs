using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PenjumlahanManager : ARManagerBase
{
    public static PenjumlahanManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public override void PlaceResultNumber()
    {
        result = number_A + number_B;
        Debug.Log($"{number_A} + {number_B} = {result}");

        int[] resultDigit = ExtractDigitsFromNumber( result );

        #region Clear_Digit_Obj
        if (digit_1_Obj != null)
            Destroy(digit_1_Obj);
        if (digit_2_Obj != null)
            Destroy(digit_2_Obj);
        #endregion

        digit_1_Obj = Instantiate(angkaPrefab[resultDigit[0]], digit_1_Parent);

        if(result.ToString().Length > 1) 
            digit_2_Obj = Instantiate(angkaPrefab[resultDigit[1]], digit_2_Parent);

        try
        {
            // OTAK ATIK Vector3.right nya
            resultDigitParent.transform.position = Card_2.position + Vector3.right * resultOffset;
        }
        catch { }
    }

    //public override void HandleCardCountChange()
    //{
    //    base.HandleCardCountChange();
    //    equalIcon.SetActive(TrackedCardCount == 2);
    //}
}
