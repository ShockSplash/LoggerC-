using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace NewLogger
{
    class Logger:ILogger
    {
        private string CurrentDirectory { get; set; }

        private string FileName { get; set; }

        private string ExName { get; set; }

        private string FilePath { get; set; }

        private string ExPath { get; set; }

        private static List<string> WarningCheck = new List<string>();

        private static List<string> ErrorCheck = new List<string>();

        public Logger()
        {
            this.CurrentDirectory = Directory.GetCurrentDirectory();     // Инициализация текущей директории

            this.FileName = "Log.txt";                                   // Имя файла с логами

            CreateDirectory create = new CreateDirectory();              // Создание папки с датой
            string s = create.GetFolderPath();                           // Получение имя папки

            this.FilePath = this.CurrentDirectory                        // Формирование FilePath
                + $"/Logs/{s}/{this.FileName}";

            this.ExName = "Errors.txt";                                  // Имя файла с ошибками

            this.ExPath = this.CurrentDirectory + $"/Logs/{s}/{this.ExName}";
        }

        public void Debug(string message)
        {
            WriteMessage($"DEBUG: {message}", FilePath);
        }

        public void Debug(string message, Exception e)
        {
            WriteMessage($"DEBUG: {message}", e, FilePath);
        }

        public void DebugFormat(string message, params object[] args)
        {
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(this.FilePath))
            {
                sw.Write("\r \nLogs: ");
                sw.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                sw.WriteLine($"{message}");
                foreach (var item in args)
                {
                    sw.Write(args);
                }
                sw.WriteLine("---------------------------------------------------------------------------");
            }
        }

        public void Error(string message)
        {
            WriteMessage($"ERROR: {message}", ExPath);
        }

        public void Error(string message, Exception e)
        {
            WriteMessage($"ERROR: {message}", e, ExPath);
            ErrorCheck.Add($"{message}{e.Message}");
        }

        public void Error(Exception ex)
        {
            WriteMessage("ERROR", ex, ExPath);
        }

        public void ErrorUnique(string message, Exception e)
        {
            FileInfo fileInf = new FileInfo(ExPath);
            if (fileInf.Exists)
            {

                string DatePath = FilePath.Substring((FilePath.Length - 18), 10);
                DateTime dt = DateTime.Now;
                string now = dt.ToShortDateString();

                bool IsHere = false;

                if (now != DatePath)
                {
                    ErrorCheck.Clear();
                }
                foreach (var item in ErrorCheck)
                {
                    if (item == $"{message}{e.Message}")
                    {
                        IsHere = true;
                    }
                }
                if (!IsHere)
                    Error(message,e);
            }
            else
            {
                Error(message,e);
            }
        }

        public void Fatal(string message)
        {
            WriteMessage($"FATAL ERROR: {message}", ExPath);
            throw new ApplicationException("Application is stop!");
        }

        public void Fatal(string message, Exception e)
        {
            WriteMessage($"FATAL ERROR: {message}", e, ExPath);
            throw new Exception(e.Message);
        }

        public void Info(string message)
        {
            WriteMessage($"INFO: {message}", FilePath);
        }

        public void Info(string message, Exception e)
        {
            WriteMessage($"INFO: {message}", e, FilePath);
        }

        public void Info(string message, params object[] args)
        {
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(this.ExPath))
            {
                sw.Write("\r \nInfo Logs: ");
                sw.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                sw.WriteLine($"Exception message: {message}");
                foreach (var item in args)
                {
                    sw.Write(item);
                }
            }
        }

        public void SystemInfo(string message, Dictionary<object, object> properties = null)
        {
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(this.FilePath))
            {
                sw.Write("\r \nSystem Logs: ");
                sw.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                sw.WriteLine($"System message: {message}");
                foreach (KeyValuePair<object, object> keyValue in properties)
                {
                    sw.WriteLine(keyValue.Key + "  " + keyValue.Value);
                }
                sw.WriteLine("---------------------------------------------------------------------------");
            }
        }

        public void Warning(string message)
        {
            WriteMessage($"WARNING: {message}",FilePath);
            WarningCheck.Add(message);
        }

        public void Warning(string message, Exception e)
        {
            WriteMessage($"WARNING: {message}", e, FilePath);
        }

        public void WarningUnique(string message)
        {
            FileInfo fileInf = new FileInfo(FilePath);
            if (fileInf.Exists)
            {

                string DatePath = FilePath.Substring((FilePath.Length - 18), 10);
                DateTime dt = DateTime.Now;
                string now = dt.ToShortDateString();

                bool IsHere = false;

                if (now != DatePath)
                {
                    WarningCheck.Clear();
                }
                foreach (var item in WarningCheck)
                {
                    if (item == message)
                    {
                        IsHere = true;
                    }
                }
                if (!IsHere)
                    Warning(message);
            }
            else
            {
                Warning(message);
            }
                
        }
        public void WriteMessage(string message,string path)
        {
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(path))
            {
                sw.Write("\r \nLogs: ");
                sw.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                sw.WriteLine($"{message}");
                sw.WriteLine("---------------------------------------------------------------------------");
            }
        }
        public void WriteMessage(string message,Exception ex, string path)
        {
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(path))
            {
                sw.Write("\r \nLogs: ");
                sw.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                sw.WriteLine($"{message}");
                sw.WriteLine($"Typ of exeption: {ex.Message}");
                sw.WriteLine("---------------------------------------------------------------------------");
            }
        }
    }

}
