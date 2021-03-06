﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Frenetic.CommandSystem;
using ShadowOperations.ClientGame.ClientMainSystem;
using ShadowOperations.ClientGame.UISystem;
using OpenTK.Input;
using Frenetic.TagHandlers;

namespace ShadowOperations.ClientGame.CommandSystem.UICommands
{
    /// <summary>
    /// A quick command to quit the game.
    /// </summary>
    class BindCommand : AbstractCommand
    {
        public Client TheClient;

        public BindCommand(Client tclient)
        {
            TheClient = tclient;
            Name = "bind";
            Description = "Binds a script to a key.";
            Arguments = "<key> [binding]";
        }

        public override void Execute(CommandEntry entry)
        {
            if (entry.Arguments.Count <= 0)
            {
                ShowUsage(entry);
                return;
            }
            string key = entry.GetArgument(0);
            Key k = KeyHandler.GetKeyForName(key);
            if (entry.Arguments.Count == 1)
            {
                CommandScript cs = KeyHandler.GetBind(k);
                if (cs == null)
                {
                    entry.Bad("That key is not bound, or does not exist.");
                }
                else
                {
                    entry.Info(TagParser.Escape(key + ": '" + cs.FullString() + "'"));
                }
            }
            else if (entry.Arguments.Count >= 2)
            {
                KeyHandler.BindKey(k, entry.GetArgument(1));
                entry.Good("Keybind updated.");
            }
        }
    }
}
