using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template.Infrastructure.Utilities
{
    public static class StoredProcedures
    {
        public const string spCheckAnyChange = "dbo.spCheckAnyChange";
        public const string spTestGetRezultate = "dbo.spTestGetRezultate";
        public const string spUpdateFreq = "dbo.spUpdateFreq";
        public const string spUpdateVremeDo = "dbo.spUpdateVremeDo";
        public const string spAddFreqLookData = "dbo.spAddFreqLookData";
        public const string AddImageToProImages = "dbo.AddImageToProImages";
        public const string UpdateImageInProImages = "dbo.UpdateImageInProImages";

        public const string CheckIfGuidExistsInProImages = "dbo.CheckIfGuidExistsInProImages";

        public const string DeleteOldImages = "dbo.DeleteOldImages";
        public const string spGetRezultate = "dbo.spGetRezultate";

    }
}
