using System;

namespace MusicManager.DBManagement
{
    internal abstract class DBDataEditorBase<TData> : DBManagerBase where TData : Enum
    {
        protected DBDataEditorBase(string connectionString) : base(connectionString) { }

        public abstract void DeleteData(TData type, params int[] parameters);
        public abstract void AddData(TData type, params int[] parameters);
    }
}