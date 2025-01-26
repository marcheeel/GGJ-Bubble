using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevel : MonoBehaviour
{
    public Image blackScreen;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeTo(1f, 1f));
        }
        
        IEnumerator FadeTo(float targetAlpha, float duration)
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
                SceneManager.LoadScene("Metroidvania");
                yield return null;
            }

            Color finalColor = blackScreen.color;
            finalColor.a = targetAlpha;
            blackScreen.color = finalColor;
        }
    }
}
