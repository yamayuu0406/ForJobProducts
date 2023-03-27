
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace SimpleUDONGimmick
{
  public class PushbuttonSwitch : UdonSharpBehaviour
    {
        [Header("同期するか")]
        public bool IsSync;
        
        [Header("次にボタンを押下できるまでの間")]
        public float m_intarval = 1;

        [Header(" スイッチ押下時に実行するUDONのCustomEvent")]
        public UdonBehaviour m_udon;
        public string        m_customEvent;


        [Header("(Option) スイッチで動作するアニメーションとパラメータ名")]
        public Animator   m_switch_animation;
        public string     m_animation_name;

        [Header("(Option) スイッチ押下のSE")]
        public AudioSource m_switch_audioSource;
        public AudioClip   m_switch_ON_sound;

        float _time;

        // Start is called before the first frame update
        public override void Interact()
        {
            if(Time.time - _time < m_intarval)
                return;            
            if (IsSync)
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "SyncPush");
            else
                SyncPush();
        }


        public void SyncPush()
        {
            _time = Time.time;
            if(m_switch_animation != null)
                m_switch_animation.SetBool(m_animation_name,true);
            if (m_switch_audioSource != null && m_switch_ON_sound != null)
                m_switch_audioSource.PlayOneShot(m_switch_ON_sound);
            m_udon.SendCustomEvent(m_customEvent);
        }
    }
}