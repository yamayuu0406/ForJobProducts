
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PickUpCube : UdonSharpBehaviour
{
    public GameObject FlameThrower;
    public GameObject PickUpCube2;
    public GameObject PickUpCube3;
    public GameObject PickUpCube4;
    public GameObject PickUpCube5;
    public AudioSource AudioSource;
    [UdonSynced(UdonSyncMode.None)]
    private float height;
    [UdonSynced(UdonSyncMode.None)]
    private float height2;
    [UdonSynced(UdonSyncMode.None)]
    private float height3;
    [UdonSynced(UdonSyncMode.None)]
    private float height4;
    [UdonSynced(UdonSyncMode.None)]
    private float height5;
    private int join;

    void Update()
    {
        height = this.gameObject.transform.position.y;
        height2 = PickUpCube2.gameObject.transform.position.y;
        height3 = PickUpCube3.gameObject.transform.position.y;
        height4 = PickUpCube4.gameObject.transform.position.y;
        height5 = PickUpCube5.gameObject.transform.position.y;

        FlameThrower.transform.localScale = Vector3.one * joiner();
    }

    int joiner(){
        join = 0;
        if(height>1.2) join += 1;
        if(height2>1.2) join += 1;
        if(height3>1.2) join += 1;
        if(height4>1.2) join += 1;
        if(height5>1.2) join += 1;
        if(join>0) AudioSource.volume = 0.2f * join;
        if(join>5) return 5;
        return join;
    }
}