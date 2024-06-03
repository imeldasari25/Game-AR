using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    
    public void switchPengenalanAngka()
    {
        SceneManager.LoadScene("AR - PengenalanAngka"); 
    }
    
    public void switchPengurangan()
    {
        SceneManager.LoadScene("AR - Pengurangan"); 
    }
    
    public void switchPenjumlahan()
    {
        SceneManager.LoadScene("AR - Penjumlahan");
    }
}
