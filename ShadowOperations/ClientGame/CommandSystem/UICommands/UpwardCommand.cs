﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Frenetic.CommandSystem;
using ShadowOperations.ClientGame.ClientMainSystem;

namespace ShadowOperations.ClientGame.CommandSystem.UICommands
{
    /// <summary>
    /// A command to move upward (jump).
    /// </summary>
    class UpwardCommand : AbstractCommand
    {
        public Client TheClient;

        public UpwardCommand(Client tclient)
        {
            TheClient = tclient;
            Name = "upward";
            Description = "Moves the player upward (jumps).";
            Arguments = "";
        }

        public override void Execute(CommandEntry entry)
        {
            if (entry.Marker == 0)
            {
                entry.Bad("Must use +, -, or !");
            }
            else if (entry.Marker == 1)
            {
                TheClient.Player.Upward = true;
            }
            else if (entry.Marker == 2)
            {
                TheClient.Player.Upward = false;
            }
            else if (entry.Marker == 3)
            {
                TheClient.Player.Upward = !TheClient.Player.Upward;
            }
        }
    }
}
