using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGenerator : INPCGenerator
{
    public NPCUnit GenerateARandomNPC()
    {
        return new NPCUnit().Init();
    }
}
