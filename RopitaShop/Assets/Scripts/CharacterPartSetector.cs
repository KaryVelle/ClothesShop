using UnityEngine;
using System;

public class CharacterPartSetector : MonoBehaviour
{
     #region Singleton

        private static CharacterPartSetector _instance;

        public static CharacterPartSetector Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<CharacterPartSetector>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject();
                        go.name = "CharacterPartSelector";
                        _instance = go.AddComponent<CharacterPartSetector>();
                    }
                }

                return _instance;
            }
        }

        #endregion

        
    [SerializeField] private CharacterBodyData _characterBodyData;
    [SerializeField] private BodyPartSelection[] bodyPartSelections;
    public static Action<Color> OnHairColorChange;
    public static Action<ClotheType> OnBodyPartUpdate;
    
        private void Awake()
        {
            _instance = this;
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            for (int i = 0; i < bodyPartSelections.Length; i++)
            {
                GetCurrentBodyParts(i);
            }
        }

        public void ChangeBodyPart(string bodyPartName, int newCharacterPartDataIndex)
        {
            int bodyPartIndex = Array.FindIndex(bodyPartSelections, bp => bp.bodyPartName == bodyPartName);
            if (bodyPartIndex == -1)
            {
                Debug.Log($"Error no body part: {bodyPartName}");
                return;
            }
            bodyPartSelections[bodyPartIndex].bodyPartCurrentIndex = newCharacterPartDataIndex;
            ClotheType clotheType = (ClotheType) Enum.Parse(typeof(ClotheType), bodyPartName);
            OnBodyPartUpdate?.Invoke(clotheType);
            UpdateCurrentPart(bodyPartIndex, clotheType);
            Debug.Log($"Changing part: {bodyPartName} to index: {newCharacterPartDataIndex}");
        }


        private void GetCurrentBodyParts(int partIndex)
        {
            BodyPartSelection currentBodyPartSelection = bodyPartSelections[partIndex];
            BodyPart currentBodyPart = _characterBodyData.characterBodyParts[partIndex];
            int currentBodyPartAnimationID = currentBodyPart.CharacterPartData.BodyPartAnimationID;
            currentBodyPartSelection.bodyPartCurrentIndex = currentBodyPartAnimationID;
        }

        private void UpdateCurrentPart(int partIndex, ClotheType clotheType)
        {
            BodyPartSelection currentSelection = bodyPartSelections[partIndex];
            int currentIndex = currentSelection.bodyPartCurrentIndex;
            CharacterPartData currentPartData = currentSelection.CharacterPartOptions[currentIndex];
            _characterBodyData.characterBodyParts[partIndex].CharacterPartData = currentPartData;
            OnBodyPartUpdate?.Invoke(clotheType);
            Debug.Log($"Updating visuals for {clotheType}");
        }
    }

    [Serializable]
    public class BodyPartSelection
    {
        public string bodyPartName;
        public CharacterPartData[] CharacterPartOptions;
        [HideInInspector] public int bodyPartCurrentIndex;
    }
