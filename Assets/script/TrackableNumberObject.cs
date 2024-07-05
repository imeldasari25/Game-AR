using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackableNumberObject : MonoBehaviour
{
    public int number;

    public bool isTracked = false;

    public AudioClip numberVoice;

    public AudioSource cameraAudioSource;

    // Update is called once per frame
    void Update()
    {
        if (isTracked)
        {
            float xCoordinate = Camera.main.WorldToScreenPoint(transform.position).x;

            Debug.Log(xCoordinate);

            //Jika scene saat ini adalah Pengenalan Angka, maka pakai ini
            if(PengenalanAngkaManager.Instance != null)
            {
                PengenalanAngkaManager.Instance.AssignCard(transform, xCoordinate);
                //Debug.Log("Pengenalan Angka - Test");
            }
            else if(PenjumlahanManager.Instance != null)
            {
                PenjumlahanManager.Instance.AssignCard(transform, xCoordinate);
                //Debug.Log("Penjumlahan - Test");
            }
            else if(PenguranganManager.Instance != null)
            {
                PenguranganManager.Instance.AssignCard(transform, xCoordinate);
                //Debug.Log("Pengurangan - Test");
            }
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// Ini di panggil di dalam Event Image Target
    /// </summary>
    /// <param name="isTracked"></param>
    public void SetIsTracked(bool isTracked)
    {
        this.isTracked = isTracked;

        if (isTracked)
        {
            try
            {
                if(numberVoice != null)
                {
                    cameraAudioSource.PlayOneShot(numberVoice);
                }
            }
            catch { }
        }
    }
}
