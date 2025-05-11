using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
public class DialogueManager : MonoBehaviour
{
    public Action<Dialogue> OnDialogueStart;
        
    public List<DialogueContainer> dialogues = new List<DialogueContainer>();

    void Start()
    {
        foreach (var dialogueContainer in dialogues)
        {
            dialogueContainer.ResetDialogues();
        }
    }

    public void StartDialogue(DialogueContainer dialogueContainer)
    {
        Dialogue dialogue = null;
        bool allDialoguesRead = dialogueContainer.dialogues.All(d => d.isEndDialogue);

        if (allDialoguesRead)
        {
            dialogue = dialogueContainer.dialogues.LastOrDefault();
        }
        else
        {
            dialogue = dialogueContainer.dialogues.FirstOrDefault(d => !d.isEndDialogue);
        }

        OnDialogueStart?.Invoke(dialogue);

        if (dialogue != null)
        {
            dialogue.isEndDialogue = true;
        }
    }
}

