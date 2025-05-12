using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{

    public MonsterListVariableSO monsterList;

    private PlayerController playerController;

    private WeaponHandler weapon;
    [Header("OrbitSkill")]
    [SerializeField]private GameObject orbitPrefab;
    
    [SerializeField] private PlayerVisual playerVisual;

    
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

    public MonsterController GetNearestEnemy()
    {
        float Nearest = Mathf.Infinity;
        float dis;
        
        MonsterController NearestMonster = null;


        for (int i = 0; i < monsterList.RuntimeValue.Count; i++)
        {
            //
            dis = (transform.position - monsterList.RuntimeValue[i].transform.position).sqrMagnitude;

            if (dis < Nearest)
            {
                NearestMonster = monsterList.RuntimeValue[i];
                Nearest = dis;

            }
            //else if(monster die) continue return 등등 죽으면 처리 방법 팀원 상의후 추가하기 
        }
        return NearestMonster;
    }


    public void SetOrbitSkill()
    { 
        GameObject orbit =  Instantiate(orbitPrefab);
        orbit.transform.SetParent(this.transform);

    }




}


