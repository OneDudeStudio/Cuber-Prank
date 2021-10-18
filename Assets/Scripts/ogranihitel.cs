using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ogranihitel : MonoBehaviour
{
    bool isActive = false;
    public Death death;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(isActive != true)
            {
                Debug.Log("SetACTIVE ");
                Invoke("OgranithiteliOn", 2f);
            }
            
            else if(isActive == true)
            {
                Debug.Log("KEKU");
                death.JustDeath();
            }
            

            

        }
    }

    void OgranithiteliOn()
    {
        isActive = true;
    }
}
