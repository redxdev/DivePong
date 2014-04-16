namespace Pong.Game.States
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Dive.Engine;
    using Dive.Engine.Attributes;
    using Dive.Engine.Components;
    using Dive.Engine.Components.Graphics;
    using Dive.Entity;

    [GameState]
    public class PlayState : AbstractGameState
    {
        public static int PlayerScore
        {
            get;
            set;
        }

        public static int AIScore
        {
            get;
            set;
        }

        private TextRendererComponent playerText = null;
        private TextRendererComponent aiText = null;

        public override void Start(IGameState previous)
        {
            GameEngine.Instance.ClearColor = ColorConstants.Black;

            GameEngine.Instance.EntityManager.CreateEntityFromTemplateWithName("Pong.Player", "player");
            GameEngine.Instance.EntityManager.CreateEntityFromTemplateWithName("Pong.AI", "ai");
            GameEngine.Instance.EntityManager.CreateEntityFromTemplateWithName("Pong.Ball", "ball");

            Entity playerScore = GameEngine.Instance.EntityManager.CreateEntityFromTemplateWithName("Engine.Spatial", "playerScore");
            playerText = playerScore.AddComponent<TextRendererComponent>();

            playerScore.GetComponent<TransformComponent>().SetPosition(GameEngine.Instance.Window.DefaultView.Center.X - 50, 30);
            playerText.DrawLayer = -1;
            playerText.Drawable.Color = ColorConstants.Aqua;
            playerText.Drawable.Font = GameEngine.Instance.AssetManager.Load<SFML.Graphics.Font>("content/fonts/DroidSansMono.ttf");
            playerText.Drawable.DisplayedString = "0";
            playerText.Drawable.CharacterSize = 100;

            Entity aiScore = GameEngine.Instance.EntityManager.CreateEntityFromTemplateWithName("Engine.Spatial", "aiScore");
            aiText = aiScore.AddComponent<TextRendererComponent>();

            aiScore.GetComponent<TransformComponent>().SetPosition(GameEngine.Instance.Window.DefaultView.Center.X + 10, 30);
            aiText.DrawLayer = -1;
            aiText.Drawable.Color = ColorConstants.Crimson;
            aiText.Drawable.Font = GameEngine.Instance.AssetManager.Load<SFML.Graphics.Font>("content/fonts/DroidSansMono.ttf");
            aiText.Drawable.DisplayedString = "0";
            aiText.Drawable.CharacterSize = 100;
        }

        public override void Draw()
        {
            playerText.Drawable.DisplayedString = PlayerScore.ToString();
            aiText.Drawable.DisplayedString = AIScore.ToString();
        }
    }
}
