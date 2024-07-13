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

        // Step 2: Extract each digit of the sum and store them in separate variables
        int digit1 = result % 10;         // Extract the ones place digit
        int digit2 = (result / 10) % 10;  // Extract the tens place digit

        if (digit_1 != null)
            Destroy(digit_1);
        if (digit_2 != null)
            Destroy(digit_2);

        digit_1 = Instantiate(angkaPrefab[digit1], digit_1_Parent);

        if(result.ToString().Length > 1) 
            digit_2 = Instantiate(angkaPrefab[digit2], digit_2_Parent);

        try
        {
            // OTAK ATIK Vector3.right nya
            resultDigitParent.transform.position = Card_2.position + Vector3.right * resultOffset;
        }
        catch { }
    }
}
