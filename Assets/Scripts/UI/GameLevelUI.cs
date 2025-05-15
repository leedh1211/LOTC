using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameLevelUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Image expImage;
    
    
    //이 클래스는 외부라는 느낌보다 메인 컨트롤러의 구성요소라는 느낌이 강하기때문에
    //이벤트 사용보다는 메인 컨트롤러에서 실행시켜주는게 더 좋을것같아요
    //마치 Text랑 Image가 같이 있는 클래스를 MainGameController 에서 실행시켜주는 느낌
    
    /*[SerializeField] private IntegerEventChannelSO levelUp;
    
    [SerializeField] private FloatEventChannelSO gainExp;*/
    
    public void InitLevelText(int targetLevel) => levelText.text =  "Lv." + targetLevel.ToString();
    public void FillExpImage(float currentExp, float maxExp) => expImage.fillAmount = currentExp / maxExp;




    //슬라이더는 도트 이미지에서 픽셀이 뭉개질수도 있고
    //마우스로 직접 슬라이더를 통제하는 상황이 아니면 너무 번잡해요
    
    //[SerializeField] private Slider slider;



    //UI는 UI의 역할만!
    /*private int displayLevel = 1;
    private float _currentExp = 0;
    private float _maxExp = 20;
    [SerializeField] private GameObject skillSelect;
    [SerializeField] private float testGainExp;*/


    //여러개의 경험치가 각각 거의 동시에 들어오는 상황에 
    //애니메이션이 대응하기 힘들다고 판단되어 사용되지 못했습니다.
    /*private Coroutine _sliderCoroutine;

    private void OnGainExp(float gainExp)
    {
        if (_sliderCoroutine != null)
        {
            StopCoroutine(_sliderCoroutine);
            _sliderCoroutine = null;
        }

        float targetExp = _currentExp + gainExp;

        if (targetExp >= _maxExp)
        {
            float overflowExp = targetExp - _maxExp;

            _sliderCoroutine = StartCoroutine(SmoothSliderUpdate(_maxExp, overflowExp));
        }
        else
        {
            _sliderCoroutine = StartCoroutine(SmoothSliderUpdate(targetExp, 0));
        }
    }

    private IEnumerator SmoothSliderUpdate(float targetExp, float overflowExp)
    {
        float duration = 0.7f;

        float elapsed = 0f;

        float startValue = slider.value;

        float targetValue = targetExp / _maxExp;


        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float newValue = Mathf.Lerp(startValue, targetValue, elapsed / duration);

            slider.value = newValue;

            yield return null;
        }

        slider.value = targetValue;

        _currentExp = targetExp;

        if (_currentExp >= _maxExp)
        {
            yield return LevelUpRoutine(overflowExp);
        }

        _sliderCoroutine = null;
    }

    private IEnumerator LevelUpRoutine(float overflowExp)
    {
        _currentExp = 0;

        slider.value = 0;

        levelText.text = $"Lv {displayLevel += 1}";


        onLevelUp.Raise();

        yield return new WaitForSeconds(0.2f);

        if (overflowExp > 0)
        {
            yield return new WaitForSeconds(0.1f);

            OnGainExp(overflowExp);
        }
    }*/
}
