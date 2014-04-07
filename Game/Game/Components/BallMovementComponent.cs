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

    [EntityComponent(Name = "Pong.BallMovement", ExecutionLayer = EngineLayers.UpdatePhysics)]
    public class BallMovementComponent : AbstractComponent
    {
        private ComponentLookup<TransformComponent> transform = null;
        private ComponentLookup<BallComponent> ball = null;

        public float Angle
        {
            get;
            set;
        }

        public float Speed
        {
            get;
            set;
        }

        public override void Initialize()
        {
            this.transform = new ComponentLookup<TransformComponent>(this.ParentEntity);
            this.ball = new ComponentLookup<BallComponent>(this.ParentEntity);

            this.Angle = (float)(90 * (Math.PI / 180));
            this.Speed = 40f;
        }

        public override void Update()
        {
            if (transform.Component.Position.Y < 0 || transform.Component.Position.Y + this.ball.Component.Size > GameEngine.Instance.Window.Size.Y
                || transform.Component.Position.X < 0 || transform.Component.Position.X + this.ball.Component.Size > GameEngine.Instance.Window.Size.X)
            {
                this.Angle = -this.Angle;
            }

            this.transform.Component.AddPosition((float)(GameEngine.Instance.Delta * this.Speed * Math.Cos(this.Angle)), (float)(GameEngine.Instance.Delta * this.Speed * Math.Sin(this.Angle)));
        }
    }
}
