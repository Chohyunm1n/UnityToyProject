using UnityEngine;
using UnityEngine.UI;

public class PlayerPowerMode : MonoBehaviour
{
    [SerializeField]
    private GameObject powerEffect;

    [SerializeField]
    private GameObject powerGameObject;

    [SerializeField]
    private Image powerGauge;

    private float powerAmount = 0;

    public float PowerAmount
    {
        set
        {
            powerAmount = value;

            if (powerAmount >= 1)
            {
                powerAmount = 1;
                IsPowerMode = true;
                powerGauge.color = Color.red;
                powerEffect.SetActive(true);
            }
            
            else if (powerAmount <= 0)
            {
                powerAmount = 0;
                IsPowerMode = false;
                powerGauge.color = Color.white;
                powerEffect.SetActive(false);
                
            }
        }
        get => powerAmount;
    }

    public bool IsPowerMode { private set; get; } = false;

    public void UpdatePowerMode(bool isClicked)
    {
        float increaseAmount = 0.8f; //마우스 클릭 시 게이지 증가량
        float decreaseAmountNormal = 0.5f; // 일반 모드에서 게이지 감소량
        float decreaseAmountPower = 0.3f; // 파워 모드에서 게이지 감소량
        float activateAmount = 0.3f; // 게이지 활성화 조건 값

        if (IsPowerMode)
        {
            // 파워 모드일 때 초당 decreaseAmountPower 만큼 감소
            PowerAmount -= Time.deltaTime * decreaseAmountPower;
            
        }
        else
        {
            // 파워 모드가 아닐 때
            // 마우스 클릭 상태이면 초당 increaseAmount 증가
            // 마우스 클릭 상태가 아니면 decreaseAmountPower 감소
            if (isClicked)
            {

                PowerAmount += Time.deltaTime * increaseAmount;
            }
            else
            {
                PowerAmount -= Time.deltaTime * decreaseAmountNormal;
            }
        }
        
        // 파워 상태가 30% 이상이거나 파워모드 상태이면
        if (PowerAmount >= activateAmount || IsPowerMode)
        {
            powerGameObject.SetActive(true);
        }
        
        // 그렇지 않으면 게이지 비활성화
        else
        {
            powerGameObject.SetActive(false);
        }
        
        // 게이지 업데이트
        powerGauge.fillAmount = PowerAmount;
        
    }

    public void DeactivateAll()
    {
        powerEffect.SetActive(false);
        
        powerGameObject.SetActive(false);
    }

}
