using System.Collections.Concurrent;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace HealthSup.Infrastructure.Data.Scripts
{
    public static class ScriptManager
    {
        public static class FileNames
        {
            public static class Doctor
            {
                static readonly string EntityName = $"{nameof(Doctor)}";
            }

            public static class HealthSupAgent
            { 
                static readonly string EntityName = $"{nameof(HealthSupAgent)}";

                public static readonly string GetByNameAndPassword = $"{EntityName}.GetByNameAndPassword";
            }

            public static class UserDTO
            {
                static readonly string EntityName = $"{nameof(UserDTO)}";

                public static readonly string GetByEmailAndPassword = $"{EntityName}.GetByEmailAndPassword";
                public static readonly string UpdatePassword = $"{EntityName}.UpdatePassword";
            }

            public static class Question 
            {
                static readonly string EntityName = $"{nameof(Question)}";

                public static readonly string GetInitialByDiseaseId = $"{EntityName}.GetInitialByDiseaseId";
            }

            public static class PossibleAnswer
            {
                static readonly string EntityName = $"{nameof(PossibleAnswer)}";

                public static readonly string ListByQuestionId = $"{EntityName}.ListByQuestionId";
            }
        }

        private static Assembly _thisAssemnbly = Assembly.GetExecutingAssembly();
        private static string _rootNamespace = typeof(ScriptManager).Namespace;

        private static ConcurrentDictionary<string, string> _scriptsSql = new ConcurrentDictionary<string, string>();

        public static string GetByName(string scriptName)
        {
            string scriptContent;

            if (_scriptsSql.TryGetValue(scriptName, out scriptContent))
            {
                return scriptContent;
            }

            scriptContent = LoadFromResources(scriptName);
            scriptContent = CleanQuery(scriptContent);

            return scriptContent;
        }

        private static string LoadFromResources(string scriptName)
        {
            string fileContent;

            var resourceStream = _thisAssemnbly.GetManifestResourceStream($"{_rootNamespace}.{scriptName}.sql");
            using (var reader = new StreamReader(resourceStream))
            {
                fileContent = reader.ReadToEndAsync().Result;
            }

            return fileContent;
        }

        private static string CleanQuery(string query)
        {
            // Removing ALL comments
            query = Regex.Replace(query, @"--(.*)", "", RegexOptions.Multiline);

            // Removing ALL whitespaces (except those inside quotes that must be preserved)
            query = Regex.Replace(query, @"(?<=^([^']| '[^']*')*)\s+", " ");

            return query;
        }
    }
}
