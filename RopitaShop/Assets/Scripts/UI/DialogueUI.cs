using System;
using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class DialogueUI : MenuWindow
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _openStoreButton;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    private PlayerMov _playerMov;
    private MenuManager _menuManager;
    private DialogueManager _dialogueManager;

    private void Awake()
    {
        _menuManager = FindAnyObjectByType<MenuManager>();
        _playerMov = FindFirstObjectByType<PlayerMov>();
        _dialogueManager = FindAnyObjectByType<DialogueManager>();
    }

    protected override void Initialize()
    {
        base.Initialize();
        _closeButton.onClick.AddListener(() => CloseWindow());
        _openStoreButton.onClick.AddListener(() => OpenStore());
        _dialogueManager.OnDialogueStart += SetDialogue;
    }

    private void SetDialogue(Dialogue dialogue)
    {
        StartCoroutine(SetDialogueTxt(dialogue));
    }
    private IEnumerator SetDialogueTxt(Dialogue dialogue)
    {
        _dialogueText.text = "";
        foreach (char character in dialogue.Text.ToCharArray())
        {
            _dialogueText.text += character;
            yield return new WaitForSeconds(0.05f);
        }
    }

    private void OpenStore()
    {
        HideWindow();
        StartCoroutine(DelayedOpenStore());
    }

    private void CloseWindow()
    {
        HideWindow();
        _playerMov.EnableMovement(true);
    }

    private IEnumerator DelayedOpenStore()
    {
        yield return new WaitForSeconds(0.5f);
        _menuManager.OpenWindow(_menuManager.GetWindow("store"));
    }
}

