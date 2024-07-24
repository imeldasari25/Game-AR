using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PenguranganManager : ARManagerBase
{
    //Singleton
    public static PenguranganManager Instance;

    public GameObject negativeResultNotification;

    public bool ShowNegativeNotif
    {
        get => _showNegNotif;
        set
        {
            if (value == _showNegNotif)
                return;
            else
                _showNegNotif = value;

            negativeResultNotification.SetActive(_showNegNotif);
        }
    }

    bool _showNegNotif;

    private void Awake()
    {
        Instance = this;
    }

    protected override void Start()
    {
        base.Start();

        ShowNegativeNotif = false;
        negativeResultNotification.SetActive(ShowNegativeNotif);
    }

    public override void PlaceResultNumber()
    {
        #region Clear_Digit_Obj
        if (digit_1_Obj != null)
            Destroy(digit_1_Obj);
        if (digit_2_Obj != null)
            Destroy(digit_2_Obj);
        #endregion

        result = number_A - number_B;
        Debug.Log($"{number_A} + {number_B} = {result}");

        if (result < 0)
        {
            ShowNegativeNotif = true;
            return;
        }
        else
        {
            ShowNegativeNotif = false;
        }

        int[] resultDigit = ExtractDigitsFromNumber(result);      

        digit_1_Obj = Instantiate(angkaPrefab[resultDigit[0]], digit_1_Parent);

        if (result.ToString().Length > 1)
            digit_2_Obj = Instantiate(angkaPrefab[resultDigit[1]], digit_2_Parent);

        try
        {
            // OTAK ATIK Vector3.right nya
            resultDigitParent.transform.position = Card_2.position + Vector3.right * resultOffset;
        }
        catch { }
    }
}
