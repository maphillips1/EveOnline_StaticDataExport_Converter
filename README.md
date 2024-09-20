# EveOnline_StaticDataExport_Converter
 Converts the Eve online SDE into both JSON and SQLite formats

There are two steps to the conversion. 

1.) Main.py converts yaml to JSON
2.) C# main.cs - converts json to SQLite. 
	- This uses Newtonsoft.JSon to convert the json files to objects
	- Utilizes a couple of custom converters to handle the yaml formatting
		- converts some "property names" to the type id's for the object
	- outputs the SQLite to the bin area. This area is ignored and not stored in the repository. 
		- I will use the release functionality for publishing the conversions. 

