using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
namespace SimpleUDONGimmick
{
    public class MicrophoneGimmick : UdonSharpBehaviour
    {
        [Header("最初からONにするか")]
        public bool IsON;
        [Header("声の大きさ  (標準10)")]
        public float m_gain   = 10;
        [Header("声が届く距離（標準40）")]
        public float m_far    = 40;
        [Header("声が減衰せずに100％届く距離（VRCのバグ多いので基本的に触らないほうが無難）")]
        public float m_near   = 0;
        [Header("声が発生する範囲（VRCのバグ多いので基本的に触らないほうが無難）")]
        public float m_radius = 0; 
        [Header("このマイクの当たり判定")]
        public Collider m_collidder;

        [Header("スイッチ切り替え時のSE")]
        public AudioSource m_switch_audioSource;
        public AudioClip m_switch_ON_sound;
        public AudioClip m_switch_OFF_sound;

        [Header("スイッチONの時に表示/非表示するオブジェクト")]
        public GameObject[] m_activeObjects;
        public GameObject[] m_desableObjects;

        [Header("(Ooption) アクセスを許可する人のリスト。空ならば誰でもアクセスできる")]
        public string[] m_membership;

        const float const_gain_default = 10;
        const float const_far_default = 40;
        const float const_near_default = 0;
        const float const_radius_default = 0;

        public override void Interact(){
            if(m_membership.Length > 0){
                bool flg = false;            
                for (int i = 0; i < m_membership.Length; i++)
                {
                    if(m_membership[i] == Networking.LocalPlayer.displayName){
                        flg = true;
                        break;
                    }
                    if(!flg)
                        return;
                }                
            }
            IsON = !IsON;
            if(IsON){
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All,"SyncON");
            }else{
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All,"SyncOFF");
            }
        }

        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            if (!Networking.IsMaster)
                return;
            if (IsON)
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "SyncON_Joined");
            }
            else
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "SyncOFF_Joined");
            }
        }

        public void SyncON()
        {
            IsON = true;
            m_collidder.enabled = IsON;
            if (m_switch_audioSource != null && m_switch_ON_sound != null)
                m_switch_audioSource.PlayOneShot(m_switch_ON_sound);
            for (int i = 0; i < m_activeObjects.Length; i++)
            {
                m_activeObjects[i].SetActive(IsON);
            }
            for (int i = 0; i < m_desableObjects.Length; i++)
            {
                m_desableObjects[i].SetActive(!IsON);
            }
        }

        public void SyncON_Joined()
        {
            IsON = true;
            m_collidder.enabled = IsON;
            for (int i = 0; i < m_activeObjects.Length; i++)
            {
                m_activeObjects[i].SetActive(IsON);
            }
            for (int i = 0; i < m_desableObjects.Length; i++)
            {
                m_desableObjects[i].SetActive(!IsON);
            }
        }

        public void SyncOFF()
        {
            IsON = false;
            m_collidder.enabled = IsON;
            if (m_switch_audioSource != null && m_switch_OFF_sound != null)
                m_switch_audioSource.PlayOneShot(m_switch_OFF_sound);
            for (int i = 0; i < m_activeObjects.Length; i++)
            {
                m_activeObjects[i].SetActive(IsON);
            }
            for (int i = 0; i < m_desableObjects.Length; i++)
            {
                m_desableObjects[i].SetActive(!IsON);
            }
        }

        public void SyncOFF_Joined()
        {
            IsON = false;
            m_collidder.enabled = IsON;
            for (int i = 0; i < m_activeObjects.Length; i++)
            {
                m_activeObjects[i].SetActive(IsON);
            }
            for (int i = 0; i < m_desableObjects.Length; i++)
            {
                m_desableObjects[i].SetActive(!IsON);
            }
        }

        public override void OnPlayerTriggerEnter(VRCPlayerApi player)
        {
            if(m_membership.Length > 0){
                bool flg = false;            
                for (int i = 0; i < m_membership.Length; i++)
                {
                    if(m_membership[i] == player.displayName){
                        flg = true;
                        break;
                    }
                    if(!flg)
                        return;
                }                
            }
            player.SetVoiceDistanceFar(m_far);
            player.SetVoiceDistanceNear(m_near);
            player.SetVoiceGain(m_gain);
            player.SetVoiceVolumetricRadius(m_radius);
        }

        public override void OnPlayerTriggerExit(VRCPlayerApi player)
        {
            if(m_membership.Length > 0){
                bool flg = false;            
                for (int i = 0; i < m_membership.Length; i++)
                {
                    if(m_membership[i] == player.displayName){
                        flg = true;
                        break;
                    }
                    if(!flg)
                        return;
                }                
            }
            player.SetVoiceDistanceFar(const_far_default);
            player.SetVoiceDistanceNear(const_near_default);
            player.SetVoiceGain(const_gain_default);
            player.SetVoiceVolumetricRadius(const_radius_default);
        }
    }
}