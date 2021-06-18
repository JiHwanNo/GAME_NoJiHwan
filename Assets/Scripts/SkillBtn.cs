using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SkillBtn : MonoBehaviour,MouseInput.IPlayerActions
{
    MouseInput mouseInput;

    public string[] SkiilName;
    float cooldown;
    public float[] coolTime;
    public Image[] CTimage;
    public bool cool;
    public float[] castingTime;
    public GameObject[] skillObj;
    private void Awake()
    {
        mouseInput = new MouseInput();
        mouseInput.Player.SetCallbacks(this);
    }

    private void OnEnable()
    {
        mouseInput.Player.Enable();
    }
    private void OnDisable()
    {
        mouseInput.Player.Disable();
    }



    public void OnClickBtn(int index)
    {
        if(index == 2)//힐
        {
            if (!cool && !Manager.instance.characterMove.onCasting)
            {
                cool = true;
                StartCoroutine(CoolDown(index));
                Manager.instance.characterMove.Casting(castingTime[index], SkiilName[index], skillObj[index]);
            }
        }
        else // 그외 공격마법
        {
            if (!cool && Manager.instance.characterMove.target != null && Manager.instance.characterMove.target.gameObject.tag == "Enemy"
            && !Manager.instance.characterMove.onCasting)
            {
                cool = true;
                StartCoroutine(CoolDown(index));
                Manager.instance.characterMove.Casting(castingTime[index], SkiilName[index], skillObj[index]);
            }
        }
    }
 

    IEnumerator CoolDown(int index)
    {
        cooldown = 0;

        while (true)
        {
            cooldown += Time.deltaTime;
            CTimage[index].fillAmount = cooldown / coolTime[index];

            if (cooldown >= coolTime[index])
            {
                cool = false;
                StopCoroutine("CoolDown");
            }

            yield return null;
        }
    }


    public void OnSkill_1(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            OnClickBtn(0);
        }
    }

    public void OnSkill_2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnClickBtn(1);
        }
    }

    public void OnSkill_3(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnClickBtn(2);
        }
    }
}
