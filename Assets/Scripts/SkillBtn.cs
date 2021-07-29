using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SkillBtn : MonoBehaviour, MouseInput.IPlayerActions
{
    MouseInput mouseInput;

    public string[] SkiilName;
    float cooldown;
    public float[] coolTime;
    public Image[] CTimage;
    public bool[] cool;
    public float[] castingTime;
    public GameObject[] skillObj;
    CharacterMove character;

    public Transform BlinkPoint;
    private void Awake()
    {
        mouseInput = new MouseInput();
        mouseInput.Player.SetCallbacks(this);
        character = Manager.instance.characterMove;
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
        if (index == 2)//��
        {
            if (!cool[index] && !character.onCasting)
            {
                cool[index] = true;
                StartCoroutine(CoolDown(index));
                character.Casting(castingTime[index], SkiilName[index], skillObj[index]);
            }
        }
        else if (index == 1 || index == 0)// �׿� ���ݸ���
        {
            if (!cool[index] && character.target != null && character.target.gameObject.tag == "Enemy" && !character.onCasting)
            {
                cool[index] = true;
                StartCoroutine(CoolDown(index));
                character.Casting(castingTime[index], SkiilName[index], skillObj[index]);
            }
            if (!cool[index] && character.target != null && character.target.gameObject.tag == "Boss" && !character.onCasting)
            {
                cool[index] = true;
                StartCoroutine(CoolDown(index));
                character.Casting(castingTime[index], SkiilName[index], skillObj[index]);
            }
        }
        // ��ũ
        else if (index ==3) 
        {
            if (!cool[index] && !character.onCasting)
            {
                skillObj[3].SetActive(true);
                character.Player.position = BlinkPoint.position;
                StartCoroutine("Blink");
                StartCoroutine(CoolDown(index));
                character.PlayerNav.SetDestination(character.Player.transform.position);
            }
        }
        //�ǵ�
        else if(index ==4)
        {
            skillObj[4].SetActive(true);
            skillObj[4].GetComponent<Skill_Effect>().StartSheild();
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
                cool[index] = false;
                break;
            }
            yield return null;
        }
    }


    public void OnSkill_1(InputAction.CallbackContext context)
    {
        if (context.performed)
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

    public void OnSkill_4(InputAction.CallbackContext context)// ��ũ ��ų ����
    {
        if (context.performed)
        {
            OnClickBtn(3);
        }
    }

    public void OnSpace(InputAction.CallbackContext context) // �ǵ� ��ų ����
    {
        if(context.started)
        {
            OnClickBtn(4);
        }
        else if(context.canceled)
        {
            skillObj[4].SetActive(false);
            skillObj[4].GetComponent<Skill_Effect>().StopSheild();
        }
        
    }

    IEnumerator Blink()
    {
        character.PlayerAni.Play("Player_Casting");
        character.onCasting = true;
        float time = 0;
        while (true)
        {
            time += Time.deltaTime;
        
            skillObj[3].transform.position = character.Player.position;

            if(time >= 1f)
            {
                skillObj[3].SetActive(false);
                StopCoroutine("Blink");
                character.PlayerAni.Play("Idle");
                character.onCasting = false;
            }

            yield return null;
        }
    }
}
