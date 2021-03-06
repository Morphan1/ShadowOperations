﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShadowOperations.ClientGame.JointSystem
{
    public abstract class BaseFJoint: InternalBaseJoint
    {
        public abstract void Solve();

        public override void Enable()
        {
            Enabled = true;
        }

        public override void Disable()
        {
            Enabled = false;
        }
    }
}
