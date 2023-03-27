using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace SimpleUDONGimmick
{
    public class AreaSwitch : UdonSharpBehaviour
    {
        [Header("接触誤差の許容範囲")]
        public float m_tolerance_radius = 0.1f;

        [Header("スイッチONする範囲")]
        public Collider m_collider;

        [Header("スイッチON/OFFの表示用")]
        public GameObject m_switch_ON;
        public GameObject m_switch_OFF;

        [Header("最初からONにするか")]
        public bool IsON;

        [Header("スイッチ切り替え時のSE")]
        public AudioSource m_switch_audioSource;
        public AudioClip m_switch_ON_sound;
        public AudioClip m_switch_OFF_sound;

        [Header("スイッチONの時に表示/非表示するオブジェクト")]
        public GameObject[] m_activeObjects;
        public GameObject[] m_desableObjects;

        void Update()
        {
            if (Networking.LocalPlayer != null)
            {
                Vector3 player_pos = Networking.LocalPlayer.GetPosition();
                if (!IsON)
                {
                    
                    if (m_tolerance_radius > Vector3.Distance(m_collider.ClosestPoint(player_pos), player_pos))
                    {
                        IsON = true;
                        ON();
                        return;
                    }
                }
                else
                {
                    if (m_tolerance_radius < Vector3.Distance(m_collider.ClosestPoint(player_pos), player_pos))
                    {
                        IsON = false;
                        OFF();
                        return;
                    }
                  
                }
            }
        }


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

        public void ON()
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

        public void OFF()
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
    }
}