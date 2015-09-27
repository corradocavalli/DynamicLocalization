using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Resources.Core;

namespace DynamicLanguage
{
	public class MyViewModel : INotifyPropertyChanged, ICommand
	{
		private ResourceContext resoureContext;
		private string language;

		public MyViewModel(string language)
		{
			this.UpdateCulture(language);

		}

		public string Foo => "ciao";

		public void UpdateCulture(string language)
		{
			this.resoureContext = ResourceContext.GetForCurrentView();
			this.resoureContext.Languages = new List<string> { language };
			this.language = language;
			this.OnPropertyChanged("Item[]");
		}

		public string this[string key] => this.GetResource(key);

		public string GetResource(string stringResource)
		{
			try
			{
				var resourceStringMap = ResourceManager.Current.MainResourceMap.GetSubtree("Resources");
				return resourceStringMap.GetValue(stringResource, this.resoureContext).ValueAsString;
			}
			catch
			{
				return $"?{stringResource}?";
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public void Execute(object parameter)
		{
			string newLanguage = this.language == "it-IT" ? "en-US" : "it-IT";
			this.UpdateCulture(newLanguage);

		}

		public event EventHandler CanExecuteChanged;
	}
}
