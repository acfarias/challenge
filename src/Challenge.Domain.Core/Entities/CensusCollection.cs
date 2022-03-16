using System.Collections.Generic;

namespace Challenge.Domain.Core.Entities
{
    public class CensusCollection : EntityBase<CensusCollection>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SkinColor { get; set; }
        public string Schooling { get; set; }
        public int Region { get; set; }
        public Parents Parents { get; set; }
        public List<Son> Sons { get; set; }
    }

    public class Parents
    {
        public string FatherName { get; set; }
        public string MotherName { get; set; }
    }

    public class Son
    {
        public int Age { get; set; }
        public string FullName { get; set; }
    }
}