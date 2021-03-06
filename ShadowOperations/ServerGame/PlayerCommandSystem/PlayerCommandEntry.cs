﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShadowOperations.ServerGame.EntitySystem;

namespace ShadowOperations.ServerGame.PlayerCommandSystem
{
    public class PlayerCommandEntry
    {
        public PlayerEntity Player;

        public AbstractPlayerCommand Command;

        public List<string> InputArguments;

        public string AllArguments()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < InputArguments.Count; i++)
            {
                sb.Append(InputArguments[i]);
                if (i + 1 < InputArguments.Count)
                {
                    sb.Append(' ');
                }
            }
            return sb.ToString();
        }
    }
}
