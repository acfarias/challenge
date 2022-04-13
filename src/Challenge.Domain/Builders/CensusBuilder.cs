using Challenge.Domain.Aggregate;
using Challenge.Domain.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Challenge.Domain.Builders
{
    public class CensusBuilder
    {
        private readonly Census _census = new();

        public CensusBuilder(string firstName, string lastName, string skinColor, string schooling, int region)
        {
            _census.FirstName = firstName;
            _census.LastName = lastName;
            _census.SkinColor = skinColor;
            _census.Schooling = schooling;
            _census.Region = region;
        }

        public CensusBuilder AddParents(Parents parents)
        {
            if (_census.Parents == null)
                _census.Parents = new Parents();

            _census.Parents.FatherName = parents.FatherName;
            _census.Parents.MotherName = parents.MotherName;

            return this;
        }

        public CensusBuilder AddSons(List<Son> sons)
        {
            if (_census.Sons == null)
                _census.Sons = new List<Son>();

            _census.Sons.AddRange(sons);

            return this;
        }

        public Census Build()
        {
            return _census;
        }
    }
}