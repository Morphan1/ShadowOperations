﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShadowOperations.ClientGame.EntitySystem
{
    public interface EntityAnimated
    {
        void SetAnimation(string anim, byte mode);
    }
}
