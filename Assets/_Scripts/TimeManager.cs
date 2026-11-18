using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    
    [SerializeField]
    [Tooltip("Time intervals add up when reaching 1 they move to the next data Point")]
    private float TimeProgessionSpeed = 0.1f;
    [SerializeField]
    [Tooltip("The number which has to be reached to get to the next data point")]
    private float TimeProgressionUnit = 1f;
    public static TimeManager Manager;
    private void Awake()
    {
        if(Manager != null && Manager != this)
        {
            Destroy(this);
        }
        Manager = this;
        DontDestroyOnLoad(Manager);
    }

    public IEnumerator StartTime()
    {
        int TimeCounter = 0;
        float TimeStepCounter = 0;

        while (true)
        {
            if(TimeCounter >= WeatherDataManager.Manager.WeatherDataPoints.Length)
            {
                yield break;
            }else {TimeCounter++;}
            if(TimeStepCounter >= TimeProgressionUnit)
            {
                WeatherDataManager.Manager.CurrentDataPoint = WeatherDataManager.Manager.WeatherDataPoints[TimeCounter];
                WeatherDataManager.Manager.PrintCurrentDataPoint();
                TimeStepCounter = 0;
            }
            TimeStepCounter += TimeProgessionSpeed;
            yield return new WaitForSeconds(1f);
        }
    }
    public void StopTime()
    {
        StopCoroutine(StartTime());
    }
}
