using TMPro;
using UnityEngine;

public class DropItem_Manager : MonoBehaviour
{
    // 자식들 받기.
    Transform[] childs;
    public Transform SoltBox; // 부모받기

    int Random_Index;
    public GameObject[] Drop_Iven;


    public GameObject GoalText;
    public int getgoal;
    EnemyState enemyState;

    private void Awake()
    {   // 시작하면 아이템 하위 생성
        Drop_Iven = new GameObject[transform.GetChild(1).childCount];
        childs = new Transform[3];
        for (int i = 0; i < 3; i++)
        {
            childs[i] = SoltBox.GetChild(i);
        }
        enemyState = Manager.instance.characterMove.target.GetComponent<EnemyState>();
    }
    private void OnEnable()
    {
        GetGoalAmount();
        Random_Index = Random.Range(1, 3); // 득템 수를 결정한다. 1개 혹은 2개

        for (int i = 0; i <= Random_Index; i++)
        {
            if (i >= 1)
            {
                //생성
                Drop_Iven[i] = Manager.instance.ObjectPool.GetItem();
                if (Drop_Iven[i] == null)
                {
                    break;
                }
                Drop_Iven[i].transform.SetParent(childs[i]);
                Drop_Iven[i].transform.position = Drop_Iven[i].transform.parent.position;
                Drop_Iven[i].SetActive(true);
            }
        }
    }
    private void OnDisable()
    {
        Manager.instance.ObjectPool.ClearItem();

        for (int i = 1; i < 3; i++)
        {
            if(SoltBox.GetChild(i).childCount == 1)
            {
                Destroy(SoltBox.GetChild(i).GetChild(0).gameObject);
            }
        }
        Manager.instance.characterMove.target.GetComponent<EnemyHit>().is_get = true;
    }

    void GetGoalAmount()
    {
        Transform parent = GoalText.transform.parent;
        if(!parent.gameObject.activeSelf)
        {
            parent.gameObject.SetActive(true);
        }
        getgoal = (int)(enemyState.goal * Random.Range(0.8f, 1.2f));
        GoalText.GetComponent<TextMeshProUGUI>().text = getgoal.ToString();

    }


}
