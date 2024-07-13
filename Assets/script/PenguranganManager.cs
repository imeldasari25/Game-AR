using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PenguranganManager : ARManagerBase
{
    //Singleton
    public static PenguranganManager Instance;

    public GameObject minusIcon;

    private void Awake()
    {
        Instance = this;
    }

    protected override void Start()
    {
        base.Start();

        minusIcon.SetActive(false);
    }

    public override void PlaceResultNumber()
    {
        result = number_A - number_B;

        int digit1 = result % 10;         // Extract the ones place digit
        int digit2 = (result / 10) % 10;  // Extract the tens place digit

        if (digit_1 != null)
            Destroy(digit_1);
        if (digit_2 != null)
            Destroy(digit_2);

        minusIcon.SetActive(false);

        if (result < 0)
        {
            digit_1 = Instantiate(angkaPrefab[-digit1], digit_1_Parent);

            if (result.ToString().Length > 2)
                digit_2 = Instantiate(angkaPrefab[-digit2], digit_2_Parent);

            minusIcon.SetActive(true);
        }
        else
        {
            digit_1 = Instantiate(angkaPrefab[digit1], digit_1_Parent);

            if (result.ToString().Length > 1)
                digit_2 = Instantiate(angkaPrefab[digit2], digit_2_Parent);

            minusIcon.SetActive(false);
        }
        

        try
        {
            // OTAK ATIK Vector3.right nya
            resultDigitParent.transform.position = Card_2.position + Vector3.right * resultOffset;
        }
        catch { }
    }

    public override void RemoveCardCounter()
    {
        base.RemoveCardCounter();

        if (TrackedCardCount != 2)
        {
            minusIcon.SetActive(false);
        }
    }
}
