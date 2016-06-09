using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trackmatic.Trains
{
    public class Town
    {
        public string Name {  get; private set; }

        public Town(char name) :
            this(name.ToString())
        { }
        public Town(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            Name = name;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Town);
        }

        public override string ToString()
        {
            return Name;
        }

        public bool Equals(Town other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return this.Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public static implicit operator Town(string name)
        {
            return new Town(name);
        }
    }
}
