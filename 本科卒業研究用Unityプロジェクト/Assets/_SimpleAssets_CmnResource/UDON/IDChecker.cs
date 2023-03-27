using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace SimpleUDONGimmick
{
    public class IDChecker : UdonSharpBehaviour
    {
        [Header("判定するID")]
        public string[] m_id;

        [Header("IDが一致したらアクティブ化するオブジェクト / コライダ / メッシュ")]
        public GameObject[] m_active_objects;
        public Collider[] m_active_colliders;
        public Renderer[] m_active_renderers;

        [Header("IDが一致したら非アクティブ化するオブジェクト / コライダ / メッシュ")]
        public GameObject[] m_disable_objects;
        public Collider[] m_disable_colliders;
        public Renderer[] m_disable_renderers;

        void Start()
        {
            for (int i = 0; i < m_id.Length; i++)
            {
                if (m_id[i] == Networking.LocalPlayer.displayName)
                {
                    for (int j = 0; j < m_active_objects.Length; j++)
                    {
                        m_active_objects[j].SetActive(true);
                    }
                    for (int j = 0; j < m_active_colliders.Length; j++)
                    {
                        m_active_colliders[j].enabled = true;
                    }
                    for (int j = 0; j < m_active_renderers.Length; j++)
                    {
                        m_active_renderers[j].enabled = true;
                    }

                    for (int j = 0; j < m_active_objects.Length; j++)
                    {
                        m_disable_objects[j].SetActive(false);
                    }
                    for (int j = 0; j < m_active_colliders.Length; j++)
                    {
                        m_disable_colliders[j].enabled = false;
                    }
                    for (int j = 0; j < m_active_renderers.Length; j++)
                    {
                        m_disable_renderers[j].enabled = false;
                    }
                    return;
                }
            }
            for (int j = 0; j < m_active_objects.Length; j++)
            {
                m_active_objects[j].SetActive(false);
            }
            for (int j = 0; j < m_active_colliders.Length; j++)
            {
                m_active_colliders[j].enabled = false;
            }
            for (int j = 0; j < m_active_renderers.Length; j++)
            {
                m_active_renderers[j].enabled = false;
            }

            for (int j = 0; j < m_active_objects.Length; j++)
            {
                m_disable_objects[j].SetActive(true);
            }
            for (int j = 0; j < m_active_colliders.Length; j++)
            {
                m_disable_colliders[j].enabled = true;
            }
            for (int j = 0; j < m_active_renderers.Length; j++)
            {
                m_disable_renderers[j].enabled = true;
            }
        }
    }
}