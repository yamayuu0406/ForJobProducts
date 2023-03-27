using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Components;

namespace SimpleUDONGimmick
{
    public class ObjectsPool : UdonSharpBehaviour
    {
        [Header("同期するか")]
        public bool           IsSync;
        [Header("取り出すときに動きをリセットするか")]
        public bool         IsResetVelocity;
        [Header("保持するオブジェクト")]
        public GameObject[] m_poolObjects;
        [Header("取り出す位置")]
        public Transform    m_locations;

        Rigidbody[] _rigidbody;

        void Start()
        {
            
            if (IsResetVelocity)
                _rigidbody = new Rigidbody[m_poolObjects.Length];
            for (int i = 0; i < m_poolObjects.Length; i++)
            {
                m_poolObjects[i].SetActive(false);
                if (IsResetVelocity)
                {
                    _rigidbody[i] = m_poolObjects[i].GetComponent<Rigidbody>();
                }
            }         
        }

        public override void Interact()
        {
            if (IsSync)
            {
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "SyncActive");
                return;
            }
            for (int i = 0; i < m_poolObjects.Length; i++)
            {
                if (!m_poolObjects[i].activeSelf)
                {
                    m_poolObjects[i].transform.position = m_locations.position;
                    m_poolObjects[i].transform.rotation = m_locations.rotation;
                    if (IsResetVelocity && _rigidbody[i] != null)
                    {
                        _rigidbody[i].velocity = Vector3.zero;
                        _rigidbody[i].angularVelocity = Vector3.zero;
                    }
                    m_poolObjects[i].SetActive(true);
                    return;
                }
            }
        }

        public void SyncActive()
        {
            for (int i = 0; i < m_poolObjects.Length; i++)
            {
                if (!m_poolObjects[i].activeSelf)
                {
                    m_poolObjects[i].transform.position = m_locations.position;
                    m_poolObjects[i].transform.rotation = m_locations.rotation;
                    if (IsResetVelocity && _rigidbody[i] != null)
                    {
                        _rigidbody[i].velocity = Vector3.zero;
                        _rigidbody[i].angularVelocity = Vector3.zero;
                    }
                    m_poolObjects[i].SetActive(true);
                    return;
                }
            }
        }
    }
}
