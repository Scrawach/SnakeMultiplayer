using System.Collections.Generic;
using Network.Services.RoomHandlers;
using Network.Services.Snakes;
using Reflex.Attributes;
using Services;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Usernames
{
    public class UsernameLabelsFitter : MonoBehaviour
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

        private void Start()
        {
            _snakes.Added += OnSnakeAdded;
            _snakes.Removed += OnSnakeRemoved;
        }

        private void OnDestroy()
        {
            _snakes.Added -= OnSnakeAdded;
            _snakes.Removed -= OnSnakeRemoved;
        }

        private void Update() => 
            MoveUILabels();
        
        private void OnSnakeAdded(string snakeId)
        {
            if (_status.IsPlayer(snakeId) && !_showSelfUsername)
                return;

            var snakeInfo = _snakes[snakeId];
            var label = CreateLabel(snakeInfo.Player.username, snakeInfo.Player.skinId);
            _labels[snakeId] = label;
            _root.Add(label);
        }

        private void OnSnakeRemoved(string snakeId)
        {
            if (_status.IsPlayer(snakeId) && !_showSelfUsername)
                return;

            var label = _labels[snakeId];
            _labels.Remove(snakeId);
            _root.Remove(label);
        }

        private void MoveUILabels()
        {
            foreach (var (snakeId, label) in _labels)
            {
                var info = _snakes[snakeId];
                var headTransform = info.Snake.Head.transform;
                var labelWorldPosition = headTransform.position - headTransform.forward;
                var labelScreenPosition = _cameraProvider.Current.WorldToScreenPoint(labelWorldPosition);
                label.SetScreenPosition(labelScreenPosition);
            }
        }

        private UsernameLabel CreateLabel(string username, int skinId)
        {
            var skin = _staticData.ForSnakeSkin(skinId);
            var label = new UsernameLabel(_usernameLabelTree);
            label.SetUsername(username);
            label.SetLabelColor(skin.color);
            return label;
        }
    }
}