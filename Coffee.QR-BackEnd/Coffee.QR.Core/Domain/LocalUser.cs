using Coffee.QR.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.QR.Core.Domain
{
    public class LocalUser : Entity
    {
        public Local local { get; private set; }
        public long LocalId {get; set;}
        public User user { get; set;}
        public long UserId { get; set;}

        public LocalUser(long localId, long userId)
        {
            LocalId = localId;
            UserId = userId;
        }   
    }
}
