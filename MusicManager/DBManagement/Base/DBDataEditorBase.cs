using System;

namespace MusicManager.DBManagement
{
    internal abstract class DBDataEditorBase<TData> where TData : Enum
    {



        /// <summary>
        /// add the data to the DataBase, throws an exception if an error occurs
        /// </summary>
        /// <param name="type">type of the data to add</param>
        /// <param name="parameters">additional parameters for the query, can be empty</param>
        public abstract void AddData(TData type, params int[] parameters);

        /// <summary>
        /// try add the data to the DataBase, returns false if an error occurs
        /// </summary>
        /// <param name="type">type of the data to add</param>
        /// <param name="parameters">additional parameters for the query, can be empty</param>
        /// <returns>true - if the data was successfully added, else - false.</returns>
        public abstract bool TryAddData(TData type, params int[] parameters);
    }
}