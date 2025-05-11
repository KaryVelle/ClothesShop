using System;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip gemSound;
    [SerializeField] private AudioClip npcSound;
    [SerializeField] private AudioClip selectUISound;
    [SerializeField] private AudioClip openUISound;
    [SerializeField] private AudioClip closeUISound;
    [SerializeField] private AudioClip purchaseSound;

    private Dictionary<Sounds, AudioClip> _soundMap;

    public enum Sounds
    {
        Gem,
        Npc,
        Select,
        OpenUI,
        CloseUI,
        Purchase
    }

    private void Awake()
    {
        if (_audioSource == null)
            _audioSource = gameObject.AddComponent<AudioSource>();

        _soundMap = new Dictionary<Sounds, AudioClip>
        {
            { Sounds.Gem, gemSound },
            { Sounds.Npc, npcSound },
            { Sounds.Select, selectUISound },
            { Sounds.OpenUI, openUISound },
            { Sounds.CloseUI, closeUISound },
            { Sounds.Purchase, purchaseSound },
        };
    }

    public void PlaySfx(Sounds sound)
    {
        if (_soundMap.TryGetValue(sound, out AudioClip clip) && clip != null)
        {
            _audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning($"🎧 No AudioClip assigned for: {sound}");
        }
    }
}
