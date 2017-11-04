using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{

    public Behaviour[] componentsToDisable;

    void Start()
    {
        if (!isLocalPlayer)
        {
            for (int i = 0; i < componentsToDisable.Length; i++)
            {
                componentsToDisable[i].enabled = false;
            }
            DisableAllAnimators(transform);
        }

    }

    private void DisableAllAnimators(Transform t)
    {
        foreach (var animator in  t.gameObject.GetComponents<Animator>())
        {
            animator.enabled = false;
        }
        foreach (object obj in t)
        {
            Transform child = (Transform)obj;
            DisableAllAnimators(child);
        }
    }
}
