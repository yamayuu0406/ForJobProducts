
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Cube2 : UdonSharpBehaviour
{
    [UdonSynced(UdonSyncMode.None)]
    public float Objectsize = 1.0f;
    void Update()
    {
    }

    public void ScaleDefault(){
        this.gameObject.transform.localScale = Vector3.one;
    }

    // public override void Interact(){

    //     var player = Networking.LocalPlayer;
    //     Networking.SetOwner(player, this.gameObject);
        
    //     if (player.IsOwner(this.gameObject))
    //     {
    //       Objectsize += 0.1f;
    //     }
        
    // }

     public void SerializeData()
    {
        RequestSerialization();       // 同期更新
    }
}
