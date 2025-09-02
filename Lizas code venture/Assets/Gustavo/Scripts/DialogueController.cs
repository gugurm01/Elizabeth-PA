using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NPCNameText;
    [SerializeField] private TextMeshProUGUI NPCDialogueText;
    [SerializeField] private float typeSpeed = 10;
    public UnityEvent finishEvent;

    private Queue<string> paragraphs = new Queue<string>();
    private bool dialogueEnded;
    private bool isTyping;
    private string p;
    private const float MAX_TYPE_TIME = 0.1f;
    private Coroutine typeDialogueCoroutine;

    public void StartDialogue(DialogueText dialogueText)
    {
        paragraphs.Clear();
        dialogueEnded = false;
        if (!gameObject.activeSelf) gameObject.SetActive(true);
        NPCNameText.text = dialogueText.speakerName;
        foreach (var paragraph in dialogueText.paragraphs) paragraphs.Enqueue(paragraph);
        DisplayNextParagraph();
    }

    public void DisplayNextParagraph()
    {
        if (isTyping) { FinishParagraphEarly(); return; }

        if (paragraphs.Count == 0)
        {
            EndDialogue();
            return;
        }

        p = paragraphs.Dequeue();
        typeDialogueCoroutine = StartCoroutine(TypeDialogueText(p));
    }

    private void EndDialogue()
    {
        dialogueEnded = true;
        finishEvent.Invoke();
    }

    IEnumerator TypeDialogueText(string p)
    {
        isTyping = true;
        NPCDialogueText.text = p;
        NPCDialogueText.maxVisibleCharacters = 0;

        for (int i = 0; i <= p.Length; i++)
        {
            NPCDialogueText.maxVisibleCharacters = i;
            yield return new WaitForSeconds(MAX_TYPE_TIME / typeSpeed);
        }

        isTyping = false;
    }

    private void FinishParagraphEarly()
    {
        StopCoroutine(typeDialogueCoroutine);
        NPCDialogueText.maxVisibleCharacters = p.Length;
        isTyping = false;
    }
}
