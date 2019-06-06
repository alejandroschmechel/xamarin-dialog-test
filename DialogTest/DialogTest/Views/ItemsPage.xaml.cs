using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DialogTest.Models;
using DialogTest.Views;
using DialogTest.ViewModels;

namespace DialogTest.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new ItemsViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            //ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new NewItemPage()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }

        async void Button_Clicked(object sender, EventArgs e)
        {
            string action = await DisplayActionSheet("Selecione uma ação!", "Cancel", null, "Tirar Foto", "Anexar Foto da Galeria");
            if (action != null) { 
                acao.Text = action;
                string action2 = await DisplayActionSheet("Selecione um tipo de documento!", "Cancel", null, "Termo Bacen", "Documento de Identificação", "Comprovante de Endereço", "Contrato Social", "Procuração", "Informe de Rendimentos", "Balanço Patrimonial");
                if (action2 != null)
                {
                    tipoDocumento.Text = action2;
                }
            }
        }
    }
}