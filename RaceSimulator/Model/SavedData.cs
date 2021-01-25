using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class SavedData<T> where T : IParticipantData
    {
        private List<IParticipantData> _list = new List<IParticipantData>();
        public void Add(T item)
        {
            item.Add(_list);
        }

        public string GetBestParticipant()
        {
            if (_list.Count == 0)
                return "";
            else
                return _list[0].GetBestParticipant(_list);
        }
    }
}
