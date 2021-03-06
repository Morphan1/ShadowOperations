﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShadowOperations.Shared;
using ShadowOperations.ClientGame.ClientMainSystem;
using ShadowOperations.ClientGame.JointSystem;

namespace ShadowOperations.ClientGame.EntitySystem
{
    /// <summary>
    /// Represents an object within the world.
    /// </summary>
    public abstract class Entity
    {
        public OpenTK.Graphics.Color4 Color;

        public Entity(Client tclient, bool tickme, bool cast_shadows)
        {
            TheClient = tclient;
            Ticks = tickme;
            Color = new OpenTK.Graphics.Color4((float)Utilities.UtilRandom.NextDouble(), (float)Utilities.UtilRandom.NextDouble(), 0f, 1f);
            CastShadows = cast_shadows;
        }

        /// <summary>
        /// The unique ID for this entity.
        /// </summary>
        public long EID;

        /// <summary>
        /// Whether this entity should tick.
        /// </summary>
        public readonly bool Ticks;

        /// <summary>
        /// Wether this entity should cast shadows.
        /// </summary>
        public readonly bool CastShadows;

        /// <summary>
        /// The client that manages this entity.
        /// </summary>
        public Client TheClient = null; 

        /// <summary>
        /// Draw the entity in the 3D world.
        /// </summary>
        public abstract void Render();

        public bool Visible = false;

        public abstract BEPUutilities.Quaternion GetOrientation();

        public abstract void SetOrientation(BEPUutilities.Quaternion quat);

        public List<InternalBaseJoint> Joints = new List<InternalBaseJoint>();

        /// <summary>
        /// Tick the entity. Default implementation throws exception.
        /// </summary>
        public virtual void Tick()
        {
            throw new NotImplementedException();
        }

        public abstract Location GetPosition();

        public abstract void SetPosition(Location pos);
    }
}
