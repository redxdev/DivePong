namespace Pong.Game.States
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dive.Engine;
    using Dive.Engine.Attributes;

    [GameState]
    public class PlayState : AbstractGameState
    {
        public override void Start(IGameState previous)
        {
            GameEngine.Instance.ClearColor = ColorConstants.Black;
        }
    }
}
