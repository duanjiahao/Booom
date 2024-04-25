using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CommonUtils
{
    public static List<int> RollRange(List<int> weights, int rollTime = 1) 
    {
        List<int> result = new List<int>();

        float amount = 0;
        foreach (var weight in weights)
        {
            amount += weight;
        }

        for (int i = 0; i < rollTime; i++)
        {
            var roll = Random.Range(0, amount);

            for (int j = 0; j < weights.Count; j++)
            {
                if (result.Contains(j)) continue;

                var weight = weights[j];
                if (weight >= roll) 
                {
                    result.Add(j);
                    amount -= weight;
                    break;
                }

                roll -= weight;
            }
        }

        return result;
    }
}
