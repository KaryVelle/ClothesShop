using UnityEngine;
using System;
using DG.Tweening;

public class MenuWindow : MonoBehaviour
{
        [Header("Menu Window Settings")] 
        [SerializeField] private string windowID;
        [SerializeField] private Transform windowTransform;
        [SerializeField] private Canvas canvas;
        [SerializeField] private float duration = 0.5f;
        [SerializeField] private float delay = 0f;
        [SerializeField] private Ease showEase = Ease.OutBack;
        [SerializeField] private Ease hideEase = Ease.InBack;
        public bool IsShowing => canvas.enabled;
        
        public string WindowName => windowID;

        public Action OnStartShowingWindow;
        public Action OnFinishedShowingWindow;
        public Action OnStartHideWindow;
        public Action OnFinishedHideWindow;

        private void Start()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            canvas.enabled = false;
            windowTransform.localScale = Vector3.zero;

        }

        public virtual void ShowWindow()
        {
            OnStartShowingWindow?.Invoke();
            canvas.enabled = true;
            windowTransform.DOScale(Vector3.one, duration).SetEase(showEase).SetDelay(delay).OnComplete(() =>
            {
                OnFinishedShowingWindow?.Invoke();
            });
        }

        public virtual void HideWindow()
        {
            OnStartHideWindow?.Invoke();
            windowTransform.DOScale(Vector3.zero, duration).SetEase(hideEase).SetDelay(delay).OnComplete(() =>
            {
                canvas.enabled = false;
                OnFinishedHideWindow?.Invoke();
            });
        }
    }
