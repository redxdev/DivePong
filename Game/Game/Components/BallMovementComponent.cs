namespace Pong.Game.Components
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dive.Engine;
    using Dive.Engine.Components;
    using Dive.Engine.Scheduler;
    using Dive.Entity;
    using Dive.Entity.Attributes;
    using Dive.Util;
    using SFML.Window;
    using SFML.Graphics;

    [EntityComponent(Name = "Pong.BallMovement", ExecutionLayer = EngineLayers.UpdatePhysics)]
    public class BallMovementComponent : AbstractComponent
    {
        private static Random random = new Random();

        private ComponentLookup<TransformComponent> transform = null;
        private ComponentLookup<BallComponent> ball = null;

        public double Angle
        {
            get;
            set;
        }

        public float Speed
        {
            get;
            set;
        }

        public float SpeedIncrease
        {
            get;
            set;
        }

        public override void Initialize()
        {
            this.transform = new ComponentLookup<TransformComponent>(this.ParentEntity);
            this.ball = new ComponentLookup<BallComponent>(this.ParentEntity);

            this.Angle = (float)(random.Next(25, 70) * (Math.PI / 180));
            this.Speed = 400f;
            this.SpeedIncrease = 20f;

            this.transform.Component.SetPosition(300, 300);
        }

        public override void Update()
        {
            if (transform.Component.Position.Y <= 0 || transform.Component.Position.Y + this.ball.Component.Size >= GameEngine.Instance.Window.Size.Y)
            {
                this.Angle = -this.Angle;
            }

            if (transform.Component.Position.X <= 0 || transform.Component.Position.X + this.ball.Component.Size >= GameEngine.Instance.Window.Size.X)
            {
                GameEngine.Instance.EntityManager.RemoveEntity(this.ParentEntity);
                GameEngine.Instance.Scheduler.ScheduleTask(new TaskInfo()
                {
                    ExecuteAfter = 2f,
                    Task = () =>
                    {
                        GameEngine.Instance.EntityManager.CreateEntityFromTemplateWithName("Pong.Ball", "ball");
                    }
                });
                return;
            }

            Entity ai = GameEngine.Instance.EntityManager.GetEntityByName("ai");
            Entity player = GameEngine.Instance.EntityManager.GetEntityByName("player");
            FloatRect myRect = new FloatRect(this.transform.Component.Position.X, this.transform.Component.Position.Y, this.ball.Component.Size, this.ball.Component.Size);
            if (ai != null)
            {
                PaddleComponent paddle = ai.GetComponent<PaddleComponent>();
                if (paddle.Paddle.Intersects(myRect))
                {
                    this.Angle += 2 * this.Angle;
                    this.Speed += this.SpeedIncrease;
                    this.transform.Component.SetX(GameEngine.Instance.Window.Size.X - paddle.Paddle.Width - this.ball.Component.Size);
                }
            }
            
            if (player != null)
            {
                PaddleComponent paddle = player.GetComponent<PaddleComponent>();
                if (paddle.Paddle.Intersects(myRect))
                {
                    this.Angle += 2 * this.Angle;
                    this.Speed += this.SpeedIncrease;
                    this.transform.Component.SetX(paddle.Paddle.Width);
                }
            }

            this.Speed = MathHelper.Clamp(this.Speed, 0, 1000);

            this.Angle = this.Angle % (2 * Math.PI);

            Vector2f position = this.transform.Component.Position;
            position.X += (float)(GameEngine.Instance.Delta * this.Speed * Math.Cos(this.Angle));
            position.Y += (float)(GameEngine.Instance.Delta * this.Speed * Math.Sin(this.Angle));

            if (position.Y < 0)
                position.Y = 0;
            else if (position.Y + this.ball.Component.Size > GameEngine.Instance.Window.Size.Y)
                position.Y = GameEngine.Instance.Window.Size.Y - this.ball.Component.Size;

            this.transform.Component.Position = position;
        }
    }
}
