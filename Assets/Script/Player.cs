using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int MaxHp;
    public int curHp;

    public SkillData skillData;
    public SkillData Skilldata => skillData;

    private WeaponHandler PlayerweaponHandler;

    private void Start()
    {
        PlayerweaponHandler = GetComponentInChildren<WeaponHandler>();
         
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            object effect = SkillEffectFactory.CreateEffect(skillData.type);

            if (effect == null)
            {
                Debug.Log($"스킬 생성 실패 {skillData.type}");
                return;
            }

            var context = new SkillApplyContext
            {
                player = this,
                weaponHandler = PlayerweaponHandler
            };


            SkillManager.instance.ApplySkill(effect, context);
#if UNITY_EDITOR
            Debug.Log("스킬이 들어왔습니다 " + skillData.name);
#endif
        }
    }

}
