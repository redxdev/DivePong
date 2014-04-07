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

    [EntityComponent(Name = "Pong.AI", ExecutionLayer = EngineLayers.UpdatePhysicsPositions)]
    public class AIComponent : AbstractComponent
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

            this.Speed = 315f;
            this.transform.Component.SetX(GameEngine.Instance.Window.Size.X - this.paddle.Component.Paddle.Width);
        }

        public override void Update()
        {
            Entity ballEnt = GameEngine.Instance.EntityManager.GetEntityByName("ball");
            if (ballEnt == null)
                return;

            TransformComponent ballTransform = ballEnt.GetComponent<TransformComponent>();

            float middle = this.transform.Component.Position.Y + (this.paddle.Component.Paddle.Height / 2);
            if (middle < ballTransform.Position.Y)
            {
                this.transform.Component.AddY((float)(this.Speed * GameEngine.Instance.Delta));
            }
            else if (middle > ballTransform.Position.Y)
            {
                this.transform.Component.AddY(-(float)(this.Speed * GameEngine.Instance.Delta));
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
