This app has just one page. It basically is where they copy and paste some data from a 3rd party eyeball scanning network to start creating a "Diagnostic record" about the person's eyeball. 

Then they rank what is wrong with the person's eyeball if anything. They save that record, and then a fax sends to the original office where the scan was done, so they know what's wrong with their patient's eyeball.

My issue is with how the models are. Some are lookup tables. Some are attributes of the main DiagnosticReport model. But they just don't go well together and the FaxAttempt model uses the Interfax api library and it feels redundant somehow. 