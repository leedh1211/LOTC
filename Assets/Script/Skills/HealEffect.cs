using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEffect : IPlayerApplicable
{
    public void ApplyToPlayer(SkillApplyContext context)
    {
        context.player.curHp += 50;
       //�ϴ� ü�� ȸ������ ���
    }


}
