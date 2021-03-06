﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShadowOperations.ServerGame.EntitySystem
{
    public interface EntityDamageable
    {
        float GetHealth();

        float GetMaxHealth();

        void SetHealth(float health);

        void SetMaxHealth(float health);

        void Damage(float amount);
    }
}
