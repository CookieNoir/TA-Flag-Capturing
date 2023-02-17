using FlagCapturing.Flags;
using FlagCapturing.Core;

namespace FlagCapturing.Connectors
{
    public class FlagAndGameConnector
    {
        FlagSpawner _flagSpawner;
        Game _game;

        public FlagAndGameConnector(FlagSpawner flagSpawner, Game game)
        {
            _flagSpawner = flagSpawner;
            _game = game;

            _flagSpawner.OnFlagSpawned += _ReactOnCapturing;
        }

        private void _ReactOnCapturing(Flag flag)
        {
            flag.Capturable.OnCaptured += _game.FlagCaptured;
        }
    }
}
