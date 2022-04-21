using UnityEngine;
using System.Collections;

public class Weather : MonoBehaviour
{
    [SerializeField] private Transform _targetPosition, _cloudPosition;  
    [SerializeField] private GameObject _snow;
    [SerializeField] private GameObject _snowstorm;
    [SerializeField] private GameObject _supersnowstorm;
    [SerializeField] private int _choseWeater;
    [SerializeField] private GameObject _sun;
    [SerializeField] private float _timeDay;
    float maxBR = 1;
    public float curBR;

    [Header("Sounds")]
    [SerializeField] private AudioSource _snowstormSound;
    [SerializeField] private AudioSource _supersnowstormSound;

    public int dependWeather;   // влияние ветра на игрока

    private void Awake()
    {
        _sun = GameObject.Find("Directional Light");
         curBR = _sun.GetComponent<Light>().intensity;
    }

    private void Start()
    {
        curBR = maxBR;
    }
    private void Update()
    {
        _timeDay -= Time.deltaTime;
        curBR = CalculatorHealt();

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

    float CalculatorHealt()
    {
        return curBR / maxBR;
    }

}
