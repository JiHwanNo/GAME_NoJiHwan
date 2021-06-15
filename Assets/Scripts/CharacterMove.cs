using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using TMPro;

public class CharacterMove : MonoBehaviour, MouseInput.IPlayerActions, IPointerDownHandler
{
    Animator PlayerAni;
    MouseInput mouseInput;

    [Header("Camera")]
    public Camera mycamera;
    public GameObject touchEffect;
    RaycastHit hit;

    [Header("Player")]
    public Transform Player;
    public float moveSpeed = 5f;
    public float rotateSpeed = 200f;
    public float Range;
    NavMeshAgent PlayerNav;

    [Header("Targeting")]
    public Transform target;
    public GameObject target_Tool;
    public TextMeshProUGUI name_Target;
    public GameObject hpBar_Target;
    private void Awake()
    {
        mouseInput = new MouseInput();
        mouseInput.Player.SetCallbacks(this);

        PlayerAni = Player.GetComponent<Animator>();
        PlayerNav = Player.GetComponent<NavMeshAgent>();

    }

    void Start()
    {
        PlayerNav.speed = moveSpeed;
        PlayerNav.angularSpeed = rotateSpeed;
    }
    private void FixedUpdate()
    {
       PlayerAni.SetBool("Walk", PlayerNav.velocity != Vector3.zero);

        OnTarget();
    }
    private void OnEnable()
    {
        mouseInput.Enable();
    }

    private void OnDisable()
    {
        mouseInput.Disable();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(Input.GetMouseButton(0))
        {
            Ray ray = mycamera.ScreenPointToRay(eventData.position);
            Physics.Raycast(ray, out hit);

            if (hit.transform != null)
            {
                touchEffect.SetActive(false);
                touchEffect.transform.position = mycamera.WorldToScreenPoint(hit.point);
                touchEffect.SetActive(true);

                if (hit.transform.gameObject.tag == "Ground")
                {
                    PlayerNav.SetDestination(hit.point);
                }
                if (hit.transform.gameObject.tag == "Player")
                {
                    Manager.instance.myaudio.audioSource.PlayOneShot(Manager.instance.myaudio.button_Click);
                    Manager.instance.inventory.charInfoFrame.SetActive(true);
                }

                if (hit.transform.gameObject.tag == "Enemy")
                {
                    Targeting();
                }
            }
        }
      
       
    }
    // Ÿ���� ���� �Լ�
    void Targeting()
    {
        target = hit.transform;
        name_Target.text = target.GetComponent<Obj_Info>().Obj_Name;
        hpBar_Target.SetActive(target.GetComponent<Obj_Info>().type == "Enemy");
        target_Tool.SetActive(true);
    }
    //Ÿ�� üũ �Լ�
    void OnTarget()
    {
        if(target != null)
        {
            float targetDis = (target.position - Player.position).magnitude;

            if(targetDis > Range)
            {
                target = null;
                target_Tool.SetActive(false);
            }

            if(target_Tool.activeSelf)
            {
                target_Tool.transform.position = mycamera.WorldToScreenPoint(target.position+new Vector3(0,1,0));
            }
        }
    }

    //��ư �Է� �Լ� 2��
    public void OpenInventory()
    {
        Manager.instance.inventory.InvenFrame.SetActive(true);
    }
    public void CloseInventory()
    {
        Manager.instance.inventory.InvenFrame.SetActive(false);
    }

    public void OnSkill1(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnSkill2(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnSkill3(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
