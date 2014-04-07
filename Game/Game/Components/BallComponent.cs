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

    [EntityComponent(Name = "Pong.Ball", ExecutionLayer = EngineLayers.DrawGame)]
    public class BallComponent : AbstractComponent
    {
        private ComponentLookup<TransformComponent> transform = null;

        public Color BallColor
        {
            get;
            set;
        }

        public float Size
        {
            get;
            set;
        }

        public override void Initialize()
        {
            this.transform = new ComponentLookup<TransformComponent>(this.ParentEntity);

            this.BallColor = Color.White;
            this.Size = 50f;
        }

        public override void Draw()
        {
            RectangleShape shape = new RectangleShape();
            shape.FillColor = this.BallColor;
            shape.Position = this.transform.Component.Position;
            shape.Size = new Vector2f(this.Size, this.Size);

            GameEngine.Instance.AddToRenderQueue(shape);
        }
    }
}
