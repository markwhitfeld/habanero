using System;
using System.Collections;

namespace Chillisoft.Generic.v2
{
    /// <summary>
    /// Manages a collection of property definitions for a user interface
    /// editing form, as specified in the class definitions xml file
    /// </summary>
    public class UIFormDef : ICollection
    {
        private IList _list;
        private int _width;
        private int _height;
        private string _heading;

        /// <summary>
        /// Constructor to initialise a new definition
        /// </summary>
        public UIFormDef()
        {
            _list = new ArrayList();
        }

        /// <summary>
        /// Adds a tab to the form
        /// </summary>
        /// <param name="tab">A UIFormTab object</param>
        public void Add(UIFormTab tab)
        {
            _list.Add(tab);
        }

        /// <summary>
        /// Not yet implemented
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        /// TODO ERIC - implement
        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the number of definitions held
        /// </summary>
        public int Count
        {
            get { return _list.Count; }
        }

        /// <summary>
        /// Returns the synchronisation root
        /// </summary>
        public object SyncRoot
        {
            get { return _list.SyncRoot; }
        }

        /// <summary>
        /// Indicates whether the definitions are synchronised
        /// </summary>
        public bool IsSynchronized
        {
            get { return _list.IsSynchronized; }
        }

        /// <summary>
        /// Returns the definition list's enumerator
        /// </summary>
        /// <returns>Returns an IEnumerator-type object</returns>
        public IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        /// <summary>
        /// Provides an indexing facility so that the contents of the definition
        /// collection can be accessed with square brackets like an array
        /// </summary>
        /// <param name="index">The index position to access</param>
        /// <returns>Returns the property definition at the index position
        /// specified</returns>
        public UIFormTab this[int index]
        {
            get { return (UIFormTab) _list[index]; }
        }

        /// <summary>
        /// Gets and sets the width
        /// </summary>
        public int Width
        {
            set { _width = value; }
            get { return _width; }
        }

        /// <summary>
        /// Gets and sets the height
        /// </summary>
        public int Height
        {
            set { _height = value; }
            get { return _height; }
        }

        /// <summary>
        /// Gets and sets the heading
        /// </summary>
        public string Heading
        {
            set { _heading = value; }
            get { return _heading; }
        }
    }
}