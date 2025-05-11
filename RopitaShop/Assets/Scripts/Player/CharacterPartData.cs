using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterPartData", menuName = "BGS/Character/CharacterPartData", order = 0)]
public class CharacterPartData : ScriptableObject
{
    public string BodyPartName;
    public int BodyPartAnimationID;
    public List<AnimationClip> AllBodyPartAnimations = new List<AnimationClip>();
}
