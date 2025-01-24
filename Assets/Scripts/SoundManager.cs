using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Serialization;

public class SoundManager : MonoBehaviour
{
    public bool pause;

    public GameObject pauseMenu;

    public AudioMixer audioMixer;

    public Slider masterVolumeSlider;
    public Slider BGMVolumeSlider;
    public Slider SFXVolumeSlider;
    public Slider dialogueVolumeSlider;

    private void Update()
    {
        audioMixer.SetFloat("Master", masterVolumeSlider.value);
        audioMixer.SetFloat("Music", BGMVolumeSlider.value);
        audioMixer.SetFloat("SFX", SFXVolumeSlider.value);
        audioMixer.SetFloat("Dialogue", dialogueVolumeSlider.value);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
        }
        if (pause == true)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive (false);
        }
    }
}
