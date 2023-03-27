using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;
using System;

namespace SimpleUDONGimmick
{
    public class JetGimmick : UdonSharpBehaviour
    {
        [Header("ジェット噴射するオブジェクト")]
        public Transform      m_return_point;

        [Header("ジェット噴射するオブジェクト")]
        public Transform      m_body;
        [Header("ジェットのパワー")]
        public float          m_power = 1;
        [Header("ジェットの最大速度")]
        public float          m_max_speed = 1;
        [Header("ジェットの噴射可能時間")]
        public float          m_max_use_time = 1;
        [Header("ジェットのエネルギー回復が始まるまでの時間")]
        public float          m_need_recharge_time = 0.5f;
        [Header("ジェットの完全回復までにかかる時間")]
        public float          m_chageing_time = 1;
        [Header("(Option) ジェットのエフェクト")]
        public ParticleSystem m_particle;
        [Header("(Option) ジェットのエネルギー残量表示")]
        public Image          m_show_energy;
        [Header("(Option) ジェットの噴射音(ループ音を推奨)")]
        public AudioSource    m_sound;
        [Header("(Option) リセット位置と時間")]
        public Transform      m_reset_position;
        public VRC_Pickup     m_reset_pickup;
        public int            m_reset_time = 30;

        bool   IsUse = false;

        float _energy ;
        float _rechage_rate = 0;
        float _timer        = 0;
        bool  isPicked      = false;

        DateTime _reset_time;

        void Start(){
            _energy = m_max_use_time;
            if(m_max_use_time > 0 && m_chageing_time > 0)
                _rechage_rate = m_max_use_time / m_chageing_time;
        }

        public override void OnPickupUseDown()
        {
            IsUse = true;
            if(m_particle != null)
                m_particle.Play();
            if(m_sound != null)
                m_sound.Play();
        }
        public override void OnPickupUseUp()
        {
            IsUse = false;
            if(m_particle != null)
                m_particle.Stop();
            if(m_sound != null)
                m_sound.Stop();
        }

        void Update()
        {
            if (IsUse && _energy > 0)
            {
                Vector3 vector = Networking.LocalPlayer.GetVelocity();
                vector += (m_body.up * m_power);
                if(vector.magnitude > m_max_speed){
                   vector = vector.normalized * m_max_speed;
                }
                Networking.LocalPlayer.SetVelocity(vector);
                _energy -= Time.deltaTime;
                _timer  =  m_need_recharge_time;
                if(_energy <= 0){
                    if(m_particle != null)
                        m_particle.Stop();
                    if(m_sound != null)
                        m_sound.Play();
                }
            }else if(_timer > 0){
                _timer -= Time.deltaTime;
            }else if(_energy < m_max_use_time){
                _energy += Time.deltaTime * _rechage_rate;
                if(_energy > m_max_use_time)
                    _energy = m_max_use_time;
            }
            if(m_show_energy != null)
                m_show_energy.fillAmount = _energy / m_max_use_time;
            if(m_reset_position != null && m_reset_pickup != null){
                if(m_reset_pickup.IsHeld){
                    isPicked      = true;
                    _reset_time = DateTime.UtcNow.AddSeconds( m_reset_time );
                }else if(DateTime.UtcNow > _reset_time && isPicked){
                    isPicked        = false;
                    m_body.position = m_reset_position.position;
                    m_body.rotation = m_reset_position.rotation;
                }
            }
        }
    }
}
