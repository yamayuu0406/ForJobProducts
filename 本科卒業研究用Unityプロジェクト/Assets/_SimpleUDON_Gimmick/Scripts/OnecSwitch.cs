using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace SimpleUDONGimmick
{
    public class OnecSwitch : UdonSharpBehaviour
    {
        [Header("同期するか")]
        public bool IsSync;

        [Header("スイッチON/OFFの表示用")]
        public GameObject m_switch_ON;
        public GameObject m_switch_OFF;

        [Header("スイッチ切り替え時のSE")]
        public AudioSource m_switch_audioSource;
        public AudioClip   m_switch_ON_sound;

        [Header("スイッチONの時に表示/非表示するオブジェクト")]
        public GameObject[] m_activeObjects;
        public GameObject[] m_desableObjects;

        bool IsON = false;

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
        void OnEnable(){
            SyncSwitchOFF();
        }


        public override void Interact()
        {
            IsON = !IsON;
            if (IsON)
            {
                if (IsSync)
                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "SyncSwitchON");
                else
                    SyncSwitchON();
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
        }


        public void SyncSwitchON()
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

        public void SyncSwitchOFF()
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