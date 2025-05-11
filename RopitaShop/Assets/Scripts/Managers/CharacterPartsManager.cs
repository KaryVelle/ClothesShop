using System.Collections.Generic;
using UnityEngine;
public class CharacterPartsManager : MonoBehaviour
{
        [Header("Character Body")] [SerializeField]
        private CharacterBodyData characterBodyData;
        [SerializeField] private string[] bodyPartTypes;
        [SerializeField] private string[] characterStates;
        [SerializeField] private string[] characterDirections;
    
        [SerializeField] private SpriteRenderer _hairSpriteRenderer;
        [SerializeField] private SpriteRenderer _torsoSpriteRenderer;
        private Animator _animator;
        private AnimationClip _animationClip;
        private AnimatorOverrideController _animatorOverrideController;
        private AnimationClipOverrides _defaultAnimationClips;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            _animator = GetComponent<Animator>();

            _animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
            _animator.runtimeAnimatorController = _animatorOverrideController;

            _defaultAnimationClips = new AnimationClipOverrides(_animatorOverrideController.overridesCount);
            _animatorOverrideController.GetOverrides(_defaultAnimationClips);

            SetBodyParts();

            CharacterPartSetector.OnBodyPartUpdate += SetBodyParts;
            
            EnableHairRenderer(false);
            EnableTorsoRenderer(false);
        }
        
        private void EnableHairRenderer(bool enable)
        {
            _hairSpriteRenderer.enabled = enable;
        }
        
        private void EnableTorsoRenderer(bool enable)
        {
            _torsoSpriteRenderer.enabled = enable;
        }

        private void SetHairColor(Color color)
        {
            _hairSpriteRenderer.color = color;
        }
        
        private void SetBodyParts(ClotheType clotheType = default)
        {
            switch (clotheType)
            {
                case ClotheType.Hair:
                    EnableHairRenderer(true);
                    break;
                case ClotheType.Torso:
                    EnableTorsoRenderer(true);
                    break;
            }
            
            for (int partIndex = 0; partIndex < bodyPartTypes.Length; partIndex++)
            {
                string partType = bodyPartTypes[partIndex];
                string partID = characterBodyData.characterBodyParts[partIndex].CharacterPartData.BodyPartAnimationID
                    .ToString();

                for (int stateIndex = 0; stateIndex < characterStates.Length; stateIndex++)
                {
                    string state = characterStates[stateIndex];
                    for (int directionIndex = 0; directionIndex < characterDirections.Length; directionIndex++)
                    {
                        string direction = characterDirections[directionIndex];
                        _animationClip = Resources.Load<AnimationClip>("PlayerAnimations/" + partType + "/" + partType +
                                                                       "_" + partID + "_" + state + "_" + direction);
                        _defaultAnimationClips[partType + "_" + 0 + "_" + state + "_" + direction] = _animationClip;
                    }
                    Debug.Log(state);
                    Debug.Log(_animationClip);
                }
            }

            _animatorOverrideController.ApplyOverrides(_defaultAnimationClips);
            
            
        }
    }

    public class AnimationClipOverrides : List<KeyValuePair<AnimationClip, AnimationClip>>
    {
        public AnimationClipOverrides(int capacity) : base(capacity)
        {
        }

        public AnimationClip this[string name]
        {
            get { return this.Find(x => x.Key.name.Equals(name)).Value; }
            set
            {
                int index = this.FindIndex(x => x.Key.name.Equals(name));
                if (index != -1)
                    this[index] = new KeyValuePair<AnimationClip, AnimationClip>(this[index].Key, value);
            }
        }
    }

