using System.Collections.Generic;
using Network.Services.RoomHandlers;
using Network.Services.Snakes;
using Reflex.Attributes;
using Services;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class WorldSpaceUsernameLabels : MonoBehaviour
    {
        [SerializeField] private UIDocument _ui;
        [SerializeField] private VisualTreeAsset _usernameLabelTree;
        [SerializeField] private bool _showSelfUsername = true;

        private INetworkStatusProvider _status;
        private SnakesRegistry _snakes;
        private CameraProvider _cameraProvider;
        private StaticDataService _staticData;
        
        private VisualElement _root;
        private readonly Dictionary<string, UsernameLabel> _labels = new();

        [Inject]
        public void Construct(INetworkStatusProvider status, SnakesRegistry snakes, CameraProvider cameraProvider, 
            StaticDataService staticData)
        {
            _status = status;
            _snakes = snakes;
            _cameraProvider = cameraProvider;
            _staticData = staticData;
        }

        private void Awake()
        {
            _root = new VisualElement();
            _ui.rootVisualElement.Add(_root);
        }

        private void OnEnable() => 
            _snakes.Updated += OnSnakedUpdated;

        private void OnDisable() => 
            _snakes.Updated -= OnSnakedUpdated;

        private void Update() => 
            MoveUILabels();

        private void MoveUILabels()
        {
            foreach (var (snakeId, snakeInfo) in _snakes.All())
            {
                if (!_labels.TryGetValue(snakeId, out var label))
                    continue;
                
                var headTransform = snakeInfo.Snake.Head.transform;
                var labelPosition = headTransform.position - headTransform.forward;
                var screenPosition = _cameraProvider.Current.WorldToScreenPoint(labelPosition);
                label.SetScreenPosition(screenPosition);
            }
        }

        private void OnSnakedUpdated()
        {
            _root.Clear();
            _labels.Clear();
            
            foreach (var (snakeId, snakeInfo) in _snakes.All())
            {
                if (!_showSelfUsername && _status.IsPlayer(snakeId))
                    continue;
                
                var skin = _staticData.ForSnakeSkin(snakeInfo.Player.skinId);
                var label = new UsernameLabel(_usernameLabelTree);
                label.SetUsername(snakeInfo.Player.username);
                label.SetLabelColor(skin.color);
                _labels[snakeId] = label;
                _root.Add(label);
            }
        }
    }
}