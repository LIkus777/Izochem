using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Buttons : MonoBehaviour
{
    public Image attenuation;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    public AudioSource menuSound;

    private void Awake() {
        attenuation.gameObject.SetActive(false);
        videoPlayer.gameObject.SetActive(false);
    }

    public void PlayGame() {
        Debug.Log("Played!");
        StartCoroutine(PlayAnimationAndVideo());
    }

    public void QuitGame() {
        Debug.Log("Quitted!");
        Application.Quit();
    }

    public void PlayAudio() {
        audioSource.Play();
    }

    private IEnumerator PlayAnimationAndVideo() {
        attenuation.gameObject.SetActive(true);
        for (float i = 0.0f; i <= 1.2f; i += 0.05f) {
            SetAlpha(attenuation, i);
            if (i < 1.0f) {
                menuSound.volume = 1.0f - i;
            } else {
                menuSound.volume = 0.0f;
            }
            yield return new WaitForSeconds(0.03f);
        }
        videoPlayer.gameObject.SetActive(true);
        videoPlayer.Play();
        yield return new WaitForSeconds(63.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void SetAlpha(Image image, float aplha) {
        Color color = image.color;
        color.a = aplha;
        image.color = color;
    }
}
