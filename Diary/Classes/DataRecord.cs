using System;

namespace Diary
{
    public struct DataRecord
    {
        public string recordText;
        public DateTime dateTime;
        public DayEvaluation evaluation;


        public DataRecord(DateTime dateTime)
        {
            this.dateTime = dateTime;
            this.recordText = string.Empty;
            this.evaluation = DayEvaluation.NONE;
        }
    }
}
