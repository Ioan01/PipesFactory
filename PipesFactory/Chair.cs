using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipesFactory
{
    internal class Chair
    {
        private static int _idIndex = 0;

        public string Id { get; }

        public bool CutSeat { get; set; }

        public bool AssembledFeet { get; set; }

        public bool AssembledBackrest { get; set; }

        public bool AssembledStabilizerBar { get; set; }

        public bool Packaged { get; set; }

        public Chair()
        {
            Id = $"chair{_idIndex++}";
        }

        public override string ToString()
        {
            return $"{CutSeat} {AssembledFeet} {AssembledBackrest} {AssembledStabilizerBar} {Packaged}";
        }
    }
}
