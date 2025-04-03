using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;
using Template.Domain.Aggregates;
using Template.Domain.Events;

namespace Template.Domain.DataModels;

public partial class Auto : IAggregateRoot
 {




    // Method to update Name
   
    public static Auto FromJson(string json)
    {
        Auto model = JsonConvert.DeserializeObject<Auto>(json);

        if (model == null) throw new InvalidOperationException("Invalid JSON data for User.");

        return model;
    }
 
    public static Auto Create(int Id,
         int GodinaProizvodnje,
     string TipGoriva ,
     string NazivAutomobila)
    {
        return new Auto
        {
            Id = Id,
            GodinaProizvodnje = GodinaProizvodnje,
            TipGoriva = TipGoriva,
            NazivAutomobila = NazivAutomobila
          
        };
    }

}

