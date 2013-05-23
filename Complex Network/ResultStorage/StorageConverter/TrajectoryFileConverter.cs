using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Numerics;

using RandomGraph.Common.Model.Generation;
using RandomGraph.Common.Model.Result;
using RandomGraph.Common.Model;
using CommonLibrary.Model.Result;
using Model.ERModel;

namespace ResultStorage.StorageConverter
{
    public class TrajectoryFileConverter: AbstractStorageConverter
    {
        private TrajectoryDictionaryStructure dictStruct = new TrajectoryDictionaryStructure();

        public TrajectoryFileConverter(string path)
            : base(path) { }

        public override void ReadRootDirectory()
        {
            DirectoryInfo parentDir = new DirectoryInfo(this.rootPath);
            foreach (DirectoryInfo dir in parentDir.GetDirectories())
            {
                dictStruct.ReadDirectory(dir.FullName);
                this.assembliesToConvert.Add(CreateAssembly());
            }
        }

        private ResultAssembly CreateAssembly()
        {
            ResultAssembly result = new ResultAssembly();

            result.Name = dictStruct.fileName;
            result.ModelType = typeof(ERModel);
            result.ModelName = result.ModelType.Name;
            result.Size = dictStruct.N;

            result.GenerationParams.Add(GenerationParam.Vertices, dictStruct.N);
            result.GenerationParams.Add(GenerationParam.P, dictStruct.p);
            result.GenerationParams.Add(GenerationParam.Permanent, dictStruct.perm);

            result.AnalyzeOptionParams[AnalyzeOptionParam.TrajectoryMu] = (double)dictStruct.mu;

            foreach (SortedDictionary<int, double> t in dictStruct.dictionaries)
            {
                AnalizeResult r = new AnalizeResult();

                r.TriangleTrajectory = t;
                result.AnalyzeOptionParams[AnalyzeOptionParam.TrajectoryStepCount] = 
                    (BigInteger)t.Count;

                result.Results.Add(r);
            }

            return result;
        }
    }
}
