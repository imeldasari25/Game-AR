using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberAnimator : MonoBehaviour
{
    public static event System.Action<GameObject> OnNumberAnimationDone;

    public float moveSpeed = 3;
    public float scaleSpeed = 100;
    public GameObject numberObj;

    [Space(10)]
    public Transform targetToGo;    

    bool _isGoToTarget;
    bool _isScaleDown;
    bool _isAnimationDone;

    Vector3 _dir;

    Vector3 _defPos;
    Quaternion _defRot;
    Vector3 _defScale;

    private void Start()
    {
        _defPos = numberObj.transform.localPosition;
        _defRot = numberObj.transform.localRotation;
        _defScale = numberObj.transform.localScale;

        _isAnimationDone = false;
    }

    private void Update()
    {
        if(_isGoToTarget)
        {
            ToTarget();
        }
        
        if(_isScaleDown)
        {
            ScaleDown();
        }
    }

    public void StartGoTo()
    {
        _isGoToTarget = true;
        _isScaleDown = false;

        _isAnimationDone = true;
    }

    public void ToTarget()
    {
        numberObj.transform.LookAt(targetToGo);
        numberObj.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if(Vector3.Distance(numberObj.transform.position, targetToGo.transform.position) < 0.5f)
        {
            _isGoToTarget = false;
            _isScaleDown = true;
        }
    }

    public void ScaleDown()
    {
        numberObj.transform.localScale -= Vector3.one * scaleSpeed * Time.deltaTime;

        if(numberObj.transform.localScale.x <= 0)
        {
            _isScaleDown = false;           
            OnNumberAnimationDone?.Invoke(numberObj);
        }
    }

    /// <summary>
    /// Dipanggil didalam Vuforia Event
    /// </summary>
    public void Reset()
    {
        if(_isAnimationDone)
        {
            numberObj.transform.localPosition = _defPos;
            numberObj.transform.localRotation = _defRot;
            numberObj.transform.localScale = _defScale;
        }        
    }
}
