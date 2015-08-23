using AdBrainTask.DataModels;
using System;
using System.Collections.Generic;

namespace AdBrainTask.Tests.Comparers
{
    class SportPostComparer : IEqualityComparer<SportPost>
    {
        public bool Equals(SportPost x, SportPost y)
        {
            //Check whether the objects are the same object.  
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether the products' properties are equal.  
            return x != null && y != null
                && x.Id.Equals(y.Id) && x.Author.Equals(y.Author)
                && x.Domain.Equals(y.Domain) && x.Created.Equals(y.Created)
                && x.Title.Equals(y.Title) && x.Permalink.Equals(y.Permalink);
        }

        public int GetHashCode(SportPost obj)
        {
            //Get hash code for the Title field if it is not null.  
            int hashProductTitle = obj.Title == null ? 0 : obj.Title.GetHashCode();

            //Get hash code for the Id field.  
            int hashProductId = obj.Id.GetHashCode();

            //Calculate the hash code for the product.  
            return hashProductTitle ^ hashProductId;
        }
    }
}
