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

    [EntityComponent(Name = "Pong.Paddle", ExecutionLayer = EngineLayers.DrawGame)]
    public class PaddleComponent : AbstractComponent
    {
        private ComponentLookup<TransformComponent> transform = null;

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

        public override void Initialize()
        {
            this.transform = new ComponentLookup<TransformComponent>(this.ParentEntity);

            this.Paddle = new FloatRect(this.transform.Component.Position.X, this.transform.Component.Position.Y, 15, 100);
            this.PaddleColor = Color.White;
        }

        public override void Draw()
        {
            this.Paddle = new FloatRect(this.transform.Component.Position.X, this.transform.Component.Position.Y, this.Paddle.Width, this.Paddle.Height);

            RectangleShape shape = new RectangleShape();
            shape.FillColor = this.PaddleColor;
            shape.Position = new Vector2f(this.Paddle.Left, this.Paddle.Top);
            shape.Size = new Vector2f(this.Paddle.Width, this.Paddle.Height);

            GameEngine.Instance.AddToRenderQueue(shape);
        }
    }
}
