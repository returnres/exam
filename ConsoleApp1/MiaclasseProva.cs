namespace ConsoleApp1
{
    internal class MiaclasseProva :IMiaclasseProva
    {
        private int nome;
        public string Surname { get; set; }
        public string MioMetodo(int a)
        {
            return "";
        }

        private void MetodoProvato(int a)
        {
            
        }
    }

    internal interface IMiaclasseProva
    {
        string MioMetodo(int a);
    }
}