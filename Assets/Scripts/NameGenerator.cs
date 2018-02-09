using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NameGenerator {

    private static string[] names = {
            "Michael",
            "Dwight",
            "Jim",
            "Pam",
            "Ryan",
            "Andy",
            "Robert",
            "Janice",
            "Joyce",
            "Meredith",
            "Oscar",
            "Darryl",
            "Creed",
            "Nellie",
            "Clark",
            "Jo",
            "David",
            "Gareth",
            "Tim",
            "Dawn",
            "Keith",
            "Chris",
            "Jennifer",
            "Glyn",
            "Ray",
            "Oggy",
            "Krystal"
        };

    private static string[] job1 = {
            "Lead",
            "Senior",
            "Direct",
            "Corporate",
            "Dynamic",
            "Future",
            "National",
            "Regional",
            "Assistant to the",
            "Customer",
            "Dynamic",
            "Internal",
            "Chief",
            "Principal"
        };

    private static string[] job2 = {
            "Solutions",
            "Program",
            "Brand",
            "Security",
            "Research",
            "Marketing",
            "Directives",
            "Integration",
            "Functionality",
            "Tactics",
            "Markets",
            "Group",
            "Resonance",
            "Applications",
            "Optimization",
            "Operations",
            "Infrastructure",
            "Communications",
            "Web",
            "Quality",
            "Impact",
            "Data",
            "Creative",
            "Accountability",
            "Resources",
            "Customer"
        };

    private static string[] job3 = {
            "Supervisor",
            "Associate",
            "Executive",
            "Liason",
            "Officer",
            "Manager",
            "Engineer",
            "Specialist",
            "Director",
            "Coordinator",
            "Administrator",
            "Architect",
            "Analyst",
            "Designer",
            "Synergist",
            "Technician",
            "Developer",
            "Producer",
            "Consultant",
            "Assistant",
            "Agent",
            "Representative",
            "Strategist"
        };

	// Use this for initialization
	static void Start () {
        
    }

    // Update is called once per frame
    static void Update () {
        
	}

    public static string Name()
    {
        return names[Random.Range(0, names.Length)];
    }

    public static string Job()
    {
        return job1[Random.Range(0, job1.Length)] + " " +
                job2[Random.Range(0, job2.Length)] + " " +
                job3[Random.Range(0, job3.Length)];
    }
}
