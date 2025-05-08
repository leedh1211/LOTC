using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SkillType
{ 
    MultiShot,
    ChangeArrow,
    State
}

[CreateAssetMenu(menuName = "Skill/SkillData")]
public class SkillData : ScriptableObject
{
    public string SkillName;
    [TextArea] public string skillInfo;
    public Sprite icon;
    public SkillType type;  
    
}
