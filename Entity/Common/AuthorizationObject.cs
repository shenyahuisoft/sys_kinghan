using System;
using System.Collections;
using DatabaseLayer;

namespace Entity
{
    public class AuthorizationObject
    {
        public String DefaultDataSource { get; set; }
        public String DevelopeCompany { get; set; }
        public String ReleaseDate { get; set; }
        public String ExpiryDate { get; set; }
        public String Version { get; set; }
        public String DataSource { get; set; }
        public String InitialCatalog { get; set; }
    }
}
