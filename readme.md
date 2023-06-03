A small utility for patching .NET binaries to convert internal and private functions and 
methods to public functions and methods.

Optionally, you may use a search query to convert only functions and methods which contain a 
specific key. Otherwise, this code will attempt to modify everything. 

While this code is effective for off-the-shelf binaries, your mileage may vary with malware 
samples.

Some lines have been commented out which can be enabled in order to traverse extended PE 
fields.

Tested on Debian. To build/rebuild:

```
dotnet new console
dotnet add package dnlib
dotnet build
```

Basic usage:

`dotnetmod.exe <input file> <output file> <search query>`

Remarks on my blog: 
[https://hexagr.blogspot.com/2023/05/parsing-net-binaries-with-dnlib.html](https://hexagr.blogspot.com/2023/05/parsing-net-binaries-with-dnlib.html)

Inspired by [MuffSec](https://muffsec.com/blog/programatically-modifying-net-assemblies/)
