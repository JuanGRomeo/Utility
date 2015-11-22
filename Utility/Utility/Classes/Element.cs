using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Classes
{
    /// <summary>
    /// Represents a basic element clonable.
    /// </summary>
    public abstract class Element : ICloneable
    {
        #region Fields
        private int id = int.MinValue;
        private string description = string.Empty;
        #endregion

        #region Properties
        /// <summary>
        /// Get and set the element id
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Get and set the element description
        /// </summary>
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Returns a copy of the object
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            object elementCloned = Activator.CreateInstance(this.GetType());

            // Get all the fields of the object that we want to copy
            FieldInfo[] object_fields = this.GetType().GetFields(
                BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
 
            // Copy each value over the new object 
            foreach (FieldInfo field in object_fields)
                field.SetValue(elementCloned, field.GetValue(this));

            return elementCloned;
        }
        #endregion        
    }
}
