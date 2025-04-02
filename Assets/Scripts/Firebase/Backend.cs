using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Backend : MonoBehaviour
{
    public static Backend self;
    public RealtimeAPI realtimeAPI;
    public LudoOnline ludoOnline;
    // Start is called before the first frame update

    private void Awake()
    {
        self = this;
    }

    void Start()
    {
        
    }


    public void sendBlueDice()
    {
        realtimeAPI.Put("blue", new { dice = ludoOnline.bluePlayer.dice,
        clicked = true
        });
    }

    public void sendBluePice()
    {
        realtimeAPI.Put("blue", new
        {
            pice = ludoOnline.bluePlayer.pice,
            clicked = true
        });
    }

    public bool checkUpdate(string path)
    {
        bool r = false;
        realtimeAPI.IsFieldUpdated(path, "clicked", false, (updated) =>
        {
            r = updated;


        });
        return r;
    }

        



    // Update is called once per frame
    void Update()
    {
        
    }
}
