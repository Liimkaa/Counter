using System.Collections.ObjectModel;
using System.Text.Json;

namespace counter
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<CounterItem> Counters { get; set; } = new ObservableCollection<CounterItem>();

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;

            string savedData = Preferences.Get("MojeLiczniki", "");
            if (!string.IsNullOrEmpty(savedData))
            {
                var lista = JsonSerializer.Deserialize<List<CounterItem>>(savedData);
                if (lista != null)
                {
                    foreach (var item in lista)
                    {
                        Counters.Add(item);
                    }
                }
            }
        }

        private void OnPlusClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var item = (CounterItem)button.CommandParameter;
            item.Value++;
            Zapisz();
        }

        private void OnMinusClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var item = (CounterItem)button.CommandParameter;
            item.Value--;
            Zapisz();
        }

        private async void OnAddCounterClicked(object sender, EventArgs e)
        {
            string nazwa = await DisplayPromptAsync("Nowy licznik", "Wpisz nazwę:");

            if (!string.IsNullOrEmpty(nazwa))
            {
                string wartoscStr = await DisplayPromptAsync("Nowy licznik", "Wpisz wartość początkową:", initialValue: "0", keyboard: Keyboard.Numeric);

                int wartosc = 0;
                int.TryParse(wartoscStr, out wartosc);

                CounterItem nowy = new CounterItem();
                nowy.Name = nazwa;
                nowy.Value = wartosc;

                Counters.Add(nowy);
                Zapisz();
            }
        }

        private void Zapisz()
        {
            string json = JsonSerializer.Serialize(Counters);
            Preferences.Set("MojeLiczniki", json);
        }
    }
}