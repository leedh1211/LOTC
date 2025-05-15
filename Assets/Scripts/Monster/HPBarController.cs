using UnityEngine;
using UnityEngine.UI;

namespace Monster
{
    public class HPBarController : MonoBehaviour
    {
        [SerializeField] private Image fillImage;

        public void SetFill(float ratio)
        {
            fillImage.fillAmount = Mathf.Clamp01(ratio);
        }
    }
}