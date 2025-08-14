using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NPCNameText;
    [SerializeField] private TextMeshProUGUI NPCDialogueText;
    [SerializeField] private float typeSpeed = 10;

    private Queue<string> paragraphs = new Queue<string>();

    private bool dialogueEnded;
    private bool isTyping;

    private string p;

    private const string HTML_APLHA = "<color=#00000000>";
    private const float MAX_TYPE_TIME = 0.1f;

    private Coroutine typeDialogueCoroutine;

    public void DisplayNextParagraph(DialogueText dialogueText)
    {
        if(paragraphs.Count == 0)
        {
            if (!dialogueEnded)
            {
                StartDialogue(dialogueText);
            }
            else if(dialogueEnded && !isTyping)
            {
                EndDialogue();
                return;
            }
        }

        if (!isTyping)
        {
            p = paragraphs.Dequeue();

            typeDialogueCoroutine = StartCoroutine(TypeDialogueText(p));
        }

        else
        {
            FinishParagraphEarly();
        }

        //NPCDialogueText.text = p;

        if (paragraphs.Count == 0)
        {
            dialogueEnded = true;
        }
    }

    private void StartDialogue(DialogueText dialogueText)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }

        NPCNameText.text = dialogueText.speakerName;

        for(int i = 0; i < dialogueText.paragraphs.Length; i++)
        {
            paragraphs.Enqueue(dialogueText.paragraphs[i]);
        }
    }
    private void EndDialogue()
    {
        dialogueEnded = false;

        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }

    /*IEnumerator TypeDialogueText(string p)
    {
        float elapsedTime = 0f;

        int charIndex = 0;
        charIndex = Mathf.Clamp(charIndex, 0, p.Length);

        while(charIndex < p.Length)
        {
            elapsedTime += Time.deltaTime * typeSpeed;
            charIndex = Mathf.FloorToInt(elapsedTime);

            NPCDialogueText.text = p.Substring(0, charIndex);

            yield return null;
        }

        NPCDialogueText.text = p;
    }*/

    IEnumerator TypeDialogueText(string p)
    {
        isTyping = true;
        NPCDialogueText.text = "";

        string originalText = p;
        int alphaIndex = 0;

        foreach (char c in p.ToCharArray())
        {
            alphaIndex++;
            NPCDialogueText.text = originalText.Insert(alphaIndex, HTML_APLHA);

            yield return new WaitForSeconds(MAX_TYPE_TIME / typeSpeed);
        }

        NPCDialogueText.text = originalText;
        isTyping = false;
    }

    void FinishParagraphEarly()
    {
        if (typeDialogueCoroutine != null)
        {
            StopCoroutine(typeDialogueCoroutine);
        }

        NPCDialogueText.text = p;
        isTyping = false;
    }
}
