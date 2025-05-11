using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterBodyData", menuName = "Character/CharacterBodyData", order = 0)]
public class CharacterBodyData : ScriptableObject
{
    public List<BodyPart> characterBodyParts;
    
    private CharacterPartData _bodyPartData;
    private CharacterPartData _torsoPartData;
    private CharacterPartData _hairPartData;


    private void OnEnable()
    {
        GetCharacterPartData();
    }

    private void GetCharacterPartData()
    {
        foreach (var bodyPart in characterBodyParts)
        {
            switch (bodyPart.bodyPartName)
            {
                case "Body":
                    _bodyPartData = bodyPart.CharacterPartData;
                    break;
                
                case "Torso":
                    _torsoPartData = bodyPart.CharacterPartData;
                    
                    break;
                
                case "Hair":
                    _hairPartData = bodyPart.CharacterPartData;
                    break;
            }
            
        }
    }

    private void OnDisable()
    {
        ResetCharacterPartData();
    }

    private void ResetCharacterPartData()
    {
        foreach (var bodyPart in characterBodyParts)
        {
            switch (bodyPart.bodyPartName)
            {
                case "Body":
                    bodyPart.CharacterPartData = _bodyPartData;
                    break;
                
                case "Torso":
                    bodyPart.CharacterPartData = _torsoPartData;
                    
                    break;
                
                case "Hair":
                    bodyPart.CharacterPartData = _hairPartData;
                    break;
            }
            
        }
    }
}


[Serializable]
public class BodyPart
{
    public string bodyPartName;
    public CharacterPartData CharacterPartData;
}
