using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    public struct PlayerState
    {
        public int maxHp;
        public int curHp;
        public int Speed;

    }
    private PlayerController playerController;

    public List<Transform> monsterList; //Test
   
    [SerializeField] private PlayerVisual playerVisual;



    [Header("WeaPon")]
    private WeaponHandler weapon;
    
    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerController.Init(playerVisual);

        //Weapon
        weapon = GetComponentInChildren<WeaponHandler>();
    }

    public void SetSkillData(SkillData data)
    {
        object effect = SkillEffectFactory.CreateEffect(data.type);
        if (effect == null)
        {
            return;
        }

        var context = new SkillApplyContext
        {
            player = this,
            weaponHandler = weapon
            //직접 인스턴스화 시켜서 사용함. 
        };


        SkillManager.Instance.LearnSkill(data, context); //스킬 적용

    }

    public Transform GetNearestEnemy()
    {
        float Nearest = Mathf.Infinity;
        float dis;
        Transform NearestMonster = null;


        for (int i = 0; i < monsterList.Count; i++)
        {
            //
            dis = (transform.position - monsterList[i].position).sqrMagnitude;

            if (dis < Nearest)
            {
                NearestMonster = monsterList[i];
                Nearest = dis;

            }
            //else if(monster die) continue return 등등 죽으면 처리 방법 팀원 상의후 추가하기 



        }
        return NearestMonster;
    }





}


