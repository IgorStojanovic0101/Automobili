using System;
using System.Collections.Generic;
using Template.Domain.Base;

namespace Template.Domain.DataModels;

public partial class Auto : BaseEntity<int>
{

    public int GodinaProizvodnje { get; private set; }
    public string TipGoriva { get; private set; } = null!;
    public string NazivAutomobila { get; private set; } = null!;
 


    public Auto() { }

}

