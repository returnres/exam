net framework
tipi
espressioni operatori
flusso
oop
eredit polif
eccezioni
generic coll
delegate eventi
multithread
reflection
compiler
linq
accesso dati
xml

enumerable
enumerator
iterato
comparer
strutture dati collection tuple

thread1 e threa2 condividono virtualmemory heap
ma ognuobno ha suo stack

[threadstatic]
public static int x

oppure
private readonly ThreadLocal<int> x = new ThreadLocal<int>();

//ThreadLocal inizializza sempre dal valore che gli dai
        //per ogni thread _count inizia da 5
        //la differenza con thredstatic è che tlocal inizilizza sempre la variabile
        /*
        Thread-local storage (TLS) is a computer programming method that uses static or global memory local to a thread. 
        All threads of a process share the virtual address space of the process.
        The local variables of a function are unique to each thread that runs the function. 
        However, the static and global variables are shared by all threads in the process. 
        With thread local storage (TLS), you can provide unique data for each thread that the process can access using a global index.
        One thread allocates the index, which can be used by the other threads to retrieve the unique data associated with the index.
        In the .NET Framework version 4, you can use the System.Threading.ThreadLocal<T> class to create thread-local objects.
        */