using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace SimpleUDONGimmick
{
    public class GroupSwitch : UdonSharpBehaviour
    {
        [Header("プレイヤー間で同期するか")]
        public bool        IsSync;
        [Header("最初からONの状態にするか")]
        public bool        IsON;
        [Header("パーティクルが衝突した時もスイッチを動作させるか")]
        public bool        UseParticleCollision;
        [Header("Triggerが衝突した時もスイッチを動作させるか")]
        public bool        UseTrigger;


        [Header("共有参照するステートオブジェクト")]
        public GameObject  m_toggle_state;

        [Header("スイッチON/OFFの時の表示")]
        public GameObject  m_switch_ON;
        public GameObject  m_switch_OFF;

        [Header("(Option) スイッチ切り替えの時のSE")]
        public AudioSource m_switch_audioSource;
        public AudioClip   m_switch_ON_sound;
        public AudioClip   m_switch_OFF_sound;
        
        [Header("(Option) スイッチONの時に表示/非表示するオブジェクト")]
        public GameObject[] m_activeObjects;
        public GameObject[] m_desableObjects;

        float       _timer = 0;

        const float const_interval = 0.5f;

        void OnParticleCollision(GameObject col)
        {
            if(UseParticleCollision)
                TriggerAction();
        }
        void OnTriggerEnter(Collider col)
        {
            if(UseTrigger)
                TriggerAction();
        }
        public override void Interact()
        {
            TriggerAction();
        }

        void TriggerAction()
        {
            if (Time.time - _timer < const_interval)
                return;
            if (IsSync)
            {
                if (IsON)
                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "ToggleOFF");
                else
                    SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "ToggleON");
            }
            else
            {
                _timer = Time.time;
                IsON  = !IsON;
                ToggleState();
            }
        }
        public void ToggleState()
        {
            m_toggle_state.SetActive(IsON);
            m_switch_ON   .SetActive(IsON);
            m_switch_OFF  .SetActive(!IsON);

            if (IsON)
            {
                if (m_switch_audioSource != null && m_switch_ON_sound != null)
                    m_switch_audioSource.PlayOneShot(m_switch_ON_sound);
            }
            else
            {
                if (m_switch_audioSource != null && m_switch_OFF_sound != null)
                    m_switch_audioSource.PlayOneShot(m_switch_OFF_sound);
            }           
            for (int i = 0; i < m_activeObjects.Length; i++)
            {
                m_activeObjects[i].SetActive(IsON);
            }
            for (int i = 0; i < m_desableObjects.Length; i++)
            {
                m_desableObjects[i].SetActive(!IsON);
            }
        }
        public void ToggleON()
        {
            _timer = Time.time;
            IsON  = true;
            ToggleState();
        }

        public void ToggleOFF()
        {
            _timer = Time.time;
            IsON  = false;
            ToggleState();
        }
        // Start is called before the first frame update
        void Start()
        {
            m_toggle_state.SetActive(IsON);

            m_switch_ON .SetActive(IsON);
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

        void Update()
        {
            if (m_toggle_state.activeSelf == IsON)
                return;
            IsON = m_toggle_state.activeSelf;
            ToggleChange();

        }


        void ToggleChange()
        {
            _timer = Time.deltaTime;

            m_switch_ON.SetActive(IsON);
            m_switch_OFF.SetActive(!IsON);
        }
    }
}