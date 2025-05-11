using System;
using UnityEngine;

public class PlaySfx : MonoBehaviour
{
    [SerializeField] private SfxManager.Sounds _sounds;
    private SfxManager _sfxManager;
    private void Awake()
    {
        _sfxManager = FindAnyObjectByType<SfxManager>();
    }

    public void PlaySound()
    {
        _sfxManager.PlaySfx(_sounds);
    }
}
