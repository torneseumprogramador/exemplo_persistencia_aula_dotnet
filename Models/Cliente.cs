namespace console_app.Models;

public class Cliente
{
    public Cliente()
    {
        this.id = Guid.NewGuid().ToString();
    }

    public Cliente(string _id)
    {
        this.id = _id;
    }

    private string id;
    public string Id
    { 
        get
        {
            return this.id;
        }
    }

    public string Nome { get; set; }
    public string Telefone { get; set; }
}