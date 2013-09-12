using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;
using System.Data.Common;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

using RandomGraph.Common.Storage;
using CommonLibrary.Model.Result;
using RandomGraph.Common.Model.Result;
using RandomGraph.Common.Model;
using CommonLibrary.Model.Attributes;
using RandomGraph.Common.Model.Generation;
using Model.HierarchicModel;
using System.Collections;
using System.Globalization;
using log4net;

namespace ResultStorage.Storage
{
    // Реализация хранилища данных - SQL DB.
    public class SQLResultStorage : ResultStorage
    {
        // Организация работы с лог файлом.
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(SQLResultStorage));

        // Информация для соединения с SQL сервером.
        private ConnectionStringSettings connSettings;
        private DbProviderFactory provider;

        // Конструктор, который получает строку соединения.
        public SQLResultStorage(ConnectionStringSettings connectionStringSettings)
        {
            string dp = connectionStringSettings.ProviderName;
            this.connSettings = connectionStringSettings;
            provider = DbProviderFactories.GetFactory(dp);
        }

        // Сохранение сборки в БД.
        public override void Save(ResultAssembly assembly)
        {
            log.Info("Saving analyze results int DB.");

            log.Info("Openning connection.");
            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = GetConnectionString();
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    log.Info("Could not open a connection with DB.");
                    return;
                }

                SaveAssembliesTable(conn, assembly);
                SaveGenerationParameters(conn, assembly);
                SaveAnalyzeParameters(conn, assembly);
                SaveAnalyzeResults(conn, assembly);

                conn.Close();
            }
        }

        // Удаление сборки по данному идентификатору сборки.
        // Используется хранимая процедура "DeleteAssembly".
        public override void Delete(Guid assemblyID)
        {
            log.Info("Deleting data from DB.");
            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = GetConnectionString();
                conn.Open();

                using (SqlCommand command = (SqlCommand)conn.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 3600;  // Ждать час.
                    command.Parameters.Add("@AssID", SqlDbType.UniqueIdentifier).Value = assemblyID;
                    command.CommandText = "DeleteAssembly";
                    command.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        // Загрузка сборки по данному идентификатору сборки.
        public override ResultAssembly Load(Guid assemblyID)
        {
            log.Info("Loading assembly with ID " + assemblyID.ToString() + ".");

            ResultAssembly resultAssembly = new ResultAssembly();
            resultAssembly.ID = assemblyID;

            log.Info("Openning connection.");
            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = GetConnectionString();
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    log.Info("Could not open a connection with DB.");
                    return new ResultAssembly();
                }

                LoadAssemblyInformation(conn, resultAssembly);
                LoadAnalyzeResults(conn, resultAssembly);

                conn.Close();
            }

            return resultAssembly;
        }

        // Загрузка всех сборок (информация из таблицы Assemblies).
        public override List<ResultAssembly> LoadAllAssemblies()
        {
            List<ResultAssembly> results = new List<ResultAssembly>();
            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = GetConnectionString();
                conn.Open();

                ResultAssembly result = null;
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT * FROM Assemblies";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            result = new ResultAssembly();
                            results.Add(result);
                            result.ID = (Guid) dr["AssemblyID"];
                            result.ModelType = GetModelType((int)dr["ModelID"]);
                            result.ModelName = result.ModelType.Name;
                            result.Name = (string)dr["Name"];
                        }
                    }
                }

                conn.Close();
            }
            return results;
        }

        // Загрузка всех сборок по имени модели (информация из таблицы Assemblies).
        public override List<ResultAssembly> LoadAssembliesByModel(string modelName)
        {
            List<ResultAssembly> results = new List<ResultAssembly>();
            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = GetConnectionString();
                conn.Open();

                ResultAssembly result = null;
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT * FROM Assemblies WHERE ModelID=@ModelID";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpResultsID = provider.CreateParameter();
                    dpResultsID.ParameterName = "ModelID";
                    dpResultsID.Value = GetModelID(GetModelType(modelName));
                    cmd.Parameters.Add(dpResultsID);

                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            result = new ResultAssembly();
                            results.Add(result);
                            result.ID = (Guid)dr["AssemblyID"];
                            result.ModelType = GetModelType((int)dr["ModelID"]);
                            result.ModelName = result.ModelType.Name;
                            result.Name = (string)dr["Name"];
                        }
                    }
                }

                conn.Close();
            }
            return results;
        }

        // Открытая часть класса (не из общего интерфейса).
        // Используется статистическим анализатором.

        // Удаление сборки по данному имени job-а.
        // Используется хранимая процедура "DeleteAssembly".
        public void Delete(string jobName)
        {
            log.Info("Deleting data from DB.");
            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = GetConnectionString();
                conn.Open();

                Guid assemblyID = new Guid();
                using (SqlCommand command = (SqlCommand)conn.CreateCommand())
                {
                    string sqlQuery = "SELECT AssemblyID FROM Assemblies WHERE Name=@Name";
                    command.CommandText = sqlQuery;
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = jobName;

                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            assemblyID = Guid.Parse(dr["AssemblyID"].ToString());
                        }
                    }

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 3600;  // Ждать час.
                    command.Parameters.Clear();
                    command.Parameters.Add("@AssID", SqlDbType.UniqueIdentifier).Value = assemblyID;
                    command.CommandText = "DeleteAssembly";
                    command.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        // Возвращает имена всех сборок в БД (не загружая их).
        public object[] GetJobNames()
        {
            ArrayList result = new ArrayList();
            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = GetConnectionString();
                conn.Open();

                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT Name FROM Assemblies";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            result.Add((string)dr["Name"]);
                        }
                    }
                }

                conn.Close();
            }
            return result.ToArray();
        }

        // Возвращает имена всех сборок в БД по типы модели (не загружая их).
        public object[] GetJobNamesByModelID(Type modelType)
        {
            ArrayList result = new ArrayList();
            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = GetConnectionString();
                conn.Open();

                using (SqlCommand cmd = (SqlCommand)conn.CreateCommand())
                {
                    string sqlQuery = "SELECT Name FROM Assemblies WHERE ModelID=@ModelID";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.Add("@ModelID", SqlDbType.Int).Value = GetModelID(modelType);

                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            result.Add((string)dr["Name"]);
                        }
                    }
                }

                conn.Close();
            }

            return result.ToArray();
        }

        // Получение идентификатора сборки по имени job-а.
        public Guid GetAssemblyIDByJobName(string jobName)
        {
            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = GetConnectionString();
                conn.Open();

                using (SqlCommand command = (SqlCommand)conn.CreateCommand())
                {
                    string sqlQuery = "SELECT AssemblyID FROM Assemblies WHERE Name=@Name";
                    command.CommandText = sqlQuery;
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = jobName;

                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            return Guid.Parse(dr["AssemblyID"].ToString());
                        }
                    }
                }

                conn.Close();
            }

            return new Guid();
        }

        // Возвращает число реализаций для данного job-а.
        // Используется функция "RealizationsCount".
        public int GetRealizationCount(string jobName)
        {
            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = GetConnectionString();
                conn.Open();

                Guid assemblyID = new Guid();
                using (SqlCommand command = (SqlCommand)conn.CreateCommand())
                {
                    string sqlQuery = "SELECT AssemblyID FROM Assemblies WHERE Name=@Name";
                    command.CommandText = sqlQuery;
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = jobName;

                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            assemblyID = Guid.Parse(dr["AssemblyID"].ToString());
                        }
                    }

                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 3600;  // Ждать час.

                    command.Parameters.Clear();
                    SqlParameter param = new SqlParameter("@AssID", SqlDbType.UniqueIdentifier);
                    param.Direction = ParameterDirection.Input;
                    SqlParameter retValue = new SqlParameter("@Result", SqlDbType.Int);
                    retValue.Direction = ParameterDirection.ReturnValue;

                    param.Value = assemblyID;
                    command.Parameters.Add(param);
                    command.Parameters.Add(retValue);

                    command.CommandText = "RealizationsCount";
                    command.ExecuteNonQuery();

                    if (retValue.Value != DBNull.Value)
                        return (int)retValue.Value;
                    else
                        return 0;
                }
            }
        }

        // Возвращает список идентификаторов ансамблей, выбранных по параметрам генерации, по параметрам анализа,
        // по типу модели.
        public List<Guid> GetAssembliesID(Type modelType, Dictionary<int, string> gValues,
            Dictionary<int, string> aValues)
        {
            List<Guid> result = new List<Guid>();

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = GetConnectionString();
                conn.Open();

                using (SqlCommand command = (SqlCommand)conn.CreateCommand())
                {
                    string sqlQuery = "SELECT AssemblyID " +
                        "FROM Assemblies WHERE ModelID=@ModelID AND " + 
                        "AssemblyID IN (";

                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@ModelID", SqlDbType.Int).Value = GetModelID(modelType);

                    Dictionary<int, string>.KeyCollection keys = gValues.Keys;
                    int i = 1;
                    foreach (int key in keys)
                    {
                        command.Parameters.Add("@Param" + i.ToString(), SqlDbType.Int).Value = key;
                        command.Parameters.Add("@ParamValue" + i.ToString(), SqlDbType.NVarChar).Value =
                            gValues[key];

                        if (i > 1)
                        {
                            sqlQuery += " INTERSECT ";
                        }

                        sqlQuery += "SELECT AssemblyID FROM GenerationParamValues WHERE " +
                            "GenerationParamID=@param" + i.ToString() +
                            " AND [Value] = @ParamValue" + i.ToString();

                        ++i;
                    }

                    sqlQuery += ") AND AssemblyID IN (";

                    keys = aValues.Keys;
                    i = 1;
                    foreach (int key in keys)
                    {
                        command.Parameters.Add("@AnalyzeParam" + i.ToString(), SqlDbType.Int).Value = key;
                        command.Parameters.Add("@AnalyzeParamValue" + i.ToString(), SqlDbType.NVarChar).Value =
                            aValues[key];

                        if (i > 1)
                        {
                            sqlQuery += " INTERSECT ";
                        }

                        sqlQuery += "SELECT AssemblyID FROM AnalyzeOptionParamValues WHERE " +
                            "AnalyzeOptionParamID=@AnalyzeParam" + i.ToString() +
                            " AND [Value] = @AnalyzeParamValue" + i.ToString();

                        ++i;
                    }

                    sqlQuery += ")";

                    command.CommandText = sqlQuery;

                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            result.Add(Guid.Parse(dr["AssemblyID"].ToString()));
                        }
                    }
                }
            }

            return result;
        }

        // Возвращает строковое значения параметра генерации с данным идентификатором по имени job-а.
        public string GetParameterValueByID(string jobName, int id)
        {
            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = GetConnectionString();
                conn.Open();

                using (SqlCommand command = (SqlCommand)conn.CreateCommand())
                {
                    string sqlQuery = "SELECT GenerationParamValues.[Value] FROM Assemblies LEFT JOIN " +
                        "GenerationParamValues ON GenerationParamValues.AssemblyID=Assemblies.AssemblyID " +
                        "WHERE Assemblies.[Name]=@Name AND GenerationParamValues.GenerationParamID=@GenerationParamID";
                    command.CommandText = sqlQuery;
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = jobName;
                    command.Parameters.Add("@GenerationParamID", SqlDbType.Int).Value = id;

                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            return dr["Value"].ToString();
                        }
                    }
                }

                conn.Close();
            }

            return "";
        }

        // Возвращает список строковых значений параметра генерации с данным идентификатором по имени модели.
        public List<string> GetParameterValuesByID(Type modelType, int id)
        {
            List<string> result = new List<string>();
            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = GetConnectionString();
                conn.Open();

                using (SqlCommand command = (SqlCommand)conn.CreateCommand())
                {
                    string sqlQuery = "SELECT GenerationParamValues.[Value] FROM Assemblies LEFT JOIN " +
                        "GenerationParamValues ON GenerationParamValues.AssemblyID=Assemblies.AssemblyID " +
                        "WHERE Assemblies.ModelID=@ModelID AND GenerationParamValues.GenerationParamID=@GenerationParamID";
                    command.CommandText = sqlQuery;
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@ModelID", SqlDbType.Int).Value = GetModelID(modelType);
                    command.Parameters.Add("@GenerationParamID", SqlDbType.Int).Value = id;

                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            result.Add(dr["Value"].ToString());
                        }
                    }
                }

                conn.Close();
            }

            result.Sort();
            result = result.Distinct().ToList();
            return result;
        }

        // Возвращает список строковых значений параметра генерации с данным идентификатором
        // по имени модели и по значениям других параметров генерации.
        public List<string> GetParameterValuesByID(Type modelType, Dictionary<int, string> values,
            int id)
        {
            List<string> result = new List<string>();

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = GetConnectionString();
                conn.Open();

                using (SqlCommand command = (SqlCommand)conn.CreateCommand())
                {
                    string sqlQuery = "SELECT DISTINCT [Value] " +
                        "FROM GenerationParamValues " +
                        "WHERE GenerationParamID=@GenerationParamID AND AssemblyID IN " +
                        "(SELECT AssemblyID FROM Assemblies WHERE ModelID=@ModelID) AND " +
                        "AssemblyID IN (";

                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@ModelID", SqlDbType.Int).Value = GetModelID(modelType);
                    command.Parameters.Add("@GenerationParamID", SqlDbType.Int).Value = id;

                    Dictionary<int, string>.KeyCollection keys = values.Keys;
                    int i = 1;
                    foreach (int key in keys)
                    {
                        command.Parameters.Add("@Param" + i.ToString(), SqlDbType.Int).Value = key;
                        command.Parameters.Add("@ParamValue" + i.ToString(), SqlDbType.NVarChar).Value =
                            values[key];

                        if (i > 1)
                        {
                            sqlQuery += " INTERSECT ";
                        }

                        sqlQuery += "SELECT AssemblyID " +
                            "FROM GenerationParamValues " +
                            "WHERE GenerationParamID = @Param" + i.ToString() +
                            " AND [Value] = @ParamValue" + i.ToString();

                        ++i;
                    }

                    sqlQuery += ")";
                    command.CommandText = sqlQuery;

                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            result.Add(dr["Value"].ToString());
                        }
                    }
                }
            }

            return result;
        }

        // Возвращает список строковых значений параметра анализа с данным идентификатором
        // по имени модели и по значениям параметров генерации.
        public List<string> GetOptionParameterValuesByID(Type modelType, Dictionary<int, string> values,
            int id)
        {
            List<string> result = new List<string>();

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = GetConnectionString();
                conn.Open();

                using (SqlCommand command = (SqlCommand)conn.CreateCommand())
                {
                    string sqlQuery = "SELECT DISTINCT [Value] " +
                        "FROM AnalyzeOptionParamValues " +
                        "WHERE AnalyzeOptionParamID=@AnalyzeOptionParamID AND " +
                        "AssemblyID IN (SELECT AssemblyID FROM Assemblies WHERE ModelID=@ModelID) AND " +
                        "AssemblyID IN (";

                    command.CommandType = CommandType.Text;
                    command.Parameters.Add("@ModelID", SqlDbType.Int).Value = GetModelID(modelType);
                    command.Parameters.Add("@AnalyzeOptionParamID", SqlDbType.Int).Value = id;

                    Dictionary<int, string>.KeyCollection keys = values.Keys;
                    int i = 1;
                    foreach (int key in keys)
                    {
                        command.Parameters.Add("@Param" + i.ToString(), SqlDbType.Int).Value = key;
                        command.Parameters.Add("@ParamValue" + i.ToString(), SqlDbType.NVarChar).Value =
                            values[key];

                        if (i > 1)
                        {
                            sqlQuery += " INTERSECT ";
                        }

                        sqlQuery += "SELECT AssemblyID " +
                            "FROM GenerationParamValues " +
                            "WHERE GenerationParamID = @Param" + i.ToString() +
                            " AND [Value] = @ParamValue" + i.ToString();

                        ++i;
                    }

                    sqlQuery += ")";
                    command.CommandText = sqlQuery;

                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            result.Add(dr["Value"].ToString());
                        }
                    }
                }
            }

            return result;
        }

        // Добавление строк в усредненные таблицы для всех сборок в БД.
        // Вызывается в ручную перед статистическим анализом.
        public void FillAvgTablesForAllJobs()
        {
            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = GetConnectionString();
                conn.Open();

                List<Guid> assemblyIDs = new List<Guid>();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT * FROM Assemblies";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            assemblyIDs.Add((Guid)dr["AssemblyID"]);
                        }
                    }
                }

                foreach (Guid assemblyID in assemblyIDs)
                {
                    FillAvgTables(conn, assemblyID);
                }
            }
        }

        // Добавление строк в усредненные таблицы для сбороки с данным именем.
        // Вызывается в ручную перед статистическим анализом данной сборки.
        public void FillAvgTablesForCurrentJob(string jobName)
        {
            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = GetConnectionString();
                conn.Open();

                Guid assemblyId = new Guid();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT * FROM Assemblies WHERE Name=@Name";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpResultsID = provider.CreateParameter();
                    dpResultsID.ParameterName = "Name";
                    dpResultsID.Value = jobName;
                    cmd.Parameters.Add(dpResultsID);

                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            assemblyId = (Guid)dr["AssemblyID"];
                        }
                    }
                }

                FillAvgTables(conn, assemblyId);
            }
        }

        // Получение Avg и Sigma для траектории триугольников.
        // Используется хранимая процедура "CountTriangleTrajectoryAvgSigma".
        public void GetAvgSigma(Guid id, int k, out double avg, out double sigma)
        {
            avg = 0;
            sigma = 0;

            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = GetConnectionString();
                conn.Open();

                SqlCommand command = (SqlCommand)conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@AssID", SqlDbType.UniqueIdentifier).Value = id;
                command.Parameters.Add("@StepsToRemove", SqlDbType.Int).Value = k;
                command.CommandText = "CountTriangleTrajectoryAvgSigma";
                command.ExecuteNonQuery();

                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT * FROM TriangleTrajectoryAvgSigma " +
                        "WHERE AssemblyID=@AssemblyID AND StepsToRemove=@StepsToRemove";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter p1 = provider.CreateParameter();
                    p1.ParameterName = "AssemblyID";
                    p1.Value = id;
                    cmd.Parameters.Add(p1);

                    DbParameter p2 = provider.CreateParameter();
                    p2.ParameterName = "StepsToRemove";
                    p2.Value = k;
                    cmd.Parameters.Add(p2);

                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            avg = (double)dr["Average"];
                            sigma = (double)dr["Sigma"];
                        }
                    }
                }
            }
        }

        // Утилиты.

        // Возвращает строку соединения.
        private string GetConnectionString()
        {
            return connSettings.ConnectionString;
        }

        #region Save and Load implementation

        private void SaveAssembliesTable(DbConnection conn, ResultAssembly assembly)
        {
            log.Info("Saving data to Assemblies table.");
            using (DbCommand cmd = conn.CreateCommand())
            {
                string sqlQuery = "INSERT INTO Assemblies(AssemblyID,ModelID,Name,Date)" +
                    "VALUES(@AssemblyID,@ModelID,@Name,getDate())";
                cmd.CommandText = sqlQuery;
                cmd.CommandType = CommandType.Text;

                DbParameter dpID = provider.CreateParameter();
                dpID.ParameterName = "AssemblyID";
                dpID.Value = assembly.ID;
                cmd.Parameters.Add(dpID);

                DbParameter dpModelID = provider.CreateParameter();
                dpModelID.ParameterName = "ModelID";
                dpModelID.Value = GetModelID(assembly.ModelType);
                cmd.Parameters.Add(dpModelID);

                DbParameter dpName = provider.CreateParameter();
                dpName.ParameterName = "Name";
                dpName.Value = assembly.Name;
                cmd.Parameters.Add(dpName);

                cmd.ExecuteNonQuery();
            }
        }

        private void SaveGenerationParameters(DbConnection conn, ResultAssembly assembly)
        {
            log.Info("Saving generation params values in DB.");
            foreach (GenerationParam genParameter in assembly.GenerationParams.Keys)
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "INSERT INTO GenerationParamValues(AssemblyID,GenerationParamID,Value) " +
                                        "VALUES(@AssemblyID,@GenerationParamID,@Value)";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpAssemblyID = provider.CreateParameter();
                    dpAssemblyID.ParameterName = "AssemblyID";
                    dpAssemblyID.Value = assembly.ID;
                    cmd.Parameters.Add(dpAssemblyID);

                    DbParameter dpGenerationParamID = provider.CreateParameter();
                    dpGenerationParamID.ParameterName = "GenerationParamID";
                    dpGenerationParamID.Value = Convert.ToInt32(genParameter);
                    cmd.Parameters.Add(dpGenerationParamID);

                    DbParameter dpValue = provider.CreateParameter();
                    dpValue.ParameterName = "Value";
                    dpValue.Value = assembly.GenerationParams[genParameter].ToString();
                    cmd.Parameters.Add(dpValue);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void SaveAnalyzeParameters(DbConnection conn, ResultAssembly assembly)
        {
            // !исправить! ????
            log.Info("Saving analyze option params values in DB.");
            using (DbCommand cmd = conn.CreateCommand())
            {
                string sqlQuery = "INSERT INTO AnalyzeOptionParamValues(AssemblyID,AnalyzeOptionParamID,Value) " +
                                    "VALUES(@AssemblyID,@AnalyzeOptionParamID,@Value)";
                cmd.CommandText = sqlQuery;
                cmd.CommandType = CommandType.Text;

                DbParameter dpAssemblyID = provider.CreateParameter();
                dpAssemblyID.ParameterName = "AssemblyID";
                dpAssemblyID.Value = assembly.ID;
                cmd.Parameters.Add(dpAssemblyID);

                DbParameter dpGenerationParamID = provider.CreateParameter();
                dpGenerationParamID.ParameterName = "AnalyzeOptionParamID";
                dpGenerationParamID.Value = 5;
                cmd.Parameters.Add(dpGenerationParamID);

                DbParameter dpValue = provider.CreateParameter();
                dpValue.ParameterName = "Value";
                dpValue.Value = assembly.AnalyzeOptionParams[AnalyzeOptionParam.TrajectoryMu].ToString();
                cmd.Parameters.Add(dpValue);

                cmd.ExecuteNonQuery();
            }

            using (DbCommand cmd = conn.CreateCommand())
            {
                string sqlQuery = "INSERT INTO AnalyzeOptionParamValues(AssemblyID,AnalyzeOptionParamID,Value) " +
                                    "VALUES(@AssemblyID,@AnalyzeOptionParamID,@Value)";
                cmd.CommandText = sqlQuery;
                cmd.CommandType = CommandType.Text;

                DbParameter dpAssemblyID = provider.CreateParameter();
                dpAssemblyID.ParameterName = "AssemblyID";
                dpAssemblyID.Value = assembly.ID;
                cmd.Parameters.Add(dpAssemblyID);

                DbParameter dpGenerationParamID = provider.CreateParameter();
                dpGenerationParamID.ParameterName = "AnalyzeOptionParamID";
                dpGenerationParamID.Value = 6;
                cmd.Parameters.Add(dpGenerationParamID);

                DbParameter dpValue = provider.CreateParameter();
                dpValue.ParameterName = "Value";
                dpValue.Value = assembly.AnalyzeOptionParams[AnalyzeOptionParam.TrajectoryStepCount].ToString();
                cmd.Parameters.Add(dpValue);

                cmd.ExecuteNonQuery();
            }
        }

        private void SaveAnalyzeResults(DbConnection conn, ResultAssembly assembly)
        {
            log.Info("Saving data to AssemblyResults table.");
            foreach (AnalizeResult result in assembly.Results)
            {
                int resultsID = 0;
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "INSERT INTO AssemblyResults(AssemblyID,NetworkSize) VALUES(@AssemblyID,@NetworkSize) " +
                                        "SELECT ResultsID FROM AssemblyResults WHERE ResultsID=SCOPE_IDENTITY()";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpAssemblyID = provider.CreateParameter();
                    dpAssemblyID.ParameterName = "AssemblyID";
                    dpAssemblyID.Value = assembly.ID;
                    cmd.Parameters.Add(dpAssemblyID);

                    DbParameter dpNetworkSize = provider.CreateParameter();
                    dpNetworkSize.ParameterName = "NetworkSize";
                    dpNetworkSize.Value = result.Size;
                    cmd.Parameters.Add(dpNetworkSize);

                    resultsID = (int)cmd.ExecuteScalar();
                }

                log.Info("Saving data to AnalyzeResults table.");
                foreach (AnalyseOptions analyseOption in result.Result.Keys)
                {
                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "INSERT INTO AnalyzeResults(ResultsID,AnalyzeOptionID,Result) " +
                                            "VALUES(@ResultsID,@AnalyzeOptionID,@Result)";
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter dpResultsID = provider.CreateParameter();
                        dpResultsID.ParameterName = "ResultsID";
                        dpResultsID.Value = resultsID;
                        cmd.Parameters.Add(dpResultsID);

                        DbParameter dpAnalyseOptionID = provider.CreateParameter();
                        dpAnalyseOptionID.ParameterName = "AnalyzeOptionID";
                        dpAnalyseOptionID.Value = Convert.ToInt32(analyseOption);
                        cmd.Parameters.Add(dpAnalyseOptionID);

                        DbParameter dpResult = provider.CreateParameter();
                        dpResult.ParameterName = "Result";
                        dpResult.Value = result.Result[analyseOption];
                        cmd.Parameters.Add(dpResult);

                        cmd.ExecuteNonQuery();
                    }
                }
                
                SqlBulkCopy cp = new SqlBulkCopy(GetConnectionString(),
                    SqlBulkCopyOptions.CheckConstraints);

                log.Info("Saving data to VertexDegree table.");
                DataTable commonTable = new DataTable();
                commonTable.Columns.Add("ResultsID", typeof(Int32));
                commonTable.Columns.Add("Degree", typeof(Int32));
                commonTable.Columns.Add("Count", typeof(Int32));

                foreach (int degree in result.VertexDegree.Keys)
                {
                    DataRow r = commonTable.NewRow();
                    r[0] = resultsID;
                    r[1] = degree;
                    r[2] = result.VertexDegree[degree];
                    commonTable.Rows.Add(r);
                }

                cp.DestinationTableName = "VertexDegree";
                cp.WriteToServer(commonTable);

                log.Info("Saving data to VertexDegreeLocal table.");
                commonTable.Rows.Clear();
                commonTable.Columns.Clear();
                commonTable.Columns.Add("AssemblyID", typeof(Guid));
                commonTable.Columns.Add("Degree", typeof(Int32));
                commonTable.Columns.Add("Distribution", typeof(Double));

                foreach (int degree in assembly.VertexDegreeLocal.Keys)
                {
                    DataRow r = commonTable.NewRow();
                    r[0] = assembly.ID;
                    r[1] = degree;
                    r[2] = assembly.VertexDegreeLocal[degree];
                    commonTable.Rows.Add(r);
                }

                cp.DestinationTableName = "VertexDegreeLocal";
                cp.WriteToServer(commonTable);

                log.Info("Saving data to Coefficients table.");
                commonTable.Rows.Clear();
                commonTable.Columns.Clear();
                commonTable.Columns.Add("ResultsID", typeof(Int32));
                commonTable.Columns.Add("Coefficient", typeof(Double));
                commonTable.Columns.Add("Count", typeof(Int32));

                foreach (double coefficient in result.Coefficient.Keys)
                {
                    DataRow r = commonTable.NewRow();
                    r[0] = resultsID;
                    r[1] = coefficient;
                    r[2] = result.Coefficient[coefficient];
                    commonTable.Rows.Add(r);
                }

                cp.DestinationTableName = "Coefficients";
                cp.WriteToServer(commonTable);

                log.Info("Saving data to CoefficientsLocal table.");
                commonTable.Rows.Clear();
                commonTable.Columns.Clear();
                commonTable.Columns.Add("AssemblyID", typeof(Guid));
                commonTable.Columns.Add("Coefficient", typeof(Double));
                commonTable.Columns.Add("Distribution", typeof(Double));

                foreach (double coeff in assembly.CoefficientsLocal.Keys)
                {
                    DataRow r = commonTable.NewRow();
                    r[0] = assembly.ID;
                    r[1] = coeff;
                    r[2] = assembly.CoefficientsLocal[coeff];
                    commonTable.Rows.Add(r);
                }

                cp.DestinationTableName = "CoefficientsLocal";
                cp.WriteToServer(commonTable);

                log.Info("Saving data to ConSubgraphs table.");
                commonTable.Rows.Clear();
                commonTable.Columns.Clear();
                commonTable.Columns.Add("ResultsID", typeof(Int32));
                commonTable.Columns.Add("VX", typeof(Int32));
                commonTable.Columns.Add("Count", typeof(Int32));

                foreach (int subgraph in result.Subgraphs.Keys)
                {
                    DataRow r = commonTable.NewRow();
                    r[0] = resultsID;
                    r[1] = subgraph;
                    r[2] = result.Subgraphs[subgraph];
                    commonTable.Rows.Add(r);
                }

                cp.DestinationTableName = "ConSubgraphs";
                cp.WriteToServer(commonTable);

                log.Info("Saving data to ConSubgraphsLocal table.");
                commonTable.Rows.Clear();
                commonTable.Columns.Clear();
                commonTable.Columns.Add("AssemblyID", typeof(Guid));
                commonTable.Columns.Add("VX", typeof(int));
                commonTable.Columns.Add("Distribution", typeof(Double));

                foreach (int vx in assembly.SubgraphsLocal.Keys)
                {
                    DataRow r = commonTable.NewRow();
                    r[0] = assembly.ID;
                    r[1] = vx;
                    r[2] = assembly.SubgraphsLocal[vx];
                    commonTable.Rows.Add(r);
                }

                cp.DestinationTableName = "ConSubgraphsLocal";
                cp.WriteToServer(commonTable);

                log.Info("Saving data to FullSubgraphs table.");
                commonTable.Rows.Clear();
                commonTable.Columns.Clear();
                commonTable.Columns.Add("ResultsID", typeof(Int32));
                commonTable.Columns.Add("VX", typeof(Int32));
                commonTable.Columns.Add("Count", typeof(Int32));

                foreach (int subgraph in result.FullSubgraphs.Keys)
                {
                    DataRow r = commonTable.NewRow();
                    r[0] = resultsID;
                    r[1] = subgraph;
                    r[2] = result.FullSubgraphs[subgraph];
                    commonTable.Rows.Add(r);
                }

                cp.DestinationTableName = "FullSubgraphs";
                cp.WriteToServer(commonTable);

                log.Info("Saving data to VertexDistance table.");
                commonTable.Rows.Clear();
                commonTable.Columns.Clear();
                commonTable.Columns.Add("ResultsID", typeof(Int32));
                commonTable.Columns.Add("Distance", typeof(Int32));
                commonTable.Columns.Add("Count", typeof(Int32));

                foreach (int dist in result.DistanceBetweenVertices.Keys)
                {
                    DataRow r = commonTable.NewRow();
                    r[0] = resultsID;
                    r[1] = dist;
                    r[2] = result.DistanceBetweenVertices[dist];
                    commonTable.Rows.Add(r);
                }

                cp.DestinationTableName = "VertexDistance";
                cp.WriteToServer(commonTable);

                cp.DestinationTableName = "ConSubgraphs";
                cp.WriteToServer(commonTable);

                log.Info("Saving data to VertexDistanceLocal table.");
                commonTable.Rows.Clear();
                commonTable.Columns.Clear();
                commonTable.Columns.Add("AssemblyID", typeof(Guid));
                commonTable.Columns.Add("Distance", typeof(int));
                commonTable.Columns.Add("Distribution", typeof(Double));

                foreach (int dist in assembly.DistanceBetweenVerticesLocal.Keys)
                {
                    DataRow r = commonTable.NewRow();
                    r[0] = assembly.ID;
                    r[1] = dist;
                    r[2] = assembly.DistanceBetweenVerticesLocal[dist];
                    commonTable.Rows.Add(r);
                }

                cp.DestinationTableName = "VertexDistanceLocal";
                cp.WriteToServer(commonTable);

                log.Info("Saving data to EigenValues table.");
                commonTable.Rows.Clear();
                commonTable.Columns.Clear();
                commonTable.Columns.Add("ResultsID", typeof(Int32));
                commonTable.Columns.Add("EigenValue", typeof(Double));

                foreach (double value in result.EigenVector)
                {
                    DataRow r = commonTable.NewRow();
                    r[0] = resultsID;
                    r[1] = value;
                    commonTable.Rows.Add(r);
                }

                cp.DestinationTableName = "EigenValues";
                cp.WriteToServer(commonTable);

                log.Info("Saving data to EigenValuesDistance table.");
                commonTable.Rows.Clear();
                commonTable.Columns.Clear();
                commonTable.Columns.Add("ResultsID", typeof(Int32));
                commonTable.Columns.Add("Distance", typeof(Double));
                commonTable.Columns.Add("Count", typeof(Int32));

                foreach (double dist in result.DistancesBetweenEigenValues.Keys)
                {
                    DataRow r = commonTable.NewRow();
                    r[0] = resultsID;
                    r[1] = dist;
                    r[2] = result.DistancesBetweenEigenValues[dist];
                    commonTable.Rows.Add(r);
                }

                cp.DestinationTableName = "EigenValuesDistance";
                cp.WriteToServer(commonTable);

                log.Info("Saving data to Cycles table.");
                commonTable.Rows.Clear();
                commonTable.Columns.Clear();
                commonTable.Columns.Add("ResultsID", typeof(Int32));
                commonTable.Columns.Add("Order", typeof(Int32));
                commonTable.Columns.Add("Count", typeof(long));

                foreach (int order in result.Cycles.Keys)
                {
                    DataRow r = commonTable.NewRow();
                    r[0] = resultsID;
                    r[1] = order;
                    r[2] = result.Cycles[order];
                    commonTable.Rows.Add(r);
                }

                log.Info("Saving data to EigenValuesDistanceLocal table.");
                commonTable.Rows.Clear();
                commonTable.Columns.Clear();
                commonTable.Columns.Add("AssemblyID", typeof(Guid));
                commonTable.Columns.Add("Distance", typeof(Double));
                commonTable.Columns.Add("Distribution", typeof(Double));

                foreach (double dist in assembly.DistancesBetweenEigenValuesLocal.Keys)
                {
                    DataRow r = commonTable.NewRow();
                    r[0] = assembly.ID;
                    r[1] = dist;
                    r[2] = assembly.DistancesBetweenEigenValuesLocal[dist];
                    commonTable.Rows.Add(r);
                }

                cp.DestinationTableName = "EigenValuesDistanceLocal";
                cp.WriteToServer(commonTable);

                cp.DestinationTableName = "Cycles";
                cp.WriteToServer(commonTable);

                log.Info("Saving data to Triangles table.");
                commonTable.Rows.Clear();
                commonTable.Columns.Clear();
                commonTable.Columns.Add("ResultsID", typeof(Int32));
                commonTable.Columns.Add("TriangleCount", typeof(Int32));
                commonTable.Columns.Add("Count", typeof(Int32));

                foreach (int tr in result.TriangleCount.Keys)
                {
                    DataRow r = commonTable.NewRow();
                    r[0] = resultsID;
                    r[1] = tr;
                    r[2] = result.TriangleCount[tr];
                    commonTable.Rows.Add(r);
                }

                cp.DestinationTableName = "Triangles";
                cp.WriteToServer(commonTable);

                log.Info("Saving data to TriangleTrajectory table.");
                commonTable.Rows.Clear();
                commonTable.Columns.Clear();
                commonTable.Columns.Add("ResultsID", typeof(Int32));
                commonTable.Columns.Add("Time", typeof(Int32));
                commonTable.Columns.Add("TriangleCount", typeof(Double));

                foreach (int time in result.TriangleTrajectory.Keys)
                {
                    DataRow r = commonTable.NewRow();
                    r[0] = resultsID;
                    r[1] = time;
                    r[2] = result.TriangleTrajectory[time];
                    commonTable.Rows.Add(r);
                }

                cp.DestinationTableName = "TriangleTrajectory";
                cp.WriteToServer(commonTable);

                log.Info("Saving data to TriangleTrajectoryLocal table.");
                commonTable.Rows.Clear();
                commonTable.Columns.Clear();
                commonTable.Columns.Add("AssemblyID", typeof(Guid));
                commonTable.Columns.Add("Time", typeof(int));
                commonTable.Columns.Add("Distribution", typeof(Double));

                foreach (int time in assembly.TriangleTrajectoryLocal.Keys)
                {
                    DataRow r = commonTable.NewRow();
                    r[0] = assembly.ID;
                    r[1] = time;
                    r[2] = assembly.TriangleTrajectoryLocal[time];
                    commonTable.Rows.Add(r);
                }

                cp.DestinationTableName = "TriangleTrajectoryLocal";
                cp.WriteToServer(commonTable);

                log.Info("Saving data to Motifs table.");
                commonTable.Rows.Clear();
                commonTable.Columns.Clear();
                commonTable.Columns.Add("ResultsID", typeof(Int32));
                commonTable.Columns.Add("ID", typeof(Int32));
                commonTable.Columns.Add("Count", typeof(float));

                foreach (int id in result.MotivesCount.Keys)
                {
                    DataRow r = commonTable.NewRow();
                    r[0] = resultsID;
                    r[1] = id;
                    r[2] = result.MotivesCount[id];
                    commonTable.Rows.Add(r);
                }

                cp.DestinationTableName = "Motifs";
                cp.WriteToServer(commonTable);

                cp.Close();
            }
        }

        private void LoadAssemblyInformation(DbConnection conn, ResultAssembly resultAssembly)
        {
            log.Info("Loading data from tables Assemblies, GenerationParamValues, AnalyzeOptionParamValues.");
            using (DbCommand cmd = conn.CreateCommand())
            {
                string sqlQuery = "SELECT Assemblies.[Name], Assemblies.ModelID, GenerationParamValues.* FROM Assemblies " +
                                    "LEFT JOIN GenerationParamValues ON GenerationParamValues.AssemblyID=Assemblies.AssemblyID " +
                                    "WHERE Assemblies.AssemblyID=@AssemblyID ORDER BY GenerationParamID";
                cmd.CommandText = sqlQuery;
                cmd.CommandType = CommandType.Text;

                DbParameter dpID = provider.CreateParameter();
                dpID.ParameterName = "AssemblyID";
                dpID.Value = resultAssembly.ID;
                cmd.Parameters.Add(dpID);

                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        resultAssembly.ModelType = GetModelType((int)dr["ModelID"]);
                        resultAssembly.Name = (string)dr["Name"];

                        GenerationParam param = (GenerationParam)Enum.ToObject(typeof(GenerationParam),
                            (int)dr["GenerationParamID"]);

                        GenerationParamInfo paramInfo = (GenerationParamInfo)(param.GetType().GetField(param.ToString()).GetCustomAttributes(typeof(GenerationParamInfo), false)[0]);
                        if (paramInfo.Type.Equals(typeof(Double)))
                        {
                            resultAssembly.GenerationParams.Add(param, Convert.ToDouble(dr["Value"], CultureInfo.InvariantCulture));
                        }
                        else if (paramInfo.Type.Equals(typeof(Int16)))
                        {
                            resultAssembly.GenerationParams.Add(param, Convert.ToInt16(dr["Value"]));
                        }
                        else if (paramInfo.Type.Equals(typeof(Int32)))
                        {
                            resultAssembly.GenerationParams.Add(param, Convert.ToInt32(dr["Value"]));
                        }
                        else if (paramInfo.Type.Equals(typeof(bool)))
                        {
                            resultAssembly.GenerationParams.Add(param, Convert.ToBoolean(dr["Value"]));
                        }
                        else if (paramInfo.Type.Equals(typeof(String)))
                        {
                            resultAssembly.GenerationParams.Add(param, Convert.ToString(dr["Value"]));
                        }
                    }
                }

                sqlQuery = "SELECT Assemblies.[Name],AnalyzeOptionParamValues.* FROM Assemblies " +
                    "LEFT JOIN AnalyzeOptionParamValues ON AnalyzeOptionParamValues.AssemblyID=Assemblies.AssemblyID " +
                    "WHERE Assemblies.AssemblyID=@AssemblyID ORDER BY AnalyzeOptionParamID";
                cmd.CommandText = sqlQuery;
                cmd.CommandType = CommandType.Text;

                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        AnalyzeOptionParam param = (AnalyzeOptionParam)Enum.ToObject(typeof(AnalyzeOptionParam),
                            (int)dr["AnalyzeOptionParamID"]);

                        AnalyzeOptionParamInfo paramInfo = (AnalyzeOptionParamInfo)(param.GetType().GetField(param.ToString()).GetCustomAttributes(typeof(AnalyzeOptionParamInfo), false)[0]);
                        if (paramInfo.Type.Equals(typeof(Double)))
                        {
                            resultAssembly.AnalyzeOptionParams[param] = Convert.ToDouble(dr["Value"],
                                CultureInfo.InvariantCulture);
                        }
                        else if (paramInfo.Type.Equals(typeof(Int16)))
                        {
                            resultAssembly.AnalyzeOptionParams[param] = Convert.ToInt16(dr["Value"]);
                        }
                        else if (paramInfo.Type.Equals(typeof(BigInteger)))
                        {
                            resultAssembly.AnalyzeOptionParams[param] = Convert.ToString(dr["Value"]);
                        }
                    }
                }
            }
        }

        private void LoadAnalyzeResults(DbConnection conn, ResultAssembly resultAssembly)
        {
            Guid assemblyID = resultAssembly.ID;

            log.Info("Loading data from table CoefficientsLocal.");
            using (DbCommand cmd = conn.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM CoefficientsLocal WHERE AssemblyID=@AssemblyID";
                cmd.CommandText = sqlQuery;
                cmd.CommandType = CommandType.Text;

                DbParameter dpResultsID = provider.CreateParameter();
                dpResultsID.ParameterName = "AssemblyID";
                dpResultsID.Value = assemblyID;
                cmd.Parameters.Add(dpResultsID);

                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        resultAssembly.CoefficientsLocal.Add(Convert.ToDouble(dr["Coefficient"]),
                            Convert.ToDouble(dr["Distribution"]));
                    }
                }
            }

            log.Info("Loading data from table VertexDegreeLocal.");
            using (DbCommand cmd = conn.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM VertexDegreeLocal WHERE AssemblyID=@AssemblyID";
                cmd.CommandText = sqlQuery;
                cmd.CommandType = CommandType.Text;

                DbParameter dpResultsID = provider.CreateParameter();
                dpResultsID.ParameterName = "AssemblyID";
                dpResultsID.Value = assemblyID;
                cmd.Parameters.Add(dpResultsID);

                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        resultAssembly.VertexDegreeLocal.Add(Convert.ToDouble(dr["Degree"]),
                            (double)dr["Distribution"]);
                    }
                }
            }

            log.Info("Loading data from table ConSubgraphsLocal.");
            using (DbCommand cmd = conn.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM ConSubgraphsLocal WHERE AssemblyID=@AssemblyID";
                cmd.CommandText = sqlQuery;
                cmd.CommandType = CommandType.Text;

                DbParameter dpResultsID = provider.CreateParameter();
                dpResultsID.ParameterName = "AssemblyID";
                dpResultsID.Value = assemblyID;
                cmd.Parameters.Add(dpResultsID);

                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        resultAssembly.SubgraphsLocal.Add(Convert.ToDouble(dr["VX"]), (double)dr["Distribution"]);
                    }
                }
            }

            log.Info("Loading data from table VertexDistanceLocal.");
            using (DbCommand cmd = conn.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM VertexDistanceLocal WHERE AssemblyID=@AssemblyID";
                cmd.CommandText = sqlQuery;
                cmd.CommandType = CommandType.Text;

                DbParameter dpResultsID = provider.CreateParameter();
                dpResultsID.ParameterName = "AssemblyID";
                dpResultsID.Value = assemblyID;
                cmd.Parameters.Add(dpResultsID);

                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        resultAssembly.DistanceBetweenVerticesLocal.Add(Convert.ToDouble(dr["Distance"]),
                            (double)dr["Distribution"]);
                    }
                }
            }

            log.Info("Loading data from table EigenValuesDistanceLocal.");
            using (DbCommand cmd = conn.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM EigenValuesDistanceLocal WHERE AssemblyID=@AssemblyID";
                cmd.CommandText = sqlQuery;
                cmd.CommandType = CommandType.Text;

                DbParameter dpResultsID = provider.CreateParameter();
                dpResultsID.ParameterName = "AssemblyID";
                dpResultsID.Value = assemblyID;
                cmd.Parameters.Add(dpResultsID);

                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        resultAssembly.DistancesBetweenEigenValuesLocal.Add((double)dr["Distance"],
                            (double)dr["Distribution"]);
                    }
                }
            }

            log.Info("Loading data from table TriangleTrajectoryLocal.");
            using (DbCommand cmd = conn.CreateCommand())
            {
                string sqlQuery = "SELECT * FROM TriangleTrajectoryLocal WHERE AssemblyID=@AssemblyID";
                cmd.CommandText = sqlQuery;
                cmd.CommandType = CommandType.Text;

                DbParameter dpResultsID = provider.CreateParameter();
                dpResultsID.ParameterName = "AssemblyID";
                dpResultsID.Value = assemblyID;
                cmd.Parameters.Add(dpResultsID);

                using (DbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        resultAssembly.TriangleTrajectoryLocal.Add(Convert.ToDouble(dr["Time"]),
                            (double)dr["Distribution"]);
                    }
                }
            }

            log.Info("Loading analyze results data.");
            using (DbCommand mainCmd = conn.CreateCommand())
            {
                string sql = "SELECT ResultsID, NetworkSize FROM AssemblyResults WHERE AssemblyID=@AssemblyID";
                mainCmd.CommandText = sql;
                mainCmd.CommandType = CommandType.Text;

                DbParameter dpID = provider.CreateParameter();
                dpID.ParameterName = "AssemblyID";
                dpID.Value = assemblyID;
                mainCmd.Parameters.Add(dpID);

                List<KeyValuePair<int,int>> resultInfos = new List<KeyValuePair<int,int>>();
                using (DbDataReader mainReader = mainCmd.ExecuteReader())
                {
                    while (mainReader.Read())
                    {
                        resultInfos.Add(new KeyValuePair<int,int>((int)mainReader["ResultsID"],
                            (int)mainReader["NetworkSize"]));
                    }
                }

                foreach (KeyValuePair<int, int> info in resultInfos)
                {
                    AnalizeResult result = new AnalizeResult();
                    result.Size = info.Value;
                    resultAssembly.Results.Add(result);

                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT * FROM AnalyzeResults WHERE ResultsID=@ResultsID";
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter dpResultsID = provider.CreateParameter();
                        dpResultsID.ParameterName = "ResultsID";
                        dpResultsID.Value = info.Key;
                        cmd.Parameters.Add(dpResultsID);

                        using (DbDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                result.Result.Add((AnalyseOptions)Enum.ToObject(typeof(AnalyseOptions), 
                                    dr["AnalyzeOptionID"]), Convert.ToDouble(dr["Result"]));
                            }
                        }
                    }

                    // Coefficient Global
                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT * FROM CoefficientsGlobal WHERE ResultsID=@ResultsID";
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter dpResultsID = provider.CreateParameter();
                        dpResultsID.ParameterName = "ResultsID";
                        dpResultsID.Value = info.Key;
                        cmd.Parameters.Add(dpResultsID);

                        using (DbDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                result.CoefficientGlobal = (double)dr["AvgCoefficient"];
                            }
                        }
                    }

                    // Degree Global
                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT * FROM VertexDegreeGlobal WHERE ResultsID=@ResultsID";
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter dpResultsID = provider.CreateParameter();
                        dpResultsID.ParameterName = "ResultsID";
                        dpResultsID.Value = info.Key;
                        cmd.Parameters.Add(dpResultsID);

                        using (DbDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                result.DegreeGlobal = (double)dr["AvgDegree"];
                            }
                        }
                    }

                    log.Info("Loading data from table FullSubgraphs.");
                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT * FROM FullSubgraphs  WHERE ResultsID=@ResultsID";
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter dpResultsID = provider.CreateParameter();
                        dpResultsID.ParameterName = "ResultsID";
                        dpResultsID.Value = info.Key;
                        cmd.Parameters.Add(dpResultsID);

                        using (DbDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                result.FullSubgraphs.Add(Convert.ToInt32(dr["VX"]), (int)dr["Count"]);
                            }
                        }
                    }

                    log.Info("Loading data from table EigenValue.");    // !проверить!
                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT * FROM EigenValues  WHERE ResultsID=@ResultsID";
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter dpResultsID = provider.CreateParameter();
                        dpResultsID.ParameterName = "ResultsID";
                        dpResultsID.Value = info.Key;
                        cmd.Parameters.Add(dpResultsID);

                        using (DbDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                result.EigenVector.Add(dr["EigenValue"]);
                            }
                        }
                    }

                    log.Info("Loading data from table Cycles.");    // !проверить!
                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT * FROM Cycles  WHERE ResultsID=@ResultsID";
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter dpResultsID = provider.CreateParameter();
                        dpResultsID.ParameterName = "ResultsID";
                        dpResultsID.Value = info.Key;
                        cmd.Parameters.Add(dpResultsID);

                        using (DbDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                result.Cycles.Add(Convert.ToInt32(dr["Order"]), (long)dr["Count"]);
                            }
                        }
                    }

                    log.Info("Loading data from table Triangles.");
                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT * FROM Triangles  WHERE ResultsID=@ResultsID";
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter dpResultsID = provider.CreateParameter();
                        dpResultsID.ParameterName = "ResultsID";
                        dpResultsID.Value = info.Key;
                        cmd.Parameters.Add(dpResultsID);

                        using (DbDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                result.TriangleCount.Add(Convert.ToInt32(dr["TriangleCount"]), (int)dr["Count"]);
                            }
                        }
                    }

                    log.Info("Loading data from table Motifs.");
                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "SELECT * FROM Motifs  WHERE ResultsID=@ResultsID";
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter dpResultsID = provider.CreateParameter();
                        dpResultsID.ParameterName = "ResultsID";
                        dpResultsID.Value = info.Key;
                        cmd.Parameters.Add(dpResultsID);

                        using (DbDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                result.MotivesCount.Add(Convert.ToInt32(dr["ID"]), float.Parse(dr["Count"].ToString()));
                            }
                        }
                    }
                }
            }
        }

        #endregion

        // Добавление строк в усредненные таблицы для данной сборки.
        // Используется хранимая процедура "FillOptimizationTables".
        private void FillAvgTables(DbConnection conn, Guid assemblyID)
        {
            SqlCommand command = (SqlCommand)conn.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 3600;  // Ждать час.
            command.Parameters.Add("@AssID", SqlDbType.UniqueIdentifier).Value = assemblyID;
            command.CommandText = "FillOptimizationTables";
            command.ExecuteNonQuery();
        }
    }
}
