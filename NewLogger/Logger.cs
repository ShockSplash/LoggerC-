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

        }

        public void DebugFormat(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void Error(string message)
        {
            throw new NotImplementedException();
        }

        public void Error(string message, Exception e)
        {
            using (System.IO.StreamWriter sw = System.IO.File.AppendText(this.ExPath))
            {
                sw.Write("\r \nException Logs: ");
                sw.WriteLine($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()}");
                sw.WriteLine($"{message}");
                sw.WriteLine($"{e.Message}");
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

        }

        public void Fatal(string message)
        {
            throw new Exception("Критическая ошибка, приложение не может функционировать");
        }

        public void Fatal(string message, Exception e)
        {
            throw new Exception(message + " " + e.Message);
        }

        public void Info(string message)
        {
            throw new NotImplementedException();
        }

        public void Info(string message, Exception e)
        {
            throw new NotImplementedException();
        }

        public void Info(string message, params object[] args)
        {
            throw new NotImplementedException();
        }

        public void SystemInfo(string message, Dictionary<object, object> properties = null)
        {
            throw new NotImplementedException();
        }

        public void Warning(string message)
        {
            throw new NotImplementedException();
        }

        public void Warning(string message, Exception e)
        {
            throw new NotImplementedException();
        }

        public void WarningUnique(string message)
        {
            throw new NotImplementedException();
        }
    }
}
