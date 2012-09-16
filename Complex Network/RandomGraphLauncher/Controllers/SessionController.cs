using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RandomGraph.Core.Events;
using RandomGraph.Core.Manager.Status;
using RandomGraph.Common.Model;
using RandomGraph.Common.Model.Generation;
using RandomGraph.Settings;
using CommonLibrary.Model.Events;
using CommonLibrary.Model.Attributes;
using AnalyzerFramework.Manager.ModelRepo;
using log4net;

namespace RandomGraphLauncher.Controllers
{
    // Статический класс, который организует работу сессии job-ов.
    static class SessionController
    {
        // Организация работы с лог файлом.
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(SessionController));

        // Dictionary имен доступных моделей и их описаний.
        public static Dictionary<string, Type> models;
        // Dictionary имен job-ов и их контроллеров.
        private static Dictionary<string, JobController> session = new Dictionary<string, JobController>();

        // Возвращает массив имен доступных моделей.
        public static object[] GetAvailableModelNames()
        {
            models = new Dictionary<string, Type>();
            List<Type> availableModelFactoryTypes = ModelRepository.GetInstance().GetAvailableModelTypes();
            foreach (Type modelType in availableModelFactoryTypes)
            {
                string modelName = modelType.Name;
                models.Add(modelName, modelType);
            }

            return models.Keys.ToArray();
        }

        // Проверяет уникальность данной имени job-а. 
        // Возвращает true, если уникальность неудовлетворена.
        public static bool CheckJobName(string jobName)
        {
            foreach (var result in Options.StorageManager.LoadAllAssemblies())
            {
                if (result.Name == jobName)
                {
                    return true;
                }
            }

            Dictionary<string, JobController>.KeyCollection names = session.Keys;
            foreach (string name in names)
            {
                if (name == jobName)
                {
                    return true;
                }
            }

            return false;
        }

        // Возвращает true, если в сессии есть job-ы, которые еще не закончили работу.
        public static bool ExistsAnyRunningJob()
        {
            Dictionary<string, JobController>.KeyCollection keys = session.Keys;
            foreach (string k in keys)
            {
                if (session[k].Finished == true)
                    return true;
            }

            return false;
        }

        // Регистрация нового job-а в сессию.
        public static void AddJobToSession(string modelName, string jobName)
        {
            log.Info("Adding a new job to the session.");
            session.Add(jobName, new JobController(models[modelName], jobName));
        }

        // Удаление job-а из сесии.
        public static void RemoveJobFromSession(string jobName)
        {
            log.Info("Removing a job from the session.");
            session.Remove(jobName);
        }

        // ??
        public static void JobFinished(string jobName)
        {
            session[jobName].Finished = false;
        }

        // Возвращает GraphModel job-а в сессии с данным именем.
        public static GraphModel GetGraphModel(string jobName)
        {
            return (GraphModel)(session[jobName].ModelType.GetCustomAttributes(typeof(GraphModel), false)[0]);
        }

        // Возвращает список нужных параметров генерации для job-а с данным именем.
        public static List<RequiredGenerationParam> GetRequiredGenParams(string jobName)
        {
            log.Info("Getting required generation parameters for a job in the session.");
            List<RequiredGenerationParam> res = new List<RequiredGenerationParam>((RequiredGenerationParam[])
                session[jobName].ModelType.GetCustomAttributes(typeof(RequiredGenerationParam), false));
            res.Sort(delegate(RequiredGenerationParam arg1, RequiredGenerationParam arg2)
            {
                return arg1.Index.CompareTo(arg2.Index);
            });

            return res;
        }

        // Возвращает доступные свойства анализа для job-а в сессии с данным именем.
        public static AnalyseOptions GetAvailableAnalyzeOptions(string jobName)
        {
            log.Info("Getting available analyze options for a job in the session.");
            AvailableAnalyzeOptions[] optionsAttributes = (AvailableAnalyzeOptions[])session[jobName].ModelType.
                GetCustomAttributes(typeof(AvailableAnalyzeOptions), false);
            return optionsAttributes[0].Options;
        }

        // Передача значений параметров генерации для job-а в сессии с данным именем.
        public static void SetGenParamValuesForJob(string jobName, Dictionary<GenerationParam, object> values)
        {
            log.Info("Setting generation parameter values for a job in the session.");
            session[jobName].GenParamValues = values;
        }

        // Передача имени файла (статическая генерация) для job-а в сессии с данным именем.
        public static void SetFilePath(string jobName, string path)
        {
            log.Info("Setting file path for a job in the session.");
            session[jobName].FilePath = path;
        }

        // Передача выбранных свойств анализа для job-а в сессии с данным именем.
        public static void SetSelectedOptions(string jobName, AnalyseOptions opt)
        {
            log.Info("Setting selected analyze options for a job in the session.");
            session[jobName].SelectedOptions = opt;
        }

        // Передача числа реализаций для job-а в сессии с данным именем.
        public static void SetInstanceCount(string jobName, int count)
        {
            log.Info("Setting realization count for a job in the session.");
            session[jobName].InstanceCount = count;
        }

        // Передача значений некоторых свойств анализа для job-а в сессии с данным именем.
        public static void SetAnalyzeOptionValue(string jobName, string optName, object value)
        {
            log.Info("Setting analyze option values for a job in the session.");
            session[jobName].AnalyzeOptionValues[optName] = value;
        }

        // Проверка на правильность значений параметров генерации для job-а в сессии с данным именем.
        public static bool CheckParameters(string jobName)
        {
            log.Info("Checking generation parameter values for a job in the session.");
            return session[jobName].CheckParameters();
        }

        // Возвращает текст ошибки для job-а в сессии с данным именем.
        public static string GetErrorMessage(string jobName)
        {
            log.Info("Getting error message for a job in the session.");
            return session[jobName].ErrorMessage;
        }

        // Возвращает число уже полученных результатов для job-а в сессии с данным именем.
        public static int GetResultsCount(string jobName)
        {
            log.Info("Getting finished results count for a job in the session.");
            return session[jobName].ResultsCount;
        }

        // Остановка работы job-а в сессии с данным именем.
        public static void StopJob(string jobName)
        {
            log.Info("Stop a job in the session.");
            session[jobName].Manager.Stop();
        }

        // Остановка работы данной реализации job-а в сессии с данным именем.
        public static void StopJob(string jobName, int index)
        {
            log.Info("Stop a realization of job in the session.");
            session[jobName].Manager.Stop(index);
        }

        // Пауза работы job-а в сессии с данным именем.
        public static void PauseJob(string jobName)
        {
            log.Info("Pause a job in the session.");
            session[jobName].Manager.Pause();
        }

        // Пауза работы данной реализации job-а в сессии с данным именем.
        public static void PauseJob(string jobName, int index)
        {
            log.Info("Pause a realization of job in the session.");
            session[jobName].Manager.Pause(index);
        }

        // Продолжение работы job-а в сессии с данным именем.
        public static void ContinueJob(string jobName)
        {
            log.Info("Continue a job in the session.");
            session[jobName].Manager.Continue();
        }

        // Продолжение работы данной реализации job-а в сессии с данным именем.
        public static void ContinueJob(string jobName, int index)
        {
            log.Info("Continue a realization of job in the session.");
            session[jobName].Manager.Continue(index);
        }

        // Передача статуса выполнения для job-а в сессии с данным именем.
        public static void SetStatusChangedEventHandler(string jobName, 
            StatusChangedEventHandler manager_ExecutionStatusChange)
        {
            log.Info("Setting execution status for a job in the session.");
            session[jobName].SetStatusChangedEventHandler(manager_ExecutionStatusChange);
        }

        // Передача статуса развития для job-а в сессии с данным именем.
        public static void SetGraphProgressEventHandler(string jobName, 
            GraphProgressEventHandler manager_GraphProgressEventHandler)
        {
            log.Info("Setting progress status for a job in the session.");
            session[jobName].SetGraphProgressEventHandler(manager_GraphProgressEventHandler);
        }

        // Передача статуса "сгенерировано" для job-а в сессии с данным именем.
        public static void SetGraphsGeneratedEventHandler(string jobName, 
            GraphsAreGenerated manager_GraphsGenerated)
        {
            log.Info("Setting generated status for a job in the session.");
            session[jobName].SetGraphsGeneratedEventHandler(manager_GraphsGenerated);
        }

        // Сохранение результатов для job-а в сессии с данным именем.
        public static void SaveResults(string jobName)
        {
            log.Info("Saving results for a job in the session.");
            session[jobName].Save();
        }

        // ??
        public static Dictionary<GenerationParam, object> GetGenParamValues(string jobName)
        {
            return session[jobName].GenParamValues;
        }

        // ??
        public static AnalyseOptions GetSelectedAnalyzeOptions(string jobName)
        {
            return session[jobName].SelectedOptions;
        }

        // ??
        public static Dictionary<String, Object> GetAnalyzeOptionsValues(string jobName)
        {
            return session[jobName].AnalyzeOptionValues;
        }

        // ??
        public static void StartGraphModel(string jobName, object[] invokeParams)
        {
            session[jobName].Start(invokeParams);
        }
    }
}

/*
public void InitFlashApi(AxShockwaveFlashObjects.AxShockwaveFlash proxy, ExternalInterfaceCallEventHandler proxy_ExternalInterfaceCall)
{
    this.proxy = new ExternalInterfaceProxy(proxy);
    this.proxy.ExternalInterfaceCall += new ExternalInterfaceCallEventHandler(proxy_ExternalInterfaceCall);
}

public void CallFlash(String jsonString)
{
    this.proxy.Call("sendToActionScript", jsonString);
}
 */
