using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
namespace SimpleUDONGimmick
{
    public class PickupMicrophoneGimmick : UdonSharpBehaviour
    {
        [UdonSynced]
        public int  UsePlayerId = -1;

        [Header("声の大きさ  (標準10)")]
        public float m_gain   = 10;
        [Header("声が届く距離（標準40）")]
        public float m_far    = 1000;
        [Header("声が減衰せずに100％届く距離（VRCのバグ多いので基本的に触らないほうが無難）")]
        public float m_near   = 0;
        [Header("声が発生する範囲（VRCのバグ多いので基本的に触らないほうが無難）")]
        public float m_radius = 0; 

        [Header("(Option) スイッチ切り替え時のSE")]
        public AudioSource m_switch_audioSource;
        public AudioClip m_switch_ON_sound;
        public AudioClip m_switch_OFF_sound;

        [Header("(Option) スイッチONの時に表示/非表示するオブジェクト")]
        public GameObject[] m_activeObjects;
        public GameObject[] m_desableObjects;

        const float const_gain_default = 10;
        const float const_far_default = 40;
        const float const_near_default = 0;
        const float const_radius_default = 0;

        public override void OnPickup(){
            UsePlayerId = Networking.LocalPlayer.playerId;
            RequestSerialization();
        }
        public override void OnPickupUseDown(){
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All,"SyncON");
            if (m_switch_audioSource != null && m_switch_ON_sound != null)
                m_switch_audioSource.PlayOneShot(m_switch_ON_sound);
        }
        public override void OnPickupUseUp(){
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All,"SyncOFF");
            if (m_switch_audioSource != null && m_switch_OFF_sound != null)
                m_switch_audioSource.PlayOneShot(m_switch_OFF_sound);
        }
        public override void OnDrop(){
            SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All,"SyncOFF");            
        }

        public void SyncON()
        {
            if(UsePlayerId < 0)
                return;                            
            var player = VRCPlayerApi.GetPlayerById(UsePlayerId);
            if(player == null)
                return;
            player.SetVoiceGain(m_gain);
            player.SetVoiceDistanceFar(m_far);
            player.SetVoiceDistanceNear(m_near);           
            player.SetVoiceVolumetricRadius(m_radius);

            for (int i = 0; i < m_activeObjects.Length; i++)
            {
                m_activeObjects[i].SetActive(true);
            }
            for (int i = 0; i < m_desableObjects.Length; i++)
            {
                m_desableObjects[i].SetActive(false);
            }
        }

        public void SyncOFF( )
        {
            if(UsePlayerId < 0)
                return;                            
            var player = VRCPlayerApi.GetPlayerById(UsePlayerId);
            if(player == null)
                return;
            player.SetVoiceGain(const_gain_default);            
            player.SetVoiceDistanceFar(const_far_default);
            player.SetVoiceDistanceNear(const_near_default);         
            player.SetVoiceVolumetricRadius(const_radius_default);
            for (int i = 0; i < m_activeObjects.Length; i++)
            {
                m_activeObjects[i].SetActive(false);
            }
            for (int i = 0; i < m_desableObjects.Length; i++)
            {
                m_desableObjects[i].SetActive(true);
            }
        }
    }
}