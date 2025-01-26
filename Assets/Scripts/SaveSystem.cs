using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem current;
    
    public UnityEngine.UI.Image blackScreen;
    
    public TextMeshProUGUI coinsText;

    private void Awake()
    {
        if (current != null)
        {
            Destroy(gameObject);
        }
        else
        {
            current = this;
        }

        Color color = blackScreen.color;
        color.a = 0;
        blackScreen.color = color;
    }

    private void Update()
    {
        coinsText.text = "\u00d7 " + Controller.current.money;
    }

    public IEnumerator BackToCheckpoint()
    {
        yield return StartCoroutine(FadeTo(1.0f, 0.5f));
        
        PlayerPrefs.GetFloat("Checkpoint.X");
        PlayerPrefs.GetFloat("Checkpoint.Y");
        PlayerPrefs.GetFloat("Checkpoint.Z");
        
        Controller.current.money = PlayerPrefs.GetInt("Money");
        Controller.current.GetComponent<Transform>().position = 
            new Vector3(PlayerPrefs.GetFloat("Checkpoint.X"), 
            PlayerPrefs.GetFloat("Checkpoint.Y"), 
            PlayerPrefs.GetFloat("Checkpoint.Z"));

        yield return StartCoroutine(FadeTo(0.0f, 0.5f));
    }

    private IEnumerator FadeTo(float targetAlpha, float duration)
    {
        float startAlpha = blackScreen.color.a;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / duration);
            Color color = blackScreen.color;
            color.a = newAlpha;
            blackScreen.color = color;
            yield return null;
        }

        Color finalColor = blackScreen.color;
        finalColor.a = targetAlpha;
        blackScreen.color = finalColor;
    }
}
