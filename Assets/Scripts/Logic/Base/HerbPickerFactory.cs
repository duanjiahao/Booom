using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HerbPickerFactory
{
    private static IHerbPicker _HerbPickerSingleton;

    public static IHerbPicker GetHerbPicker()
    {
        if (_HerbPickerSingleton == null) 
        {
            _HerbPickerSingleton = new HerbPicker();
            _HerbPickerSingleton.Init();
        }

        return _HerbPickerSingleton;
    }
}
