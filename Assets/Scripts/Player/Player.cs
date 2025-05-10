using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    private PlayerController playerController;
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


    


    
}


/*public class Player : MonoBehaviour
{
    public int MaxHp;
    public int curHp;

    public SkillData skillData;
    
    // public SkillData Skilldata => skillData;

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
                Debug.Log($"��ų ���� ���� {skillData.type}");
                return;
            }

            var context = new SkillApplyContext
            {
                player = this,
                weaponHandler = PlayerweaponHandler
            };


            SkillManager.Instance.ApplySkill(effect, context);
#if UNITY_EDITOR
            Debug.Log("��ų�� ���Խ��ϴ� " + skillData.name);
#endif
        }
    }

}*/