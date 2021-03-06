﻿namespace Dive.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Abstract version of <see cref="IGameState"/>.
    /// </summary>
    public abstract class AbstractGameState : IGameState
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="IGameState"/> is active.
        /// </summary>
        /// <value>The value indicating whether this <see cref="IGameState"/> is active.</value>
        public bool IsActive
        {
            get
            {
                return GameEngine.Instance.StateManager.IsStateActive(this);
            }
        }

        /// <summary>
        /// Called when the <see cref="IGameState" /> is first added to the <see cref="GameStateManager" />.
        /// </summary>
        public virtual void Initialize()
        {
        }

        /// <summary>
        /// Called when this game state becomes active.
        /// </summary>
        /// <param name="previous">The previous game state.</param>
        public virtual void Start(IGameState previous)
        {
        }

        /// <summary>
        /// Called when this game state becomes inactive.
        /// </summary>
        /// <param name="next">The next game state.</param>
        public virtual void Stop(IGameState next)
        {
        }

        /// <summary>
        /// Called when the game state should be shut down.
        /// </summary>
        public virtual void Shutdown()
        {
        }

        /// <summary>
        /// Called if the game state is active and Update is called.
        /// </summary>
        public virtual void Update()
        {
        }

        /// <summary>
        /// Called if the game state is active and Draw is called.
        /// </summary>
        public virtual void Draw()
        {
        }

        /// <summary>
        /// Called if the game state is active and input is detected.
        /// </summary>
        /// <param name="action">The input action.</param>
        public virtual void OnInputAction(string action)
        {
        }
    }
}
