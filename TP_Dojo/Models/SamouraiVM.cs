﻿using BO;
using System.Collections.Generic;

namespace TP_Dojo.Models
{
    public class SamouraiVM
    {
        public Samourai Samourai { get; set; }
        public List<Arme> Armes { get; set; }
        public int? IdSelectedArme { get; set; }
        public List<ArtMartial> ArtMartials { get; set; } = new List<ArtMartial>();
        public List<int> ArtMartialsIds { get; set; } = new List<int>();
    }
}