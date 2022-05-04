using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float _timeOfDay;
    [SerializeField] private float _dayDuration;
    [SerializeField] private Light _sun;
    [SerializeField] private AnimationCurve SunCurve;
    private float sunIntesivity;
    public float TimeDayPublic;
    // Start is called before the first frame update
    void Start()
    {
        sunIntesivity = _sun.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        _timeOfDay += Time.deltaTime / _dayDuration;
        if(_timeOfDay >= 1)
        {
            _timeOfDay -= 1;
        }
        _sun.transform.localRotation = Quaternion.Euler(_timeOfDay * 360f, 180, 0);
        _sun.intensity = sunIntesivity * SunCurve.Evaluate(_timeOfDay);

    }
    

    public float TimeDay()
    {
        TimeDayPublic = _timeOfDay;
        return TimeDayPublic;
    }



}
