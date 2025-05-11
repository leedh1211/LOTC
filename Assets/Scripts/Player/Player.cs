using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] private PlayerVisual playerVisual;
    private PlayerData playerData;

    [Header("WeaPon")]
    private WeaponHandler weapon;

    private PlayerData PlayerData => playerData;
    
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

        SkillManager.Instance.ApplySkill(effect,context);


    
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