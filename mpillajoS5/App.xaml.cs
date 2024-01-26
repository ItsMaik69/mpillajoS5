namespace mpillajoS5
{
    public partial class App : Application
    {
        public static PersonRepository PersonR { get; set; }
        public App(PersonRepository personRepo)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new VISTAS.VistaDatos());
            PersonR = personRepo;
        }
    }
}
