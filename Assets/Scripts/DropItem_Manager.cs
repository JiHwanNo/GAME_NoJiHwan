using TMPro;
using UnityEngine;

public class DropItem_Manager : MonoBehaviour
{
    // �ڽĵ� �ޱ�.
    Transform[] childs;
    public Transform SoltBox; // �θ�ޱ�

    int Random_Index;
    public GameObject[] Drop_Iven;


    public GameObject GoalText;
    public int getgoal;
    EnemyState enemyState;
    private void Awake()
    {   // �����ϸ� ������ ���� ����
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
        Debug.Log("1");
        GetGoalAmount();
        Random_Index = Random.Range(1, 3); // ���� ���� �����Ѵ�. 1�� Ȥ�� 2��

        for (int i = 0; i <= Random_Index; i++)
        {
            if (i >= 1)
            {
                //����
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
        //Manager.instance.ObjectPool.ClearItem();
        //for (int i = 0; i < Drop_Iven.Length; i++)
        //{
        //    Destroy(Drop_Iven[i]);
        //}
    }

    void GetGoalAmount()
    {
        getgoal = (int)(enemyState.goal * Random.Range(0.8f, 1.2f));
        GoalText.GetComponent<TextMeshProUGUI>().text = getgoal.ToString();

    }


}
