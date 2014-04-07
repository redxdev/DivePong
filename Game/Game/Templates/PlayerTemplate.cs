namespace Pong.Game.Templates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dive.Engine.Components;
    using Dive.Entity;
    using Dive.Entity.Attributes;
    using Pong.Game.Components;

    [EntityTemplate(Name = "Pong.Player")]
    public class PlayerTemplate : ITemplate
    {
        public Entity BuildEntity(Entity entity, params object[] args)
        {
            entity.AddComponent<TransformComponent>();
            entity.AddComponent<PaddleComponent>();
            entity.AddComponent<PlayerInputComponent>();

            return entity;
        }
    }
}
