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
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(this.FilePath))
            {
                sw.Write("\r \nLogs: ");
                sw.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                sw.WriteLine($"{message}");
                sw.WriteLine("---------------------------------------------------------------------------");
            }
        }

        public void Debug(string message, Exception e)
        {
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(this.FilePath))
            {
                sw.Write("\r \nLogs: ");
                sw.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                sw.WriteLine($"{e.Message}");
                sw.WriteLine("---------------------------------------------------------------------------");
            }
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
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(this.ExPath))
            {
                sw.Write("\r \nException Logs: ");
                sw.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                sw.WriteLine($"Exception message: {message}");
                sw.WriteLine($"Type of exception: Error in the application: the calculation operation is terminated, the application continues to work");
                sw.WriteLine("---------------------------------------------------------------------------");
            }
        }

        public void Error(string message, Exception e)
        {
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(this.ExPath))
            {
                sw.Write("\r \nException Logs: ");
                sw.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                sw.WriteLine($"Exception message: {message}");
                sw.WriteLine($"Type of exception: {e.Message}");
                sw.WriteLine("---------------------------------------------------------------------------");
            }
        }

        public void Error(Exception ex)
        {
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(this.ExPath))
            {
                sw.Write("\r \nException Logs: ");
                sw.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                sw.WriteLine($"Расчет приостановлен, приложение продолжает работу");
                sw.WriteLine("---------------------------------------------------------------------------");
            }
        }

        public void ErrorUnique(string message, Exception e)
        {
            FileInfo fileInf = new FileInfo(ExPath);
            if (fileInf.Exists)
            {
                using (StreamReader sr = new StreamReader(ExPath, System.Text.Encoding.Default))
                {
                    string line;
                    bool IsThisMessage = false;
                    bool isThisEx = false;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line == $"Exception message: {message}")
                            IsThisMessage = true;
                        if (line == $"Exception message: {message}")
                            isThisEx = true;
                    }
                    if (!IsThisMessage && !isThisEx)
                    {
                        sr.Close();
                        Error(message, e);
                    }
                    IsThisMessage = false;
                    isThisEx = false;
                }
            }
            else
            {
                Error(message, e);
            }
           
        }

        public void Fatal(string message)
        {
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(this.ExPath))
            {
                sw.Write("\r \nException Logs: ");
                sw.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                sw.WriteLine($"Exception message: {message}");
                sw.WriteLine($"Type of exception: Fatal error! The application has been stopped!");
                sw.WriteLine("---------------------------------------------------------------------------");
                throw new ApplicationException("The application has been stopped!");
            }
        }

        public void Fatal(string message, Exception e)
        {
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(this.ExPath))
            {
                sw.Write("\r \nException Logs: ");
                sw.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                sw.WriteLine($"Exception message: {message}");
                sw.WriteLine($"Type of exception:{e.Message}! The application has been stopped!");
                sw.WriteLine("---------------------------------------------------------------------------");
                throw new ApplicationException("The application has been stopped!");
            }
        }

        public void Info(string message)
        {
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(this.FilePath))
            {
                sw.Write("\r \nInfo Logs: ");
                sw.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                sw.WriteLine($"Info message: {message}");
                sw.WriteLine("---------------------------------------------------------------------------");
            }
        }

        public void Info(string message, Exception e)
        {
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(this.ExPath))
            {
                sw.Write("\r \nInfo Logs: ");
                sw.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                sw.WriteLine($"Info message: {message}");
                sw.WriteLine($"Type of info exception: {e.Message}");
                sw.WriteLine("---------------------------------------------------------------------------");
            }
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
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(this.FilePath))
            {
                sw.Write("\r \nWarning Logs: ");
                sw.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                sw.WriteLine($"Warning message: {message}");
                sw.WriteLine($"Type of warning: There may be potential errors in the calculation!");
                sw.WriteLine("---------------------------------------------------------------------------");
            }
        }

        public void Warning(string message, Exception e)
        {
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(this.FilePath))
            {
                sw.Write("\r \nWarning Logs: ");
                sw.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                sw.WriteLine($"Warning message: {message}");
                sw.WriteLine($"There may be potential errors in the calculation!");
                sw.WriteLine($"Type of warning: {e.Message}");
                sw.WriteLine("---------------------------------------------------------------------------");
            }
        }

        public void WarningUnique(string message)
        {
            FileInfo fileInf = new FileInfo(FilePath);
            if (fileInf.Exists)
            {
                using (StreamReader sr = new StreamReader(FilePath, System.Text.Encoding.Default))
                {
                    string line;
                    bool IsThisMessage = false;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line == $"Warning message: {message}")
                            IsThisMessage = true;
                    }
                    if (!IsThisMessage)
                    {
                        sr.Close();
                        Warning(message);
                    }
                    IsThisMessage = false;
                }
            }
            else
            {
                Warning(message);
            }
                
        }
    }
}
