﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShadowOperations.ServerGame.EntitySystem;
using ShadowOperations.Shared;
using BEPUphysics.Constraints.TwoEntity;
using BEPUphysics.Constraints.TwoEntity.Motors;

namespace ShadowOperations.ServerGame.JointSystem
{
    class JointPullPush : BaseJoint
    {
        public JointPullPush(PhysicsEntity e1, PhysicsEntity e2, float stren)
        {
            Ent1 = e1;
            Ent2 = e2;
            Strength = stren;
        }

        public float Strength;

        public override TwoEntityConstraint GetBaseJoint()
        {
            LinearAxisMotor lam = new LinearAxisMotor(Ent1.Body, Ent2.Body, Ent1.GetPosition().ToBVector(), Ent2.GetPosition().ToBVector(), (Ent2.GetPosition() - Ent1.GetPosition()).Normalize().ToBVector());
            lam.Settings.Mode = MotorMode.VelocityMotor;
            lam.Settings.MaximumForce = 100 * 5;
            lam.Settings.VelocityMotor.Softness = 0.01f;
            lam.Settings.VelocityMotor.GoalVelocity = Strength;
            return lam;
        }
    }
}
