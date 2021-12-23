# AzRanger

The initial idea of the tool was to learn about Azure APIs and what possibilites exist to get different data from it. But then I thought okay, now you have a lot of data, what to do with it? So I added code to verify these information and settings agains different recommendations. It is not fully featured or reliable yet, but I will include more checks over time. I hope you find it usefull too. 

The tool is a learning project for me so the results may be incorrekt or the tool may encouteres some errors. So please provide feedback on any issues, recommendations or additional checks.

## Acknowledgement

Most of the APIs, are already included in other tool, so most of the credits goes to:

* [Dr. Nestori Syynimaa](https://twitter.com/DrAzureAD)
* [Przemysław Kłys](https://twitter.com/PrzemyslawKlys)
* [Dirk-Jan](https://twitter.com/_dirkjan)

Thank you for your work!

## Usage

```
  -u, --username    Specify the Username.
  -p, --password    Specifiy the password.
  --proxy           Specifiy a proxy.
  --debug           Enable verbose logging.
  --logfile         Set the logfile path.
  --audit           Perform an audit against your tenant.
  --dumpall         Dump all information the tools gather into JSON.
  --dumpsettings    Dump all the tenant settings the tool gathers into JSON.
  --outfile         File to write.
  --help            Display this help screen.
  --version         Display version information.
```

When you run it without --username and --password, then an interactive logon will be performed.

## Prerequisites

The user should have the Role "Global Reader" assigned.