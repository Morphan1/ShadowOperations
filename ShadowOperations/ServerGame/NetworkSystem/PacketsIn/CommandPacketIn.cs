﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShadowOperations.Shared;

namespace ShadowOperations.ServerGame.NetworkSystem.PacketsIn
{
    public class CommandPacketIn: AbstractPacketIn
    {
        public override bool ParseBytesAndExecute(byte[] data)
        {
            string[] datums = FileHandler.encoding.GetString(data).Split('\n');
            List<string> args =  datums.ToList();
            string cmd = args[0];
            args.RemoveAt(0);
            Player.TheServer.PCEngine.Execute(Player, args, cmd);
            return true;
        }
    }
}
