using System;
using System.Windows;
using System.Windows.Controls;

namespace Diary
{
    public partial class MainWindow : Window
    {
        private DiaryManager diaryManager;

        private bool elementsInteractable = true;
        private DataRecord tempRecord;
        private RadioButton[] evaluationRadioButtons;


        public MainWindow()
        {
            InitializeComponent();

            evaluationRadioButtons = new RadioButton[] { MoodVariant_1, MoodVariant_2, MoodVariant_3, MoodVariant_4, MoodVariant_5 };
            SetUIElementsInteractable(false);

            diaryManager = new DiaryManager(DataConsts.SAVED_DATA_FILE_PATH);
        }


        private void OnCalendarDayClick(object sender, SelectionChangedEventArgs e)
        {
            ResetEvaluationRadioButtons();

            DateTime? selectedDate = Calendar.SelectedDate;
            if (selectedDate == null)
                return;

            SetUIElementsInteractable(true);

            tempRecord = diaryManager.GetDataRecord(selectedDate.Value);

            NotesInputZone.Text = tempRecord.recordText;
            SetEvaluationRadioButtonChecked(tempRecord.evaluation);
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            DayEvaluation evaluation = GetEvaluationRadioButtonChecked();
            if (evaluation == DayEvaluation.NONE)
            {
                MessageBox.Show(DataConsts.EVALUATION_MESSAGE);
                return;
            }

            tempRecord.evaluation = evaluation;
            tempRecord.recordText = NotesInputZone.Text;

            diaryManager.SaveDataRecord(tempRecord);
        }


        private void ResetEvaluationRadioButtons()
        {
            foreach (RadioButton button in evaluationRadioButtons)
                button.IsChecked = false;
        }

        private void SetUIElementsInteractable(bool isEnabled)
        {
            if (elementsInteractable == isEnabled)
                return;

            NotesInputZone.IsReadOnly = !isEnabled;
            SaveButton.IsEnabled = isEnabled;

            foreach (RadioButton button in evaluationRadioButtons)
                button.IsEnabled = isEnabled;

            elementsInteractable = isEnabled;
        }

        private void SetEvaluationRadioButtonChecked(DayEvaluation dayEvaluation)
        {
            switch (dayEvaluation)
            {
                case DayEvaluation.EXCELLENT:
                    MoodVariant_1.IsChecked = true;
                    break;
                case DayEvaluation.GOOD:
                    MoodVariant_2.IsChecked = true;
                    break;
                case DayEvaluation.NORMAL:
                    MoodVariant_3.IsChecked = true;
                    break;
                case DayEvaluation.BAD:
                    MoodVariant_4.IsChecked = true;
                    break;
                case DayEvaluation.HORRIBLE:
                    MoodVariant_5.IsChecked = true;
                    break;
                case DayEvaluation.NONE:
                    break;
            }
        }

        private DayEvaluation GetEvaluationRadioButtonChecked()
        {
            if (MoodVariant_1.IsChecked == true)
                return DayEvaluation.EXCELLENT;

            if (MoodVariant_2.IsChecked == true)
                return DayEvaluation.GOOD;

            if (MoodVariant_3.IsChecked == true)
                return DayEvaluation.NORMAL;

            if (MoodVariant_4.IsChecked == true)
                return DayEvaluation.BAD;

            if (MoodVariant_5.IsChecked == true)
                return DayEvaluation.HORRIBLE;

            return DayEvaluation.NONE;
        }
    }
}
