using System;
using System.Data;
using System.IO;
using MusicManager.DBManagement.Query;
using MusicManager.Utilities;

namespace MusicManager.DBManagement
{
    public enum MusicDataType
    {
        Default,
        Id,
        Name,
        Duration,
        Style,
    }

    public enum AuthorDataType
    {
        Default,
        Id,
        Name,
    }

    public enum AuthorMusicDataType
    {
        Default,
        Duration,
        Style,
    }

    internal class DataProvider<T> : DBToolBase where T : Enum
    {
        private DBQueryCollection<T> _queryCollection;

        public DataProvider(DataBase dataBase) : base(dataBase)
        {
            _queryCollection = new DBQueryCollection<T>();
            SetDefaultState();
        }

        public DataProvider(DataBase dataBase, string statePath) : base(dataBase)
        {
            _queryCollection = new DBQueryCollection<T>();
        
            if (_queryCollection.LoadStateFromJson(statePath) == false)
            {
                SetDefaultState();
            }
        }

        protected override void SetDefaultState()
        {
            _queryCollection = DefaultQueries<T>.RequestDefaultQueries();
        }


        /// <summary>
        /// request the data from DataBase, throws an exception if an error occurs
        /// </summary>
        /// <param name="type">type of the data to get</param>
        /// <param name="parameters">additional parameters for the query, can be empty</param>
        /// <returns>DataTable with the requested data from the DataBase</returns>
        public DataTable RequestData(T type, params int[] parameters)
        {
            DataTable requestedData = null;

            if (_queryCollection.ContainKey(type))
            {
                _queryCollection[type].Parameters = parameters;
                requestedData = DataBase.SendQuery(_queryCollection[type]);
            }
            return requestedData;
        }


        /// <summary>
        /// check if data is available in the DataBase
        /// </summary>
        /// <param name="type">type of the data to check</param>
        /// <param name="parameters">additional parameters for the query, can be empty</param>
        /// <returns>true - if the data is available, else - false</returns>
        public bool IsDataAvailable(T type, params int[] parameters)
        {
            bool isAvailable = false;

            if (_queryCollection.ContainKey(type))
            {
                _queryCollection[type].Parameters = parameters;
                isAvailable = DataBase.SendQuery(_queryCollection[type]).DefaultView.Count != 0;
            }

            return isAvailable;
        }
    }
}
