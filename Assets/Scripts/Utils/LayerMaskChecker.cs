using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LayerMaskChecker
{
    public static bool IsContain(this LayerMask layerMask, int layer)
    {
        return ((1 << layer) & layerMask) != 0;
    }
}
