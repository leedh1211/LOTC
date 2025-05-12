using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SkillType
{ 
    Shot,
    ChangeArrow,
    State,
    Orbit
}

[CreateAssetMenu(menuName = "Skill/SkillData")]
public class SkillData : ScriptableObject
{
    public string SkillName;
    [TextArea] public string skillInfo;
    public Sprite icon;
    public SkillType type;

    [Header("조합설정")]
    public List<SkillType> combinable;
    public SkillType? resultCombo;

}
// 스킬 적용받는 대상 + 소스를 넣는다 -> 민혁님 조언

//멀티샷 튕기는거 상태 관련 스킬만 먼저 만들어보자 
