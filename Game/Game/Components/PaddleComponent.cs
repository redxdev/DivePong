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

    [EntityComponent(Name = "Pong.PaddleColor", ExecutionLayer = EngineLayers.DrawGame)]
    public class PaddleComponent : AbstractComponent
    {
        public Color PaddleColor
        {
            get;
            set;
        }

        public FloatRect Paddle
        {
            get;
            set;
        }

        private ComponentLookup<TransformComponent> transform = null;

        public PaddleComponent()
        {
            transform = new ComponentLookup<TransformComponent>(this.ParentEntity);

            this.Paddle = new FloatRect(transform.Component.Position.X, transform.Component.Position.Y, 10, 50);
            this.PaddleColor = Color.White;
        }

        public override void Draw()
        {
            this.Paddle = new FloatRect(transform.Component.Position.X, transform.Component.Position.Y, this.Paddle.Width, this.Paddle.Height);

            RectangleShape shape = new RectangleShape();
            shape.FillColor = this.PaddleColor;
            shape.Position = new Vector2f(this.Paddle.Left, this.Paddle.Top);
            shape.Size = new Vector2f(this.Paddle.Width, this.Paddle.Height);

            GameEngine.Instance.AddToRenderQueue(shape);
        }
    }
}
