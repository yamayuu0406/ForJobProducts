﻿
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace SimpleUDONGimmick
{
    public class SimpleTrigger : UdonSharpBehaviour
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

        public override void OnPlayerTriggerEnter(VRCPlayerApi player){
            if(!player.isLocal && (IsSync))
                return;
            IsON = !IsON;
            if (IsON)
            {
                SyncON();
            }
            else
            {
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