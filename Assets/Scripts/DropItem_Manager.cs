using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropItem_Manager : MonoBehaviour
{
    // �ڽĵ� �ޱ�.
    int RandomIndex;
    GameObject[] Drop_Iven;


    private void Awake()
    {   // �����ϸ� ������ ���� ����
        Drop_Iven = new GameObject[transform.childCount];

    }
    private void OnEnable()
    {
        RandomIndex = Random.Range(0, 2);
        for (int i = 0; i < RandomIndex; i++)
        {
            
        }
        if(RandomIndex == 1)
        {
            Drop_Iven[1] = Manager.instance.ObjectPool.GetItem();
            Drop_Iven[1].transform.parent = transform;
        }
    }

    
}
