using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void PenjumlahanScene()
    {
        SceneManager.LoadScene("Penjumlahan");
    }

    public void PenguranganScene()
    {
        SceneManager.LoadScene("Pengurangan");
    }
    public void MenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
