using System;
using UnityEngine;
using Utils;

public class NPC : MonoBehaviour
{
    [SerializeField] private string _UIName = "clothe.store";
    [SerializeField] private DialogueContainer _dialogueContainer;
    private MenuManager _menuManager;
    private PlayerMov _playerMov;
    private DialogueManager _dialogueManager;
    private InputManager _inputManager;
    private bool isPlayerInZone;
    [SerializeField] private GameObject interact;
    private SfxManager _sfxManager;

    private void Awake()
    {
        _menuManager = FindAnyObjectByType<MenuManager>();
        _playerMov = FindAnyObjectByType<PlayerMov>();
        _dialogueManager = FindAnyObjectByType<DialogueManager>();
        _inputManager = FindAnyObjectByType<InputManager>();
        _sfxManager = FindAnyObjectByType<SfxManager>();
    }
    private void Update()
    {
        if (isPlayerInZone && _inputManager.InteractAction.action.WasPressedThisFrame())
        {
            Debug.Log("Interaction triggered");
            Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = true;
            interact.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
            interact.SetActive(false);
        }
    }

    private void Interact()
    {        
        _menuManager.OpenWindow(_menuManager.GetWindow(_UIName));
        _playerMov.EnableMovement(false);
        Vector2 directionDown = new Vector2(0, -1);
        _playerMov.SetAnimToFixedDirection(directionDown);
        _dialogueManager.StartDialogue(_dialogueContainer);
        _sfxManager.PlaySfx(SfxManager.Sounds.Npc);
    }
}
