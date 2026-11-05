using System;
using Microsoft.Maui.Controls;

namespace counter
{
    public class CounterItem : BindableObject
    {
        private string name;
        public string Name
        {
            get => name;
            set { name = value; OnPropertyChanged(); }
        }

        private int value;
        public int Value
        {
            get => value;
            set { this.value = value; OnPropertyChanged(); }
        }
    }
}