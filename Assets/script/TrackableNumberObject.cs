using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackableNumberObject : MonoBehaviour
{
    public int number;

    public bool isTracked = false;

    public AudioClip numberVoice;

    public AudioSource cameraAudioSource;

    public float PosInScreenX
    {
        get => Camera.main.WorldToScreenPoint(transform.position).x;
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
            if (PengenalanAngkaManager.Instance != null)
            {
                PengenalanAngkaManager.Instance.AddCard(this);
            }
            else if (PenjumlahanManager.Instance != null)
            {
                PenjumlahanManager.Instance.AddCard(this);
            }
            else if (PenguranganManager.Instance != null)
            {
                PenguranganManager.Instance.AddCard(this);
            }

            try
            {
                if(numberVoice != null)
                {
                    cameraAudioSource.PlayOneShot(numberVoice);
                }
            }
            catch { }
        }
        else
        {
            if (PengenalanAngkaManager.Instance != null)
            {
                PengenalanAngkaManager.Instance.RemoveCard(this);
            }
            else if (PenjumlahanManager.Instance != null)
            {
                PenjumlahanManager.Instance.RemoveCard(this);
            }
            else if (PenguranganManager.Instance != null)
            {
                PenguranganManager.Instance.RemoveCard(this);
            }
        }
    }
}
