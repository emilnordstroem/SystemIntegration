using System;
using System.Collections.Generic;
using System.Text;

public class Person
{
	public Person(string firstname, string lastname, int age, string email)
	{
		Firstname = firstname;
		Lastname = lastname;
		Age = age;
		Email = email;
	}

	public string Firstname { get; set; }
	public string Lastname { get; set; }
	public int Age { get; set; }
	public string Email { get; set; }

	public override string ToString()
	{
		return $"\n{Firstname} {Lastname} ({Age})\nEmail: {Email}";
	}

}
