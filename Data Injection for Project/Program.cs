using Npgsql;
using System.Threading;
using System.Diagnostics;

// === SEQUENTIAL DATA INJECTION ===

// MelbourneSydney inj1 = new MelbourneSydney();
// BrisbaneMelbourne inj2 = new BrisbaneMelbourne();
// DarwinPerth inj3 = new DarwinPerth();
// AdelaideCanberra inj4 = new AdelaideCanberra();
// inj1.inject();
// inj2.inject();
// inj3.inject();
// inj4.inject();

// // === PARALLEL AND REAL-TIME DATA INJECTION ===

Thread thread1 = new Thread(new MelbourneSydneyRealTime().inject);
Thread thread2 = new Thread(new BrisbaneMelbourneRealTime().inject);
Thread thread3 = new Thread(new DarwinPerthRealTime().inject);
Thread thread4 = new Thread(new AdelaideCanberraRealTime().inject);

thread1.Start();
thread2.Start();
thread3.Start();
thread4.Start();

// === MODIFIED CODE FRO EVALUATION ===

// DateTime start = DateTime.Now;

// MelbourneSydney inj1 = new MelbourneSydney();
// BrisbaneMelbourne inj2 = new BrisbaneMelbourne();
// DarwinPerth inj3 = new DarwinPerth();
// AdelaideCanberra inj4 = new AdelaideCanberra();
// inj1.inject(); inj2.inject(); inj3.inject(); inj4.inject();

// DateTime end = DateTime.Now;

// TimeSpan ts = (end - start);

// Console.WriteLine("Time taken to inject data without threads is {0} ms", ts.TotalMilliseconds);

// DateTime start1 = DateTime.Now;

// Thread thread1 = new Thread(new MelbourneSydney().inject);
// Thread thread2 = new Thread(new BrisbaneMelbourne().inject);
// Thread thread3 = new Thread(new DarwinPerth().inject);
// Thread thread4 = new Thread(new AdelaideCanberra().inject);

// thread1.Start(); thread2.Start(); thread3.Start(); thread4.Start();

// thread1.Join(); thread2.Join(); thread3.Join(); thread4.Join();

// DateTime end1 = DateTime.Now;

// TimeSpan ts1 = (end1 - start1);

// Console.WriteLine("Time taken to inject data with threads is {0} ms",  ts1.TotalMilliseconds);
