using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/New Dialogue Container")]
public class DialogueText : ScriptableObject
{
    public string speakerName;

    public string[] paragraphs;
}
