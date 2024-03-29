using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackableObject : MonoBehaviour
{
    public bool isTracked = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isTracked)
        {
            float xCoordinate = Camera.main.WorldToScreenPoint(transform.position).x;

            PengenalanAngkaManager.Instance.AssignCard(transform, xCoordinate);

        }
        else
        {
            return;
        }
    }

    public void SetIsTracked(bool isTracked)
    {
        this.isTracked = isTracked;
    }
}
