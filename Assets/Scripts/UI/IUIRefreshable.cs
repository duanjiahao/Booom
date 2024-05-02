using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIRefreshable
{
    public void RefreshUI(params object[] data);
}
