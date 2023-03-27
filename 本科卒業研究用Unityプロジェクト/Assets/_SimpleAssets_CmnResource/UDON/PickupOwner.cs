
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class PickupOwner : UdonSharpBehaviour
{
    public GameObject[] targets;

    public override void OnPickup(){
        for (int i = 0; i < targets.Length; i++)
        {
            Networking.SetOwner(Networking.LocalPlayer,targets[i]);
        }
    }
}
