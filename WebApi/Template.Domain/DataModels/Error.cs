using System;
using System.Collections.Generic;
using Template.Domain.Base;

namespace Template.Domain.DataModels
{
    public partial class Error : BaseEntity<int>
    {
        public string Naziv { get; private set; } = null!;
        public string? Opis { get; private set; }
        public string? Detaljnije { get; private set; }
        public DateTime? VremeGreske { get; private set; }
        public string? User { get; private set; }
    }
}
