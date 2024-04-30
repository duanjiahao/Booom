using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NPCGeneratorFactory
{
    public static INPCGenerator GetNPCGenerator() 
    {
        return new NPCGenerator();
    }
}
