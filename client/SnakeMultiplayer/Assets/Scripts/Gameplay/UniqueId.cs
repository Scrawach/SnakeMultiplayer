using UnityEngine;

namespace Gameplay
{
    public class UniqueId : MonoBehaviour
    {
        [field: SerializeField] public string Value { get; private set; }

        public void Construct(string id) => 
            Value = id;
    }
}