﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShadowOperations.ClientGame.NetworkSystem.PacketsOut
{
    public class PingPacketOut: AbstractPacketOut
    {
        public PingPacketOut(byte bit)
        {
            ID = 0;
            Data = new byte[] { bit };
        }
    }
}
