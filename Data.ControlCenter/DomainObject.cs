using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.ControlCenter
{
    /// <summary>
    /// For a discussion of this object, see 
    /// http://devlicio.us/blogs/billy_mccafferty/archive/2007/04/25/using-equals-gethashcode-effectively.aspx
    /// </summary>
    public abstract class DomainObject<IdT>
    {
        /// <summary>
        /// ID may be of type string, int, custom type, etc.
        /// Setter is protected to allow unit tests to set this property via reflection and to allow 
        /// domain objects more flexibility in setting this for those objects with assigned IDs.
        /// </summary>
        public virtual IdT EntityID
        {
            get { return id; }
            protected set { id = value; }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DomainObject<IdT>);
        }

        public virtual bool Equals(DomainObject<IdT> compareTo)
        {
            return (compareTo != null) &&
                   (HasSameNonDefaultIdAs(compareTo) ||
                // Since the IDs aren't the same, either of them must be transient to 
                // compare business value signatures
                    (((IsTransient()) || compareTo.IsTransient()) &&
                     HasSameBusinessSignatureAs(compareTo)));
        }

        //public virtual bool Equals(Entity<TId> other)
        //{
        //    if (other == null)
        //        return false;
        //    if (ReferenceEquals(this, other))
        //        return true;
        //    if (!IsTransient(this) &&
        //    !IsTransient(other) &&
        //    Equals(Id, other.Id))
        //    {
        //        var otherType = other.GetUnproxiedType();
        //        var thisType = GetUnproxiedType();
        //        return thisType.IsAssignableFrom(otherType) ||
        //        otherType.IsAssignableFrom(thisType);
        //    }
        //    return false;
        //}

        /// <summary>
        /// Transient objects are not associated with an item already in storage.  For instance,
        /// a <see cref="Customer" /> is transient if its ID is 0.
        /// </summary>
        public virtual bool IsTransient()
        {
            return EntityID == null || EntityID.Equals(default(IdT));
        }

        /// <summary>
        /// Must be provided to properly compare two objects
        /// </summary>
        //public abstract override int GetHashCode();
        public override int GetHashCode()
        {
            if (Equals(EntityID, default(IdT)))
                return base.GetHashCode();
            return EntityID.GetHashCode();
        }

        private bool HasSameBusinessSignatureAs(DomainObject<IdT> compareTo)
        {
            Check.Require(compareTo != null, "compareTo may not be null");

            return GetHashCode().Equals(compareTo.GetHashCode());
        }

        /// <summary>
        /// Returns true if self and the provided persistent object have the same ID values 
        /// and the IDs are not of the default ID value
        /// </summary>
        private bool HasSameNonDefaultIdAs(DomainObject<IdT> compareTo)
        {
            Check.Require(compareTo != null, "compareTo may not be null");

            return (EntityID != null && !EntityID.Equals(default(IdT))) &&
                   (compareTo.EntityID != null && !compareTo.EntityID.Equals(default(IdT))) &&
                   EntityID.Equals(compareTo.EntityID);
        }

        private IdT id = default(IdT);
    }
}
