using UnityEngine;
using UnityEngine.UI;

public class DialogueStarter : MonoBehaviour
{
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private Button nextButton;

    void Start()
    {
        dialogueController.StartDialogue(dialogueText);
        nextButton.onClick.AddListener(dialogueController.DisplayNextParagraph);
    }
}
