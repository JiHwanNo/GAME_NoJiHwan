using UnityEngine;

public class Questdata 
{
    public string questName;
    public int[] NpcId;

    public Questdata(string name, int[] npc)
    {
        questName = name;
        NpcId = npc;
    }
}
