using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace SimpleUDONGimmick
{
    public class SimpleSwitch : UdonSharpBehaviour
    {
        [Header("同期するか")]
        public bool IsSync;

        [Header("スイッチON/OFFの表示用")]
        public GameObject m_switch_ON;
        public GameObject m_switch_OFF;

        [Header("最初からONにするか")]
        public bool IsON;

        [Header("スイッチ切り替え時のSE")]
        public AudioSource m_switch_audioSource;
        public AudioClip   m_switch_ON_sound;
        public AudioClip   m_switch_OFF_sound;

        [Header("スイッチONの時に表示/非表示するオブジェクト")]
        public GameObject[] m_activeObjects;
        public GameObject[] m_desableObjects;

        [Header("(Ooption) アクセスを許可する人のリスト。空ならば誰でもアクセスできる")]
        public string[] m_membership;

        // Start is called before the first frame update
        void Start()
        {
            m_switch_ON.SetActive(IsON);
            m_switch_OFF.SetActive(!IsON);
            for (int i = 0; i < m_activeObjects.Length; i++)
            {
                m_activeObjects[i].SetActive(IsON);
            }
            for (int i = 0; i < m_desableObjects.Length; i++)
            {
                m_desableObjects[i].SetActive(!IsON);
            }
        }

        public override void Interact()
        {
            if(m_membership.Length > 0){
                bool flg = false;
                string user_name = Networking.LocalPlayer.displayName;
                for (int i = 0; i < m_membership.Length; i++)
                {
                    if(m_membership[i] == user_name){
                        flg = true;
                        break;
                    }
                }
                if(!flg)
                    return;
            }
            IsON = !IsON;
            if (IsON)
            {
                if (IsSync)
                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "SyncON");
                else
                    SyncON();
            }
            else
            {
                if (IsSync)
                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "SyncOFF");
                else
                    SyncOFF();
            }
        }
        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            if (!IsSync || !Networking.IsMaster)
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
            m_switch_ON.SetActive(IsON);
            m_switch_OFF.SetActive(!IsON);
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
            m_switch_ON.SetActive(IsON);
            m_switch_OFF.SetActive(!IsON);
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
            m_switch_ON.SetActive(IsON);
            m_switch_OFF.SetActive(!IsON);
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
            m_switch_ON.SetActive(IsON);
            m_switch_OFF.SetActive(!IsON);
            for (int i = 0; i < m_activeObjects.Length; i++)
            {
                m_activeObjects[i].SetActive(IsON);
            }
            for (int i = 0; i < m_desableObjects.Length; i++)
            {
                m_desableObjects[i].SetActive(!IsON);
            }
        }
    }
}