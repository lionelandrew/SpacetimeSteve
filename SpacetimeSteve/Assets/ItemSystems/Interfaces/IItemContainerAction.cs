using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class IItemContainerAction : MonoBehaviour
{
    /// <summary>
    /// Performes the defined action for a Item Container
    /// </summary>
    /// <param name="data">the Item that the action is performed with.</param>
    /// <returns>returns true if the action is Active.</returns>
    public abstract bool PerformAction(ItemData data);
}

