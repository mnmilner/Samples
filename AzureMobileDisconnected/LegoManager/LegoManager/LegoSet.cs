using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LegoManager
{
    public class LegoSet
    {
        public string Id { get; set; }

        public string LegoProductNumber { get; set; }

        public string Name { get; set; }

        public int NumberOfPieces { get; set; }

        public DateTime ReleaseDate { get; set; }

        public bool Owned { get; set; }

        public DateTime BuildDate { get; set; }

        [Version]
        public string Version { get; set; }

    }
}
