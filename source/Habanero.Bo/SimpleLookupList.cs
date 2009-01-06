//---------------------------------------------------------------------------------
// Copyright (C) 2008 Chillisoft Solutions
// 
// This file is part of the Habanero framework.
// 
//     Habanero is a free framework: you can redistribute it and/or modify
//     it under the terms of the GNU Lesser General Public License as published by
//     the Free Software Foundation, either version 3 of the License, or
//     (at your option) any later version.
// 
//     The Habanero framework is distributed in the hope that it will be useful,
//     but WITHOUT ANY WARRANTY; without even the implied warranty of
//     MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//     GNU Lesser General Public License for more details.
// 
//     You should have received a copy of the GNU Lesser General Public License
//     along with the Habanero framework.  If not, see <http://www.gnu.org/licenses/>.
//---------------------------------------------------------------------------------

using System.Collections.Generic;
using Habanero.Base;
using Habanero.Base.Exceptions;

namespace Habanero.BO
{
    /// <summary>
    /// A basic lookup-list content provider that stores a collection of
    /// string-Guid pairs as provided in the constructor.
    /// A lookup-list is typically used to populate features like a ComboBox,
    /// where the string would be displayed, but the Guid would be the
    /// value stored (for reasons of data integrity).
    /// </summary>
    public class SimpleLookupList : ILookupList
    {
        /// <summary>
        /// Provides a key value pair where the persisted value can be returned for 
        ///   any displayed value. E.g. the persisted value may be a GUID but the
        ///   displayed value may be a related string.
        /// </summary>
        private readonly Dictionary<string, object> _displayValueDictionary;
        /// <summary>
        /// Provides a key value pair where the persisted value can be returned for 
        ///   any displayed value. E.g. the persisted value may be a GUID but the
        ///   displayed value may be a related string.
        /// </summary>
        private readonly Dictionary< object, string> _keyValueDictionary = new Dictionary< object, string>();

        /// <summary>
        /// Constructor to initialise the provider with a specified
        /// collection of string-Guid pairs
        /// </summary>
        /// <param name="collection">The string-Guid pair collection</param>
        public SimpleLookupList(Dictionary<string, object> collection)
        {
            _displayValueDictionary = collection;
        }

        /// <summary>
        /// Returns the lookup list contents being held
        /// </summary>
        /// <returns>Returns a collection of display-value pairs</returns>
        public Dictionary<string, object> GetLookupList()
        {
            return _displayValueDictionary;
        }

        /// <summary>
        /// Returns the lookup list contents being held where the list is keyed on the display value and
        ///   the key value can be looked up.
        /// </summary>
        /// <param name="connection">Ignored for this lookup list type.</param>
        /// <returns>Returns a collection of display-value pairs</returns>
        public Dictionary<string, object> GetLookupList(IDatabaseConnection connection)
        {
            return _displayValueDictionary;
        }

        ///<summary>
        /// The property definition that this lookup list is for
        ///</summary>
        public IPropDef PropDef
        {
            get ;
            set;
        }

        /// <summary>
        /// Returns the lookup list contents being held where the list is keyed on the list key 
        ///  either a Guid, int or Business object i.e. the value being stored for the property.
        /// The display value can be looked up.
        /// </summary>
        ///<returns>The Key Value Lookup List</returns>
        public Dictionary<object, string> GetKeyLookupList()
        {
            if (this.PropDef != null && _keyValueDictionary.Count != _displayValueDictionary.Count)
            {
                FillKeyValueDictionary();
            }
            return _keyValueDictionary;
        }

        private void FillKeyValueDictionary()
        {
            foreach (KeyValuePair<string, object> pair in _displayValueDictionary)
            {
                object parsedKey;
                if (this.PropDef.TryParsePropValue(pair.Value, out parsedKey))
                {
                    _keyValueDictionary.Add(parsedKey, pair.Key);
                }
                else
                {
                    throw new HabaneroDeveloperException("There is an application setup error Please contact your system administrator"
                        , "There is a class definition setup error the simple lookup list has lookup value items that are not of type " + this.PropDef.PropertyTypeName);
                }
            }
        }
    }
}