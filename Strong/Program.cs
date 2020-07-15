using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strong
{
    /*
     * Strongly signed assemblies
Quelli che Visual Studio produce normalmente sono regular assemblies. 
Se vuoi garantire l'originalità dei tuoi assemblies, ci puoi porre uno strong name che è composto da:
•	Il nome dell'assembly, ovviamente
•	La versione
•	La Culture
•	La tua chiave pubblica (terrai la privata ben custodita)
•	Una firma digitale, che non è altro che un hash del contenuto dell'assembly, crittato con la chiave privata.

Il client lo decritterà con la chiave pubblica, 
ri-eseguirà l'hash e vedrà se i due risultati corrispondono.
Questa è la prova dell'integrità e dell'autore dell'assembly.

        per creare coppia chiavi 
        1da vs
        2sn -k myKey.snk  e poi firmarlo dopo compilazione csc.exe …. /keyfile:"Mia Chiave.snk"

        per vedere chiave pubb + hash :
        sn -Tp System.Data.dll
        oppure
        assembly.GetName().GetPublicKeyToken();
        Per verificare hash
        sn -v System.Data.dll

        Per consentire il delay signing, devi aggiungere questi due attributi nell'assemblyinfo.cs
[assembly:AssemblyKeyFileAttribute("myKey.snk")]
[assembly:AssemblyDelaySignAttribute(true)]

        Probing
Quando il CLR va caricando gli assembly, 
li cerca in cartelle predefinite (bin) e nella GAC. 
Puoi aggiungere altre cartelle mettendo questo nel file config


<?xml version=”1.0” encoding=”utf-8” ?>
<configuration>
	<runtime>
		<assemblyBinding xmlns=”urn:schemas-microsoft-com:asm.v1”>
			<probing privatePath=”MyLibraries”/>
		</assemblyBinding>
	</runtime>
</configuration>

Tieni presente che col probing puoi soltanto indicare percorsi relativi. 
Se vuoi referenziare assemblies fuori dalla root della tua applicazione, 
devi usare codebase ma l'assembly deve essere obbligatoriamente strongly signed.

<?xml version=”1.0” encoding=”utf-8” ?>
<configuration>
	<runtime>
		<assemblyBinding xmlns=”urn:schemas-microsoft-com:asm.v1”>
		<dependentAssembly>
			<codeBase version=”1.0.0.0”
				href= “http://www.mydomain.com/ReferencedAssembly.dll”/>
		</dependentAssembly>
		</assemblyBinding>
	</runtime>
</configuration>

     * 
     */
    class Program
    {
        static void Main(string[] args)
        {
#warning This code is obsolete

#pragma warning disable
#pragma warning restore

#line 200 "OtherFileName"
            int a; // line 200
#line default
            int b; // line 4
#line hidden
            int c; // hidden
            int d; // line 7

#if !DEBUG
		Console.WriteLine("Not debug");
#elif !WINRT
            Console.WriteLine("Not WINRT");
#else
		  Console.WriteLine(" debug");
#endif


            var drives = DriveInfo.GetDrives();
        }
    }
}
