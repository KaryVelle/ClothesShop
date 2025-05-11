using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DialogueContainer", menuName = "DialogueContainer", order = 0)]
public class DialogueContainer : ScriptableObject
{
    public List<Dialogue> dialogues;

    public void ResetDialogues()
    {
        foreach (var dialogue in dialogues)
        {
            dialogue.isEndDialogue = false;
        }
    }
    private void OnDisable()
    {
        ResetDialogues();
    }
}

[System.Serializable]
public class Dialogue
{
    public string Text;
    public bool isEndDialogue;
}

