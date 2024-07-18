using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PengenalanAngkaManager : ARManagerBase
{
    public static PengenalanAngkaManager Instance;

    private void Awake()
    {
        Instance = this;
    }
}
