using System;

namespace FlagCapturing.Minigames
{
    public interface IMinigame
    {
        event Action OnSuccess;
        event Action OnFailure;

        void Launch();
        void Win();
        void Lose();
    }
}
