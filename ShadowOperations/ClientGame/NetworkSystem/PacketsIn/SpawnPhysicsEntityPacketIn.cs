﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShadowOperations.Shared;
using ShadowOperations.ClientGame.ClientMainSystem;
using ShadowOperations.ClientGame.EntitySystem;
using ShadowOperations.ClientGame.GraphicsSystems;

namespace ShadowOperations.ClientGame.NetworkSystem.PacketsIn
{
    public class SpawnPhysicsEntityPacketIn: AbstractPacketIn
    {
        public override bool ParseBytesAndExecute(byte[] data)
        {
            int len = 4 + 12 + 12 + 16 + 12 + 8 + 4 + 12 + 1 + 4 + 1;
            if (data.Length != len
                && data.Length != len + 4 * 6 + 4 * 6
                && data.Length != len + 4 + 1)
            {
                return false;
            }
            byte type = data[4 + 12 + 12 + 16 + 12 + 8 + 4 + 12];
            float mass = Utilities.BytesToFloat(Utilities.BytesPartial(data, 0, 4));
            Location pos = Location.FromBytes(data, 4);
            Location vel = Location.FromBytes(data, 4 + 12);
            BEPUutilities.Quaternion ang = Utilities.BytesToQuaternion(data, 4 + 12 + 12);
            Location angvel = Location.FromBytes(data, 4 + 12 + 12 + 16);
            long eID = Utilities.BytesToLong(Utilities.BytesPartial(data, 4 + 12 + 12 + 16 + 12, 8));
            float fric = Utilities.BytesToFloat(Utilities.BytesPartial(data, 4 + 12 + 12 + 16 + 12 + 8, 4));
            Location halfsize = Location.FromBytes(data, 4 + 12 + 12 + 16 + 12 + 8 + 4);
            PhysicsEntity ce;
            if (type == 0)
            {
                CubeEntity ce1 = new CubeEntity(TheClient, halfsize);
                ce = ce1;
                int start = len - (4 + 1);
                NetStringManager strings = TheClient.Network.Strings;
                ce1.Textures[0] = strings.StringForIndex(Utilities.BytesToInt(Utilities.BytesPartial(data, start, 4)));
                ce1.Textures[1] = strings.StringForIndex(Utilities.BytesToInt(Utilities.BytesPartial(data, start + 4, 4)));
                ce1.Textures[2] = strings.StringForIndex(Utilities.BytesToInt(Utilities.BytesPartial(data, start + 4 * 2, 4)));
                ce1.Textures[3] = strings.StringForIndex(Utilities.BytesToInt(Utilities.BytesPartial(data, start + 4 * 3, 4)));
                ce1.Textures[4] = strings.StringForIndex(Utilities.BytesToInt(Utilities.BytesPartial(data, start + 4 * 4, 4)));
                ce1.Textures[5] = strings.StringForIndex(Utilities.BytesToInt(Utilities.BytesPartial(data, start + 4 * 5, 4)));
                ce1.Coords[0] = TextureCoordinates.FromString(strings.StringForIndex(Utilities.BytesToInt(Utilities.BytesPartial(data, start + 4 * 6, 4))));
                ce1.Coords[1] = TextureCoordinates.FromString(strings.StringForIndex(Utilities.BytesToInt(Utilities.BytesPartial(data, start + 4 * 6 + 4, 4))));
                ce1.Coords[2] = TextureCoordinates.FromString(strings.StringForIndex(Utilities.BytesToInt(Utilities.BytesPartial(data, start + 4 * 6 + 4 * 2, 4))));
                ce1.Coords[3] = TextureCoordinates.FromString(strings.StringForIndex(Utilities.BytesToInt(Utilities.BytesPartial(data, start + 4 * 6 + 4 * 3, 4))));
                ce1.Coords[4] = TextureCoordinates.FromString(strings.StringForIndex(Utilities.BytesToInt(Utilities.BytesPartial(data, start + 4 * 6 + 4 * 4, 4))));
                ce1.Coords[5] = TextureCoordinates.FromString(strings.StringForIndex(Utilities.BytesToInt(Utilities.BytesPartial(data, start + 4 * 6 + 4 * 5, 4))));
            }
            else if (type == 1)
            {
                ce = new OtherPlayerEntity(TheClient, halfsize);
            }
            else if (type == 2)
            {
                int start = len - (4 + 1);
                NetStringManager strings = TheClient.Network.Strings;
                ModelEntity me = new ModelEntity(strings.StringForIndex(Utilities.BytesToInt(Utilities.BytesPartial(data, start, 4))), TheClient);
                byte moder = data[start + 4];
                me.mode = (ModelCollisionMode)moder;
                ce = me;
            }
            else
            {
                SysConsole.Output(OutputType.WARNING, "Unknown physent type " + type);
                return false;
            }
            float bounce = Utilities.BytesToFloat(Utilities.BytesPartial(data, data.Length - 5, 4));
            bool Visible = (data[data.Length - 1] & 1) == 1;
            int solidity = (data[data.Length - 1] & (2|4|8));
            if (solidity == 2)
            {
                ce.CGroup = ce.TheClient.Collision.Solid;
            }
            else if (solidity == 4)
            {
                ce.CGroup = ce.TheClient.Collision.NonSolid;
            }
            else if (solidity == (2 | 4))
            {
                ce.CGroup = ce.TheClient.Collision.Item;
            }
            else if (solidity == (8))
            {
                ce.CGroup = ce.TheClient.Collision.Player;
            }
            else if (solidity == (4 | 8))
            {
                ce.CGroup = ce.TheClient.Collision.Trigger;
            }
            else if (solidity == (2 | 8))
            {
                ce.CGroup = ce.TheClient.Collision.Solid; // PlaceHolder
            }
            else if (solidity == (2 | 4 | 8))
            {
                ce.CGroup = ce.TheClient.Collision.Solid; // PlaceHolder
            }
            ce.Visible = Visible;
            ce.SetMass(mass);
            ce.SetPosition(pos);
            ce.SetVelocity(vel);
            ce.SetOrientation(ang);
            ce.SetAngularVelocity(angvel);
            ce.EID = eID;
            ce.SetFriction(fric);
            ce.SetBounciness(bounce);
            TheClient.SpawnEntity(ce);
            return true;
        }
    }
}
