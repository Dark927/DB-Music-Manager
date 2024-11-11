using MusicManager.DBManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicManager
{
    internal class MusicDataEditor : DBDataEditorBase<DataListType>
    {
        public MusicDataEditor(string connectionString) : base(connectionString) { }

        public override void AddData(DataListType type, params int[] parameters)
        {
            throw new NotImplementedException();
        }

        public override void DeleteData(DataListType type, params int[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
