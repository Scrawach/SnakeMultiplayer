using UnityEngine;

namespace Gameplay
{
    public class Apple : MonoBehaviour
    {
        public void Collect()
        {
            Debug.Log("COLLECT!");
            gameObject.SetActive(false);
        }
    }
}