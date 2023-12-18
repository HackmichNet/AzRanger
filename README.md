# AzRanger

The initial idea of the tool was to learn about Azure APIs and what possibilities exist to get different data from it. But then I thought okay, now you have a lot of data, what to do with it? So I added code to verify these information and settings agains different recommendations. It is not fully featured or reliable yet, but I will include more checks over time. I hope you find it useful too. 

The tool is a learning project for me so the results may be incorrect or the tool may encounters some errors. So please provide feedback on any issues, recommendations or additional checks.

## Acknowledgement

Most of the APIs, are already included in other tool, so most of the credits goes to:

* [Dr. Nestori Syynimaa](https://twitter.com/DrAzureAD)
* [Przemysław Kłys](https://twitter.com/PrzemyslawKlys)
* [Dirk-Jan](https://twitter.com/_dirkjan)

Thank you for your work!

## Usage

```
  -u, --username       Specify the username.
  -p, --password       Specifiy the password.
  -c, --clientid       Specify the client id.
  -s, --secret         Specify the client secret.
  -t, --tenant         Specify a tenant.
  --proxy              Specify a proxy.
  --debug              Enable verbose logging.
  --logfile            Set the logfile path.
  --outpath            Path to write results.
  --writeallresults    Write all results to console. Can result in a very large
                       output.
  --output             (Default: HTML) Only for audit. Specify 'console', 'html'
                       or 'json'.
  --scope              Set ScopeEnum AAD, Teams, SharePoint(SPO),
                       ExchangeOnline(EXO) or Azure. If not set all scopes will
                       be used.
  --batch              (Default: false) Batch mode. Use for automatic runs.
  --mode               (Default: Audit) AzRager mode.
  --help               Display this help screen.
  --version            Display version information.
```

When you run it without --username and --password, then an interactive login will be performed.

## Prerequisites

The user should have the Role "Global Reader" or "Global Admin" assigned.
