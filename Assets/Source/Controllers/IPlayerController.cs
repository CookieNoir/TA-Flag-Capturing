using System;
using UnityEngine;

namespace FlagCapturing.Controllers
{
    public interface IPlayerController
    {
        event Action<Vector2> OnAxisValuesChanged;
    }
}
