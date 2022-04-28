
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuPanel : MonoBehaviour
{
    
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Button _closeSettingsPanel;
    [SerializeField] private AudioMixerSnapshot _paused;
    [SerializeField] private AudioMixerSnapshot _unpaused;



    private GameObject _gameManager;
    private void Start()
    {
        _gameManager = GameObject.Find("GameManager");
    }

      
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
                Pause();
        }
    }


    public void Pause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        Lowpass();
        if (_settingsPanel.activeInHierarchy == false)
        {
            _settingsPanel.SetActive(true);
        }
        else if (_settingsPanel.activeInHierarchy == true)
        {
            _settingsPanel.SetActive(false);
        }
    }


    private void Lowpass()
    {
        if(Time.timeScale == 0)
        {
            _paused.TransitionTo(0.01f);
        }
        else
        {
            _unpaused.TransitionTo(0.01f);
        }
    }


    public void ExitMenuScene()
    {
        Pause();
        Destroy(_gameManager);
        SceneManager.LoadScene(0);
    }



}
