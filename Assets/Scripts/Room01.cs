using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room01 : MonoBehaviour {

    public ColorLightUp[] AllLightsOn;
    
    public GameObject NextRoom;
    
    private void Start()
    {
    
        NextRoom.SetActive(false);
        //NextRoom.SetActive(true);
        AllLightsOn = GetComponentsInChildren<ColorLightUp>();
    }

    private void Update()
    {
        if (AreAllLightsOn() == true)
        {
            GameManager.GM.OpenRoom("Room01");
    
            NextRoom.SetActive(true);
            //Invoke("DestroyRoom", 5);
        }
            
    }

    private bool AreAllLightsOn()
    {
        for (int i = 0; i < AllLightsOn.Length; i++)
        {
            if (AllLightsOn[i].IsActive == false)
                return false;                
        }
        return true;
    }

    void DestroyRoom()
    {
        Destroy(gameObject);       
    }
}
