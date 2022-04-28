
using UnityEngine;
using UnityEngine.Audio;

public class MixLevels : MonoBehaviour
{

    [SerializeField] private AudioMixer _audioMixer;
    // Start is called before the first frame update


    public void SetSfxLevl(float sfxLvl)
    {
        _audioMixer.SetFloat("sfxVol", sfxLvl);
    }

    public void SetMusicLvl(float musicLvl)
    {
        _audioMixer.SetFloat("musicVol", musicLvl);
    }
}
