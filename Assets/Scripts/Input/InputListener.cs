using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Input
{
    public class InputListener : MonoBehaviour,IPointerDownHandler
    {
        public event Action<RaycastHit> OnHit;
        
        [SerializeField] private Camera _camera;

        public void OnPointerDown(PointerEventData eventData)
        {
            Ray ray = _camera.ScreenPointToRay(eventData.position);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                OnHit.Invoke(hit);
            }
        }
    }
}