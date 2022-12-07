using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileHelpers;
namespace TimeClock.Services
{
    public class Logging : ILogging
    {
        private string logLocation = @"C:\TimeClock\Log.csv";
        public IEnumerable<StatusChangeResult> ReadHistory(int userId)
        {
            List<StatusChangeResult> history = new List<StatusChangeResult>();
            var engine = new FileHelperEngine<StatusChangeResult>();

            if (File.Exists(logLocation))
            {
                if (userId > 0)
                {

                    history = engine.ReadFileAsList(logLocation).Where(x => x.Id == userId).ToList();

                }
                else
                {
                    history = engine.ReadFileAsList(logLocation).ToList();
                }
            }

            return history;
        }
        public void WriteHistoryRecord(StatusChangeResult output)
        {
            if (!File.Exists(logLocation))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(logLocation));
                File.Create(logLocation).Close();
            }
            var engine = new FileHelperEngine<StatusChangeResult>();
            engine.AppendToFile(logLocation, new List<StatusChangeResult>() { output });
        }
    }
}
