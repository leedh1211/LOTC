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

    [Header("���ռ���")]
    public List<SkillType> combinable;
    public SkillType? resultCombo;

}
// ��ų ����޴� ��� + �ҽ��� �ִ´� -> ������ ����

//��Ƽ�� ƨ��°� ���� ���� ��ų�� ���� ������ 
