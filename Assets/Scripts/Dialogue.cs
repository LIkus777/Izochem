using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject dialogueWindow;
    public GameObject podskaska;
    public Animator windowAnimator;
    public TextMeshProUGUI textComponent;
    public Image playerImage;
    public Image npcImage;
    public Image first;
    public string[] lines;
    public float textSpeed = 0.3f;

    private bool isStarted = false;
    private bool isPlayed = false;
    private int index = -1;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Return) && isStarted) {
            if (textComponent.text == lines[index]) {
                if (index + 1 >= lines.Length) {
                    EndDialogue();
                } else {
                    NextLine();
                }
            } else {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && !isStarted && !isPlayed) {
            StartDialogue();
        }
    }

    private void StartDialogue() {
        isStarted = true;
        FindObjectOfType<PlayerMovement>().SetStop(true);
        textComponent.text = "";
        StartCoroutine(PlayAnimationAndReturn());
    }

    private void EndDialogue() {
        isStarted = false;
        isPlayed = true;
        textComponent.text = "";
        StartCoroutine(PlayEndAnimation());
    }

    private IEnumerator PlayEndAnimation() {
        if (podskaska != null) {
            podskaska.SetActive(false);
        }
        SetAlpha(npcImage, 0);
        SetAlpha(playerImage, 0);
        windowAnimator.Play("Ending", 0, 0f);
        yield return new WaitForSeconds(windowAnimator.GetCurrentAnimatorStateInfo(0).length - 0.02f);
        dialogueWindow.SetActive(false);
        FindObjectOfType<PlayerMovement>().SetStop(false);
        StartCoroutine(PlayNPCDestroy());
    }

    private IEnumerator PlayAnimationAndReturn() {
        SetAlpha(playerImage, 0);
        SetAlpha(npcImage, 0);
        dialogueWindow.SetActive(true);
        windowAnimator.Play("Creating", 0, 0f);
        yield return new WaitForSeconds(windowAnimator.GetCurrentAnimatorStateInfo(0).length - 0.02f);
        if (podskaska != null) {
            podskaska.SetActive(true);
        }
        SetAlpha(first, 1);
        NextLine();
    }

    private IEnumerator PlayNPCDestroy() {
        Transform npcTransform = transform.Find("NPC Sprite");
        SpriteRenderer npcSpriteRenderer = npcTransform.GetComponent<SpriteRenderer>();
        for (float i = 1.0f; i >= 0; i -= 0.05f) {
            Color color = npcSpriteRenderer.color;
            color.a = i;
            npcSpriteRenderer.color = color;
            yield return new WaitForSeconds(0.005f);
        }
        Destroy(gameObject);
    }

    private void NextLine() {
        index++;
        if (index > 0)  {
            if (playerImage.color.a == 0) {
                SetAlpha(playerImage, 1);
                SetAlpha(npcImage, 0);
            } else {
                SetAlpha(playerImage, 0);
                SetAlpha(npcImage, 1);
            }
        }
        StartCoroutine(AnimateText());
    }

    private IEnumerator AnimateText() {
        textComponent.text = "";
        foreach (char letter in lines[index].ToCharArray())
        {
            textComponent.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void SetAlpha(Image image, int aplha) {
        Color color = image.color;
        color.a = aplha;
        image.color = color;
    }
}