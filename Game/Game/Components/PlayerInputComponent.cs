namespace Pong.Game.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dive.Engine;
    using Dive.Engine.Components;
    using Dive.Entity;
    using Dive.Entity.Attributes;
    using SFML.Window;
    using SFML.Graphics;

    [EntityComponent(Name = "Pong.PlayerInput", ExecutionLayer = EngineLayers.UpdateInput)]
    public class PlayerInputComponent : AbstractComponent
    {
        private ComponentLookup<TransformComponent> transform = null;
        private ComponentLookup<PaddleComponent> paddle = null;

        public float Speed
        {
            get;
            set;
        }

        public override void Initialize()
        {
            this.transform = new ComponentLookup<TransformComponent>(this.ParentEntity);
            this.paddle = new ComponentLookup<PaddleComponent>(this.ParentEntity);

            this.Speed = 200f;
        }

        public override void Update()
        {
            if (GameEngine.Instance.Input.IsActionActive("up"))
            {
                this.transform.Component.AddY(-(float)(this.Speed * GameEngine.Instance.Delta));
            }

            if (GameEngine.Instance.Input.IsActionActive("down"))
            {
                this.transform.Component.AddY((float)(this.Speed * GameEngine.Instance.Delta));
            }


            float y = this.transform.Component.Position.Y;

            if (y < 0)
            {
                y = 0;
            }
            else if (y > GameEngine.Instance.Window.Size.Y - this.paddle.Component.Paddle.Height)
            {
                y = GameEngine.Instance.Window.Size.Y - this.paddle.Component.Paddle.Height;
            }

            this.transform.Component.SetY(y);
        }
    }
}
