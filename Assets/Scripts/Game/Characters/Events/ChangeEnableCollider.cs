using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEnableCollider : MonoBehaviour
{
    private Collider collider;

    void Start()
    {
        collider = GetComponent<Collider>();
    }

    public void Execute()
    {
        collider.enabled = !collider.enabled;
    }
}
