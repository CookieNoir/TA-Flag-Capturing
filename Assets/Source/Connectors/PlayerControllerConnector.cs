using FlagCapturing.Entities;
using FlagCapturing.Controllers;

namespace FlagCapturing.Connectors
{
    public class PlayerControllerConnector
    {
        Player _player;
        IPlayerController _controller;

        public PlayerControllerConnector(Player player, IPlayerController playerController)
        {
            _player = player;
            _controller = playerController;
            _controller.OnAxisValuesChanged += _player.Movement.SetMovementDirection;
        }

        ~PlayerControllerConnector()
        {
            _controller.OnAxisValuesChanged -= _player.Movement.SetMovementDirection;
        }
    }
}
