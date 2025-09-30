using SR_DMG.Source.Employ;
using System.ComponentModel;

namespace SR_DMG.Source.UI.Model
{
    public class Progress : INotifyPropertyChanged
    {
        private float _Value;

        private float _Maxmum;

        private string _Title = string.Empty;

        private string _Text = string.Empty;

        private bool _Dorpdown;

        private bool _IsRetry;

        public CancellationTokenSource Canceller = new();

        public float Value
        {
            set => Program.SetField(ref _Value, value, nameof(Value), OnPropertyChanged);
            get => _Value;
        }

        public float Maxmum
        {
            set => Program.SetField(ref _Maxmum, value, nameof(Maxmum), OnPropertyChanged);
            get => _Maxmum;
        }

        public string Title
        {
            set => Program.SetField(ref _Title, value, nameof(Title), OnPropertyChanged);
            get => _Title;
        }

        public string Text
        {
            set => Program.SetField(ref _Text, value, nameof(Text), OnPropertyChanged);
            get => _Text;
        }

        public bool Dorpdown
        {
            set => Program.SetField(ref _Dorpdown, value, nameof(Dorpdown), OnPropertyChanged);
            get => _Dorpdown;
        }

        public bool IsRetry
        {
            set => Program.SetField(ref _IsRetry, value, nameof(IsRetry), OnPropertyChanged);
            get => _IsRetry;
        }

        public void Initialize(string Title, string Text)
        {
            Value = Maxmum = 0;
            this.Title = Title;
            this.Text = Text;
            Dorpdown = true;
            if (Canceller.IsCancellationRequested)
            {
                Canceller.Dispose();
                Canceller = new();
            }
        }

        public void Completion()
        {
            Text = Simple.Tip_Progress_Finished_Text;
            Title = Simple.Tip_Progress_Finished_Title;
        }

        public async Task<bool> Retry()
        {
            IsRetry = false;
            Text = Simple.Tip_Progress_Unfinished;
            while (!IsRetry)
            {
                if (Canceller.IsCancellationRequested) return false;
                await Task.Delay(1000);
            }
            Text = Simple.Tip_Progress_WillRetry;
            await Task.Delay(2000);
            return true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string? PropertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
    }
}