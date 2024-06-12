using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakhlukHidup : MonoBehaviour
{
    [Header("Punya Makhluk Hidup")]
    public int jumlahTangan;
    public int jumlahKaki;

    public JenisKelamin JenisKelamin;
}

public enum JenisKelamin
{
    Cowok, Cewek, NonBinary
}
