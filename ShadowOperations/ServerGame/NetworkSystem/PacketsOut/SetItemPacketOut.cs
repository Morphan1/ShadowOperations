﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShadowOperations.ServerGame.ItemSystem;
using ShadowOperations.Shared;

namespace ShadowOperations.ServerGame.NetworkSystem.PacketsOut
{
    public class SetItemPacketOut : AbstractPacketOut
    {
        public SetItemPacketOut(int spot, ItemStack item)
        {
            ID = 21;
            byte[] itemdat = item.ToBytes();
            Data = new byte[4 + itemdat.Length];
            Utilities.IntToBytes(spot).CopyTo(Data, 0);
            itemdat.CopyTo(Data, 4);
        }
    }
}
