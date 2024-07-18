using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PenguranganManager : ARManagerBase
{
    //Singleton
    public static PenguranganManager Instance;

    public GameObject equalIcon;
    public GameObject minusIcon;

    private void Awake()
    {
        Instance = this;
    }

    protected override void Start()
    {
        base.Start();

        minusIcon.SetActive(false);
        equalIcon.SetActive(false);
    }

    public override void PlaceResultNumber()
    {
        result = number_A - number_B;

        int[] resultDigit = ExtractDigitsFromNumber(Mathf.Abs(result));

        if (digit_1_Obj != null)
            Destroy(digit_1_Obj);
        if (digit_2_Obj != null)
            Destroy(digit_2_Obj);

        minusIcon.SetActive(false);

        if (result < 0)
        {
            digit_1_Obj = Instantiate(angkaPrefab[resultDigit[0]], digit_1_Parent);

            if (result.ToString().Length > 2)
                digit_2_Obj = Instantiate(angkaPrefab[resultDigit[1]], digit_2_Parent);

            minusIcon.SetActive(true);
        }
        else
        {
            digit_1_Obj = Instantiate(angkaPrefab[resultDigit[0]], digit_1_Parent);

            if (result.ToString().Length > 1)
                digit_2_Obj = Instantiate(angkaPrefab[resultDigit[1]], digit_2_Parent);

            minusIcon.SetActive(false);
        }
        

        try
        {
            // OTAK ATIK Vector3.right nya
            resultDigitParent.transform.position = Card_2.position + Vector3.right * resultOffset;
        }
        catch { }
    }

    public override void HandleCardCountChange()
    {
        base.HandleCardCountChange();
        minusIcon.SetActive(TrackedCardCount == 2);
        equalIcon.SetActive(TrackedCardCount == 2);
    }
}
