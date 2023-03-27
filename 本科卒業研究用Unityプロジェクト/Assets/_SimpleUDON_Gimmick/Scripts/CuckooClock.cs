using System;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace SimpleUDONGimmick
{
    public class CuckooClock : UdonSharpBehaviour
    {
        [Header("どの地域のタイムゾーンを使うか")]
        public string       m_byTime = "Tokyo Standard Time";
        [Header("時針")]
        public Transform    m_hourHand;
        [Header("分針")]
        public Transform    m_minuteHand;
        [Header("秒針")]
        public Transform    m_secoundHand;
        [Header("1時間ごとにアクティブされるオブジェクト")]
        public GameObject[] m_hourTriggers = new GameObject[24];

        int m_last_time = 0;

        void Start()
        {
            DateTime now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(m_byTime));
            m_last_time = now.Hour;
            for (int i = 0; i < m_hourTriggers.Length; i++)
            {
                if (m_hourTriggers[i] != null)
                    m_hourTriggers[i].SetActive(false);
            }
        }

        void Update()
        {
            DateTime now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(m_byTime));
            m_hourHand.localRotation = Quaternion.Euler(0, 0, now.Hour * 30 + now.Minute * 0.5f);
            m_minuteHand.localRotation = Quaternion.Euler(0, 0, now.Minute * 6 + now.Second * 0.1f);
            m_secoundHand.localRotation = Quaternion.Euler(0, 0, now.Second * 6 + now.Millisecond * 0.006f);
            if (m_last_time != now.Hour)
            {
                if (m_hourTriggers[m_last_time] != null)
                    m_hourTriggers[m_last_time].SetActive(false);
                m_last_time = now.Hour;
                if (m_hourTriggers[now.Hour] != null)
                    m_hourTriggers[now.Hour].SetActive(true);
            }
        }
    }
}