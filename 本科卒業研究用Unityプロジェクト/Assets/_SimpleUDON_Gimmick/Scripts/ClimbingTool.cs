using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using System;

namespace SimpleUDONGimmick
{
    public class ClimbingTool : UdonSharpBehaviour
    {   
        [Header("よじ登ることができるオブジェクト")]
        public Collider[] m_climbing_targets;

        [Header("クライミングツールの当たり判定の余裕")]
        public float     m_hit_margin = 0.1f;

        [Header("パワー")]
        public float     m_climing_power = 2;

        [Header("(Option) リセット位置と時間")]
        public Transform      m_reset_position;
        public VRC_Pickup     m_reset_pickup;
        public int            m_reset_time = 30;

        Transform        m_transfrom;
        
        Collider         m_grab_target;
        Vector3          m_base_pos;
        Vector3          m_grap_pos;

        bool             isGrab     = false;
        bool             isPicked   = false;

        DateTime         _reset_time;


        void Start()
        {
            m_transfrom = this.transform;
        }

        public override void OnPickupUseDown(){
            for (int i = 0; i < m_climbing_targets.Length; i++)
            {
                if(m_hit_margin >= (m_transfrom.position - m_climbing_targets[i].ClosestPoint(m_transfrom.position)).magnitude){
                    m_base_pos       = Networking.LocalPlayer.GetPosition();
                    m_grap_pos       = m_transfrom.position;
                    m_grab_target    = m_climbing_targets[i];
                    isGrab           = true;
                    return;
                }
            }            
        }

        public override void OnPickupUseUp(){
            isGrab = false;
        }

        public override void OnDrop(){
            isGrab = false;
        }

        void Update(){
            if(isGrab && m_grab_target != null &&  m_hit_margin >= (m_transfrom.position - m_grab_target.ClosestPoint(m_transfrom.position)).magnitude){
                Networking.LocalPlayer.SetVelocity((m_grap_pos - m_transfrom.position) * m_climing_power);
            }
            if(m_reset_position != null && m_reset_pickup != null){
                if(m_reset_pickup.IsHeld){
                    isPicked      = true;
                    _reset_time = DateTime.UtcNow.AddSeconds( m_reset_time );
                }else if(DateTime.UtcNow > _reset_time && isPicked){
                    isPicked        = false;
                    this.transform.position = m_reset_position.position;
                    this.transform.rotation = m_reset_position.rotation;
                }
            }
        }
    }

}