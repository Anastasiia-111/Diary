using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Diary
{
    public class DiaryManager
    {
        private string savedDataFilePath = string.Empty;
        private Dictionary<DateTime, DataRecord> diaryRecords;


        public DiaryManager(string savedDataFilePath)
        {
            this.savedDataFilePath = savedDataFilePath;

            if (!File.Exists(savedDataFilePath))
            {
                diaryRecords = new Dictionary<DateTime, DataRecord>();
                return;
            }

            string diaryData = File.ReadAllText(savedDataFilePath);
            Dictionary<DateTime, DataRecord>? deserializedData = JsonConvert.DeserializeObject<Dictionary<DateTime, DataRecord>>(diaryData);

            if (deserializedData == null)
            {
                diaryRecords = new Dictionary<DateTime, DataRecord>();
                return;
            }

            diaryRecords = deserializedData;
        }

        public DataRecord GetDataRecord(DateTime selectedDate)
        {
            foreach (DateTime dateKey in diaryRecords.Keys)
                if (dateKey == selectedDate)
                    return diaryRecords[dateKey];

            return new DataRecord(selectedDate);
        }

        public void SaveDataRecord(DataRecord record)
        {
            if (diaryRecords.ContainsKey(record.dateTime))
                diaryRecords[record.dateTime] = record;
            else
                diaryRecords.Add(record.dateTime, record);

            string serializedData = JsonConvert.SerializeObject(diaryRecords, Formatting.Indented);
            File.WriteAllText(savedDataFilePath, serializedData);
        }
    }
}
