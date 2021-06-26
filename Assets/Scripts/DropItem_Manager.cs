using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropItem_Manager : MonoBehaviour
{
    // 자식들 받기.
    Transform[] childs;
    public Transform SoltBox; // 부모받기

    int Random_Index;
    GameObject[] Drop_Iven;


    public GameObject GoalText;
    int getgoal;
    EnemyState enemyState;
    private void Awake()
    {   // 시작하면 아이템 하위 생성
        Drop_Iven = new GameObject[transform.GetChild(1).childCount];
        childs = new Transform[3];
        for(int i = 0; i< 3; i++)
        {
            childs[i] = SoltBox.GetChild(i);
        }
        enemyState = Manager.instance.characterMove.target.GetComponent<EnemyState>();
    }
    private void OnEnable()
    {
        GetGoalAmount();
           Random_Index = Random.Range(1, 3);
        for (int i = 0; i <= Random_Index; i++)
        {
            
            if(i >=1)
            {
                //생성
                Drop_Iven[i] = Manager.instance.ObjectPool.GetItem();
                Drop_Iven[i].transform.SetParent(childs[i]);
                Drop_Iven[i].transform.position = Drop_Iven[i].transform.parent.position;
                Drop_Iven[i].SetActive(true);
            }
        }
    }
    private void OnDisable()
    {
        Manager.instance.ObjectPool.ClearItem();
        for (int i = 0; i < Drop_Iven.Length; i++)
        {
            Destroy(Drop_Iven[i]);
        }
    }

    void GetGoalAmount()
    {
        getgoal = (int)(enemyState.goal * Random.Range(0.8f, 1.2f));
        GoalText.GetComponent<TextMeshProUGUI>().text = getgoal.ToString();

    }


}
