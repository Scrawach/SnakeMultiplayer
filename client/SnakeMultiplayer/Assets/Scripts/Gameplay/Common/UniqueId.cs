using UnityEngine;

namespace Gameplay.Common
{
    public class UniqueId : MonoBehaviour
    {
        [field: SerializeField] public string Value { get; private set; }

        public void Construct(string id) => 
            Value = id;
    }
}