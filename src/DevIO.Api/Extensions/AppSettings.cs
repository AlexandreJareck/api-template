﻿namespace DevIO.Api.Extensions;

public class AppSettings
{
    public string Secret { get; set; }
    public int ExpiresIn { get; set; }
    public string Issuer { get; set; }
    public string ValidIn { get; set; }
}
