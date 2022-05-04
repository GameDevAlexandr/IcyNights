using UnityEngine;
using System.Collections;

public class Weather : MonoBehaviour
{
    [SerializeField] private Transform _targetPosition, _cloudPosition;  
    [SerializeField] private GameObject _snow;
    [SerializeField] private GameObject _snowstorm;
    [SerializeField] private GameObject _supersnowstorm;
    [SerializeField] private int _choseWeater;
    private float timerChoseWeather = 300f;
 

    [Header("Sounds")]
    [SerializeField] private AudioSource _snowstormSound;
    [SerializeField] private AudioSource _supersnowstormSound;

    public int dependWeather;   // влияние ветра на игрока

    private void Awake()
    {
      _targetPosition =  GameObject.Find("Player").transform;
    }

    private void Update()
    {
        
        timerChoseWeather -= Time.deltaTime;

        if(timerChoseWeather <= 0)
        {
            timerChoseWeather = 300f;
            ChoseWeather(Random.Range(0, 4));
        }
        _cloudPosition.transform.position = new Vector3(_targetPosition.transform.position.x, _targetPosition.transform.position.y + 10f, _targetPosition.transform.position.z);
        switch (_choseWeater)
        {
            case 3:
                _supersnowstorm.SetActive(true);
                _snowstorm.SetActive(false);
                _snow.SetActive(false);
                dependWeather = 3;
                break;
            case 2:
                
                _snowstorm.SetActive(true);
                _snow.SetActive(false);
                _supersnowstorm.SetActive(false);
                dependWeather = 2;
                break;
            case 1:
                _snow.SetActive(true);
                _snowstorm.SetActive(false);
                _supersnowstorm.SetActive(false);
                dependWeather = 1;
                break;
            case 0:
                _snow.SetActive(false);
                _snowstorm.SetActive(false);
                _supersnowstorm.SetActive(false);
                dependWeather = 0;
                break;
        }    
    }

    public  void  ChoseWeather(int weather)
    {
        _choseWeater = weather;
    }


}
