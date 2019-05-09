using Help;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private Button _start;

        private void Start()
        {
            this._start.onClick.AddListener(LoadLevelHelper.LoadGame);
        }
    }
}