using System;
using System.ComponentModel;

namespace Hell.Model
{
    public class FinishedProduction : INotifyPropertyChanged
    {
        private string _название;
        private int _количество;
        private DateTime _датаПроизводства;
        private DateTime _срокГодности;
        private string _тип;
        private string _местоположение;

        public string Название
        {
            get => _название;
            set
            {
                _название = value;
                OnPropertyChanged(nameof(Название));
            }
        }

        public int Количество
        {
            get => _количество;
            set
            {
                _количество = value;
                OnPropertyChanged(nameof(Количество));
            }
        }

        public DateTime ДатаПроизводства
        {
            get => _датаПроизводства;
            set
            {
                _датаПроизводства = value;
                OnPropertyChanged(nameof(ДатаПроизводства));
            }
        }

        public DateTime СрокГодности
        {
            get => _срокГодности;
            set
            {
                _срокГодности = value;
                OnPropertyChanged(nameof(СрокГодности));
            }
        }

        public string Тип
        {
            get => _тип;
            set
            {
                _тип = value;
                OnPropertyChanged(nameof(Тип));
            }
        }

        public string Местоположение
        {
            get => _местоположение;
            set
            {
                _местоположение = value;
                OnPropertyChanged(nameof(Местоположение));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
