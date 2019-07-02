using System;

public class Uzivatel
{
	private Guid Id { get; set; }
    private string jmeno { get; set; }
    private string prijmeni { get; set; }
    private string email { get; set; }
    private string heslo { get; set; }
    private string? mobil { get; set; }
    private byte[] fotka { get; set; }
}
