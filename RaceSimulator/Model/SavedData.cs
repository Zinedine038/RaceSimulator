using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SavedData<T> where T : IParticipantData
    {
        private List<T> _list = new List<T>();
        public void Add(T item)
        {
            _list.Add(item);
        }
    }
}
