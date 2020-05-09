using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqConsoleApp
{
    public class LinqSamples
    {
        public static IEnumerable<Emp> Emps { get; set; }
        public static IEnumerable<Dept> Depts { get; set; }

        public LinqSamples()
        {
            LoadData();
        }

        public void LoadData()
        {
            var empsCol = new List<Emp>();
            var deptsCol = new List<Dept>();

            #region Load depts
            var d1 = new Dept
            {
                Deptno = 1,
                Dname = "Research",
                Loc = "Warsaw"
            };

            var d2 = new Dept
            {
                Deptno = 2,
                Dname = "Human Resources",
                Loc = "New York"
            };

            var d3 = new Dept
            {
                Deptno = 3,
                Dname = "IT",
                Loc = "Los Angeles"
            };

            deptsCol.Add(d1);
            deptsCol.Add(d2);
            deptsCol.Add(d3);
            Depts = deptsCol;
            #endregion

            #region Load emps
            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            empsCol.Add(e1);
            empsCol.Add(e2);
            empsCol.Add(e3);
            empsCol.Add(e4);
            empsCol.Add(e5);
            empsCol.Add(e6);
            empsCol.Add(e7);
            empsCol.Add(e8);
            empsCol.Add(e9);
            empsCol.Add(e10);
            Emps = empsCol;

            #endregion

        }


        /*
            Celem ćwiczenia jest uzupełnienie poniższych metod.
         *  Każda metoda powinna zawierać kod C#, który z pomocą LINQ'a będzie realizować
         *  zapytania opisane za pomocą SQL'a.
         *  Rezultat zapytania powinien zostać wyświetlony za pomocą kontrolki DataGrid.
         *  W tym celu końcowy wynik należy rzutować do Listy (metoda ToList()).
         *  Jeśli dane zapytanie zwraca pojedynczy wynik możemy je wyświetlić w kontrolce
         *  TextBox WynikTextBox.
        */

        /// <summary>
        /// SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        public void Przyklad1()
        {
            Console.WriteLine("Zapytanie 1" + "\n");
            //var res = new List<Emp>();
            //foreach(var emp in Emps)
            //{
            //    if (emp.Job == "Backend programmer") res.Add(emp);
            //}

            //1. Query syntax (SQL)
            var res = from emp in Emps
                      where emp.Job == "Backend programmer"
                      select new
                      {
                          Nazwisko = emp.Ename,
                          Zawod = emp.Job
                      };

           
            //2. Lambda and Extension methods

            var res1 = Emps.Where(emp => emp.Job == "Backend programmer")
                 .Select(emp => new
                 {
                     emp.Deptno,
                     emp.Empno,
                     emp.Ename,
                     emp.HireDate,
                     emp.Job,
                     emp.Mgr,
                     emp.Salary,
                 })
                .ToList();

            foreach (var _res in res1)
            {
                Console.WriteLine(_res);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// SELECT * FROM Emps Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
        /// </summary>
        public void Przyklad2()
        {
            Console.WriteLine("Zapytanie 2" + "\n");
            var res2 = Emps.Where(emp => emp.Job == "Frontend programmer" && emp.Salary > 1000)
                           .OrderByDescending(emp => emp.Ename)
                           .Select(emp => new
                           {
                               emp.Deptno,
                               emp.Empno,
                               emp.Ename,
                               emp.HireDate,
                               emp.Job,
                               emp.Mgr,
                               emp.Salary,
                           })
                           .ToList();

            foreach (var res in res2)
            {
                Console.WriteLine(res);
            }

            Console.WriteLine();

        }

        /// <summary>
        /// SELECT MAX(Salary) FROM Emps;
        /// </summary>
        public void Przyklad3()
        {
            Console.WriteLine("Zapytanie 3" + "\n");
            var res3 = Emps.Max(emp => emp.Salary);

      
            Console.WriteLine(res3 + "\n");

        }


        /// <summary>
        /// SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
        /// </summary>
        public void Przyklad4()
        {
            Console.WriteLine("Zapytanie 4" + "\n");
            var max = Emps.Max(emp => emp.Salary);
            var res4 = Emps.Where(emp => emp.Salary == max)
                 .Select(emp => new
                 {
                     emp.Deptno,
                     emp.Empno,
                     emp.Ename,
                     emp.HireDate,
                     emp.Job,
                     emp.Mgr,
                     emp.Salary,
                 })
                .ToList();

            foreach (var res in res4)
            {
                Console.WriteLine(res);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// SELECT ename AS Nazwisko, job AS Praca FROM Emps;
        /// </summary>
        public void Przyklad5()
        {
            Console.WriteLine("Zapytanie 5" + "\n");
            var res5 = Emps.Select(emp => new
            {
                Nazwisko = emp.Ename,
                Praca = emp.Job
            }).ToList();

            foreach (var res in res5)
            {
                Console.WriteLine(res);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
        /// INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
        /// Rezultat: Złączenie kolekcji Emps i Depts.
        /// </summary>
        public void Przyklad6()
        {
            Console.WriteLine("Zapytanie 6" + "\n");
            var res6 = Emps.Join(Depts, emp => emp.Deptno, dept => dept.Deptno, (emp, dept) => new
            {
                Nazwisko = emp.Ename,
                Zawód = emp.Job,
                Nazwa = dept.Dname
            }).ToList();

            foreach (var res in res6)
            {
                Console.WriteLine(res);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
        /// </summary>
        public void Przyklad7()
        {
            Console.WriteLine("Zapytanie 7" + "\n");
            var res7 = Emps.Select(emp => new
            {
                Praca = emp.Job
            }).GroupBy(emp => emp.Praca)
            .Count();

            Console.WriteLine(res7.ToString() + "\n");
        }

        /// <summary>
        /// Zwróć wartość "true" jeśli choć jeden
        /// z elementów kolekcji pracuje jako "Backend programmer".
        /// </summary>
        public void Przyklad8()
        {
            Console.WriteLine("Zapytanie 8" + "\n");
            var res8 = Emps.Any(emp => emp.Job == "Backend programmer");

            Console.WriteLine(res8.ToString() + "\n");

        }

        /// <summary>
        /// SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
        /// ORDER BY HireDate DESC;
        /// </summary>
        public void Przyklad9()
        {
            Console.WriteLine("Zapytanie 9" + "\n");
            var res9 = Emps.Where(emp => emp.Job == "Frontend programmer")
                .OrderByDescending(emp => emp.HireDate)
                .FirstOrDefault();

            Console.WriteLine(res9 + "\n");
        }

        /// <summary>
        /// SELECT Ename, Job, Hiredate FROM Emps
        /// UNION
        /// SELECT "Brak wartości", null, null;
        /// </summary>
        public void Przyklad10Button_Click()
        {
            Console.WriteLine("Zapytanie 10" + "\n");
            var res1 = Emps.Select(emp => new
            {
                Ename = emp.Ename,
                Job = emp.Job,
                HireDate = emp.HireDate
            });

            var res2 = Emps.Select(emp => new
            {
                Ename = "Brak wartości",
                Job = "null",
                HireDate = (DateTime?) null
            });

            var res10 = res1.Union(res2).ToList();

            foreach (var res in res10)
            {
                Console.WriteLine(res);
            }

            Console.WriteLine();
        }

        //Znajdź pracownika z najwyższą pensją wykorzystując metodę Aggregate()
        public void Przyklad11()
        {
            Console.WriteLine("Zapytanie 11" + "\n");
            var res11 = Emps.Aggregate((res, next) => new Emp { Salary = res.Salary > next.Salary ? res.Salary : next.Salary });

            Console.WriteLine(res11.ToString()+ "/n");
        }

        //Z pomocą języka LINQ i metody SelectMany wykonaj złączenie
        //typu CROSS JOIN
        public void Przyklad12()
        {
            Console.WriteLine("Zapytanie 12" + "\n");
            var res12 = Emps.SelectMany(emp => Depts.Select(dept => Tuple.Create(emp, dept)).ToList());

            foreach (var res in res12)
            {
                Console.WriteLine(res);
            }
        }
    }
}