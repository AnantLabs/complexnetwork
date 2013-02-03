using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Configuration;
using System.Data;

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

        private string GetConnectionString()
        {
            return connSettings.ConnectionString;
        }

        #region IResultStorage Members

        public override void Save(ResultAssembly assembly)
        {
            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = GetConnectionString();
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
                    return;
                }
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "INSERT INTO Assemblies(AssemblyID,ModelID,Name,Date) VALUES(@AssemblyID,@ModelID,@Name,getDate())";
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

                foreach (AnalizeResult result in assembly.Results)
                {
                    int resultsID = 0;
                    using (DbCommand cmd = conn.CreateCommand())
                    {
                        string sqlQuery = "INSERT INTO AssemblyResults(AssemblyID) VALUES(@AssemblyID) " +
                                            "SELECT ResultsID FROM AssemblyResults WHERE ResultsID=SCOPE_IDENTITY()";
                        cmd.CommandText = sqlQuery;
                        cmd.CommandType = CommandType.Text;

                        DbParameter dpAssemblyID = provider.CreateParameter();
                        dpAssemblyID.ParameterName = "AssemblyID";
                        dpAssemblyID.Value = assembly.ID;
                        cmd.Parameters.Add(dpAssemblyID);

                        resultsID = (int) cmd.ExecuteScalar();
                    }

                    foreach (AnalyseOptions analyseOption in result.Result.Keys)
                    {
                        using (DbCommand cmd = conn.CreateCommand())
                        {
                            string sqlQuery = "INSERT INTO AnalyzeResults(ResultsID,AnalyseOptionID,Result) " +
                                                "VALUES(@ResultsID,@AnalyseOptionID,@Result)";
                            cmd.CommandText = sqlQuery;
                            cmd.CommandType = CommandType.Text;

                            DbParameter dpResultsID = provider.CreateParameter();
                            dpResultsID.ParameterName = "ResultsID";
                            dpResultsID.Value = resultsID;
                            cmd.Parameters.Add(dpResultsID);

                            DbParameter dpAnalyseOptionID = provider.CreateParameter();
                            dpAnalyseOptionID.ParameterName = "AnalyseOptionID";
                            dpAnalyseOptionID.Value = Convert.ToInt32(analyseOption);
                            cmd.Parameters.Add(dpAnalyseOptionID);

                            DbParameter dpResult = provider.CreateParameter();
                            dpResult.ParameterName = "Result";
                            dpResult.Value = result.Result[analyseOption];
                            cmd.Parameters.Add(dpResult);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    foreach (int degree in result.VertexDegree.Keys)
	                {
                        using (DbCommand cmd = conn.CreateCommand())
                        {
                            string sqlQuery = "INSERT INTO VertexDegree(ResultsID,Degree,Count) " +
                                                "VALUES(@ResultsID,@Degree,@Count)";
                            cmd.CommandText = sqlQuery;
                            cmd.CommandType = CommandType.Text;

                            DbParameter dpResultsID = provider.CreateParameter();
                            dpResultsID.ParameterName = "ResultsID";
                            dpResultsID.Value = resultsID;
                            cmd.Parameters.Add(dpResultsID);

                            DbParameter dpDegree = provider.CreateParameter();
                            dpDegree.ParameterName = "Degree";
                            dpDegree.Value = degree;
                            cmd.Parameters.Add(dpDegree);

                            DbParameter dpCount = provider.CreateParameter();
                            dpCount.ParameterName = "Count";
                            dpCount.Value = result.VertexDegree[degree];
                            cmd.Parameters.Add(dpCount);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    foreach (double coefficient in result.Coefficient.Keys)
                    {
                        using (DbCommand cmd = conn.CreateCommand())
                        {
                            string sqlQuery = "INSERT INTO Coefficients(ResultsID,Coefficient,Count) " +
                                                "VALUES(@ResultsID,@Coefficient,@Count)";
                            cmd.CommandText = sqlQuery;
                            cmd.CommandType = CommandType.Text;

                            DbParameter dpResultsID = provider.CreateParameter();
                            dpResultsID.ParameterName = "ResultsID";
                            dpResultsID.Value = resultsID;
                            cmd.Parameters.Add(dpResultsID);

                            DbParameter dpCoefficient = provider.CreateParameter();
                            dpCoefficient.ParameterName = "Coefficient";
                            dpCoefficient.Value = coefficient;
                            cmd.Parameters.Add(dpCoefficient);

                            DbParameter dpCount = provider.CreateParameter();
                            dpCount.ParameterName = "Count";
                            dpCount.Value = result.Coefficient[coefficient];
                            cmd.Parameters.Add(dpCount);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    foreach (int subgraph in result.Subgraphs.Keys)
                    {
                        using (DbCommand cmd = conn.CreateCommand())
                        {
                            string sqlQuery = "INSERT INTO ConSubgraphs(ResultsID,VX,Count) " +
                                                "VALUES(@ResultsID,@VX,@Count)";
                            cmd.CommandText = sqlQuery;
                            cmd.CommandType = CommandType.Text;

                            DbParameter dpResultsID = provider.CreateParameter();
                            dpResultsID.ParameterName = "ResultsID";
                            dpResultsID.Value = resultsID;
                            cmd.Parameters.Add(dpResultsID);

                            DbParameter dpSub = provider.CreateParameter();
                            dpSub.ParameterName = "VX";
                            dpSub.Value = subgraph;
                            cmd.Parameters.Add(dpSub);

                            DbParameter dpCount = provider.CreateParameter();
                            dpCount.ParameterName = "Count";
                            dpCount.Value = result.Subgraphs[subgraph];
                            cmd.Parameters.Add(dpCount);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    foreach (int dist in result.DistanceBetweenVertices.Keys)
                    {
                        using (DbCommand cmd = conn.CreateCommand())
                        {
                            string sqlQuery = "INSERT INTO VertexDistance(ResultsID,Distance,Count) " +
                                                "VALUES(@ResultsID,@Distance,@Count)";
                            cmd.CommandText = sqlQuery;
                            cmd.CommandType = CommandType.Text;

                            DbParameter dpResultsID = provider.CreateParameter();
                            dpResultsID.ParameterName = "ResultsID";
                            dpResultsID.Value = resultsID;
                            cmd.Parameters.Add(dpResultsID);

                            DbParameter dpSub = provider.CreateParameter();
                            dpSub.ParameterName = "Distance";
                            dpSub.Value = dist;
                            cmd.Parameters.Add(dpSub);

                            DbParameter dpCount = provider.CreateParameter();
                            dpCount.ParameterName = "Count";
                            dpCount.Value = result.DistanceBetweenVertices[dist];
                            cmd.Parameters.Add(dpCount);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    foreach (int dist in result.DistancesBetweenEigenValues.Keys)
                    {
                        using (DbCommand cmd = conn.CreateCommand())
                        {
                            string sqlQuery = "INSERT INTO DistancesBetweenEigenValues(ResultsID,Sub,Count) " +
                                                "VALUES(@ResultsID,@Sub,@Count)";
                            cmd.CommandText = sqlQuery;
                            cmd.CommandType = CommandType.Text;

                            DbParameter dpResultsID = provider.CreateParameter();
                            dpResultsID.ParameterName = "ResultsID";
                            dpResultsID.Value = resultsID;
                            cmd.Parameters.Add(dpResultsID);

                            DbParameter dpSub = provider.CreateParameter();
                            dpSub.ParameterName = "Distance";
                            dpSub.Value = dist;
                            cmd.Parameters.Add(dpSub);

                            DbParameter dpCount = provider.CreateParameter();
                            dpCount.ParameterName = "Count";
                            dpCount.Value = result.DistancesBetweenEigenValues[dist];
                            cmd.Parameters.Add(dpCount);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    // !добавить все остальные резултаты анализа!
                }

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
        }

        public override void Delete(Guid assemblyID)
        {
            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = GetConnectionString();
                conn.Open();
                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "DELETE FROM AnalyzeResults WHERE ResultsID IN (SELECT ResultsID FROM AssemblyResults WHERE AssemblyID=@AssemblyID) " +
                                        "DELETE FROM Coefficients WHERE ResultsID IN (SELECT ResultsID FROM AssemblyResults WHERE AssemblyID=@AssemblyID) " +
                                        "DELETE FROM VertexDegree WHERE ResultsID IN (SELECT ResultsID FROM AssemblyResults WHERE AssemblyID=@AssemblyID) " +
                                        "DELETE FROM ConSubgraphs WHERE ResultsID IN (SELECT ResultsID FROM AssemblyResults WHERE AssemblyID=@AssemblyID) " +
                                        // !добавить все остальные резултаты анализа!
                                        "DELETE FROM AssemblyResults WHERE AssemblyID=@AssemblyID " +
                                        "DELETE FROM GenerationParamValues WHERE AssemblyID=@AssemblyID " +
                                        "DELETE FROM Assemblies WHERE AssemblyID=@AssemblyID";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpAssemblyID = provider.CreateParameter();
                    dpAssemblyID.ParameterName = "AssemblyID";
                    dpAssemblyID.Value = assemblyID;
                    cmd.Parameters.Add(dpAssemblyID);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public override ResultAssembly Load(Guid assemblyID)
        {
            ResultAssembly resultAssembly = new ResultAssembly();
            resultAssembly.ID = assemblyID;
            using (DbConnection conn = provider.CreateConnection())
            {
                conn.ConnectionString = GetConnectionString();
                conn.Open();

                using (DbCommand cmd = conn.CreateCommand())
                {
                    string sqlQuery = "SELECT Assemblies.[Name], Assemblies.ModelID,GenerationParamValues.* FROM Assemblies " +
                                        "LEFT JOIN GenerationParamValues ON GenerationParamValues.AssemblyID=Assemblies.AssemblyID " +
                                        "WHERE Assemblies.AssemblyID=@AssemblyID ORDER BY GenerationParamID";
                    cmd.CommandText = sqlQuery;
                    cmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = "AssemblyID";
                    dpID.Value = assemblyID;
                    cmd.Parameters.Add(dpID);

                    using (DbDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            resultAssembly.ModelType = GetModelType((int)dr["ModelID"]);
                            resultAssembly.Name = (string)dr["Name"];    //Convert.ToDouble(paramNode.Attributes["value"].Value, CultureInfo.InvariantCulture)


                            GenerationParam param = (GenerationParam)Enum.ToObject(typeof(GenerationParam), (int)dr["GenerationParamID"]);

                            GenerationParamInfo paramInfo = (GenerationParamInfo)(param.GetType().GetField(param.ToString()).GetCustomAttributes(typeof(GenerationParamInfo), false)[0]);
                            if (paramInfo.Type.Equals(typeof(Double)))
                            {
                                /*
                                NumberFormatInfo provider = new NumberFormatInfo();
                                provider.NumberDecimalSeparator = ".";
                                */
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
                        }
                    }
                }

                using (DbCommand mainCmd = conn.CreateCommand())
                {
                    string sql = "SELECT ResultsID FROM AssemblyResults WHERE AssemblyID=@AssemblyID";
                    mainCmd.CommandText = sql;
                    mainCmd.CommandType = CommandType.Text;

                    DbParameter dpID = provider.CreateParameter();
                    dpID.ParameterName = "AssemblyID";
                    dpID.Value = assemblyID;
                    mainCmd.Parameters.Add(dpID);

                    List<int> resultIDs = new List<int>();
                    using (DbDataReader mainReader = mainCmd.ExecuteReader())
                    {
                        while (mainReader.Read())
                        {
                            resultIDs.Add((int)mainReader["ResultsID"]);
                        }
                    }

                    foreach (int resultID in resultIDs)
	                {
	                    AnalizeResult result = new AnalizeResult();
                        resultAssembly.Results.Add(result);

                        using (DbCommand cmd = conn.CreateCommand())
                        {
                            string sqlQuery = "SELECT * FROM AnalyzeResults WHERE ResultsID=@ResultsID";
                            cmd.CommandText = sqlQuery;
                            cmd.CommandType = CommandType.Text;

                            DbParameter dpResultsID = provider.CreateParameter();
                            dpResultsID.ParameterName = "ResultsID";
                            dpResultsID.Value = resultID;
                            cmd.Parameters.Add(dpResultsID);

                            using (DbDataReader dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    result.Result.Add((AnalyseOptions)Enum.ToObject(typeof(AnalyseOptions), dr["AnalyseOptionID"]), Convert.ToDouble(dr["Result"]));
                                }
                            }
                        }

                        using (DbCommand cmd = conn.CreateCommand())
                        {
                            string sqlQuery = "SELECT * FROM VertexDegree WHERE ResultsID=@ResultsID";
                            cmd.CommandText = sqlQuery;
                            cmd.CommandType = CommandType.Text;

                            DbParameter dpResultsID = provider.CreateParameter();
                            dpResultsID.ParameterName = "ResultsID";
                            dpResultsID.Value = resultID;
                            cmd.Parameters.Add(dpResultsID);

                            using (DbDataReader dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    result.VertexDegree.Add((int)dr["Degree"], (int)dr["Count"]);
                                }
                            }
                        }

                        using (DbCommand cmd = conn.CreateCommand())
                        {
                            string sqlQuery = "SELECT * FROM Coefficients  WHERE ResultsID=@ResultsID";
                            cmd.CommandText = sqlQuery;
                            cmd.CommandType = CommandType.Text;

                            DbParameter dpResultsID = provider.CreateParameter();
                            dpResultsID.ParameterName = "ResultsID";
                            dpResultsID.Value = resultID;
                            cmd.Parameters.Add(dpResultsID);

                            using (DbDataReader dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    result.Coefficient.Add(Convert.ToDouble(dr["Coefficient"]), (int)dr["Count"]);
                                }
                            }
                        }

                        using (DbCommand cmd = conn.CreateCommand())
                        {
                            string sqlQuery = "SELECT * FROM Subgraphs  WHERE ResultsID=@ResultsID";
                            cmd.CommandText = sqlQuery;
                            cmd.CommandType = CommandType.Text;

                            DbParameter dpResultsID = provider.CreateParameter();
                            dpResultsID.ParameterName = "ResultsID";
                            dpResultsID.Value = resultID;
                            cmd.Parameters.Add(dpResultsID);

                            using (DbDataReader dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    result.Subgraphs.Add(Convert.ToInt32(dr["Sub"]), (int)dr["Count"]);
                                }
                            }
                        }

                        using (DbCommand cmd = conn.CreateCommand())
                        {
                            string sqlQuery = "SELECT * FROM ParisiBitArray  WHERE ResultsID=@ResultsID";
                            cmd.CommandText = sqlQuery;
                            cmd.CommandType = CommandType.Text;

                            DbParameter dpResultsID = provider.CreateParameter();
                            dpResultsID.ParameterName = "ResultsID";
                            dpResultsID.Value = resultID;
                            cmd.Parameters.Add(dpResultsID);

                            List<bool> bits = new List<bool>();
                            using (DbDataReader dr = cmd.ExecuteReader())
                            {
                                while (dr.Read())
                                {
                                    bits.Add(Convert.ToBoolean(dr["Bit"]));
                                }
                            }
                            if (bits.Count != 0)
                            {
                                result.TreeVector = new BitArray(bits.ToArray<bool>());
                            }
                        }
                    }
                }
            }
            return resultAssembly;
        }

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
            }
            return results;
        }


        #endregion
    }
}
