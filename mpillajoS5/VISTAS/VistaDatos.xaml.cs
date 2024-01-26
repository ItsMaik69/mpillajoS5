using mpillajoS5.Modelos;
using System.Xml.Linq;

namespace mpillajoS5.VISTAS;

public partial class VistaDatos : ContentPage
{
    private Persona selectedPerson;
    private bool isEditing;

    public VistaDatos()
    {
        InitializeComponent();
    }

    private void btnAgregar_Clicked(object sender, EventArgs e)
    {
        StatusMessage.Text = "";
        App.PersonR.AddNewPerson(txtName.Text);
        StatusMessage.Text = App.PersonR.StatusMessague;

    }

    private void btnMostrar_Clicked(object sender, EventArgs e)
    {
        StatusMessage.Text = "";
        List<Persona> people = App.PersonR.GetAllPeople();
        ListaPersona.ItemsSource = people;
    }

    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var person = (Persona)button.BindingContext;

        bool answer = await DisplayAlert("Confirmación", $"¿Estás seguro de que deseas eliminar a {person.Name}?", "Sí", "No");

        if (answer)
        {
            App.PersonR.DeletePerson(person.Id);
            btnMostrar_Clicked(sender, e); // Actualizar la lista después de eliminar la persona
        }
    }
    private void btnEditar_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        selectedPerson = (Persona)button.BindingContext;
        txtName.Text = selectedPerson.Name;
    }
    private async void btnGuardar_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
        var person = (Persona)button.BindingContext;
        if (selectedPerson != null && !string.IsNullOrEmpty(txtName.Text))
        {
            string newName = txtName.Text;
            App.PersonR.UpdatePerson(selectedPerson.Id, newName);
            StatusMessage.Text = App.PersonR.StatusMessague;
            txtName.Text = string.Empty;

            await DisplayAlert("Alerta", "Acaba de recibir una modificación", "Aceptar");
            btnMostrar_Clicked(sender, e); // Actualizar la lista después de guardar los cambios
        }
    }

}