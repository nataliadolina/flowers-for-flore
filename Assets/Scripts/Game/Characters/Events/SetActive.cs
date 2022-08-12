using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActiveOrInactive
{
    Active,
    Inactive
}

public class SetActive : MonoBehaviour
{
    [SerializeField] private ActiveOrInactive switchTo;
    private Dictionary<ActiveOrInactive, bool> typesEvent = null;
    public void Execute()
    {
        typesEvent = new Dictionary<ActiveOrInactive, bool>()
        {
            { ActiveOrInactive.Active, true },
            { ActiveOrInactive.Inactive, false },
        };
        gameObject.SetActive(typesEvent[switchTo]);
    }

 
}
