using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manusia : MakhlukHidup
{
    [Header("Punya Manusia")]
    public JenisRambut JenisRambut;

    public float tinggiBadan;
    public float jumlahJari;
}

public enum JenisRambut
{
    Lurus, Keriting, Bergelombang, Botak
}
