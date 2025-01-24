using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Musiccontroller : MonoBehaviour
{
    public bool pause;

    public GameObject canvasPau;

    public AudioMixer Master;

    public Slider SliderMaster;
    public Slider SliderMusic;
    public Slider SliderSFX;
    public Slider SliderDialogo;

    private void Update()
    {
        Master.SetFloat("Master", SliderMaster.value);
        Master.SetFloat("Music", SliderMusic.value);
        Master.SetFloat("SFX", SliderSFX.value);
        Master.SetFloat("Dialogo", SliderDialogo.value);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
        }
        if (pause == true)
        {
            canvasPau.SetActive(true);
        }
        else
        {
            canvasPau.SetActive (false);
        }
    }
}
