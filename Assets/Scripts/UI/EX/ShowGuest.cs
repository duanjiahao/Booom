using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowGuest : MonoBehaviour
{
    public GuestBeFound[] effect;
    private void Awake()
    {
        effect = GameObject.FindObjectsOfType<GuestBeFound>();
    }

    private void OnEnable()
    {
        GetNPCAndShow();
    }

    public void GetNPCAndShow()
    {
        NPCItem nowNPC = NPCDataManager.Instance.GetNowNPC();

        foreach (var item in effect)
        {
            item.NotToShow();
        }

        if (nowNPC != null)
        {
            foreach (var item in nowNPC.NpcUnit._needEffectIds)
            {
                int newID = IDSimplification(item);
                for (int i = 0; i < effect.Length; i++)
                {
                    if (newID == effect[i].id)
                        effect[i].ShowGuestMessage(true);
                }
            }

            foreach (var item in nowNPC.NpcUnit._avoidEffectIds)
            {
                int newID = IDSimplification(item);
                for (int i = 0; i < effect.Length; i++)
                {
                    if (newID == effect[i].id)
                        effect[i].ShowGuestMessage(false);
                }
            }

        }

    }

    public int IDSimplification(int id)
    {
        switch (id)
        {
            case 21001:
            case 21101:
                return 11;
            case 21002:
            case 21102:
                return 12;
            case 21003:
            case 21103:
                return 13;
            case 22001:
            case 22101:
                return 21;
            case 22002:
            case 22102:
                return 22;
            case 22003:
            case 22103:
                return 23;
            case 23001:
            case 23101:
                return 31;
            case 23002:
            case 23102:
                return 32;
            case 23003:
            case 23103:
                return 33;
            case 24001:
            case 24101:
                return 41;
            case 24002:
            case 24102:
                return 42;
            case 24003:
            case 24103:
                return 43;
        }
        return 0;
    }
}
