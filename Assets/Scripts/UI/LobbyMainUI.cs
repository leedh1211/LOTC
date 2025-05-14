using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMainUI : MonoBehaviour
{
    [SerializeField] private HorizontalSnapScrollView snapScrollView;
    
    [SerializeField] private TextMeshProUGUI stageText;
    [SerializeField] private Text goldText;
    
    [SerializeField] private Image stageImage;
    [SerializeField] private Animator stageAnimator;
    
    [SerializeField] private Button startButton;
    
    
    [Space(10f)]
    [SerializeField] private StageDataTableSO stageDataTable;
    [SerializeField] private IntegerVariableSO selectedStageLevel;
    [SerializeField] private IntegerVariableSO gold;
    [SerializeField] private VoidEventChannelSO onGoldChanged;
    
    private void Awake()
    {
        SetStageInfo(selectedStageLevel.RuntimeValue);
      
        startButton.onClick.AddListener(() => GameManager.Instance.StartMainGame());
        onGoldChanged.OnEventRaised += SetGoldText;
    }

    private void Start()
    {
        snapScrollView.DirectUpdateItemList(1);
        SetGoldText();
    }
    private void OnDestroy()
    {
        onGoldChanged.OnEventRaised -= SetGoldText;
    }

    public void SetStageInfo(int level)
    {
        var targetData = stageDataTable.Datas[level];
        
        stageImage.sprite = targetData.MainImage;

        stageAnimator.runtimeAnimatorController = targetData.MainAnimator;

        stageText.text = targetData.StageName;
    }
    public void SetGoldText()
    {
        goldText.text = gold.RuntimeValue.ToString();
    }
}
