using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;

// UI를 참조할 수 있는 PlayerCondition
// 외부에서 능력치 변경 기능은 이곳을 통해서 호출. 내부적으로 UI 업데이트 수행.
public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerHealthDecay;   // hunger가 0일때 사용할 값 (value > 0)
    public event Action onTakeDamage;   // Damage 받을 때 호출할 Action (6강 데미지 효과 때 사용)



    private void Update()
    {
        hunger.Subtract(hunger.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if (hunger.curValue == 0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (health.curValue == 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    public IEnumerator StaminaBuffCoroutine(float amount, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            stamina.Add(amount * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }
    public IEnumerator SpeedBuffCoroutine(float amount, float duration)
    {
        float originalSpeed = CharacterManager.Instance.Player.controller.moveSpeed;
        CharacterManager.Instance.Player.controller.moveSpeed = amount;

        yield return new WaitForSeconds(duration);

        CharacterManager.Instance.Player.controller.moveSpeed = originalSpeed;
    }


    public void Die()
    {
        Debug.Log("플레이어가 죽었다.");
    }

    public bool UseStamina(float amount)
    {
        if (stamina.curValue - amount < 0)
        {
            return false;
        }
        stamina.Subtract(amount);
        return true;
    }
}