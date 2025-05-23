using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.XR;

// UI�� ������ �� �ִ� PlayerCondition
// �ܺο��� �ɷ�ġ ���� ����� �̰��� ���ؼ� ȣ��. ���������� UI ������Ʈ ����.
public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float noHungerHealthDecay;   // hunger�� 0�϶� ����� �� (value > 0)
    public event Action onTakeDamage;   // Damage ���� �� ȣ���� Action (6�� ������ ȿ�� �� ���)



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
        Debug.Log("�÷��̾ �׾���.");
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