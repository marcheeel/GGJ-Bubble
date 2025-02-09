using System.Collections;
using UnityEngine;

public class EvolveTutorial : MonoBehaviour
{
    public AudioSource dialogueSource;
    public AudioSource BGMSource;

    public AudioClip dialogueLineClip;

    public AudioClip NewBGMClip;
    
    public UnityEngine.UI.Image blackScreen;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private GameObject newPosition;

    [SerializeField] private GameObject camera1;
    [SerializeField] private GameObject camera2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerPosition = other.transform;
            dialogueSource.clip = dialogueLineClip;
            dialogueSource.Play();
            
            BGMSource.Stop();
            BGMSource.clip = NewBGMClip;
            StartCoroutine(FadeToBlackAndBack());
        }
    }

    private IEnumerator FadeToBlackAndBack()
    {
        Controller.current.canMove = false;
        yield return StartCoroutine(FadeTo(1.0f, 1.0f));
        
        playerPosition.position = newPosition.transform.position;
        camera1.SetActive(false);
        camera2.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        
        yield return StartCoroutine(FadeTo(0.0f, 1.0f));
        BGMSource.Play();
        Controller.current.canMove = true;
        yield return null;
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