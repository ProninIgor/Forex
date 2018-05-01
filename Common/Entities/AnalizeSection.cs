using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Entities
{
    public class AnalizeSection
    {
        public double MinRate { get; set; }

        public double MaxRate { get; set; }

        public bool InSection(double rate)
        {
            return this.MinRate <= rate && rate <= this.MaxRate;
        }

    }
}
