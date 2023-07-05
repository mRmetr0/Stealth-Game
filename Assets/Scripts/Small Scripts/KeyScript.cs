using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class KeyScript : PickUpScript
{
    
    public static event Action OnPickedUp;    
    private void OnDestroy()
    {
        OnPickedUp?.Invoke();
    }
}
