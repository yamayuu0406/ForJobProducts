
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class Cube : UdonSharpBehaviour
{
    [UdonSynced(UdonSyncMode.None)]
    public float RotateSpeed;
    public GameObject Cube2;
    [UdonSynced(UdonSyncMode.None)]
    public int isSiting = 0;
    public bool State = false;
    UdonBehaviour udonSitTarget;
   
    void Start(){
        udonSitTarget = (UdonBehaviour) Cube2.GetComponent(typeof(UdonBehaviour));
    }
    void Update()
    {
        this.gameObject.transform.Rotate(this.gameObject.transform.up * RotateSpeed * Time.deltaTime);
    }

    public override void Interact(){

        var player = Networking.LocalPlayer;
        Networking.SetOwner(player, this.gameObject);
        
        if (player.IsOwner(this.gameObject))
        {
            RotateSpeed += 1;
        }
        
        //udonSitTarget.SendCustomEvent("ScaleDefault");

        //Cube2.transform.localScale = Vector3.one * RotateSpeed/10f;
    }

     public void SerializeData()
    {
        RequestSerialization();       // 同期更新
    }
}
