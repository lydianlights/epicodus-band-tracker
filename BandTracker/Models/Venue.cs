using System;
using System.Collections.Generic;

namespace BandTracker.Models
{
  public class Venue
  {
      public int Id {get; private set;}
      public string Name {get; private set;}

      public Venue(string name, int id = 0)
      {
          Id = id;
          Name = name;
      }
      public override bool Equals(object other)
      {
          if(!(other is Venue))
          {
              return false;
          }
          else
          {
              var otherVenue = (Venue)other;
              return this.Name == otherVenue.Name;
          }
      }
      public override int GetHashCode()
      {
          return this.Name.GetHashCode();
      }
  }
}
