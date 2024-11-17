using MusicManager.DBManagement.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MusicManager.DBManagement
{
    class DataAdder<T> : DBToolBase<T> where T : Enum
    {
        public DataAdder(DataBase dataBase, DBQueryCollection<T> queriesCollection) : base(dataBase, queriesCollection)
        {

        }

        /// <summary>
        /// try add the data to the DataBase, returns false if an error occurs
        /// </summary>
        /// <param name="type">type of the data to add</param>
        /// <param name="parameters">additional parameters for the query, can be empty</param>
        /// <returns>true - if the data was successfully added, else - false.</returns>
        public bool TryAddData(T type, params string[] parameters)
        {
            bool isAdded = true;

            if (QueriesCollection.ContainKey(type))
            {
                try
                {
                    QueriesCollection[type].Parameters = parameters;
                    DB.SendQuery(QueriesCollection[type]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    isAdded = false;
                }
            }

            return isAdded;
        }

        /// <summary>
        /// delete the data from the DataBase, throws an exception if an error occurs
        /// </summary>
        /// <param name="type">type of the data to delete</param>
        /// <param name="parameters">additional parameters for the query, can be empty</param>
        public void AddData(T type, params string[] parameters)
        {
            if (QueriesCollection.ContainKey(type))
            {
                QueriesCollection[type].Parameters = parameters;
                DB.SendQuery(QueriesCollection[type]);
            }
        }
    }
}
